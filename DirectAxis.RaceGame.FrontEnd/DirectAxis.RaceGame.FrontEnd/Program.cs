﻿using DirectAxis.RaceGame.DatabaseManager.ContactsImplementation;
using DirectAxis.RaceGame.DatabaseManager.Contratcs;
using DirectAxis.RaceGame.DatabaseManager.DTOs;
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
            Console.WriteLine("Please see vehicle descriptions and their attributes then make your 2 choice that will go against ultimate");

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

            Console.WriteLine($"Race will be between : {choseVehicle1.Description} vs {choseVehicle2.Description}");
            Console.WriteLine("Now choose the type of race the above vehicles will be participating in");

            var trackTypes = raceService.GetAllTrackTypes();

            foreach (var track in trackTypes)
            {
                Console.WriteLine($"option: {track.Id} - {track.Complexity}");
            }

            int trackType = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Chosen track types is {trackType} with complexity of {trackTypes.FirstOrDefault(x => x.Id == trackType).Complexity}");

            var vehicle1Attribute = raceService.GetVehicleAttributes(vehicleType1);
            var vehicle2Attribute = raceService.GetVehicleAttributes(vehicleType2);

            double vehicleOneResult = CalculateRaceResults(1, trackTypes.FirstOrDefault(x => x.Id == trackType).Complexity, vehicle1Attribute);
            double vehicleTwoResult = CalculateRaceResults(1, trackTypes.FirstOrDefault(x => x.Id == trackType).Complexity, vehicle2Attribute);

            Console.WriteLine($"{choseVehicle1.Description} scored: {vehicleOneResult} points and {choseVehicle2.Description} scored {vehicleTwoResult} points");

            Console.ReadKey();
            logger.LogDebug("All done!");
        }

        private static double CalculateRaceResults(int vehicleType, string raceComplexity, VehicleAttributeDto vehicleAttributeDto)
        {
            double straightResults = 0;
            double cornerResults = 0;

            var racePoints = raceComplexity.ToCharArray();
            for (int x = 0; x < racePoints.Length; x++)
            {
                if (char.GetNumericValue(racePoints[x]) == 1)
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
