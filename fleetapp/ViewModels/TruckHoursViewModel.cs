using Caliburn.Micro;
using fleetapp.DataAccessClasses;
using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace fleetapp.ViewModels
{
    public class TruckHoursViewModel : Screen
    {
        TruckHousDataAccess da;
        public BindableCollection<TruckHoursModel> TruckHours { get; set; }

        public TruckHoursViewModel()
        {
            da = new TruckHousDataAccess();
            TruckHours = new BindableCollection<TruckHoursModel>(da.GetTruckHours());
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
        }
    }
}
