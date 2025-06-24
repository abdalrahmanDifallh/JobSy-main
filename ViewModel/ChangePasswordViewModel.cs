using System.ComponentModel.DataAnnotations;

namespace JopSy.ViewModel
{
    public class ChangePasswordViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email address is required")]

        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        [Compare("ConfirmNewPassword", ErrorMessage = "Password dosnt match")]
        public string NewPassword { get; set; }



        [Display(Name = "Confirm new password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        // [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmNewPassword { get; set; }
    }
}
