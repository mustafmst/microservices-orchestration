using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using orchestration.service.core.Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace orchestration.service.services.Common
{
    public class RabbitMQMessageBus : IMessageBus
    {
        public void Listen(string channelName)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("tmp_queue", true, false, false, null);
                    channel.BasicQos(0, 1, false);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) => {
                        Console.WriteLine($"message: {ea.Body}");
                        channel.BasicAck(ea.DeliveryTag, false);
                    };
                    Console.WriteLine("Start listening");
                    channel.BasicConsume("tmp_queue", false, consumer);
                }
            }
        }
    }
}