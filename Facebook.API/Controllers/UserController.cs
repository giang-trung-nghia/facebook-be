using Facebook.API.Controllers.Base;
using Facebook.Application.Dtos.Base;
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

        [HttpGet("strange-people")]
        [Authorize]
        public async Task<PagingResponse<UserDto>> GetPeopleMaybeYouKnow([FromQuery] Guid id, [FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 20)
        {
            var result = await _userService.GetPeopleMaybeYouKnow(id, pageNumber, pageSize);
            return result;
        }

        [HttpGet("add-friend-offers")]
        [Authorize]
        public async Task<PagingResponse<UserRelationshipDto>> GetAddFriendOffers([FromQuery] Guid id, [FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 20)
        {
            var result = await _userService.GetAddFriendOffers(id, pageNumber, pageSize);
            return result;
        }
    }
}
