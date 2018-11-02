using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace fleetapp.ViewModels
{
    public class ScenariosViewModel : Screen
    {
        private String _scenarioName;
        private int _startYear;
        private int _timePeriod;

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
            //_projectDAO.InsertProject(_projectName, _projectDescription);
            //LoadProjects();
            //NotifyOfPropertyChange("Projects");
            MessageBox.Show("Create scenario");
        }
    }
}
