using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.ResourceControlClasses
{
    public class RefereeInformation
    {
        public int? RequestID { get; set; }
        public string StudentCode { get; set; }
        public string StudentFullName { get; set; }
        public string RefereeMobile { get; set; }
        public string RequestDate { get; set; }
        //public int CollegeId { get; set; }
        public string CollegeName { get; set; }
        public string RefereeFullName { get; set; }
        public string RefereeOrder { get; set; }//martabe
        public decimal? RefereeOrderValue { get; set; }//martabe
        public string RefereePayment { get; set; }
        public string RefereeSiba { get; set; }
        public bool? DefenceHasDone { get; set; }
        //public bool? PayedDefence { get; set; } = false;

        public string Term { get; set; }

        public string RefereeType { get; set; }
        public bool? ChkPaymentReferee1 { get; set; } = false;
        public bool? ChkPaymentReferee2 { get; set; } = false;
    }
}
