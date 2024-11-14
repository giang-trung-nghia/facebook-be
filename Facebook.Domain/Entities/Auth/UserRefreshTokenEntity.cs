using Facebook.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domain.Entities.Auth
{
    public class UserRefreshTokenEntity : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string RefreshToken { get; set; }
        public DateTime ExpiredDate { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
