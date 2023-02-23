using FreeGames.Domain.Entities;
using FreeGames.Domain.Interfaces.Services;
using FreeGames.Identity.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeGames.Api.Controllers.v1
{
    [Authorize(Roles = Roles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class DiscordConfigurationController : ControllerBase
    {
        private IDiscordConfigurationService _discordConfigurationService;

        public DiscordConfigurationController(IDiscordConfigurationService discordConfigurationService)
        {
            _discordConfigurationService = discordConfigurationService;
        }

        [HttpGet("GetConfiguracao")]
        public async Task<ActionResult<DiscordConfiguration>> GetConfiguracao()
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            return await _discordConfigurationService.ObterPorUserIdAsync(userId);
        }

        [HttpPost("CadastrarConfiguracao")]
        public async Task<ActionResult<object>> CadastrarConfiguracao(string urlWebhook)
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var discordConfiguration = new DiscordConfiguration()
            {
                UserId = userId,
                UrlWebhook = urlWebhook
            };

            return await _discordConfigurationService.AdicionarAsync(discordConfiguration);
        }
    }
}