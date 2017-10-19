using System;

namespace Bonbonniere.Core.Models.ThirdParty
{
    public class User : EntityBase, IAggregateRoot
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? LastLoginTime { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}
