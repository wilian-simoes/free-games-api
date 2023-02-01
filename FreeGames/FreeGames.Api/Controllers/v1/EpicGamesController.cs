using FreeGames.Api.Attributes;
using FreeGames.Api.Services;
using FreeGames.Identity.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeGames.Api.Controllers.v1
{
    [Authorize(Roles = Roles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class EpicGamesController : ControllerBase
    {
        private readonly EpicGamesService _epicGamesService;

        public EpicGamesController(EpicGamesService epicGamesService)
        {
            _epicGamesService = epicGamesService;
        }

        /// <summary>
        /// Envia para o canal do discord os jogos grátis da semana.
        /// </summary>
        /// <returns></returns>
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
    }
}