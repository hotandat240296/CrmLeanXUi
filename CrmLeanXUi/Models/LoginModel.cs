using System.ComponentModel.DataAnnotations;

namespace CrmLeanXUi.Models
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập Email của bạn")]
        public string username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập Password của bạn")]
        [StringLength(maximumLength: 100, MinimumLength = 8, ErrorMessage = "Độ dài phải nằm trong khoảng từ 8 đến 50 ký tự")]
        public string password { get; set; }

        public bool rememberMe { get; set; } = true;
    }
}
