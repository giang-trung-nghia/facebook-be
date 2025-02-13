using Facebook.Domain.Entities;
using Facebook.Domain.Enums;
using Facebook.Domain.IRepositories.IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domain.IRepositories.Users
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        Task<int> GetTotalFriends(Guid userId);

        Task<List<UserEntity>> GetFriends(Guid id, int pageNumber, int pageSize);

        Task<List<UserEntity>> GetPeopleMaybeYouKnow(Guid id, int pageNumber, int pageSize);

        Task<List<UserEntity>> GetAddFriendOffers(Guid id, int pageNumber, int pageSize);
    }
}
