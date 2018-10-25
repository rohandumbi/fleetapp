using Caliburn.Micro;
using fleetapp.DataAccessClasses;
using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace fleetapp.ViewModels
{
    public class HubDefinitionViewModel : Screen
    {
        public BindableCollection<HubModel> HubDefinitions { get; set; }

        public HubDefinitionViewModel()
        {
            HubDataAccess da = new HubDataAccess();
            HubDefinitions = new BindableCollection<HubModel>(da.GetHubs());
        }
    }
}
