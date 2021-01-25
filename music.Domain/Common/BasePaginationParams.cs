namespace music.Domain.Common
{
    public class BasePaginationParams : DateParams 
    {
        private int _PageNumber {get;set;} = 1 ;
        private int _PageSize {get;set;} = 5 ;
        private int MaxPageSize = 10 ; 
        public int PageNumber {
            get {
                return _PageNumber ;
            }
            set {
                if(value>0) {
                    _PageNumber = value ;
                }
            }
        
        }
        public int PageSize {
            get {
                return _PageSize ;
            }
            set {
                if(value>1 && value<=MaxPageSize ) {
                    _PageSize = value  ; 
                }
            }
        }
    }
}