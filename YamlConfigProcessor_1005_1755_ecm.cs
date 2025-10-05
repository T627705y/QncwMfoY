// 代码生成时间: 2025-10-05 17:55:44
// YAML配置文件处理器
// 此文件包含一个用于处理YAML配置文件的类

using System;
using YamlDotNet;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace YamlConfigProcessor
{
    // 定义一个异常类，用于处理YAML配置文件解析过程中的错误
    public class YamlConfigException : Exception
    {
        public YamlConfigException(string message) : base(message)
        {
        }
    }

    // YAML配置处理器类
    public class YamlConfigProcessor
    {
        private readonly string _configFilePath;

        public YamlConfigProcessor(string configFilePath)
        {
            _configFilePath = configFilePath;
        }

        // 解析YAML配置文件
        public T LoadConfig<T>() where T : class
        {
            try
            {
                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(UnderscoredNamingConvention.Instance)
                    .Build();

                var configText = System.IO.File.ReadAllText(_configFilePath);
                return deserializer.Deserialize<T>(configText);
            }
            catch (Exception ex)
            {
                // 抛出自定义异常，包含错误信息
                throw new YamlConfigException($"Error loading YAML config file: {ex.Message}");
            }
        }
    }
}
