using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DTO.University.Request;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DAO.University.Request;


namespace IAUEC_Apps.Business.university.Request
{
    public class EditPersonalInformationBusiness
    {

        RequestStudentEditInfDAO EditDAO = new RequestStudentEditInfDAO();


        public void insertCreateDateRequest(string stcode, string createdate, int EventId)
        {
            EditDAO.insertCreateDateRequest(stcode, createdate, EventId);
        }
        public DataTable getStudentInfoFromInitialRegistration(string stcode)
        {
            var dt= EditDAO.getStudentInfoFromInitialRegistration(stcode);
            if(dt.Rows.Count==0)
                dt= EditDAO.getStudentInfoFromInitialRegistration_International(stcode);
            return dt;
        }

        public void updateStudentInfo(string stcode, int editPersonalID)
        {
            EditDAO.updateStudentInfo(stcode, editPersonalID);
        }
        public void updateStudentChild(string stcode, int editPersonalID,int childID=0)
        {
            EditDAO.updateStudentChild(stcode, editPersonalID,childID);
        }
        public void insertStudentChild(string stcode, int editPersonalID_name,  int editPersonalID_family, int editPersonalID_gender, int editPersonalID_birthday)
        {
            EditDAO.insertStudentChild(stcode,  editPersonalID_name,  editPersonalID_family, editPersonalID_gender,  editPersonalID_birthday);
        }

        public DataTable getallrequest(int requesttype)
        {
            return EditDAO.getallrequest(requesttype);

        }
        public DataTable GetMaxEvents(int EventID, string stcode)
        {
            return EditDAO.GetMaxEvents(EventID, stcode);
        }
        public void UpdatePhotoinSida(byte[] stu_pic, string stcode)
        {
            EditDAO.UpdatePhotoinSida(stu_pic, stcode);
        }

        public void UpdateimageRequest(byte[] personalimage, int StudentRequestID)
        {
            EditDAO.UpdateimageRequest(personalimage, StudentRequestID);

        }

        public DataTable PixChangeRequestedEdit()
        {
            return EditDAO.PixChangeRequestedEdit();
        }

        public DataTable GetReportEditImageRequest(int RequestLogID)
        {
            return EditDAO.GetReportEditImageRequest(RequestLogID);
        }
        public DataTable GetReportEditRequest(int RequestLogID)
        {
            return EditDAO.GetReportEditRequest(RequestLogID);
        }



        /// <summary> این متد اطلاعات دانشجو را از دیتابیس می خواند
        ///</summary>
        ///<param name="stcode">شماره دانشجویی</param>
        /// <returns>آدرس،موبایل،استان،شهرستان،کدپستی،تلفن،عکس پرسنلی</returns>
        public DataTable GetStPersonalInf(string stcode)
        {
            return EditDAO.GetStPersonalInf(stcode);
        }

        /// <summary> درخواست های ویرایش را از دیتابیس می خواند
        /// </summary>
        /// <param name="stcode">شماره دانشجویی</param>
        /// <returns>شماره دانشجویی،نوع ویرایش،نوع درخواست،وضعیت درخواست،نوع ویرایش،توضیحات رد درخواست</returns>

        public DataTable GetStEditRequest(string stcode)
        {
            return EditDAO.GetStEditRequest(stcode);
        }
        /// <summary>درخواست ویرایش یک دانشجو را از دیتابیس می خواند
        ///</summary>
        /// <param name="stcode">شماره دانشجویی</param>
        /// <returns>شماره دانشجویی،نام،نام خانوادگی</returns>
        public DataTable GetStcodesEditeRequest(string stcode)
        {
            return EditDAO.GetStcodeEditeRequest(stcode);
        }
        /// <summary> این متد شناسه درخواست دانشجو را بر اساس شماره دانشجویی، نوع و وضعیت درخواست از دیتابیس می خواند
        ///</summary>
        ///<param name="stcode">شماره دانشجویی</param>
        /// <param name="RequestLogID">شناسه وضعیت درخواست</param>
        /// <param name="RequestTypeID">شناسه نوع درخواست</param>
        /// <returns>شناسه درخواست دانشجو</returns>
        public DataTable GetStudentRequestID(string stcode, int RequestLogID, int RequestTypeID)
        {
            return EditDAO.GetStudentRequestID(stcode, RequestLogID, RequestTypeID);
        }
        /// <summary>چنانچه دانشجو عکس پرسنلی خود را ویرایش نماید، عکس دانشجو را در دیتابیس ذخیره می نماید
        /// </summary>
        /// <param name="stcode">شماره دانشجویی</param>
        /// <param name="EditedID">شناسه نوع درخواست ویرایش</param>
        /// <param name="PersonalImage">عکس پرسنلی</param>
        /// <param name="RequestLogID">شناسه وضعیت درخواست</param>
        ///  <param name="StudentRequestID">شناسه درخواست دانشجو</param>
        public void InsertImageIntoEditePerInfo(string stcode, int EditedID, byte[] PersonalImage, int RequestLogID, int StudentRequestID)
        {
            EditDAO.InsertImageIntoEditePerInfo(stcode, EditedID, PersonalImage, RequestLogID, StudentRequestID);
        }
        /// <summary> درخواست ویرایش دانشجو را در دیتابیس وارد می نماید
        /// </summary>
        ///<param name="stcode">شماره دانشجویی</param>
        ///  <param name="NewContent">عبارت صحیح</param>
        /// <param name="EditedID">شناسه نوع درخواست ویرایش</param>
        /// <param name="RequestLogID">شناسه وضعیت درخواست</param>
        /// <param name="StudentRequestID">شناسه درخواست دانشجو</param>
        public int InsertIntoEditPersonalInf(string stcode, string NewContent, int EditedID, int RequestLogID, int StudentRequestID)
        {
            return EditDAO.InsertIntoEditPersonalInf(stcode, NewContent, EditedID, RequestLogID, StudentRequestID);
        }
        /// <summary>شماره دانشجویی، دانشجویانی که درخواست ویرایش داشته اند را بر می گرداند
        /// </summary>
        ///  <returns>نام،نام خانوادگی،شماره دانشجویی</returns>
        public DataTable GetEditRequest()
        {
            return EditDAO.GetEditRequest();
        }
        public DataTable GetPicEditRequest()
        {
            return EditDAO.GetPicEditRequest();
        }

        /// <summary>کلیه اطلاعات دانشجو را از جدول درخواست ویرایش بر می گرداند
        ///</summary>
        ///<param name="stcode">شماره دانشجویی</param>
        /// <returns>شناسه نوع ویرایش،نام نوع ویرایش،عبارت صحیح،عکس پرسنلی</returns>
        public DataTable GetSTudentEditRequest(string stcode)
        {
            return EditDAO.GetSTudentEditRequest(stcode);
        }
        /// <summary> عکس پرسنلی دانشجو که تصحیح شده است را از دیتابیس می خواند
        /// </summary>
        /// <param name="stcode">شماره دانشجویی</param>
        /// <returns>عکس پرسنلی</returns>
        public DataTable GetStudentPic(string stcode)
        {
            return EditDAO.GetStudentPic(stcode);
        }
        /// <summary> <summary> علت رد مدرک دانشجو را در دیتابیس ذخیره می نماید
        ///</summary>
        ///<param name="stcode">شماره دانشجویی</param>
        /// <param name="EditedID">شناسه نوع ویرایش</param>
        /// <param name="RequestLogID">شناسه وضعیت درخواست</param>
        /// <param name="Tozihat">علت رد درخواست ویرایش</param>
        public void UpdateIsOk(string stcode, int EditedID, int RequestLogID, string Tozihat)
        {
            EditDAO.UpdateIsOk(stcode, EditedID, RequestLogID, Tozihat);
        }
        ///<summary> عکس پرسنلی ویرایش شده  دانشجو را بر می گرداند</summary>
        /// <param name="stcode">شماره دانشجویی</param>
        /// <returns>عکس پرسنلی</returns>
        public DataTable GetEditedIdOfStcode(string stcode)
        {
            return EditDAO.GetEditedIdOfStcode(stcode);
        }

        /// <summary> به روزرسانی می کند amozpic عکس قبلی دانشجو را در دیتابیس 
        /// </summary>
        ///<param name="stcode">شماره دانشجویی</param>
        ///<param name="PersonalImage">عکس پرسنلی</param>
        public void updateEditedStuImage(string stcode, byte[] PersonalImage)
        {
            EditDAO.updateEditedStuImage(stcode, PersonalImage);
        }
        /// <summary>
        ///را بروزرسانی می کند fsf2 اطلاعات جدول
        /// </summary>
        /// <param name="stcode">شماره دانشجویی</param>
        ///   <param name="tel">شماره تلفن</param>
        /// <param name="address">آدرس</param>
        ///   <param name="codeposti">کدپستی</param>
        ///   <param name="Ostan">استان</param>
        ///   <param name="Shahrestan">شهرستان</param>
        ///   <param name="mobile">شماره موبایل</param>
        public void UpdateEditedInfoFSF2(string stcode, string tel, string address, string codeposti, int Ostan, int Shahrestan, string mobile)
        {
            EditDAO.UpdateEditedInfoFSF2(stcode, tel, address, codeposti, Ostan, Shahrestan, mobile);
        }

        /// <summary>
        /// وضعیت درخواست ویرایش دانشجو را بروزرسانی می کند
        /// </summary>
        ///  <param name="stcode">شماره دانشجویی</param>
        ///   <param name="RequestLogID">شناسه وضعیت درخواست</param>
        ///  <param name="RequestTypeID">شناسه نوع درخواست</param>
        public void UpdateStudentEditeRequestLogID(string stcode, int RequestLogID, int RequestTypeID)
        {
            EditDAO.UpdateStudentEditeRequestLogID(stcode, RequestLogID, RequestTypeID);
        }

        /// <summary>
        /// شماره موبایل دانشجو را از دیتابیس می خواند
        /// </summary>
        /// <param name="stcode">شماره دانشجویی</param>
        ///   <returns>شماره موبایل</returns>
        public DataTable GetStudentMobile(string stcode)
        {
            return EditDAO.GetStudentMobile(stcode);
        }


    }
}
