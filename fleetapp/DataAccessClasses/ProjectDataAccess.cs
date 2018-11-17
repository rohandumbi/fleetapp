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
    
    public class ProjectDataAccess : BaseDataAccess
    {
        
        public List<ProjectModel> GetProjects()
        {
            using (IDbConnection connection = getConnection()) 
            {
                return connection.Query<ProjectModel>("select * from project").ToList();
            }
        }

        public Boolean InsertProject(String projectName, String projectDescription)
        {
            using (IDbConnection connection = getConnection())
            {
                String insertQuery = $"insert into Project (Name, Description, CreatedDate, ModifiedDate) OUTPUT INSERTED.Id" +
                    $" VALUES(@ProjectName, @ProjectDescription, GETDATE(), GETDATE())";
                connection.Execute(insertQuery);
            }

            return true;
        }
    }
    
}
