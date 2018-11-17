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
    public class TruckHourDataAccess : BaseDataAccess
    {
        public List<TruckHourModel> GetTruckHours()
        {
            using (IDbConnection connection = getConnection())
            {
                var TruckHours = connection.Query<TruckHourModel>($"select * from TruckHours where ScenarioID = { Context.ScenarioId }").ToList();
                foreach (var TruckHour in TruckHours)
                {
                    TruckHour.TruckHourYearMapping = connection.Query<TruckHourYearMappingModel>($"select * from TruckHourYearMapping where TruckHourID = { TruckHour.Id }").ToList();
                }
                return TruckHours;
            }
        }

        public void InsertTruckHours(TruckHourModel newTruckHour)
        {

            using (IDbConnection connection = getConnection())
            {
                String insertQuery = $"insert into TruckHours (ScenarioId, AssetModel, GroupName, Hub, Mode)" +
                    $" OUTPUT INSERTED.Id  " +
                    $" VALUES(@ScenarioId, @AssetModel, @GroupName, @Hub, @Mode)";

                String insertMappingQuery = $"insert into TruckHoursYearMapping (TruckHoursId, Year, Value)" +
                    $" VALUES(@TruckHoursId, @Year, @Value)";

                newTruckHour.Id = connection.QuerySingle<int>(insertQuery, new
                {
                    newTruckHour.ScenarioId,
                    newTruckHour.AssetModel,
                    newTruckHour.GroupName,
                    newTruckHour.Hub,
                    newTruckHour.Mode
                });

                foreach(TruckHourYearMappingModel TruckHoursYearMapping in newTruckHour.TruckHourYearMapping) {
                    TruckHoursYearMapping.TruckHoursId = newTruckHour.Id;
                    connection.QuerySingle(insertMappingQuery, new
                    {
                        TruckHoursYearMapping.TruckHoursId,
                        TruckHoursYearMapping.Year,
                        TruckHoursYearMapping.Value
                    });
                }
            }
        }
    }
}
