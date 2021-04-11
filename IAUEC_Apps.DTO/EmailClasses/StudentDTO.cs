using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.EmailClasses
{
    public class StudentDTO
    {
       public string stcode { get; set; }
       public string name { get; set; }
       public string family { get; set; }
       public string namep { get; set; }
       public string idd { get; set; }
       public string idd_Meli { get; set; }
       public short sex { get; set; }
       public short dorpar { get; set; }
       public string magh { get; set; }
      
       public string sal_vorod { get; set; }
       public decimal idvazkol { get; set; }
       public double payed { get; set; }
       public string sal_mali { get; set; }
       public decimal idgara { get; set; }
       public string nameresh { get; set; }
       public decimal idpazeresh { get; set; }
       public decimal idresh { get; set; }
       public short nimsal_vorod { get; set; }
       public string password_stu { get; set; }
    }
}
