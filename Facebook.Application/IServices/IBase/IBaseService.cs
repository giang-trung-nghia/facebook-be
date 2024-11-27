using Facebook.Domain.Entities.Base;
using Facebook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.IServices.IBase
{
    public interface IBaseService<TEntityDto, TInsertDto, TUpdateDto>
    {
        Task<List<TEntityDto>> GetAllAsync();

        Task<TEntityDto> GetAsync(Guid id);

        Task<List<TEntityDto>> PagingAsync(
            int pageNumber, 
            int pageSize,
            SortOption? sort,
            string? sortBy,
            string? searchKey,
        string? searchBy);

        Task<TEntityDto> InsertAsync(TInsertDto entity);

        Task<TEntityDto> UpdateAsync(Guid id, TUpdateDto entity);

        Task<int> DeleteAsync(Guid id);
    }
}
