using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using src.Enums;
using src.Interface;
using src.Models;
using src.ViewModels;

namespace src.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<Models.User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserRepository _userRepository;

        public AdminController(UserManager<Models.User> userManager, IUserRepository userRepository, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
        }
        // GET: /Admin/CreateStudent
        public IActionResult CreateStudent() => View();

        // POST: /Account/Register
        [HttpPost]
        public async Task<IActionResult> CreateStudent(RegisterViewModel model)
        {
            ModelState.Remove(nameof(model.Username)); //so validation passes :: username is generated later in code
            if (!ModelState.IsValid) return View(model);

            var user = new User
            {
                UserName = GenerateMatricNumber(),
                FullName = model.FullName,
                Gender = model.Gender,
                DOB = model.DateOfBirth,
                Program = model.Program,
                Level = model.Level,
                EntryMode = model.EntryMode
            };

            const string defaultPass = "user@Yeshua123";
            string role = Roles.Student.ToString();
            var result = await _userManager.CreateAsync(user, defaultPass);

            if (result.Succeeded)
            {
                if (await _roleManager.RoleExistsAsync(role))
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
                return RedirectToAction("Index", "Dashboard");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(model);
        }

        private string GenerateMatricNumber()
        {
            const string prefix = "UNI";
            int year = DateTime.Now.Year;
            int randomDigits = new Random().Next(1000, 9999); // 4-digit random number
            return $"{prefix}{year}{randomDigits}";
        }

        //admin dashboard
        public IActionResult Index() => View(); 
    }
}
