using Bonbonniere.Services.ViewModels;

namespace Bonbonniere.Services.Messaging.UserService
{
    public class UpdateUserResponse : ResponseMessage
    {
        public UserView User { get; set; }
    }
}
