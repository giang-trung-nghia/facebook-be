using Facebook.Domain.Entities;
using Facebook.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Infrastructure.Migrations.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<UserRefreshTokenEntity> UserRefreshToken { get; set; }
        public DbSet<RelationshipEntity> Relationship { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<RelationshipEntity>(e =>
            {
                e.HasOne(r => r.FromUser)
                    .WithMany(u => u.RelationsInitiated)
                    .HasForeignKey(r => r.FromUserId)
                    .OnDelete(DeleteBehavior.NoAction);

                e.HasOne(r => r.ToUser)
                   .WithMany(u => u.RelationsReceived)
                   .HasForeignKey(r => r.ToUserId)
                   .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
