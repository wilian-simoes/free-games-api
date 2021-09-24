using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FreeGamesConsole
{
    public static class EpicGames
    {
        private static async Task<FreeGamesPromotions> GetFreeGamesPromotions()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://store-site-backend-static-ipv4.ak.epicgames.com/");

            var response = await httpClient.GetAsync("freeGamesPromotions?locale=pt-BR&country=BR&allowCountries=BR");
            if (!response.IsSuccessStatusCode)
                throw new Exception("Erro ao conectar-se a API da Epic Games.");

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<FreeGamesPromotions>(jsonResponse);
        }

        public static async Task<List<FreeGamesPromotions.Element>> ListarJogosGratis()
        {
            var novosJogos = await GetFreeGamesPromotions();
            return novosJogos.data.Catalog.searchStore.elements.Where(x => x.promotions != null).ToList();
        }

        public static DiscordMessage CriarRequest(List<FreeGamesPromotions.Element> jogos)
        {
            DiscordMessage discordMessage = new DiscordMessage
            {
                embeds = new List<Embed>()
            };

            jogos.ForEach(jogo => discordMessage.embeds.Add(
                new Embed()
                {
                    title = jogo.title,
                    url = "https://www.epicgames.com/store/pt-BR/p/" + jogo.urlSlug,
                    image = new Embed.Image()
                    {
                        url = jogo.keyImages[0].url.Replace(" ", "%20")
                    }
                }));

            return discordMessage;
        }

        public static async Task<bool> PostDiscord(DiscordMessage discordMessage)
        {
            var jsonDiscordMessage = JsonConvert.SerializeObject(discordMessage);

            var httpContent = new StringContent(jsonDiscordMessage, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            var response = await client.PostAsync("https://discord.com/api/webhooks/880941172770603059/EXoPt-3eyDrCrrMqHPkZbbtIxWqMRQB8oqbD1zVocfD-0p2oopcpCV5m23LTBueLD-P0", httpContent);

            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }
    }
}