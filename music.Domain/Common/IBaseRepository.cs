using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace music.Domain.Common
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(string Id) ; 
        void Remove(T entity) ; 
        Task AddAsync(T Entity) ; 
        void Update(T Entity) ;
        IQueryable<T> FindByExpression(Expression<Func<T,bool>> predicate) ;  
    }
}