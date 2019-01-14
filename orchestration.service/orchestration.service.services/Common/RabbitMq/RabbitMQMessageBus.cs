using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using orchestration.service.core.Common;
using RabbitMQ.Client;

namespace orchestration.service.services.Common.RabbitMQ
{
    public class RabbitMQMessageBus : IMessageBus, IDisposable
    {
        private readonly ILogger<RabbitMQMessageBus> logger;
        private const string channel = "task_queue";

        private RabbitMQConnection connection;
        public RabbitMQMessageBus(ILogger<RabbitMQMessageBus> logger)
        {
            this.logger = logger;
        }
        public void Dispose()
        {
            logger.LogInformation("Disposing...");
            connection.Dispose();
        }

        public void Listen(string channelName)
        {
            logger.LogInformation("Starting new connection for channel: {}", channel);
            connection = new RabbitMQConnection(channel);

            logger.LogInformation("Regiser reciever event action for channel: {}", channel);
            connection.RegisterRecievingEvent((model, ea) => {
                logger.LogInformation("=> Recieved message:\n\t\t{}", Encoding.UTF8.GetString(ea.Body));
            });

            logger.LogInformation("Start listening to channel: {}", channel);
            connection.Listen();
        }
    }
}