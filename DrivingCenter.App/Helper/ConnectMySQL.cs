using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DrivingCenter.App.Helper
{
    public class ConnectMySQL
    {
        private static MySqlConnection connection;
        private static MySqlCommand cmd = null;

        public static void EstablishConnection()
        {
            try
            {
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                builder.Server = "192.168.50.10";
                builder.UserID = "root";
                builder.Password = "vNdc@123";
                builder.Database = "esmartn";

                builder.SslMode = MySqlSslMode.Disabled;
                connection = new MySqlConnection(builder.ToString());
            }
            catch
            {
                MessageBox.Show("connection Failed");
            }
        }

        public static MySqlCommand ExcuteNonQuery(string query)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch
            {
                connection.Close();
            }
            return cmd;
        }

        public static MySqlCommand ExcuteReader(string query)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.ExecuteReader();
                    connection.Close();
                }
            }
            catch
            {
                connection.Close();
            }
            return cmd;
        }

        public static MySqlCommand RunQuery(string query, string username)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch
            {
                connection.Close();
            }
            return cmd;
        }

        public static void Delay(double delay)
        {
            double delayTime = 0;
            while (delayTime < delay)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.05));
                delayTime++;
            }
        }
    }
}
