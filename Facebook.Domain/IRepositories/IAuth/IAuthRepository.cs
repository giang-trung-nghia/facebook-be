using Facebook.Domain.Entities;
using Facebook.Domain.Entities.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domain.IRepositories
{
    public interface IAuthRepository
    {
        Task<UserEntity> SignInAsync(AuthEntity auth);
        Task<UserEntity> SignUpAsync(AuthEntity auth);
        Task<UserEntity> SignOutAsync(string username);
        Task<bool> IsValidUserAsync(AuthEntity users);
        UserRefreshTokens AddUserRefreshTokens(UserRefreshTokens user);
        UserRefreshTokens GetSavedRefreshTokens(string username, string refreshtoken);
        bool DeleteUserRefreshTokens(string username, string refreshToken);
        IActionResult SignInGoogle();
        IActionResult GoogleCallback();
        IActionResult RefreshToken(Token token);
    }
}
