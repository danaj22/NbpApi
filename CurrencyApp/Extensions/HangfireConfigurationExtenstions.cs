using Hangfire;
using Microsoft.Data.SqlClient;

namespace CurrencyAppApi.Extensions
{
    public static class HangfireConfigurationExtenstions
    {

        public static IServiceCollection AddHangfire(this IServiceCollection services, 
            string? connectionString,
            string? dbName)
        {
            if (connectionString == null || dbName == null)
            {
                throw new Exception($"{nameof(connectionString)} or {nameof(dbName)} for Hangfire does not set.");
            }

            CreateDatabaseIfNotExists(connectionString, dbName);
            
            services.AddHangfire(opt =>
            {
                opt.UseSqlServerStorage(connectionString)
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings();
            });

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            return services;
        }

        private static void CreateDatabaseIfNotExists(string connectionString, string dbName)
        {
            using (var connection = new SqlConnection(string.Format(connectionString, "master")))
            {
                connection.Open();

                using (var command = new SqlCommand(string.Format(
                    @"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'{0}') 
                                    create database [{0}];
                      ", dbName), connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
