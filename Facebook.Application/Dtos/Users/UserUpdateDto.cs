using Facebook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.Dtos.Users
{
    public class UserUpdateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Phone { get; set; }
        public DateOnly? Dob { get; set; }
        public string? ProfilePicture { get; set; }
        public Gender? Gender { get; set; }
        public string? WorkAt { get; set; }
        public string? Location { get; set; }
        public string? University { get; set; }
    }
}
