using AutoMapper;
using Facebook.Application.Dtos.Auth;
using Facebook.Application.Dtos.Common;
using Facebook.Application.Dtos.Users;
using Facebook.Application.IServices.IAuth;
using Facebook.Domain.Entities;
using Facebook.Domain.Entities.Auth;
using Facebook.Domain.IRepositories;
using Microsoft.AspNetCore.Http;

namespace Facebook.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _AuthRepository;
        private readonly IMapper _mapper;

        public AuthService(IAuthRepository authRepository, IMapper mapper)
        {
            _AuthRepository = authRepository;
            _mapper = mapper;
        }

        public async Task<Token> SignInAsync(SignInDto dto)
        {
            var token = await _AuthRepository.SignInAsync(new SignInEntity
            {
                Password = dto.Password,
                Email = dto.Email
            });

            return token;
        }

        public UserRefreshTokenEntity AddUserRefreshTokens(UserRefreshTokenEntity user)
        {
            var result = _AuthRepository.AddUserRefreshTokens(user);
            return result;
        }

        public UserRefreshTokenEntity GetSavedRefreshTokens(Guid userId, string refreshtoken)
        {
            var result = _AuthRepository.GetSavedRefreshTokens(userId, refreshtoken);
            return result;
        }

        public bool DeleteUserRefreshTokens(Guid userId, string refreshToken)
        {
            var result = _AuthRepository.DeleteUserRefreshTokens(userId, refreshToken);
            return result;
        }

        public async Task<ApiResponse> SignOutAsync(string refreshToken)
        {
            await _AuthRepository.SignOutAsync(refreshToken);
            
            return new ApiResponse(StatusCodes.Status200OK);
        }

        public async Task<UserDto> SignUpAsync(SignUpDto dto)
        {
            var entity = await _AuthRepository.SignUpAsync(new SignUpEntity
            {
                Password = dto.Password,
                Email = dto.Email,
                Name = dto.Name,
            });

            var entityDtos = MapEntityToEntityDto(entity);

            return entityDtos;
        }

        public async Task<UserDto> SignInWithGoogleAsync(string googleId)
        {
            var result = await _AuthRepository.SignInWithGoogleAsync(googleId);
            var dto = MapEntityToEntityDto(result);

            return dto;
        }

        public UserDto MapEntityToEntityDto(UserEntity entity)
        {
            var user = _mapper.Map<UserDto>(entity);

            return user;
        }

        //public IActionResult RefreshToken(Token token)
        //{
        //    var result = _AuthRepository.RefreshToken(token);
        //    throw new NotImplementedException();
        //}
    }
}
