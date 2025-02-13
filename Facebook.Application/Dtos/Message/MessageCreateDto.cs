using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.Dtos.Message
{
    public class MessageCreateDto
    {
        public required string Content { get; set; }
        public required Guid UserId { get; set; }
        public required Guid ConservationId { get; set; }
    }
}
