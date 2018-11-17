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
    public class TruckTypeMinePlanDataAccess : BaseDataAccess
    {
        public List<TruckTypeMinePlanModel> GetTruckTypeMinePlans()
        {
            using (IDbConnection connection = getConnection())
            {
                var TruckTypeMinePlans = connection.Query<TruckTypeMinePlanModel>($"select * from TruckTypeMinePlan where ScenarioId = { Context.ScenarioId }").ToList();
                foreach (var TruckTypeMinePlan in TruckTypeMinePlans)
                {
                    TruckTypeMinePlan.TruckTypeMinePlanYearMapping
                        = connection.Query<TruckTypeMinePlanYearMappingModel>($"select * from TruckTypeMinePlanYearMapping where TruckTypeMinePlanId = { TruckTypeMinePlan.Id }").ToList();
                }
                return TruckTypeMinePlans;
            }
        }

        public void InsertTruckTypeMinePlan(TruckTypeMinePlanModel newTruckTypeMinePlan)
        {

            using (IDbConnection connection = getConnection())
            {
                String insertQuery = $"insert into TruckTypeMinePlan (ScenarioId, Hub, Physical, TruckType, MinePlanPayload)" +
                    $" OUTPUT INSERTED.Id  " +
                    $" VALUES(@ScenarioId, @Hub, @Physical, @TruckType, @MinePlanPayload)";

                String insertMappingQuery = $"insert into TruckTypeMinePlanYearMapping (TruckTypeMinePlanId, Year, Value)" +
                    $" VALUES(@TruckTypeMinePlanId, @Year, @Value)";

                newTruckTypeMinePlan.Id = connection.QuerySingle<int>(insertQuery, new
                {
                    newTruckTypeMinePlan.ScenarioId,
                    newTruckTypeMinePlan.Hub,
                    newTruckTypeMinePlan.Physical,
                    newTruckTypeMinePlan.TruckType,
                    newTruckTypeMinePlan.MinePlanPayload
                });

                foreach (TruckTypeMinePlanYearMappingModel TruckTypeMinePlanYearMapping in newTruckTypeMinePlan.TruckTypeMinePlanYearMapping)
                {
                    TruckTypeMinePlanYearMapping.TruckTypeMinePlanId = newTruckTypeMinePlan.Id;
                    connection.QuerySingle(insertMappingQuery, new
                    {
                        TruckTypeMinePlanYearMapping.TruckTypeMinePlanId,
                        TruckTypeMinePlanYearMapping.Year,
                        TruckTypeMinePlanYearMapping.Value
                    });
                }
            }
        }
    }
}
