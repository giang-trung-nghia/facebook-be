using Facebook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domain.IRepositories
{
    public interface IAuthRepository
    {
        Task<UserEntity> SignInAsync(AuthEntity auth);
        Task<UserEntity> SignUpAsync(AuthEntity auth);
        Task<UserEntity> SignOutAsync(string username);
    }
}
