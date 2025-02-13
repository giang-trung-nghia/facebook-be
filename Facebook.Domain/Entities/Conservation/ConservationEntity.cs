using Facebook.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domain.Entities.Conservation
{
    public class ConservationEntity : BaseEntity
    {
        public ICollection<ConservationMemberEntity> Members { get; set; }

        public DateTime LastMessageTime { get; set; }
        public string Name { get; set; }

    }
}
