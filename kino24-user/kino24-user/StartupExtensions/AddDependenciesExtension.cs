using Microsoft.AspNetCore.Mvc.Infrastructure;
using kino24_user.BL.Interfaces;
using kino24_user.BL.Services;
using kino24_user.BL.Interfaces.Auth;
using kino24_user.BL.Interfaces.Jwt;
using kino24_user.BL.Interfaces.Logging;
using kino24_user.BL.Interfaces.UniqueId;
using kino24_user.BL.Services.Auth;
using kino24_user.BL.Services.Jwt;
using kino24_user.BL.Services.Logging;
using kino24_user.BL.Services.UniqueId;

namespace kino24_user.StartupExtensions
{
    public static class AddDependencyExtension
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));

            services.AddSingleton<IActionContextAccessor        , ActionContextAccessor>();
            services.AddScoped<IGlobalLoggerService             , GlobalLoggerService>();

            services.AddScoped<IAuthService       , AuthService>();
            services.AddTransient<IJwtService     , JwtService>();
            services.AddTransient<IUniqueIdService, UniqueIdService>();

            return services;
        }
    }
}
