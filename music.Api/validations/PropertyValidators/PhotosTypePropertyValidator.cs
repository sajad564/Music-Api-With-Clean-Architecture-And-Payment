using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Http;

namespace music.Api.validations.PropertyValidators
{
    public class PhotosTypePropertyValidator : PropertyValidator
    {
        protected override bool IsValid(PropertyValidatorContext context)
        {
            var photos = context.PropertyValue as IEnumerable<IFormFile> ;  
            return ValidatePhotos(photos) ;     
        
        }
        private bool ValidatePhotos(IEnumerable<IFormFile> photos)
        {
            bool isValid = true ; 
            foreach(var photo in photos)
            {
                isValid = !ValidatePhoto(photo) ; 
                if(!isValid)
                    break ; 
            }
            return isValid ; 
        }
        private bool ValidatePhoto(IFormFile photo)
        {
            if(photo.Length==0)
                return false ; 
            var fileExtension = Path.GetExtension(photo.FileName) ; 
            var isValidType = validTypes().Where(type => type==fileExtension).Any() ;
            return isValidType ; 
        }
        public static IEnumerable<string> validTypes()
        {
            return new string[] {".jpeg" , ".png" , "svg" } ; 
        }
    }
}