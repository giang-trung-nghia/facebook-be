using Facebook.Application.Dtos.Auth;
using Facebook.Application.Dtos.Users;
using Facebook.Domain.Entities.Auth;
using Facebook.Application.Dtos.Common;

namespace Facebook.Application.IServices.IAuth
{
    public interface IAuthService
    {
        Task<UserDto> SignInAsync(SignInDto dto);
        Task<UserDto> SignUpAsync(SignUpDto dto);
        Task<ApiResponse> SignOutAsync(string refreshToken);
        UserRefreshTokens AddUserRefreshTokens(UserRefreshTokens user);
        UserRefreshTokens GetSavedRefreshTokens(Guid userId, string refreshtoken);
        bool DeleteUserRefreshTokens(string username, string refreshToken);
        //IActionResult RefreshToken(Token token);
        Task<UserDto> SignInWithGoogleAsync(string googleId);
    }
}
