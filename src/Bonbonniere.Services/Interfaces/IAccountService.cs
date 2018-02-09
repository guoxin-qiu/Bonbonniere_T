using Bonbonniere.Services.Messaging.AccountService;

namespace Bonbonniere.Services.Interfaces
{
    public interface IAccountService
    {
        CheckLoginResponse CheckLogin(CheckLoginRequest request);
        GetAccountInfoResponse GetAccountInfo(GetAccountInfoRequest request);
    }
}
