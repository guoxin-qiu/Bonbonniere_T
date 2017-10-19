namespace Bonbonniere.Core.Interfaces
{
    public interface IAccountService
    {
        bool CheckSignIn(string email, string password);
    }
}
