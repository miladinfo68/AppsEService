using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Request
{
    public class CheckOutPajoheshDTO
    {
        public int CheckOutPajooheshID { get; set; }

        public int StudentRequestID { get; set; }

        public string StCode { get; set; }

        public string Def_Date { get; set; }

        public string Def_Point { get; set; }

        public string Date_Paper_Cancel { get; set; }

        public bool HasPaper { get; set; }

        public string Date_Recieve_Doc_Accept { get; set; }

       public string  Date_Send_Doc_Edu { get; set; }

        public bool IsFinalize { get; set; }

        public bool IsArchive { get; set; }

        public bool IsDeleted { get; set; }

   

        public bool HasCancelForm { get; set; }
      public string DeadLineDate  { get; set; }
      public string  EditThes { get; set; }
    }
}
