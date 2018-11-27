using Caliburn.Micro;
using fleetapp.DataAccessClasses;
using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.ViewModels
{
    class TruckHubPriorityViewModel : Screen
    {
        public BindableCollection<TruckHubPriorityModel> TruckHubPriorities { get; set; }
        private TruckHubPriorityDataAccess _truckHubPriorityDataAccess;
        void LoadTruckHubPriorities()
        {
            TruckHubPriorities = new BindableCollection<TruckHubPriorityModel>(_truckHubPriorityDataAccess.GetTruckHubPriorities());
        }

        public TruckHubPriorityViewModel()
        {
            _truckHubPriorityDataAccess = new TruckHubPriorityDataAccess();
            LoadTruckHubPriorities();

        }
    }
}
