using Bonbonniere.Services.Dtos.IAccountService;

namespace Bonbonniere.Services.Interfaces
{
    public interface IAccountService
    {
        bool CheckSignIn(string email, string password);
        AccountInfoDto GetAccountInfo(string email);
    }
}
