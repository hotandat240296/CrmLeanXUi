namespace CrmLeanXUi.ViewModel.ApiResponse.ManagementUser
{
    public class ManagementUserModel
    {
        public string Id { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
        public string FullName { get; set; } = "";
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; } = "";
        public string Gender { get; set; } = "";
        public string AvatarUrl { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public string Nationality { get; set; } = "";
        public string IdentityNumber { get; set; } = "";
        public string UserType { get; set; } = "";
        public string HotelName { get; set; } = "";
        public long? HotelId { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
