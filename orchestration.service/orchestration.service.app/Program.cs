using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using orchestration.service.app.Infrastructure;

namespace orchestration.service.app
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .UseEnvironment(EnvironmentName.Development)
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IConsoleWrapper, ConsoleWrapper>();
                    services.AddHostedService<NewHostedService>();
                    services.AddHostedService<HostedServiceTest>();
                });

            await builder.Build().RunAsync();
        }
    }
}