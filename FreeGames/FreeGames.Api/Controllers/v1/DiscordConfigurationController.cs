using FreeGames.Domain.Entities;
using FreeGames.Domain.Interfaces.Services;
using FreeGames.Identity.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FreeGames.Api.Controllers.v1
{
    [Authorize(Roles = Roles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class DiscordConfigurationController : ControllerBase
    {
        private readonly IDiscordConfigurationService _discordConfigurationService;

        public DiscordConfigurationController(IDiscordConfigurationService discordConfigurationService)
        {
            _discordConfigurationService = discordConfigurationService;
        }

        /// <summary>
        /// Recupera a configuração do discord.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetConfiguracao")]
        public async Task<ActionResult<DiscordConfiguration>> GetConfiguracao()
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var discordConfiguration = await _discordConfigurationService.ObterPorUserIdAsync(userId);

            if (discordConfiguration == null)
                return Ok("Configuração não encontrada.");

            return Ok(discordConfiguration);
        }

        /// <summary>
        /// Cadastra uma configuração do discord.
        /// </summary>
        /// <param name="urlWebhook"></param>
        /// <returns></returns>
        [HttpPost("CadastrarConfiguracao")]
        public async Task<ActionResult<object>> CadastrarConfiguracao([Required(ErrorMessage = "urlWebhook não foi preenchida.")] string urlWebhook)
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var discordConfiguration = await _discordConfigurationService.ObterPorUserIdAsync(userId);

            if (discordConfiguration != null)
            {
                discordConfiguration.UrlWebhook = urlWebhook;
                await _discordConfigurationService.AtualizarAsync(discordConfiguration);
                return Ok(discordConfiguration);
            }

            return await _discordConfigurationService.AdicionarAsync(new DiscordConfiguration()
            {
                UserId = userId,
                UrlWebhook = urlWebhook
            });
        }
    }
}