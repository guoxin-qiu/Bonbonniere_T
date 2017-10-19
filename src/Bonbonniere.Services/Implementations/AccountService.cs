using Bonbonniere.Core.Interfaces;
using Bonbonniere.Infrastructure.EFData;
using System;
using System.Linq;

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
    }
}
