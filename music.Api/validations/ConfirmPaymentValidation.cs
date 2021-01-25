using FluentValidation;
using music.Api.Dtos;

namespace music.Api.validations
{
    public class ConfirmPaymentValidation : AbstractValidator<ConfirmPaymentDto>
    {
        public ConfirmPaymentValidation()
        {
            RuleFor(ce => ce.Authority).Must(authority => !string.IsNullOrEmpty(authority)).WithMessage("درخواست نامعتبر") ;
            RuleFor(ce => ce.Status).Must(status => !string.IsNullOrEmpty(status)).WithMessage("درخواست نامعتبر") ; 
        }   
    }
}