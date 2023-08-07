using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace com.portfolio.website.Controllers.VM
{
    public class SignupVM : Models.User
    {
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
