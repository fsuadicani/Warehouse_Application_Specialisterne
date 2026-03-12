using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetEnv;

namespace WarehouseStorage.Infrastructure
{
    public static class ConnectionString
    {
        public static string GetString()
        {
            Env.Load();
            
            var host = Environment.GetEnvironmentVariable("DB_HOST");
            var username = Environment.GetEnvironmentVariable("DB_USERNAME");
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
            var port = Environment.GetEnvironmentVariable("DB_PORT");
            var database = Environment.GetEnvironmentVariable("DB_NAME");

            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new Exception("Database environment variables are not set.");

            return $"Host={host};Port={port};Database={database};Username={username};Password={password}"; 
        }
    }
}