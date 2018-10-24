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
        public BindableCollection<PersonModel> People { get; set; }

        public HubDefinitionViewModel()
        {
            DataAccess da = new DataAccess();
            People = new BindableCollection<PersonModel>(da.GetPeople());
        }
    }
}
