using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockApp.Core.Domain.IdentityEntities;
using StockApp.Core.Domain.RepositoryContracts;
using StockApp.Core.ServiceContracts;
using StockApp.Core.ServiceContracts.FinnhubService;
using StockApp.Core.Services;
using StockApp.Core.Services.FinnhubService;
using StockApp.Infrastructure.DbContext;
using StockApp.Infrastructure.Repositories;

namespace StockApp.UI.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static void ConfigureServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            builder.Services.AddHttpClient();

            builder.Services.AddSession();

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

            // App level
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                // Repository level
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
                .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

                options.AddPolicy("NotAuthenticated", options =>
                {
                    options.RequireAssertion(context =>
                        context.User.Identity is not null && !context.User.Identity.IsAuthenticated);
                });
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });
        }
    }
}
