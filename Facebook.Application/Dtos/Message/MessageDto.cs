using Facebook.Application.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.Dtos.Message
{
    public class MessageDto : BaseDto
    {
        public required string Content { get; set; }
        public required Guid MemberId { get; set; }
    }
}
