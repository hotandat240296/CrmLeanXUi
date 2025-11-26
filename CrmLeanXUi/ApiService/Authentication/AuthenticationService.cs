using Microsoft.JSInterop;

namespace CrmLeanXUi.ApiService.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly HttpClient _httpClient;

        public AuthenticationService(IJSRuntime jsRuntime, HttpClient httpClient)
        {
            _jsRuntime = jsRuntime;
            _httpClient = httpClient;
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------------
        public async Task SaveTokenAsync(string token)
        {
            await _jsRuntime.InvokeVoidAsync("cookieHelper.setCookie", "access_token", token, 1);
        }

        public async Task<string> GetTokenAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("cookieHelper.getCookie", "access_token");
        }

        public async Task RemoveTokenAsync()
        {
            await _jsRuntime.InvokeVoidAsync("cookieHelper.deleteCookie", "access_token");
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------------
        // authori token
        public async Task SetAuthorizationHeaderAsync()
        {
            var access_token = await GetTokenAsync();
            if (!string.IsNullOrEmpty(access_token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", access_token);
            }
        }


        // ------------------------------------------------------------------------------------------------------------------------------------------------------

        // save value dark mode
        public async Task SaveDarkModeAsync(bool isDarkMode)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "darkMode", isDarkMode.ToString().ToLower());
        }

        // get value dark mode
        public async Task<bool> GetDarkModeAsync()
        {
            var darkModeValue = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "darkMode");
            return !string.IsNullOrEmpty(darkModeValue) && darkModeValue == "true";
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------------

        // save hotelId when admin access to hotel
        public async Task SaveHotelIdAdminAccessAsync(string hotelId)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "HotelIdAdminAccess", hotelId);
        }

        // get hotelId when admin access to hotel
        public async Task<string?> GetHotelIdAdminAccessAsync()
        {
            var hotelIdValue = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "HotelIdAdminAccess");
            return string.IsNullOrWhiteSpace(hotelIdValue) ? null : hotelIdValue;
        }

        // delete hotelid
        public async Task RemoveHotelIdAdminAccessAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "HotelIdAdminAccess");
        }


        // ------------------------------------------------------------------------------------------------------------------------------------------------------
        // Save userId vào Cookie
        public async Task SaveUserIdAsync(string userId)
        {
            await _jsRuntime.InvokeVoidAsync("eval", $"document.cookie = 'UserId={userId}; path=/;'");
        }

        // get userId từ Cookie
        public async Task<string?> GetUserIdAsync()
        {
            var cookies = await _jsRuntime.InvokeAsync<string>("eval", "document.cookie");
            var cookieArray = cookies.Split(';');
            foreach (var cookie in cookieArray)
            {
                var trimmed = cookie.Trim();
                if (trimmed.StartsWith("UserId="))
                {
                    return trimmed.Substring("UserId=".Length);
                }
            }
            return null;
        }

        // delete userId - Cookie
        public async Task RemoveUserIdAsync()
        {
            await _jsRuntime.InvokeVoidAsync("eval", "document.cookie = 'UserId=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;'");
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------------

        // Save hotelName - localStorage
        public async Task SaveHotelNameAsync(string hotelName)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "HotelName", hotelName);
        }

        // get hotelName from localStorage
        public async Task<string?> GetHotelNameAsync()
        {
            var hotelNameValue = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "HotelName");
            return string.IsNullOrWhiteSpace(hotelNameValue) ? null : hotelNameValue;
        }

        // delete hotelName - localStorage
        public async Task RemoveHotelNameAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "HotelName");
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------------
        public async Task SaveRoleAsync(string roleName)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "RoleName", roleName);
        }

        // get hotelName from localStorage
        public async Task<string?> GetRoleNameAsync()
        {
            var hotelNameValue = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "RoleName");
            return string.IsNullOrWhiteSpace(hotelNameValue) ? null : hotelNameValue;
        }

        // delete hotelName - localStorage
        public async Task RemoveRoleNameAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "RoleName");
        }
    }
}
