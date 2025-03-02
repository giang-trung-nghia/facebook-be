using Facebook.Application.Dtos.Base;
using Facebook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.Dtos.Relationship
{
    public class RelationshipUpdateDto
    {
        public RelationshipType? RelationshipType { get; set; }
        public RelationshipStatus? Status { get; set; }
        public Guid ConservationId { get; set; }
    }
}
