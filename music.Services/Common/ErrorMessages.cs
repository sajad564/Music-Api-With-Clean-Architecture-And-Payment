using music.Domain.Common;

namespace music.Services.Common
{
    public class ErrorMessages : IErrorMessages
    {
        public string NotFoundUser => "اطلاعات شما معتبر نمیباشد";

        public string UserAlreadyExist => "کاربری با این اطلاعات قبلا ثبت نام کرده است";

        public string MusicIsNotFree => throw new System.NotImplementedException();

        public string TransactionFail => "عملایت با شکست مواجه شد";

        public string EmailNotConfirmed => "لطفا ابتدا حساب کاربری خود را تایید کنید";

        public string NotAuthorized => "شما اجازه انجام اینکار را ندارید" ;

        public string NotFound => "آیتم مورد نظر یافت نشد";

        public string FileQualityExist => " چنین کیفیتی از قبل برای این موزیک/آلبوم قرار گرفته است " ; 
        public string FileTypeIsNotValid => "نوع فایل وارد شده معتبر نمیباشد";

        public string ScoreAlreadyExist => "شما قبلا یک امتیاز ثبت کرده اید";

        public string SelfScoreIsNotValid => "شما نمیتوانید به خود امتیاز دهید";

        public string AlreadyIsInCart => "شما قبلا این آیتم را سفارش داده اید";

        public string paymentProblem => "عملایت پرداخت ناموفق بود,لطفا بعدا امتحان کنید" ;

        public string RefreshTokenFail => "لطفا مجددا لاگین نمایید" ;
    }
}