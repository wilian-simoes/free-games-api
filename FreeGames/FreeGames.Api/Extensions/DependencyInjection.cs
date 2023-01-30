using FreeGames.Api.Services;
using FreeGames.Application.Interfaces.Services;
using FreeGames.Data.Context;
using FreeGames.Identity.Data;
using FreeGames.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FreeGames.Api.Extensions
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("FreeGamesConnection"))
            );

            services.AddDbContext<IdentityDataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("FreeGamesConnection"))
            );

            // InMemory Database
            //services.AddDbContext<DataContext>(options =>
            //options.UseInMemoryDatabase("InMemoryDatabase"));

            //services.AddDbContext<IdentityDataContext>(options =>
            //    options.UseInMemoryDatabase("InMemoryDatabase"));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<EpicGames_Service>();
            services.AddScoped<Discord_Service>();
        }
    }
}