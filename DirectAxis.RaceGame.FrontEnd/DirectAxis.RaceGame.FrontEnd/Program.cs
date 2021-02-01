using DirectAxis.RaceGame.DatabaseManager.ContactsImplementation;
using DirectAxis.RaceGame.DatabaseManager.Contratcs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace DirectAxis.RaceGame.FrontEnd
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IRaceManager, RaceManager>()
                .BuildServiceProvider();

            //configure console logging
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            //do the actual work here
            var raceService = serviceProvider.GetService<IRaceManager>();

            //TODO remove this line it is just for initial testing.
            Console.Write(raceService.GetAllTrackTypes().Count);
            Console.ReadKey();

            logger.LogDebug("All done!");
        }
    }
}
