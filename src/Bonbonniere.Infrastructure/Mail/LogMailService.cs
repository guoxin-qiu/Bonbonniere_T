using Bonbonniere.Infrastructure.Environment;
using Microsoft.Extensions.Logging;

namespace Bonbonniere.Infrastructure.Mail
{
    public class LogMailService : IMailService
    {
        private readonly string _fromAddress;
        private readonly ILogger _logger;
        public LogMailService(EnvSettings envSettings, ILogger<LogMailService> logger)
        {
            _fromAddress = envSettings.MailFromAddress;
            _logger = logger;
        }

        public void Send(string to, string subject, string body)
        {
            _logger.LogInformation($"from: {_fromAddress}, to: {to}, subject: {subject}, body: {body}");
        }
    }
}
