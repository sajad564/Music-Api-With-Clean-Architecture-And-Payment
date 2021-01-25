using FluentValidation;
using music.Api.Dtos;

namespace music.Api.validations
{
    public class AddAlbumDtoValidation : AbstractValidator<AddAlbumDto>
    {
        public AddAlbumDtoValidation()
        {
            RuleFor(albumDto => albumDto.Name).Must(name => !string.IsNullOrEmpty(name)).WithMessage("لطفا نام آلبوم را وارد کنید") ; 
            RuleFor(AlbumDto => AlbumDto.Count).Must(count => count!=0).WithMessage("تعداد آهنگ های آلبوم نمیتواند صفر باشد") ; 
        }
    }
}