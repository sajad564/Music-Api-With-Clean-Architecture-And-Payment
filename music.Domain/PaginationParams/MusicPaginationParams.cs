using music.Domain.Common;
using music.Domain.Entities;

namespace music.Domain.PaginationParams
{
    public class MusicPaginationParams : BasePaginationParams
    {
        public string Name {get;set;}
        public string signerId {get;set;}
        public bool IsFree {get;set;}
        public double? MaxPrice {get;set;}
        public double? minPrice {get;set;}
        private double _minSize {get;set;} = 0 ;
        private double _maxSize {get;set;} = 200 ; 
        public double MinSize {
            get {
                return _minSize ;
            }
            set {
                if(value>_minSize && value<=_maxSize)
                    _minSize = value ;
            }
        } //per meg
        public double MaxSize {
            get {
                return _maxSize ; 
            }
            set {
                if(value>=MinSize && value<=_maxSize)
                   _maxSize = value ; 
            }
        }
    }
}