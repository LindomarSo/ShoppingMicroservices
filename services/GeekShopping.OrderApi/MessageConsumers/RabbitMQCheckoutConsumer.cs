using GeekShopping.OrderApi.Messages;
using GeekShopping.OrderApi.Models;
using GeekShopping.OrderApi.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace GeekShopping.OrderApi.MessageConsumers;

public class RabbitMQCheckoutConsumer : BackgroundService
{
    private readonly OrderRepository _orderRepository;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    const string _queueName = "checkoutQueue"; 

    public RabbitMQCheckoutConsumer(OrderRepository orderRepository)
    {
        _orderRepository = orderRepository;

        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest",
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(_queueName, false, false, false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (chanel, evt) =>
        {
            var content = Encoding.UTF8.GetString(evt.Body.ToArray());
            CheckoutHeaderDto dto = JsonSerializer.Deserialize<CheckoutHeaderDto>(content)!;
            ProcessOrder(dto).GetAwaiter().GetResult();

            // Remover a mensagem da fila
            _channel.BasicAck(evt.DeliveryTag, false);
        };

        _channel.BasicConsume(_queueName, false, consumer);
        return Task.CompletedTask;
    }

    private async Task ProcessOrder(CheckoutHeaderDto dto)
    {
        OrderHeader order = new()
        {
            UserId = dto.UserId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            CardNumber = dto.CardNumber,
            CartTotalItems = dto.CartTotalItems,
            CouponCode = dto.CouponCode,
            Email = dto.Email,
            CVV = dto.CVV,
            ExpireMonthYear = dto.ExpireMonthYear,
            OrderTime = DateTime.Now,
            PurchaseAmount = dto.PurchaseAmount,
            PaymentStatus = false,
            PhoneNumber = dto.PhoneNumber,
            DateTime = dto.DateTime
        };

        foreach(var details in dto.CartDetails)
        {
            OrderDetail detail = new()
            {
                ProductId = details.ProductId,
                ProductName = details.Product?.Name,
                Price = details.Product!.Price,
                Count = details.Count, 
            };

            order.CartTotalItems += detail.Count;
            order.OrderDetails.Add(detail);
        }

        await _orderRepository.AddOrderAsync(order);
    }
}
