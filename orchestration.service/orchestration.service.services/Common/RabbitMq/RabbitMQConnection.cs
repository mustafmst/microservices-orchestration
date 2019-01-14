using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace orchestration.service.services.Common.RabbitMQ
{
    public class RabbitMQConnection : IDisposable
    {
        public string channelName
        {
            get;
            private set;
        }
        private IConnection connection;
        private IModel model;
        private EventingBasicConsumer consumer;
        public RabbitMQConnection(string channelName)
        {
            this.channelName = channelName;
            var factory = new ConnectionFactory() { HostName = "localhost" };
            this.connection = factory.CreateConnection();
            this.model = connection.CreateModel();
            model.QueueDeclare(queue: channelName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            model.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            this.consumer = new EventingBasicConsumer(model);
        }

        public RabbitMQConnection RegisterRecievingEvent(Action<object, BasicDeliverEventArgs> action)
        {
            consumer.Received += (model, ea) => action(model, ea);
            return this;
        }

        public void Listen()
        {
            model.BasicConsume(queue: channelName,
                                 autoAck: true,
                                 consumer: consumer);
        }
        public void Dispose()
        {
            model.Close();
            connection.Close();
            model.Dispose();
            connection.Dispose();
        }
    }
}