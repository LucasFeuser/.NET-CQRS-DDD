using Sistema.Cadastro.API.Configurations.Culture;
using Sistema.Cadastro.API.Configurations.Swagger;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Localization;
using System.Text.Json.Serialization;
using System.ComponentModel;
using System.Reflection;
using Sistema.Cadastro.API.Configurations.HealthCheck;
using Microsoft.AspNetCore.Mvc;
using Sistema.Cadastro.API.Configurations.Json;

namespace Sistema.Cadastro.API.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection UseApiServices(this IServiceCollection services, IConfiguration configuration, Type startup)
        {
            services.AddHealthCheckService(configuration);
            services.SetDefaultCultureToBrazilian();

            services.Configure<JsonOptions>(options => {
                options.JsonSerializerOptions.Converters.Add(new JsonCustomConfig());
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.Configure<RequestLocalizationOptions>(options => options.DefaultRequestCulture = new RequestCulture("pt-BR"));

            AuthenticationOptions athenticationOptions = new AuthenticationOptions();
            configuration.GetSection("AuthenticationOptions").Bind(athenticationOptions);

            services.AddEndpointsApiExplorer();

            services.AddSwaggerService(athenticationOptions);

            return services;
        }
    }
}
