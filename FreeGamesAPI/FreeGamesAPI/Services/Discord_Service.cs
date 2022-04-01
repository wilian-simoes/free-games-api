using FreeGamesAPI.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FreeGamesAPI.Services
{
    public class Discord_Service
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