﻿using FreeGames.Domain.Entities.Shared;

namespace FreeGames.Domain.Entities
{
    public class DiscordConfiguration : Entity
    {
        public string UserId { get; set; }
        public string UrlWebhook { get; set; }
        public string WebhookCode { get => UrlWebhook.Split("webhooks/")[1]; }
    }
}