using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace orchestration.service.app.Infrastructure
{
    public class HostedServiceTest : IHostedService
    {
        private IConsoleWrapper consoleWrapper;
        public HostedServiceTest(IConsoleWrapper consoleWrapper)
        {
            this.consoleWrapper = consoleWrapper;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            consoleWrapper.WriteServiceState("starting");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            consoleWrapper.WriteServiceState("stopping");
            return Task.CompletedTask;
        }
    }
}