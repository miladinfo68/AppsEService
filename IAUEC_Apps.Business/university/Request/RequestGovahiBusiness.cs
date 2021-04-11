using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using IAUEC_Apps.DAO.University.Request;
using IAUEC_Apps.DTO.University.Request;

namespace IAUEC_Apps.Business.university.Request
{

    public class RequestGovahiBusiness
    {
        /// <summary>
        ///ایجاد می نماید Request_GovahiDAO یک شئ از کلاس
        /// </summary>
        Request_GovahiDAO GovahiDAO = new Request_GovahiDAO();
        /// <summary>
        ///ایجاد می نماید Request_StudentCartDAO یک شئ از کلاس
        /// </summary>
        Request_StudentCartDAO CartDAO = new Request_StudentCartDAO();

        public DataTable getMashmulStatus(string stCode)
        {
            return GovahiDAO.getMashmulStatus(stCode);
        }

        public void DeleteGovahiRequest(int ReqId, int PaymentId)
        {
            GovahiDAO.DeleteGovahiRequest(ReqId, PaymentId);
        }

        public void UpdatePymentDetail(long OrderID, string Date, int PaymentID, int Amount, string RequestKey)
        {
            GovahiDAO.UpdatePymentDetail(OrderID, Date, PaymentID, Amount, RequestKey);
        }

        public DataTable GetAmountForPay(string stcode)
        {
            return GovahiDAO.GetAmountForPay(stcode);
        }
        public bool CanPay(string stcode)
        {
            return GovahiDAO.CanPay(stcode);
        }


        public void UpdateEraeBe(string EraeBe, int StudentRequestID)
        {
            GovahiDAO.UpdateEraeBe(EraeBe, StudentRequestID);
        }

        public DataTable GetMojazGovahi(string stcode)
        {
            return GovahiDAO.GetMojazGovahi(stcode);
        }

        public DataTable GetPayment(int RequestTypeID)
        {
            return GovahiDAO.GetPayment(RequestTypeID);
        }
        public DataTable GetAcceptedGovahiReport(int RequestTypeID)
        {
            return GovahiDAO.GetAcceptedGovahiReport(RequestTypeID);
        }
        public DataTable GetGovahiReport(int RequestLogID)
        {
            return GovahiDAO.GetGovahiReport(RequestLogID);
        }


        //public void InsertMashmulNumber(string MashmulNumber,int StudentRequestID, string MashmulTarikh)
        //{
        //    GovahiDAO.InsertMashmulNumber(MashmulNumber, StudentRequestID,MashmulTarikh);
        //}

        public void DeleteFromStudentReqByReqID(int RequestID)
        {
            GovahiDAO.DeleteFromStudentReqByReqID(RequestID);
        }
        public DataTable GetRequestByRequestID(int StudentrequestID)
        {
            return GovahiDAO.GetRequestByRequestID(StudentrequestID);
        }

        /// <summary>
        ///بر اساس کد دانشجویی، بدهی دانشجو را استخراج می نماید
        /// </summary>
        ///  <param name="stcode">شماره دانشجویی</param>
        ///  <returns>مبلغ بدهی</returns>
        public DataTable GetBedehkar(string stcode)
        {
            return GovahiDAO.GetBedehkar(stcode);
        }
        /// <summary>
        /// بررسی می کند که دانشجو در این ترم ثبت نام داشته یا نه
        /// </summary>
        /// <param name="stcode">شماره دانشجویی</param>
        ///  <returns>شماره دانشجویی،ترم جاری</returns>
        public DataTable GetStRegisterd(string stcode)
        {
            return GovahiDAO.GetStRegisterd(stcode);
        }
        /// <summary>
        /// بررسی می کند که دانشجو در این ترم ثبت نام داشته یا نه
        /// </summary>
        /// <param name="stcode">شماره دانشجویی</param>
        ///  <returns>شماره دانشجویی،ترم جاری</returns>
        public DataTable GetStRegisterd2(string stcode)
        {
            return GovahiDAO.GetStRegisterd2(stcode);
        }


        /// <summary>
        /// وضعیت درخواست گواهی را از دیتابیس استخراج می نماید
        /// </summary>
        /// <param name="stcode">The stcode.</param>
        ///<returns> شماره دانشجویی,وضعیت درخواست,نام ارائه ,شماره مرسوله پستی,شرح 
        ///</returns>
        public DataTable GetGovahiStatus(string stcode)
        {
            return GovahiDAO.GetGovahiStatus(stcode);
        }

        /// <summary>
        ///ثبت می نماید fgovahi اطلاعات درخواست گواهی را در جدول
        /// </summary>
        /// <param name="stcode">شماره دانشجویی</param>
        /// <param name="date_sabt">تاریخ ثبت درخواست</param>
        /// <param name="time_sabt">زمان ثبت درخواست</param>
        /// <param name="sharh_ara_bekoja">محلی که گواهی برای آن درخواست می گردد </param>
        /// <param name="TypeGovahi">نوع گواهی که در اینجا گواهی اشتغال به تحصیل می باشد</param>
        public void InsertGovahiIntoStrequest(string stcode, string date_sabt, string time_sabt, string sharh_ara_bekoja, int TypeGovahi)
        {
            GovahiDAO.InsertGovahiIntoStrequest(stcode, date_sabt, time_sabt, sharh_ara_bekoja, TypeGovahi);
        }
        /// <summary>
        /// این متد درخواست های گواهی را بر می گرداند
        /// </summary>
        ///    <returns> شماره دانشجویی,نام,نام خانوادگی,شناسه درخواست,شناسه وضعیت درخواست,رشته,سال ورود,شماره مرسوله پستی,شناسه نوع درخواست
        ///</returns>
        public DataTable GetGovahiRequest(int RequestLogId1, int RequestLogId2)
        {
            return GovahiDAO.GetGovahiRequest(RequestLogId1, RequestLogId2);
        }
        /// <summary>
        /// این متد کد رهگیری مرسوله پستی را در دیتابیس ذخیره می نماید
        /// </summary>
        /// <param name="stcode">شماره دانشجویی</param>
        /// <param name="PostNumber">کد مرسوله پستی</param>
        /// <param name="RequestTypeID">شناسه نوع درخواست</param>
        public void UpdateStudentPOstNumber(string stcode, string PostNumber, int RequestTypeID, int StudentRequestID)
        {
            CartDAO.UpdateStudentPOstNumber(stcode, PostNumber, RequestTypeID, StudentRequestID);
        }


        public void UpdatePayment(int currentRequestId, int newRequestId)
        {
            CartDAO.UpdatePayment(currentRequestId, newRequestId);
        }

        public void UpdateAmount(int currentRequestId, int newRequestId)
        {
            CartDAO.UpdateAmount(currentRequestId, newRequestId);
        }


        public List<GovahiRequest> PaymentByPastPayments(string listOfRequest, string stCode)
        {
            return GovahiDAO.PaymentByPastPayments(listOfRequest, stCode);
        }
        public void UpdateRollBackingPastPayment(int currentReqId, int trasferReqId,int fourteenReqId)
        {
             GovahiDAO.UpdateRollBackingPastPayment(currentReqId, trasferReqId, fourteenReqId);
        }
        public List<GovahiRequest> PaymentStatus(string listOfRequest, string stCode)
        {
            return GovahiDAO.PaymentStatus(listOfRequest, stCode);
        }

    }
}
