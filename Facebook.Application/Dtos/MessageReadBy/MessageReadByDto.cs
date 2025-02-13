using Facebook.Application.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.Dtos.MessageReadBy
{
    public class MessageReadByDto : BaseDto
    {
        public Guid MemberId { get; set; }
        public Guid MessageId { get; set; }
    }
}
