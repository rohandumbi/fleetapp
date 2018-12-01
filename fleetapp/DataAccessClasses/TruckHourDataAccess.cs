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
                var TruckHours = connection.Query<TruckHourModel>($"select a.*, b.Name as HubName " +
                    $"from TruckHour a, Hub b " +
                    $"where b.Id = a.HubId  and ScenarioID = { Context.ScenarioId }").ToList();
                foreach (var TruckHour in TruckHours)
                {
                    TruckHour.TruckHourYearMapping
                        = connection.Query<TruckHourYearMappingModel>($"select * from TruckHourYearMapping where TruckHourID = { TruckHour.Id }").ToList();
                }
                return TruckHours;
            }
        }

        public void InsertTruckHour(TruckHourModel newTruckHour)
        {

            using (IDbConnection connection = getConnection())
            {
                String insertQuery = $"insert into TruckHour (ScenarioId, AssetModel, GroupName, HubId, Mode)" +
                    $" OUTPUT INSERTED.Id  " +
                    $" VALUES(@ScenarioId, @AssetModel, @GroupName, @HubId, @Mode)";

                String insertMappingQuery = $"insert into TruckHourYearMapping (TruckHourId, Year, Value)" +
                    $" VALUES(@TruckHourId, @Year, @Value)";

                newTruckHour.Id = connection.QuerySingle<int>(insertQuery, new
                {
                    newTruckHour.ScenarioId,
                    newTruckHour.AssetModel,
                    newTruckHour.GroupName,
                    newTruckHour.HubId,
                    newTruckHour.Mode
                });

                foreach(TruckHourYearMappingModel TruckHourYearMapping in newTruckHour.TruckHourYearMapping) {
                    TruckHourYearMapping.TruckHourId = newTruckHour.Id;
                    connection.Query(insertMappingQuery, new
                    {
                        TruckHourYearMapping.TruckHourId,
                        TruckHourYearMapping.Year,
                        TruckHourYearMapping.Value
                    });
                }
            }
        }

        public void InsertTruckHourMapping(TruckHourModel TruckHour)
        {

            using (IDbConnection connection = getConnection())
            {

                String insertMappingQuery = $"insert into TruckHourYearMapping (TruckHourId, Year, Value)" +
                    $" VALUES(@TruckHourId, @Year, @Value)";

                foreach (TruckHourYearMappingModel TruckHoursYearMapping in TruckHour.TruckHourYearMapping)
                {
                    TruckHoursYearMapping.TruckHourId = TruckHour.Id;
                    connection.Query(insertMappingQuery, new
                    {
                        TruckHoursYearMapping.TruckHourId,
                        TruckHoursYearMapping.Year,
                        TruckHoursYearMapping.Value
                    });
                }
            }
        }

        public void DeleteTruckHourMapping(int TruckHourId)
        {

            using (IDbConnection connection = getConnection())
            {

                String deleteMappingQuery = $"delete from TruckHourYearMapping where TruckHourId = {TruckHourId} ";
                connection.Execute(deleteMappingQuery);
            }
        }
    }
}
