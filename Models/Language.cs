using System.ComponentModel.DataAnnotations;

namespace com.portfolio.website.Models
{
    public class Language
    {
        [Key]
        public int Id { get; set; }
       
        [Required]
        public string LName { get; set; }

       
    }
}
