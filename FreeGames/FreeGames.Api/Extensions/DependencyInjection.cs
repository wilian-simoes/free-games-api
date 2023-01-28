using FreeGames.Api.Services;

namespace FreeGames.Api.Extensions
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<EpicGames_Service>();
            services.AddScoped<Discord_Service>();
        }
    }
}