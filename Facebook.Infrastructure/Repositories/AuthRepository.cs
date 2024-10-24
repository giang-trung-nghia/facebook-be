using Facebook.Domain.Entities;
using Facebook.Domain.IRepositories;
using Facebook.Infrastructure.Migrations.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

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
    }
}
