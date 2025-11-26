namespace CrmLeanXUi.ViewModel.ApiResponse.ManagementUser
{
    public class ResultInfoUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string AvatarUrl { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public string Nationality { get; set; }
        public string IdentityNumber { get; set; }
        //public int UserType { get; set; }
        public List<string> Roles { get; set; }
    }
}
