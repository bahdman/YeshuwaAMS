using Microsoft.AspNetCore.Mvc;

namespace src.Controllers
{
    public class ResultsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
