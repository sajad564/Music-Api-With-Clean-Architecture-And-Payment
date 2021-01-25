using FluentValidation;
using music.Api.Dtos;

namespace music.Api.validations
{
    public class AddAlbumGradeDtoValidation : AbstractValidator<AddAlbumGradeDto>
    {
        public AddAlbumGradeDtoValidation()
        {
           RuleFor(a => a.Albumid).Must(aId => !string.IsNullOrEmpty(aId)).WithMessage("لطفا آیدی را وارد کنید") ; 
            RuleFor(a => a.Score).Must(score => score>=0 && score<=5).WithMessage("امتیاز وارد شده معتبر نمیباشد"); 
        }
    }
}