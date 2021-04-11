using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using IAUEC_Apps.DAO.University.Request;

namespace IAUEC_Apps.Business.university.Request
{
    public class RequestStudentCartBusiness
    {
        /// <summary>
        ///ایجاد می نماید Request_StudentCartDAO یک شئ از کلاس 
        /// </summary>
        Request_StudentCartDAO CartDAO = new Request_StudentCartDAO();
        /// <summary>
        /// کد پستی باید به صورت عدد و 10 رقم باشد
        /// </summary>
        private const string regexZipCode = @"^[1-9][0-9]{9}$";


        /// <summary> این متد یک عدد رندوم از بازه تعیین شده تولید می نماید
        /// </summary>
        ///  <param name="min">شروع بازه</param>
        /// <param name="max">پایان بازه</param>
        /// <returns>عدد تصادفی</returns>
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        /// <summary>
        /// این متد تاریخ را به صورت شمسی وارد می نماید
        /// </summary>
        ///  <returns>تاریخ</returns>
        public string PersianCalander()
        {
            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
            string date = ((pc.GetYear(DateTime.Now)).ToString()).Substring(2, 2) + "/" + pc.GetMonth(DateTime.Now) + "/" + pc.GetDayOfMonth(DateTime.Now);

            return date;
        }
        /// <summary>
        /// این متد اعتبار کدپستی را بررسی می کند 
        /// </summary>
        ///  <param name="str">کدپستی</param>
        ///  <returns>پیغام</returns>
        public static bool ValidateZipCode(string str)
        {
            return new Regex(regexZipCode).IsMatch(str);
        }
        /// <summary>
        /// این متد بررسی می کند که دانشجو قبلا درخواست ارسال کارت داشته یا خیر 
        /// </summary>
        ///<param name="stcode">شماره دانشجویی</param>
        /// <returns>شماره دانشجویی</returns>
        public DataTable GetstHasCartRequest(string stcode)
        {
            return CartDAO.GetstHasCartReq(stcode);
        }

        /// <summary>
        /// این متد وضعیت درخواست کارت دانشجو را نمایش می دهد
        /// </summary>
        /// <param name="stcode">شماره دانشجویی</param>
        ///  <returns>شماره دانشجویی</returns>
        public DataTable GetRequestsStatus(string stcode)
        {
            return CartDAO.GetRequestStatus(stcode);
        }
        /// <summary>
        /// این متد اطلاعات دانشجو را بر می گرداند
        /// </summary>
        /// <param name="stcode">شماره دانشجویی</param>
        ///  <returns>نام،نام خانوادگی،شماره دانشجویی</returns>
        public DataTable GetStudentsInfo(string stcode)
        {
            return CartDAO.GetStudentInfo(stcode);
        }
        public DataTable GetStudentChildren(string stcode)
        {
            return CartDAO.GetStudentChildren(stcode);
        }
        public DataTable GetStudentsInfoAmoozeshyar(string stcode)
        {
            return CartDAO.GetStudentInfoAmoozeshyar(stcode);
        }
        /// <summary>
        /// این متد درخواست دانشجو را در دیتابیس ثبت می نماید
        /// </summary>
        /// <param name="stcode">شماره دانشجویی</param>
        ///  <param name="RequestTypeID">شناسه نوع درخواست</param>
        /// <param name="RequestLogID">شناسه وضعیت درخواست</param>
        public int InsertInToStudentRequest(string stcode, int RequestTypeID, int RequestLogID, string Erae_Be, string MashmulNumber, int isOnline)
        {
            int reqId = int.Parse(CartDAO.InsertInToStudentRequest(stcode, RequestTypeID, RequestLogID, Erae_Be, MashmulNumber, isOnline).ToString());
            return reqId;
        }
        public bool CheckStCodeIsExistInStudentRequest(string stcode, int RequestTypeID, int RequestLogID)
        {
            return CartDAO.CheckStCodeIsExistInStudentRequest(stcode, RequestTypeID, RequestLogID);
        }
        /// <summary>
        /// این متد تاریخچه عملیاتی که توسط دانشجو انجام شده است را در دیتابیس ثبت می نماید
        /// </summary>
        /// <param name="stcode">شماره دانشجویی</param>
        /// <param name="LogDate">تاریخ عملیات</param>
        /// <param name="LogTime">زمان عملیات</param>
        ///<param name="LogTypeID">شناسه نوع عملیات</param>
        public void InsertInToStudentLog(string stcode, DateTime LogDate, string LogTime, int LogTypeID)
        {
            CartDAO.InsertInToStudentLog(stcode, LogDate, LogTime, LogTypeID);
        }
        /// <summary>
        /// این متد آدرس دانشجو را بروزرسانی می نماید
        /// </summary>
        /// <param name="stcode">شماره دانشجویی</param>
        ///   <param name="newtel">شماره تلفن جدید.</param>
        ///  <param name="newaddress">آدرس جدید</param>
        ///  <param name="codeposti">کدپستی</param>
        public void UpdateAddres(string stcode, string newtel, string newaddress, string codeposti)
        {
            CartDAO.UpdateAddres(stcode, newtel, newaddress, codeposti);
        }
        /// <summary>
        /// این متد درخواست ارسال کارت یک دانشجو را بر می گرداند
        /// </summary>
        ///<returns>شماره دانشجویی,شناسه درخواست دانشجو, نام,نام خانوادگی,نام رشته,سال ورود,شناسه نوع درخواست 
        ///</returns>
        public DataTable GetCartRequest(int RequestLogID)
        {
            return CartDAO.GetCartRequest(RequestLogID);
        }
        /// <summary>
        /// وضعیت درخواست نرم افزار را بروزرسانی می نماید
        /// </summary>
        ///<param name="stcode">شماره دانشجویی</param>
        ///    <param name="RequestLogID">شناسه وضعیت درخواست</param>
        ///   <param name="RequestTypeID">شناسه نوع درخواست</param>
        ///   <param name="StudentRequestID">شناسه درخواست دانشجو</param>
        //public void updateStudentRequestLogID(string stcode, int RequestLogID, int RequestTypeID, int StudentRequestID)
        // {
        //     CartDAO.UpdateStudentRequestLogID(stcode,RequestLogID,RequestTypeID,StudentRequestID);
        // }
        /// <summary>
        /// Updates the student request log identifier.
        /// </summary>
        ///  <param name="stcode">شماره دانشجویی</param>
        /// <param name="RequestLogID">شناسه وضعیت درخواست</param>
        /// <param name="RequestTypeID">شناسه نوع درخواست</param>
        /// <param name="StudentRequestID">شناسه درخواست دانشجو</param>
        public void UpdateStudentRequestLogID(string stcode, int RequestLogID, int RequestTypeID, int StudentRequestID)
        {
            CartDAO.UpdateStudentRequestLogID(stcode, RequestLogID, RequestTypeID, StudentRequestID);
        }
        //
        //public void InsertIntoUserLog(int UserID, DateTime LogDate, string LogTime, string StCode, int LogTypeID)
        //{
        //    CartDAO.InsertIntoUserLog(UserID,LogDate,LogTime,StCode,LogTypeID);
        //}
        //        

        /// <summary>
        ///درج می نماید payinterm این متد مبلغ 2000 تومان در جدول 
        /// </summary>
        /// <param name="stcode">شماره دانشجویی</param>
        ///   <param name="serial_pay">شماره سریال پرداخت</param>
        ///  <param name="idtypepay">نوع پرداخت</param>
        ///    <param name="amount">مقدار</param>
        ///   <param name="datefish">تاریخ فیش</param>
        ///     <param name="datesabt">تاریخ ثبت</param>
        public void InsertIntoPayinterm(string stcode, int serial_pay, int idtypepay, decimal amount, string datefish, string datesabt)
        {
            CartDAO.InsertIntoPayinterm(stcode, serial_pay, idtypepay, amount, datefish, datesabt);
        }
        /// <summary>
        /// بروزرسانی می نمایدfsf2 را در جدول addressm فیلد 
        /// </summary>
        /// <param name="stcode">شماره دانشجویی</param>
        public void UpdateAddressm(string stcode)
        {
            CartDAO.UpdateAddressm(stcode);
        }

        public void DeleteCard(string stcode)
        {
            CartDAO.DeleteCard(stcode);
        }

        public DataTable Get_StudentAddress(string stcode, int param)
        {
            return CartDAO.Get_StudentAddress(stcode, param);
        }

        public bool CheckOrderId(int serial_pay, int idtypepay)
        {
            return CartDAO.CheckOrderId(serial_pay, idtypepay);

        }

        public bool CheckEraeBeExist(string stcode, int RequestTypeID, int RequestLogID, string Erae_Be)
        {
            return CartDAO.CheckEraeBeExist(stcode, RequestTypeID, RequestLogID, Erae_Be);
        }
    }
}
