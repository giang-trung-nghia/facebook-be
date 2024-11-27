using Facebook.API.Controllers.Base;
using Facebook.Application.Dtos.Users;
using Facebook.Application.IServices.IUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Facebook.API.Controllers
{
    public class UserController : BaseController<UserDto, UserCreateDto, UserUpdateDto>
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }
    }
}
