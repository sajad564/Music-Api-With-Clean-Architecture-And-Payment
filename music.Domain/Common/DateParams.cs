using System;

namespace music.Domain.Common
{
    public class DateParams
    {
       private DateTime _MaxDateTime = DateTime.Now ; 
       private DateTime _MinDateTime = DateTime.MinValue ; 
       public DateTime MaxDateTime {
           get {
               return _MaxDateTime ; 
           }
           set {
               if(value>=_MinDateTime && value<=MaxDateTime) {
                   _MaxDateTime = value ; 
               }
           }
       }
       public DateTime MinDateTime {
           get {
               return _MinDateTime ;
           }
           set {
               if(value>=_MinDateTime && value<MaxDateTime) {
                   _MinDateTime = value ;
               }
           }
       } 
    }
}