using AutoMapper;
using Facebook.Application.Dtos.Relationship;
using Facebook.Application.Dtos.Users;
using Facebook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.Mapping
{
    public class RelationshipProfile : Profile
    {
        public RelationshipProfile()
        { 
            CreateMap<RelationshipEntity, RelationshipDto>();
            CreateMap<RelationshipCreateDto, RelationshipEntity>();
            CreateMap<RelationshipUpdateDto, RelationshipEntity>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
