using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Faculty
{
    public class ListHoursAndAbsenceDTO
    {
        public string Term { get; set; }
        public string Departman { get; set; }
        public string Daneshkade { get; set; }
        public string CodeOstad { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string AzJobrani { get; set; }
        public string TaJobrani { get; set; }
    }
}
