using JopSy.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JopSy.Models
{
    public class Job
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

        [Required(ErrorMessage = "تاريخ نشر فرصه العمل مطلوب")]
        public DateTime PostedDate { get; set; } = DateTime.UtcNow;

        // ربط الوظيفة بالكيان
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        // ربط الوظيفة بالعنوان
        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address Address { get; set; }
    }
}