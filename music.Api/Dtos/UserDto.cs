namespace music.Api.Dtos
{
    public class UserDto
    {
        public string Username {get;set;}
        public string Firstname {get;set;}
        public string Aboutme {get;set;}
        public PhotoDto Photo {get;set;}
        public GradeDto Grade {get;set;}
        
    }
}