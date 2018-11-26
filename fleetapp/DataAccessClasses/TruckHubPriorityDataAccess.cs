﻿using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace fleetapp.DataAccessClasses
{
    public class TruckHubPriorityDataAccess : BaseDataAccess
    {
        public List<TruckHubPriorityModel> GetTruckHubPriorities()
        {
            using (IDbConnection connection = getConnection())
            {
                return connection.Query<TruckHubPriorityModel>($"select * from TruckHubPriority where ProjectId = { Context.ProjectId }").ToList();
            }
        }

        public void InsertTruckHubPriority(IEnumerable<TruckHubPriorityModel> TruckHubPriorities)
        {
            using (IDbConnection connection = getConnection())
            {
                foreach (var TruckHubPriority in TruckHubPriorities)
                {
                    String insertQuery = $"insert into TruckHubPriority (ProjectID, AssetModel, Hub, Priority ) " +
                    $"values ( @ProjectID, @AssetModel, @Hub, @Priority)";
                    connection.Query(insertQuery, new
                    {
                        TruckHubPriority.ProjectId,
                        TruckHubPriority.AssetModel,
                        TruckHubPriority.Hub,
                        TruckHubPriority.Priority
                    });
                }                 
            }
        }

        public void DeleteAll()
        {
            using (IDbConnection connection = getConnection())
            {
                connection.Execute($"delete from TruckHubPriority where ProjectID = { Context.ProjectId }");
            }
        }
    }
}