using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.ResourceControlClasses
{
   public class ScoreDefence
    {
        public int RequestId { get; set; }
        public string Stcode { set; get; }
        public string FullName { get; set; }
        public string RequestDate { set; get; }
        public decimal ? Score { get; set; }
        public string  ScoreLetters { set; get; }
        public bool ?FlagAcceptScoreDavin { get; set; }
        public bool ?FlagAcceptScoreDavOut { get; set; }
        public bool ?FlagAcceptScoreMosh1 { get; set; }
        public bool ?FlagAcceptScoreMosh2 { get; set; }
        public bool ?FlagAcceptScoreRah1 { get; set; }
        public bool ?FlagAcceptScoreRah2 { get; set; }
    }
}
