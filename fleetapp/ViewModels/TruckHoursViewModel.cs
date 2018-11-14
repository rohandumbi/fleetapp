﻿using Caliburn.Micro;
using fleetapp.DataAccessClasses;
using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.ViewModels
{
    public class TruckHoursViewModel : Screen
    {
        TruckHousDataAccess da;
        public BindableCollection<TruckHoursModel> TruckHours { get; set; }

        public TruckHoursViewModel()
        {
            da = new TruckHousDataAccess();
            TruckHours = new BindableCollection<TruckHoursModel>(da.GetTruckHours());
        }
    }
}