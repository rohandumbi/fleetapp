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
    public class TruckPayloadsViewModel : Screen
    {
        private TruckPayloadDataAccess _truckPayloadDataAccess;
        public BindableCollection<TruckPayloadModel> TruckPayloads { get; set; }

        public TruckPayloadsViewModel()
        {
            _truckPayloadDataAccess = new TruckPayloadDataAccess();
            TruckPayloads = new BindableCollection<TruckPayloadModel>(_truckPayloadDataAccess.GetTruckPayloads());
        }
    }
}
