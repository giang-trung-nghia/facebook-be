using Facebook.Application.Dtos.Users;
using Facebook.Application.IServices.IBase;
using Facebook.Application.IServices.IUsers;
using Facebook.Application.Services.Base;
using Facebook.Domain.Entities;
using Facebook.Domain.Enums;
using Facebook.Domain.Exceptions;
using Facebook.Domain.IRepositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.Services.Users
{
    public class UserService : BaseService<UserEntity, UserDto, UserCreateDto, UserUpdateDto>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public override async Task ValidateInsertBusiness(UserEntity user)
        {
            var existedUser = await GetAsync(user.Id);

            if (existedUser != null)
            {
                throw new ConflictException($"User: {user.Id} existed in the database");
            }

            // should validate email dupplicated

        }

        public override UserDto MapEntityToEntityDto(UserEntity userEntity)
        {
            var userDto = new UserDto
            {
                CreatedBy = userEntity.CreatedBy,
                CreatedDate = userEntity.CreatedDate,
                Id = userEntity.Id,
                ModifiedBy = userEntity.ModifiedBy,
                ModifiedDate = userEntity.ModifiedDate,
                Dob = userEntity.Dob,
                Email = userEntity.Email,
                Name = userEntity.Name,
                Phone = userEntity.Phone,
                ProfilePicture = userEntity.ProfilePicture,
                Gender = userEntity.Gender,
                Location = userEntity.Location,
                University = userEntity.University,
                WorkAt = userEntity.WorkAt

            };

            return userDto;
        }

        public override UserEntity MapInsertDtoToEntity(UserCreateDto insertDto)
        {
            var userEntity = new UserEntity
            {
                Email = insertDto.Email,
                Name = insertDto.Name,
                Password = insertDto.Password,
                ProfilePicture = insertDto?.ProfilePicture,
            };

            return userEntity;
        }

        public override UserEntity MapUpdateDtoToEntity(UserUpdateDto updateDto, UserEntity entity)
        {
            var userEntity = new UserEntity
            {
                Email = updateDto.Email,
                Name = updateDto.Name,
                Password = updateDto.Password,
                Gender = updateDto?.Gender ?? Gender.Other,
                ProfilePicture = updateDto?.ProfilePicture,
                Location = updateDto?.Location,
                Dob= updateDto?.Dob,
                Phone = updateDto?.Phone,
                University= updateDto?.University,
                WorkAt= updateDto?.WorkAt

            };

            return userEntity;
        }
    }
}
