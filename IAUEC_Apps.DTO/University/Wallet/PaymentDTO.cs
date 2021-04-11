using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Wallet
{
    public class PaymentDTO
    {
      public decimal Id { get; set; }
      public decimal OrderId { get; set; }
      public decimal? Result { get; set; }
      public string RetrivalRefNo { get; set; }
      public string RequestKey { get; set; }
      public decimal Amount { get; set; }
      public int Status { get; set; }
      public decimal BankId { get; set; }
      public decimal? TraceNumber { get; set; }
      public DateTime CreateDate { get; set; }
      public int PayType { get; set; }
      public string stcode { get; set; }
    }
}
