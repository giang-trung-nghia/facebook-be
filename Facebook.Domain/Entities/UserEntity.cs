using Facebook.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public string? Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? Phone { get; set; }
        public DateOnly? Dob { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
