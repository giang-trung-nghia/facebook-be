using Facebook.Application.Dtos.Conservation;
using Facebook.Application.Dtos.ConservationMember;
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
    public class ConservationMemberService : BaseService<ConservationMemberEntity, ConservationMemberDto, ConservationMemberCreateDto, ConservationMemberUpdateDto>, IConservationMemberService
    {
        private readonly IConservationMemberRepository _conservationMemberRepository;

        public ConservationMemberService(IConservationMemberRepository conservationMemberRepository) : base(conservationMemberRepository)
        {
            _conservationMemberRepository = conservationMemberRepository;
        }

        public override ConservationMemberDto MapEntityToEntityDto(ConservationMemberEntity entity)
        {
            throw new NotImplementedException();
        }

        public override ConservationMemberEntity MapInsertDtoToEntity(ConservationMemberCreateDto insertDto)
        {
            throw new NotImplementedException();
        }

        public override ConservationMemberEntity MapUpdateDtoToEntity(ConservationMemberUpdateDto updateDto, ConservationMemberEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
