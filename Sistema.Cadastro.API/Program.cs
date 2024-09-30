using Sistema.Cadastro.API.Configurations;
using Sistema.Cadastro.API.Configurations.Swagger;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.UseApiServices(builder.Configuration, typeof(Program));

    var app = builder.Build();
    app.UseAppBuildConfiguration(builder.Configuration);

    if (!app.Environment.IsProduction())
    {
        app.UseSwaggerConfig();
    }

    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    //FATAL ERROR
    throw;
}
