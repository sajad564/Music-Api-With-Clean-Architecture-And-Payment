using System.Threading.Tasks;
using music.Domain.Common;
using music.Domain.Entities;
using music.Domain.PaginationParams;

namespace music.Domain.Services.Internal
{
    public interface IShopingService : IBaseService
    {
        Task<Error<PaymentInfo>>  Payment() ; 
        Error<PagedList<Order>> OrderPerPage(OrderPaginationParams paginationParams) ;
        Task<Error<Order>> GetOrderById(string OrderId) ;       
        Task<Error> ClearCart() ; 
    }
}