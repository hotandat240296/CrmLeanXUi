namespace CrmLeanXUi.Routes
{
    public static class AccountEndpoints
    {
        public static string Register = "api/Account/register";
        public static string Login = "api/Account/login";
        public static string ForgotPassword = "api/Account/forgot-password";
        public static string ResetPassword = "api/Account/reset-password";
        public static string ChangePassword = "api/Account/change-password";
        public static string LogoutAccount = "api/Account/logout";
        public static string GetInfoUserByAccessToken = "/api/Account/get-user-token";
    }
}
