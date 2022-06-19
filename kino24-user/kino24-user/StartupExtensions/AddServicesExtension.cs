using Hangfire;
using Microsoft.AspNetCore.Identity;
using kino24_user.BL.Services.Jwt;
using kino24_user.core;
using kino24_user.core.Entities.User;

namespace kino24_user.StartupExtensions
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
            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<UserServiceDBContext>()
                    .AddDefaultTokenProviders();
            services.AddCors();
            services.AddSwagger();
            
            services.AddControllers()
                    .AddNewtonsoftJson();
            services.AddLogging();
            services.Configure<JwtOptions>(Configuration.GetSection("Jwt"));
            services.AddAuthorization();
            services.AddIdentityOptions();

            services.AddDependency();

            return services;
        }
    }
}
