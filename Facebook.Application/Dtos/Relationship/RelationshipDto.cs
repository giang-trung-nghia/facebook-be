using Facebook.Application.Dtos.Base;
using Facebook.Domain.Entities;
using Facebook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.Dtos.Relationship
{
    public class RelationshipDto : BaseDto
    {
        public Guid FromUserId { get; set; }
        public Guid ToUserId { get; set; }
        public RelationshipType RelationshipType { get; set; }
        public RelationshipStatus Status { get; set; }
    }
}
