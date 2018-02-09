using Bonbonniere.Core.Enums;
using System;

namespace Bonbonniere.Core.Models.ThirdParty
{
    public class User : EntityBase, IAggregateRoot
    {
        public User()
        {

        }

        public User(string email, string password, string username, string address, Gender gender)
        {
            Email = email;
            Password = password;
            UserProfile = new UserProfile
            {
                UserName = username,
                Address = address,
                Gender = gender,
                CreatedTime = ModifiedTime = DateTime.Now // TODO: DateTime.Now
            };
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? LastLoginTime { get; set; }

        public UserProfile UserProfile { get; set; }

        public void UpdateInfo(string username, Gender gender, string address)
        {
            if(UserProfile == null)
            {
                UserProfile = new UserProfile
                {
                    UserName = username,
                    Address = address,
                    Gender = gender,
                    CreatedTime = ModifiedTime = DateTime.Now // TODO: DateTime.Now
                };
            }
            else
            {
                UserProfile.UserName = username;
                UserProfile.Gender = gender;
                UserProfile.Address = address;
                UserProfile.ModifiedTime = DateTime.Now;
            }
        }
    }
}
