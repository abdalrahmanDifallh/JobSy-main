using System.ComponentModel.DataAnnotations;

namespace JopSy.Models
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "الاسم مطلوب")]
        public string FullName { get; set; }

       
        [Phone]
        public string PhoneNumber { get; set; }

        public string Summary { get; set; }
 
        public ICollection<Skill> Skills { get; set; } = new List<Skill>();
        public ICollection<Experience> Experiences { get; set; } = new List<Experience>();


    }
}
