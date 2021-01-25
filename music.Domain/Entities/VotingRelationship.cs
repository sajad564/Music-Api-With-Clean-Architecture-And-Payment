using music.Domain.Common;

namespace music.Domain.Entities
{
    public class VotingRelationship : BaseEntity
    {
        public string VoterId {get;set;}
        public string GradeId {get;set;}
        public double Value {get;set;}
        public User Voter {get;set;}
        public Grade Grade {get;set;}
    }
}