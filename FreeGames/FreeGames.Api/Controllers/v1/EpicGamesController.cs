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
        private readonly EpicGames_Service _epicGamesService;

        public EpicGamesController(EpicGames_Service epicGamesService)
        {
            _epicGamesService = epicGamesService;
        }

        [ClaimsAuthorize(ClaimTypes.Mensagem, "Enviar")]
        [HttpGet("GetFreeGames")]
        public async Task<ActionResult> GetFreeGames()
        {
            try
            {
                var response = await _epicGamesService.GetFreeGamesAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}