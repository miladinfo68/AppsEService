using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.AdobeClasses
{
    public class MPanelDTO
    {        
        public int Customers_ClassName_Id { set; get; }
        public int Customers_ClassName_CustomerId { set; get; }
        public string Customers_ClassName_Name { set; get; }
        public string Customers_ClassName_NameLatin { set; get; }
        public int Customers_ClassName_UserCount { set; get; }
        public int Customers_ClassName_SessionCount { set; get; }
        public int Customers_ClassName_ScoId { set; get; }
        public string Customers_ClassName_ServerName { set; get; }
        public string Customers_ClassName_DateStart { set; get; }
        public string Customers_ClassName_DateEnd { set; get; }
        public string Customers_ClassName_MeetingAccess { set; get; }
    }


    public class Customers_ClassDayTime
    {
        public int Customers_ClassDayTime_Id { set; get; }
        public int Customers_ClassDayTime_ClassNameId { set; get; }
        public int Customers_ClassDayTime_DayTimeId { set; get; }
    }


    public class Customers_Meeting
    {
        public int Customers_Meeting_Id { set; get; }
        public int Customers_Meeting_ScoId { set; get; }
        public string Customers_Meeting_ScoName { set; get; }
        public int Customers_Meeting_ClassId { set; get; }
        public string Customers_Meeting_ServerName { set; get; }        

    }

    public class Customers_Users
    {
        public int Customers_Users_Id { set; get; } 
        public string Customers_Users_Name { set; get; } 
        public string Customers_Users_Family { set; get; } 
        public string Customers_Users_LatinName { set; get; } 
        public string Customers_Users_LatinFamily { set; get; } 
        public string Customers_Users_UserMobile { set; get; } 
        public string Customers_Users_Email { set; get; } 
        public string Customers_Users_UserName { set; get; } 
        public string Customers_Users_NationalCode { set; get; } 
        public int Customers_Users_Sex { set; get;}
        public int Customers_Users_CustomerId { set; get; }
        public string Customers_Users_AdobeUserName { set; get; }

        // چک کردن داده که آیا تکراری است یا خیر؟
        public bool Customers_Users_IsDuplicate { set; get; }

        public string Customers_Users_IdNumber { set; get; }

    }


    public class Customers_UserMeeting
    {
        public int Customers_UserMeeting_Id { set; get; }
        public int Customers_UserMeeting_IdUser { set; get; }
        public int Customers_UserMeeting_IdMeeting { set; get; }
        public string Customers_UserMeeting_UserAccess { set; get; } 

    }





}
