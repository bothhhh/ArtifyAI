using Microsoft.AspNetCore.Mvc;

namespace ArtifyAI.Controllers
{
    public class AIToolsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}