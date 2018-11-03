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
    class ScenariosMainViewModel : Conductor<Object>
    {
        private String _machineParameterButtonForeground;
        private String _truckPayloadButtonForeground;
        private String _truckGroupButtonForeground;
        private String _minePlanButtonForeground;
        private String _truckHourButtonForeground;

        public string MachineParameterButtonForeground
        {
            get { return _machineParameterButtonForeground; }
            set
            {
                _machineParameterButtonForeground = value;
                NotifyOfPropertyChange(() => MachineParameterButtonForeground);
            }
        }

        public string TruckPayloadButtonForeground
        {
            get { return _truckPayloadButtonForeground; }
            set
            {
                _truckPayloadButtonForeground = value;
                NotifyOfPropertyChange(() => TruckPayloadButtonForeground);
            }
        }

        public string TruckGroupButtonForeground
        {
            get { return _truckGroupButtonForeground; }
            set
            {
                _truckGroupButtonForeground = value;
                NotifyOfPropertyChange(() => TruckGroupButtonForeground);
            }
        }

        public string MinePlanButtonForeground
        {
            get { return _minePlanButtonForeground; }
            set
            {
                _minePlanButtonForeground = value;
                NotifyOfPropertyChange(() => MinePlanButtonForeground);
            }
        }

        public string TruckHourButtonForeground
        {
            get { return _truckHourButtonForeground; }
            set
            {
                _truckHourButtonForeground = value;
                NotifyOfPropertyChange(() => TruckHourButtonForeground);
            }
        }

        public ScenariosMainViewModel()
        {
            SetDefaultButtonForegrounds();
            MachineParameterButtonForeground = "#FF189AD3";
            ShowMachineParametersScreen();
        }

        public void SetDefaultButtonForegrounds()
        {
            MachineParameterButtonForeground = "#FF0E1A1F";
            TruckPayloadButtonForeground = "#FF0E1A1F";
            TruckGroupButtonForeground = "#FF0E1A1F";
            MinePlanButtonForeground = "#FF0E1A1F";
            TruckHourButtonForeground = "#FF0E1A1F";
        }

        public void ClickTab(object sender)
        {
            Button selectedButton = sender as Button;
            SetDefaultButtonForegrounds();
            if (selectedButton != null)
            {
                String keyword = selectedButton.Content.ToString();
                switch (keyword)
                {
                    case "Machine Parameters":
                        MachineParameterButtonForeground = "#FF189AD3";
                        NotifyOfPropertyChange(() => MachineParameterButtonForeground);
                        ShowMachineParametersScreen();
                        break;
                    case "Truck Payloads":
                        TruckPayloadButtonForeground = "#FF189AD3";
                        NotifyOfPropertyChange(() => TruckPayloadButtonForeground);
                        ShowTruckPayloadsScreen();
                        break;
                    case "Truck Groups":
                        TruckGroupButtonForeground = "#FF189AD3";
                        NotifyOfPropertyChange(() => TruckGroupButtonForeground);
                        ShowTruckGroupsScreen();
                        break;
                    case "Mine Plan":
                        MinePlanButtonForeground = "#FF189AD3";
                        NotifyOfPropertyChange(() => MinePlanButtonForeground);
                        ShowMinePlanScreen();
                        break;
                    case "Truck Hours":
                        TruckHourButtonForeground = "#FF189AD3";
                        NotifyOfPropertyChange(() => TruckHourButtonForeground);
                        ShowTruckHoursScreen();
                        break;
                    default:
                        ShowMachineParametersScreen();
                        break;
                }
            }
        }

        private void ShowMachineParametersScreen()
        {
            ActivateItem(new MachineParametersViewModel());
        }
        private void ShowTruckPayloadsScreen()
        {
            ActivateItem(new TruckPayloadsViewModel());
        }

        private void ShowTruckGroupsScreen()
        {
            ActivateItem(new TruckGroupsViewModel());
        }

        private void ShowMinePlanScreen()
        {
            ActivateItem(new MinePlanViewModel());
        }

        private void ShowTruckHoursScreen()
        {
            ActivateItem(new TruckHoursViewModel());
        }
    }
}
