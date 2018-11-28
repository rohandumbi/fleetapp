using Caliburn.Micro;
using fleetapp.DataAccessClasses;
using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            LoadMachineParameterList();
        }

        public String MachineParameterFile
        {
            set { _MachineParameterFileName = value; }
        }

        public void ImportFile()
        {
            if (String.IsNullOrEmpty(_MachineParameterFileName))
            {
                MessageBox.Show("Please select a file!");
                return;
            }
            try
            {
                IEnumerable<MachineParameterModel> newMachineParameters = ReadCSV(_MachineParameterFileName);
                _machineParameterDataAccess.DeleteAll();
                _machineParameterDataAccess.InsertMachineParameters(newMachineParameters);
                LoadMachineParameterList();
                NotifyOfPropertyChange("MachineParameters");
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show("Could not import the file. Please verify the file again.");
                return;
            }

        }

        private void GenerateDefaultColumns()
        {
            MachineParameterColumns.Add(new DataGridTextColumn { Header = "AssetModel", Binding = new Binding("AssetModel") });
            MachineParameterColumns.Add(new DataGridTextColumn { Header = "Hub", Binding = new Binding("Hub") });
            MachineParameterColumns.Add(new DataGridTextColumn { Header = "Mode", Binding = new Binding("Mode") });

            for (int i = 0; i < Scenario.TimePeriod; i++)
            {
                int CurrentYear = Scenario.StartYear + i;
                String BindingString = "MachineParameterYearMapping[" + i + "].Value";
                MachineParameterColumns.Add(new DataGridTextColumn { Header = CurrentYear, Binding = new Binding(BindingString) });
            }
        }

        private List<MachineParameterModel> ReadCSV(string fileName)
        {
            List<MachineParameterModel> newMachineParameters = new List<MachineParameterModel>();
            string[] lines = File.ReadAllLines(System.IO.Path.ChangeExtension(fileName, ".csv"));
            String[] Headers = lines[0].Split(',');
            String[] DateRow = lines[1].Split(',');
            Dictionary<string, List<String[]>> MachineParamterDictionary = new Dictionary<string, List<String[]>>();
            for (int i = 2; i < lines.Length; i++)
            {
                var data = lines[i].Split(',');
                var key = data[0] + "-" + data[1] + "-" + data[2];
                List<String[]> dataList;
                if (MachineParamterDictionary.ContainsKey(key))
                {
                    dataList = MachineParamterDictionary[key];
                } else
                {
                    dataList = new List<String[]>();
                    MachineParamterDictionary.Add(key, dataList);
                }
                dataList.Add(data);
                
            }
            for (int index = 0; index < MachineParamterDictionary.Count; index++)
            {
                var item = MachineParamterDictionary.ElementAt(index);
                var key = item.Key;
                var datarows = item.Value.ToArray();
                var keyData = key.Split('-');
                var newMachineParameter = new MachineParameterModel
                {
                    ScenarioId = Context.ScenarioId,
                    AssetModel = keyData[0],
                    Hub = keyData[1],
                    Mode = keyData[2],
                    MachineParameterYearMapping = new List<MachineParameterYearMappingModel>()
                };
                for(var i = 4; i < Headers.Length; i++) // skipped index 3 as that is supposed to contain names only
                {
                    Console.WriteLine(DateRow[i], "MM-dd-yyyy", CultureInfo.InvariantCulture);
                    var mapping = new MachineParameterYearMappingModel
                    {
                        Year = Int32.Parse(Headers[i]),
                        StartDate = DateTime.ParseExact(DateRow[i], "MM-dd-yyyy", CultureInfo.InvariantCulture),
                        SchEU = Decimal.Parse(datarows[0][i]),
                        Npot = Decimal.Parse(datarows[1][i]),
                        UtEu = Decimal.Parse(datarows[2][i]),
                        Payload = Int32.Parse(datarows[3][i])
                    };
                    mapping.Days = (new DateTime(mapping.StartDate.Year, 12, 31)).DayOfYear - mapping.StartDate.DayOfYear;
                    mapping.EngineHours = (mapping.SchEU + mapping.Npot + mapping.UtEu) * mapping.Days * 24 / 100;
                    mapping.UsableHours = mapping.SchEU * mapping.Days * 24 / 100;
                    newMachineParameter.MachineParameterYearMapping.Add(mapping);
                }
                newMachineParameters.Add(newMachineParameter);


            }
            return newMachineParameters;
        }
    }
}
