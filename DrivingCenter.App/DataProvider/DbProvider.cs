using Dapper;
using DrivingCenter.App.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DrivingCenter.App.DataProvider
{
    public sealed class DbProvider
    {
        private readonly string _connectionString;

        public DbProvider(DatabaseType databaseType)
        {
            _connectionString = DbConnectionFactory.GetConnection(databaseType);

            if (string.IsNullOrWhiteSpace(_connectionString))
                throw new Exception($"Không tìm thấy ConnectionString của {databaseType}");
        }

        #region Connection

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        #endregion

        #region Query

        public IEnumerable<T> Query<T>(string sql,object param = null,CommandType commandType = CommandType.Text)
        {
            using (var conn = CreateConnection())
            {
                return conn.Query<T>(sql, param, commandType: commandType);
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            using (var conn = CreateConnection())
            {
                return await conn.QueryAsync<T>(sql, param, commandType: commandType);
            }
        }

        #endregion

        #region Query Single

        public T QuerySingle<T>(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            using (var conn = CreateConnection())
            {
                return conn.QuerySingleOrDefault<T>(sql, param, commandType: commandType);
            }
        }

        public async Task<T> QuerySingleAsync<T>(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            using (var conn = CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<T>(sql, param, commandType: commandType);
            }
        }

        #endregion

        #region Execute

        public int Execute(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            using (var conn = CreateConnection())
            {
                return conn.Execute(sql, param, commandType: commandType);
            }
        }

        public async Task<int> ExecuteAsync(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            using (var conn = CreateConnection())
            {
                return await conn.ExecuteAsync(sql, param, commandType: commandType);
            }
        }

        #endregion

        #region Query Multiple

        public (SqlMapper.GridReader Reader, IDbConnection Connection) QueryMultiple( string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            var conn = CreateConnection();

            conn.Open();

            var reader = conn.QueryMultiple(sql, param, commandType: commandType);

            return (reader, conn);
        }

        public async Task<(SqlMapper.GridReader Reader, IDbConnection Connection)> QueryMultipleAsync( string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            var conn = CreateConnection();

            conn.Open();

            var reader = await conn.QueryMultipleAsync(sql, param, commandType: commandType);

            return (reader, conn);
        }

        #endregion

        #region DataTable

        public DataTable QueryToDataTable( string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            using (var conn = CreateConnection())
            using (var reader = conn.ExecuteReader(sql, param, commandType: commandType))
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
        }

        #endregion

        #region Transaction

        public void ExecuteTransaction(Action<IDbConnection, IDbTransaction> action)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();

                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        action(conn, tran);

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task ExecuteTransactionAsync(Func<IDbConnection, IDbTransaction, Task> action)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();

                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        await action(conn, tran);

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        #endregion
    }
}
