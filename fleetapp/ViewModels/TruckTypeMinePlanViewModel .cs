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
using System.IO;

namespace fleetapp.ViewModels
{
    public class TruckTypeMinePlanViewModel : Screen
    {
        private TruckTypeMinePlanDataAccess _TruckTypeMinePlanDataAccess;
        private ScenarioDataAccess _ScenarioDataAccess;
        private String _truckMinePlanFileName;
        public BindableCollection<TruckTypeMinePlanModel> TruckTypeMinePlans { get; set; }
        public ScenarioModel Scenario;
        public ObservableCollection<DataGridColumn> TruckTypeMinePlansColumns { get; private set; }

        public TruckTypeMinePlanViewModel()
        {
            _TruckTypeMinePlanDataAccess = new TruckTypeMinePlanDataAccess();
            _ScenarioDataAccess = new ScenarioDataAccess();
           
            Scenario = _ScenarioDataAccess.GetScenario(Context.ScenarioId);
            this.TruckTypeMinePlansColumns = new ObservableCollection<DataGridColumn>();
            this.GenerateDefaultColumns();
        }

        private void LoadFTruckTypeMinePlanList()
        {
            TruckTypeMinePlans = new BindableCollection<TruckTypeMinePlanModel>(_TruckTypeMinePlanDataAccess.GetTruckTypeMinePlans());

        }

        public String TruckMinePlanFile
        {
            set { _truckMinePlanFileName = value; }
        }

        public void ImportFile()
        {
            IEnumerable<TruckTypeMinePlanModel> newTruckTypeMinePlans = ReadCSV(_truckMinePlanFileName);
            _TruckTypeMinePlanDataAccess.DeleteAll();
            _TruckTypeMinePlanDataAccess.InsertTruckTypeMinePlans(newTruckTypeMinePlans);
            LoadFTruckTypeMinePlanList();
            NotifyOfPropertyChange("TruckTypeMinePlans");
        }

        

        private void GenerateDefaultColumns()
        {
            this.TruckTypeMinePlansColumns.Add(
                new DataGridTextColumn { Header = "Hub", Binding = new Binding("Hub") });
            this.TruckTypeMinePlansColumns.Add(new DataGridTextColumn { Header = "Truck Type", Binding = new Binding("TruckType") });
            this.TruckTypeMinePlansColumns.Add(new DataGridTextColumn { Header = "Mine Plan Payload", Binding = new Binding("MinePlanPayload") });

            for (int i=0; i<Scenario.TimePeriod; i++)
            {
                int CurrentYear = Scenario.StartYear + i;
                String BindingString = "TruckTypeMinePlanYearMapping[" + i + "].Value";
                Console.WriteLine(BindingString);
                this.TruckTypeMinePlansColumns.Add(new DataGridTextColumn { Header = CurrentYear, Binding = new Binding(BindingString) });
            }
        }

        private IEnumerable<TruckTypeMinePlanModel> ReadCSV(string fileName)
        {
            string[] lines = File.ReadAllLines(System.IO.Path.ChangeExtension(fileName, ".csv"));
            String[] Headers = lines[0].Split(',');
            lines = lines.Skip(1).ToArray();          
            return lines.Select(line =>
            {
                string[] data = line.Split(',');
                TruckTypeMinePlanModel newTruckTypeMinePlan = new TruckTypeMinePlanModel
                {
                    ScenarioId = Context.ScenarioId,
                    Hub = data[0],
                    TruckType = data[1],
                    MinePlanPayload = Int32.Parse(data[2])
                };
                newTruckTypeMinePlan.TruckTypeMinePlanYearMapping = new List<TruckTypeMinePlanYearMappingModel>();
                for (var i= 3; i<line.Length; i++)
                {
                    TruckTypeMinePlanYearMappingModel TruckTypeMinePlanYearMapping = new TruckTypeMinePlanYearMappingModel
                    {
                        Year = Int32.Parse(Headers[i]),
                        Value = Decimal.Parse(data[i])
                    };
                    newTruckTypeMinePlan.TruckTypeMinePlanYearMapping.Add(TruckTypeMinePlanYearMapping);

                }
                return newTruckTypeMinePlan;
            });
        }
    }
}
