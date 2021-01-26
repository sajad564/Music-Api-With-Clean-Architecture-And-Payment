using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using music.Domain.Entities;
using music.Infrastructure.Data.Data.Configurations;

namespace music.Infrastructure.Data.Data
{
    public class MusicDbContext : IdentityDbContext<User>
    {
        public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options)
        {}
        public DbSet<Music> Musics {get;set;}
        public DbSet<Album> Albums {get;set;}
        public DbSet<File> Files {get;set;}
        public DbSet<Photo> Photos {get;set;}
        public DbSet<Grade> Grades {get;set;}
        public DbSet<Cart> Carts {get;set;}
        public DbSet<Favorite> Favorites {get;set;}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AlbumConfiguration()) ; 
            builder.ApplyConfiguration(new FollowerRelationConfiguration()) ; 
            builder.ApplyConfiguration(new MusicConfiguration()) ; 
            builder.ApplyConfiguration(new UserConfiguration()) ; 
            builder.SeedData() ;

        }
    }
}