using FreeGames.Domain.Entities;
using FreeGames.Domain.Interfaces.Repositories;
using FreeGames.Domain.Interfaces.Services;
using FreeGames.Domain.Services.Shared;

namespace FreeGames.Domain.Services
{
    public class DiscordConfigurationService : ServiceBase<DiscordConfiguration>, IDiscordConfigurationService
    {
        protected readonly IDiscordConfigurationRepository _discordConfigurationRepository;

        public DiscordConfigurationService(IDiscordConfigurationRepository discordConfigurationRepository) : base(discordConfigurationRepository)
        {
            _discordConfigurationRepository = discordConfigurationRepository;
        }

        public async Task<DiscordConfiguration> ObterPorUserIdAsync(string userId)
        {
            return await _discordConfigurationRepository.ObterPorUserIdAsync(userId);
        }
    }
}