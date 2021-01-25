using System.Collections.Generic;
using music.Domain.Common;

namespace music.Domain.Entities
{
    public class BaseProduct : BaseEntity
    {
        public double Price {get;set;}
        public List<Order> Orders {get;set;}
        public BaseProduct()
        {
            Orders = new List<Order>() ; 
        }
    }
}