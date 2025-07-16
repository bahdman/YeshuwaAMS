using Microsoft.AspNetCore.Mvc;

namespace src.Controllers
{
    public class ComplaintsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
