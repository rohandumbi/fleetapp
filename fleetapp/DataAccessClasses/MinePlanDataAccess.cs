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
        public List<MinePlanModel> GetMinePlans()
        {
            using (IDbConnection connection = getConnection())
            {
                var MinePlans = connection.Query<MinePlanModel>($"select * from MinePlan where ScenarioId = { Context.ScenarioId }").ToList();
                foreach (var MinePlan in MinePlans)
                {
                    MinePlan.MinePlanYearMapping = connection.Query<MinePlanYearMappingModel>($"select * from MinePlanYearMapping where MinePlanId = { MinePlan.Id }").ToList();
                }
                return MinePlans;
            }
        }

        public void InsertMinePlan(MinePlanModel newMinePlan)
        {

            using (IDbConnection connection = getConnection())
            {
                String insertQuery = $"insert into MinePlan (ScenarioId, Hub, Physical)" +
                    $" OUTPUT INSERTED.Id  " +
                    $" VALUES(@ScenarioId, @Hub, @Physical)";

                String insertMappingQuery = $"insert into MinePlanYearMapping (MinePlanId, Year, Value)" +
                    $" VALUES(@MinePlanId, @Year, @Value)";

                newMinePlan.Id = connection.QuerySingle<int>(insertQuery, new
                {
                    newMinePlan.ScenarioId,
                    newMinePlan.Hub,
                    newMinePlan.Physical
                });

                foreach (MinePlanYearMappingModel MinePlanYearMapping in newMinePlan.MinePlanYearMapping)
                {
                    MinePlanYearMapping.MinePlanId = newMinePlan.Id;
                    connection.QuerySingle(insertMappingQuery, new
                    {
                        MinePlanYearMapping.MinePlanId,
                        MinePlanYearMapping.Year,
                        MinePlanYearMapping.Value
                    });
                }
            }
        }
    }
}
