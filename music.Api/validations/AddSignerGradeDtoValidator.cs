using FluentValidation;
using music.Api.Dtos;

namespace music.Api.validations
{
    public class AddSignerGradeDtoValidator : AbstractValidator<AddSignerGradeDto>
    {
        public AddSignerGradeDtoValidator()
        {
            RuleFor(asGrade => asGrade.Signerid).Must(signerId => !string.IsNullOrEmpty(signerId)).WithMessage("درخواست نامعتبر") ; 
            RuleFor(asGrade => asGrade.Score).Must(score => score>=0 && score<=5).WithMessage("امتیاز وارد شده معتبر نمیباشد") ; 
        }
    }
}