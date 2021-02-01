using System;
using System.Collections.Generic;
using System.Text;

namespace DirectAxis.RaceGame.DatabaseManager.DTOs
{
    public class RaceStatisticDto
    {
        public int Id { get; set; }
        public int CarTypeId { get; set; }
        public int TrackTypeId { get; set; }
        public int Score { get; set; }
        public DateTime DateRaced { get; set; }
    }
}
