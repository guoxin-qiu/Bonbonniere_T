using Bonbonniere.Infrastructure.EFData;
using Bonbonniere.Services.Interfaces;
using System;
using System.Linq;
using Bonbonniere.Services.Dtos.IAccountService;
using Microsoft.EntityFrameworkCore;

namespace Bonbonniere.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly ThirdPartyContext _context;

        public AccountService(ThirdPartyContext context)
        {
            _context = context;
        }

        public bool CheckSignIn(string email, string password)
        {
            if(string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException("email");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("password");
            }

            var user = _context.Users.FirstOrDefault(t => t.Email == email && t.Password == password);

            return user != null;
        }

        public AccountInfoDto GetAccountInfo(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException("email");
            }

            var user = _context.Users.Include(t => t.UserProfile).FirstOrDefault(t => t.Email == email);
            if(user == null)
            {
                return null;
            }
            var accountInfo = new AccountInfoDto
            {
                Email = user.Email,
                Address = user.UserProfile?.Address ?? string.Empty,
                Gender = user.UserProfile?.Gender,
                UserName = user.UserProfile?.UserName ?? Guid.NewGuid().ToString()
            };

            return accountInfo;
        }
    }
}
