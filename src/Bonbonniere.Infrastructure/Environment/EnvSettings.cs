namespace Bonbonniere.Infrastructure.Environment
{
    public class EnvSettings
    {
        public string ProjectName { get; set; }

        public string BonbonniereConnection { get; set; }

        public string ThirdPartyConnection { get; set; }

        public string MailFromAddress { get; set; }

        public string ApiCookieAuthenticationSchema { get; set; }

        public string Origins { get; set; }
    }
}
