using Bonbonniere.Core.Enums;
using Bonbonniere.Core.Models.ThirdParty;
using Bonbonniere.Services.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Bonbonniere.Services.Mapping
{
    public static class UserMapper
    {
        // TODO: use AutoMapper
        public static UserView ConvertToUserView(this User user)
        {
            if(user == default(User))
            {
                return default(UserView);
            }

            var userView = new UserView
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserProfile?.UserName,
                Address = user.UserProfile?.Address,
                Gender = user.UserProfile?.Gender ?? Gender.Male
            };

            return userView;
        }

        public static IEnumerable<UserView> ConvertToUsersView(this IEnumerable<User> users)
        {
            return users.Select(user => user.ConvertToUserView()).ToList();
        }
    }
}
