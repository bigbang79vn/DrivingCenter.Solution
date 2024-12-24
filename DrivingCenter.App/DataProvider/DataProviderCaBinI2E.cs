using DevExpress.XtraCharts.Native;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DataProvider
{
    public class DataProviderCaBinI2E
    {
        private static DataProviderCaBinI2E instance;

        public static DataProviderCaBinI2E Instance
        {
            get { if (instance == null) instance = new DataProviderCaBinI2E(); return DataProviderCaBinI2E.instance; }
            private set => instance = value;
        }

        public DataProviderCaBinI2E()
        {
        }

        //----Data test----
        //private string connectionSTR = @"Data Source=TRUNGHIEU\SQLEXPRESS;Initial Catalog=I2ECABIN3D;User ID=sa;Password=hieu*123";

        //----Data Cabin----
        private string connectionSTR = @"Data Source=192.168.10.19;Initial Catalog=I2ECABIN3D;User ID=it;Password=hieu*123";

        public DataTable ExcuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPare = query.Split(' ');
                    int i = 0;
                    foreach (var item in listPare)
                    {
                        if (item.Contains('@'))
                        {
                            sqlCommand.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);

                adapter.Fill(data);

                connection.Close();
            }
            return data;
        }

        public int ExcuteNoneQuery(string query, object[] parameter = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPare = query.Split(' ');
                    int i = 0;
                    foreach (var item in listPare)
                    {
                        if (item.Contains('@'))
                        {
                            sqlCommand.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = sqlCommand.ExecuteNonQuery();

                connection.Close();
            }
            return data;
        }

        public object ExcuteScalar(string query, object[] parameter = null)
        {
            object data = 0;
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPare = query.Split(' ');
                    int i = 0;
                    foreach (var item in listPare)
                    {
                        if (item.Contains('@'))
                        {
                            sqlCommand.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = sqlCommand.ExecuteScalar();

                connection.Close();
            }
            return data;
        }

        public int ExecuteNonQueryNew(string query, SqlParameter[] parameters = null)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
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
    }
}