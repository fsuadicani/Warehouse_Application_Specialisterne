using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WarehouseStorage.Infrastructure
{
    public static class ConnectionString
    {
        private static IConfiguration? _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string GetString()
        {
            if (_configuration == null)
                throw new InvalidOperationException("Configuration not initialized. Call Initialize first.");
            
            var host = _configuration["DB_HOST"];
            var username = _configuration["DB_USERNAME"];
            var password = _configuration["DB_PASSWORD"];
            var port = _configuration["DB_PORT"];
            var database = _configuration["DB_NAME"];

            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new Exception("Database environment variables are not set.");

            return $"Host={host};Port={port};Database={database};Username={username};Password={password}"; 
        }
    }
}