using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using music.Domain.Common;
using music.Domain.Entities;
using music.Domain.Repositories;
using music.Infrastructure.Data.Data;

namespace music.Infrastructure.Data.Repository
{
    public class CartRepository :  BaseRepository<Cart>,ICartRepository
    {
        public CartRepository(MusicDbContext context) : base(context)
        {
            
        }

        public async Task<Cart> GetCartById(string cartId)
        {
            return await FindByExpression(cart => cart.Id==cartId).Include(c => c.Items).FirstOrDefaultAsync() ; 

        }

        public async Task<Cart> GetCartByUserId(string userId)
        {
            return await FindByExpression(c => c.UserId==userId).Include(c => c.Items).FirstOrDefaultAsync() ;
        }
    }
}