using Facebook.Application.Dtos.Users;
using Facebook.Application.IServices.IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.IServices.IUsers
{
    public interface IUserService : IBaseService<UserDto>
    {
    }
}
