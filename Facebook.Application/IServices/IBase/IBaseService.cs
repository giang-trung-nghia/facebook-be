using Facebook.Application.Dtos.Base;
using Facebook.Domain.Entities.Base;
using Facebook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.IServices.IBase
{
    public interface IBaseService<TEntityDto, TCreateDto, TUpdateDto>
    {
        Task<List<TEntityDto>> GetAllAsync();

        Task<TEntityDto> GetAsync(Guid id);

        Task<PagingResponse<TEntityDto>> PagingAsync(
            int pageNumber, 
            int pageSize,
            SortOption? sort,
            string? sortBy,
            string? searchKey,
        string? searchBy);

        Task<TEntityDto> InsertAsync(TCreateDto entity);

        Task<TEntityDto> UpdateAsync(Guid id, TUpdateDto entity);

        Task<int> DeleteAsync(Guid id);
    }
}
