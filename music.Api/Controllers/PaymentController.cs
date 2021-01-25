using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using music.Api.Common;
using music.Domain.Services.Internal;
using music.Domain.Common;
using ZarinpalSandbox;
using music.Api.Dtos;

namespace music.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IShopingService shopingService;
        private readonly IErrorMessages ErrorMessages;
        private readonly ICartService cartService;
        public PaymentController(ICartService cartService, IShopingService shopingService, IErrorMessages ErrorMessages)
        {
            this.cartService = cartService;
            this.ErrorMessages = ErrorMessages;
            this.shopingService = shopingService;

        }
        [Authorize]
        [HttpPost("[Action]")]
        public async Task<Response<string>> Payment()
        {
            var result = await shopingService.Payment();
            if (result.HaveError)
                return CustomResponse.Fail<string>(result.Message, StatusCodeEnum.BADREQUEST);

            var payment = new Payment((int)result.item.Value);
            var res = payment.PaymentRequest(result.item.Subject, result.item.CallBack, result.item.userEmail);
            if (res.Result.Status == 100)
            {
                var paymentUrl = result.item.PaymentUrl + res.Result.Authority;
                return CustomResponse.Ok<string>(paymentUrl);
            }
            return CustomResponse.Fail<string>(ErrorMessages.paymentProblem, StatusCodeEnum.OK);
        }

        [HttpPost("[Action]")]
        public async Task<Response<bool>> Confirm([FromQuery] ConfirmPaymentDto confirmPayment)
        {
            var status = confirmPayment.Status.ToLower();
            if(status!="ok")
                return CustomResponse.Fail<bool>(ErrorMessages.paymentProblem,StatusCodeEnum.BADREQUEST) ;
            
            var result = await cartService.GetCartById(confirmPayment.cartId) ; 
            var cart = result.item ; 
            var payment = new Payment((int)cart.Price) ; 
            var res = payment.Verification(confirmPayment.Authority).Result ; 
            if(res.Status==100)
            {
                 await shopingService.ClearCart() ; 
                
                return CustomResponse.Ok() ; 
            } ;
             return CustomResponse.Fail<bool>(ErrorMessages.paymentProblem , StatusCodeEnum.INTERNALSERVERERROR) ;
        }
    }
}