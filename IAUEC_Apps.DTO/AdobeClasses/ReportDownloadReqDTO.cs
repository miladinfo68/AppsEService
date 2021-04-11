using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.AdobeClasses
{
   public class ReportDownloadReqDTO
    {
        public double AssetID { get; set; }
        public string Class_Code { get; set; }
        public string URL_File { get; set; }
        public string Term { get; set; }
        public string Session { get; set; }
        public int Fee { get; set; }
        public double FileSize { get; set; }
        public string FileName { get; set; }
        public string FileTime { get; set; }
        public string FileDate { get; set; }

        public double RequestID { get; set; }
        public string StCode { get; set; }
        public string AssetClassCode { get; set; }
        public string Namedars { get; set; }
        public string TimeCode { get; set; }
        public string PayId { get; set; }
        public bool Link_Click { get; set; }
        public string URL_Link { get; set; }
        public string RowId { get; set; }
        public string SaveDate { get; set; }
    }
}
