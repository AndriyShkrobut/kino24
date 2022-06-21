using Hangfire;
using Hangfire.MemoryStorage;

namespace kino24_like.StartupExtensions
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
