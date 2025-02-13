using Facebook.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domain.Entities.Conservation
{
    public class ConservationMemberEntity : BaseEntity
    {
        public string? Nickname { get; set; }

        [ForeignKey(nameof(Conservation))]
        public Guid ConservationId { get; set; }
        public ConservationEntity Conservation { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }

    }
}
