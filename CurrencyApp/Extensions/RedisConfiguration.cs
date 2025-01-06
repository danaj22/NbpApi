using CurrencyAppApi.Services;
using StackExchange.Redis;
using System.Xml.Linq;

namespace CurrencyAppApi.Extensions
{
    public static class RedisConfiguration
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, string? connectionString)
        {
            if (connectionString == null)
            {
                throw new Exception($"{nameof(connectionString)} for Redis does not set.");
            }

            var redis = ConnectionMultiplexer.Connect(connectionString);

            services.AddSingleton<IConnectionMultiplexer>(redis);
            services.AddSingleton<ICacheService, CacheService>();
            return services;
        }
    }
}
