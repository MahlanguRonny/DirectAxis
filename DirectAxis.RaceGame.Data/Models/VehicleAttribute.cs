using System;
using System.Collections.Generic;

#nullable disable

namespace DirectAxis.RaceGame.Data.Models
{
    public partial class VehicleAttribute
    {
        public int Id { get; set; }
        public int VehicleTypeId { get; set; }
        public int Accelaration { get; set; }
        public int Breaking { get; set; }
        public int Cornering { get; set; }
        public int TopSpeed { get; set; }
    }
}
