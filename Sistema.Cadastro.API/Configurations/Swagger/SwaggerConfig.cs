using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Authentication;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json;

namespace Sistema.Cadastro.API.Configurations.Swagger
{
    public static class SwaggerConfig
    {

        public static IServiceCollection AddSwaggerService(this IServiceCollection services, AuthenticationOptions authOptions)
        {

            services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = ApiVersion.Default;
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                // Agrupar por número de versão
                options.GroupNameFormat = "'v'VVV";
                // Necessário para o correto funcionamento das rotas
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new()
                {
                    Version = "v1",
                    Title = "Sistema Cadastro",
                    Description = "Esta API e apenas um template"
                });

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                c.UseInlineDefinitionsForEnums();

                c.CustomSchemaIds(x => x.FullName);
                c.OperationFilter<SwaggerDefaultValues>();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }


        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder configuration)
        {
            configuration.UseSwagger();

            var provider = configuration.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            configuration.UseSwaggerUI(options =>
            {
                // Geração de um endpoint do Swagger para cada versão descoberta
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", $"API {description.GroupName}");
                }
            });

            return configuration;
        }



        public class SwaggerDefaultValues : IOperationFilter
        {
            /// <summary>
            /// Applies the filter to the specified operation using the given context.
            /// </summary>
            /// <param name="operation">The operation to apply the filter to.</param>
            /// <param name="context">The current operation filter context.</param>
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                var apiDescription = context.ApiDescription;

                operation.Deprecated |= apiDescription.IsDeprecated();

                // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1752#issue-663991077
                foreach (var responseType in context.ApiDescription.SupportedResponseTypes)
                {
                    // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/b7cf75e7905050305b115dd96640ddd6e74c7ac9/src/Swashbuckle.AspNetCore.SwaggerGen/SwaggerGenerator/SwaggerGenerator.cs#L383-L387
                    var responseKey = responseType.IsDefaultResponse ? "default" : responseType.StatusCode.ToString();
                    var response = operation.Responses[responseKey];

                    foreach (var contentType in response.Content.Keys)
                    {
                        if (!responseType.ApiResponseFormats.Any(x => x.MediaType == contentType))
                        {
                            response.Content.Remove(contentType);
                        }
                    }
                }

                if (operation.Parameters == null)
                {
                    return;
                }

                // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/412
                // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/pull/413
                foreach (var parameter in operation.Parameters)
                {
                    var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

                    if (parameter.Description == null)
                    {
                        parameter.Description = description.ModelMetadata?.Description;
                    }

                    if (parameter.Schema.Default == null && description.DefaultValue != null)
                    {
                        // REF: https://github.com/Microsoft/aspnet-api-versioning/issues/429#issuecomment-605402330
                        var json = JsonSerializer.Serialize(description.DefaultValue, description!.ModelMetadata!.ModelType);
                        parameter.Schema.Default = OpenApiAnyFactory.CreateFromJson(json);
                    }

                    parameter.Required |= description.IsRequired;
                }
            }
        }
    }
}
