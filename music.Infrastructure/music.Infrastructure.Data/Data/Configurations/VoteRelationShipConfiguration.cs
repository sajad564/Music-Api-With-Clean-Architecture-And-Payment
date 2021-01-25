using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using music.Domain.Entities;

namespace music.Infrastructure.Data.Data.Configurations
{
    public class VoteRelationShipConfiguration : IEntityTypeConfiguration<VotingRelationship>
    {
        public void Configure(EntityTypeBuilder<VotingRelationship> builder)
        {
            builder.HasKey(v => v.Id) ; 
            
            builder
            .HasOne(v =>v.Voter)
            .WithMany(u => u.Votes)
            .HasForeignKey(v => v.VoterId)
            .OnDelete(DeleteBehavior.SetNull) ; 

            builder
            .HasOne(v => v.Grade)
            .WithMany(g => g.Votes)
            .HasForeignKey(v => v.GradeId)
            .OnDelete(DeleteBehavior.SetNull) ; 
        }
    }
}