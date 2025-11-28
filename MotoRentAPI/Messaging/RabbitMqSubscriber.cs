using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MotoRentAPI.Messaging;

public class RabbitMqSubscriber : IMessageSubscriber
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqSubscriber(string hostname, string username, string password)
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

        Console.WriteLine("RabbitMqSubscriber inicializado.");
    }

    public void Subscribe<T>(Func<T, Task> handler)
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);
        Console.WriteLine("Subscribe chamado: aguardando mensagens...");

        consumer.Received += async (sender, e) =>
        {
            try
            {
                var body = e.Body.ToArray();
                var message = JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(body));

                if (message != null)
                {
                    Console.WriteLine($"Evento recebido: {JsonSerializer.Serialize(message)}");
                    await handler(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar mensagem: {ex.Message}");
            }
            finally
            {
                _channel.BasicAck(e.DeliveryTag, false);
            }
        };

        _channel.BasicConsume(queue: "motorcycle-created", autoAck: false, consumer: consumer);
    }
}
