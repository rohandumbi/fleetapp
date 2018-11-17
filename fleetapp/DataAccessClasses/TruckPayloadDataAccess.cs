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
                return connection.Query<TruckPayloadModel>($"select * from TruckPayload where ScenarioID = { Context.ScenarioId }").ToList();
            }
        }
    }
}
