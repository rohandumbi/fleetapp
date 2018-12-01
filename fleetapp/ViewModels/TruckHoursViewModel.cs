using Caliburn.Micro;
using fleetapp.DataAccessClasses;
using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace fleetapp.ViewModels
{
    public class TruckHoursViewModel : Screen
    {
        TruckHourDataAccess da;
        public BindableCollection<TruckHourModel> TruckHours { get; set; }
        private ScenarioDataAccess _ScenarioDataAccess;
        private ScenarioModel Scenario;
        public List<HubModel> HubModels { get; set; }
        public BindableCollection<String> HubNames { get; set; }
        public BindableCollection<String> AssetModels { get; set; }
        public BindableCollection<String> ModeNames { get; set; }

        public String SelectedAssetModel { get; set; }
        public String SelectedHubName { get; set; }
        public String SelectedMode { get; set; }

        private HubDataAccess _hubDataAccess;
        private FleetDataAccess _fleetDataAccess;

        public TruckHoursViewModel()
        {
            da = new TruckHourDataAccess();
            TruckHours = new BindableCollection<TruckHourModel>(da.GetTruckHours());
            foreach (TruckHourModel truckHourModel in TruckHours)
            {
                truckHourModel.PropertyChanged += truckHour_PropertyChanged;
                foreach (TruckHourYearMappingModel truckHourYearMapping in truckHourModel.TruckHourYearMapping)
                {
                    truckHourYearMapping.PropertyChanged += truckHourMapping_PropertyChanged;
                }
            }
            _ScenarioDataAccess = new ScenarioDataAccess();
            Scenario = _ScenarioDataAccess.GetScenario(Context.ScenarioId);
            _hubDataAccess = new HubDataAccess();
            _fleetDataAccess = new FleetDataAccess();
            HubModels = new List<HubModel>(_hubDataAccess.GetHubs());
            HubNames = new BindableCollection<string>(GetHubNames());
            ModeNames = new BindableCollection<string>(new string[] { "AHS", "Manned" });
            AssetModels = new BindableCollection<string>(_fleetDataAccess.GetAssetModels());
            this.TruckHoursColumns = new ObservableCollection<DataGridColumn>();
            this.GenerateDefaultColumns();
        }

        private void truckHour_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //Do stuff with fleet here
            //TruckHubPriorityModel updatedTruckHubPriorityModel = (TruckHubPriorityModel)sender;
            //_truckHubPriorityDataAccess.UpdateTruckHubPriority(updatedTruckHubPriorityModel);
            //NotifyOfPropertyChange(() => TruckHubPriorities);
            MessageBox.Show("detected change");
        }

        private void truckHourMapping_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //Do stuff with fleet here
            //TruckHubPriorityModel updatedTruckHubPriorityModel = (TruckHubPriorityModel)sender;
            //_truckHubPriorityDataAccess.UpdateTruckHubPriority(updatedTruckHubPriorityModel);
            //NotifyOfPropertyChange(() => TruckHubPriorities);
            TruckHourYearMappingModel UpdatedTruckHourYearMappingModel = (TruckHourYearMappingModel)sender;
            TruckHourModel UpdatedTruckHourModel = GetTruckHourById(UpdatedTruckHourYearMappingModel.TruckHourId);
            if (UpdatedTruckHourModel == null)
            {
                MessageBox.Show("Could not find truck hour to update. Contact administrator");
                return;
            }
            da.DeleteTruckHourMapping(UpdatedTruckHourModel.Id);
            da.InsertTruckHourMapping(UpdatedTruckHourModel);
            NotifyOfPropertyChange(() => TruckHours);
        }

        private TruckHourModel GetTruckHourById(int id)
        {
            TruckHourModel model = null;
            foreach ( TruckHourModel truckHour in TruckHours)
            {
                if (truckHour.Id == id)
                {
                    model = truckHour;
                }
            }
            return model;
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

        public ObservableCollection<DataGridColumn> TruckHoursColumns { get; private set; }

        private void GenerateDefaultColumns()
        {
            this.TruckHoursColumns.Add(
                new DataGridTextColumn { Header = "Asset_Model", Binding = new Binding("AssetModel") });
            this.TruckHoursColumns.Add(new DataGridTextColumn { Header = "Group Name", Binding = new Binding("GroupName"), IsReadOnly = true });
            this.TruckHoursColumns.Add(new DataGridTextColumn { Header = "Hub", Binding = new Binding("HubName"), IsReadOnly = true });
            this.TruckHoursColumns.Add(new DataGridTextColumn { Header = "Mode", Binding = new Binding("Mode"), IsReadOnly = true });
            for (int i = 0; i < Scenario.TimePeriod; i++)
            {
                int CurrentYear = Scenario.StartYear + i;
                String BindingString = "TruckHourYearMapping[" + i + "].Value";
                this.TruckHoursColumns.Add(new DataGridTextColumn { Header = CurrentYear, Binding = new Binding(BindingString) });
            }
        }

        public void CreateTruckHour()
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
            else if (SelectedMode == null)
            {
                MessageBox.Show("Select a mode");
                return;
            }
            int HubId = GetHubId(SelectedHubName);
            if (HubId < 0)
            {
                MessageBox.Show("Selected Hub does not exist");
                return;
            }
            Console.WriteLine("AssetModel: " + SelectedAssetModel + " Hub Name: " + SelectedHubName + " Hub Id: " + HubId + " Mode: " + SelectedMode);
            TruckHourModel NewTruckHourModel = new TruckHourModel
            {
                ScenarioId = Context.ScenarioId,
                AssetModel = SelectedAssetModel,
                GroupName = "ALL",
                HubId = HubId,
                HubName = SelectedHubName,
                Mode = SelectedMode
                
            };
            List<TruckHourYearMappingModel> NewTruckHourYearMapping = new List<TruckHourYearMappingModel>();
            for (int i = 0; i < Scenario.TimePeriod; i++)
            {
                TruckHourYearMappingModel TempModel = new TruckHourYearMappingModel();
                int CurrentYear = Scenario.StartYear + i;
                TempModel.Year = CurrentYear;
                TempModel.Value = -1;
                NewTruckHourYearMapping.Add(TempModel);
            }
            NewTruckHourModel.TruckHourYearMapping = NewTruckHourYearMapping;
            da.InsertTruckHour(NewTruckHourModel);
            TruckHours.Add(NewTruckHourModel);
            NotifyOfPropertyChange("TruckHours");
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
