using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.Models
{
    public class ProjectModel
    {
        private String _projectName;
        private String _projectDescription;
        private String _projectCreationDate;

        public ProjectModel(String projectName, String projectDescription, String projectCreationDate)
        {
            this._projectName = projectName;
            this._projectDescription = projectDescription;
            this._projectCreationDate = projectCreationDate;
        }
        public String ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; }
        }

        public String ProjectDescription
        {
            get { return _projectDescription; }
            set { _projectDescription = value; }
        }

        public String ProjectCreationDate
        {
            get { return _projectCreationDate; }
            set { _projectCreationDate = value; }
        }

    }
}
