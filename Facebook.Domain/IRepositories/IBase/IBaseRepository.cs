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

        Task<TEntity>? GetAsync(Guid id);

        Task<TEntity>? FindAsync(Guid id);

        Task<List<TEntity>?> PagingAsync(int pageNumber, int pageSize, string searchKey);

    }
}
