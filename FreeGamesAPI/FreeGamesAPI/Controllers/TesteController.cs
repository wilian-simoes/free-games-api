﻿using FreeGamesAPI.Models;
using FreeGamesAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeGamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        private readonly Discord_Service _discordService;

        public TesteController(Discord_Service discordService)
        {
            _discordService = discordService;
        }

        [HttpPost("EnviarMensagem")]
        public async Task<ActionResult> EnviarMensagem([FromQuery]string title, [FromQuery] string urlImagem)
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

            if(!response)
                return BadRequest();

            return Ok();
        }
    }
}