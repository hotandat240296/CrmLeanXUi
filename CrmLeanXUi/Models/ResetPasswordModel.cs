using System.ComponentModel.DataAnnotations;

namespace CrmLeanXUi.Models
{
    public class ResetPasswordModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your email")]
        public string email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the OTP sent to your email")]
        public string otp { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter your new password")]
        [StringLength(maximumLength: 16, MinimumLength = 6, ErrorMessage = "Length must be between 3 to 16")]
        public string newPassword { get; set; }
    }
}
