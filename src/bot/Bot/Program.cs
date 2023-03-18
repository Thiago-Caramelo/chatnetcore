using System.Text;
using Bot;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory { HostName = "rabbitmq", Port = 5672, UserName = "guest", Password = "guest", Ssl = { Enabled = false } };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "stock",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    try
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);

        Console.WriteLine($"Received: {message}");

        Stock.ProcessStock(message);
    }
    catch (Exception ex)
    {
        Console.WriteLine("There was error: ", ex.Message + ex.StackTrace);
    }
};

channel.BasicConsume(queue: "stock",
                     autoAck: true,
                     consumer: consumer);

Console.WriteLine("Listening to messages.");
Console.ReadLine();