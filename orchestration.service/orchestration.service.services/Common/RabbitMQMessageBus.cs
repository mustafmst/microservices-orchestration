using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using orchestration.service.core.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace orchestration.service.services.Common
{
    public class RabbitMQMessageBus : IMessageBus, IDisposable
    {
        private IConnection connection;
        private IModel channel;

        public RabbitMQMessageBus()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }

        public void Dispose()
        {
            Console.WriteLine("Disposing...");
            connection.Dispose();
            channel.Dispose();
        }

        public void Listen(string channelName)
        {
            channel.QueueDeclare(queue: "task_queue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("\t[x] Received:\n\t\t{0}\n", message);
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            channel.BasicConsume(queue: "task_queue",
                                 autoAck: false,
                                 consumer: consumer);
        }
    }
}