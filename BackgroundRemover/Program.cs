using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: bgr -i <image_path> | -f <folder_path>");
            return;
        }

        var option = args[0];
        var path = args[1];

        var exeDirectory = AppContext.BaseDirectory;
        var appSettingsPath = Path.Combine(exeDirectory, "appsettings.json");

        var builder = new ConfigurationBuilder()
            .SetBasePath(exeDirectory)
            .AddJsonFile(appSettingsPath, optional: false, reloadOnChange: true);

        IConfiguration configuration = builder.Build();

        var serviceProvider = new ServiceCollection()
            .AddLogging(configure => 
            {
                configure.AddSimpleConsole(options =>
                {
                    options.SingleLine = true;
                    options.TimestampFormat = "hh:mm:ss ";
                    options.IncludeScopes = false;
                }).SetMinimumLevel(LogLevel.Warning);
            })
            .AddSingleton(configuration)
            .AddSingleton<BackgroundRemover>()
            .BuildServiceProvider();

        var remover = serviceProvider.GetService<BackgroundRemover>();

        if (option == "-i" || option == "--image")
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"The file {path} does not exist.");
                return;
            }

            await remover.RemoveBackgroundAsync(path);
        }
        else if (option == "-f" || option == "--folder")
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine($"The folder {path} does not exist.");
                return;
            }

            await remover.RemoveBackgroundFromFolderAsync(path);
        }
        else
        {
            Console.WriteLine("Invalid option. Use -i for image or -f for folder.");
        }
    }
}
