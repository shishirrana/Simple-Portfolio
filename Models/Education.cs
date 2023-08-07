using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace com.portfolio.website.Models
{
    public class Education
    {
        [Key]
        public int Id { get; set; }
        public int StartYear { get; set;}
        public int EndYear { get; set;}
        public bool IsCurrentlyEnrolled { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public decimal CGPA { get; set; }
        public string UniversityName { get; set; }

        public int PersonId { get; set; }

        [ValidateNever]
        public virtual PersonalInformation Person { get; set; }

    }
}
