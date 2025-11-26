using System.ComponentModel.DataAnnotations;

namespace CrmLeanXUi.Models
{
    public class ForgotPasswordModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your email")]
        public string email { get; set; }
    }
}
