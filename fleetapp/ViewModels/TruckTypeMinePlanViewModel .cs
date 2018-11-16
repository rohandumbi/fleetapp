using Caliburn.Micro;
using fleetapp.DataAccessClasses;
using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace fleetapp.ViewModels
{
    public class TruckTypeMinePlanViewModel : Screen
    {
        private TruckTypeMinePlanDataAccess da;
        public BindableCollection<TruckTypeMinePlanModel> TruckTypeMinePlans { get; set; }

        public TruckTypeMinePlanViewModel()
        {
            da = new TruckTypeMinePlanDataAccess();
            TruckTypeMinePlans = new BindableCollection<TruckTypeMinePlanModel>(da.GetTruckTypeMinePlans());
            this.TruckTypeMinePlansColumns = new ObservableCollection<DataGridColumn>();
            this.GenerateDefaultColumns();
        }

        public ObservableCollection<DataGridColumn> TruckTypeMinePlansColumns { get; private set; }

        private void GenerateDefaultColumns()
        {
            this.TruckTypeMinePlansColumns.Add(
                new DataGridTextColumn { Header = "Hub", Binding = new Binding("Hub") });
            this.TruckTypeMinePlansColumns.Add(new DataGridTextColumn { Header = "Physical", Binding = new Binding("Physical") });
            this.TruckTypeMinePlansColumns.Add(new DataGridTextColumn { Header = "Truck Type", Binding = new Binding("TruckType") });
            this.TruckTypeMinePlansColumns.Add(new DataGridTextColumn { Header = "Mine Plan Payload", Binding = new Binding("MinePlanPayload") });

            foreach (var map in this.TruckTypeMinePlans[0].mapping.Select((value, i) => new { i, value }))
            {
                var value = map.value;
                var index = map.i;
                int CurrentYear = value.Year;
                String BindingString = "mapping[" + index.ToString() + "].Value";
                Console.WriteLine(BindingString);
                this.TruckTypeMinePlansColumns.Add(new DataGridTextColumn { Header = CurrentYear, Binding = new Binding(BindingString) });
            }
        }
    }
}
