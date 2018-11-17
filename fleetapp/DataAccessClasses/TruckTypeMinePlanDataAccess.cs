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
                var TruckTypeMinePlans = connection.Query<TruckTypeMinePlanModel>($"select * from TruckTypeMinePlan where ScenarioID = { Context.ScenarioId }").ToList();
                foreach (var TruckTypeMinePlan in TruckTypeMinePlans)
                {
                    TruckTypeMinePlan.TruckTypeMinePlanYearMapping
                        = connection.Query<TruckTypeMinePlanYearMappingModel>($"select * from TruckTypeMinePlanYearMapping where TruckTypeMinePlanID = { TruckTypeMinePlan.Id }").ToList();
                }
                return TruckTypeMinePlans;
            }
        }
    }
}
