using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using orchestration.service.core.Common;
using orchestration.service.services.Common;
using orchestration.service.services.SubscribtionManagmentService;

namespace orchestration.service.app
{
    class Program
    
    {
        public static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IMessageBus,RabbitMQMessageBus>();
                    services.AddHostedService<SubscribtionManagmentService>();
                });

            await builder.Build().RunAsync();
        }
    }
}