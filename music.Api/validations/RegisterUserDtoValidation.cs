using FluentValidation;
using music.Api.Dtos;

namespace music.Api.validations
{
    public class RegisterUserDtoValidation : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidation()
        {
            RuleFor(rUser => rUser.Username).Must(username => !string.IsNullOrEmpty(username)) ;
            RuleFor(rUser => rUser.Firstname).Must(firstname => !string.IsNullOrEmpty(firstname)) ;
            RuleFor(rUser => rUser.Lastname).Must(lastname => !string.IsNullOrEmpty(lastname)) ;
            RuleFor(rUser => rUser.Password).Must(password => !string.IsNullOrEmpty(password)) ;
            RuleFor(rUser => rUser.Email).Must(email => !string.IsNullOrEmpty(email)).EmailAddress().WithMessage("ایمیل نامعتبر") ;  
        }
    }
}