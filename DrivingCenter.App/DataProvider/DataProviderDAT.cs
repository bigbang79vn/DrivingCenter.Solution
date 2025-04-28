using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DataProvider
{
    public class DataProviderDAT
    {
        // Singleton instance
        private static readonly DataProviderDAT instance = new DataProviderDAT();

        private readonly string connectionString = @"Data Source=192.168.50.253;Initial Catalog=I2EDAT2023;User ID=it;Password=@97UiLYW0Fwkl3qQWK($4My^";

        //private readonly string connectionString = @"Data Source=117.3.1.31,1433;Initial Catalog=I2EDAT2023;User ID=it;Password=@97UiLYW0Fwkl3qQWK($4My^";

        // Private constructor cho Singleton
        private DataProviderDAT() { }

        // Property Singleton
        public static DataProviderDAT Instance => instance;

        // Thực thi query trả về DataTable
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
