using Newtonsoft.Json;

namespace FreeGames.Api.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class DiscordMessage
    {
        public string content { get; set; }
        public List<Embed> embeds { get; set; }
    }

    public class Embed
    {
        public string title { get; set; }
        public string url { get; set; }
        public Image image { get; set; }

        public class Image
        {
            public string url { get; set; }
        }
    }
}