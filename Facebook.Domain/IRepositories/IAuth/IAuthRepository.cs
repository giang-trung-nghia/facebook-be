using Facebook.Domain.Entities;
using Facebook.Domain.Entities.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Facebook.Domain.IRepositories
{
    public interface IAuthRepository
    {
        Task<UserEntity> SignInAsync(SignInEntity signInEntity);
        Task<UserEntity> SignUpAsync(SignUpEntity signUpEntity);
        Task<UserEntity> SignOutAsync(string username);
        UserRefreshTokens AddUserRefreshTokens(UserRefreshTokens user);
        UserRefreshTokens GetSavedRefreshTokens(Guid userId, string refreshtoken);
        bool DeleteUserRefreshTokens(string username, string refreshToken);
        Task<UserEntity> SignInWithGoogleAsync(string googleIdToken);
        Task<UserEntity> SignUpWithGoogleAsync(Payload payload);
        //IActionResult SignInGoogle();
        //IActionResult GoogleCallback();
        //IActionResult RefreshToken(Token token);
    }
}
