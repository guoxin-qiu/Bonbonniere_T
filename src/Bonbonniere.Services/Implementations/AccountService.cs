using Bonbonniere.Infrastructure.EFData;
using Bonbonniere.Services.Interfaces;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Bonbonniere.Services.Messaging.AccountService;
using Bonbonniere.Services.Mapping;

namespace Bonbonniere.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly ThirdPartyContext _context;

        public AccountService(ThirdPartyContext context)
        {
            _context = context;
        }

        public CheckLoginResponse CheckLogin(CheckLoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new ArgumentNullException("email");
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                throw new ArgumentNullException("password");
            }

            var user = _context.Users.FirstOrDefault(t => t.Email == request.Email && t.Password == request.Password);

            return new CheckLoginResponse
            {
                Success = user != null,
                Message = user == null ? "Invalid login attempt." : string.Empty
            };
        }

        public GetAccountInfoResponse GetAccountInfo(GetAccountInfoRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new ArgumentNullException("email");
            }

            var user = _context.Users.Include(t => t.UserProfile).FirstOrDefault(t => t.Email == request.Email);

            if (user == null)
            {
                return new GetAccountInfoResponse { Success = false, Message = "Invalid user id." };
            }

            return new GetAccountInfoResponse { User = user.ConvertToUserView() };
        }
    }
}
