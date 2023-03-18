using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using RabbitMQ.Client;

namespace Bot
{
    public class BotService : IBotService
    {
        public void SendStockCode(string code)
        {
            var factory = new ConnectionFactory { HostName = "rabbitmq", Port = 5672, UserName = "guest", Password = "guest", Ssl = { Enabled = false } };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "stock",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(code);
            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: "stock",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
