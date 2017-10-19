using System;
using System.Collections.Generic;
using System.Text;

namespace Bonbonniere.Infrastructure.Environment
{
    public class EnvSettings
    {
        public string BonbonniereConnection { get; set; }
        public string ThirdPartyConnection { get; set; }

        public string MailFromAddress { get; set; } 
    }
}
