using Hangfire;
using Hangfire.PostgreSql;

namespace Sistema.Cadastro.API.Configurations.Hangfire
{
    public static class HangfireConfig
    {
        public static IServiceCollection AddHangfireService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(configuration.GetConnectionString("HangfireConnection"), new PostgreSqlStorageOptions
                {
                    PrepareSchemaIfNecessary = true,
                    DistributedLockTimeout = TimeSpan.FromMinutes(5),
                    InvisibilityTimeout = TimeSpan.FromMinutes(5),
                })
            );

            services.AddHangfireServer();

            return services;
        }

        public static IApplicationBuilder UseHangFireServices(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard();
            app.UseHangfireServer();

            return app;
        }

    }
}



