using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.ResourceControlClasses
{
   public class StatusDefenceTechnichal
    {
        public int RequestId { get; set; }
        public int StudentCode { get; set; }
        public bool? FlagAcceptTechnicalDavOut { get; set; }
        public String DateReasonTechnicalDavOut { get; set; }
        public String ReasonTechnicalDavOut { get; set; }
        public bool? FlagAcceptTechnicalDavIn { get; set; }
        public String DateReasonTechnicalDavin { get; set; }
        public String ReasonTechnicalDavin { get; set; }
        public bool? FlagAcceptTechnicalMosh1 { get; set; }
        public String DateReasonTechnicalMosh1 { get; set; }
        public String ReasonTechnicalMosh1 { get; set; }
        public bool? FlagAcceptTechnicalMosh2 { get; set; }
        public String DateReasonTechnicalMosh2 { get; set; }
        public String ReasonTechnicalMosh2 { get; set; }
        public bool? FlagAcceptTechnicalRah1 { get; set; }
        public String DateReasonTechnicalRah1 { get; set; }
        public String ReasonTechnicalRah1 { get; set; }
        public bool? FlagAcceptTechnicalRah2 { get; set; }
        public String DateReasonTechnicalRah2 { get; set; }
        public String ReasonTechnicalRah2 { get; set; }
        public bool? FlagAcceptTechnicalStudent { get; set; }
        public String DateReasonTechnicalStudent { get; set; }
        public String ReasonTechnicalStudent { get; set; }


    }
}
