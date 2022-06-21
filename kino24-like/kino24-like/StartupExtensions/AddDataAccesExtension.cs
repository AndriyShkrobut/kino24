using kino24_like.Core;
using Microsoft.EntityFrameworkCore;

namespace kino24_like.StartupExtensions
{
    public static class AddDataAccessExtension
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<FeedbackServiceDBContext>(options =>
                 options.UseNpgsql(configuration.GetConnectionString("FeedbackServiceDBConnection")));

            return services;
        }
    }
}
