using System;
using System.Collections.Generic;
using music.Domain.Common;

namespace music.Domain.Entities
{
    public class Music : BaseProduct
    {
        public string Name {get;set;}
        public bool IsFree {get;set;}

        public List<CartItem> CartItems {get;set;}
        public string SignerId {get;set;}
        public double PlayTime {get;set;}
        public User Signer {get;set;}
        public DateTime PublishedDate {get;set;}
        public List<Photo> Photos {get;set;}
        public List<File> Files {get;set;}
        public Grade Grade {get;set;}
        public List<Favorite> Favorites {get;set;}
        public string AlbumId {get;set;}
        public Album Album {get;set;}
        public Music()
        {
            Files = new List<File>() ; 
            Favorites = new List<Favorite>() ; 
            Photos = new List<Photo>() ; 
            CartItems = new List<CartItem>() ;
            
        }

    }
}