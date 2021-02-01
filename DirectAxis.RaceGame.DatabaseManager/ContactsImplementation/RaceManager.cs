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
    }
}
