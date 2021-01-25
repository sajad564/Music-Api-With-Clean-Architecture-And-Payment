using FluentValidation;
using music.Api.Dtos;
using music.Api.validations.PropertyValidators;
using System.Linq ; 
namespace music.Api.validations
{
    public class AddAlbumPhotoDtoValidation : AbstractValidator<AddAlbumPhotosDto>
    {
        public AddAlbumPhotoDtoValidation()
        {
            RuleFor(aPhoto => aPhoto.Albumid).Must(albumId => !string.IsNullOrEmpty(albumId)).WithMessage("لطفا آیدی را وارد کنید") ; 
            RuleFor(aPhoto => aPhoto.Photos).Must(photos => photos.Any()).WithMessage("لطفا یک عکس انتخاب کنید") ; 
            RuleFor(aPhoto => aPhoto.Photos).SetValidator(new PhotosTypePropertyValidator()).WithMessage("لطفا یک فایل معتبر انتخاب کنید") ; 
        }
    }
}