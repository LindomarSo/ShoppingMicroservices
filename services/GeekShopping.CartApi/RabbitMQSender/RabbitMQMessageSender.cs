﻿using GeekShopping.CartApi.Messages;
using GeekShopping.MessageBus;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GeekShopping.CartApi.RabbitMQSender;

public class RabbitMQMessageSender : IRabbitMQMessageSender
{
    private readonly string _hostName;
    private readonly string _password;
    private readonly string _userName;
    private IConnection? _connection;
    public RabbitMQMessageSender()
    {
        _hostName = "localhost";
        _password = "guest";
        _userName = "guest";
    }

    public void SendMessage(BaseMessage message, string queueName)
    {
        var factory = new ConnectionFactory
        {
            HostName = _hostName,
            Password = _password,
            UserName = _userName,
        };

        _connection = factory.CreateConnection();

        using var channel = _connection.CreateModel();
        channel.QueueDeclare(queueName, false, false, false, null);
        byte[] body = GetBytes(message);

        channel.BasicPublish(exchange: string.Empty, routingKey: queueName, basicProperties: null, body: body);
    }

    private static byte[] GetBytes(BaseMessage message)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true, // Adicionar identação
        };

        var json = JsonSerializer.Serialize((CheckoutHeaderDto)message, options);
        return Encoding.UTF8.GetBytes(json);
    }
}