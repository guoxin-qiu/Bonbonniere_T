using Bonbonniere.Services.ViewModels;

namespace Bonbonniere.Services.Messaging.UserService
{
    public class GetUserResponse : ResponseMessage
    {
        public UserView User { get; set; }
    }
}
