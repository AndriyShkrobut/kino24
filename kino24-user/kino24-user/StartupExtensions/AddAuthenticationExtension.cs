using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace kino24_user.StartupExtensions
{
    public static class AddAuthenticationExtension
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration )
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                 .AddJwtBearer(config =>
                 {
                     config.RequireHttpsMetadata = false;
                     config.SaveToken = true;

                     config.TokenValidationParameters = new TokenValidationParameters()
                     {
                         ValidIssuer = configuration["Jwt:Issuer"],
                         ValidAudience = configuration["Jwt:Audience"],
                         ValidateLifetime = true,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                         ClockSkew = TimeSpan.Zero
                     };
                 });

            return services;
        }
    }
}
