using FreeGames.Api.Attributes;
using FreeGames.Api.Models;
using FreeGames.Api.Services;
using FreeGames.Identity.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeGames.Api.Controllers.v1
{
    [Authorize(Roles = Roles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class MensagemController : ControllerBase
    {
        private readonly Discord_Service _discordService;

        public MensagemController(Discord_Service discordService)
        {
            _discordService = discordService;
        }

        [ClaimsAuthorize(ClaimTypes.Mensagem, "Enviar")]
        [HttpPost("EnviarMensagem")]
        public async Task<ActionResult> EnviarMensagem([FromQuery] string title, [FromQuery] string urlImagem)
        {
            var url_webhook = @"https://discord.com/api/webhooks/880941172770603059/EXoPt-3eyDrCrrMqHPkZbbtIxWqMRQB8oqbD1zVocfD-0p2oopcpCV5m23LTBueLD-P0";

            DiscordMessage discordMessage = new DiscordMessage
            {
                embeds = new List<Embed>()
                {
                    new Embed
                    {
                        title = title,
                        url = string.Empty,
                        image = new Embed.Image()
                        {
                            url = urlImagem
                        }
                    }
                }
            };

            var response = await _discordService.PostDiscord(discordMessage, url_webhook);

            if (!response)
                return BadRequest();

            return Ok();
        }
    }
}