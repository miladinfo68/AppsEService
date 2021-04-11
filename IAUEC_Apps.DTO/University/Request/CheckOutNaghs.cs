using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Request
{
    public class CheckOutNaghsDTO
    {
        public int NaghsId { get; set; }

        public int StudentRequestId { get; set; }

        public string StCode { get; set; }

        public string Erae_Be { get; set; }

        public int RequestLogId { get; set; }

        public string SubmitDate { get; set; }

        public string NaghsMessage { get; set; }

        public string ResolveDate { get; set; }

        public string ResolveMessage { get; set; }

        public bool IsResolved { get; set; }

        public bool IsDeleted { get; set; }
    }
}
