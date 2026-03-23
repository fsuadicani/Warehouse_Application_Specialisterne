using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            if(string.IsNullOrEmpty(username))
                throw new InvalidOperationException($"Required database configuration value: {nameof(username)} not set.");
            if(string.IsNullOrEmpty(host))
                throw new InvalidOperationException($"Required database configuration value: {nameof(host)} not set.");
            if(string.IsNullOrEmpty(password))
                throw new InvalidOperationException($"Required database configuration value: {nameof(password)} not set.");
            if(string.IsNullOrEmpty(port))
                throw new InvalidOperationException($"Required database configuration value: {nameof(port)} not set.");           
            if(string.IsNullOrEmpty(database))
                throw new InvalidOperationException($"Required database configuration value: {nameof(database)} not set."); 

            string connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password};";

            if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                connectionString += "Include Error Detail=true;";

            return connectionString; 
        }
    }
}