using com.portfolio.website.Models;

namespace com.portfolio.website.Controllers.VM
{
    public class HomePageVM
    {
        public PersonalInformation? PersonalInformation { get; set; }

        public  List<MyExpertise> MyExpertises { get; set; }

        public List<Education> Educations { get; set; }

        public List<Skill> Skills { get; set; }

        public List<Language> Languages { get; set; }

    }
}
