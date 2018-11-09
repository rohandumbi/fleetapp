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
    public class HubAllocationViewModel : Screen
    {
        public BindableCollection<HubAllocationModel> HubAllocations { get; set; }
        private HubAllocationDataAccess da;
        void LoadHubAllocation()
        {
            HubAllocations = new BindableCollection<HubAllocationModel>(da.GetHubAllocations());
        }
        public HubAllocationViewModel()
        {
            da = new HubAllocationDataAccess();
            
        }
    }

}
