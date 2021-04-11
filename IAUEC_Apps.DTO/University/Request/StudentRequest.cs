using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Request
{
    public class StudentRequest
    {
        public int NaghsId { get; set; }

        public int StudentRequestId { get; set; }

        public string StCode { get; set; }

        public string Erae_Be { get; set; }

        public int RequestLogId { get; set; }

        public bool HasStamp { get; set; }
    }
}
