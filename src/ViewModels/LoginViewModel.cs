using System.ComponentModel.DataAnnotations;

namespace src.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Matriculation number is required")]
        public string MatricNo { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
