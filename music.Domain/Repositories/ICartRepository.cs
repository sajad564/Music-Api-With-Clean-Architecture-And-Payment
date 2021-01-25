using System.Collections.Generic;
using System.Threading.Tasks;
using music.Domain.Common;
using music.Domain.Entities;

namespace music.Domain.Repositories
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        Task<Cart> GetCartById(string cartId) ;
        Task<Cart> GetCartByUserId(string userId) ;  
    }
}