using DirectAxis.RaceGame.DatabaseManager.ContactsImplementation;
using DirectAxis.RaceGame.DatabaseManager.Contratcs;
using DirectAxis.RaceGame.DatabaseManager.DTOs;
using DirectAxis.RaceGame.FrontEnd.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace DirectAxis.RaceGame.FrontEnd
{
    public class Program
    {
        public static void Main(string[] args)
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

            Console.Clear();
            Console.WriteLine("Please see vehicle descriptions and their attributes then make your 2 choices that will go against each other");

            //get all the vehicle types to choose from
            var vehicles = raceService.GetCarTypes();
            foreach (var v in vehicles)
            {
                Console.WriteLine($"option: {v.Id} - {v.Description}");
            }

            Console.WriteLine("Choose vehicle type 1");
            int vehicleType1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Choose vehicle type 2");
            int vehicleType2 = Convert.ToInt32(Console.ReadLine());

            var choseVehicle1 = vehicles.FirstOrDefault(x => x.Id == vehicleType1);
            var choseVehicle2 = vehicles.FirstOrDefault(x => x.Id == vehicleType2);

            if (choseVehicle1 == null || choseVehicle1 == null)
            {
                Console.WriteLine("One of the chosen vehicle is invalid, please chose the correct options");
            }

            Console.WriteLine($"Race will be between : {choseVehicle1.Description} vs {choseVehicle2.Description}");
            Console.WriteLine("Now choose the type of race the above vehicles will be participating in");

            //get all the track types from the service
            var trackTypes = raceService.GetAllTrackTypes();
            foreach (var track in trackTypes)
            {
                Console.WriteLine($"option: {track.Id} - {track.Complexity}");
            }

            int trackType = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Chosen track types is {trackType} with complexity of {trackTypes.FirstOrDefault(x => x.Id == trackType).Complexity}");

            //get the attributes of the two selected vehicles that will be racing
            var vehicle1Attribute = raceService.GetVehicleAttributes(vehicleType1);
            var vehicle2Attribute = raceService.GetVehicleAttributes(vehicleType2);

            //get the complexity(corners & straights) of the selected race-track and calulcate the score of each vehicle type on the selected race
            var chosenRaceType = trackTypes.FirstOrDefault(x => x.Id == trackType).Complexity;
            double vehicleOneResult = CalculateRaceResults(chosenRaceType, vehicle1Attribute);
            double vehicleTwoResult = CalculateRaceResults(chosenRaceType, vehicle2Attribute);

            Console.WriteLine($"{choseVehicle1.Description} scored: {vehicleOneResult} points and {choseVehicle2.Description} scored {vehicleTwoResult} points");
            if (vehicleOneResult > vehicleTwoResult)
            {
                Console.WriteLine($"{ choseVehicle1.Description} is the winner!!");
            }
            else if (vehicleOneResult < vehicleTwoResult)
            {
                Console.WriteLine($"{ choseVehicle2.Description} is the winner!!");
            }
            else
            {
                Console.WriteLine($"It's a draw");
            }

            Console.ReadKey();
            logger.LogDebug("All done!");
        }

        private static double CalculateRaceResults(string raceComplexity, VehicleAttributeDto vehicleAttributeDto)
        {
            double straightResults = 0;
            double cornerResults = 0;

            var racePoints = raceComplexity.ToCharArray();
            for (int x = 0; x < racePoints.Length; x++)
            {
                if (char.GetNumericValue(racePoints[x]) == (int)RacePointTypes.Straight)
                {
                    straightResults += VehicleStraightResults(vehicleAttributeDto);
                }
                else
                {
                    cornerResults += VehicleCorneringResults(vehicleAttributeDto);
                }
            }
            return straightResults + cornerResults;
        }

        private static int VehicleStraightResults(VehicleAttributeDto vehicleAttributeDto)
        {
            return vehicleAttributeDto.Accelaration + vehicleAttributeDto.TopSpeed;
        }
        private static int VehicleCorneringResults(VehicleAttributeDto vehicleAttributeDto)
        {
            return vehicleAttributeDto.Cornering + vehicleAttributeDto.Breaking;
        }
    }
}
