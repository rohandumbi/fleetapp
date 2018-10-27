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
    public class HubDefinitionViewModel : Screen
    {
        private HubDataAccess _hubDataAccess;
        public BindableCollection<HubModel> HubDefinitions { get; set; }
        private String _newHubName;

        public HubDefinitionViewModel()
        {
            _hubDataAccess = new HubDataAccess();
            LoadHubs();
        }

        private void LoadHubs()
        {
            HubDefinitions = new BindableCollection<HubModel>(_hubDataAccess.GetHubs());
        }
        public String HubName
        {
            set { _newHubName = value; }
        }

        public void AddHub()
        {
            _hubDataAccess.InsertHub(_newHubName);
            LoadHubs();
            NotifyOfPropertyChange("HubDefinitions");
        }
        
    }
}
