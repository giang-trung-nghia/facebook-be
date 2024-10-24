using Facebook.Application.Dtos.Auth;
using Facebook.Application.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.IServices.IAuth
{
    public interface IAuthService
    {
        Task<UserDto> SignInAsync(SignInDto dto);
        Task<UserDto> SignUpAsync(SignUpDto dto);
        Task<UserDto> SignOutAsync(string username);
    }
}
