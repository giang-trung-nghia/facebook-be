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
    public class UserProfile : Profile
    {
        public UserProfile()
        { 
            CreateMap<UserEntity, UserDto>();
            //CreateMap<RelationshipEntity, RelationshipDto>();
            CreateMap<UserEntity, UserRelationshipDto>()
                .ForMember(dest => dest.Relationship, opt => opt.MapFrom(src => src.RelationsInitiated.FirstOrDefault()));
            CreateMap<UserCreateDto, UserEntity>();
            CreateMap<UserUpdateDto, UserEntity>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
