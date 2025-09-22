// 代码生成时间: 2025-09-22 08:51:48
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;

namespace AuthenticationService
{    // 命名空间
    public class UserAuthenticationService
    {
        private Dictionary<string, string> _userCredentials;
        private readonly string _salt;

        public UserAuthenticationService()
        {
            // 初始化用户凭据存储和盐值
            _userCredentials = new Dictionary<string, string>();
            _salt = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 注册新用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public bool RegisterUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Username or password cannot be null or empty.");
            }

            if (_userCredentials.ContainsKey(username))
            {
                return false; // 用户名已存在
            }

            // 存储加密后的密码
            _userCredentials[username] = HashPassword(password, _salt);
            return true;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>是否登录成功</returns>
        public bool Login(string username, string password)
        {
            if (_userCredentials.TryGetValue(username, out var storedPasswordHash))
            {
                // 比较存储的哈希值和新密码的哈希值
                return storedPasswordHash == HashPassword(password, _salt);
            }
            return false; // 用户名不存在
        }

        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="salt">盐值</param>
        /// <returns>加密后的密码</returns>
        private string HashPassword(string password, string salt)
        {
            // 使用SHA256算法加密密码
            using (SHA256 sha256 = SHA256.Create())
            {
                var plainTextBytes = Encoding.UTF8.GetBytes(password + salt);
                var hashBytes = sha256.ComputeHash(plainTextBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
