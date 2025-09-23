// 代码生成时间: 2025-09-23 19:42:34
using Microsoft.Maui.Controls;
using System;
using System.Data;
using System.Data.Common;
using System.Linq;

// 命名空间为我们的程序提供必要的封装和组织
namespace PreventSqlInjectionMaui
{
    // 主要的视图模型类，用于实现防止SQL注入的功能
    public class SqlInjectionPreventionViewModel
    {
        private readonly string _connectionString;

        // 构造函数，用于初始化数据库连接字符串
        public SqlInjectionPreventionViewModel(string connectionString)
        {
            _connectionString = connectionString;
        }

        // 方法：获取用户列表，使用参数化查询防止SQL注入
        public DataTable GetUsers()
        {
            try
            {
                // 创建数据库连接对象
                using (DbConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // 创建参数化查询的SQL命令
                    string commandText = "SELECT * FROM Users";
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = commandText;

                    // 创建数据适配器，用于填充数据表
                    DbDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable users = new DataTable();
                    adapter.Fill(users);

                    return users;
                }
            }
            catch (Exception ex)
            {
                // 错误处理：记录异常信息并抛出
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        // 方法：添加用户，使用参数化查询防止SQL注入
        public bool AddUser(string username, string email)
        {
            try
            {
                // 创建数据库连接对象
                using (DbConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    // 创建参数化查询的SQL命令
                    string commandText = "INSERT INTO Users (Username, Email) VALUES (@Username, @Email)";
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = commandText;

                    // 添加参数，防止SQL注入
                    command.Parameters.Add(new SqlParameter("@Username", username));
                    command.Parameters.Add(new SqlParameter("@Email", email));

                    // 执行命令，返回影响的行数
                    int rowsAffected = command.ExecuteNonQuery();

                    // 如果有行受影响，则返回true，表示添加成功
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // 错误处理：记录异常信息并抛出
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
