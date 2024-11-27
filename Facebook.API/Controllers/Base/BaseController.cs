using Facebook.Application.IServices.IBase;
using Facebook.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Facebook.API.Controllers.Base
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<TEntityDto, TInsertDto, TUpdateDto> : ControllerBase
    {
        protected IBaseService<TEntityDto, TInsertDto, TUpdateDto> BaseService;

        public BaseController(IBaseService<TEntityDto, TInsertDto, TUpdateDto> baseService)
        {
            BaseService = baseService;
        }

        #region GetAll
        [HttpGet]
        [Authorize]
        public async Task<List<TEntityDto>> GetAllAsync()
        {
            var result = await BaseService.GetAllAsync();

            return result;
        }
        #endregion

        #region Get by id
        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<TEntityDto> GetAsync(Guid id)
        {
            var result = await BaseService.GetAsync(id);

            return result;

        }
        #endregion

        [HttpGet]
        [Route("Paging")]
        [Authorize]
        public async Task<dynamic> PagingAsync([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] SortOption? sort, [FromQuery] string? sortBy, [FromQuery] string? searchKey, [FromQuery] string? searchBy)
        {
            var result = await BaseService.PagingAsync(pageNumber, pageSize, sort, sortBy, searchKey, searchBy);

            return result;
        }

        [HttpPost]
        [Authorize]
        public async Task<dynamic> InsertAsync(TInsertDto insertDto)
        {
            var result = await BaseService.InsertAsync(insertDto);

            return result;
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<dynamic> UpdateAsync(Guid id, TUpdateDto updateDto)
        {

            var result = await BaseService.UpdateAsync(id, updateDto);

            return result;
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<int> DeleteAsync(Guid id)
        {
            var result = await BaseService.DeleteAsync(id);

            return result;
        }
    }
}
