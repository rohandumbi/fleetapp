using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fleetapp.DataAccessClasses
{
    public class BaseDataAccess
    {
        protected String connectionString;

        public BaseDataAccess()
        {
            connectionString = ConfigurationManager.ConnectionStrings["FleetAppDB"].ConnectionString;
        }

        protected IDbConnection getConnection()
        {
            return new System.Data.SqlClient.SqlConnection(connectionString);
        }
    }
}
