using FreeGames.Data.Context;
using FreeGames.Data.Repositories;
using FreeGames.Domain.Interfaces.Repositories;
using FreeGames.Domain.Interfaces.Services;
using FreeGames.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FreeGames.Test.Config
{
    public static class DependencyInjection
    {
        public static IServiceProvider RegisterServices()
        {
            // Configurar o contêiner de DI
            var services = new ServiceCollection();

            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer("connectionString")
            );

            services.AddTransient<HttpClient>();
            services.AddTransient<IEpicGamesService, EpicGamesService>();
            services.AddTransient<IDiscordService, DiscordService>();
            services.AddTransient<IDiscordConfigurationService, DiscordConfigurationService>();
            services.AddTransient<IDiscordConfigurationRepository, DiscordConfigurationRepository>();

            // Construir o provedor de serviços
            return services.BuildServiceProvider();
        }
    }
}