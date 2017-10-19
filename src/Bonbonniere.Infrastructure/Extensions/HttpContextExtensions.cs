namespace Bonbonniere.Infrastructure.Extensions
{
    public static class HttpContextExtensions
    {
        public static void SetAuthentication( string email, bool isPersistent = false)
        {
            //https://github.com/aspnet/Security/issues/1310
        }

        public static void RemoveAuthentication()
        {

        }
    }
}
