﻿using DirectAxis.RaceGame.Data.Models;
using DirectAxis.RaceGame.DatabaseManager.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DirectAxis.RaceGame.DatabaseManager.Contratcs
{    
    public interface IRaceManager
    {
        List<CarTypeDto> GetCarTypes();
        List<TrackDto> GetAllTrackTypes();
    }
}
