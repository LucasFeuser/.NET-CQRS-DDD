using Sistema.Cadastro.API.Configurations.HealthCheck;
using Sistema.Cadastro.API.Configurations.Culture;
using Sistema.Cadastro.API.Configurations.Swagger;
using Sistema.Cadastro.API.Configurations.Json;
using Sistema.Cadastro.CrossCutting.IoC;
using Microsoft.AspNetCore.Localization;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Sistema.Cadastro.API.Configurations.Hangfire;

namespace Sistema.Cadastro.API.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection UseApiServices(this IServiceCollection services, IConfiguration configuration, Type startup)
        {
            services.AddHealthCheckService(configuration);
            services.SetDefaultCultureToBrazilian();
            services.AddControllers();

            services.Configure<JsonOptions>(options => {
                options.JsonSerializerOptions.Converters.Add(new JsonCustomConfig());
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.Configure<RequestLocalizationOptions>(options => options.DefaultRequestCulture = new RequestCulture("pt-BR"));


            services.AddEndpointsApiExplorer();
            services.DependencyInjection(configuration, startup);

            services.AddSwaggerService();
            services.AddHangfireService(configuration);

            return services;
        }
    }
}
