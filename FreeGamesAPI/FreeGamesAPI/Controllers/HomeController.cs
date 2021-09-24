using Microsoft.AspNetCore.Mvc;

namespace FreeGamesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            return Ok("ok");
        }
    }
}