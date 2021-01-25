using System;
using System.Collections.Generic;
using music.Domain.Common;

namespace music.Domain.Entities
{
    public class Album : BaseProduct
    {
        public string SignerId {get;set;}
        public User Signer {get;set;}
        public string Name {get;set;}
        public int Count {get;set;}
        public Grade Grade {get;set;}
        public bool IsFree {get;set;}
        public List<File> Files {get;set;} //per quality
        public List<Music> Musics {get;set;}
        public DateTime PublishedData {get;set;}
        public List<Favorite> Favorites {get;set;}
        public List<Photo> Photos {get;set;}
        public List<CartItem> CartItems {get;set;}
        public Album()
        {
            Files = new List<File>() ; 
            Musics = new List<Music>() ; 
            Favorites = new List<Favorite>() ; 
            Photos = new List<Photo>() ; 
            CartItems = new List<CartItem>() ;
        }
    }
}