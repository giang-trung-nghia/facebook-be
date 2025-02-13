using Facebook.Application.Dtos.Conservation;
using Facebook.Application.IServices.IConservation;
using Facebook.Application.Services.Base;
using Facebook.Domain.Entities.Conservation;
using Facebook.Domain.IRepositories.IBase;
using Facebook.Domain.IRepositories.IConservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.Services.Conservation
{
    public class ConservationService : BaseService<ConservationEntity, ConservationDto, ConservationCreateDto, ConservationUpdateDto>, IConservationService
    {
        private readonly IConservationRepository _conservationRepository;

        public ConservationService(IConservationRepository conservationRepository) : base(conservationRepository)
        {
            _conservationRepository = conservationRepository;
        }

        public override ConservationDto MapEntityToEntityDto(ConservationEntity entity)
        {
            throw new NotImplementedException();
        }

        public override ConservationEntity MapInsertDtoToEntity(ConservationCreateDto insertDto)
        {
            throw new NotImplementedException();
        }

        public override ConservationEntity MapUpdateDtoToEntity(ConservationUpdateDto updateDto, ConservationEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
