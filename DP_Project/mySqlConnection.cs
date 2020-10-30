using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using MySql.Data.MySqlClient;


namespace DP_Project
{
    public class mySqlConnection : SQLConnector
    {
        string myConnectionString = "server=localhost;database=testDB;uid=root;pwd=abc123;";

        private static MySqlConnection Sc = null;  //single object
        public static MySqlConnection Get()
        {
            if (Sc == null)
            {
                Sc = new MySqlConnection();
                Sc.ConnectionString = @"Data Source=.;Initial Catalog=McSQL;Integrated Security=True";
                Sc.Open();

            }
            return Sc;
        }


        public override MySqlConnection GetMySql()
        {
            string connetionString = null;
            MySqlConnection cnn;
            connetionString = "server=localhost;database=testDB;uid=root;pwd=abc123;";
            cnn = new MySqlConnection(connetionString);
            try
            {
                cnn.Open();
                MessageBox.Show("Connection Open ! ");
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }
            return cnn;
        }

        public override SqlConnection GetSql()
        {
             SqlConnection s = new SqlConnection();
            return s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        public class MySqlConnection
        {
            private string connetionString;

            public MySqlConnection()
            {
            }

            public MySqlConnection(string connetionString)
            {
                this.connetionString = connetionString;
            }

            public string ConnectionString { get; internal set; }

            internal void Close()
            {
                throw new NotImplementedException();
            }

            internal void Open()
            {
                throw new NotImplementedException();
            }
        }
    }
}
