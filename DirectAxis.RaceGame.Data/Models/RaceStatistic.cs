using System;
using System.Collections.Generic;

#nullable disable

namespace DirectAxis.RaceGame.Data.Models
{
    public partial class RaceStatistic
    {
        public int Id { get; set; }
        public int CarTypeId { get; set; }
        public int TrackTypeId { get; set; }
        public int Score { get; set; }
        public DateTime DateRaced { get; set; }
    }
}
