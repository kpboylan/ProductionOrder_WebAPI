using ProductionOrder_WebAPI.Model;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace ProductionOrder_WebAPI.Send
{
    public class SendProdOrder
    {
        public string Send(Order prodOrder)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "ProdOrder",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var json = JsonConvert.SerializeObject(prodOrder);

            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: "ProdOrder",
                                 basicProperties: null,
                                 body: body);

            return "Success";
        }
    }
}
