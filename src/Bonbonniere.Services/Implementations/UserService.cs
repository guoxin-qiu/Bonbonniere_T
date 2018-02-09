using Bonbonniere.Core.Models.ThirdParty;
using Bonbonniere.Infrastructure.EFData;
using Bonbonniere.Infrastructure.Utilities.Cryptographer;
using Bonbonniere.Services.Interfaces;
using Bonbonniere.Services.Mapping;
using Bonbonniere.Services.Messaging.UserService;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bonbonniere.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ThirdPartyContext _context;

        public UserService(ThirdPartyContext context)
        {
            _context = context;
        }

        public CreateUserResponse CreateUser(CreateUserRequest request)
        {
            var newUser = new User(
                    request.User.Email,
                    MD5.Encrypt("admin"),
                    request.User.UserName,
                    request.User.Address,
                    request.User.Gender);

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return new CreateUserResponse { User = newUser.ConvertToUserView() };
        }

        public GetUserResponse GetUser(GetUserRequest request)
        {
            var user = _context.Users.Include(t => t.UserProfile).FirstOrDefault(u => u.Id == request.UserId);
            return new GetUserResponse
            {
                User = user.ConvertToUserView()
            };
        }

        public GetUsersResponse GetUsers(GetUsersRequest request)
        {
            var searchText = request.SearchText;
            var pageIndex = request.PageIndex ?? 1;
            var pageSize = request.PageSize ?? 9;
            var sortCol = request.SortCol;
            var sortOrder = request.SortOrder;

            var users = _context.Users.Include(t => t.UserProfile).Where(t => string.IsNullOrWhiteSpace(searchText)
            || t.Email.ToLower().Contains(searchText)
            || t.UserProfile.UserName.ToLower().Contains(searchText)
            || t.UserProfile.Address.ToLower().Contains(searchText));

            var totalPageCount = (users.Count() + pageSize - 1) / pageSize;

            if (sortOrder == "-") // TODO: need dynamic
            {
                if (sortCol == "email") users = users.OrderByDescending(t => t.Email);
                if (sortCol == "username") users = users.OrderByDescending(t => t.UserProfile.UserName);
                if (sortCol == "address") users = users.OrderByDescending(t => t.UserProfile.Address);
            }
            else
            {
                if (sortCol == "email") users = users.OrderBy(t => t.Email);
                if (sortCol == "username") users = users.OrderBy(t => t.UserProfile.UserName);
                if (sortCol == "address") users = users.OrderBy(t => t.UserProfile.Address);
            }

            users = users.Skip(pageSize * (pageIndex - 1)).Take(pageSize);

            return new GetUsersResponse
            {
                TotalPageCount = totalPageCount,
                Users = users.ConvertToUsersView()
            };
        }

        public RemoveUserResponse RemoveUser(RemoveUserRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == request.UserId);
            if(user == null)
            {
                return new RemoveUserResponse {
                    Success = false,
                    Message = "Invalid user id."
                };
            }
            _context.Remove(user);

            return new RemoveUserResponse();
        }

        public UpdateUserResponse UpdateUser(UpdateUserRequest request)
        {
            var user = _context.Users.Include(t => t.UserProfile).FirstOrDefault(u => u.Id == request.UserId);
            if(user == null)
            {
                return new UpdateUserResponse {
                    Success = false,
                    Message = "Invalid user id."
                };
            }

            user.UpdateInfo(request.UserName, request.Gender, request.Address);

            _context.Update(user);

            return new UpdateUserResponse { User = user.ConvertToUserView() };
        }
    }
}
