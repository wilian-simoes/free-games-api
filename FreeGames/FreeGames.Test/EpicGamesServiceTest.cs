using FreeGames.Domain.Interfaces.Services;
using FreeGames.Test.Config;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FreeGames.Test
{
    public class EpicGamesServiceTest
    {
        private readonly IEpicGamesService _epicGamesService;

        public EpicGamesServiceTest()
        {
            var serviceProvider = DependencyInjection.RegisterServices();

            // Resolver a dependência
            _epicGamesService = serviceProvider.GetRequiredService<IEpicGamesService>();
        }

        [Fact]
        public async Task ListaOsJogosGratisERetornaEmUmJson()
        {
            var response = await _epicGamesService.ObterJsonJogosGratisAsync();
            Assert.NotNull(response);
        }
    }
}