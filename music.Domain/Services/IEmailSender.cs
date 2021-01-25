using System.Threading.Tasks;
using music.Domain.Entities;

namespace music.Domain.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailMessage message) ;
        Task EmailConfirmationAsync(User user) ;
        Task PasswordRecoveryAsync(User user)   ; 
    }
}