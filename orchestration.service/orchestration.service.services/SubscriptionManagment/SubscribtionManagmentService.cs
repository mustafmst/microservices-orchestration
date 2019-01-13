using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using orchestration.service.core.Common;

namespace orchestration.service.services.SubscribtionManagmentService
{
    public class SubscribtionManagmentService : IHostedService
    {
        private IMessageBus messageBus;
        public SubscribtionManagmentService(IMessageBus messageBus)
        {
            this.messageBus = messageBus;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
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