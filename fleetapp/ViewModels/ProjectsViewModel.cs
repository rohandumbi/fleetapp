using Caliburn.Micro;
using fleetapp.DAOs;
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
        private ProjectDAO _projectDAO;

        public ProjectsViewModel()
        {
            _projectDAO = new ProjectDAO();
            Projects = new BindableCollection<ProjectModel>(_projectDAO.GetProjects());
        }
    }
}
