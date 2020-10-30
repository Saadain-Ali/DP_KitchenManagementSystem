using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DP_Project
{
    public class connection : SQLConnector
    {
        
        private connection() { }

        //singleton pattern
        private static SqlConnection Sc = null;  //single object
        public static SqlConnection Get()   //get instance method
        {
            if (Sc == null)
            {
                Sc = new SqlConnection();
                Sc.ConnectionString = @"Data Source=.;Initial Catalog=McSQL;Integrated Security=True";
                Sc.Open();

            }
            return Sc;

        }
        public static SqlConnection Get(SqlConnection sc)
        {
            Sc = sc;
            if (Sc == null)
            {
                Sc = new SqlConnection();
                Sc.ConnectionString = @"Data Source=.;Initial Catalog=McSQL;Integrated Security=True";
                Console.WriteLine("connnection Open ");
                Sc.Open();

            }
            return Sc;

        }

 

        //adapter pattern
        public override mySqlConnection.MySqlConnection GetMySql()
        {
            mySqlConnection.MySqlConnection mySql = new mySqlConnection.MySqlConnection();
            return mySql;
        }

        public override SqlConnection GetSql()
        {
            return Get();
        }

    }

}
