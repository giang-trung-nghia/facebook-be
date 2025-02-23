using AutoMapper;
using Facebook.Application.Dtos.Conservation;
using Facebook.Domain.Entities.Conservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.Mapping
{
    public class ConservationProfile : Profile
    {
        public ConservationProfile() 
        {
            CreateMap<ConservationEntity, ConservationDto>();
            CreateMap<ConservationCreateDto, ConservationEntity>();
            CreateMap<ConservationUpdateDto, ConservationEntity>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
