using Caliburn.Micro;
using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.ViewModels
{
    class ProjectsViewModel : Screen
    {
        public BindableCollection<ProjectModel> Projects { get; set; }

        public ProjectsViewModel()
        {
            DataAccess da = new DataAccess();
            Projects = new BindableCollection<ProjectModel>(da.GetProjects());
        }
    }
}
