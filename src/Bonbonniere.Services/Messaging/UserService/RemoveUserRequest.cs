namespace Bonbonniere.Services.Messaging.UserService
{
    public class RemoveUserRequest : RequestMessage
    {
        public int UserId { get; set; }
    }
}
