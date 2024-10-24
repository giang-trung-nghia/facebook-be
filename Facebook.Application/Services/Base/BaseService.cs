using Facebook.Application.IServices.IBase;
using Facebook.Domain.IRepositories.IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.Services.Base
{
    public abstract class BaseService<TEntity, TEntityDto> : IBaseService<TEntityDto>
    {
        protected readonly IBaseRepository<TEntity> BaseRepository;

        protected BaseService(IBaseRepository<TEntity> baseRepository)
        {
            BaseRepository = baseRepository;
        }

        #region Get All
        public async Task<List<TEntityDto>> GetAllAsync()
        {
            var entities = await BaseRepository.GetAllAsync();

            var entityDtos = entities.Select(entity => MapEntityToEntityDto(entity)).ToList();

            return entityDtos;
        }
        #endregion

        #region FindAsync
        public async Task<TEntityDto>? FindAsync(Guid id)
        {
            var entity = await BaseRepository.FindAsync(id);

            var entityDto = MapEntityToEntityDto(entity);

            return entityDto;
        }
        #endregion

        #region GetAsync
        public async Task<TEntityDto>? GetAsync(Guid id)
        {
            var entity = await BaseRepository.GetAsync(id);

            var entityDto = MapEntityToEntityDto(entity);

            return entityDto;
        }
        #endregion

        public abstract TEntityDto MapEntityToEntityDto(TEntity entity);

        public async Task<List<TEntityDto>>? PagingAsync(int pageNumber, int pageSize, string? searchKey)
        {
            var entities = await BaseRepository.PagingAsync(pageNumber, pageSize, searchKey);

            var entityDtos = entities.Select(entity => MapEntityToEntityDto(entity)).ToList();

            return entityDtos;
        }

    }
}
