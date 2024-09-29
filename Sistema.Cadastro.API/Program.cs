using Sistema.Cadastro.API.Configurations;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.UseApiServices(builder.Configuration, typeof(Program));
    var app = builder.Build();
    app.UseAppBuildConfiguration(builder.Configuration);

    if (!app.Environment.IsProduction())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    //FATAL ERROR
	throw;
}
