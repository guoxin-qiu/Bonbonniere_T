namespace Bonbonniere.Services.Messaging
{
    public class ResponseMessage
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
