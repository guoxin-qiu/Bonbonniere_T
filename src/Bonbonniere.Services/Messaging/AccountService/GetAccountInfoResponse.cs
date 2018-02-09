using Bonbonniere.Services.ViewModels;

namespace Bonbonniere.Services.Messaging.AccountService
{
    public class GetAccountInfoResponse : ResponseMessage
    {
        public UserView User { get; set; }
    }
}
