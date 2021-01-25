using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using music.Domain.Entities;

namespace music.Infrastructure.Data.Data.Configurations
{
    public class FollowerRelationConfiguration : IEntityTypeConfiguration<FollowRelationShip>
    {
        public void Configure(EntityTypeBuilder<FollowRelationShip> builder)
        {
           builder.HasKey(f => f.Id) ; 
           builder
           .HasOne(fr => fr.FromUser)
           .WithMany(u => u.Followeds)
           .HasForeignKey(fr => fr.FromUserId) ;  

           builder.HasOne(fr => fr.ToUser)
           .WithMany(u => u.Followers)
           .HasForeignKey(fr => fr.ToUserId) ; 

        }
    }
}