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
    public class MinePlanViewModel : Screen
    {
        private MinePlanDataAccess da;
        public BindableCollection<MinePlanModel> MinePlans { get; set; }

        public MinePlanViewModel()
        {
            da = new MinePlanDataAccess();
            MinePlans = new BindableCollection<MinePlanModel>(da.GetMinePlans());
            this.MinePlansColumns = new ObservableCollection<DataGridColumn>();
            this.GenerateDefaultColumns();
        }

        public ObservableCollection<DataGridColumn> MinePlansColumns { get; private set; }

        private void GenerateDefaultColumns()
        {
            this.MinePlansColumns.Add(
                new DataGridTextColumn { Header = "Hub", Binding = new Binding("Hub") });
            this.MinePlansColumns.Add(new DataGridTextColumn { Header = "Physical", Binding = new Binding("Physical") });

            foreach (var map in this.MinePlans[0].MinePlanYearMapping.Select((value, i) => new { i, value }))
            {
                var value = map.value;
                var index = map.i;
                int CurrentYear = value.Year;
                String BindingString = "mapping[" + index.ToString() + "].Value";
                Console.WriteLine(BindingString);
                this.MinePlansColumns.Add(new DataGridTextColumn { Header = CurrentYear, Binding = new Binding(BindingString) });
            }
        }
    }
}
