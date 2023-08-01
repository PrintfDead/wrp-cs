using SampSharp.Core;
using System;
using WashingtonRP.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using WashingtonRP.Structures;

namespace WashingtonRP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new GameModeBuilder()
                .Use<GameMode>()
                .UseEncodingCodePage("cp1252")
                .Run();
            /*new HostBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.Build();
                })
                .ConfigureServices((services) =>
                {
                    var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));

                    services.AddDbContext<WashingtonContext>(
                    dbContextOptions => dbContextOptions
                        .UseMySql("server=localhost;user=root;password=;database=washington", serverVersion)
                        .LogTo(Console.WriteLine, LogLevel.Debug)
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors()
                    );
                    Console.WriteLine(">>> Connection Washington Database");
                })
                .ConfigureLogging((context, logging) =>
                {
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Debug);
                })
                .Build();*/
        }
    }
}
