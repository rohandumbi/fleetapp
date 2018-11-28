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
    public class HubAllocationViewModel : Screen
    {
        public BindableCollection<HubAllocationModel> HubAllocations { get; set; }
        public List<HubModel>  HubModels{ get; set; }

        public BindableCollection<String> HubNames { get; set; }
        public BindableCollection<String> AssetModels { get; set; }

        public String SelectedAssetModel { get; set; }
        public String SelectedHubName { get; set; }

        private HubAllocationDataAccess _hubAllocationDataAccess;
        private HubDataAccess _hubDataAccess;
        private FleetDataAccess _fleetDataAccess;

        public bool IsMannedSelected { get; set; }
        public bool IsAHSSelected { get; set; }

        void LoadHubAllocation()
        {
            HubAllocations = new BindableCollection<HubAllocationModel>(_hubAllocationDataAccess.GetHubAllocations());
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

        public HubAllocationViewModel()
        {
            _hubAllocationDataAccess = new HubAllocationDataAccess();
            _hubDataAccess = new HubDataAccess();
            _fleetDataAccess = new FleetDataAccess();
            LoadHubAllocation();

        }
        public void CreateHubAllocation()
        {
            
            if (SelectedAssetModel == null)
            {
                MessageBox.Show("Select an asset model");
                return;
            } else if (SelectedHubName == null)
            {
                MessageBox.Show("Select a hub");
                return;
            }
            int HubId = GetHubId(SelectedHubName);
            if (HubId < 0)
            {
                MessageBox.Show("Selected Hub does not exist");
                return;
            }
            //MessageBox.Show("AssetModel: " + SelectedAssetModel + " Hub Name: " + SelectedHubName + " Hub Id: " + HubId + " isAHS: " + IsAHSSelected + " isManned: " + IsMannedSelected);
            HubAllocationModel newHubAllocation = new HubAllocationModel
            {
                ProjectId = Context.ProjectId,
                HubId = HubId,
                AssetModel = SelectedAssetModel,
                IsManned = IsMannedSelected,
                IsAHS = IsAHSSelected,
                HubName = SelectedHubName
            };
            _hubAllocationDataAccess.InsertHubAllocation(newHubAllocation);
            HubAllocations.Add(newHubAllocation);
            NotifyOfPropertyChange("HubAllocations");
        }

        private int GetHubId(String HubName)
        {
            int HubId = -1;
            foreach (HubModel Hub in HubModels)
            {
                if (Hub.Name == HubName)
                {
                    HubId = Hub.Id;
                }
            }
            return HubId;
        }
    }

}
