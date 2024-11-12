using Facebook.Application.Dtos.Common;
using Facebook.Domain.Entities;
using Facebook.Domain.Entities.Auth;
using Facebook.Domain.Exceptions;
using Facebook.Domain.IRepositories;
using Facebook.Infrastructure.Migrations.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Facebook.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        #region Action
        public async Task<UserEntity> SignInAsync(SignInEntity signInEntity)
        {
            var result = await _context.Users.SingleOrDefaultAsync(a => a.Email == signInEntity.Email);
            if (result == null)
            {
                return null;
            }

            if (result.Password != signInEntity.Password)
            {
                throw new Exception("Password wrong");
            }

            return result;
        }

        public async Task<ApiResponse> SignOutAsync(string refreshToken)
        {
            var userRefreshToken = await _context.UserRefreshToken
                .FirstOrDefaultAsync(rt => rt.RefreshToken == refreshToken && rt.IsActive == true);

            if (userRefreshToken == null)
            {
                throw new NotFoundException();
            }

            userRefreshToken.IsActive = false;
            await _context.SaveChangesAsync();

            return new ApiResponse(StatusCodes.Status200OK);
        }

        public async Task<UserEntity> SignUpAsync(SignUpEntity signUpEntity)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == signUpEntity.Email);
            if (user == null)
            {
                user = new UserEntity
                {
                    Email = signUpEntity.Email,
                    Password = signUpEntity.Password,
                    Name = signUpEntity.Name,
                    CreatedDate = DateTime.Now,
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }

            return user;
        }
        #endregion

        #region Auth
        public UserRefreshTokens AddUserRefreshTokens(UserRefreshTokens user)
        {
            _context.UserRefreshToken.Add(user);
            _context.SaveChanges();
            return user;
        }

        public bool DeleteUserRefreshTokens(string username, string refreshToken)
        {
            var item = _context.UserRefreshToken.FirstOrDefault(x => x.UserName == username && x.RefreshToken == refreshToken);
            if (item != null)
            {
                _context.UserRefreshToken.Remove(item);
                return true;
            }
            return false;
        }

        public UserRefreshTokens GetSavedRefreshTokens(Guid userId, string refreshToken)
        {
            var result = _context.UserRefreshToken.FirstOrDefault(x => x.Id == userId && x.RefreshToken == refreshToken && x.IsActive == true);
            return result;
        }

        public async Task<UserEntity> SignInWithGoogleAsync(string googleIdToken)
        {
            var payload = ValidateGoogleIdToken(googleIdToken);
            if (payload == null)
            {
                throw new Exception("Can't get data from this google account");
            }

            var googleId = payload.Subject;
            var email = payload.Email;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                return user;
            }
            else
            {
                var newUser = await SignUpWithGoogleAsync(payload);
                return newUser;
            }

        }

        public async Task<UserEntity> SignUpWithGoogleAsync(Payload payload)
        {
            var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                Email = payload.Email,
                Password = payload.Subject,
                Name = payload.Name,
                ProfilePicture = payload.Picture,
                CreatedDate = DateTime.Now,
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private Payload ValidateGoogleIdToken(string idToken)
        {
            var settings = new ValidationSettings
            {
                Audience = new List<string> { _configuration["Authentication:Google:ClientId"] }
            };
            return ValidateAsync(idToken, settings).Result;
        }

        #endregion
    }
}
