using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;

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

        /// <summary>Chạy truy vấn có tham số (@ten → giá trị) và trả về bảng kết quả; lỗi kết nối/truy vấn → bảng rỗng.</summary>
        public static DataTable Query(string sql, params (string Name, object Value)[] parameters)
        {
            var table = new DataTable();
            if (connection == null) return table;
            try
            {
                using (var command = BuildCommand(sql, parameters))
                using (var adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(table);   // Fill tự mở/đóng kết nối
                }
            }
            catch
            {
                if (connection.State != ConnectionState.Closed) connection.Close();
            }
            return table;
        }

        /// <summary>Chạy lệnh ghi có tham số; trả về số dòng bị ảnh hưởng (-1 nếu lỗi).</summary>
        public static int Execute(string sql, params (string Name, object Value)[] parameters)
        {
            if (connection == null) return -1;
            try
            {
                connection.Open();
                using (var command = BuildCommand(sql, parameters))
                {
                    return command.ExecuteNonQuery();
                }
            }
            catch
            {
                return -1;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed) connection.Close();
            }
        }

        private static MySqlCommand BuildCommand(string sql, (string Name, object Value)[] parameters)
        {
            var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = sql;
            foreach (var p in parameters)
                command.Parameters.AddWithValue(p.Name, p.Value ?? DBNull.Value);
            return command;
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
