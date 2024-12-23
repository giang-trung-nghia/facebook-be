using Facebook.Domain.Entities;
using Facebook.Domain.IRepositories.IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domain.IRepositories.IRelationship
{
    public interface IRelationshipRepository : IBaseRepository<RelationshipEntity>
    {
        Task<RelationshipEntity> AcceptFriend(Guid id);
    }
}
