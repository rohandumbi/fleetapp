using Dapper;
using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.DataAccessClasses
{
    public class MinePlanDataAccess : BaseDataAccess
    {
        public List<MinePlanModel> GetTruckHours()
        {
            using (IDbConnection connection = getConnection())
            {
                var MinePlans = connection.Query<MinePlanModel>($"select * from MinePlan where ScenarioID = { Context.ScenarioID }").ToList();
                foreach (var MinePlan in MinePlans)
                {
                    MinePlan.mapping = connection.Query<MinePlanYearMappingModel>($"select * from MinePlanYearMapping where MinePlanID = { MinePlan.Id }").ToList();
                }
                return MinePlans;
            }
        }
    }
}
