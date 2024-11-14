using Facebook.Application.Dtos.Common;
using Facebook.Domain.Entities;
using Facebook.Domain.Entities.Auth;
using Facebook.Domain.Exceptions;
using Facebook.Domain.IRepositories;
using Facebook.Domain.IRepositories.IAuth;
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
        private readonly IJwtRepository _jwtRepository;
        public AuthRepository(AppDbContext context, IConfiguration configuration, IJwtRepository jwtRepository)
        {
            _context = context;
            _configuration = configuration;
            _jwtRepository = jwtRepository;
        }

        #region Action
        public async Task<Token> SignInAsync(SignInEntity signInEntity)
        {
            var user = await _context.Users.SingleOrDefaultAsync(a => a.Email == signInEntity.Email);
            if (user == null)
            {
                return null;
            }

            if (user.Password != signInEntity.Password)
            {
                throw new Exception("Password wrong");
            }

            var token = _jwtRepository.GenerateToken(user.Id);

            return token;
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
        public UserRefreshTokenEntity AddUserRefreshTokens(UserRefreshTokenEntity user)
        {
            _context.UserRefreshToken.Add(user);
            _context.SaveChanges();
            return user;
        }

        public bool DeleteUserRefreshTokens(Guid userId, string refreshToken)
        {
            var item = _context.UserRefreshToken.FirstOrDefault(x => x.UserId == userId && x.RefreshToken == refreshToken);
            if (item != null)
            {
                _context.UserRefreshToken.Remove(item);
                return true;
            }
            return false;
        }

        public UserRefreshTokenEntity GetSavedRefreshTokens(Guid userId, string refreshToken)
        {
            var result = _context.UserRefreshToken.FirstOrDefault(x => x.UserId == userId && x.RefreshToken == refreshToken && x.IsActive == true);

            if (result == null)
            {
                throw new NotFoundException("Not found refreshToken", "Un authorization");
            }
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
