using FluentValidation;
using music.Api.Dtos;

namespace music.Api.validations
{
    public class AddUserPhotoDtoValidation : AbstractValidator<AddUserPhotoDto>
    {
        public AddUserPhotoDtoValidation()
        {
            RuleFor(uPhoto => uPhoto.Userid).Must(userId => !string.IsNullOrEmpty(userId)).WithMessage("درخواست نامعتبر") ; 
            RuleFor(uPhoto => uPhoto.Photo).Must(photo => photo!=null && photo.Length>0).WithMessage("لطفا یک فایل معتبر انتخاب کنید") ; 
        }
    }
}