using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace fleetapp.DataAccessClasses
{
    public class FleetDataAccess : BaseDataAccess
    {
        public List<FleetModel> GetFleets()
        {
            using (IDbConnection connection = getConnection())
            {
                return connection.Query<FleetModel>($"select * from Fleet where ProjectId = { Context.ProjectId }").ToList();
            }
        }

        public List<String> GetAssetModels()
        {
            using (IDbConnection connection = getConnection())
            {
                return connection.Query<String>($"select distinct AssetModel from Fleet where ProjectId = { Context.ProjectId }").ToList();
            }
        }

        public void InsertFleets(IEnumerable<FleetModel> Fleets)
        {
            using (IDbConnection connection = getConnection())
            {
                foreach (var Fleet in Fleets)
                {
                    String insertQuery = $"insert into Fleet (ProjectID, AssetNumber, AssetType, AssetModel, FleetId, InitialAge, FinalAge, Priority) " +
                    $"values ( @ProjectID, @AssetNumber, @AssetType, @AssetModel, @FleetId, @InitialAge, @FinalAge, @Priority)";
                    connection.Query(insertQuery, new
                    {
                        Fleet.ProjectId,
                        Fleet.AssetNumber,
                        Fleet.AssetType,
                        Fleet.AssetModel,
                        Fleet.FleetId,
                        Fleet.InitialAge,
                        Fleet.FinalAge,
                        Fleet.Priority
                    });
                }                 
            }
        }

        public void UpdateFleet(FleetModel Fleet)
        {
            using (IDbConnection connection = getConnection())
            {
                String updateQuery = $"update Fleet set Priority = @Priority where Id = @Id";
                connection.Query(updateQuery, new
                {
                    Fleet.Priority,
                    Fleet.Id
                });
                
            }
        }
        public void DeleteAll()
        {
            using (IDbConnection connection = getConnection())
            {
                connection.Query<FleetModel>($"delete from Fleet where ProjectID = { Context.ProjectId }");
            }
        }
    }
}
