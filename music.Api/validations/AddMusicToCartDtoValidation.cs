using FluentValidation;
using music.Api.Dtos;

namespace music.Api.validations
{
    public class AddMusicToCartDtoValidation : AbstractValidator<AddMusicToCartDto>
    {
        public AddMusicToCartDtoValidation()
        {
            RuleFor(amtCart => amtCart.Cartid).Must(cartId => !string.IsNullOrEmpty(cartId)).WithMessage("درخواست نامعتبر") ; 
            RuleFor(amtCart => amtCart.Musicid).Must(musicId => !string.IsNullOrEmpty(musicId)).WithMessage("درخواست نامعتبر") ; 
        }
    }
}