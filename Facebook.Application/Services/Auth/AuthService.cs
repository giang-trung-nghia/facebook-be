using Facebook.Application.Dtos.Auth;
using Facebook.Application.Dtos.Users;
using Facebook.Application.IServices.IAuth;
using Facebook.Domain.Entities;
using Facebook.Domain.Entities.Auth;
using Facebook.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _AuthRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _AuthRepository = authRepository;
        }

        public async Task<UserDto> SignInAsync(SignInDto dto)
        {
            var entity = await _AuthRepository.SignInAsync(new AuthEntity
            {
                Password = dto.Password,
                Email = dto.Email
            });

            var entityDtos = MapEntityToEntityDto(entity);

            return entityDtos;
        }

        public async Task<bool> IsValidUserAsync(AuthEntity users)
        {
            var result = await _AuthRepository.IsValidUserAsync(users);
            return result;
        }

        public UserRefreshTokens AddUserRefreshTokens(UserRefreshTokens user)
        {
            var result = _AuthRepository.AddUserRefreshTokens(user);
            return result;
        }

        public UserRefreshTokens GetSavedRefreshTokens(string username, string refreshtoken)
        {
            var result = _AuthRepository.GetSavedRefreshTokens(username, refreshtoken);
            return result;
        }

        public bool DeleteUserRefreshTokens(string username, string refreshToken)
        {
            var result = _AuthRepository.DeleteUserRefreshTokens(username, refreshToken);
            return result;
        }

        public async Task<UserDto> SignOutAsync(string username)
        {
            var entity = await _AuthRepository.SignOutAsync(username);

            var entityDtos = MapEntityToEntityDto(entity);

            return entityDtos;
        }

        public async Task<UserDto> SignUpAsync(SignUpDto dto)
        {
            var entity = await _AuthRepository.SignUpAsync(new AuthEntity
            {
                Password = dto.Password,
                Email = dto.Email
            });

            var entityDtos = MapEntityToEntityDto(entity);

            return entityDtos;
        }
        public UserDto MapEntityToEntityDto(UserEntity entity)
        {
            var user = new UserDto
            {
                CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate,
                Id = entity.Id,
                ModifiedBy = entity.ModifiedBy,
                ModifiedDate = entity.ModifiedDate
            };

            return user;
        }
    }
}
