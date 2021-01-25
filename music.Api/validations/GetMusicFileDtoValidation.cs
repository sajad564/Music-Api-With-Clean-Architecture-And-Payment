using FluentValidation;
using music.Api.Dtos;

namespace music.Api.validations
{
    public class GetMusicFileDtoValidation : AbstractValidator<GetMusicFileDto>
    {
        public GetMusicFileDtoValidation()
        {
            RuleFor(gmFile => gmFile.Musicid).Must(musicId => !string.IsNullOrEmpty(musicId)).WithMessage("درخواست نامعتبر") ; 
        }
    }
}