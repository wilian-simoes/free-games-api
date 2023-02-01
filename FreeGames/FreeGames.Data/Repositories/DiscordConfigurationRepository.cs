using FreeGames.Data.Context;
using FreeGames.Data.Repositories.Shared;
using FreeGames.Domain.Entities;
using FreeGames.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FreeGames.Data.Repositories
{
    public class DiscordConfigurationRepository : RepositoryBase<DiscordConfiguration>, IDiscordConfigurationRepository
    {
        public DiscordConfigurationRepository(DataContext dataContext) : base(dataContext) { }

        public async Task<DiscordConfiguration> ObterPorUserIdAsync(string userId)
        {
            return await Context.Set<DiscordConfiguration>().FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}