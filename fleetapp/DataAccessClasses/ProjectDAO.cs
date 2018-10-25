using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.DataAccessClasses
{
    
    class ProjectDAO
    {
        private String connectionString;

        public ProjectDAO()
        {
            connectionString = ConfigurationManager.ConnectionStrings["FleetAppDB"].ConnectionString;
        }
        public List<ProjectModel> GetProjects()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(connectionString)) 
            {
                var output = connection.Query<ProjectModel>("select * from project").ToList();
                return output;
            }
        }
    }
    
}
