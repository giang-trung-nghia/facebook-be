using Facebook.Application.IServices.IBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Facebook.API.Controllers.Base
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<TEntityDto> : ControllerBase
    {
        protected IBaseService<TEntityDto> BaseService;

        public BaseController(IBaseService<TEntityDto> baseService)
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
        [Route("Filter")]
        public async Task<dynamic> PagingAsync([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string? searchKey)
        {
            var result = await BaseService.PagingAsync(pageNumber, pageSize, searchKey);

            return result;
        }
    }
}
