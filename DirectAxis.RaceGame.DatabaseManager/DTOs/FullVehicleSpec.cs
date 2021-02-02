using System;
using System.Collections.Generic;
using System.Text;

namespace DirectAxis.RaceGame.DatabaseManager.DTOs
{
    public class FullVehicleSpec
    {
        public int Id { get; set; }
        public int VehicleTypeId { get; set; }
        public int Accelaration { get; set; }
        public int Breaking { get; set; }
        public int Cornering { get; set; }
        public int TopSpeed { get; set; }
        public string VehicleName { get; set; }
    }
}
