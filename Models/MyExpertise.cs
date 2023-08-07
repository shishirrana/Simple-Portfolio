using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace com.portfolio.website.Models
{
    public class MyExpertise
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int PersonId { get; set; }
        
        [ValidateNever]
        public virtual PersonalInformation Person { get; set; }
    }
}
