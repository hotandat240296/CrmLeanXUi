using System.ComponentModel.DataAnnotations;

namespace CrmLeanXUi.Models
{
    public class ChangePasswordModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your old password")]
        public string currentPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your new password")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Password length must be between 6 to 16 characters")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[\W_]).*$", ErrorMessage = "Password must contain at least 1 uppercase letter and 1 special character")]
        public string newPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please confirm your new password")]
        [Compare("newPassword", ErrorMessage = "Confirm password does not match new password")]
        public string confirmPassword { get; set; }
    }
}
