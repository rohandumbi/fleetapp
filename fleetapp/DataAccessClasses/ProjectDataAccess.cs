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

        public void InsertProject(ProjectModel newProject)
        {

            using (IDbConnection connection = getConnection())
            {
                String insertQuery = $"insert into Project (Name, Description, CreatedDate, ModifiedDate)" +
                    $" OUTPUT INSERTED.Id  " +
                    $" VALUES(@Name, @Description, GETDATE(), GETDATE())";
                newProject.Id = connection.QuerySingle<int>(insertQuery, new {
                     newProject.Name,
                     newProject.Description
                });
            }
        }
    }
    
}
