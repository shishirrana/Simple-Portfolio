using System.ComponentModel.DataAnnotations;

namespace com.portfolio.website.Models
{
    public class PersonalInformation
    {
        [Key]
        public int  Id { get; set; }
        public string Name { get; set; }
        public string Highlight { get; set; }
        public string Summary { get; set;}
        public string CurrentRole { get; set;}
        public string Email { get; set; }
        public DateTime? DOB { get; set; }
        public string Phone { get; set;}
        public string? Skype { get; set;} 
        public string? Facebook { get; set;}
        public string? Twitter { get; set; }

        public string? Google { get; set; }

        public string? Instagram { get; set; }

        public string? GitHub { get; set; }

        public string? Address { get; set; }
        public string? Photo { get; set; }

    }

}
