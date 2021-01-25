using music.Domain.Common;

namespace music.Domain.Entities
{
    public class FollowRelationShip : BaseEntity
    {
        public string ToUserId {get;set;}
        public string FromUserId {get;set;}
        public User ToUser {get;set;}
        public User FromUser {get;set;}
    }
}