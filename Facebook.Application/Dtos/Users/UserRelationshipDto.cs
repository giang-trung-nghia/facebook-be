using Facebook.Application.Dtos.Relationship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.Dtos.Users
{
    public class UserRelationshipDto : UserDto
    {
        public RelationshipDto Relationship{ get; set; }
    }
}
