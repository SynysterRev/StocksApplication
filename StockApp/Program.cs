using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using RepositoryContracts;
using Serilog;
using ServiceContracts;
using Services;
using StockApp;
using StockApp.Middlewares;

var builder = WebApplication.CreateBuilder(args);

//builder.Logging.ClearProviders().AddConsole().AddDebug();

builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(context.Configuration) //read configuration settings from build int config (appsettings)
    .ReadFrom.Services(services); //read out current app's services and make them available to the serilog
});

builder.Services.ConfigureServices(builder);

var app = builder.Build();

if (!builder.Environment.IsEnvironment("Test"))
{
    Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", "Rotativa");
}

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseMiddleware<ExceptionHandlingMiddleware>();
}


app.UseStaticFiles();
app.UseRouting();
app.MapControllers();


app.Run();

public partial class Program { }