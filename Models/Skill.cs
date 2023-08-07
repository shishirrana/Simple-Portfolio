using System.ComponentModel.DataAnnotations;

namespace com.portfolio.website.Models
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1,10)]
        public decimal Rating { get; set; }
    }
}
