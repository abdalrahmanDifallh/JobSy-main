using JopSy.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace JopSy.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "المسمى الوظيفي مطلوب")]
        public string Title { get; set; }

        [Required(ErrorMessage = "اسم الشركة مطلوب")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "الراتب مطلوب")]
        public int Salary { get; set; }

        [Required(ErrorMessage = "التصنيف الوظيفي مطلوب")]
        public WorkType WorkType { get; set; }

        [Required(ErrorMessage = "الوصف مطلوب")]
        public string Description { get; set; }

        [Required(ErrorMessage = "العنوان مطلوب")]
        public int AddressId { get; set; }  
        public Address Address { get; set; } 


        [Required(ErrorMessage = "تاريخ النشر مطلوب")]
        [Display(Name = "تاريخ النشر")]
        [DataType(DataType.Date)]
        public DateTime PostedDate { get; set; }
    }
}