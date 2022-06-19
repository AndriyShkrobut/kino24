using Microsoft.AspNetCore.Identity;

namespace kino24_user.StartupExtensions
{
    public static class AddIdentityOptionsExtension
    {
        public static IServiceCollection AddIdentityOptions(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            });

            return services;
        }
    }
}
