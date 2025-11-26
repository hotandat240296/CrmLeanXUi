namespace CrmLeanXUi.ViewModel.ApiResponse.ManagementUser
{
    public class ResultGetUserPagination
    {
        public List<ManagementUserModel> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
    }
}
