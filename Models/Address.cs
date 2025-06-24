using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JopSy.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "المدينة مطلوبة")]
        [StringLength(50)]
        public string City { get; set; }

        [Required(ErrorMessage = "المنطقة مطلوبة")]
        [StringLength(50)]
        public string Area { get; set; }

        [Required(ErrorMessage = "الشارع مطلوب")]
        [StringLength(100)]
        public string Street { get; set; }

        // العلاقة مع الوظائف
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}