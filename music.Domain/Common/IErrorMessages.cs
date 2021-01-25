namespace music.Domain.Common
{
    public interface IErrorMessages
    {
        string TransactionFail{get;}
        string NotFoundUser {get;}
        string UserAlreadyExist {get;}
        string MusicIsNotFree {get;}
        string EmailNotConfirmed {get;}
        string NotAuthorized {get;}
        string NotFound {get;}
        string FileQualityExist {get;}
        string ScoreAlreadyExist {get;}
        string FileTypeIsNotValid {get;}
        string SelfScoreIsNotValid {get;}
        string AlreadyIsInCart {get;}
        string paymentProblem {get;}
        string RefreshTokenFail {get;}
    }
   
}