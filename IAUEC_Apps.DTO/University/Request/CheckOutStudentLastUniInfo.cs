using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Request
{
    public class CheckOutStLastUniInfo
    {
        public string StCode { get; set; }
        public int TypeOfUni { get; set; }
        public string UniName { get; set; }
        public string FieldName { get; set; }
        public int Maghta { get; set; }
        public string FeraghatYear { get; set; }
        public int FeraghatHalfYear { get; set; }
        public string FeraghatDate { get; set; }
        public int FeraghatType { get; set; }
        public int CheckOutStatus { get; set; }
        public string LoanAmount { get; set; }
        public int IsMashmool { get; set; }
        public int ProvinceID { get; set; }
        public int CityID { get; set; }
        public string Street { get; set; }
        public string Alley { get; set; }
        public string Pelak { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string RabetPhone { get; set; }
        public string RabetMobile { get; set; }
        public string ScanMadarekURL { get; set; }
    }
}
