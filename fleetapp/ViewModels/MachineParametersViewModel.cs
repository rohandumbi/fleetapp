using Caliburn.Micro;
using fleetapp.DataAccessClasses;
using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace fleetapp.ViewModels
{
    public class MachineParametersViewModel : Screen
    {
        private MachineParameterDataAccess _machineParameterDataAccess;
        private ScenarioDataAccess _ScenarioDataAccess;
        private String _MachineParameterFileName;
        public ScenarioModel Scenario;
        public BindableCollection<MachineParameterModel> MachineParameters { get; set; }
        public ObservableCollection<DataGridColumn> MachineParameterColumns { get; private set; }

        private void LoadMachineParameterList()
        {
            MachineParameters = new BindableCollection<MachineParameterModel>(_machineParameterDataAccess.GetMachineParameters());
        }
        public MachineParametersViewModel()
        {
            _machineParameterDataAccess = new MachineParameterDataAccess();
            _ScenarioDataAccess = new ScenarioDataAccess();
            Scenario = _ScenarioDataAccess.GetScenario(Context.ScenarioId);
            MachineParameterColumns = new ObservableCollection<DataGridColumn>();
            GenerateDefaultColumns();
        }

        public String MachineParameterFile
        {
            set { _MachineParameterFileName = value; }
        }

        public void ImportFile()
        {
            IEnumerable<MachineParameterModel> newMachineParameters = ReadCSV(_MachineParameterFileName);
            _machineParameterDataAccess.DeleteAll();
            _machineParameterDataAccess.InsertMachineParameters(newMachineParameters);
            LoadMachineParameterList();
            NotifyOfPropertyChange("MachineParameters");
        }

        private void GenerateDefaultColumns()
        {
            MachineParameterColumns.Add(new DataGridTextColumn { Header = "AssetModel", Binding = new Binding("AssetModel") });
            MachineParameterColumns.Add(new DataGridTextColumn { Header = "Hub", Binding = new Binding("Hub") });
            this.MachineParameterColumns.Add(new DataGridTextColumn { Header = "Mode", Binding = new Binding("Mode") });

            for (int i = 0; i < Scenario.TimePeriod; i++)
            {
                int CurrentYear = Scenario.StartYear + i;
                String BindingString = "MachineParameterYearMapping[" + i + "].Value";
                Console.WriteLine(BindingString);
                this.MachineParameterColumns.Add(new DataGridTextColumn { Header = CurrentYear, Binding = new Binding(BindingString) });
            }
        }

        private List<MachineParameterModel> ReadCSV(string fileName)
        {
            string[] lines = File.ReadAllLines(System.IO.Path.ChangeExtension(fileName, ".csv"));
            String[] Headers = lines[0].Split(',');
            Dictionary<string, String[]> hash = new Dictionary<string, String[]>();
            for (int i = 1; i < lines.Length; i++)
            {
                
            }

            return null;
        }
    }
}
