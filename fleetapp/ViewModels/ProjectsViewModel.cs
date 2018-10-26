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
    class ProjectsViewModel : Screen
    {
        public BindableCollection<ProjectModel> Projects { get; set; }
        private ProjectDataAccess _projectDAO;
        private String _projectName;
        private String _projectDescription;

        private void LoadProjects()
        {
            Projects = new BindableCollection<ProjectModel>(_projectDAO.GetProjects());
        }
        public ProjectsViewModel()
        {
            _projectDAO = new ProjectDataAccess();
            LoadProjects();
        }

        public String ProjectName
        {
            set { _projectName = value; }
        }

        public String ProjectDescription
        {
            set { _projectDescription = value; }
        }

        public ProjectModel SelectedItem
        {
            set {
                Context.ProjectID = value.id;
            }
        }
        public void CreateProject ()
        {
            _projectDAO.InsertProject(_projectName, _projectDescription);
            LoadProjects();
            NotifyOfPropertyChange("Projects");
        }
    }
}
