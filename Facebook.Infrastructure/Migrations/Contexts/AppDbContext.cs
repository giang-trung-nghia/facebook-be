using Facebook.Domain.Entities;
using Facebook.Domain.Entities.Auth;
using Facebook.Domain.Entities.Conservation;
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
        public DbSet<ConservationEntity> Conservation { get; set; }
        public DbSet<ConservationMemberEntity> ConservationMember { get; set; }
        public DbSet<MessageEntity> Message { get; set; }

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

            modelBuilder.Entity<MessageEntity>(e =>
            {
                e.HasOne(m => m.Member)
                    .WithOne()
                    .HasForeignKey<MessageEntity>(m => m.MemberId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<MessageReadByEntity>(e =>
            {
                e.HasOne(m => m.Member)
                    .WithMany()
                    .HasForeignKey(m => m.MemberId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(m => m.Message)
                    .WithMany(m => m.ReadBy)
                    .HasForeignKey(m => m.MessageId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
