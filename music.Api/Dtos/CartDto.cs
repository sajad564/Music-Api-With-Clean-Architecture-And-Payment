using System.Collections.Generic;

namespace music.Api.Dtos
{
    public class CartDto
    {
        public string Id {get;set;}
        public double Price {get;set;}
        public string Userid {get;set;}
        public IEnumerable<CartItemDto> Items {get;set;}
        
    }
}