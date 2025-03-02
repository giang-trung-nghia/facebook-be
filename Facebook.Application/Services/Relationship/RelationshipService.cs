using AutoMapper;
using Facebook.Application.Dtos.Relationship;
using Facebook.Application.IServices.IRelationship;
using Facebook.Application.Services.Base;
using Facebook.Domain.Entities;
using Facebook.Domain.Entities.Base;
using Facebook.Domain.Enums;
using Facebook.Domain.Exceptions;
using Facebook.Domain.IRepositories.IBase;
using Facebook.Domain.IRepositories.IRelationship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.Services.Relationship
{
    public class RelationshipService : BaseService<RelationshipEntity, RelationshipDto, RelationshipCreateDto, RelationshipUpdateDto>, IRelationshipService
    {

        private readonly IRelationshipRepository _relationshipRepository;
        private readonly IMapper _mapper;

        public RelationshipService(IRelationshipRepository relationshipRepository, IMapper mapper) : base(relationshipRepository)
        {
            _relationshipRepository = relationshipRepository;
            _mapper = mapper;
        }

        public async Task<RelationshipDto> AcceptFriend(Guid id)
        {
            var relationship = await _relationshipRepository.AcceptFriend(id);

            if (relationship.RelationshipType != RelationshipType.Friend)
            {
                throw new BusinessException("This relationship not friend relationship");
            }

            if (relationship.Status != RelationshipStatus.Pending)
            {
                throw new BusinessException("This relationship status not pending status");
            }
            relationship.Status = RelationshipStatus.Accepted;
         
            var result = await _relationshipRepository.UpdateAsync(id, relationship);
            var dto = _mapper.Map<RelationshipDto>(result);

            return dto;
        }

        public override async Task<RelationshipDto> InsertAsync(RelationshipCreateDto createDto)
        {
            var entity = MapInsertDtoToEntity(createDto);

            if (entity.GetId() == Guid.Empty)
            {
                entity.SetId(Guid.NewGuid());
            }

            if (entity is BaseEntity baseEntity)
            {
                baseEntity.CreatedDate ??= DateTime.Now;
                baseEntity.ModifiedDate ??= DateTime.Now;
            }

            await _relationshipRepository.InsertAsync(entity);

            var entityDto = MapEntityToEntityDto(entity);

            return entityDto;
        }

        public override RelationshipDto MapEntityToEntityDto(RelationshipEntity entity)
        {
            var result = _mapper.Map<RelationshipDto>(entity);
            return result;
        }

        public override RelationshipEntity MapInsertDtoToEntity(RelationshipCreateDto insertDto)
        {
            var result = _mapper.Map<RelationshipEntity>(insertDto);
            return result;
        }

        public override RelationshipEntity MapUpdateDtoToEntity(RelationshipUpdateDto updateDto, RelationshipEntity entity)
        {
            var result = _mapper.Map<RelationshipUpdateDto, RelationshipEntity>(updateDto, entity);
            return result;
        }

        public async Task<RelationshipDto> UpdateConservationId(Guid id, Guid conservationId)
        {
            var entity = await _relationshipRepository.GetAsync(id);

            if (entity is BaseEntity baseEntity) {
                baseEntity.ModifiedDate = DateTime.Now;
            }

            var newEntity = new RelationshipEntity
            {
                Id = id,
                CreatedDate = entity.CreatedDate,
                ModifiedDate = entity.ModifiedDate,
                RelationshipType = entity.RelationshipType,
                FromUserId = entity.FromUserId,
                Status = entity.Status,
                ToUserId = entity.ToUserId,
                ConservationId = conservationId,
            };

            await _relationshipRepository.UpdateAsync(id, newEntity);

            var result = MapEntityToEntityDto(newEntity);

            return result;
        }
    }
}
