using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.ResourceControlClasses
{
   public class AccessStudentOperationModel
    {
        public long id { get; set; }
        public string StudentCode { get; set; }
        public string StudentName { get; set; }
        public string Term { get; set; }
        public bool FlagAllowSelectUnit  { get; set; }
        public bool FlagAllowFinancial { get; set; }
    }
}
