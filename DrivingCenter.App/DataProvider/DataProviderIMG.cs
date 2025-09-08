using DrivingCenter.App.Common;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;

namespace DrivingCenter.App.DataProvider
{
    public class DataProviderIMG
    {
        private static DataProviderIMG instance;

        public static DataProviderIMG Instance
        {
            get { if (instance == null) instance = new DataProviderIMG(); return DataProviderIMG.instance; }
            private set => instance = value;
        }

        public DataProviderIMG()
        {
        }

        // Chuỗi kết nối đến cơ sở dữ liệu
        private string connectionString = AppConstants.connectionStringDatImg;

        public DataTable ExecuteQuery(string query, object parameters = null)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = PrepareCommand(query, connection, parameters))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("ExecuteQuery", query, ex);
                throw; // Ném lại exception để caller xử lý nếu cần
            }

            return dt;
        }

        // Thực thi query trả về DataTable (async)
        public async Task<DataTable> ExecuteQueryAsync(string query, object parameters = null)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = PrepareCommand(query, connection, parameters))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("ExecuteQueryAsync", query, ex);
                throw;
            }

            return dt;
        }

        // Thực thi non-query (INSERT, UPDATE, DELETE) trả về số dòng ảnh hưởng
        public int ExecuteNonQuery(string query, object parameters = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = PrepareCommand(query, connection, parameters))
                    {
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("ExecuteNonQuery", query, ex);
                throw;
            }
        }

        // Thực thi non-query async
        public async Task<int> ExecuteNonQueryAsync(string query, object parameters = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = PrepareCommand(query, connection, parameters))
                    {
                        return await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("ExecuteNonQueryAsync", query, ex);
                throw;
            }
        }

        // Thực thi query trả về giá trị đơn (scalar)
        public object ExecuteScalar(string query, object parameters = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = PrepareCommand(query, connection, parameters))
                    {
                        return command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("ExecuteScalar", query, ex);
                throw;
            }
        }

        // Thực thi scalar async
        public async Task<object> ExecuteScalarAsync(string query, object parameters = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = PrepareCommand(query, connection, parameters))
                    {
                        return await command.ExecuteScalarAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("ExecuteScalarAsync", query, ex);
                throw;
            }
        }

        // Chuẩn bị SqlCommand với parameters
        private SqlCommand PrepareCommand(string query, SqlConnection connection, object parameters)
        {
            SqlCommand command = new SqlCommand(query, connection)
            {
                CommandType = CommandType.Text
            };

            if (parameters != null)
            {
                foreach (PropertyInfo prop in parameters.GetType().GetProperties())
                {
                    object value = prop.GetValue(parameters) ?? DBNull.Value;
                    command.Parameters.AddWithValue("@" + prop.Name, value);
                }
            }

            return command;
        }

        // Logging cơ bản (có thể thay bằng logger thật như Serilog, NLog)
        private void LogError(string method, string query, Exception ex)
        {
            Console.WriteLine($"[{DateTime.Now}] Error in {method}: {ex.Message}");
            Console.WriteLine($"Query: {query}");
        }
    }
}