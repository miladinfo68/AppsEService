using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace IAUEC_Apps.DTO.CommonClasses
{
    public class PaymentDTO
    {
        public long OrderId { get; set; }
        public long Amount { get; set; }
        public string tterm { get; set; }
        public string stcode { get; set; }
        public string ReqKey { get; set; }
        public int Result { get; set; }
        public string refId { get; set; }
        public string AppStatus { get; set; }
        public int bankId { get; set; }
        public DateTime MiladiDate { get; set; }
        public string PersianDate { get; set; }
        public string CardHolder { get; set; }
        public string CardNumber { get; set; }
        public int FailCode { get; set; }
        public string FailMessage { get; set; }
        public string Message { get; set; }
        public long TraceNumber { get; set; }
        public string numfish { get; set; }
        public DateTime TransMiladiDate { get; set; }
        public string TransPersianDate { get; set; }
        public string PayDate { get; set; }
        //سپیده حسینی-09-04-94
        /// <summary>
        /// نوع پرداخت را مشخص میکند برای سیستم گواهی عدد 1 درخواست گواهی و عدد 2 درخواست کارت دانشجویی
        /// </summary>
        public int PayType { get; set; }
        /// <summary>
        /// در صورتیکه نوع پرداخت درخواست گواهی باشد از جهت ارائه به این فیلد پر میشود
        /// </summary>
        public string Description { get; set; }
        //سپیده حسینی-14-04-94
        /// <summary>
        /// کد درخواست دانشجو در این فیلد ثبت می شود برای سیستم گواهی
        /// </summary>
        public int RequestId { get; set; }
        public long PaymentID { get; set; }
    }
}
