using Facebook.Application.Dtos.Base;
using Facebook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.Dtos.Users
{
    public class UserCreateDto : BaseDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? ProfilePicture { get; set; }

    }
}
