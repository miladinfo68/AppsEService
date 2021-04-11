using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Request
{
    public class StudentCheckOutDebit
    {
        public long RefID { get; set; }

        public string StCode { get; set; }

        public int DebitTypeID { get; set; }

        public string DebitAmount { get; set; }

        public string TotalLoanAmount { get; set; }

        public string DebitNumber { get; set; }

        public string Note { get; set; }

        public string FishNumber { get; set; }

        public string FishDate { get; set; }
    }
}
