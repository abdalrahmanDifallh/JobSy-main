using System.ComponentModel.DataAnnotations;

namespace JopSy.ViewModel
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email address is required")]
        public string Email { get; set; }
        [Required(ErrorMessage =  "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "RememberMe")]
        public bool RememberMe { get; set; } 
    }
}
