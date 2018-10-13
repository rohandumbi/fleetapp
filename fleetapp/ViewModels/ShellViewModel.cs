using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.ViewModels
{
    class ShellViewModel : Conductor<Object>
    {

        public ShellViewModel()
        {
            ActivateItem(new ProjectsViewModel());
        }

        public void ShowProjectsViewScreen()
        {
            ActivateItem(new ProjectsViewModel());
        }
        public void ShowFleetListScreen()
        {
            ActivateItem(new FleetListViewModel());
        }

        public void ShowHubDefinitionScreen()
        {
            ActivateItem(new HubDefinitionViewModel());
        }

        public void ShowHubAllocationScreen()
        {
            ActivateItem(new HubAllocationViewModel());
        }

        public void ShowScenariosViewScreen()
        {
            ActivateItem(new ScenariosViewModel());
        }
    }
}
