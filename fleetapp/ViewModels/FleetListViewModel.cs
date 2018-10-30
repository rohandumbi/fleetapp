using Caliburn.Micro;
using fleetapp.DataAccessClasses;
using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.ViewModels
{
    public class FleetListViewModel : Screen
    {
        private FleetDataAccess _fleetDataAccess;
        private String _fleetFileName;
        public BindableCollection<FleetModel> Fleets { get; set; }

        public FleetListViewModel()
        {
            _fleetDataAccess = new FleetDataAccess();
            LoadFleetList();
        }

        private void LoadFleetList()
        {
            Fleets = new BindableCollection<FleetModel>(_fleetDataAccess.GetFleets());

        }

        public String FleetFile
        {
            set { _fleetFileName = value; }
        }

        public void ImportFile()
        {
            IEnumerable<FleetModel> Fleets = ReadCSV(_fleetFileName);
            _fleetDataAccess.DeleteAll();
            _fleetDataAccess.InsertFleets(Fleets);
            LoadFleetList();
            NotifyOfPropertyChange("Fleets");
        }

        private IEnumerable<FleetModel> ReadCSV(string fileName)
        {
            string[] lines = File.ReadAllLines(System.IO.Path.ChangeExtension(fileName, ".csv"));

            return lines.Select(line =>
            {
                string[] data = line.Split(',');
                return new FleetModel { ProjectID = Context.ProjectID, AssetType = data[0], AssetModel = data[1], FleetID = data[2] };
            });
        }
    }
}
