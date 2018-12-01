using Caliburn.Micro;
using fleetapp.DataAccessClasses;
using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public TruckHoursViewModel()
        {
            da = new TruckHourDataAccess();
            TruckHours = new BindableCollection<TruckHourModel>(da.GetTruckHours());
            _ScenarioDataAccess = new ScenarioDataAccess();
            Scenario = _ScenarioDataAccess.GetScenario(Context.ScenarioId);
            this.TruckHoursColumns = new ObservableCollection<DataGridColumn>();
            this.GenerateDefaultColumns();
        }

        public ObservableCollection<DataGridColumn> TruckHoursColumns { get; private set; }

        private void GenerateDefaultColumns()
        {
            this.TruckHoursColumns.Add(
                new DataGridTextColumn { Header = "Asset_Model", Binding = new Binding("AssetModel") });
            this.TruckHoursColumns.Add(new DataGridTextColumn { Header = "Group Name", Binding = new Binding("GroupName") });
            this.TruckHoursColumns.Add(new DataGridTextColumn { Header = "Hub", Binding = new Binding("Hub") });
            this.TruckHoursColumns.Add(new DataGridTextColumn { Header = "Mode", Binding = new Binding("Mode") });
            for (int i = 0; i < Scenario.TimePeriod; i++)
            {
                int CurrentYear = Scenario.StartYear + i;
                String BindingString = "TruckHourYearMapping[" + i + "].Value";
                this.TruckHoursColumns.Add(new DataGridTextColumn { Header = CurrentYear, Binding = new Binding(BindingString) });
            }
        }
    }
}
