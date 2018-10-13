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
            Projects = new BindableCollection<ProjectModel>();
            Projects.Add(new ProjectModel("Project1", "Project1 Desc"));
            Projects.Add(new ProjectModel("Project2", "Project1 Desc"));
        }

        
    }
}
