namespace CrmLeanXUi.ApiService.Authentication
{
    public interface IAuthenticationService
    {
        Task SaveTokenAsync(string token);
        Task<string> GetTokenAsync();
        Task RemoveTokenAsync();
        Task SetAuthorizationHeaderAsync();
        Task SaveDarkModeAsync(bool isDarkMode);
        Task<bool> GetDarkModeAsync();
        Task SaveHotelIdAdminAccessAsync(string hotelId);
        Task<string?> GetHotelIdAdminAccessAsync();
        Task RemoveHotelIdAdminAccessAsync();
        Task SaveUserIdAsync(string userId);
        Task<string?> GetUserIdAsync();
        Task RemoveUserIdAsync();
        Task SaveHotelNameAsync(string hotelName);
        Task<string?> GetHotelNameAsync();
        Task RemoveHotelNameAsync();
        Task SaveRoleAsync(string roleName);
        Task<string?> GetRoleNameAsync();
        Task RemoveRoleNameAsync();
    }
}
