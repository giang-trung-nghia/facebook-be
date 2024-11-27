﻿using Facebook.Application.IServices.IBase;
using Facebook.Domain.Entities.Base;
using Facebook.Domain.Enums;
using Facebook.Domain.IRepositories.IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.Services.Base
{
    public abstract class BaseService<TEntity, TEntityDto, TInsertDto, TUpdateDto> : IBaseService<TEntityDto, TInsertDto, TUpdateDto> where TEntity : IEntity
    {
        protected readonly IBaseRepository<TEntity> BaseRepository;

        protected BaseService(IBaseRepository<TEntity> baseRepository)
        {
            BaseRepository = baseRepository;
        }


        #region Get 
        public async Task<TEntityDto> GetAsync(Guid id)
        {
            var entity = await BaseRepository.GetAsync(id);

            var entityDto = MapEntityToEntityDto(entity);

            return entityDto;
        }

        public async Task<List<TEntityDto>> GetAllAsync()
        {
            var entities = await BaseRepository.GetAllAsync();

            var entityDtos = entities.Select(entity => MapEntityToEntityDto(entity)).ToList();

            return entityDtos;
        }


        public async Task<List<TEntityDto>> PagingAsync(int pageNumber, int pageSize, SortOption? sort, string? sortBy, string? searchKey, string? searchBy)
        {
            var entities = await BaseRepository.PagingAsync(pageNumber, pageSize, sort, sortBy, searchKey, searchBy);

            var entityDtos = entities.Select(entity => MapEntityToEntityDto(entity)).ToList();
            return entityDtos;

        }
        #endregion


        public async Task<TEntityDto> InsertAsync(TInsertDto insertDto)
        {
            var entity = MapInsertDtoToEntity(insertDto);

            if (entity.GetId() == Guid.Empty)
            {
                entity.SetId(Guid.NewGuid());
            }

            if (entity is BaseEntity baseEntity)
            {
                baseEntity.CreatedBy ??= "nghia.giangtrung";
                baseEntity.CreatedDate ??= DateTime.Now;
                baseEntity.ModifiedBy ??= "nghia.giangtrung";
                baseEntity.ModifiedDate ??= DateTime.Now;
            }

            await ValidateInsertBusiness(entity); // when validated success that means insert dto already has enough data

            await BaseRepository.InsertAsync(entity); // not update/edit data in Repository layer

            var entityDto = MapEntityToEntityDto(entity);

            return entityDto;
        }

        public async Task<TEntityDto> UpdateAsync(Guid id, TUpdateDto updateDto)
        {
            var entity = await BaseRepository.GetAsync(id);

            if (entity is BaseEntity baseEntity)    
            {
                baseEntity.ModifiedBy ??= "nghia.giangtrung";
                baseEntity.ModifiedDate = DateTime.Now;
            }

            var newEntity = MapUpdateDtoToEntity(updateDto, entity);

            await ValidateUpdateBusiness(newEntity); // when validated success that means update dto already has enough data

            await BaseRepository.UpdateAsync(id, newEntity); // not update/edit data in Repository layer

            var result = MapEntityToEntityDto(newEntity);

            return result;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var result = await BaseRepository.DeleteAsync(id);

            return result;
        }

        public abstract TEntityDto MapEntityToEntityDto(TEntity entity);

        public abstract TEntity MapInsertDtoToEntity(TInsertDto insertDto);

        public abstract TEntity MapUpdateDtoToEntity(TUpdateDto updateDto, TEntity entity);

        public virtual async Task ValidateInsertBusiness(TEntity entity)
        {
            await Task.CompletedTask;
        }

        public virtual async Task ValidateUpdateBusiness(TEntity entity)
        {
            await Task.CompletedTask;
        }


    }
}
