﻿using fleetapp.Models;
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
                return connection.Query<HubAllocationModel>($"select a.*, b.Name as HubName " +
                    $"from HubAllocation a, Hub b where  b.Id = a.HubID and a.ProjectID =  { Context.ProjectId }").ToList();
            }
        }

        public void InsertHubAllocation(HubAllocationModel newHubAllocation)
        {
            using (IDbConnection connection = getConnection())
            {
                String insertQuery = $"insert into HubAllocation (ProjectId, HubId, AssetModel, IsManned, IsAHS)" +
                    $" OUTPUT INSERTED.Id  " +
                    $" VALUES(@ProjectId, @HubId, @AssetModel, @IsManned, @IsAHS)";
                newHubAllocation.Id = connection.QuerySingle<int>(insertQuery, new
                {
                    newHubAllocation.ProjectId,
                    newHubAllocation.HubId,
                    newHubAllocation.AssetModel,
                    newHubAllocation.IsManned,
                    newHubAllocation.IsAHS
                });
            }
        }

        public void UpdateHubAllocation(HubAllocationModel HubAllocation)
        {
            using (IDbConnection connection = getConnection())
            {
                String updateQuery = $"update HubAllocation set AssetModel = @AssetModel, HubId = @HubId," +
                    $" IsManned= @IsManned, IsAHS = @IsAHS where Id = @Id";

                connection.Execute(updateQuery, new
                {
                    HubAllocation.HubId,
                    HubAllocation.AssetModel,
                    HubAllocation.IsManned,
                    HubAllocation.IsAHS,
                    HubAllocation.Id
                });
            }
        }

        public void DeleteHubAllocation(int HubAllocationId)
        {
            using (IDbConnection connection = getConnection())
            {
                connection.Execute($"delete from HubAllocation where Id = { HubAllocationId } ");
            }
        }
    }
}
