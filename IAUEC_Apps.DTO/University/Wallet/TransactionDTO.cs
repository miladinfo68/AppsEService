using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Wallet
{
    public class TransactionDTO
    {
      public decimal Id { get; set; }
      public string stcode { get; set; }
      public decimal TransactionTypeId { get; set; }
      public string Description { get; set; }
      public decimal CurrentBalance { get; set; }
      public DateTime CreateDate { get; set; }
      public decimal Amount { get; set; }
    }
}
