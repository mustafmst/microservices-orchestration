using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using orchestration.service.core.Common;

namespace orchestration.service.services.SubscribtionManagmentService
{
    public class SubscribtionManagmentService : IHostedService
    {
        private ILogger<SubscribtionManagmentService> logger;
        private IMessageBus messageBus;
        public SubscribtionManagmentService(IMessageBus messageBus, ILogger<SubscribtionManagmentService> logger)
        {
            this.messageBus = messageBus;
            this.logger = logger;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Starting {} service!", this.GetType().Name);
            messageBus.Listen("aaa");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            messageBus.Dispose();
            Console.WriteLine("Stopping SubscribtionManagmentService");
            return Task.CompletedTask;
        }
    }
}