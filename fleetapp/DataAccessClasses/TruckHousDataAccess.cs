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
    public class TruckHousDataAccess : BaseDataAccess
    {
        public List<TruckHoursModel> GetTruckHours()
        {
            using (IDbConnection connection = getConnection())
            {
                var TruckHours = connection.Query<TruckHoursModel>($"select * from TruckHours where ScenarioID = { Context.ScenarioID }").ToList();
                foreach (var TruckHour in TruckHours)
                {
                    TruckHour.mapping = connection.Query<TruckHoursYearMappingModel>($"select * from TruckHourYearMapping where TruckHourID = { TruckHour.id }").ToList();
                }
                return TruckHours;
            }
        }
    }
}
