using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using music.Domain.Common;

namespace music.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string AboutMe {get;set;}
        public Photo Photo {get;set;}
        public string RefreshToken {get;set;}
        public List<FollowRelationShip> Followers {get;set;}
        public List<FollowRelationShip> Followeds {get;set;} 
        public List<Music> Musics {get;set;}
        public Cart Cart {get;set;}
        public Grade Grade {get;set;}
        public List<Album> Albums {get;set;}
        public List<Order> Orders {get;set;}
        public List<Favorite> Favorites {get;set;}
        public List<VotingRelationship> Votes {get;set;}
        public User()
        {
            Votes = new List<VotingRelationship>()  ; 
            Albums = new List<Album>() ; 
            Followers = new List<FollowRelationShip>() ; 
            Followeds = new List<FollowRelationShip>() ; 
            Musics = new List<Music>() ; 
            Favorites = new List<Favorite>() ; 
        }
    }
}