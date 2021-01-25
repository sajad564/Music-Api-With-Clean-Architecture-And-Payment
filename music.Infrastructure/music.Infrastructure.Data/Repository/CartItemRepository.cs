using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using music.Domain.Entities;
using music.Domain.Repositories;
using music.Infrastructure.Data.Data;

namespace music.Infrastructure.Data.Repository
{
    public class CartItemRepository : BaseRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(MusicDbContext context) : base(context)
        {
            
        }
        public async Task<CartItem> GetCartItemById(string cartItemId)
        {
            return await FindByExpression(ci => ci.Id==cartItemId).Include(c => c.Music).Include(c => c.Album).FirstOrDefaultAsync() ; 
        }
    }
}