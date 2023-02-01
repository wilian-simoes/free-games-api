using FreeGames.Domain.Entities;
using FreeGames.Domain.Interfaces.Services.Shared;

namespace FreeGames.Domain.Interfaces.Services
{
    public interface IDiscordConfigurationService : IServiceBase<DiscordConfiguration>
    {
        Task<DiscordConfiguration> ObterPorUserIdAsync(string userId);
    }
}