using DirectAxis.RaceGame.Data.Models;
using DirectAxis.RaceGame.DatabaseManager.Contratcs;
using DirectAxis.RaceGame.DatabaseManager.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectAxis.RaceGame.DatabaseManager.ContactsImplementation
{
    public class RaceManager : IRaceManager
    {
        private readonly DirectAxisContext _directAxisContext = new DirectAxisContext();
        //public DatabaseManager() { }
        public List<CarTypeDto> GetCarTypes()
        {
            return _directAxisContext.CarTypes.Select(x => new CarTypeDto
            {
                Id = x.Id,
                Description = x.Description
            }).ToList();
        }

        public List<TrackDto> GetAllTrackTypes()
        {
            return _directAxisContext.Tracks.Select(x => new TrackDto
            {
                Id = x.Id,
                Complexity = x.Complexity
            }).ToList();
        }

        public VehicleAttributeDto GetVehicleAttributes(int vehicleId)
        {
            var vehicleAttributes = new VehicleAttributeDto();
            var dbModel = _directAxisContext.VehicleAttributes.FirstOrDefault(x => x.VehicleTypeId == vehicleId);
            if (dbModel != null)
            {

                vehicleAttributes.Accelaration = dbModel.Accelaration;
                vehicleAttributes.Breaking = dbModel.Breaking;
                vehicleAttributes.Cornering = dbModel.Cornering;
                vehicleAttributes.TopSpeed = dbModel.TopSpeed;
            }

            return vehicleAttributes;
        }

        public List<FullVehicleSpec> GetVehicleSpec()
        {
            List<FullVehicleSpec> fullVehicleSpecsList = new List<FullVehicleSpec>();
            var dataItems = (from va in _directAxisContext.VehicleAttributes
                             join ca in _directAxisContext.CarTypes on va.VehicleTypeId equals ca.Id
                             select new
                             {
                                 va.Id,
                                 va.Accelaration,
                                 va.Breaking,
                                 va.Cornering,
                                 ca.Description,
                                 va.TopSpeed,
                                 va.VehicleTypeId
                             }).ToList();

            foreach (var item in dataItems)
            {
                fullVehicleSpecsList.Add(new FullVehicleSpec
                {
                    Accelaration = item.Accelaration,
                    Breaking = item.Breaking,
                    Cornering = item.Cornering,
                    VehicleName = item.Description,
                    TopSpeed = item.TopSpeed,
                    VehicleTypeId = item.VehicleTypeId,
                    Id = item.Id
                });
            }

            return fullVehicleSpecsList;
        }
    }
}
