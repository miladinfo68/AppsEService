using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.WelfareAffairs
{
    public class PaymentRecord
    {
        public string Term { get; set; }
        public int Debt { get; set; }
        public int Deposit { get; set; }
        public int DebtAmount { get; set; }
        public string Status { get; set; }
    }
}
