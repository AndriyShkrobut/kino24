using StackExchange.Redis;

namespace kino24_like.StartupExtensions
{
    public static class RedisExtensions
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration Configuration)
      {
            string host = Environment.GetEnvironmentVariable("REDIS_HOST");
            string password = Environment.GetEnvironmentVariable("REDIS_PASSWORD");
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(new ConfigurationOptions() 
            { 
                EndPoints = { { host, 6379 } }, 
                Password = password
            }));

            return services;
        }
    }
}
