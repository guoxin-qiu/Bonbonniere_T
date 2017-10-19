using Bonbonniere.Core.Enums;

namespace Bonbonniere.Core.Models.ThirdParty
{
    public class UserProfile : EntityBase
    {
        public string UserName { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }

        public User User { get; set; }
    }
}
