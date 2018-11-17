using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.DataAccessClasses
{
    public class HubAllocationDataAccess : BaseDataAccess
    {
        public List<HubAllocationModel> GetHubAllocations()
        {
            using (IDbConnection connection = getConnection())
            {
                return connection.Query<HubAllocationModel>($"select * from HubAllocation where ProjectID = { Context.ProjectId }").ToList();
            }
        }

        public void InsertHubAllocation(HubAllocationModel newHubAllocation)
        {
            using (IDbConnection connection = getConnection())
            {
                String insertQuery = $"insert into HubAllocation (ProjectId, HubId, AssestModel, IsManned, IsAHS)" +
                    $" OUTPUT INSERTED.Id  " +
                    $" VALUES(@ProjectId, @HubId, @AssestModel, @IsManned, @IsAHS)";
                newHubAllocation.Id = connection.QuerySingle<int>(insertQuery, new
                {
                    newHubAllocation.ProjectId,
                    newHubAllocation.HubId,
                    newHubAllocation.AssestModel,
                    newHubAllocation.IsManned,
                    newHubAllocation.IsAHS
                });
            }
        }
    }
}
