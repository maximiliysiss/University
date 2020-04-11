using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace PeopleAnalysis.Services
{
    public interface ISender
    {
        void Send<T>(T message);
    }

    public class RabbitMQClient : ISender
    {
        private readonly ConnectionFactory factory;

        public RabbitMQClient()
        {
            factory = new ConnectionFactory() { HostName = "localhost" };
        }

        public void Send<T>(T message)
        {
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "task_queue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: "",
                                 routingKey: "task_queue",
                                 basicProperties: properties,
                                 body: body);
        }
    }
}
