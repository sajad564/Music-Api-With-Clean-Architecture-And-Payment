namespace music.Domain.Entities
{
    public class PaymentInfo
    {
        public double Value {get;set;}
        public string Subject {get;set;}
        public string CartId {get;set;}
        public string CallBack {get;set;}
        public string userEmail {get;set;}
        public string PaymentUrl {get;set;}
    }
}