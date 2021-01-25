using FluentValidation;
using music.Api.Dtos;

namespace music.Api.validations
{
    public class AddAlbumToCartDtoValidation : AbstractValidator<AddAlbumToCartDto>
    {
        public AddAlbumToCartDtoValidation()
        {
            RuleFor(atCart => atCart.Albumid).Must(albumId => !string.IsNullOrEmpty(albumId)).WithMessage("درخواست نامعتبر") ;
            RuleFor(atCart => atCart.Cartid).Must(cartId => !string.IsNullOrEmpty(cartId)).WithMessage("درخواست نامعتبر"); 
        }
    }
}