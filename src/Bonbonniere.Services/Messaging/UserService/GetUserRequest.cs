namespace Bonbonniere.Services.Messaging.UserService
{
    public class GetUserRequest : RequestMessage
    {
        public int UserId { get; set; }
    }
}
