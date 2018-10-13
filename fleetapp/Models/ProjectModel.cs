using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.Models
{
    class ProjectModel
    {
        private String _projectName;
        private String _projectDescription;

        public ProjectModel(String projectName, String projectDescription)
        {
            this._projectName = projectName;
            this._projectDescription = projectDescription;
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

    }
}
