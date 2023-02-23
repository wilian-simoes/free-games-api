using FreeGames.Api.Models;
using FreeGames.Domain.Interfaces.Services;
using Newtonsoft.Json;

namespace FreeGames.Api.Services
{
    public class EpicGamesService
    {
        private readonly DiscordService _discordService;
        private readonly IDiscordConfigurationService _discordConfigurationService;

        public EpicGamesService(DiscordService discordService, IDiscordConfigurationService discordConfigurationService)
        {
            _discordService = discordService;
            _discordConfigurationService = discordConfigurationService;
        }

        private async Task<FreeGamesPromotions> GetFreeGamesPromotions()
        {
            HttpClient httpClient = new()
            {
                BaseAddress = new Uri("https://store-site-backend-static-ipv4.ak.epicgames.com/")
            };

            var response = await httpClient.GetAsync("freeGamesPromotions?locale=pt-BR&country=BR&allowCountries=BR");
            if (!response.IsSuccessStatusCode)
                throw new Exception("Erro ao conectar-se a API da Epic Games.");

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<FreeGamesPromotions>(jsonResponse);
        }

        private async Task<string> AtualizarListaJogos()
        {
            throw new NotImplementedException();
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

        public async Task<string> GetFreeGamesAsync(string userId)
        {
            var jogos = await ListarJogosGratis();
            var discordMessage = CriarRequest(jogos);

            var discordConfiguration = await _discordConfigurationService.ObterPorUserIdAsync(userId);

            bool enviado = await _discordService.PostDiscord(discordMessage, discordConfiguration.UrlWebhook);

            if (!enviado)
                return "Mensagem não enviada.";

            return JsonConvert.SerializeObject(discordMessage);
        }
    }
}