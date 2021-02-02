using DirectAxis.RaceGame.DatabaseManager.ContactsImplementation;
using DirectAxis.RaceGame.DatabaseManager.Contratcs;
using DirectAxis.RaceGame.DatabaseManager.DTOs;
using DirectAxis.RaceGame.FrontEnd.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

            var raceService = serviceProvider.GetService<IRaceManager>();


            Console.Clear();
            Console.WriteLine("Please see vehicle descriptions and their attributes then make your 2 choices that will go against each other");
            Console.WriteLine();

            var vehicleSpecList = raceService.GetVehicleSpec();
            Console.WriteLine("Name - Cornering - Breaking - Acceleration - Top Speed");
            foreach (var vehicle in vehicleSpecList)
            {
                Console.WriteLine($"{vehicle.VehicleName} - {vehicle.Cornering} - {vehicle.Breaking} -{vehicle.Accelaration} - {vehicle.TopSpeed}");
            }

            Console.WriteLine();

            //get all the vehicle types to choose from
            var vehicles = raceService.GetCarTypes();
            foreach (var v in vehicles)
            {
                Console.WriteLine($"option: {v.Id} - {v.Description}");
            }

            Console.WriteLine();
            Console.WriteLine("Choose vehicle type 1");
            int vehicleType1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Choose vehicle type 2");
            int vehicleType2 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Choose vehicle type 3");
            int vehicleType3 = Convert.ToInt32(Console.ReadLine());

            var choseVehicle1 = vehicles.FirstOrDefault(x => x.Id == vehicleType1);
            var choseVehicle2 = vehicles.FirstOrDefault(x => x.Id == vehicleType2);
            var choseVehicle3 = vehicles.FirstOrDefault(x => x.Id == vehicleType3);

            Console.WriteLine();
            if (choseVehicle1 == null || choseVehicle1 == null || choseVehicle3 == null)
            {
                Console.WriteLine("One of the chosen vehicle is invalid, please chose the correct options");
            }

            Console.WriteLine($"Race will be between : {choseVehicle1.Description} vs {choseVehicle2.Description} vs {choseVehicle3.Description}");
            Console.WriteLine();
            Console.WriteLine("Now choose the type of race the above vehicles will be participating in");

            //get all the track types from the service
            var trackTypes = raceService.GetAllTrackTypes();
            foreach (var track in trackTypes)
            {
                Console.WriteLine($"option: {track.Id} - {track.Complexity}");
            }

            Console.WriteLine();
            int trackType = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Chosen track types is {trackType} with complexity of {trackTypes.FirstOrDefault(x => x.Id == trackType).Complexity}");

            //get the attributes of the two selected vehicles that will be racing
            var vehicle1Attribute = raceService.GetVehicleAttributes(vehicleType1);
            var vehicle2Attribute = raceService.GetVehicleAttributes(vehicleType2);
            var vehicle3Attribute = raceService.GetVehicleAttributes(vehicleType3);

      
            //add each result to a list and save the list to the DB rather than calling a db each time a new result is calculated
            List<RaceStatisticDto> statsList = new List<RaceStatisticDto>();
            var chosenRaceType = trackTypes.FirstOrDefault(x => x.Id == trackType).Complexity;
            double vehicleOneResult = CalculateRaceResults(chosenRaceType, vehicle1Attribute);
            statsList.Add(new RaceStatisticDto { CarTypeId = vehicleType1, DateRaced = DateTime.Now, Score = (int)vehicleOneResult, TrackTypeId = trackType });

            double vehicleTwoResult = CalculateRaceResults(chosenRaceType, vehicle2Attribute);
            statsList.Add(new RaceStatisticDto { CarTypeId = vehicleType2, DateRaced = DateTime.Now, Score = (int)vehicleTwoResult, TrackTypeId = trackType });
            double vehicleThreeResult = CalculateRaceResults(chosenRaceType, vehicle3Attribute);
            statsList.Add(new RaceStatisticDto { CarTypeId = vehicleType3, DateRaced = DateTime.Now, Score = (int)vehicleThreeResult, TrackTypeId = trackType });

            raceService.SaveResults(statsList);

            Console.WriteLine();
            Console.WriteLine("See final scores below from highest to lowest");
            var statsResults = raceService.GetLatestResultStats();
            foreach (var stat in statsResults)
            {
                Console.WriteLine($"{stat.VehicleTypeDescription} - {stat.Score}");
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
