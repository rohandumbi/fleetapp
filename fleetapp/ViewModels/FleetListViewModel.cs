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
            // We change file extension here to make sure it's a .csv file.
            // TODO: Error checking.
            string[] lines = File.ReadAllLines(System.IO.Path.ChangeExtension(fileName, ".csv"));

            // lines.Select allows me to project each line as a FleetModel. 
            // This will give me an IEnumerable<FleetModel> back.
            return lines.Select(line =>
            {
                string[] data = line.Split(',');
                // We return a person with the data in order.
                return new FleetModel { ProjectID = Context.ProjectID, AssetType = data[0], AssetModel = data[1], FleetID = data[2] };
            });
        }
    }
}
