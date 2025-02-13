using Facebook.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domain.Entities.Conservation
{
    public class MessageReadByEntity : BaseEntity
    {
        [ForeignKey(nameof(Member))]
        public Guid MemberId { get; set; }
        public ConservationMemberEntity Member { get; set; }

        [ForeignKey(nameof(Message))]
        public Guid MessageId { get; set; }
        public MessageEntity Message { get; set; }
    }
}
