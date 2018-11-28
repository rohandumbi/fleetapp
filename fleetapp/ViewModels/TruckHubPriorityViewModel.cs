using Caliburn.Micro;
using fleetapp.DataAccessClasses;
using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace fleetapp.ViewModels
{
    class TruckHubPriorityViewModel : Screen
    {
        public BindableCollection<TruckHubPriorityModel> TruckHubPriorities { get; set; }
        private TruckHubPriorityDataAccess _truckHubPriorityDataAccess;
        public List<HubModel> HubModels { get; set; }
        public BindableCollection<String> HubNames { get; set; }
        public BindableCollection<String> AssetModels { get; set; }
        public String SelectedAssetModel { get; set; }
        public String SelectedHubName { get; set; }

        public String SelectedHubPriority { get; set; }

        private HubDataAccess _hubDataAccess;
        private FleetDataAccess _fleetDataAccess;


        void LoadTruckHubPriorities()
        {
            TruckHubPriorities = new BindableCollection<TruckHubPriorityModel>(_truckHubPriorityDataAccess.GetTruckHubPriorities());
            HubModels = new List<HubModel>(_hubDataAccess.GetHubs());
            HubNames = new BindableCollection<string>(GetHubNames());
            AssetModels = new BindableCollection<string>(_fleetDataAccess.GetAssetModels());
        }

        private List<String> GetHubNames()
        {
            List<String> hubNames = new List<string>();
            foreach (HubModel hub in HubModels)
            {
                hubNames.Add(hub.Name);
            }
            return hubNames;
        }

        public TruckHubPriorityViewModel()
        {
            _truckHubPriorityDataAccess = new TruckHubPriorityDataAccess();
            _hubDataAccess = new HubDataAccess();
            _fleetDataAccess = new FleetDataAccess();
            LoadTruckHubPriorities();

        }

        public void CreateTruckHubPriority()
        {

            if (SelectedAssetModel == null)
            {
                MessageBox.Show("Select an asset model");
                return;
            }
            else if (SelectedHubName == null)
            {
                MessageBox.Show("Select a hub");
                return;
            }
            int Priority;
            try {
                Priority = Convert.ToInt32(SelectedHubPriority);
            } catch (Exception e)
            {
                MessageBox.Show("Improper priority value");
                return;
            }
            if (Priority == 0) {
                MessageBox.Show("Priority value cannot be 0");
                return;
            }
            Console.WriteLine("AssetModel: " + SelectedAssetModel + " Hub Name: " + SelectedHubName + " Priority: " + Priority);
            TruckHubPriorityModel newTruckHubPriority = new TruckHubPriorityModel
            {
                ProjectId = Context.ProjectId,
                AssetModel = SelectedAssetModel,
                Hub = SelectedHubName,
                Priority = Priority
            };
            _truckHubPriorityDataAccess.InsertTruckHubPriority(newTruckHubPriority);
            TruckHubPriorities.Add(newTruckHubPriority);
            NotifyOfPropertyChange("TruckHubPriorities");
        }
    }
}
