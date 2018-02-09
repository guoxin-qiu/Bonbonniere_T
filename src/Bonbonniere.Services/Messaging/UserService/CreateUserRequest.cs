using Bonbonniere.Services.ViewModels;

namespace Bonbonniere.Services.Messaging.UserService
{
    public class CreateUserRequest : RequestMessage
    {
        public UserView User { get; set; }
    }
}
