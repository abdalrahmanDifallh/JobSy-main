using JopSy.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace JopSy.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }


        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "Password dosnt match")]
        public string Password { get; set; }

        [Required(ErrorMessage = "نوع الكيان مطلوب")]
        public EntityType EntityType { get; set; } // إضافة EntityType هنا

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
       // [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }

    }
}
