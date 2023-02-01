using FreeGames.Api.Models;
using Newtonsoft.Json;
using System.Text;

namespace FreeGames.Api.Services
{
    public class DiscordService
    {
        public async Task<bool> PostDiscord(DiscordMessage discordMessage, string url_webhook)
        {
            var jsonDiscordMessage = JsonConvert.SerializeObject(discordMessage);

            var httpContent = new StringContent(jsonDiscordMessage, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            var response = await client.PostAsync(url_webhook, httpContent);

            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }
    }
}