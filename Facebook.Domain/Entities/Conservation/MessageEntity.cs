using Facebook.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domain.Entities.Conservation
{
    public class MessageEntity : BaseEntity
    {
        [ForeignKey(nameof(Member))]
        public Guid? MemberId { get; set; }
        public ConservationMemberEntity? Member { get; set; }

        public required string Content { get; set; }

        public ICollection<MessageReadByEntity> ReadBy { get; set; }
    }
}
