using Microsoft.AspNetCore.Mvc;

namespace src.Controllers
{
    public class AnnouncementsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
