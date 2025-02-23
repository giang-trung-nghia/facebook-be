using AutoMapper;
using Facebook.Application.Dtos.Conservation;
using Facebook.Application.Dtos.Relationship;
using Facebook.Application.Dtos.Users;
using Facebook.Application.IServices.IConservation;
using Facebook.Application.IServices.IRelationship;
using Facebook.Application.IServices.IUsers;
using Facebook.Application.Services.Base;
using Facebook.Domain.Entities.Base;
using Facebook.Domain.Entities.Conservation;
using Facebook.Domain.IRepositories.IConservation;
using Facebook.Domain.IRepositories.IRelationship;

namespace Facebook.Application.Services.Conservation
{
    public class ConservationService : BaseService<ConservationEntity, ConservationDto, ConservationCreateDto, ConservationUpdateDto>, IConservationService
    {
        private readonly IConservationRepository _conservationRepository;
        private readonly IRelationshipService _relationshipService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ConservationService(IConservationRepository conservationRepository, IRelationshipService relationshipRepository,IUserService userService, IMapper mapper) : base(conservationRepository)
        {
            _conservationRepository = conservationRepository;
            _relationshipService = relationshipRepository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<ConservationDto> GetByRelationshipId(Guid id)
        {
            var relationshipDto = await _relationshipService.GetAsync(id);
            if (relationshipDto.ConservationId == Guid.Empty)
            {
                var memberIds = new List<Guid>();
                memberIds.Add(relationshipDto.FromUserId);
                memberIds.Add(relationshipDto.ToUserId);
                var conservationDto = await InsertAsync(new ConservationCreateDto
                {
                    MemberIds = memberIds,
                });

                var updatedRelationshipDto = await _relationshipService.UpdateAsync(id, new Dtos.Relationship.RelationshipUpdateDto
                {
                    ConservationId = conservationDto.Id
                });

                return conservationDto;

            } else
            {
                var conservationDto = await GetAsync(relationshipDto.ConservationId);
                return conservationDto;

            }

        }

        public override async Task<ConservationDto> InsertAsync(ConservationCreateDto conservationCreateDto)
        {
            var member = new List<UserDto>();
            foreach (var memberId in conservationCreateDto.MemberIds)
            {
                member.Add(await _userService.GetAsync(memberId));
            }

            var entity = new ConservationEntity(); // kiểm tra xem có add member vào luôn được không
            // tạo member mới


            if (entity.GetId() == Guid.Empty)
            {
                entity.SetId(Guid.NewGuid());
            }

            entity.CreatedDate ??= DateTime.Now;
            entity.ModifiedDate ??= DateTime.Now;

            //await ValidateInsertBusiness(entity); 

            await _conservationRepository.InsertAsync(entity);

            var entityDto = MapEntityToEntityDto(entity);
            return entityDto;
        }

        public override ConservationDto MapEntityToEntityDto(ConservationEntity entity)
        {
            var result = _mapper.Map<ConservationDto>(entity);
            return result;
        }

        public override ConservationEntity MapInsertDtoToEntity(ConservationCreateDto insertDto)
        {
            var result = _mapper.Map<ConservationEntity>(insertDto);
            return result;
        }

        public override ConservationEntity MapUpdateDtoToEntity(ConservationUpdateDto updateDto, ConservationEntity entity)
        {
            var result = _mapper.Map(updateDto, entity);
            return result;
        }
    }
}
