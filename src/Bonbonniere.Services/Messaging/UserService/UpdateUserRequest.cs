using Bonbonniere.Core.Enums;

namespace Bonbonniere.Services.Messaging.UserService
{
    public class UpdateUserRequest : RequestMessage
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
    }
}
