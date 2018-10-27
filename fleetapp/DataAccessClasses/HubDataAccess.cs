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
                return connection.Query<HubModel>($"select * from hub where ProjectID = { Context.ProjectID }").ToList();
            }
        }

        public void InsertHub(String HubName)
        {
            using (IDbConnection connection = getConnection())
            {
                String insertQuery = $"insert into Hub (ProjectID, Name) values ({ Context.ProjectID }, '{ HubName}')";
                connection.Execute(insertQuery);
            }
        }
    }
}
