using System.Collections.Generic;
using music.Domain.Common;

namespace music.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public double Price {get;set;}
        public List<CartItem> Items {get;set;}
        public string UserId {get;set;}
        public User User {get;set;}
        public Cart()
        {
            Items = new List<CartItem>() ; 
        }
    }
}