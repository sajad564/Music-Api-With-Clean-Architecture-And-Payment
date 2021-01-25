using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using music.Domain;
using music.Domain.Common;
using music.Domain.Entities;
using music.Domain.PaginationParams;
using music.Domain.Services.Internal;
using music.Services.Common;

namespace music.Services.services
{
    public class ShopingService : BaseService, IShopingService
    {
        private readonly IErrorMessages ErrorMessages;
        private readonly IUnitOfWork uow;
        private readonly PaymentConfig paymentConf;
        public ShopingService(PaymentConfig paymentConf, IErrorMessages ErrorMessages, IUnitOfWork uow, IHttpContextAccessor accessor) : base(accessor)
        {
            this.paymentConf = paymentConf;
            this.uow = uow;
            this.ErrorMessages = ErrorMessages;

        }
        public async Task<Error<PaymentInfo>> Payment()
        {
            var findCart = await uow.CartRepo.GetCartByUserId(UserId());
            if (findCart == null)
                return Error<PaymentInfo>.ToError(ErrorMessages.NotFound);
                var paymentInfo = GetPaymentInfo(findCart) ; 
            return Error<PaymentInfo>.WithoutError(paymentInfo);
        }
        public async Task<Error> ClearCart()
        {
            var findCart = await uow.CartRepo.GetCartByUserId(UserId()) ; 
            foreach(var item in findCart.Items)
            {
                var order = new Order
                {
                    UserId = UserId() , 
                    albumId = item.AlbumId ,
                    MusicId = item.MusicId ,
                    CreatedDateTime = DateTime.Now
                };

                await uow.OrderRepo.AddAsync(order) ; 
                uow.CartItemRepo.Remove(item) ; 
            }
            findCart.Price = 0 ; 
            var transactionResult = await uow.SaveChangesAsync();
            if(!transactionResult)
                return Error.ToError(ErrorMessages.TransactionFail);

            return Error.WithoutError();         
        }
        public Error<PagedList<Order>> OrderPerPage(OrderPaginationParams paginationParams)
        {
            if (string.IsNullOrEmpty(paginationParams.UserId))
                paginationParams.UserId = UserId();

            if (!Authorize(paginationParams.UserId))
                return Error<PagedList<Order>>.ToError(ErrorMessages.NotAuthorized);

            PagedList<Order> pagedOrder = uow.OrderRepo.GetOrderPerPage(paginationParams);
            return Error<PagedList<Order>>.WithoutError(pagedOrder);
        }
        // public Task<Error> PaymentConfirm(string status, string Authority)
        // {
        //     status = status.ToLower() ; 
        //     var payment = new Payment()
        // }
        public async Task<Error<Order>> GetOrderById(string OrderId)
        {
            var access = await AuthorizeAccessOrder(OrderId) ;
            if(!access.Authorize)
                return Error<Order>.ToError(ErrorMessages.NotAuthorized) ;
        
            if(access.order==null)
                return Error<Order>.ToError(ErrorMessages.NotFound) ;
        
            return Error<Order>.WithoutError(access.order) ; 
        }
        private async Task<(bool Authorize , Order order)> AuthorizeAccessOrder(string OrderId)
        {
            var order = await uow.OrderRepo.GetByIdAsync(OrderId) ; 
            if(order==null)
                return (false , null) ; 

            if(order.UserId!=UserId())
                return  (false , order) ; 

            return (true , order) ; 
        }
        
        private bool Authorize(string userId)
        {
            return IsInRole("admin") || userId == UserId();
        }
        private PaymentInfo GetPaymentInfo(Cart cart)
        {
            
            return  new PaymentInfo
            {
                Value = cart.Price,
                Subject = $"پرداخت سبد شماره {cart.Id}",
                CartId = cart.Id,
                CallBack = $"{paymentConf.CallBackUrl}?cartId={cart.Id}",
                userEmail = cart.User.Email
            };
        }

        
    }
}