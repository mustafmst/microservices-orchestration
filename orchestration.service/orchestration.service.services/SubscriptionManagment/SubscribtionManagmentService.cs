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
        private readonly ILogger<SubscribtionManagmentService> logger;
        private readonly IMessageBus messageBus;
        public SubscribtionManagmentService(IMessageBus messageBus, ILogger<SubscribtionManagmentService> logger)
        {
            this.messageBus = messageBus;
            this.logger = logger;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Starting {} service!", this.GetType().Name);
            messageBus.Listen("");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Stopping {} service!", this.GetType().Name);
            messageBus.Dispose();
            return Task.CompletedTask;
        }
    }
}