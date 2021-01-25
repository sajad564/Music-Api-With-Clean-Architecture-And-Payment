using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using music.Domain.Common;
using music.Infrastructure.Data.Data;

namespace music.Infrastructure.Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private DbSet<T> table {get;set;}
        protected MusicDbContext db {get;set;}
        public BaseRepository(MusicDbContext Db)
        {
            db = Db ;
            table = Db.Set<T>() ; 
        }
       

        public IQueryable<T> GetAll()
        {
            return  table ; 
        }
        public async Task AddAsync(T Entity)
        {
            await table.AddAsync(Entity) ; 
        }
        public  IQueryable<T> FindByExpression(Expression<Func<T, bool>> predicate)
        {
            return  table.Where(predicate).AsQueryable() ;
        }

        public void Update(T Entity)
        {
            table.Update(Entity) ; 
        }

        public async Task<T> GetByIdAsync(string Id)
        {
            return await table.FindAsync(Id) ;
        }

        public void Remove(T entity)
        {
            table.Remove(entity)  ;
        }
    }
}