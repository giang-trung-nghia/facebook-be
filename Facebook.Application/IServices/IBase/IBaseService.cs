using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.IServices.IBase
{
    public interface IBaseService<TEntityDto>
    {
        Task<List<TEntityDto>> GetAllAsync();

        Task<TEntityDto>? GetAsync(Guid id);

        Task<TEntityDto>? FindAsync(Guid id);

        Task<List<TEntityDto>>? PagingAsync(int pageNumber, int pageSize, string? searchKey);

    }
}
