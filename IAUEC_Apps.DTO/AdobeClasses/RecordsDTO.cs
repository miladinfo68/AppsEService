using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.AdobeClasses
{
  public class RecordsDTO
    {
        public int AssetID { get; set; }
        public string LessonCode { get; set; }
        public string ClassCode { get; set; }
        public string Size { get; set; }
        public string Duration { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string Date { get; set; }
        public string Session { get; set; }
        public string Link { get; set; }
    }
}
