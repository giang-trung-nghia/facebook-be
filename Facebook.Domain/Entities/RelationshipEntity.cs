using Facebook.Domain.Entities.Base;
using Facebook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domain.Entities
{
    public class RelationshipEntity : BaseEntity
    {
        //[ForeignKey(nameof(FromUser))] Use fluentAPI in AppDbContext.cs to handle because we have 2 foreign keys reflect to one table
        public Guid FromUserId { get; set; }
        public UserEntity FromUser { get; set; }

        //[ForeignKey(nameof(ToUser))]
        public Guid ToUserId { get; set; }
        public UserEntity ToUser { get; set; }
        [Required]
        public RelationshipType RelationshipType { get; set; }
        [Required]
        public RelationshipStatus Status { get; set; }
    }
}
