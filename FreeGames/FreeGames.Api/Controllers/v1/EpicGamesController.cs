using FreeGames.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeGames.Api.Controllers.v1
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EpicGamesController : ControllerBase
    {
        private readonly EpicGames_Service _epicGamesService;

        public EpicGamesController(EpicGames_Service epicGamesService)
        {
            _epicGamesService = epicGamesService;
        }

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