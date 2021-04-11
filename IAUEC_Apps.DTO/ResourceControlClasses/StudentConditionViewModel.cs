using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.ResourceControlClasses
{
    public class StudentConditionViewModel
    {
        public StudentConditionViewModel()
        {

        }

        public StudentConditionViewModel(int stateNum, int percentProgressRequest, string percentProgressRequestDegree, int dayAmontRequest, int percentDayRequest, string stateText, bool isDeleted)
        {
            StateNum = stateNum;
            PercentProgressRequest = percentProgressRequest;
            PercentProgressRequestDegree = percentProgressRequestDegree;
            DayAmontRequest = dayAmontRequest;
            PercentDayRequest = percentDayRequest;
            StateText = stateText;
            IsDeleted = isDeleted;
        }
        public int StateNum { get; set; }
        public int PercentProgressRequest { get; set; }
        public string PercentProgressRequestDegree { get; set; }
        public int DayAmontRequest { get; set; }
        public int PercentDayRequest { get; set; }
        public string StateText { get; set; }
        public bool IsDeleted { get; set; }
    }
}
