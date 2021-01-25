using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using music.Domain.Entities;

namespace music.Infrastructure.Data.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasKey(u => u.Id) ; 
            builder
            .HasMany(u => u.Albums)
            .WithOne(a => a.Signer)
            .HasForeignKey(a => a.SignerId)
            .OnDelete(DeleteBehavior.SetNull) ; 

            builder
            .HasMany(u => u.Musics)
            .WithOne(m => m.Signer)
            .HasForeignKey(m => m.SignerId)
            .OnDelete(DeleteBehavior.SetNull) ; 

            builder
            .HasOne(u => u.Photo)
            .WithOne(p => p.User)
            .HasForeignKey<Photo>( p => p.UserId) 
            .OnDelete(DeleteBehavior.Cascade) ; 

            builder
            .HasOne(u => u.Grade)
            .WithOne(g => g.Signer)
            .HasForeignKey<Grade>(g => g.SignerId)
            .OnDelete(DeleteBehavior.Cascade) ; 

            builder
            .HasMany(u => u.Favorites)
            .WithOne(f => f.User)
            .HasForeignKey(f => f.UserId) 
            .OnDelete(DeleteBehavior.Cascade) ;
            
            builder
            .HasOne(u => u.Cart)
            .WithOne(c => c.User)
            .IsRequired()
            .HasForeignKey<Cart>(c =>c.UserId);

            builder
            .HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.SetNull) ; 
        }
    }
}