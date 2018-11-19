﻿using fleetapp.Models;
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
                return connection.Query<ScenarioModel>($"select * from Scenario where ProjectId = { Context.ProjectId } ").ToList();
            }
        }

        public ScenarioModel GetScenario(int Id)
        {
            using (IDbConnection connection = getConnection())
            {
                return connection.QuerySingle<ScenarioModel>($"select * from Scenario where Id = { Id } ");
            }
        }

        public void InsertScenario(ScenarioModel newScenario)
        {
            using (IDbConnection connection = getConnection())
            {
                String insertQuery = $"insert into Scenario (ProjectID, Name, StartYear, TimePeriod ) " +
                    $" OUTPUT INSERTED.Id  " +
                    $" values ( @ProjectId, @Name, @StartYear, @TimePeriod)";
                newScenario.Id = connection.QuerySingle<int>(insertQuery, new
                {
                    newScenario.ProjectId,
                    newScenario.Name,
                    newScenario.StartYear,
                    newScenario.TimePeriod
                });
            }
            
        }
    }
    
}
