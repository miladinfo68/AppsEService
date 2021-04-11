using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Support
{
    public class PassProfessorDTO
    {
        public string code { get; set; }
        public string name { get; set; }
        public string family { get; set; }
        public string mobile { get; set; }
        public string add_email { get; set; }
        public string password { get; set; }
        public string msg { get; set; }
        public string IDAppMessage { get; set; }
        public string to { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string TypeSend { get; set; }
        public string Daneshkade { get; set; }
        public string Departman { get; set; }
        public string Term { get; set; }

        public enum ostadType
        {
            استاد_پژوهشی = 4,
            آموزشی = 1
        }

        public enum enumTypeSend
        {
            ارسال_رمز_خدمات = 1,
            ارسال_ثبت_نمرات = 2,
            ارسال_شرکت_در_کلاس_امتحانات = 3,
            ارسال_رمز_پورتال_پژوهش = 4
        }
    }
}
