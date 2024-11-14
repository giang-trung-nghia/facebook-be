using Facebook.Domain.Entities.Base;
using Facebook.Domain.Exceptions;
using Facebook.Domain.IRepositories.IBase;
using Facebook.Infrastructure.Migrations.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Facebook.Infrastructure.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var result = await _context.Set<TEntity>().ToListAsync();

            return result.ToList();
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            var result = await _context.Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id);

            if (result == null)
            {
                throw new NotFoundException($"Not found user {id}", "Not Found User!!!");
            }
            return result;
        }

        public Task<List<TEntity>?> PagingAsync(int pageNumber, int pageSize, string searchKey)
        {
            throw new NotImplementedException();
        }
    }
}
