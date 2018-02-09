namespace Bonbonniere.Services.Messaging.UserService
{
    public class GetUsersRequest : RequestMessage
    {
        public string SearchText { get; set; }
        public int? PageIndex { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
        public string SortCol { get; set; }
        public string SortOrder { get; set; }
    }
}
