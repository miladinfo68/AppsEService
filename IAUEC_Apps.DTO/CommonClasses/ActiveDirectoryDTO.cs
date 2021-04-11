using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.CommonClasses
{
    public class ActiveDirectoryDTO
    {
        public string SamAccountName {set;get;}
        public string Uid {set;get;}
        public string GivenName {set;get;}
        public string Description {set;get;}
        public string Company {set;get;}
        public string DisplayName {set;get;}
        public string Name {set;get;}
        public string Password {set;get;}
        public string Department { get; set; }
        public string sn { get; set; }
        public int FieldCode { get; set; }

 
    }
}
