using System.Threading.Tasks;
using music.Domain;
using music.Domain.Repositories;
using music.Infrastructure.Data.Data;
using music.Infrastructure.Data.Repository;

namespace music.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private MusicDbContext context {get;set;}
        public UnitOfWork(MusicDbContext _context)
        {
            context = _context ;
        }
        public IUserRepository UserRepo => new UserRepository(context) ; 
        public IPhotoRepository PhotoRepo => new PhotoRepository(context) ; 
        public IFileRepository FileRepo => new FileRepository(context) ; 
        public IMusicRepository MusicRepo => new MusicRepository(context) ; 
        public IAlbumRepository AlbumRepo => new AlbumRepository(context) ;

        public IGradeRepository GradeRepo =>new GradeRepository(context);

        public IOrderRepository OrderRepo => new OrderRepository(context) ;

        public ICartItemRepository CartItemRepo => new CartItemRepository(context);

        public ICartRepository CartRepo => new CartRepository(context) ; 

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync()>0 ; 
        }
    }
}