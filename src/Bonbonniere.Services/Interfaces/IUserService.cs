using Bonbonniere.Services.Messaging.UserService;

namespace Bonbonniere.Services.Interfaces
{
    public interface IUserService
    {
        GetUserResponse GetUser(GetUserRequest request);
        GetUsersResponse GetUsers(GetUsersRequest request);
        CreateUserResponse CreateUser(CreateUserRequest request);
        UpdateUserResponse UpdateUser(UpdateUserRequest request);
        RemoveUserResponse RemoveUser(RemoveUserRequest request);
    }
}
