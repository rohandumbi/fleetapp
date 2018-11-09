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
                return connection.Query<TruckGroupModel>($"select * from TruckGroup where ScenarioID = { Context.ScenarioID }").ToList();
            }
        }
    }
}
