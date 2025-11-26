using CrmLeanXUi.ApiService.Authentication;
using CrmLeanXUi.Models;
using CrmLeanXUi.ViewModel.ApiResponse.Authencation;
using CrmLeanXUi.ViewModel.ApiResponse.ManagementUser;
using Microsoft.AspNetCore.Components.Routing;
using System.Net.Http.Json;
using System.Text.Json;

namespace CrmLeanXUi.ApiService.Account
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthenticationService _authenticationService;

        public AccountService(HttpClient httpClient, IAuthenticationService authenticationService)
        {
            _httpClient = httpClient;
            _authenticationService = authenticationService;
        }


        // http post register
        public async Task<ResultRegister> RegisterUserAsync(RegisterModel request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.AccountEndpoints.Register, request);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return new ResultRegister { Successed = true };
            }
            else
            {
                var errors = JsonSerializer.Deserialize<List<ResultRegister>>(responseContent);
                var errorMessage = string.Join(", ", errors.Select(e => e.description));
                return new ResultRegister { Successed = false, description = errorMessage };

            }

        }

        // http post login
        public async Task<ResultLogin> LoginUserAsync(LoginModel request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.AccountEndpoints.Login, request);
            var result = await response.Content.ReadFromJsonAsync<JsonElement>();

            if (!response.IsSuccessStatusCode)
            {
                return new ResultLogin
                {
                    succeeded = false,
                    message = result.GetProperty("message").GetString()
                };
            }

            if (result.TryGetProperty("token", out var tokenElement) &&
                result.TryGetProperty("userId", out var userIdElement) &&
                result.TryGetProperty("roles", out var rolesElement) &&
                result.TryGetProperty("hotelIds", out var hotelIdsElement))
            {
                var token = tokenElement.GetString();
                var userId = userIdElement.GetString();
                var roles = new List<RoleInformation>();
                var hotelIds = new List<int>();


                if (rolesElement.ValueKind == JsonValueKind.Array)
                {
                    foreach (var roleItem in rolesElement.EnumerateArray())
                    {
                        int? hotelId = null;

                        if (roleItem.TryGetProperty("hotelId", out var hotelIdElement) &&
                            hotelIdElement.ValueKind == JsonValueKind.Number)
                        {
                            hotelId = hotelIdElement.GetInt32();
                        }

                        var roleName = roleItem.TryGetProperty("roleName", out var roleNameElement)
                            ? roleNameElement.GetString()
                            : null;

                        if (!string.IsNullOrEmpty(roleName))
                        {
                            roles.Add(new RoleInformation
                            {
                                HotelId = hotelId,
                                RoleName = roleName
                            });
                        }
                    }
                }


                if (hotelIdsElement.ValueKind == JsonValueKind.Array)
                {
                    hotelIds = hotelIdsElement.EnumerateArray()
                        .Where(id => id.ValueKind == JsonValueKind.Number)
                        .Select(id => id.GetInt32())
                        .ToList();
                }

                if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(userId))
                {
                    await _authenticationService.SaveTokenAsync(token);
                    await _authenticationService.SaveUserIdAsync(userId);
                    return new ResultLogin
                    {
                        succeeded = true,
                        token = token,
                        userId = userId,
                        roles = roles,
                        hotelIds = hotelIds
                    };
                }
            }

            return new ResultLogin
            {
                succeeded = false,
                message = "Token not found or response is invalid."
            };
        }

        // http post forgot password
        public async Task<ResultForgotPassword> ForgotPasswordUserAsync(ForgotPasswordModel request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.AccountEndpoints.ForgotPassword, request);
            var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();

            if (response.IsSuccessStatusCode)
            {
                return new ResultForgotPassword
                {
                    successed = true,
                };
            }
            return new ResultForgotPassword
            {
                successed = false,
                message = result?.GetValueOrDefault("message")
            };
        }

        //http post reset password
        public async Task<ResultResetPassword> ResetPasswordUserAsync(ResetPasswordModel request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.AccountEndpoints.ResetPassword, request);
            var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();

            if (response.IsSuccessStatusCode)
            {
                return new ResultResetPassword
                {
                    successed = true,
                };
            }
            return new ResultResetPassword
            {
                successed = false,
                message = result?.GetValueOrDefault("message")
            };
        }

        // change password
        public async Task<ResultChangePassword> ChangePasswordUserAsync(ChangePasswordModel request)
        {
            await _authenticationService.SetAuthorizationHeaderAsync();
            var response = await _httpClient.PostAsJsonAsync(Routes.AccountEndpoints.ChangePassword, request);
            var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();

            if (response.IsSuccessStatusCode)
            {
                return new ResultChangePassword
                {
                    successed = true,
                };
            }
            return new ResultChangePassword
            {
                successed = false,
                message = result?.GetValueOrDefault("message")
            };
        }

        //http post logout account
        public async Task<ResultLogout> LogoutAsync()
        {
            try
            {
                await _authenticationService.RemoveTokenAsync();
                await _authenticationService.RemoveHotelIdAdminAccessAsync();
                await _authenticationService.RemoveUserIdAsync();
                await _authenticationService.RemoveHotelNameAsync();
                await _authenticationService.RemoveRoleNameAsync();
                return new ResultLogout
                {
                    successed = true
                };
            }
            catch (Exception ex)
            {
                return new ResultLogout
                {
                    successed = false,
                    message = ex.Message
                };
            }
        }

        // get information user by accessToken
        public async Task<ResultInfoUser> GetInfoUser()
        {
            try
            {
                var accessToken = await _authenticationService.GetTokenAsync();
                var response = await _httpClient.PostAsJsonAsync(Routes.AccountEndpoints.GetInfoUserByAccessToken, accessToken);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ResultInfoUser>();
                    return result ?? new ResultInfoUser();
                }
                else
                {
                    Console.WriteLine($"Lỗi: {response.StatusCode}");
                    return new ResultInfoUser();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResultInfoUser();
            }
        }

    }
}
