using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.EmailClasses
{
    public class Email_ConnectDTO
    {
        public string SmsText { get; set; }
        public string EmailText { get; set; }
        public int StatusId { get; set; }
    }
}
