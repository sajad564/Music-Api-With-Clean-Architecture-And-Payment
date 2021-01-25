using FluentValidation;
using music.Api.Dtos;

namespace music.Api.validations
{
    public class AddFileDtoValidation : AbstractValidator<AddFileDto>
    {
        public AddFileDtoValidation()
        {
            RuleFor(adFile => adFile.Parentid).Must(parentId => !string.IsNullOrEmpty(parentId)).WithMessage("درخواست نامعتبر"); 
            RuleFor(adFile => adFile.file).Must(file => file!=null && file.Length>0).WithMessage("لطفا یک فایل معتبر انتخاب کنید") ; 
        }
    }
}