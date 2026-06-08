using Dapper;
using DrivingCenter.App.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace App.DrivingManager.Dataprovider
{
    public sealed class DbProvider : IDisposable
    {
        private static readonly Lazy<DbProvider> _instance = new Lazy<DbProvider>(() => new DbProvider());
        // Chuỗi kết nối đến cơ sở dữ liệu
        private string _connectionString = AppConstants.connectionStringGplx_CDB;

        private IDbConnection _connection;
        private IDbTransaction _transaction;

        private DbProvider() { }

        public static DbProvider Instance => _instance.Value;

        private IDbConnection GetConnection()
        {
            if (string.IsNullOrEmpty(_connectionString))
                throw new InvalidOperationException("DataProvider chưa được khởi tạo.");

            if (_connection == null)
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
            }
            else if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }

            return _connection;
        }

        #region Transaction
        public void BeginTransaction()
        {
            if (_transaction == null)
            {
                var conn = GetConnection();
                _transaction = conn.BeginTransaction();
            }
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
            DisposeTransaction();
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            DisposeTransaction();
        }

        private void DisposeTransaction()
        {
            _transaction?.Dispose();
            _transaction = null;
        }
        #endregion

        #region Sync methods
        public IEnumerable<T> Query<T>(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            var conn = GetConnection();
            return conn.Query<T>(sql, param, transaction: _transaction, commandType: commandType);
        }

        public T QuerySingle<T>(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            var conn = GetConnection();
            return conn.QuerySingleOrDefault<T>(sql, param, transaction: _transaction, commandType: commandType);
        }

        public int Execute(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            var conn = GetConnection();
            return conn.Execute(sql, param, transaction: _transaction, commandType: commandType);
        }

        public SqlMapper.GridReader QueryMultiple(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure)
        {
            var conn = GetConnection();
            return conn.QueryMultiple(sql, param, transaction: _transaction, commandType: commandType);
        }

        public DataTable QueryToDataTable(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure)
        {
            var conn = GetConnection();
            var reader = conn.ExecuteReader(sql, param, transaction: _transaction, commandType: commandType);
            var dt = new DataTable();
            dt.Load(reader);
            return dt;
        }
        #endregion

        #region Async methods
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            var conn = GetConnection();
            return await conn.QueryAsync<T>(sql, param, transaction: _transaction, commandType: commandType);
        }

        public async Task<T> QuerySingleAsync<T>(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            var conn = GetConnection();
            return await conn.QuerySingleOrDefaultAsync<T>(sql, param, transaction: _transaction, commandType: commandType);
        }

        public async Task<int> ExecuteAsync(string sql, object param = null, CommandType commandType = CommandType.Text)
        {
            var conn = GetConnection();
            return await conn.ExecuteAsync(sql, param, transaction: _transaction, commandType: commandType);
        }

        public async Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure)
        {
            var conn = GetConnection();
            return await conn.QueryMultipleAsync(sql, param, transaction: _transaction, commandType: commandType);
        }
        #endregion

        public void Dispose()
        {
            DisposeTransaction();

            if (_connection != null)
            {
                if (_connection.State != ConnectionState.Closed)
                    _connection.Close();
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
