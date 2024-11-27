using Facebook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domain.IRepositories.IBase
{
    public interface IBaseRepository<TEntity>
    {
        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(Guid id);

        Task<List<TEntity>> PagingAsync(
            int pageNumber,
            int pageSize,
            SortOption? sort,
            string? sortBy,
            string? searchKey,
            string? searchBy);

        Task<TEntity> InsertAsync(TEntity entity);

        Task<TEntity> UpdateAsync(Guid id, TEntity entity);

        Task<int> DeleteAsync(Guid id);


    }
}
