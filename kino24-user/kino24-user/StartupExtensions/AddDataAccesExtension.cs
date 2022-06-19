using kino24_user.core;
using Microsoft.EntityFrameworkCore;

namespace kino24_user.StartupExtensions
{
    public static class AddDataAccessExtension
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<UserServiceDBContext>(options =>
                 options.UseNpgsql(configuration.GetConnectionString("UserServiceDBConnection")));
               //options.UseSqlServer(configuration.GetConnectionString("UserServiceDBConnection")));


            return services;
        }
    }
}
