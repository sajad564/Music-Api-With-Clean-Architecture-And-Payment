using System.Collections.Generic;
using System.Threading.Tasks;

namespace music.Domain.Services.Internal
{
    public interface IBaseService
    {
        string UserId() ;
        bool IsInRole(string role) ;
        bool isInOneRole(string role1 , string role2) ;   
        (bool role1 , bool role2) inWichRole(string role1 , string role2);
    }

}