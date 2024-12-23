using Facebook.Application.Dtos.Base;
using Facebook.Application.Dtos.Relationship;
using Facebook.Application.IServices.IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.IServices.IRelationship
{
    public interface IRelationshipService : IBaseService<RelationshipDto, RelationshipCreateDto, RelationshipUpdateDto>
    {
        Task<RelationshipDto> AcceptFriend(Guid id);
    }
}
