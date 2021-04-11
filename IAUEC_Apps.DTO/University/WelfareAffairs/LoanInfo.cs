using System;
using System.Collections.Generic;

namespace IAUEC_Apps.DTO.University.WelfareAffairs
{
    public class LoanInfo
    {
        public int LoanId { get; set; }
        public string StudentCode { get; set; }
        public string StudentFirstname { get; set; }
        public string StudentLastname { get; set; }
        public byte LoanType { get; set; }
        public string LoanTypeTitle { get; set; }
        public byte Status { get; set; }
        public string Term { get; set; }
        public DateTime ReqRegisterDate { get; set; }
        public List<LoanDocInfoDTO> LoanDocs { get; set; }
        public string Message { get; set; }
    }
}
