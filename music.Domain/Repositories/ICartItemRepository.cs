using System.Threading.Tasks;
using music.Domain.Common;
using music.Domain.Entities;

namespace music.Domain.Repositories
{
    public interface ICartItemRepository : IBaseRepository<CartItem>
    {
        Task<CartItem> GetCartItemById(string cartItemId) ;
    }
}