using Hangfire;
using kino24_like.Extensions;
using kino24_like.StartupExtensions;

namespace kino24_like
{
    public class Startup
    {
        private string[] _secrets = null;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Configuration["Jwt:Key"] = Environment.GetEnvironmentVariable("JWT_SECRET");

            var host = Environment.GetEnvironmentVariable("HOST");
            var password = Environment.GetEnvironmentVariable("PASSWORD");
            var dbconnection = "Host=" + host + ";" + Configuration["ConnectionStrings:FeedbackServiceDBConnection"] + "Password=" + password + ";";
            Configuration["ConnectionStrings:FeedbackServiceDBConnection"] = dbconnection;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env,
                              IServiceProvider serviceProvider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/V1/swagger.json", "kino24-like");
                                    c.RoutePrefix = string.Empty;
            });
            if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); }
            else
            {
                app.ConfigureCustomExceptionMiddleware();
                app.UseHsts();
            }
            app.UseWebSockets();
            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(builder =>
            {
                builder.AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowAnyOrigin();
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseHangfireDashboard();
            app.Run(async (context) =>
            {
                foreach (string secret in _secrets)
                {
                    var result = string.IsNullOrEmpty(secret) ? "Null" : "Not Null";
                    await context.Response.WriteAsync($"Secret is {result}");
                }
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _secrets = new string[] { };
            services.AddServices(Configuration);
        }
    }
}
