using System.ComponentModel.DataAnnotations;

namespace CrmLeanXUi.Models
{
    public class RegisterModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your name")]
        public string username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your email")]
        public string email { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z]).*$", ErrorMessage = "Password must contain at least one lowercase and one uppercase letter.")]
        [StringLength(maximumLength: 16, MinimumLength = 6, ErrorMessage = "Length must be between 6 to 16")]
        public string password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please confirm your password")]
        [Compare("password", ErrorMessage = "Passwords do not match")]
        public string rePassword { get; set; }
    }
}
