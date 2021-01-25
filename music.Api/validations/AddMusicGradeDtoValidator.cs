using FluentValidation;
using music.Api.Dtos;

namespace music.Api.validations
{
    public class AddMusicGradeDtoValidator : AbstractValidator<AddMusicGradeDto>
    {
        public AddMusicGradeDtoValidator()
        {
                RuleFor(mGrade => mGrade.musicId).Must(musicId => !string.IsNullOrEmpty(musicId)).WithMessage("آیدی موزیک اجباری میباشد") ;
                RuleFor(mGrade => mGrade.Score).Must(grade => grade>=0 && grade<=5).WithMessage("امتیاز وارد شده معتبر نمیباشد") ; 
        }
    }
}