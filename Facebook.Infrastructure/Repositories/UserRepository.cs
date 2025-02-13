using Facebook.Domain.Entities;
using Facebook.Domain.Entities.Base;
using Facebook.Domain.Enums;
using Facebook.Domain.Exceptions;
using Facebook.Domain.IRepositories.Users;
using Facebook.Infrastructure.Migrations.Contexts;
using Facebook.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Facebook.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<UserEntity> _dbUserSet;
        private readonly DbSet<RelationshipEntity> _dbRelationshipSet;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
            _dbUserSet = _context.Set<UserEntity>();
        }

        public async Task<int> GetTotalFriends(Guid userId)
        {
            var totalFriends = await GetAsync(userId);
            var ri = totalFriends.RelationsInitiated.Where(ur => ur.RelationshipType == RelationshipType.Friend && ur.Status == RelationshipStatus.Accepted).Count();
            var rr = totalFriends.RelationsReceived.Where(ur => ur.RelationshipType == RelationshipType.Friend && ur.Status == RelationshipStatus.Accepted).Count();

            return ri + rr;
        }

        public async Task<List<UserEntity>> GetFriends(Guid id, int pageNumber, int pageSize)
        {
            var user = await _dbUserSet
                .Include(u => u.RelationsInitiated)
                .Include(u => u.RelationsReceived)
                .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new NotFoundException($"User with ID {id} not found.", $"User with ID {id} not found.");
            }

            var rltInit = user.RelationsInitiated.Where(ur => ur.Status == RelationshipStatus.Accepted).ToList();
            var rltReceived = user.RelationsReceived.Where(ur => ur.Status == RelationshipStatus.Accepted).ToList();

            var users = new List<UserEntity>();

            foreach (var ur in rltInit)
            {
                var userInit = await _dbUserSet.FindAsync(ur.ToUserId);
                if (userInit != null)
                {
                    userInit.RelationsInitiated = new List<RelationshipEntity> { ur };
                    users.Add(userInit);
                }
            }

            foreach (var ur in rltReceived)
            {
                var userReceived = await _dbUserSet.FindAsync(ur.FromUserId);
                if (userReceived != null)
                {
                    userReceived.RelationsInitiated = new List<RelationshipEntity> { ur };
                    users.Add(userReceived);
                }
            }

            return users;
        }

        public async Task<List<UserEntity>> GetPeopleMaybeYouKnow(Guid id, int pageNumber, int pageSize)
        {
            var user = await _dbUserSet
                .Include(u => u.RelationsInitiated)
                .Include(u => u.RelationsReceived)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                throw new Exception("User not found");

            var relatedUserIds = user.RelationsInitiated
                .Where(r => r.Status == RelationshipStatus.Pending || // @todo: nghiệp vụ cần được xử lý bên phía service -> sai kiến trúc
                    r.Status == RelationshipStatus.Accepted ||
                    r.Status == RelationshipStatus.Blocked)
                .Select(r => r.ToUserId)
                .Union(user.RelationsReceived
                    .Where(r => r.Status == RelationshipStatus.Pending ||
                        r.Status == RelationshipStatus.Accepted ||
                        r.Status == RelationshipStatus.Blocked)
                    .Select(r => r.FromUserId))
                .ToHashSet();

            var listPeopleDontHaveRelation = await _dbUserSet
                .Where(u => u.Id != id && !relatedUserIds.Contains(u.Id))
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return listPeopleDontHaveRelation;

        }

        public async Task<List<UserEntity>> GetAddFriendOffers(Guid id, int pageNumber, int pageSize)
        {
            var user = await _dbUserSet
                .Include(u => u.RelationsReceived)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                throw new Exception("User not found");

            var result = await _dbUserSet
                .Where(u => u.RelationsInitiated.Any(r => r.ToUserId == id && r.Status == RelationshipStatus.Pending))
                .SelectMany(u => u.RelationsInitiated
                    .Where(r => r.ToUserId == id && r.Status == RelationshipStatus.Pending)
                    .Skip(pageSize * (pageNumber - 1))
                    .Take(pageSize)
                    .Select(r => new
                    {
                        User = r.FromUser,
                        Relationship = r
                    })
                )
                .ToListAsync();

            var userOffers = result
                .Select(r =>
                {
                    var user = r.User;
                    user.RelationsInitiated = new List<RelationshipEntity> { r.Relationship };
                    return user;
                })
                .ToList();

            return userOffers;
        }
    }
}
