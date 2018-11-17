using Caliburn.Micro;
using fleetapp.DataAccessClasses;
using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace fleetapp.ViewModels
{
    public class ScenariosViewModel : Conductor<Object>
    {
        public BindableCollection<ScenarioModel> Scenarios { get; set; }
        private ScenarioDataAccess _scenarioDataAccess;
        private String _scenarioName;
        private int _startYear;
        private int _timePeriod;

        private readonly IEventAggregator _eventAggregator;

        private void LoadScenarios()
        {
            Scenarios = new BindableCollection<ScenarioModel>(_scenarioDataAccess.GetScenarios());
        }
        public ScenariosViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _scenarioDataAccess = new ScenarioDataAccess();
            LoadScenarios();
        }


        public String ScenarioName
        {
            set { _scenarioName = value; }
        }

        public int StartYear
        {
            set { _startYear = value; }
        }

        public int TimePeriod
        {
            set { _timePeriod = value; }
        }

        public void CreateScenario()
        {
            _scenarioDataAccess.InsertScenario(_scenarioName, _startYear, _timePeriod);
            LoadScenarios();
            NotifyOfPropertyChange("Scenarios");
        }

        public ScenarioModel SelectedItem
        {
            set
            {
                Context.ScenarioId = value.Id;
                _eventAggregator.PublishOnUIThread("loaded:scenario");
            }
        }

    }
}
