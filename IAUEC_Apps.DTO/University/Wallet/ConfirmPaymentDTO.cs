using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Wallet
{
    public class ConfirmPaymentDTO
    {
        public bool Result { get; set; }
        public string stcode { get; set; }
        public string Message { get; set; }
    }
}
