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
    public class MachineParameterDataAccess : BaseDataAccess
    {
        public List<MachineParameterModel> GetMachineParameters()
        {
            using (IDbConnection connection = getConnection())
            {
                var MachineParameters = connection.Query<MachineParameterModel>($"select * from MachineParameter where ScenarioID = { Context.ScenarioId }").ToList();
                foreach (var MachineParameter in MachineParameters)
                {
                    MachineParameter.MachineParameterYearMapping = connection.Query<MachineParameterYearMappingModel>($"select * from MachineParameterYearMapping where MachineParameterId = { MachineParameter.Id }").ToList();
                }
                return MachineParameters;
            }
        }

        public void InsertMachineParameters(IEnumerable<MachineParameterModel> newMachineParameters)
        {

            using (IDbConnection connection = getConnection())
            {
                String insertQuery = $"insert into MachineParameter (ScenarioId, AssetModel, Hub, Mode)" +
                    $" OUTPUT INSERTED.Id  " +
                    $" VALUES(@ScenarioId, @AssetModel, @Hub, @Mode)";

                String insertMappingQuery = $"insert into MachineParameterYearMapping (MachineParameterId, Year, StartDate, Days, SchEU, Npot, UtEu, Payload, EngineHours, UsableHours)" +
                    $" VALUES(@MachineParameterId, @Year, @StartDate, @Days, @SchEU, @Npot, @UtEu, @Payload, @EngineHours, @UsableHours)";
                foreach(var newMachineParameter in newMachineParameters)
                {
                    newMachineParameter.Id = connection.QuerySingle<int>(insertQuery, new
                    {
                        newMachineParameter.ScenarioId,
                        newMachineParameter.AssetModel,
                        newMachineParameter.Hub,
                        newMachineParameter.Mode
                    });

                    foreach (MachineParameterYearMappingModel MachineParameterYearMapping in newMachineParameter.MachineParameterYearMapping)
                    {
                        MachineParameterYearMapping.MachineParameterId = newMachineParameter.Id;
                        connection.Execute(insertMappingQuery, new
                        {
                            MachineParameterYearMapping.MachineParameterId,
                            MachineParameterYearMapping.Year,
                            MachineParameterYearMapping.StartDate,
                            MachineParameterYearMapping.Days,
                            MachineParameterYearMapping.SchEU,
                            MachineParameterYearMapping.Npot,
                            MachineParameterYearMapping.UtEu,
                            MachineParameterYearMapping.Payload,
                            MachineParameterYearMapping.EngineHours,
                            MachineParameterYearMapping.UsableHours
                        });
                    }
                }
            }
        }

        public void DeleteAll()
        {
            using (IDbConnection connection = getConnection())
            {
                connection.Execute($"delete from MachineParameterYearMapping where MachineParameterId" +
                    $" in ( select id from MachineParameter where ScenarioId = { Context.ScenarioId } )");
                connection.Execute($"delete from MachineParameter where ScenarioId = { Context.ScenarioId } ");
            }
        }
    }
}
