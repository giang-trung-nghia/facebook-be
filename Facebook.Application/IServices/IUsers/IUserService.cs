using Facebook.Application.Dtos.Base;
using Facebook.Application.Dtos.Users;
using Facebook.Application.IServices.IBase;
using Facebook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.IServices.IUsers
{
    public interface IUserService : IBaseService<UserDto, UserCreateDto, UserUpdateDto>
    {
        Task<PagingResponse<UserRelationshipDto>> GetFriends(Guid id, int pageNumber, int pageSize);
        Task<PagingResponse<UserDto>> GetPeopleMaybeYouKnow(Guid id, int pageNumber, int pageSize);
        Task<PagingResponse<UserRelationshipDto>> GetAddFriendOffers(Guid id, int pageNumber, int pageSize);

    }
}
