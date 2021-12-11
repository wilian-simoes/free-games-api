using FreeGamesConsole;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FreeGamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpicGamesController : ControllerBase
    {
        public async Task<ActionResult> GetFreeGames()
        {
            try
            {
                var jogos = await EpicGames.ListarJogosGratis();
                var discordMessage = EpicGames.CriarRequest(jogos);
                bool enviado = await EpicGames.PostDiscord(discordMessage);

                if (enviado)
                    return Ok("Enviado com sucesso. Verifique o Discord.");
                else
                    return Ok("Mensagem não enviada.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}