using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingCenter.App.DataProvider
{
    public class DataProviderDAT
    {
        private static DataProviderDAT instance;

        public static DataProviderDAT Instance
        {
            get { if (instance == null) instance = new DataProviderDAT(); return DataProviderDAT.instance; }
            private set => instance = value;
        }

        public DataProviderDAT()
        {
        }

        ////-------Server--------------------------
        private string connectionSTR = @"Data Source=117.3.1.31,1433;Initial Catalog=I2EDAT2023;User ID=it;Password=@97UiLYW0Fwkl3qQWK($4My^";

        //private string connectionSTR = @"Data Source=192.168.50.253;Initial Catalog=I2EDAT2023;User ID=it;Password=@97UiLYW0Fwkl3qQWK($4My^";

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

        public int ExcuteQueryReturn(string query)
        {
            int returnValue;
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter returnParameter = new SqlParameter();

                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnParameter);

                    // Thực thi Stored Procedure
                    command.ExecuteNonQuery();

                    // Lấy giá trị trả về
                    returnValue = (int)returnParameter.Value;
                }

                connection.Close();
            }
            return returnValue;
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
    }
}