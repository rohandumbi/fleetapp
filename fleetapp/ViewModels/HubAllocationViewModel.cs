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
        private HubAllocationDataAccess _hubAllocationDataAccess;
        void LoadHubAllocation()
        {
            HubAllocations = new BindableCollection<HubAllocationModel>(_hubAllocationDataAccess.GetHubAllocations());
        }
        public HubAllocationViewModel()
        {
            _hubAllocationDataAccess = new HubAllocationDataAccess();
            
        }
        public void CreateHubAllocation()
        {
            HubAllocationModel newHubAllocation = new HubAllocationModel
            {
                ProjectId = Context.ProjectId,
            };
            _hubAllocationDataAccess.InsertHubAllocation(newHubAllocation);
            HubAllocations.Add(newHubAllocation);
            NotifyOfPropertyChange("HubAllocations");
        }
    }

}
