using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JopSy.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "المدينة مطلوبة")]
        [StringLength(50, ErrorMessage = "المدينة يجب ألا تتجاوز 50 حرفًا")]
        public string City { get; set; }

        [Required(ErrorMessage = "المنطقة مطلوبة")]
        [StringLength(50, ErrorMessage = "المنطقة يجب ألا تتجاوز 50 حرفًا")]
        public string Area { get; set; }

        [Required(ErrorMessage = "الشارع مطلوب")]
        [StringLength(100, ErrorMessage = "الشارع يجب ألا يتجاوز 100 حرف")]
        public string Street { get; set; }
    }
}