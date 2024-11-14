using Facebook.Application.Dtos.Auth;
using Facebook.Application.Dtos.Users;
using Facebook.Domain.Entities.Auth;
using Facebook.Application.Dtos.Common;

namespace Facebook.Application.IServices.IAuth
{
    public interface IAuthService
    {
        Task<Token> SignInAsync(SignInDto dto);
        Task<UserDto> SignUpAsync(SignUpDto dto);
        Task<ApiResponse> SignOutAsync(string refreshToken);
        UserRefreshTokenEntity AddUserRefreshTokens(UserRefreshTokenEntity user);
        UserRefreshTokenEntity GetSavedRefreshTokens(Guid userId, string refreshtoken);
        bool DeleteUserRefreshTokens(Guid userId, string refreshToken);
        //IActionResult RefreshToken(Token token);
        Task<UserDto> SignInWithGoogleAsync(string googleId);
    }
}
