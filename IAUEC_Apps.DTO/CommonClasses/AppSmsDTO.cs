using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.CommonClasses
{
    public class AppSmsDTO
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int CategoryId { get; set; }
        public bool SendNotif { get; set; }
        public string[] Receivers { get; set; }
        public string Link { get; set; }
        public string VideoLink { get; set; }
    }
    public class ResponceAppSms
    {
        public int ErrorId { get; set; }
        public string ErrorTitle { get; set; }
        public string Result { get; set; }
    }
}
