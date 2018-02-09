using Bonbonniere.Infrastructure.Utilities.Cryptographer;

namespace Bonbonniere.Infrastructure.Utilities.Helpers
{
    public static class TokenHelper
    {
        public static string GetToken(string username, string password)
        {
            return MD5.Encrypt($"{username}:{password}");
        }
    }
}
