using System.Collections.Generic;

namespace music.Domain.Common
{
    public class Error<T>
    {
        public bool HaveError {get;set;} 
        public string Message {get;set;}
        public T item {get;set;}
        public static Error<T> ToError(string errormessage  )
        {
            return new Error<T>
            {
                item = default , 
                HaveError = true , 
                Message = errormessage 
            } ;
        }
        public static Error<T> WithoutError(T item)
        {
            return new Error<T>
            {
                item = item , 
                HaveError = false ,
                Message = null
            }; 
        }
    }
    public class Error 
    {
        public bool HaveError {get;set;} = false ;
        public string Message {get;set;}
        public static Error ToError(string errormessage   )
        {
            return new Error
            { 
                HaveError = true , 
                Message = errormessage 
            } ;
        }
        public static Error WithoutError()
        {
            return new Error
            {
                HaveError = false , 
                Message = null
            } ;
        }
    }
}