using FluentValidation;
using music.Api.Dtos;

namespace music.Api.validations
{
    public class AddMusicDtoValidation : AbstractValidator<AddMusicDto>
    {
        public AddMusicDtoValidation()
        {
            RuleFor(aMusic => aMusic.Name).Must(name => !string.IsNullOrEmpty(name)).WithMessage("لطفا نام موزیک را وارد کنید") ;
            RuleFor(aMusic => aMusic.Playtime).Must(playTime => playTime>0).WithMessage("لطفا زمان آهنگ خود را وارد کنید") ; 
        }
    }
}