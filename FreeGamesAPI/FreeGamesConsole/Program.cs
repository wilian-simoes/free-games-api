using System;
using System.Threading.Tasks;

namespace FreeGamesConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var jogos = await EpicGames.ListarJogosGratis();
            var discordMessage = EpicGames.CriarRequest(jogos);
            await EpicGames.PostDiscord(discordMessage);
            
            Console.ReadKey();
        }
    }
}