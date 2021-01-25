using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace music.Domain.Common
{
    public class PagedList<T> : List<T> where T : class
    {
        public int Pagenumber {get;set;}
        public int Pagesize {get;set;}
        public int Totalpages {get;set;}
        public int Totalcount {get;set;}
        public PagedList( int _Pagenumber , int _Pagesize  , int _Totalcount, IEnumerable<T> items)
        {
            Totalpages = (int)Math.Ceiling(_Totalcount/(double)_Pagesize) ; 
            Totalcount = _Totalcount ; 
            Pagenumber = _Pagenumber ;
            Pagesize = _Pagesize ;
            AddRange(items) ; 

        }
        public static  PagedList<T> ToPagedList(IQueryable<T> source , int PageSize , int Pagenumber )
        {
            int count = source.Count() ; 
            IEnumerable<T> items = source.Skip((Pagenumber-1)*PageSize).Take(PageSize)  ; 
            return new PagedList<T>(Pagenumber , PageSize , count ,items) ; 
        }
    }
}