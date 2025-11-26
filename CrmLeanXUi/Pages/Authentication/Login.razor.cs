using CrmLeanXUi.ApiService.Account;
using CrmLeanXUi.ApiService.Authentication;
using CrmLeanXUi.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CrmLeanXUi.Pages.Authentication
{
    public partial class Login
    {
        private bool _passwordVisibility;
        private InputType _passwordInput = InputType.Password;
        private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
        private LoginModel loginModel = new LoginModel();
        private bool isLoading = false;
        [Inject] private IAccountService _accountService { get; set; }
        [Inject] private IAuthenticationService _authencationService { get; set; }

        // Hàm submit login
        private async Task SubmitLoginAsync()
        {
            isLoading = true;

            try
            {
                await _authencationService.RemoveHotelIdAdminAccessAsync();
                await _authencationService.RemoveRoleNameAsync();
                await _authencationService.RemoveHotelNameAsync();

                var response = await _accountService.LoginUserAsync(loginModel);

                if (!string.IsNullOrEmpty(response.message))
                {
                    _snackBar.Add(response.message, Severity.Error);
                }

                var roleNames = response.roles?.Select(r => r.RoleName).ToList() ?? new List<string>();

                if (response.succeeded && roleNames.Contains("Admin"))
                {
                    Navigation.NavigateTo("/management/manage-user");
                    await Task.Delay(1000);
                    _snackBar.Add("Sign in success", Severity.Success);
                }
                else if (response.succeeded && (roleNames.Contains("HotelManager") || roleNames.Contains("Operator")))
                {
                    Navigation.NavigateTo($"/hotel/manage-role/{response.userId}/assign-hotels");

                }
                else if (response.succeeded && (roleNames.Contains("Customer") || roleNames.Count == 0))
                {
                    Navigation.NavigateTo("/home");
                    await Task.Delay(500);
                    _snackBar.Add("Sign in success", Severity.Success);
                }
                else
                {
                    _snackBar.Add(response.message, Severity.Error);
                }
            }
            catch (Exception ex)
            {
                _snackBar.Add("An error occurred during login.", Severity.Error);
            }
            finally
            {
                isLoading = false;
            }
        }


        // Hàm toggle password visibility
        void TogglePasswordVisibility()
        {
            if (_passwordVisibility)
            {
                _passwordVisibility = false;
                _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
                _passwordInput = InputType.Password;
            }
            else
            {
                _passwordVisibility = true;
                _passwordInputIcon = Icons.Material.Filled.Visibility;
                _passwordInput = InputType.Text;
            }
        }
    }
}
