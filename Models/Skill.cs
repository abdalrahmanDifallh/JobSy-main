using System.ComponentModel.DataAnnotations;

namespace JopSy.Models
{
    public class Skill
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


    }
}