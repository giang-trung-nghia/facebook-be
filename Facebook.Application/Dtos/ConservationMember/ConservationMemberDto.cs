using Facebook.Application.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.Dtos.ConservationMember
{
    public class ConservationMemberDto : BaseDto
    {
        public string Nickname {  get; set; }
        public Guid UserId { get; set; }
        public Guid ConservationId { get; set; }
    }
}
