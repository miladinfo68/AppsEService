using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.AdobeClasses
{
   public class DownloadRequestDTO
    {
       public long RequestID { get; set; }
       public string StCode { get; set; }
       public string Class_Code { get; set; }
       public string TimeCode { get; set; }
       public string PayId { get; set; }
       public bool Link_Click { get; set; }
       public string URL_Link { get; set; }
       public long OrderID { get; set; }
      //  public string Term { get; set; }

    }
}
