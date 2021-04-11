using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Request
{
    [Serializable]
   public class GovahiRequest
    {
        public int RowNumber { get; set; }
        public int Number { get; set; }
        public int AmountTrans { get; set; }
        public bool HasBeenPaid { get; set; }
        public int trasferRequestID { get; set; }
    }
}
