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
                return connection.Query<FleetModel>($"select * from Fleet where ProjectID = { Context.ProjectID }").ToList();
            }
        }

        public Boolean InsertFleets(IEnumerable<FleetModel> Fleets)
        {
            using (IDbConnection connection = getConnection())
            {
                foreach (var Fleet in Fleets)
                {
                    String insertQuery = $"insert into Fleet (ProjectID, AssetType, AssetModel, FleetID) " +
                    $"values ({ Context.ProjectID }, '{ Fleet.AssetType }', '{ Fleet.AssetModel }' , '{ Fleet.FleetID }' )";
                    connection.Execute(insertQuery);
                }                 
            }

            return true;
        }
    }
}
