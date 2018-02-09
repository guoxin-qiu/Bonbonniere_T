namespace Bonbonniere.Services.Messaging.AccountService
{
    public class CheckLoginRequest: RequestMessage
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
