using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DataProvider
{
    public class DataProvider
    {
        private static DataProvider instance;

        private static readonly object lockObj = new object();

        private DataProvider() { }

        public static DataProvider Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                        instance = new DataProvider();
                    return instance;
                }
            }
        }

        //----Data test-----
        private string connectionString = @"Data Source=TRUNGHIEU\SQLEXPRESS;Initial Catalog=EDS_CT;User ID=sa;Password=hieu*123";

        //----Data Cabin----
        //private string connectionString = @"Data Source=192.168.10.19;Initial Catalog=EDS_CT;User ID=it;Password=hieu*123";

        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }

        public object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            object result;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    result = command.ExecuteScalar();
                }
            }

            return result;
        }
    }
}
