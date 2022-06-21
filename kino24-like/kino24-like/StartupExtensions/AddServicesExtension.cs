using kino24_like.Core;
using Hangfire;
using Microsoft.AspNetCore.Identity;

namespace kino24_like.StartupExtensions
{
    public static class AddServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddMvc();
            services.AddHangFire();
            services.AddHangfireServer();
            services.AddAuthentication(Configuration);
            services.AddDataAccess(Configuration);
            
            services.AddCors();
            services.AddSwagger();
            services.AddRedis(Configuration);
            services.AddStackExchangeRedisCache(options => options.Configuration = Configuration.GetConnectionString("Redis"));

            services.AddControllers()
                    .AddNewtonsoftJson();
            services.AddLogging();
            services.AddAuthorization();

            services.AddDependency(Configuration);

            return services;
        }
    }
}
