using Bonbonniere.Core.Enums;
using Bonbonniere.Core.Models.ThirdParty;
using System.Linq;

namespace Bonbonniere.Infrastructure.EFData
{
    public static class ThirdPartyContextInitializer
    {
        public static void Initialize(this ThirdPartyContext context)
        {
            context.Database.EnsureCreated();

            #region Initialize User
            if (!context.Set<User>().Any())
            {
                var users = new User[]
                {
                    new User
                    {
                        Email = "admin@admin.net",
                        Password = "123456",
                        IsActive = true,
                        UserProfile = new UserProfile
                        {
                            Address = "Dalian China",
                            Gender = Gender.Male,
                            UserName = "administrator"
                        }
                    }
                };
                context.AddRange(users);
                context.SaveChanges();
            }
            #endregion Initialize User
        }
    }
}
