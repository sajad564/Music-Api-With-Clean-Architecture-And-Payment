using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using music.Domain;
using music.Domain.Common;
using music.Domain.Entities;
using music.Domain.Services.Internal;
using music.Services.Common;

namespace music.Services.services
{
    public class CartService : BaseService, ICartService
    {
        private readonly IUnitOfWork uow;
        private readonly IErrorMessages ErrorMessages;
        public CartService(IErrorMessages ErrorMessages, IUnitOfWork uow, IHttpContextAccessor accessor) : base(accessor)
        {
            this.ErrorMessages = ErrorMessages;
            this.uow = uow;

        }
        public async Task<Error<Cart>> GetCartById(string cartId)
        {
            var authorizeAccessCart =await AuthorizeAccessCart(cartId) ;  
            if (!authorizeAccessCart.isValid)
                return Error<Cart>.ToError(ErrorMessages.NotAuthorized) ; 

            var Cart = await uow.CartRepo.GetCartById(cartId) ; 
            if(Cart==null)
                return Error<Cart>.ToError(ErrorMessages.NotFound) ; 
            
            return Error<Cart>.WithoutError(Cart) ; 
        }
        public async Task<Cart> GetCurrenUserCart()
        {
            return await uow.CartRepo.GetCartByUserId(UserId()) ; 
        }

        public async Task<Error> AddAlbumToCart(string albumId, string CartId)
        {
            var authorizeAccessCart =await AuthorizeAccessCart(CartId) ;   
            if (!authorizeAccessCart.isValid)
                return Error.ToError(ErrorMessages.NotAuthorized) ;  
            var cart = authorizeAccessCart.Cart ;
            if(!ProductExist(albumId,CartItemType.ALBUM))
                return Error.ToError(ErrorMessages.NotFound) ;
            
            if(AlreadyIsInCart(CartId , albumId , CartItemType.ALBUM))
                return Error.ToError(ErrorMessages.AlreadyIsInCart) ; 
            
            var album = await uow.AlbumRepo.GetByIdAsync(albumId) ;
            cart.Price+=album.Price ;  
            var newCartItem = new CartItem {AlbumId = albumId , CartId = CartId} ; 
            await uow.CartItemRepo.AddAsync(newCartItem) ; 
            var transactionResult = await uow.SaveChangesAsync() ; 
            if(!transactionResult)
                return Error.ToError(ErrorMessages.TransactionFail) ; 
        
            return Error.WithoutError() ;
        }

        public async Task<Error> AddMusicToCart(string musicId, string CartId)
        {
            var authorizeAccessCart =await AuthorizeAccessCart(CartId) ;  
            if(!authorizeAccessCart.isValid)
                return Error.ToError(ErrorMessages.NotAuthorized) ;
            var cart = authorizeAccessCart.Cart ; 
            if(!ProductExist(musicId , CartItemType.MUSIC))
                return Error.ToError(ErrorMessages.NotFound) ;  

            if(AlreadyIsInCart(CartId , musicId , CartItemType.MUSIC))
                return Error.ToError(ErrorMessages.AlreadyIsInCart) ; 
            var music  = await uow.MusicRepo.GetByIdAsync(musicId) ; 
            cart.Price += music.Price ; 
            var newCartItem = new CartItem {MusicId =musicId ,CartId=CartId} ; 
            await uow.CartItemRepo.AddAsync(newCartItem) ; 
            var transactionResult = await uow.SaveChangesAsync() ; 
            if(!transactionResult)
                return Error.ToError(ErrorMessages.TransactionFail); 
            
            return Error.WithoutError() ;

        }
        public async Task<Error> RemoveCartItem(string itemId)
        {
            var cartItemWithType = await GetCartItemWithType(itemId) ;
            var cartItem = cartItemWithType.cartItem ; 
            var itemType = cartItemWithType.type ; 
            var authorizeAccessCart = await AuthorizeAccessCart(cartItem.CartId) ;  
            if(!authorizeAccessCart.isValid)
                return Error.ToError(ErrorMessages.NotAuthorized) ;
            
            var cart = authorizeAccessCart.Cart ; 
            cart.Price-= itemType==CartItemType.ALBUM ?  cartItem.Album.Price  : cartItem.Music.Price ;
            uow.CartItemRepo.Remove(cartItem) ; 
            var transactionResult = await uow.SaveChangesAsync() ; 
            if(!transactionResult)
                return Error.ToError(ErrorMessages.TransactionFail) ; 

            return Error.WithoutError()  ;
        }
        private async Task<(bool isValid, Cart Cart)> AuthorizeAccessCart(string cartId)
        {
            var Cart = await uow.CartRepo.GetCartById(cartId) ; 
            var Valid = ( Cart!=null && Cart.UserId==UserId()) || (IsInRole("admin")  && Cart!=null)  ;  
            return (Valid , Cart) ; 
        }
        private bool AlreadyIsInCart( string CartId, string parentId , CartItemType type)
        {
            if(type==CartItemType.ALBUM)
                return uow.CartItemRepo.FindByExpression(ci => ci.CartId==CartId && ci.AlbumId==parentId).Any() ; 
            else
                return uow.CartItemRepo.FindByExpression(ci =>ci.CartId==CartId && ci.MusicId==parentId).Any() ; 
        }
        private bool ProductExist(string parentId , CartItemType type)
        {
            if(type==CartItemType.ALBUM)
                return uow.AlbumRepo.FindByExpression(album => album.Id==parentId).Any() ; 
            else
                return uow.MusicRepo.FindByExpression(music => music.Id==parentId).Any() ; 
        }
        private async Task<(CartItemType type , CartItem cartItem)> GetCartItemWithType(string cartItemId)
        {
            var cartItem = await uow.CartItemRepo.GetCartItemById(cartItemId) ; 
            var type = cartItem.Album!=null ? CartItemType.ALBUM : CartItemType.MUSIC ;
            return (type ,cartItem) ; 
        }

        
    }
}