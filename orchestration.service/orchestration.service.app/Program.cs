using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using orchestration.service.core.Common;
using orchestration.service.services.Common;
using orchestration.service.services.Common.RabbitMQ;
using orchestration.service.services.SubscribtionManagmentService;

namespace orchestration.service.app
{
    class Program
    
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IMessageBus,RabbitMQMessageBus>();
                    services.AddHostedService<SubscribtionManagmentService>();
                })
                .ConfigureLogging(builder => {
                    builder.AddConsole();
                })
                .Build();

            await host.RunAsync();
        }
    }
}