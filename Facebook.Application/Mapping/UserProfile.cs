using AutoMapper;
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
            CreateMap<UserCreateDto, UserEntity>();
            CreateMap<UserUpdateDto, UserEntity>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
