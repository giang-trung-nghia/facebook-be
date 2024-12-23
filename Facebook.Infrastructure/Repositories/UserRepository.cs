using Facebook.Domain.Entities;
using Facebook.Domain.Enums;
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

            var relationshipOffers = user.RelationsReceived
                .Where(r => r.Status == RelationshipStatus.Pending)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToList();

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
