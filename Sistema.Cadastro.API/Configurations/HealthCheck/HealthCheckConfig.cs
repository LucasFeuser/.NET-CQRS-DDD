using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

namespace Sistema.Cadastro.API.Configurations.HealthCheck
{
    public static class HealthCheckConfig
    {
        public static IServiceCollection AddHealthCheckService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks();

            return services;
        }

        public static IApplicationBuilder UseHealthCheckConfig(this IApplicationBuilder app)
        {

            app.UseHealthChecks("/status");

            app.UseHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            return app;
        }
    }
}
