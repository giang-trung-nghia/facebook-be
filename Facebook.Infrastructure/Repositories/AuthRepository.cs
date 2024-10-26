using Facebook.Domain.Entities;
using Facebook.Domain.Entities.Auth;
using Facebook.Domain.IRepositories;
using Facebook.Infrastructure.Migrations.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _iconfiguration;
        public AuthRepository(AppDbContext context, IConfiguration iconfiguration)
        {
            _context = context;
            _iconfiguration = iconfiguration;
        }

        #region Action
        public async Task<UserEntity> SignInAsync(AuthEntity auth)
        {
            var result = await _context.Users.SingleAsync(a => a.Email == auth.Email);
            if (result.Password != auth.Password)
            {
                throw new Exception("Password wrong");
            }

            return result;
        }

        public async Task<UserEntity> SignOutAsync(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<UserEntity> SignUpAsync(AuthEntity auth)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == auth.Email);
            if (user == null)
            {
                user = new UserEntity
                {
                    Email = auth.Email,
                    Password = auth.Password,
                    Name = "new user",
                    Phone = "+8400000001",
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

        public UserRefreshTokens GetSavedRefreshTokens(string username, string refreshToken)
        {
            return _context.UserRefreshToken.FirstOrDefault(x => x.UserName == username && x.RefreshToken == refreshToken && x.IsActive == true);
        }

        public async Task<bool> IsValidUserAsync(AuthEntity users)
        {
            var u = _context.Users.FirstOrDefault(o => o.Email == users.Email && o.Password == users.Password);

            if (u != null)
                return true;
            else
                return false;
        }
        #endregion
    }
}
