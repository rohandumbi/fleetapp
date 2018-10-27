using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace fleetapp.ViewModels
{
    class ShellViewModel : Conductor<Object>
    {
        //public String ProjectButtonForeground;
        //public String FleetListButtonForeground;
        //public String HubDefinitionButtonForeground;
        //public String HubAllocationButtonForeground;
        //public String ScenarioButtonForeground;

        private String _projectButtonForeground;
        private String _fleetListButtonForeground;
        private String _hubDefinitionButtonForeground;
        private String _hubAllocationButtonForeground;
        private String _scenarioButtonForeground;

        public string ProjectButtonForeground
        {
            get { return _projectButtonForeground; }
            set
            {
                _projectButtonForeground = value;
                NotifyOfPropertyChange(() => ProjectButtonForeground);
            }
        }

        public string FleetListButtonForeground
        {
            get { return _fleetListButtonForeground; }
            set
            {
                _fleetListButtonForeground = value;
                NotifyOfPropertyChange(() => FleetListButtonForeground);
            }
        }

        public string HubDefinitionButtonForeground
        {
            get { return _hubDefinitionButtonForeground; }
            set
            {
                _hubDefinitionButtonForeground = value;
                NotifyOfPropertyChange(() => HubDefinitionButtonForeground);
            }
        }

        public string HubAllocationButtonForeground
        {
            get { return _hubAllocationButtonForeground; }
            set
            {
                _hubAllocationButtonForeground = value;
                NotifyOfPropertyChange(() => HubAllocationButtonForeground);
            }
        }

        public string ScenarioButtonForeground
        {
            get { return _scenarioButtonForeground; }
            set
            {
                _scenarioButtonForeground = value;
                NotifyOfPropertyChange(() => ScenarioButtonForeground);
            }
        }

        public ShellViewModel()
        {
            SetDefaultButtonForegrounds();
            ProjectButtonForeground = "#FF189AD3";
            ActivateItem(new ProjectsViewModel());
        }

        public void SetDefaultButtonForegrounds()
        {
            ProjectButtonForeground = "#FF0E1A1F";
            FleetListButtonForeground = "#FF0E1A1F";
            HubDefinitionButtonForeground = "#FF0E1A1F";
            HubAllocationButtonForeground = "#FF0E1A1F";
            ScenarioButtonForeground = "#FF0E1A1F";
        }

        public void ClickTab(object sender)
        {
            var selectedButton = sender as Button;
            SetDefaultButtonForegrounds();
            if (selectedButton != null)
            {
                String keyword = selectedButton.Content.ToString();
                switch (keyword)
                {
                    case "Projects":
                        ProjectButtonForeground = "#FF189AD3";
                        NotifyOfPropertyChange(() => ProjectButtonForeground);
                        ShowProjectsViewScreen();
                        break;
                    case "Fleet List":
                        FleetListButtonForeground = "#FF189AD3";
                        NotifyOfPropertyChange(() => FleetListButtonForeground);
                        ShowFleetListScreen();
                        break;
                    case "Hub Definition":
                        HubDefinitionButtonForeground = "#FF189AD3";
                        NotifyOfPropertyChange(() => HubDefinitionButtonForeground);
                        ShowHubDefinitionScreen();
                        break;
                    case "Hub Allocation":
                        HubAllocationButtonForeground = "#FF189AD3";
                        NotifyOfPropertyChange(() => HubAllocationButtonForeground);
                        ShowHubAllocationScreen();
                        break;
                    case "Scenario":
                        ScenarioButtonForeground = "#FF189AD3";
                        NotifyOfPropertyChange(() => ScenarioButtonForeground);
                        ShowScenariosViewScreen();
                        break;
                    default:
                        ShowProjectsViewScreen();
                        break;
                }
            }
        }

        private void ShowProjectsViewScreen()
        {
            ActivateItem(new ProjectsViewModel());
        }
        private void ShowFleetListScreen()
        {
            ActivateItem(new FleetListViewModel());
        }

        private void ShowHubDefinitionScreen()
        {
            ActivateItem(new HubDefinitionViewModel());
        }

        private void ShowHubAllocationScreen()
        {
            ActivateItem(new HubAllocationViewModel());
        }

        private void ShowScenariosViewScreen()
        {
            ActivateItem(new ScenariosViewModel());
        }
    }
}
