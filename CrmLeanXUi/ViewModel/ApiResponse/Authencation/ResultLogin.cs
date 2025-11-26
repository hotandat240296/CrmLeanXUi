namespace CrmLeanXUi.ViewModel.ApiResponse.Authencation
{
    public class ResultLogin
    {
        public bool succeeded { get; set; }
        public string? token { get; set; }
        public string? userId { get; set; }
        public string? message { get; set; }
        public List<RoleInformation>? roles { get; set; }
        public List<int>? hotelIds { get; set; }
    }

    public class RoleInformation
    {
        public int? HotelId { get; set; }
        public string? RoleName { get; set; }
    }
}
