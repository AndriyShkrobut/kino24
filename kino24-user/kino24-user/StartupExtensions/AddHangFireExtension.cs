using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.DependencyInjection;

namespace kino24_user.StartupExtensions
{
    public static class AddHangFireExtension
    {
        public static IServiceCollection AddHangFire(this IServiceCollection services)
        {
            services.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseMemoryStorage();
            }
            );

            return services;
        }
    }
}
