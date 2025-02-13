using Facebook.Domain.Entities;
using Facebook.Domain.Enums;
using Facebook.Domain.IRepositories.IRelationship;
using Facebook.Infrastructure.Migrations.Contexts;
using Facebook.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Infrastructure.Repositories
{
    public class RelationshipRepository : BaseRepository<RelationshipEntity>, IRelationshipRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<RelationshipEntity> _dbRelationshipSet; // test: use _dbSet in base
        public RelationshipRepository(AppDbContext context) : base(context)
        {
            _context = context;
            _dbRelationshipSet = _context.Set<RelationshipEntity>();
        }

        public async Task<RelationshipEntity> AcceptFriend(Guid id)
        {
            var relationship = await _dbRelationshipSet.SingleAsync(r => r.Id == id);
            return relationship;
        }
    }
}
