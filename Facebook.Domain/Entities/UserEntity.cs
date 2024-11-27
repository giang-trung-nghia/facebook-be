using Facebook.Domain.Entities.Base;
using Facebook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        // Basic information
        public string? Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? Phone { get; set; }
        public DateOnly? Dob { get; set; }
        public string? ProfilePicture { get; set; }
        public Gender Gender { get; set; }
        public string? WorkAt {  get; set; }
        public string? Location { get; set; }
        public string? University { get; set; }

        // Relationship
        public ICollection<RelationshipEntity> RelationsInitiated { get; set; }
        public ICollection<RelationshipEntity> RelationsReceived { get; set; }
    }
}
