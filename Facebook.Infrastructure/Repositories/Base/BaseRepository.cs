using Facebook.Domain.Entities.Base;
using Facebook.Domain.Enums;
using Facebook.Domain.Exceptions;
using Facebook.Domain.IRepositories.IBase;
using Facebook.Infrastructure.Migrations.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Facebook.Infrastructure.Repositories.Base
{
    /**
     * base repository used to interact with the database, we will not edit or update data in this layer
     */
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        // Get all records
        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // Get a single record by ID
        public async Task<TEntity> GetAsync(Guid id)
        {
            var entity = await _dbSet.SingleOrDefaultAsync(e => e.Id == id);
            if (entity == null)
            {
                throw new NotFoundException($"Entity with ID {id} not found.", $"Entity with ID {id} not found.");
            }
            return entity;
        }

        // Paging with optional sorting and search
        public async Task<List<TEntity>> PagingAsync(
            int pageNumber,
            int pageSize,
            SortOption? sort,
            string? sortBy,
            string? searchKey,
            string? searchBy)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                throw new ConflictException("Page number and size must be greater than 0.");
            }

            var query = _dbSet.AsQueryable();

            // Apply dynamic search
            if (!string.IsNullOrEmpty(searchKey) && !string.IsNullOrEmpty(searchBy))
            {
                var property = typeof(TEntity).GetProperty(searchBy);
                if (property == null)
                {
                    throw new ConflictException($"Property '{searchBy}' does not exist on type '{typeof(TEntity).Name}'.");
                }

                query = query.Where(e =>
                    EF.Functions.Like(EF.Property<string>(e, searchBy), $"%{searchKey}%"));
            }

            // Apply dynamic sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                var property = typeof(TEntity).GetProperty(sortBy);
                if (property == null)
                {
                    throw new ConflictException($"Property '{sortBy}' does not exist on type '{typeof(TEntity).Name}'.");
                }

                query = sort switch
                {
                    SortOption.ASC => query.OrderBy(e => EF.Property<object>(e, sortBy)),
                    SortOption.DESC => query.OrderByDescending(e => EF.Property<object>(e, sortBy)),
                    _ => query
                };
            }

            // Apply paging
            return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        // Insert a new entity
        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Update an existing entity
        public async Task<TEntity> UpdateAsync(Guid id, TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var existingEntity = await GetAsync(id); // Throws NotFoundException if not found
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            //_context.Entry(existingEntity).Property(e => e.Id).IsModified = false;
            await _context.SaveChangesAsync();
            return existingEntity;
        }

        // Delete an entity
        public async Task<int> DeleteAsync(Guid id)
        {
            var entity = await GetAsync(id); // Throws NotFoundException if not found
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return 1;
        }
    }
}
