using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using music.Domain.Entities;

namespace music.Infrastructure.Data.Data.Configurations
{
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasKey(a => a.Id) ;

            builder
            .HasOne(a => a.Grade)
            .WithOne(g => g.Album)
            .HasForeignKey<Grade>(g => g.AlbumId)
            .OnDelete(DeleteBehavior.Cascade) ; 

            builder
            .HasMany(a => a.Files)
            .WithOne(af => af.Album)
            .HasForeignKey(af => af.AlbumId) 
            .OnDelete(DeleteBehavior.SetNull) ;
            builder
            .HasMany(a => a.Musics)
            .WithOne(m => m.Album)
            .HasForeignKey(m => m.AlbumId)
            .OnDelete(DeleteBehavior.SetNull) ; 

            builder
            .HasMany(a => a.Photos)
            .WithOne(ph => ph.Album)
            .HasForeignKey(ph => ph.AlbumId)
            .OnDelete(DeleteBehavior.Cascade) ;

             

            builder
            .HasMany(a =>a.Favorites)
            .WithOne(fav => fav.Album)
            .HasForeignKey(fav => fav.AlbumId)
            .OnDelete(DeleteBehavior.Cascade) ; 

            builder
            .HasMany(a => a.Orders)
            .WithOne(o => o.Album)
            .HasForeignKey(o => o.albumId)
            .OnDelete(DeleteBehavior.SetNull) ;
            
            builder
            .HasMany(a => a.CartItems)
            .WithOne(ci => ci.Album)
            .HasForeignKey(ci => ci.AlbumId)
            .OnDelete(DeleteBehavior.Cascade) ;  
        }
    }
}