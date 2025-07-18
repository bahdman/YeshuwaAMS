using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.Interface;

namespace src.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly IUserRepository _userRepository;
        public StudentController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        //student dashboard
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account");

            var dahboardData = await _userRepository.GetStudentDashboard(userId);
            return View(dahboardData);
        }

        //profile
        public IActionResult Profile() => View();
        
    }
}