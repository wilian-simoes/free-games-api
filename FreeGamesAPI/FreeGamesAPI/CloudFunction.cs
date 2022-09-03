using Google.Cloud.Functions.Framework;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using FreeGamesAPI.Services;

namespace FreeGamesAPI
{
    // Define a class that implements the IHttpFunction interface
    public class CloudFunction : IHttpFunction
    {

        // Implement the HandleAsync() method to handle HTTP requests
        public async Task HandleAsync(HttpContext context)
        {
            EpicGames_Service _epicGamesService = new EpicGames_Service(new Discord_Service());
            // Your code here
            var response = await _epicGamesService.GetFreeGamesAsync();
            // Send an HTTP response
            await context.Response.WriteAsync(response);
        }
    }
}
