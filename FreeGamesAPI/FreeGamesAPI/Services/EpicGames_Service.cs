using FreeGamesAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FreeGamesAPI.Services
{
    public class EpicGames_Service
    {
        private readonly Discord_Service _discord_service;

        public EpicGames_Service(Discord_Service discordService)
        {
            _discord_service = discordService;
        }

        private async Task<FreeGamesPromotions> GetFreeGamesPromotions()
        {
            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://store-site-backend-static-ipv4.ak.epicgames.com/")
            };

            var response = await httpClient.GetAsync("freeGamesPromotions?locale=pt-BR&country=BR&allowCountries=BR");
            if (!response.IsSuccessStatusCode)
                throw new Exception("Erro ao conectar-se a API da Epic Games.");

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<FreeGamesPromotions>(jsonResponse);
        }

        private async Task<List<FreeGamesPromotions.Element>> ListarJogosGratis()
        {
            var novosJogos = await GetFreeGamesPromotions();
            return novosJogos.data.Catalog.searchStore.elements.Where(x => x.promotions != null && x.price.totalPrice.discountPrice == 0).ToList();
        }

        private DiscordMessage CriarRequest(List<FreeGamesPromotions.Element> jogos)
        {
            DiscordMessage discordMessage = new DiscordMessage
            {
                embeds = new List<Embed>()
            };

            jogos.ForEach(jogo => discordMessage.embeds.Add(
                new Embed()
                {
                    title = jogo.effectiveDate.Year == DateTime.Now.Year ? jogo.title : jogo.title + " / (EM BREVE)",
                    url = "https://www.epicgames.com/store/pt-BR/p/" + jogo.urlSlug,
                    image = new Embed.Image()
                    {
                        url = jogo.keyImages[0].url.Replace(" ", "%20")
                    }
                }));

            return discordMessage;
        }

        public async Task<string> GetFreeGamesAsync()
        {
            var jogos = await ListarJogosGratis();
            var discordMessage = CriarRequest(jogos);

            var url_webhook = @"https://discord.com/api/webhooks/880941172770603059/EXoPt-3eyDrCrrMqHPkZbbtIxWqMRQB8oqbD1zVocfD-0p2oopcpCV5m23LTBueLD-P0";
            bool enviado = await _discord_service.PostDiscord(discordMessage, url_webhook);

            if (!enviado)
                return "Mensagem não enviada.";

            return JsonConvert.SerializeObject(discordMessage);
        }
    }
}