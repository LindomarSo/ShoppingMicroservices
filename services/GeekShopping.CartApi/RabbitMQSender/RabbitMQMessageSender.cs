using GeekShopping.CartApi.Messages;
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
        // Abrimos uma conexão com um nó RabbitMQ
        _connection = factory.CreateConnection();
        /*
            Criamos um canal onde vamos definir uma fila, uma mensagem e publicar a mensagem 
            Cada operação realizada por um cliente é feita em um canal e um canal só existe no contexto de uma conexão se eu fechar a conexão todos os canais também 
            serão fechados.
         */
        using var channel = _connection.CreateModel();


        channel.QueueDeclare(queueName, // Nome da fila
                            durable: false, // Se igual a true a fila permanece ativa após o servidor ser reiniciado
                            exclusive: false, // Se igual a true ela só pode ser acessada via conexão atual são excluídas ao fechar a conexão
                            autoDelete: false, // Se igual a true será deletada automaticamente após os consumidores usar a fila
                            arguments: null // 
                            );


        byte[] body = GetBytes(message);

        // Publicamos a mensagem informando a fila e o corpo da mensagem
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
