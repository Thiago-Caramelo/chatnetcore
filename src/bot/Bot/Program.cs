using System.Text;
using Bot;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory();
var endpoints = new List<AmqpTcpEndpoint> {
  new AmqpTcpEndpoint("localhost")
};
using var connection = factory.CreateConnection(endpoints);
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

        var currentQuote = Stock.ProcessStock(message);

        channel.QueueDeclare(queue: "stock-result",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        body = Encoding.UTF8.GetBytes(currentQuote);
        channel.BasicPublish(exchange: string.Empty,
                             routingKey: "stock-result",
                             basicProperties: null,
                             body: body);

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