using Bonbonniere.Core.Enums;

namespace Bonbonniere.Services.Dtos.IAccountService
{
    public class AccountInfoDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public Gender? Gender { get; set; }
        public string Address { get; set; }
    }
}
