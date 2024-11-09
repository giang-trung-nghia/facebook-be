using Facebook.Application.Dtos.Users;
using Facebook.Application.IServices.IBase;
using Facebook.Application.IServices.IUsers;
using Facebook.Application.Services.Base;
using Facebook.Domain.Entities;
using Facebook.Domain.IRepositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.Services.Users
{
    public class UserService : BaseService<UserEntity, UserDto>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public override UserDto MapEntityToEntityDto(UserEntity entity)
        {
            var user = new UserDto
            {
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                Id = entity.Id,
                ModifiedBy = entity.ModifiedBy,
                ModifiedDate = entity.ModifiedDate,
                Dob = entity.Dob,
                Email = entity.Email,
                Name = entity.Name,
                Phone = entity.Phone,
                ProfilePicture = entity.ProfilePicture,
            };

            return user;
        }
    }
}
