using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.CommonClasses
{
    public class PaymentRequest
    {
        public int RequestID { get; set; }
        public long OrderID { get; set; }
    }
}
