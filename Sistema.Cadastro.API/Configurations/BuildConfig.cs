using Sistema.Cadastro.API.Configurations.HealthCheck;
using Sistema.Cadastro.API.Middlewares;

namespace Sistema.Cadastro.API.Configurations
{
    public static class BuildConfig
    {
        public static IApplicationBuilder UseAppBuildConfiguration(this IApplicationBuilder applicationBuilder, IConfiguration configuration)
        {

            applicationBuilder.UseHealthCheckConfig();
            applicationBuilder.UseHttpsRedirection();
            applicationBuilder.UseAuthentication();
            applicationBuilder.UseAuthorization();
            applicationBuilder.UseMiddleware<ErrorHandlingMiddleware>();

            return applicationBuilder;
        }
    }
}
