using FreeGames.Domain.Models;

namespace FreeGames.Domain.Interfaces.Services
{
    public interface IDiscordService
    {
        Task<bool> PostDiscord(DiscordMessage discordMessage, string url_webhook);
    }
}