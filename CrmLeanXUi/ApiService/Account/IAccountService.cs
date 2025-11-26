using CrmLeanXUi.Models;
using CrmLeanXUi.ViewModel.ApiResponse.Authencation;
using CrmLeanXUi.ViewModel.ApiResponse.ManagementUser;

namespace CrmLeanXUi.ApiService.Account
{
    public interface IAccountService
    {
        Task<ResultRegister> RegisterUserAsync(RegisterModel request);
        Task<ResultLogin> LoginUserAsync(LoginModel request);
        Task<ResultForgotPassword> ForgotPasswordUserAsync(ForgotPasswordModel request);
        Task<ResultResetPassword> ResetPasswordUserAsync(ResetPasswordModel request);
        Task<ResultChangePassword> ChangePasswordUserAsync(ChangePasswordModel request);
        Task<ResultLogout> LogoutAsync();
        Task<ResultInfoUser> GetInfoUser();
    }
}
