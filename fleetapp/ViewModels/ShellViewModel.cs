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
    class ShellViewModel : Conductor<Object>, IHandle<object>
    {
        private String _projectButtonForeground;
        private String _fleetListButtonForeground;
        private String _hubDefinitionButtonForeground;
        private String _hubAllocationButtonForeground;
        private String _scenarioButtonForeground;
        private String _hubPriorityButtonForeground;
        private bool _isProjectSelected;

        private readonly IEventAggregator _eventAggregator;

        public bool IsProjectSelected
        {
            get { return _isProjectSelected; }
            set
            {
                _isProjectSelected = value;
                NotifyOfPropertyChange(() => IsProjectSelected);
            }
        }

        public string ProjectButtonForeground
        {
            get { return _projectButtonForeground; }
            set
            {
                _projectButtonForeground = value;
                NotifyOfPropertyChange(() => ProjectButtonForeground);
            }
        }

        public string HubPriorityButtonForeground
        {
            get { return _hubPriorityButtonForeground; }
            set
            {
                _hubPriorityButtonForeground = value;
                NotifyOfPropertyChange(() => HubPriorityButtonForeground);
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
            _eventAggregator = new EventAggregator();
            _eventAggregator.Subscribe(this);
            SetDisabledButtonForegrounds();
            ProjectButtonForeground = "#FF189AD3";
            IsProjectSelected = false;
            ActivateItem(new ProjectsViewModel(_eventAggregator));
        }

        public void SetDefaultButtonForegrounds()
        {
            ProjectButtonForeground = "#FF0E1A1F";
            FleetListButtonForeground = "#FF0E1A1F";
            HubPriorityButtonForeground = "#FF0E1A1F";
            HubDefinitionButtonForeground = "#FF0E1A1F";
            HubAllocationButtonForeground = "#FF0E1A1F";
            ScenarioButtonForeground = "#FF0E1A1F";
        }

        public void SetDisabledButtonForegrounds()
        {
            FleetListButtonForeground = "#D3D3D3";
            HubDefinitionButtonForeground = "#D3D3D3";
            HubPriorityButtonForeground = "#D3D3D3";
            HubAllocationButtonForeground = "#D3D3D3";
            ScenarioButtonForeground = "#D3D3D3";
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
                    case "Truck Hub Priority":
                        HubPriorityButtonForeground = "#FF189AD3";
                        NotifyOfPropertyChange(() => HubPriorityButtonForeground);
                        ShowTruckHubPriorityScreen();
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
            ActivateItem(new ProjectsViewModel(_eventAggregator));
        }
        private void ShowFleetListScreen()
        {
            ActivateItem(new FleetListViewModel());
        }

        private void ShowTruckHubPriorityScreen()
        {
            ActivateItem(new TruckHubPriorityViewModel());
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
            ActivateItem(new ScenariosViewModel(_eventAggregator));
        }

        public void Handle(object message)
        {
            //MessageBox.Show(message as String);
            String EventName = message as String;
            if (EventName == "loaded:project")
            {
                IsProjectSelected = true;
                SetDefaultButtonForegrounds();
                ProjectButtonForeground = "#FF189AD3";
            }
            else if (EventName == "loaded:scenario")
            {
                //MessageBox.Show("Selected scenario");
                ActivateItem(new ScenariosMainViewModel());
            }
        }
    }
}
