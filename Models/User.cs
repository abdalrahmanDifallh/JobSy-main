using JopSy.Data.Enum;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace JopSy.Models
{
    public class User : IdentityUser
    {

        [Required(ErrorMessage = "اسم الكيان مطلوب")]
        [StringLength(100, ErrorMessage = "اسم الكيان يجب ألا يتجاوز 100 حرف")]
        public string FullName { get; set; }

        
        [Required(ErrorMessage = "نوع الكيان مطلوب")]
        public  EntityType EntityType { get; set; } // مثال: Company, Hospital, Store, Other

        // علاقة مع الوظائف المنشورة
        public ICollection<Job> Job { get; set; } = new List<Job>();
    }

}
