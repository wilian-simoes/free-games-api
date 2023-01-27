using FreeGames.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FreeGames.Api.Controllers
{
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