using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DP_Project.mySqlConnection;

namespace DP_Project
{
    public abstract class SQLConnector
    {
        public abstract SqlConnection GetSql();
        public abstract MySqlConnection GetMySql();
    }
}
