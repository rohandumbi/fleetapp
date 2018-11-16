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
    public class TruckGroupsViewModel : Screen
    {
        private TruckGroupDataAccess _truckGroupDataAccess;
        public BindableCollection<TruckGroupModel> TruckGroups { get; set; }

        public TruckGroupsViewModel()
        {
            _truckGroupDataAccess = new TruckGroupDataAccess();
            TruckGroups = new BindableCollection<TruckGroupModel>(_truckGroupDataAccess.GetTruckGroups());
        }
    }
}
