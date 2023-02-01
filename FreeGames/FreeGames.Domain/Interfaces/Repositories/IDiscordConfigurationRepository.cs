using FreeGames.Domain.Entities;
using FreeGames.Domain.Interfaces.Repositories.Shared;

namespace FreeGames.Domain.Interfaces.Repositories
{
    public interface IDiscordConfigurationRepository : IRepositoryBase<DiscordConfiguration>
    {
        Task<DiscordConfiguration> ObterPorUserIdAsync(string userId);
    }
}