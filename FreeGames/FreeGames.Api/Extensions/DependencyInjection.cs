using FreeGames.Application.Interfaces.Services;
using FreeGames.Data.Context;
using FreeGames.Data.Repositories;
using FreeGames.Domain.Interfaces.Repositories;
using FreeGames.Domain.Interfaces.Services;
using FreeGames.Domain.Services;
using FreeGames.Identity.Data;
using FreeGames.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
            services.AddScoped<IEpicGamesService, EpicGamesService>();
            services.AddScoped<IDiscordService, DiscordService>();
            services.AddScoped<IDiscordConfigurationService, DiscordConfigurationService>();
            services.AddScoped<IDiscordConfigurationRepository, DiscordConfigurationRepository>();
        }
    }
}