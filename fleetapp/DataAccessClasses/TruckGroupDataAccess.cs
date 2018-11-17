using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.DataAccessClasses
{
    public class TruckGroupDataAccess : BaseDataAccess
    {
        public List<TruckGroupModel> GetTruckGroups()
        {
            using (IDbConnection connection = getConnection())
            {
                return connection.Query<TruckGroupModel>($"select * from TruckGroup where ScenarioId = { Context.ScenarioId }").ToList();
            }
        }

        public void InsertTruckGroup(TruckGroupModel newTruckGroup)
        {

            using (IDbConnection connection = getConnection())
            {
                String insertQuery = $"insert into TruckGroup (ScenarioId, Name, AssetModel, FleetId)" +
                    $" OUTPUT INSERTED.Id  " +
                    $" VALUES(@ScenarioId,@Name, @AssetModel, @FleetId)";

                newTruckGroup.Id = connection.QuerySingle<int>(insertQuery, new
                {
                    newTruckGroup.ScenarioId,
                    newTruckGroup.Name,
                    newTruckGroup.AssetModel,
                    newTruckGroup.FleetId
                });

            }
        }
    }
}
