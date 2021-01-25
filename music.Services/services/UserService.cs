using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using music.Domain;
using music.Domain.Common;
using music.Domain.Entities;
using music.Domain.Services;
using music.Domain.Services.Internal;
namespace music.Services.services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IErrorMessages ErrorMessages;
        private readonly ITokenGenerator TokenGenerator;
        public UserService(IUnitOfWork uow, IErrorMessages ErrorMessages, ITokenGenerator TokenGenerator)
        {
            this.TokenGenerator = TokenGenerator;
            this.ErrorMessages = ErrorMessages;
            this.uow = uow;

        }
        public async Task<Error> AddUser(User newUser, string Password)
        {
            var checkUsernameAndPassword = await uow.UserRepo
                                        .FindByExpression(u => u.UserName == newUser.UserName || u.Email == newUser.Email)
                                        .FirstOrDefaultAsync();
            if (checkUsernameAndPassword != null)
                return Error.ToError(ErrorMessages.UserAlreadyExist);

            newUser.PasswordHash = HashPassword(Password);
            newUser.Cart = new Cart() ; 
            await uow.UserRepo.AddAsync(newUser);
            var transactionResult = await uow.SaveChangesAsync();
            if (!transactionResult)
                return Error.ToError(ErrorMessages.TransactionFail);

            return Error.WithoutError();
        }
        private string HashPassword(string Password)
        {
            return BCrypt.Net.BCrypt.HashPassword(Password);
        }

        public async Task<Error> EditUser(User user)
        {
            var findUser = await uow.UserRepo.GetByIdAsync(user.Id);
            findUser.FirstName = user.FirstName;
            findUser.LastName = user.LastName;
            findUser.AboutMe = user.AboutMe;
            uow.UserRepo.Update(findUser);
            var transactionResult = await uow.SaveChangesAsync();
            if (!transactionResult)
                return Error.ToError(ErrorMessages.TransactionFail);

            return Error.WithoutError();
        }
        public async Task<Error<Token>> Refresh(Token token)
        {
            var userId = TokenGenerator.GetUserIdFromExpiredToken(token.AccessToken) ; 
            var findUser = await uow.UserRepo.GetByIdAsync(userId) ;     
            if(token.RefreshToken!=token.RefreshToken)
                return Error<Token>.ToError(ErrorMessages.RefreshTokenFail);

            var newToken = await TokenGenerator.GenerateTokenAsync(findUser) ;
            return Error<Token>.WithoutError(newToken) ;
        }
        public async Task<Error<Token>> LoginUser(string Username, string password)
        {

            var findUser = await uow.UserRepo.GetUserByUsernameAsync(Username);
            if (findUser == null)
                return Error<Token>.ToError(ErrorMessages.NotFoundUser);
            if(!findUser.EmailConfirmed)
                return Error<Token>.ToError(ErrorMessages.EmailNotConfirmed); 
            bool IsValidPassword = VerifyHash(password, findUser.PasswordHash);
            if (!IsValidPassword)
                return Error<Token>.ToError(ErrorMessages.NotFoundUser);
            
            Token token = await TokenGenerator.GenerateTokenAsync(findUser) ; 
            findUser.RefreshToken = token.RefreshToken ;
            var transactionResult = await uow.SaveChangesAsync() ;
            if(!transactionResult)
                return Error<Token>.ToError(ErrorMessages.TransactionFail);
            
            return Error<Token>.WithoutError(token) ; 

        }
        private bool VerifyHash(string password, string HashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, HashPassword);
        }

    }
}