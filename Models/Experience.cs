using System.ComponentModel.DataAnnotations;

namespace JopSy.Models
{
    public class Experience
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string JobTitle { get; set; }

        public string Company { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; } // null لو ما زال يعمل

        public string Description { get; set; }
    }
}