using FreeGames.Domain.Interfaces.Services;
using FreeGames.Domain.Models;
using Newtonsoft.Json;
using System.Text;

namespace FreeGames.Domain.Services
{
    public class DiscordService : IDiscordService
    {
        public async Task<bool> PostDiscord(DiscordMessage discordMessage, string url_webhook)
        {
            var jsonDiscordMessage = JsonConvert.SerializeObject(discordMessage);

            var httpContent = new StringContent(jsonDiscordMessage, Encoding.UTF8, "application/json");

            HttpClient client = new();
            var response = await client.PostAsync(url_webhook, httpContent);

            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }
    }
}