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

        private readonly IEventAggregator _eventAggregator;

        private void LoadProjects()
        {
            Projects = new BindableCollection<ProjectModel>(_projectDAO.GetProjects());
        }
        public ProjectsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
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
                Context.ProjectId = value.Id;
                _eventAggregator.PublishOnUIThread("loaded:project");
            }
        }

        public void CreateProject ()
        {
            ProjectModel newProject = new ProjectModel
            {
                Name = _projectName,
                Description = _projectDescription
            };
            _projectDAO.InsertProject(newProject);
            Projects.Add(newProject);
            NotifyOfPropertyChange("Projects");
        }
    }
}
