using System.Collections.Generic;
using music.Domain.Common;

namespace music.Domain.Entities
{
    public class Grade : BaseEntity
    {
        public int Count {get;set;}
        public double Value {get;set;} // grade value :)
        public string SignerId {get;set;}
        public string AlbumId {get;set;}
        public string MusicId {get;set;}
        public Album Album {get;set;}
        public Music Music {get;set;}
        public List<VotingRelationship> Votes {get;set;}
        public User Signer {get;set;}
        public Grade()
        {
            Votes = new List<VotingRelationship>() ; 
        }
    }
}