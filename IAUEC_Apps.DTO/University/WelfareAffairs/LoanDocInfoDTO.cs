using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.WelfareAffairs
{
    public class LoanDocInfoDTO
    {
        public int? DocId { get; set; }
        public int? LoanId { get; set; }
        public string DocPath { get; set; }
        public string DocName { get; set; }
        public string DocTitle { get; set; }
        public byte? DocType { get; set; }
        public byte? DocStatus { get; set; }      
        public string Description { get; set; }
    }
}
