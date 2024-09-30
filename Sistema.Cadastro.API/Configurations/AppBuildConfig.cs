using Sistema.Cadastro.API.Configurations.Hangfire;
using Sistema.Cadastro.API.Configurations.HealthCheck;
using Sistema.Cadastro.API.Middlewares;

namespace Sistema.Cadastro.API.Configurations
{
    public static class AppBuildConfig
    {
        public static IApplicationBuilder UseAppBuildConfiguration(this IApplicationBuilder builder, IConfiguration configuration)
        {

            builder.UseHealthCheckConfig();
            builder.UseHttpsRedirection();
            builder.UseAuthentication();
            builder.UseAuthorization();
            builder.UseMiddleware<ErrorHandlingMiddleware>();

            builder.UseHangFireServices();

            return builder;
        }
    }
}
