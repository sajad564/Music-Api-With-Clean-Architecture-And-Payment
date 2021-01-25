using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using music.Domain.Entities;

namespace music.Infrastructure.Data.Data.Configurations
{
    public class MusicConfiguration : IEntityTypeConfiguration<Music>
    {
        public void Configure(EntityTypeBuilder<Music> builder)
        {
            builder
            .HasMany(m =>m.Photos)
            .WithOne(p => p.Music)
            .IsRequired()
            .HasForeignKey(p => p.MusicId)
            .OnDelete(DeleteBehavior.Cascade) ; 

            builder
            .HasMany(m => m.Favorites)
            .WithOne(f => f.Music)
            .IsRequired()
            .HasForeignKey(f => f.MusicId)
            .OnDelete(DeleteBehavior.Cascade) ;

            builder
            .HasMany(m => m.Files) 
            .WithOne(f => f.Music)
            .IsRequired()
            .HasForeignKey(f => f.MusicId) 
            .OnDelete(DeleteBehavior.Cascade) ; 
            builder
            .HasOne(m => m.Grade)
            .WithOne(g => g.Music)
            .HasForeignKey<Grade>(g => g.MusicId) 
            .OnDelete(DeleteBehavior.Cascade) ; 
            builder
            .HasMany(m => m.Orders)
            .WithOne(o => o.Music)
            .HasForeignKey(o => o.MusicId)
            .OnDelete(DeleteBehavior.SetNull) ;

            builder
            .HasMany(m => m.CartItems)
            .WithOne(ci => ci.Music)
            .HasForeignKey(ci => ci.MusicId)
            .OnDelete(DeleteBehavior.Cascade) ; 
        }
    }
}