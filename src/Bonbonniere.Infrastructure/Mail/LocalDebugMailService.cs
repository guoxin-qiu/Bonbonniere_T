using Bonbonniere.Infrastructure.Environment;
using System;
using System.Diagnostics;

namespace Bonbonniere.Infrastructure.Mail
{
    public class LocalDebugMailService : IMailService
    {
        private readonly string fromAddress;
        public LocalDebugMailService(EnvSettings envSettings)
        {
            fromAddress = envSettings.MailFromAddress;
        }

        public void Send(string to, string subject, string body)
        {
            Debug.WriteLine($"from: {fromAddress}, to: {to}, subject: {subject}, body: {body}");
        }
    }
}
