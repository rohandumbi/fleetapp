﻿using Dapper;
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
                var TruckHours = connection.Query<TruckHourModel>($"select * from TruckHour where ScenarioID = { Context.ScenarioId }").ToList();
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
                String insertQuery = $"insert into TruckHour (ScenarioId, AssetModel, GroupName, Hub, Mode)" +
                    $" OUTPUT INSERTED.Id  " +
                    $" VALUES(@ScenarioId, @AssetModel, @GroupName, @Hub, @Mode)";

                String insertMappingQuery = $"insert into TruckHourYearMapping (TruckHourId, Year, Value)" +
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
                    TruckHoursYearMapping.TruckHourId = newTruckHour.Id;
                    connection.QuerySingle(insertMappingQuery, new
                    {
                        TruckHoursYearMapping.TruckHourId,
                        TruckHoursYearMapping.Year,
                        TruckHoursYearMapping.Value
                    });
                }
            }
        }
    }
}
