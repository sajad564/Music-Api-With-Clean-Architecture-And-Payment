using System.Linq;
using music.Domain.Common;
using music.Domain.Entities;
using music.Domain.PaginationParams;
using music.Domain.Repositories;
using music.Infrastructure.Data.Data;

namespace music.Infrastructure.Data.Repository
{
    public class OrderRepository : BaseRepository<Order>,IOrderRepository
    {
        public OrderRepository(MusicDbContext context) : base(context)
        {
            
        }

        public PagedList<Order> GetOrderPerPage(OrderPaginationParams paginationParams)
        {
            var filteredSource = Filter(paginationParams) ; 
            return PagedList<Order>.ToPagedList(filteredSource ,paginationParams.PageSize , paginationParams.PageNumber ) ; 
        }
        private IQueryable<Order> Filter(OrderPaginationParams paginationParams)
        {
            return FindByExpression(o =>
            (string.IsNullOrEmpty(paginationParams.musicId) || o.MusicId==paginationParams.musicId)&&
            (string.IsNullOrEmpty(paginationParams.albumId) || o.albumId==paginationParams.albumId)&&
            (string.IsNullOrEmpty(paginationParams.UserId) || o.UserId==paginationParams.UserId)&&
            (o.CreatedDateTime<=paginationParams.MaxDateTime && o.CreatedDateTime>=paginationParams.MinDateTime )
            ).AsQueryable() ; 
        }
    }
}