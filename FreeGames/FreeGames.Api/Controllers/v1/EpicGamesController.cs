using FreeGames.Api.Attributes;
using FreeGames.Domain.Interfaces.Services;
using FreeGames.Identity.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeGames.Api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpicGamesController : ControllerBase
    {
        private readonly IEpicGamesService _epicGamesService;

        public EpicGamesController(IEpicGamesService epicGamesService)
        {
            _epicGamesService = epicGamesService;
        }

        /// <summary>
        /// Envia para o canal do discord os jogos grátis da semana.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Roles.Admin)]
        [ClaimsAuthorize(ClaimTypes.Mensagem, "Enviar")]
        [HttpGet("GetFreeGames")]
        public async Task<ActionResult> GetFreeGames()
        {
            try
            {
                string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                var response = await _epicGamesService.GetFreeGamesAsync(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna o json com os jogos grátis da semana.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterJsonJogosGratis")]
        public async Task<ActionResult> ObterJsonJogosGratis()
        {
            try
            {
                var response = await _epicGamesService.ObterJsonJogosGratisAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}