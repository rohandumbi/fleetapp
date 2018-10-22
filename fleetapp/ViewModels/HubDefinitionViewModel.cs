﻿using Caliburn.Micro;
using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace fleetapp.ViewModels
{
    public class HubDefinitionViewModel : Screen
    {
        public BindableCollection<HubDefinitionModel> HubDefinitions { get; set; }

        public HubDefinitionViewModel()
        {
            DataAccess da = new DataAccess();
            HubDefinitions = new BindableCollection<HubDefinitionModel>(da.GetHubDefinitions());
        }
    }
}
