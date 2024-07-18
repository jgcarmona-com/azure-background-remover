using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: bgr <image_path>");
            return;
        }

        var imagePath = args[0];

        var serviceProvider = new ServiceCollection()
            .AddLogging(configure => configure.AddConsole())
            .AddSingleton<BackgroundRemover>()
            .AddSingleton<IConfiguration>(provider =>
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();

                return builder.Build();
            })
            .BuildServiceProvider();

        var remover = serviceProvider.GetService<BackgroundRemover>();
        await remover.RemoveBackgroundAsync(imagePath);
    }
}
