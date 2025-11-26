namespace CrmLeanXUi.ViewModel.ApiResponse.ManagementUser
{
    public class ResultInfoAllCustomer
    {
        public bool Successed { get; set; }
        public List<InfoCustomerResponse> Data { get; set; }
    }
    public class InfoCustomerResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
    }
}
