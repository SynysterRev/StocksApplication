using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.FinnhubService;
using Services;
using Services.FinnhubService;
using StockApp.Middlewares;

namespace StockApp
{
    public static class ConfigureServicesExtension
    {
        public static void ConfigureServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();

            builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
            builder.Services.AddScoped<IStocksService, StocksService>();
            builder.Services.AddScoped<IFinnhubCompanyProfileService, FinnhubCompanyProfileService>();
            builder.Services.AddScoped<IFinnhubStocksService, FinnhubStocksService>();
            builder.Services.AddScoped<IFinnhubSearchStocksService, FinnhubSearchStocksService>();
            builder.Services.AddScoped<IFinnhubStockPriceQuoteService, FinnhubStockPriceQuoteService>();
            builder.Services.AddScoped<IFinnhubRepository, FinnhubRepository>();
            builder.Services.AddScoped<IStocksRepository, StocksRepository>();

            //builder.Services.AddTransient<ExceptionHandlingMiddleware>();

            if (!builder.Environment.IsEnvironment("Test"))
            {
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            }
        }
    }
}
