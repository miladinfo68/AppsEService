using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.AdobeClasses
{
    public class ManagementPanelDTO
    {
        // in Table: Course_Class
        public int Id { set; get; }
        public string Class{set;get;}
        public string Course{set;get;}
        public int MeetingCount{set;get;}
        public int UserCount{set;get;}
        public string Tterm{set;get;}
        public int IdUniversity { set; get; }
        public int IdCourseTimeClass { set; get; }
        public int IdCourseType { set; get; }


        // in Table: Course_AdobeAbility
        //public int Id_Course_Adobe_Ability { set; get; }
        public int Id_CourseClass { set; get; }
        public int Id_AdobeAbility { set; get; }
        public List<long> List_Id_AdobeAbility { set; get; }



        // in Tbl_Users
        public string Name{set;get;}
        public string Family{set;get;}
        public string LatinName{set;get;}
        public string LatinFamily{set;get;}
        public string NationalID{set;get;}
        public string Mobile{set;get;}
        public string EmailAddress{set;get;}
        public string UserName{set;get;}
        public string Password{set;get;}
        public int TypeAccount{set;get;}







    }
}
