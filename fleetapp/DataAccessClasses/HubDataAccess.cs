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
        public List<HubModel> GetHubAllocations()
        {
            using (IDbConnection connection = getConnection())
            {
                return connection.Query<HubModel>("select * from hub").ToList();
            }
        }
    }
}
