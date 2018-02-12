using Bonbonniere.Services.Interfaces;
using Bonbonniere.Services.Messaging.UserService;
using Bonbonniere.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Bonbonniere.WebApis.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var response = _userService.GetUser(new GetUserRequest { UserId = id });
            if (response.Success)
            {
                return Ok(response.User);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult Get(GetUsersRequest request)
        {
            return Ok(_userService.GetUsers(request).Users);
        }

        [HttpGet("[action]")]
        public IEnumerable<UserView> GetAll()
        {
            return _userService.GetUsers(new GetUsersRequest { PageSize = 100 }).Users;
        }

        [HttpPost]
        public IActionResult Post(CreateUserRequest user)
        {
            var response = _userService.CreateUser(user);
            if (response.Success)
            {
                return Ok(new { user = response.User });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UserView user)
        {
            var response = _userService.UpdateUser(new UpdateUserRequest
            {
                Address = user.Address,
                Gender = user.Gender,
                UserId = id,
                UserName = user.UserName
            });

            if (response.Success)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = _userService.RemoveUser(new RemoveUserRequest { UserId = id });
            if (response.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
