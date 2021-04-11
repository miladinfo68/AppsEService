using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Request
{
    public class PaymentRequest
    {
        public long OrderID { get; set; }
        public long RequestId { get; set; }
        public long AmountTrans { get; set; }
        public string StudentCode { get; set; }
        public string AppStatus { get; set; }
        public string fullname { get; set; }
    }

    public enum PayType
    {
        all=0,
        govahiEshteghal=1,
        studentCard=2,
        stamp=3
    }
}
