using JopSy.Data.Enum;
using JopSy.Models;
using System.ComponentModel.DataAnnotations;

namespace JopSy.ViewModel
{
    public class CreateJobViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "عنوان الوظيفة مطلوب")]
        [StringLength(100, ErrorMessage = "عنوان الوظيفة يجب ألا يتجاوز 100 حرف")]
        public string Title { get; set; }

        [Required(ErrorMessage = "الوصف مطلوب")]
        public string Description { get; set; }

        [Required(ErrorMessage = "نوع العقد مطلوب")]
        public ContractType ContractType { get; set; }

        [Required(ErrorMessage = "وضع العمل مطلوب")]
        public WorkMode WorkMode { get; set; }

      
        public DateTime PostedDate { get; set; } = DateTime.UtcNow;

        public Address Address { get; set; }

        public int UserId { get; set; }
    }
}
