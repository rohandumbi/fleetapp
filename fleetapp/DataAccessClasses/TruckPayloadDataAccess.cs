using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.DataAccessClasses
{
    public class TruckPayloadDataAccess : BaseDataAccess
    {
        public List<TruckPayloadModel> GetTruckPayloads()
        {
            using (IDbConnection connection = getConnection())
            {
                return connection.Query<TruckPayloadModel>($"select * from TruckPayload where ScenarioId = { Context.ScenarioId }").ToList();
            }
        }

        public void InsertTruckPayload(TruckPayloadModel newTruckPayload)
        {

            using (IDbConnection connection = getConnection())
            {
                String insertQuery = $"insert into TruckPayload (ScenarioId, AssetModel, MaterialType, Payload)" +
                    $" OUTPUT INSERTED.Id  " +
                    $" VALUES(@ScenarioId, @AssetModel, @MaterialType, @Payload)";

                newTruckPayload.Id = connection.QuerySingle<int>(insertQuery, new
                {
                    newTruckPayload.ScenarioId,
                    newTruckPayload.AssetModel,
                    newTruckPayload.MaterialType,
                    newTruckPayload.Payload
                });

            }
        }
    }
}
