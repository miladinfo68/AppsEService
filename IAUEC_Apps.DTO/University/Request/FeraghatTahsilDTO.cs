using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DTO.University.Request
{
    public class FeraghatTahsilDTO
    {
        public int Id { get; set; }

        public int RizNomarat { get; set; }
        public string DateRizNomarat { get; set; }

        public int GovahiMovaghat { get; set; }
        public string DateGovahiMovaghat { get; set; }

        public int DaneshNameh { get; set; }
        public string DateDaneshNameh { get; set; }

        public string Stcode { get; set; }

        public string family { get; set; }

        public int StudentRequestId { get; set; }

        public bool sendRiz { get; set; }

        public bool sendGovahi { get; set; }

        public bool sendDanesh { get; set; }
        public string dateVoroodGovahi { get; set; }
        public string dateVoroodRizNomre { get; set; }
        public string dateVoroodDaneshname { get; set; }
        public string dateSodoorDaneshname { get; set; }
        public string dateSodoorGovahi { get; set; }
        public string dateSodoorRizNomre { get; set; }
        public string dateErsalRizNomre { get; set; }
        public Int64 archiveCode_movaghat { get; set; }
        public Int64 archiveCode_daneshname { get; set; }
        public Int64 archiveCode_rizNomre { get; set; }

    }


    public class MahaleSodoor
    {
        public int Id { get; set; }

        public string Vahed { get; set; }
    }
    public class Field
    {
        public int Id { get; set; }

        public string field { get; set; }
    }
    public class Daneshkade
    {
        public int Id { get; set; }

        public string daneshkade { get; set; }
    }

    public class allAcceptedRequest
    {
        public long StudentRequestID { get; set; }
        public string CreateDate { get; set; }
        public string StCode { get; set; }
        public string name { get; set; }

        public string DateVoroodRizNomre { get; set; }
        public string DateSodoorRizNomre { get; set; }

        public string DateVoroodDaneshname { get; set; }
        public string DateSodoorDaneshname { get; set; }

        public string DateVoroodGovahi { get; set; }
        public string DateSodoorGovahi { get; set; }

        public decimal Shahriye { get; set; }
        public decimal Takhfif { get; set; }
        public decimal enteghalBestankar { get; set; }
        public decimal Pay { get; set; }
        public string status { get { return "اسامي بر اساس گزارش سيستم مورد تاييد مي باشد"; } }


    }
    public class allCases
    {

        public string stcode { get; set; }
        public string name { get; set; }
        public long StudentRequestID { get; set; }
        public string nameresh { get; set; }
        public string namedanesh { get; set; }
        public string vahed { get; set; }
        public string sodoormadrak { get; set; }
        public string voroodmadrak { get; set; }
        public string madrakdeliver { get; set; }


    }
    public class ListReqVahed
    {
        public long StudentRequestID { get; set; }
        public string CreateDate { get; set; }
        public string StCode { get; set; }
        public long RequestLogID { get; set; }
        public string name { get; set; }
        public long RequestTypeID { get; set; }
        public string RequestTypeName { get; set; }
        public string Note { get; set; }
        public bool IsPrinted { get; set; }
        public string message { get; set; }
        public string StudentMessage { get; set; }
        public int IdVahedSodoor { get; set; }
        public string DateVoroodRizNomre { get; set; }
        public string DateSodoorRizNomre { get; set; }

        public string DateVoroodDaneshname { get; set; }
        public string DateSodoorDaneshname { get; set; }

        public string DateVoroodGovahi { get; set; }
        public string DateSodoorGovahi { get; set; }
        public string vahed { get; set; }
        public string TypeName { get; set; }

        public bool PaiedToVahedSodoor { get; set; }
        public string DateSendToPay { get; set; }
        public string DateApprovePay { get; set; }
        public decimal shahriye { get; set; }
        public decimal takhfif { get; set; }
        public decimal pay { get; set; }
        public decimal enteghalBestankar { get; set; }


    }
    public class ApproveList
    {
        public int Id { get; set; }
        public long StudentRequestId { get; set; }
        public string ApproveDateFaregh { get; set; }
        public string ApproveDateMali { get; set; }

    }



}
