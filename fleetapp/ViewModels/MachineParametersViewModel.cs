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
    public class MachineParametersViewModel : Screen
    {
        //public BindableCollection<HubAllocationModel> HubAllocations { get; set; }

        public MachineParametersViewModel()
        {
            //HubAllocationDataAccess da = new HubAllocationDataAccess();
            //HubAllocations = new BindableCollection<HubAllocationModel>(da.GetHubAllocations());
        }
    }
}
