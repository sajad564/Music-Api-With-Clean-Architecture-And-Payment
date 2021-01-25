using System.Threading.Tasks;
using music.Domain.Repositories ; 

namespace music.Domain
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepo {get;}
        IPhotoRepository PhotoRepo {get;}
        IFileRepository FileRepo {get;}
        IOrderRepository OrderRepo {get;}
        IMusicRepository MusicRepo {get;}
        IAlbumRepository AlbumRepo {get;}
        IGradeRepository GradeRepo {get;}
        ICartItemRepository CartItemRepo {get;}
        ICartRepository CartRepo {get;}
        Task<bool> SaveChangesAsync() ; 
    }
}