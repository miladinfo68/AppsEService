using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Graduate
{
    public class GraduateFormsDTO
    {
        public string stCode { get; set; }
        public string family { get; set; }
        public string iddMeli { get; set; }
    }

    public class StudentFeraghatDocument
    {
        public string stcode { get; set; }
        public int RequestID { get; set; }
        public int RequestStatus { get; set; }
        public string RequestStatus_String { get; set; }
        public string name { get; set; }
        public string family { get; set; }
        public string pName { get; set; }
        public string idd_meli { get; set; }
        public int countEnterStudentDep { get; set; }
        public string genderName { get; set; }
        public int genderValue { get; set; }
        public string reshte { get; set; }
        public string gerayesh { get; set; }
        public string maghta { get; set; }
        public string lastTerm { get; set; }
        public string mobile { get; set; }
        public bool vamdar { get; set; }
        public bool mashmul { get; set; }
        public string mashmulName { get; set; }
        public string vahedSodur { get; set; }
        public string dateSendPack { get; set; }
        public string dateGovahiVorud { get; set; }
        public string dateGovahiSodur { get; set; }
        public string dateGovahiTahvil { get; set; }
        public string dateDaneshnameVorud { get; set; }
        public string dateDaneshnameSodur { get; set; }
        public string dateDaneshnameTahvil { get; set; }
        public string dateRiznomreVorud { get; set; }
        public string dateRiznomreSodur { get; set; }
        public string dateRiznomreTahvil { get; set; }
        public string dateRiznomreErsal { get; set; }
        public string DateEnterStudentDep { get; set; }
        public string DateRejToDep { get; set; }
        public string ResultRejToDep { get; set; }
        public byte[] StudentImage { get; set; }
        public bool HasPaymentReceipt { get; set; }
        public bool HasStamp { get; set; }
        public string docArchiveID { get; set; }
        public Int64 archiveID_Movaghat { get; set; }
        public Int64 archiveID_Daneshname { get; set; }
        public Int64 archiveID_Riznomre { get; set; }
        public string serialNumber_Movaghat { get; set; }
        public string serialNumber_Daneshname { get; set; }
        public string documentNumber_Movaghat { get; set; }
        public string documentNumber_Daneshname { get; set; }
        public string ArchivesCode { get; set; }
        public bool PaiedToVahedSodoor { get; set; }
        public string SpecialTips { get; set; }
        public bool isOnline { get; set; }

    }

    public class Inquiry
    {
        public int inquiryID { get; set; }
        public string stcode { get; set; }
        public int inquiryType { get; set; }
        public string toPeresentTo { get; set; }
        public string letterNumber { get; set; }
        public string letterDate { get; set; }
        public string documentNumber { get; set; }
        public string documentAcceptDate { get; set; }
        public string serialNumber { get; set; }
        public string note { get; set; }
    }
}
