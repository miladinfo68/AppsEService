using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.AdobeClasses
{
    [Serializable()]
    public  class ClassListDTO
    {
        public string CourseCode { get; set; }
        public string CourseName{get;set;}
        public string ClassCode { get; set; }
        public string ClassName { get; set; }
        public string ClassDateTime { get; set; }
        public int ClassDay { get; set; }
        public string ClassStartTime { get; set; }
        public string ClassEndTime { get; set; }
        public Int64 ProfID { get; set; }
        public string ProfName { get; set; }
        public string MergeCode { get; set; }
        public string Semester { get; set; }
        public int SessionCount { get; set; }
        public string FirstSession { get; set; }
        public string namedars { get; set; }
        public string Ost_Name { get; set; }
        public string Klas_Day { get; set; }
        public string ClassTime { get; set; }
 public int ClassCount { get; set; }


    }
}
