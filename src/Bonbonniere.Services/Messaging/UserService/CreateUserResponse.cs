using Bonbonniere.Services.ViewModels;

namespace Bonbonniere.Services.Messaging.UserService
{
    public class CreateUserResponse : ResponseMessage
    {
        public UserView User { get; set; }
    }
}
