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

        public TruckHoursViewModel()
        {
            da = new TruckHourDataAccess();
            TruckHours = new BindableCollection<TruckHourModel>(da.GetTruckHours());
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

            foreach (var map in this.TruckHours[0].TruckHourYearMapping.Select((value, i) => new { i, value }))
            {
                var value = map.value;
                var index = map.i;
                int CurrentYear = value.Year;
                String BindingString = "mapping[" + index.ToString() + "].Value";
                Console.WriteLine(BindingString);
                this.TruckHoursColumns.Add(new DataGridTextColumn { Header = CurrentYear, Binding = new Binding(BindingString) });
            }
        }
    }
}
