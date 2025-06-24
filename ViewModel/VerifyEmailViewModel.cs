using System.ComponentModel.DataAnnotations;

namespace JopSy.ViewModel
{
    public class VerifyEmailViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email address is required")]
   
        [EmailAddress]
        public string Email { get; set; }
    }
}
 