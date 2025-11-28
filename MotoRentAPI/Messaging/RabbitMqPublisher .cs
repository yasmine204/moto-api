using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace MotoRentAPI.Messaging;

public class RabbitMqPublisher : IMessagePublisher, IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqPublisher(string hostname, string username, string password)
    {
        var factory = new ConnectionFactory
        {
            HostName = hostname,
            UserName = username,
            Password = password,
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: "motorcycle-created",
            durable: true,
            exclusive: false,
            autoDelete: false
        );

        Console.WriteLine("RabbitMqPublisher inicializado.");
    }

    public Task PublishAsync<T>(T message)
    {
        ArgumentNullException.ThrowIfNull(message);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        _channel.BasicPublish("", "motorcycle-created", null, body);

        Console.WriteLine($"Mensagem publicada: {json}");
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
    }
}
