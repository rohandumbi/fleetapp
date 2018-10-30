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
    
    public class ScenarioDataAccess : BaseDataAccess
    {
        
        public List<ScenarioModel> GetScenarios()
        {
            using (IDbConnection connection = getConnection()) 
            {
                return connection.Query<ScenarioModel>($"select * from Scenario where ProjectID = { Context.ProjectID } ").ToList();
            }
        }

        public Boolean InsertScenario(String scenarioName, int startYear, int timePeriod)
        {
            using (IDbConnection connection = getConnection())
            {
                String insertQuery = $"insert into Scenario (ProjectID, Name, StartYear, TimePeriod ) " +
                    $"values ('{ Context.ProjectID }', '{ scenarioName }', { startYear } , { timePeriod } )";
                connection.Execute(insertQuery);
            }
            return true;
        }
    }
    
}
