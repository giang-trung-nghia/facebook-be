using Facebook.API.Controllers.Base;
using Facebook.Application.Dtos.Users;
using Facebook.Application.IServices.IUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Facebook.API.Controllers
{
    public class UserController : BaseController<UserDto>
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }

        //[HttpGet]
        //[Authorize]
        //[Route("GetAll")]
        //public async Task<List<UserDto>> GetAllAsync()
        //{

        //    var result = await _userService.GetAllAsync();

        //    return result;

        //}
    }
}
