using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.WelfareAffairs
{
    [Serializable]
    public class DigitParvande
    {
        public string StCode { get; set; }
        public string Ext { get; set; }
        public byte[] Pic { get; set; }
        public decimal Cat { get; set; }
        public string DateSend { get; set; }
        public string TimeSend { get; set; }
        public string Base64Image { get; set; }
        public string CategoryName { get; set; }
    }
}
