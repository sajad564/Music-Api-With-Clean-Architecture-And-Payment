using FluentValidation;
using music.Api.Dtos;
using music.Api.validations.PropertyValidators;

namespace music.Api.validations
{
    public class AddMusicPhotoDtoValidator : AbstractValidator<AddMusicPhotosDto>
    {
        public AddMusicPhotoDtoValidator()
        {
            RuleFor(mPhoto => mPhoto.Musicid).Must(musicId => !string.IsNullOrEmpty(musicId)) ; 
            RuleFor(mPhoto => mPhoto.Photos).SetValidator(new PhotosTypePropertyValidator()).WithMessage("لطفا یک یا چند فایل معتبر انتخاب کنید") ; 
        }
    }
}