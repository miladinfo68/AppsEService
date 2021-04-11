using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.EmailClasses
{
   public class Email_Class
    {
        public int id { get; set; }
        public string Stcode { set; get; }
        public string Email_Address { set; get; }
        public string Password { set; get; }
        public DateTime Date_Save { set; get; }
        public string CEMAIL { set; get; }
        public int ConnectType { set; get; }
        public bool UpdateEmail { set; get; }
        public string Mobile { set; get; }
    }
}
