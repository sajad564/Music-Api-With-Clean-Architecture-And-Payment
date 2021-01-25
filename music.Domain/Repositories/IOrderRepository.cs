using music.Domain.Common;
using music.Domain.Entities;
using music.Domain.PaginationParams;

namespace music.Domain.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        PagedList<Order> GetOrderPerPage(OrderPaginationParams paginationParams) ; 
    }
}