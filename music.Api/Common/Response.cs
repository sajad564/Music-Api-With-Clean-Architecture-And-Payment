using System.Collections.Generic;
using music.Domain.Common;

namespace music.Api.Common
{
    public static class CustomResponse
    {
        public static Response<T> Fail<T>(string error , StatusCodeEnum status )
        {
            IDictionary<string,string[]> errors  = new Dictionary<string,string[]>() ; 
            errors.Add("Global" , new string[]{error}); 
            return new Response<T>(errors , status , default) ; 
        }
        public static Response<bool> Fail(string error , StatusCodeEnum status )
        {
            IDictionary<string,string[]> errors  = new Dictionary<string,string[]>() ; 
            errors.Add("Global" , new string[]{error}); 
            return new Response<bool>(errors , status , false) ; 
        }
        public static Response<T> Fail<T>(IDictionary<string,string[]> errors , StatusCodeEnum status) 
        {
            return new Response<T>(errors, status , default) ; 
        }
        public static Response<T> Ok<T>(T item)
        {
            return new Response<T>(null , StatusCodeEnum.OK , item) ; 
        }
        public static Response<bool> Ok()
        {
            return new Response<bool>(null , StatusCodeEnum.OK , true ) ; 
        }

    }
    public class Response<T>
    {
        public T Item {get;set;}
        public StatusCodeEnum Status {get;set;}
        public IDictionary<string,string[]> Errors {get;set;}

        public Response(IDictionary<string,string[]> errors , StatusCodeEnum status, T item)
        {
            Item = item ; 
            Status = status ; 
            Errors = errors ; 
        }
    }
}