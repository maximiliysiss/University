using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PeopleAnalysis.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PeopleAnalysis.Services
{
    public interface IMessageReceiver
    {
        Task StartAsync();
    }

    public class RabbitMQService : IMessageReceiver, IHostedService
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IAIService aIService;
        private readonly ConnectionFactory factory;

        public RabbitMQService(IAIService aIService, IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
            this.aIService = aIService;
            factory = new ConnectionFactory() { HostName = "localhost" };
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: "task_queue",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (sender, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    try
                    {
                        using var scope = scopeFactory.CreateScope();
                        await aIService.ProcessTaskAsync(JsonConvert.DeserializeObject<Request>(message), scope.ServiceProvider.GetService<IAnaliticAIService>(),
                            scope.ServiceProvider.GetRequiredService<DatabaseContext>(), scope.ServiceProvider.GetRequiredService<ApisManager>());
                    }
                    catch (Exception) { }
                    finally
                    {
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                };
                channel.BasicConsume(queue: "task_queue",
                                     autoAck: false,
                                     consumer: consumer);
                while (!cancellationToken.IsCancellationRequested) ;
            });
            return Task.CompletedTask;
        }

        public Task StartAsync()
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
