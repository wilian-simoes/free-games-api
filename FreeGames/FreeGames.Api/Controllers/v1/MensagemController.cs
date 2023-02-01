using FreeGames.Api.Attributes;
using FreeGames.Api.Models;
using FreeGames.Api.Services;
using FreeGames.Domain.Interfaces.Services;
using FreeGames.Domain.Services;
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
        private readonly DiscordService _discordService;
        private IDiscordConfigurationService _discordConfigurationService;

        public MensagemController(DiscordService discordService, IDiscordConfigurationService discordConfigurationService)
        {
            _discordService = discordService;
            _discordConfigurationService = discordConfigurationService;
        }

        /// <summary>
        /// Envia mensagem com imagem para o canal do discord.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="urlImagem"></param>
        /// <returns></returns>
        [ClaimsAuthorize(ClaimTypes.Mensagem, "Enviar")]
        [HttpPost("EnviarMensagem")]
        public async Task<ActionResult> EnviarMensagem([FromQuery] string title, [FromQuery] string urlImagem)
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var discordConfiguration = await _discordConfigurationService.ObterPorUserIdAsync(userId);

            DiscordMessage discordMessage = new()
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

            var response = await _discordService.PostDiscord(discordMessage, discordConfiguration.UrlWebhook);

            if (!response)
                return BadRequest();

            return Ok();
        }
    }
}