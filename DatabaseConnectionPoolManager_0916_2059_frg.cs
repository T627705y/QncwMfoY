// 代码生成时间: 2025-09-16 20:59:18
// <copyright file="DatabaseConnectionPoolManager.cs" company="YourCompany">
//     Copyright (c) YourCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Concurrent;
using System.Data.Common;
using System.Threading;
using Microsoft.Data.SqlClient;

namespace YourApp
{
    /// <summary>
    /// A class to manage a connection pool for database connections.
# 增强安全性
    /// </summary>
    public class DatabaseConnectionPoolManager
    {
        private readonly ConcurrentBag<DbConnection> _availableConnections;
        private readonly ConcurrentBag<DbConnection> _inUseConnections;
        private readonly int _maxConnections;
# 改进用户体验
        private readonly string _connectionString;
# FIXME: 处理边界情况
        private readonly Timer _connectionCleaner;

        /// <summary>
        /// Initializes a new instance of the DatabaseConnectionPoolManager class.
# 扩展功能模块
        /// </summary>
        /// <param name="connectionString">The database connection string.</param>
        /// <param name="maxConnections">The maximum number of connections to maintain in the pool.</param>
        public DatabaseConnectionPoolManager(string connectionString, int maxConnections)
        {
            _connectionString = connectionString;
            _maxConnections = maxConnections;
            _availableConnections = new ConcurrentBag<DbConnection>();
            _inUseConnections = new ConcurrentBag<DbConnection>();

            // Initialize a timer to clean up idle connections.
            _connectionCleaner = new Timer(CleanUpConnections, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
# 改进用户体验
        }

        /// <summary>
        /// Gets a connection from the pool.
        /// </summary>
        /// <returns>A database connection.</returns>
# 扩展功能模块
        public DbConnection GetConnection()
# 增强安全性
        {
# FIXME: 处理边界情况
            if (_availableConnections.TryTake(out var connection))
            {
                _inUseConnections.Add(connection);
                return connection;
            }

            // If no available connections, create a new one if under max limit.
            if (_availableConnections.Count < _maxConnections)
            {
                var newConnection = CreateNewConnection();
                _inUseConnections.Add(newConnection);
                return newConnection;
            }

            throw new InvalidOperationException("Connection pool limit reached.");
        }

        /// <summary>
        /// Releases a connection back to the pool.
        /// </summary>
        /// <param name="connection">The connection to release.</param>
        public void ReleaseConnection(DbConnection connection)
        {
            if (_inUseConnections.TryTake(connection))
# 改进用户体验
            {
                connection.Close();
                _availableConnections.Add(connection);
            }
        }

        /// <summary>
        /// Creates a new database connection.
        /// </summary>
        /// <returns>A new database connection.</returns>
        private DbConnection CreateNewConnection()
        {
            var connection = new SqlConnection(_connectionString);
# NOTE: 重要实现细节
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Cleans up idle connections in the pool.
        /// </summary>
        private void CleanUpConnections(object state)
# NOTE: 重要实现细节
        {
# NOTE: 重要实现细节
            foreach (var connection in _inUseConnections)
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    _inUseConnections.TryTake(connection);
                }
            }
        }

        /// <summary>
        /// Disposes of the connection pool and all managed resources.
# 扩展功能模块
        /// </summary>
        public void Dispose()
        {
            _connectionCleaner?.Dispose();

            foreach (var connection in _availableConnections)
            {
                connection.Dispose();
            }

            foreach (var connection in _inUseConnections)
            {
# 改进用户体验
                connection.Dispose();
            }
        }
    }
}
