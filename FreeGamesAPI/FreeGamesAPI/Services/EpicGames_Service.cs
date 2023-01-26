using FreeGamesAPI.Models;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _config;

        public EpicGames_Service(Discord_Service discordService, IConfiguration config)
        {
            _discord_service = discordService;
            _config = config;
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

            var jogosFiltrados = novosJogos.data.Catalog.searchStore.elements.Where(e => e.promotions != null && e.offerType == "BASE_GAME").ToList();

            // Obs: Adicionado o filtro por discount e discountPrice desta forma pois os atributos discount e discountPrice são muitas vezes preenchidos de formas diferentes pela api da epic.
            jogosFiltrados = jogosFiltrados.Where(j => j.price.totalPrice.originalPrice == j.price.totalPrice.discount || j.price.totalPrice.originalPrice == j.price.totalPrice.discountPrice).ToList();

            var jogosGratis = new List<FreeGamesPromotions.Element>();

            foreach (var jogo in jogosFiltrados)
            {
                if (jogo.promotions.promotionalOffers != null && jogo.promotions.promotionalOffers.Count > 0)
                {
                    if (DateTime.Now.Date >= jogo.promotions.promotionalOffers[0].promotionalOffers[0].startDate.Date && DateTime.Now.Date <= jogo.promotions.promotionalOffers[0].promotionalOffers[0].endDate.Date)
                        jogosGratis.Add(jogo);
                }
                else
                {
                    if (jogo.promotions.promotionalOffers != null && jogo.promotions.upcomingPromotionalOffers != null && jogo.promotions.upcomingPromotionalOffers.Count > 0)
                    {
                        if (DateTime.Now <= jogo.promotions.upcomingPromotionalOffers[0].promotionalOffers[0].endDate)
                        {
                            jogo.title += jogo.title + $" (Disponível em {jogo.promotions.upcomingPromotionalOffers[0].promotionalOffers[0].startDate:dd/MM/yyyy})";
                            jogosGratis.Add(jogo);
                        }
                    }
                }
            }

            return jogosGratis;
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
                    title = jogo.title,
                    url = "https://www.epicgames.com/store/pt-BR/p/" + jogo.catalogNs.mappings.FirstOrDefault().pageSlug,
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

            bool enviado = await _discord_service.PostDiscord(discordMessage, _config["Discord:UrlWebhook"]);

            if (!enviado)
                return "Mensagem não enviada.";

            return JsonConvert.SerializeObject(discordMessage);
        }
    }
}