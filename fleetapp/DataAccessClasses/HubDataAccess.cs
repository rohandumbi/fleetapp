using fleetapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace fleetapp.DataAccessClasses
{
    public class HubDataAccess : BaseDataAccess
    {
        public List<HubModel> GetHubs()
        {
            using (IDbConnection connection = getConnection())
            {
                return connection.Query<HubModel>($"select ROW_NUMBER() OVER( ORDER BY Id asc ) AS 'HubNumber',* from hub where ProjectID = { Context.ProjectId }").ToList();
            }
        }

        public void InsertHub(HubModel newHub)
        {
            using (IDbConnection connection = getConnection())
            {
                String insertQuery = $"insert into Hub (ProjectID, Name) OUTPUT INSERTED.Id  values ( @ProjectId, @Name )";
                newHub.Id = connection.QuerySingle<int>(insertQuery, new { newHub.ProjectId, newHub.Name });
            }
        }

        public void DeletetHub(int HubId)
        {
            using (IDbConnection connection = getConnection())
            {
                connection.Execute($"delete from Hub where Id = { HubId } ");
            }
        }
    }
}
