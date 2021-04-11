using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using IAUEC_Apps.DAO.University.Exam;
using System.Configuration;
using IAUEC_Apps.DTO.University.Exam;
using System.IO;
using Ionic.Zip;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Spire.Pdf;
using Font = System.Drawing.Font;
using Image = iTextSharp.text.Image;
using PdfDocument = Spire.Pdf.PdfDocument;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace IAUEC_Apps.Business.university.Exam
{

    public class ExamBusiness
    {
        /// <summary>
        ///ایجاد نموده ایم ExamDAO یک شئ از کلاس
        /// </summary>
        ExamDAO examDAO = new ExamDAO();
        /// <summary>
        /// ////////
        /// </summary>
        //public static readonly string Exam_Term = "97-98-1";
        public static readonly string Exam_Term = ConfigurationManager.AppSettings["Exam_Term"];

        public DataTable GetExamPaperStatus(string did)
        {
            return examDAO.GetExamPaperStatus(did);

        }
        public DataTable MahalBargozariSans(string dateexam, string saatexam, string cityID, int iddanesh, string selectedTerm)
        {
            return examDAO.MahalBargozariSans(dateexam, saatexam, cityID, iddanesh, selectedTerm);
        }
        /// <summary>
        /// این متد برای تخصیص شماره صندلی به کار می رود
        /// </summary>
        /// <param name="seat">شماره صندلی</param>
        /// <param name="term">ترم جاری</param>
        /// <param name="stcode">شماره دانشجویی</param>
        /// <param name="did">کد درس</param>
        /// <param name="city">The city.</param>
        /// <param name="ExamPlace">محل آزمون</param>
        public void AssignSeatNumberToStudent(int seat, string stcode, string did, string city, string ExamPlace)
        {
            examDAO.AssignSeatNumberToStudent(seat, stcode, did, city, ExamPlace);
        }

        /// <summary>
        /// این متد دانشجویان یک کلاس را بر اساس تاریخ بر می گرداند
        /// </summary>
        /// <param name="did">کد درس</param>
        /// <param name="term">ترم</param>
        /// <returns>کلاس</returns>
        public DataTable GetAllStudentByClassInDate(int did)
        {
            return examDAO.GetAllStudentByClassInDate(did);
        }

        public DataTable GetAllStudentByClassInDate(int did, string city)
        {
            return examDAO.GetAllStudentByClassInDate(did, city);
        }

        /// <summary>
        /// این متد شماره صندلی را بر اساس ترم چک می کند که تکراری نباشد
        /// </summary>
        /// <param name="seat">صندلی</param>
        /// <param name="term">ترم</param>
        /// <param name="dateexam">تاریخ</param>
        /// <param name="saatexam">ساعت</param>
        /// <param name="city">شهر</param>
        /// <returns></returns>
        public bool CheckSeatNumberByTerm(int seat, string dateexam, string saatexam, string city)
        {
            return examDAO.CheckSeatNumberByTerm(seat, dateexam, saatexam, city);
        }
        public bool CheckSeatIsAssigned(string did, string stcode, string cityname)
        {
            return examDAO.CheckSeatIsAssigned(did, stcode, cityname);
        }

        public int GetFilledSeats(string cityName, string examDate, string examTime)
        {
            return examDAO.GetFilledSeats(cityName, examDate, examTime);
        }

        public bool CheckExamPlaceOverlap(int startRange, int endRange, string cityName, int examPlaceId = -1)
        {
            return examDAO.CheckExamPlaceOverlap(startRange, endRange, cityName, Exam_Term, examPlaceId).Rows.Count > 0;
        }

        /// <summary>
        /// این متد تمام کلاس هایی که در یک تاریخ امتحان دارند را بر می گرداند
        /// </summary>
        /// <param name="dateexam">تاریخ</param>
        /// <param name="saatexam">ساعت</param>
        /// <returns>کد درس،ظرفیت</returns>
        public DataTable GetAllClassInDate(string dateexam, string saatexam)
        {
            return examDAO.GetAllClassInDate(dateexam, saatexam);
        }

        public DataTable ListAllStudentsAndDID(string examDate, string examTime, int city)
        {
            return examDAO.ListAllStudentsAndDID(examDate, examTime, city);
        }

        /// <summary>
        /// این متد ساعت امتحان را بر می گرداند
        /// </summary>
        /// <returns>ساعت</returns>
        public DataTable Get_Exam_saatexam(bool isCanceledExamBefore = false)
        {
            return examDAO.Get_Exam_saatexam(isCanceledExamBefore);
        }
        public DataTable Get_Exam_saatexamByTerm(string term)
        {
            return examDAO.Get_Exam_saatexamByTerm(term);
        }
        /// <summary>
        /// این متد تاریخ امتحان را بر می گرداند
        /// </summary>
        /// <returns>تاریخ</returns>
        public DataTable Get_Exam_dateexam(bool isCanceledExamBefore = false)
        {
            return examDAO.Get_Exam_dateexam(Exam_Term, isCanceledExamBefore);
        }

        public DataTable Get_Exam_dateexamByTerm(string term)
        {
            return examDAO.Get_Exam_dateexamByTerm(term);
        }


        /// <summary>
        /// بر اساس هر شهر ظرفیت را بر می گرداند
        /// </summary>
        /// <param name="saatexam">ساعت</param>
        /// <param name="dateexam">تاریخ</param>
        /// <param name="ID">شناسه</param>
        /// <returns>ظرفیت</returns>
        public DataTable Get_zarfiat_per_city(string saatexam, string dateexam, int ID)
        {
            return examDAO.Get_zarfiat_per_city(saatexam, dateexam, ID);
        }
        /// <summary>
        ///این متد برای تشخیص اینکه برای این کد درس کلاس تخصیص یافته به کار می رود
        /// </summary>
        /// <param name="courseCode">کددرس</param>
        /// <returns>کد درس</returns>
        public DataTable Get_existcoursecode(int courseCode)
        {
            return examDAO.Get_existcoursecode(courseCode);
        }
        /// <summary>
        /// این متد ظرفیت باقیمانده را بر می گرداند
        /// </summary>
        /// <param name="did">کد درس</param>
        /// <returns>ظرفیت باقیمانده</returns>
        public DataTable Get_ZarfiateBaghimande(string did)
        {
            return examDAO.Get_ZarfiateBaghimande(did);
        }
        public DataTable Get_ZarfiateBaghimande(string did, string cityId)
        {
            return examDAO.Get_ZarfiateBaghimande(did, cityId);
        }


        public DataTable Get_ZarfiateBaghimande22(string did, string cityId)
        {
            return examDAO.Get_ZarfiateBaghimande22(did, cityId);
        }



        /// <summary>
        ///این متد نام محل آزمون را بر می گرداند
        /// </summary>
        /// <returns>محل آزمون</returns>
        public DataTable Get_ExamPlaceName()
        {
            return examDAO.Get_ExamPlaceName();
        }
        public DataTable Get_ExamPlaceName(string cityName)
        {
            return examDAO.Get_ExamPlaceName(cityName);
        }
        /// <summary>
        /// اطلاعات کلاس های تخصیص داده شده در جدول ذخیره می گردد
        /// </summary>
        /// <param name="did">کد درس</param>
        /// <param name="ExamPlaceID">شناسه محل آزمون</param>
        /// <param name="StartRange">رنج شروع بازه</param>
        /// <param name="EndRange">رنج پایان بازه</param>
        /// <param name="City_Name">نام شهر</param>
        /// <returns></returns>
        public int Insert_ExamClassSaved(string did, int ExamPlaceID, int StartRange, int EndRange, string City_Name)
        {
            return examDAO.Insert_ExamClassSaved(did, ExamPlaceID, StartRange, EndRange, City_Name);
        }
        /// <summary>
        /// این متد بازه پایانی را بر می گرداند
        /// </summary>
        /// <param name="ExamPlaceID">شناسه محل آزمون</param>
        /// <returns></returns>
        public int Get_EndRenge(int ExamPlaceID)
        {
            return examDAO.Get_EndRange(ExamPlaceID);
        }
        /// <summary>
        /// این متد شروع بازه را بر می گرداند
        /// </summary>
        /// <param name="ExamPlaceID">شناسه محل آزمون</param>
        /// <returns>شروع بازه</returns>
        public int Get_StartRange(int ExamPlaceID, string did)
        {
            return examDAO.Get_StartRange(ExamPlaceID, did);
        }
        /// <summary>
        /// این متد بر اساس کد درس ساعت و تاریخ امتحان را بر می گرداند
        /// </summary>
        /// <param name="did">کد درس</param>
        /// <returns>تاریخ،ساعت</returns>
        public DataTable Get_SaatDateExam_perID(string did)
        {
            return examDAO.Get_SaatDateExam_perID(did);
        }
        /// <summary>
        /// این متد بیشترین بازه انتخاب شده را بر می گرداند
        /// </summary>
        /// <param name="saatexam">ساعت</param>
        /// <param name="dateexam">تاریخ</param>
        /// <param name="ExamPlaceId">شناسه محل آزمون</param>
        /// <returns></returns>
        public DataTable Get_MaxEndRange(string saatexam, string dateexam, int ExamPlaceId)
        {
            return examDAO.Get_MaxEndRange(saatexam, dateexam, ExamPlaceId);
        }
        /// <summary>
        /// این متد برای چک کردن کد درس هایی که برای آنها کلاس تخصیص داده شده به کار می رود
        /// </summary>
        /// <param name="ClassID">شناسه کلاس آزمون</param>
        /// <returns>کد کلاس</returns>
        public DataTable Check_Noduplicate_did(string ClassID)
        {
            return examDAO.Check_Noduplicate_did(ClassID);
        }
        public DataTable Check_Noduplicate_did(string ClassID, string CityName)
        {
            return examDAO.Check_Noduplicate_did(ClassID, CityName);
        }
        /// <summary>
        /// این متد برای چک کردن کلاس می باشد 
        /// </summary>
        /// <param name="did">کد درس</param>
        /// <returns>کد کلاس</returns>
        public DataTable Get_ExistClass(string did)
        {
            return examDAO.Get_ExistClass(did);
        }
        /// <summary>
        /// جزئیات کلاس ها را بر می گرداند
        /// </summary>
        /// <param name="did">کد درس</param>
        /// <returns></returns>
        public DataTable Get_did_detail(string did)
        {
            return examDAO.Get_did_detail(did);
        }
        /// <summary>
        /// شماره دانشجویی را بر می گرداند
        /// </summary>
        /// <param name="did">کد درس</param>
        /// <param name="saatexam">ساعت</param>
        /// <param name="dateexam">تاریخ</param>
        /// <returns>شماره دانشجویی</returns>
        public DataTable Get_Stcode(int did, string saatexam, string dateexam)
        {
            return examDAO.Get_Stcode(did, saatexam, dateexam);
        }
        public DataTable GetStudentByDidAndExamPlace(string did, int idexamplace)
        {
            return examDAO.GetStudentByDidAndExamPlace(did, idexamplace);
        }
        /// <summary>
        /// تعداد دانشجو را بر می گرداند
        /// </summary>
        /// <param name="did">کد درس</param>
        /// <param name="saatexam">ساعت</param>
        /// <param name="dateexam">تاریخ</param>
        /// <returns></returns>
        public int Get_tedad_daneshju(int did, string saatexam, string dateexam)
        {
            return examDAO.Get_tedad_daneshju(did, saatexam, dateexam);
        }
        /// <summary>
        /// تعداد کلاس را بر می گرداند
        /// </summary>
        /// <param name="did">کد درس</param>
        /// <returns></returns>
        public int Get_tedad_class(int did)
        {
            return examDAO.Get_tedad_class(did);
        }
        /// <summary>
        /// کلاس های ذخیره شده را بر می گرداند
        /// </summary>
        /// <param name="did">کد درس</param>
        /// <returns>مشخصات کلاس</returns>
        public DataTable Get_ExamClassSavedDetail(string did)
        {
            return examDAO.Get_ExamClassSavedDetail(did);
        }

        public DataTable Get_ExamClassSavedDetail(string did, string cityName)
        {
            return examDAO.Get_ExamClassSavedDetail(did, cityName);
        }

        public DataTable CheckIsClassSetRange(string did)
        {
            return examDAO.CheckIsClassSetRange(did);
        }
        public DataTable CheckIsClassSetRange(string did,int zarf, int cityId)
        {
            return examDAO.CheckIsClassSetRange(did, zarf, cityId);
        }
        public DataTable CheckIsClassSetRange(List<string> didList, int cityId)
        {
            return examDAO.CheckIsClassSetRange(didList, cityId);
        }
        /// <summary>
        /// شماره صندلی تکراری را چک می کند
        /// </summary>
        /// <param name="saatexam">ساعت</param>
        /// <param name="dateexam">تاریخ</param>
        /// <returns>شماره صندلی</returns>
        public DataTable Get_No_DuplicateSeatNumber(string saatexam, string dateexam, int seatnumber, int classid)
        {
            return examDAO.Get_No_DuplicateSeatNumber(saatexam, dateexam, seatnumber, classid);
        }
        /// <summary>
        /// تعداد صندلی را بر می گرداند
        /// </summary>
        /// <param name="saatexam">ساعت</param>
        /// <param name="dateexam">تاریخ</param>
        /// <returns>تعداد صندلی</returns>
        //public DataTable Get_Tedad_SeatNumber(string saatexam, string dateexam)
        //  {
        //      return examDAO.Get_Tedad_SeatNumber(saatexam,dateexam);
        //  }
        /// <summary>
        /// نام محل آزمون را بر می گرداند
        /// </summary>
        /// <param name="did">کد درس</param>
        /// <returns>نام محل آزمون</returns>
        public DataTable Get_ExamPlaceName(int did)
        {
            return examDAO.Get_ExamPlaceName(did);
        }
        /// <summary>
        /// شماره صندلی را وارد می کند
        /// </summary>
        /// <param name="StudentCode">شماره دانشجویی</param>
        /// <param name="ClassID">شناسه کلاس</param>
        /// <param name="seatnumber">شماره صندلی</param>
        /// <param name="City">شهر</param>
        /// <param name="ExamPlace">محل آزمون</param>
        public void Insert_ExamSeat(string StudentCode, int ClassID, int seatnumber, string City)
        {
            examDAO.Insert_ExamSeat(StudentCode, ClassID, seatnumber, City);
        }
        /// <summary>
        /// نام شهر آزمون را بر می گرداند
        /// </summary>
        /// <returns>نام شهر آزمون</returns>
        public DataTable Get_ExamNameCity(int ExaminerID)
        {
            return examDAO.Get_ExamNameCity(ExaminerID);
        }
        public DataTable GetAllExamPlaceCities(int cityID = -1)
        {
            return examDAO.GetAllExamPlaceCities(cityID);
        }



        public DataTable GetCityNameFilterByExaminerExamPlace()
        {
            return examDAO.GetCityNameFilterByExaminerExamPlace();
        }
        public DataTable GetExaminerUser()
        {
            return examDAO.GetExaminerUser();
        }
        public DataTable GetExaminer(bool notParticipantOnly = false)
        {
            return examDAO.GetExaminer(notParticipantOnly);
        }
        public DataTable ListExaminerExamPlace(string term, int examinerId = 0)
        {
            return examDAO.ListExaminerExamPlace(term, examinerId);
        }
        public DataTable ListAllExamClassParticipants(int examinerId = 0, int id = 0)
        {
            return examDAO.ListAllExamClassParticipants(examinerId, id);
        }
        public void AddOrUpdateExamClassParticipants(int id = 0, string examinerName = null, string examinerUserName = null, int? examinerPlaceId = null, int? examinerId = null, string term = null, string password = null, string ePass = null)
        {
            examDAO.AddOrUpdateExamClassParticipants(id, examinerName, examinerUserName, examinerPlaceId, examinerId, term, password, ePass);
        }
        public void DeleteExamClassParticipants(int examinerId = 0, int id = 0)
        {
            examDAO.DeleteExamClassParticipants(examinerId: examinerId, id: id);
        }
        /// <summary>
        /// گزارش آزمون را بر می گرداند
        /// </summary>
        /// <param name="Examdate">تاریخ آزمون</param>
        /// <param name="ExamTime">ساعت آزمون</param>
        /// <param name="ExamPlace">محل آزمون</param>
        /// <returns>جزئیات آزمون</returns>
        public DataTable Get_ExamReports(string Examdate, string ExamTime, string ExamPlaceId, int iddanesh, string selectedTerm)
        {
            return examDAO.Get_ExamReports(Examdate, ExamTime, ExamPlaceId, iddanesh, selectedTerm);
        }
        /// <summary>
        /// نام محل آزمون دانشجو را بر می گرداند
        /// </summary>
        /// <returns>نام محل آزمون</returns>
        public DataTable Get_St_ExamPlace()
        {
            return examDAO.Get_St_ExamPlace();
        }


        public int GetCountOfStudentInTehranWestZone(int examPlaceID)
        {
            return examDAO.GetCountOfStudentInTehranWestZone(examPlaceID);
        }


        /// <summary>
        /// نام محل آزمون دانشجو را بروزرسانی می کند
        /// </summary>
        /// <param name="stcode">شماره دانشجویی</param>
        /// <param name="Id_ExamPlace">کد محل آزمون</param>
        public void Update_Webmelli_Student_ExamPlace(string stcode, int Id_ExamPlace)
        {

            examDAO.Update_Webmelli_Student_ExamPlace(stcode, Id_ExamPlace);
        }
        /// <summary>
        /// نام شهر دانشجو را بر می گرداند
        /// </summary>
        /// <param name="stcode">شماره دانشجویی</param>
        /// <returns>نام شهر</returns>
        public DataTable Get_Student_CityName(string stcode, bool? change = null)
        {
            return examDAO.Get_Student_CityName(stcode , change);
        }
        /// <summary>
        /// نام شهر امتحان را بر اساس تاریخ بر می گرداند
        /// </summary>
        /// <param name="ExamDate">The exam date.</param>
        /// <param name="ExamTime">The exam time.</param>
        /// <returns></returns>
        public DataTable Get_ListClassCityByDate(int iddanesh, string tterm, string ExamDate, string ExamTime, int examinerID)
        {
            return examDAO.Get_ListClassCityByDate(iddanesh, tterm, ExamDate, ExamTime, examinerID);
        }
        /// <summary>
        /// کد درس امتحان دانشجو را بر می گرداند
        /// </summary>
        /// <returns>کد درس</returns>
        public DataTable Get_Exam_Did_Interm(string term)
        {
            return examDAO.Get_Exam_Did_Interm(term);
        }
        /// <summary>
        /// رشته امتحان را بر می گرداند
        /// </summary>
        /// <returns>رشته</returns>
        public DataTable Get_Exam_Fresh()
        {
            return examDAO.Get_Exam_Fresh();
        }
        /// <summary>
        /// نام استادها را استخراج می کند
        /// </summary>
        /// <returns>نام استاد،شناسه استاد</returns>
        public DataTable Get_Exam_Ostad(string term)
        {
            return examDAO.Get_Exam_Ostad(term);
        }
        /// <summary>
        /// بر اساس پارامترهایی گزارش می دهد
        /// </summary>
        /// <param name="Examdate">تاریخ آزمون</param>
        /// <param name="ExamTime">زمان آزمون</param>
        /// <param name="ExamPlace">محل آزمون</param>
        /// <param name="Prof">نام استاد</param>
        /// <param name="did">کد درس</param>
        /// <param name="field">رشته</param>
        /// <returns>گزارش آزمون</returns>
        public DataTable Get_ExamReportsByParams(string Examdate="0", string ExamTime="0", int ExamPlace=-1, string Prof="0", string did="0", int field=0, int iddanesh=0, string selectedTerm=null)
        {
            return examDAO.Get_ExamReportsByParams(Examdate, ExamTime, ExamPlace, Prof, did, field, iddanesh, selectedTerm);
        }
        /// <summary>
        /// گزارش آزمون را بر اساس محل آزمون استخراج می کند
        /// </summary>
        /// <param name="ExamPlace">محل آزمون</param>
        /// <returns>جزئیات آزمون</returns>
        public DataTable Get_ExamReportsByPlace(string ExamPlace, int iddanesh, string dateexam, string selectedTerm)
        {
            return examDAO.Get_ExamReportsByPlace(ExamPlace, iddanesh, dateexam, selectedTerm);
        }

        /// <summary>
        /// این متد برای ورود می باشد
        /// </summary>
        /// <param name="userName">شناسه کاربری</param>
        /// <param name="password">رمزعبور</param>
        /// <returns>شناسه کاربری</returns>
        //public DataTable UserLogin(string userName, string password)
        // {
        //     return examDAO.UserLogin(userName, password);
        // }
        /// <summary>
        ///تعداد سانس های آزمون یک شهر را بر می گرداند
        /// </summary>
        /// <returns>سانس آزمون</returns>
        public DataTable GetAllSansCoutCity(string term = "", string userId = "-1")
        {
            return examDAO.GetAllSansCoutCity(term, userId);
        }
        public DataTable GetDidByDateAndSans(string date, string sans, string term = null)
        {
            return examDAO.GetDidByDateAndSans(date, sans, term);//, string.IsNullOrEmpty(term) ? Exam_Term : term
        }
        public DataTable GetSansByDate(string date, string term = null)
        {
            return examDAO.GetSansByDate(date, term);//string.IsNullOrEmpty(term) ? Exam_Term : term
        }

        /// <summary>
        /// چک کردن محل آزمون با صندلی و شهر
        /// </summary>
        /// <param name="SeatNumber">شماره صندلی</param>
        /// <param name="term">ترم</param>
        /// <param name="city">نام شهر</param>
        /// <returns></returns>
        public DataTable GetExamPlaceBySeatAndCity(int SeatNumber, int CityID)
        {
            return examDAO.GetExamPlaceBySeatAndCity(SeatNumber, CityID);
        }

        public DataTable Get_st_in_Examplace(int id, string term = "")
        {
            return examDAO.Get_st_in_Examplace(id, term);

        }

        public void InsertExamPlaceAddress(string cityname, string address)
        {
            examDAO.InsertExamPlaceAddress(cityname, address);
        }

        public DataTable GetAllExamPlaceAddress()
        {
            return examDAO.GetAllExamPlaceAddress();
        }

        public void UpdateExamPlaceAddress(string nameCity, string address, int id, bool isActive)
        {
            examDAO.UpdateExamPlaceAddress(nameCity, address, id, isActive);
        }

        public DataTable GetExamPlaceAddressByID(int ID)
        {
            return examDAO.GetExamPlaceAddressByID(ID);
        }
        public DataTable CardQuizStudents(string stCode)
        {
            return examDAO.CardQuizStudents(stCode, Exam_Term);
        }
        public DataTable StInMojazCart(string stcode)
        {
            return examDAO.StInMojazCart(stcode);
        }

        public void InsertExamPlaceClass(string ExamPlace, int StartRange, int EndRange, string City_Name, int CityID)
        {
            examDAO.InsertExamPlaceClass(ExamPlace, StartRange, EndRange, City_Name, CityID);
        }

        public DataTable GetAllExamPlceClasses()
        {
            return examDAO.GetAllExamPlceClasses();
        }

        public DataTable GetAllExamPlceClassesForExaminer(int examinerId)
        {
            return examDAO.GetAllExamPlceClassesForExaminer(examinerId);
        }

        public void UpdateExamPlaceClass(string ExamPlace, int StartRange, int EndRange, int ExamPlaceID)
        {
            examDAO.UpdateExamPlaceClass(ExamPlace, StartRange, EndRange, ExamPlaceID);
        }
        public void UpdateExamPlaceClass(string ExamPlace, int StartRange, int EndRange, int ExamPlaceID, string CityName)
        {
            examDAO.UpdateExamPlaceClass(ExamPlace, StartRange, EndRange, ExamPlaceID, CityName);
        }

        public DataTable GetExamPlaceClassByID(int ID)
        {
            return examDAO.GetExamPlaceClassByID(ID);
        }

        public DataTable GetDidWithoutSeat()
        {
            return examDAO.GetDidWithoutSeat();
        }

        public DataTable GetAllDaneshkade()
        {
            return examDAO.GetAllDaneshkade();
        }

        public DataTable GetMinStartRange()
        {
            return examDAO.GetMinStartRange();
        }
        public DataTable GetDidByCodeOstad(int idostad)
        {
            return examDAO.GetDidByCodeOstad(idostad);
        }
        //##############################################
        public DataTable GetClassesForOstadByCodeOstad(decimal codeostad, string family, string term)
        {
            var tterm = term == "-1" ? Exam_Term : term;
            return examDAO.GetClassesForOstadByCodeOstad(codeostad, family, tterm); //
        }

        //##############################################






        public void InsertIntoExamQuestion(string did, int ExamTime, bool HasCal, bool HasNote, bool ans1, bool ans2, bool ans3, bool LawBook, string BookName)
        {
            examDAO.InsertIntoExamQuestion(did, ExamTime, HasCal, HasNote, ans1, ans2, ans3, LawBook, BookName);
        }

        public DataTable ShowQuiezPaper(int iddanesh, int act)
        {
            return examDAO.ShowQuiezPaper(iddanesh, act);
        }

        public DataTable ShowQuiezPaperHeader(int iddanesh, int status, string date, string time)
        {
            return examDAO.ShowQuiezPaperHeader(iddanesh, status, date, time);
        }
        public void SetApproveNewHeader( int id , bool? val)
        {
            examDAO.SetApproveNewHeader(id , val);
        }

        public DataTable ShowQueizPaperByDid(string did, int? cityId =null)
        //public DataTable ShowQueizPaperByDid(int did, string examDate = "-1", int? cityId = null)
        {
            //return examDAO.ShowQueizPaperByDid(did , examDate , cityId);
            return examDAO.ShowQueizPaperByDid(did ,  cityId);
        }

        public DataTable GetAllDidsToTransferToLms(string term = null, string dateExam = null, string timeExam = null, string did =null)
        {
            return examDAO.GetAllDidsToTransferToLms(term , dateExam, timeExam ,did);
        }



        public bool GetIdgroupBydid(string did)
        {
            return examDAO.GetIdgroupBydid(did);
        }

        public bool UpdateExamQuestions(int id, int did = 0, string address = null, int status = 0, string dateSaved = null, string password = null, string term = null,
            bool calc = false, bool note = false, int examTime = 0, string attachmentAddress = null, string rejectDesc = null, bool tempDownload = false,
            bool answerSheet1 = false, bool answerSheet2 = false, bool answerSheet3 = false, string lastModifiedDate = null, bool lawBook = false, string bookName = null)
        {
            return examDAO.UpdateExamQuestions(id, did, address, status, dateSaved, password, term, calc, note, examTime, attachmentAddress, rejectDesc, tempDownload,
            answerSheet1, answerSheet2, answerSheet3, lastModifiedDate, lawBook, bookName);
        }
        public bool RemoveExamQuestionAttachment(int id)
        {
            return examDAO.RemoveExamQuestionAttachment(id);
        }

        //##################################      
        public bool UpdateExamQuestion_Status(int did, int status, string term)
        {
            var tterm = term == "-1" ? Exam_Term : term;
            return examDAO.UpdateExamQuestion_Status(did, status, tterm);
        }

        //##################################


        public void UpdateQueizStatus(int Status, string Did, string RejectDesc, bool? approvedHeader = null)
        {

            examDAO.UpdateQueizStatus(Status, Did, RejectDesc, Exam_Term , approvedHeader);

        }

        public void UpdateQueizStatus(int Status, string Did, string RejectDesc, string address, string attachmentAddress)
        {

            examDAO.UpdateQueizStatus(Status, Did, RejectDesc, address, attachmentAddress, Exam_Term);
        }


        public void UpdateExamOption(string did, int ExamTime, bool HasCal, bool HasNote, bool Ans1, bool Ans2, bool Ans3, bool LawBook, string BookName ,int? Q2CityId=null)
        {
            examDAO.UpdateExamOption(did, ExamTime, HasCal, HasNote, Ans1, Ans2, Ans3, LawBook, BookName , Q2CityId);
        }
        public DataTable GetExamQuestionsbyDid(string did, string term = null , int? cityId =null)
        {
            var tterm = (term == null || term == "-1") ? Exam_Term : term;
            
            return examDAO.GetExamQuestionsbyDid(did, tterm ,cityId);
        }

        public void UploadExamFile(string Address, string Password, string did, int status)
        {
            examDAO.UploadExamFile(Address, Password, did, status);
        }

        public DataTable Get_ExamdetailbyDid(string did, string term = null, int? cityId = null)
        {
            return examDAO.Get_ExamdetailbyDid(did ,term , cityId);

        }


        public void UploadAttachment(string did, string AttachAddress)
        {
            examDAO.UploadAttachment(did, AttachAddress);
        }

        public DataTable GetmaxminExamplace(string Examplace, int did)
        {
            return examDAO.GetmaxminExamplace(Examplace, did);
        }

        // ramezanian
        public DataTable GetMobileProfByDid(string did)
        {

            return examDAO.GetMobileProfByDid(did, Exam_Term);
        }

        public DataTable GetExamQuestionUploaded(int iddanesh=0, int status=0, string term = null)
        {
            term = string.IsNullOrEmpty(term) ? term = Exam_Term : term;
            return examDAO.GetExamQuestionUploaded(iddanesh, status, term);
        }

        public void DeleteFromExamSeat(string saatexam, string dateexam)
        {
            examDAO.DeleteFromExamSeat(saatexam, dateexam);
        }
        public void DeleteFromExamSeat(string saatexam, string dateexam, string cityName)
        {
            examDAO.DeleteFromExamSeat(saatexam, dateexam, cityName);
        }

        public DataTable zarfiat_per_cityes(string saatexam, string dateexam, int ID)
        {
            return examDAO.zarfiat_per_cityes(saatexam, dateexam, ID);
        }
        public void TemplateDownloaded(string did, bool tdl)
        {
            examDAO.TemplateDownloaded(did, tdl);
        }

        public void GenerateExamKeyCode(string did)
        {
            examDAO.GenerateExamKeyCode(did);
        }



        public DataTable QuiezPaperForDL(int ExaminerID)
        {
            return examDAO.QuiezPaperForDL(ExaminerID);
        }
        public DataTable QuiezPaperForDLBySaat(int ExaminerID, string saat, bool isExamCanceledBefore = false)
        {
            return examDAO.QuiezPaperForDLBySaat(ExaminerID, saat, isExamCanceledBefore);
        }
        public DataTable AnswerSheetForDLBySaat(int ExaminerID, string saat)
        {
            return examDAO.AnswerSheetForDLBySaat(ExaminerID, saat);
        }
        public DataTable AnswerSheetForDLByDate_Saat(int ExaminerID, string saat, string date)
        {
            return examDAO.AnswerSheetForDLByDate_Saat(ExaminerID, saat, date);
        }

        public DataTable DLExamQuestionsPermissionByParams(int examPlaceID, string date, string saat)
        {
            return examDAO.DLExamQuestionsPermissionByParams(examPlaceID, date, saat);
        }


        public DataTable GetAllDidsByInputFilters(string term = null, int examPlaceID = -1, int did = -1, string date = "", string saat = "")
        {
            return examDAO.GetAllDidsByInputFilters(term, examPlaceID, did, date, saat);
        }


        public void InsertIntoChangedExamQuestionsDate(List<ExamQuestionInfo> dataList)
        {
            examDAO.InsertIntoChangedExamQuestionsDate(dataList);
        }


        public void UpdateExamQuestionsCancled(int qId = -1, string address = null, string password = null, int status = -1, int cityIdQ2 = -1, string note = "")
        {
            examDAO.UpdateExamQuestionsCancled(qId, address, password, status, cityIdQ2, note);
        }


        public void AddOrUpdate_DLExamQuestionsPermission(List<ExamQuestionInfo> dataList)
        {
            examDAO.AddOrUpdate_DLExamQuestionsPermission(dataList);
        }
        public DataTable GetExamQuestionsUploadedByDate_Saat(string saat, string date)
        {
            return examDAO.GetExamQuestionsUploadedByDate_Saat(saat, date, Exam_Term);
        }
        public void UpdateOstadPermission(int code_Ostad)
        {
            examDAO.UpdateOstadPermission(code_Ostad);
        }
        public DataTable GetOstadPermision(int code_ostad)
        {
            return examDAO.GetOstadPermision(code_ostad);
        }
        public DataTable ExamAnswerSheetbyDid(string did, int ExaminerID)
        {
            return examDAO.ExamAnswerSheetbyDid(did, ExaminerID);
        }
        public DataTable GetSaatExamByDateExam(string examDate = "-1", bool isCanceledExamBefore = false)
        {
            return examDAO.GetSaatExamByDateExam(examDate, Exam_Term, isCanceledExamBefore);
        }
        public void Insert_ExaminerInfo(int ExaminerID, int ExamPlaceID, string ExaminerName, string Mobile, string Email, string StartDate, string EndDate, string pass, int UserId)
        {
            examDAO.Insert_ExaminerInfo(ExaminerID, ExamPlaceID, ExaminerName, Mobile, Email, StartDate, EndDate);
            Common.LoginBusiness lng = new Common.LoginBusiness();
            lng.changePassword(Common.CommonBusiness.EncryptPass(pass), UserId);
        }
        public void Update_answersheetStatus(int ans, string did)
        {
            examDAO.Update_answersheetStatus(ans, did);

        }
        public DTO.University.Exam.ExaminerDTO SendEmailSMSToExaminer(int id)
        {
            DTO.University.Exam.ExaminerDTO exmDTO = new DTO.University.Exam.ExaminerDTO();
            DataTable dt = new DataTable();
            dt = examDAO.SendEmailSMSToExaminer(id);
            exmDTO.Email = dt.Rows[0]["Email"].ToString();
            exmDTO.Mobile = dt.Rows[0]["Mobile"].ToString();
            exmDTO.UserName = dt.Rows[0]["UserName"].ToString();
            exmDTO.Pass = Common.CommonBusiness.DecryptPass(dt.Rows[0]["Password"].ToString());
            exmDTO.lms_Pass = dt.Rows[0]["password1"].ToString();
            exmDTO.epass = dt.Rows[0]["epass"].ToString();
            exmDTO.euser = dt.Rows[0]["euser"].ToString();
            return exmDTO;
        }
        public DTO.University.Exam.ExaminerDTO GetExamClassParticipantsByUserName(string userName)
        {
            DTO.University.Exam.ExaminerDTO exmDTO = new DTO.University.Exam.ExaminerDTO();
            DataTable dt = new DataTable();
            dt = examDAO.GetExamClassParticipantsByUserName(userName);
            exmDTO.Email = dt.Rows[0]["Email"].ToString();
            exmDTO.Mobile = dt.Rows[0]["Mobile"].ToString();
            exmDTO.UserName = dt.Rows[0]["UserName"].ToString();
            exmDTO.Pass = Common.CommonBusiness.DecryptPass(dt.Rows[0]["Password"].ToString());
            exmDTO.lms_Pass = dt.Rows[0]["password1"].ToString();
            return exmDTO;
        }
        public void DeleteExamClassSavedById(int Id)
        {
            examDAO.DeleteExamClassSavedById(Id);
        }

        //#################################
        //################################# added by jalali 1396/03/29

        public DataTable GetSystemAvailability(int appID, int userKind, int userStatus)
        {
            return examDAO.GetSystemAvailability(appID, userKind, userStatus);
        }

        public bool UpdateSystemAvailabilityByParams(int appID, Byte userKind, Byte userStatus, string fromDate, string startTime, string toDate, string endTime)
        {
            return examDAO.UpdateSysAvailabilityByParams(appID, userKind, userStatus, fromDate, startTime, toDate, endTime);
        }
        //#################################
        //#################################

        public string GetFirstUploadDate()
        {
            var pcal = new System.Globalization.PersianCalendar();
            string y, m, d;
            y = pcal.GetYear(DateTime.Now).ToString();

            m = pcal.GetMonth(DateTime.Now).ToString().Length == 1 ?
                   pcal.GetMonth(DateTime.Now).ToString().PadLeft(2, '0') :
                   pcal.GetMonth(DateTime.Now).ToString();

            d = pcal.GetDayOfMonth(DateTime.Now).ToString().Length == 1 ?
                  pcal.GetDayOfMonth(DateTime.Now).ToString().PadLeft(2, '0') :
                  pcal.GetDayOfMonth(DateTime.Now).ToString();

            var str = y + "/" + m + "/" + d;
            return str;
        }
        public DataTable GetCourseList()
        {
            return examDAO.GetCourseList();
        }
        public List<string> GetPreviusTerms(int yearCount = 2)
        {
            //TODO: After 1400
            var currentTerm = ConfigurationManager.AppSettings["Term"];
            List<string> res = new List<string>();
            var startYear = int.Parse(currentTerm.Split('-')[0]);
            startYear = (startYear - yearCount) > 0 ? startYear - yearCount : (100 + startYear) - yearCount;
            for (int i = 0; i <= yearCount; i++)
            {
                if (i != yearCount)
                {
                    res.Add((startYear + i).ToString() + '-' + (startYear + i + 1) + '-' + '1');
                    res.Add((startYear + i).ToString() + '-' + (startYear + i + 1) + '-' + '2');
                }
                else if (int.Parse(currentTerm.Split('-')[2]) == 2)
                    res.Add((startYear + i).ToString() + '-' + (startYear + i + 1) + '-' + '1');
            }
            return res;
        }
        public DataTable GetPreviusExamQuestions(int course = 0, string term = null)
        {
            if (string.IsNullOrEmpty(term) || term == "-1")
                term = string.Join("','", GetPreviusTerms());
            return examDAO.GetPreviusExamQuestions(course, term);
        }
        public DataTable GetExamQuestionById(int id = 0)
        {
            if (id > 0)
                return examDAO.GetExamQuestionById(id);
            return null;
        }
        public DataTable GetCourseListForTerms()
        {
            var terms = string.Join("','", GetPreviusTerms());
            return examDAO.GetCourseListForTerms(terms);
        }

        public DataTable GetExamQuestionsByDateAndExaminerId(string examDate = null, int examinerId = 0)
        {
            if (!string.IsNullOrEmpty(examDate))
                return examDAO.GetExamQuestionsByDateAndExaminerId(examDate, examinerId);
            return new DataTable();
        }
        public bool AddOrUpdateExamPaper(int examPaperId = 0, string trackNumber = null, int examPlaceId = 0, string examDate = null,
            string examTime = null)
        {
            if (!string.IsNullOrEmpty(trackNumber) && examPlaceId > 0 && !string.IsNullOrEmpty(examDate) &&
                !string.IsNullOrEmpty(examTime))
                return examDAO.AddOrUpdateExamPaper(examPaperId, trackNumber, examPlaceId, examDate, examTime);
            return false;
        }
        public bool SetSecretariatReceived(string trackNumber = null, string examDate = null, int placeId = 0)
        {
            if (!string.IsNullOrEmpty(trackNumber) && !string.IsNullOrEmpty(examDate) && placeId > 0)
                return examDAO.SetSecretariatReceived(trackNumber, examDate, placeId);
            return false;
        }
        public PollDTO GetPollById(int pollId)
        {
            return examDAO.GetPollById(pollId);
        }
        public PollDTO GetPollByTypeAndTerm(int type, string term)
        {
            return examDAO.GetPollByTypeAndTerm(type, term);
        }
        public List<PollQuestionDTO> GetQuestionsOfPoll(int pollId)
        {
            return examDAO.GetQuestionsOfPoll(pollId);
        }
        public List<PollOptionDTO> GetOptionsOfPollQuestion(int questionId)
        {
            return examDAO.GetOptionsOfPollQuestion(questionId);
        }
        public List<PollAnswerDTO> GetUserPollAnswer(int userId, int pollId, string target)
        {
            return examDAO.GetUserPollAnswer(userId, pollId, target);
        }
        public bool AddOrUpdatePollAnswer(PollAnswerDTO answer)
        {
            return examDAO.AddOrUpdatePollAnswer(answer);
        }
        public bool AddOrUpdatePollComment(PollCommentDTO comment)
        {
            return examDAO.AddOrUpdatePollComment(comment);
        }
        public PollQuestionDTO GetQuestionByOptionId(int optionId)
        {
            return examDAO.GetQuestionByOptionId(optionId);
        }
        public List<PollDTO> GetAllPolls()
        {
            return examDAO.GetAllPolls();
        }
        public bool AddOrUpdatePoll(string title, string term, string description, bool needComment, DateTime? fromDate = null, DateTime? toDate = null, int pollId = 0, int pollType = 0)
        {
            return examDAO.AddOrUpdatePoll(title, term, description, needComment, fromDate, toDate, pollId, pollType);
        }
        public DataTable GetPollAnswersByTermAndCityId(string term, int cityId)
        {
            return examDAO.GetPollAnswersByTermAndCityId(term: term, cityId: cityId);
        }
        public List<PollAnswerDTO> GetPollAnswersByTermAndCityId(string term, int cityId, bool showDetails)
        {
            return examDAO.GetPollAnswersByTermAndCityId(term: term, cityId: cityId, showDetails: showDetails);
        }
        public PollCommentDTO GetPollComment(int pollId, int targetId)
        {
            return examDAO.GetPollComment(pollId: pollId, targetId: targetId);
        }

        public bool DeletePoll(int pollId)
        {
            return examDAO.DeletePoll(pollId);
        }
        public bool CopyPoll(int pollId, string term)
        {
            return examDAO.CopyPoll(pollId, term);
        }
        public bool AddOrUpdatePollQuestion(PollQuestionDTO question)
        {
            return examDAO.AddOrUpdatePollQuestion(question);
        }
        public bool AddOrUpdatePollOption(PollOptionDTO option)
        {
            return examDAO.AddOrUpdatePollOption(option);
        }
        public bool DeletePollQuestion(int questionId)
        {
            return examDAO.DeletePollQuestion(questionId);
        }
        public bool DeletePollOption(int optionId)
        {
            return examDAO.DeletePollOption(optionId);
        }
        public int CheckPollExistForTerm(int pollId, string term, int pollType = 0)
        {
            return examDAO.CheckPollExistForTerm(pollId, term, pollType);
        }
        public DataTable GetPollAnswersByQuestion(int questionId)
        {
            return examDAO.GetPollAnswersByQuestion(questionId);
        }
        public DataTable GetStudentInfo(string stcode)
        {
            return examDAO.GetStudentInfo(stcode);
        }

        public DataTable GetProfessorInfoByProfessorCode(string code)
        {
            return examDAO.GetProfessorInfoByProfessorCode(code);
        }


        public bool ChangeTemplateOfQuestion(string rootDirectory, string did, string password, ExamStudentDTO examInfo, string questionHeaderTemplate
            , string whitePaper, string userID, int? cityIdQ2 = null)
        {

            //Directory.GetDirectories(rootDirectory).ToList().ForEach(dir => Directory.Delete(dir, true));
            //Directory.GetFiles(rootDirectory, "*.*").Where(w => !w.Contains(".zip")).ToList().ForEach(file => File.Delete(file));

            var userIdDirectory = $"{rootDirectory}\\{userID}";
            if (Directory.Exists(userIdDirectory))
            {
                Directory.Delete(userIdDirectory, true);
                Directory.CreateDirectory(userIdDirectory);
            }
            else
            {
                Directory.CreateDirectory(userIdDirectory);
            }

            FileToExtract(userIdDirectory, did, password, userID, cityIdQ2);
            ConvertExamPaperAndAttachedFileToImage(did, userIdDirectory, questionHeaderTemplate, whitePaper);
            ////======================================                   
            FillBlankFieldsInHeaderTemlate(userIdDirectory, examInfo);
            ZipFileAgain(userIdDirectory, did, password, userID);
            //======================================

            return true;
        }
        public bool GeneratePdfQuestionForStudents(string rootDirectory, string did, string password, string questionHeaderTemplate
            , string whitePaper, string userID, ExamStudentDTO examInfo = null, List<ExamStudentDTO> studentList = null ,int? cityIdQ2=null)
        {
            //Directory.GetDirectories(rootDirectory).ToList().ForEach(dir => Directory.Delete(dir, true));
            //Directory.GetFiles(rootDirectory, "*.*").Where(w => !w.Contains(".zip")).ToList().ForEach(file => File.Delete(file));

            var userDirectory = $"{rootDirectory}\\{userID}";
            if (Directory.Exists(userDirectory))
            {
                Directory.Delete(userDirectory, true);
                Directory.CreateDirectory(userDirectory);
            }
            else
            {
                Directory.CreateDirectory(userDirectory);
            }
            

            FileToExtract(userDirectory, did, password, userID, cityIdQ2);
            ConvertExamPaperAndAttachedFileToImage(did, userDirectory, questionHeaderTemplate, whitePaper);
            ////======================================                   
            FillBlankFieldsInHeaderTemlate(userDirectory, examInfo);
            ////======================================
            var studentIds = studentList.Select(x => x.StudentCode).Distinct().ToList();
            GenerateImageQuestionForStudents(did, userDirectory, studentIds);
            FillBlankFieldsForEachStudent(userDirectory, did, studentList);
            ExportToPdf(userDirectory, did, studentList);
            FileToZip(userDirectory, did, password, userID);

            return true;
        }

        void FileToExtract(string userDirectory, string did, string password, string userID , int? cityIdQ2)
        {
            var userDir = userDirectory.Split('\\').ToArray();
            var rootDirectory = string.Join("\\", userDir.Take(userDir.Length - 1).ToArray());
            string zipFilePath;           

            //سوال دوم برای تمامی شهرها
            if (cityIdQ2 == -1)
                zipFilePath = $"{rootDirectory}\\{did}_canceled_1.zip";
            //سوال دوم برای یک یا چندتا از شهرها
            else if (cityIdQ2 > 0)
                zipFilePath = $"{rootDirectory}\\{did}_canceled_2.zip";
            //سوال اول
            else
                zipFilePath = $"{rootDirectory}\\{did}.zip";

            var extracZipPath = $"{rootDirectory}\\{userID}";

            var zip = ZipFile.Read(zipFilePath);
            zip.Password = password;
            if (!Directory.Exists(extracZipPath))
                Directory.CreateDirectory(extracZipPath);
            zip.ExtractAll(extracZipPath, ExtractExistingFileAction.OverwriteSilently);
            zip.Dispose();
        }
        void ConvertExamPaperAndAttachedFileToImage(string fileName, string inputPath, string headerTemplate = null, string whitePaper = null)
        {
            var examPaperPath = string.Empty;
            var attachedPath = string.Empty;

            if (!Directory.Exists(string.Format("{0}\\ExamPaperAndAttachedFileToImage", inputPath)))
                Directory.CreateDirectory(string.Format("{0}\\ExamPaperAndAttachedFileToImage", inputPath));

            string outPath = string.Format("{0}\\ExamPaperAndAttachedFileToImage", inputPath);

            if (File.Exists(string.Format("{0}\\{1}.pdf", inputPath, fileName)))
                examPaperPath = string.Format("{0}\\{1}.pdf", inputPath, fileName);

            if (File.Exists(string.Format("{0}\\{1}Attached.pdf", inputPath, fileName)))
                attachedPath = string.Format("{0}\\{1}Attached.pdf", inputPath, fileName);

            if (File.Exists(string.Format("{0}\\{1}.jpg", inputPath, fileName)))
                examPaperPath = string.Format("{0}\\{1}.jpg", inputPath, fileName);

            if (File.Exists(string.Format("{0}\\{1}Attached.jpg", inputPath, fileName)))
                attachedPath = string.Format("{0}\\{1}Attached.jpg", inputPath, fileName);

            var fileCounter = 0;
            //cover a white page over added red link on top of pages by PdfDocument
            System.Drawing.Image mrkImg = System.Drawing.Image.FromFile(whitePaper);
            System.Drawing.Image headerTemp = System.Drawing.Image.FromFile(headerTemplate);
            if (!string.IsNullOrEmpty(examPaperPath))
            {
                if (Path.GetExtension(examPaperPath) == ".pdf")
                {
                    var document = new PdfDocument(examPaperPath);
                    for (var i = 0; i < document.Pages.Count; i++)
                    {
                        var image = document.SaveAsImage(i, 150, 150);

                        Graphics g = Graphics.FromImage(image);
                        if (i == 0)
                            g.DrawImage(headerTemp, 0, 0);
                        else
                            g.DrawImage(mrkImg, 0, 0);

                        image.Save(string.Format("{0}\\{1}-00{2}.jpg", outPath, fileName, i + 1),
                            System.Drawing.Imaging.ImageFormat.Jpeg);
                    }

                    fileCounter += document.Pages.Count;
                }
                else if (Path.GetExtension(examPaperPath) == ".jpg")
                {
                    fileCounter += 1;
                    File.Move(examPaperPath, string.Format("{0}\\{1}-00{2}.jpg", outPath, fileName, fileCounter));
                }
                File.Delete(examPaperPath);

            }
            if (!string.IsNullOrEmpty(attachedPath))
            {
                if (Path.GetExtension(attachedPath) == ".pdf")
                {
                    var document = new PdfDocument(attachedPath);
                    for (var i = 0; i < document.Pages.Count; i++)
                    {
                        var image = document.SaveAsImage(i, 150, 150);
                        //System.Drawing.Image mrkImg = System.Drawing.Image.FromFile(whitePaper);
                        Graphics g = Graphics.FromImage(image);
                        g.DrawImage(mrkImg, 0, 0);
                        image.Save(string.Format("{0}\\{1}-00{2}.jpg", outPath, fileName, (i + fileCounter + 1)),
                            System.Drawing.Imaging.ImageFormat.Jpeg);
                    }

                    fileCounter += document.Pages.Count;
                }
                else if (Path.GetExtension(attachedPath) == ".jpg")
                {
                    fileCounter += 1;
                    File.Move(attachedPath, string.Format("{0}\\{1}-00{2}.jpg", outPath, fileName, fileCounter));
                }
            }
        }
        void FillBlankFieldsInHeaderTemlate(string inputPath, ExamStudentDTO fileInfo)
        {
            var directory = string.Format("{0}\\ExamPaperAndAttachedFileToImage", inputPath);

            if (!Directory.Exists(directory)) return;
            var file = Directory.GetFiles(string.Format("{0}\\ExamPaperAndAttachedFileToImage", inputPath))[0];


            Bitmap bitMapImage = new System.Drawing.Bitmap(file);
            Graphics graphicImage = Graphics.FromImage(bitMapImage);
            graphicImage.SmoothingMode = SmoothingMode.AntiAlias;

            StringFormat stringformat = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            stringformat.Alignment = StringAlignment.Near;
            stringformat.LineAlignment = StringAlignment.Center;

            graphicImage.DrawString(fileInfo.TypeNimsal, new Font("Tahoma", 8, FontStyle.Bold), SystemBrushes.WindowText, new Point(913, 175), stringformat);

            if (fileInfo.CourseTitle?.Length > 30)
                graphicImage.DrawString(fileInfo.CourseTitle, new Font("Tahoma", 8, FontStyle.Bold), SystemBrushes.WindowText, new Point(1087, 245), stringformat);
            else
                graphicImage.DrawString(fileInfo.CourseTitle, new Font("Tahoma", 8, FontStyle.Bold), SystemBrushes.WindowText, new Point(990, 225), stringformat);

            if (fileInfo.ProfossorFullName?.Length > 20)
                graphicImage.DrawString(fileInfo.ProfossorFullName, new Font("Tahoma", 8, FontStyle.Bold), SystemBrushes.WindowText, new Point(1087, 292), stringformat);
            else
                graphicImage.DrawString(fileInfo.ProfossorFullName, new Font("Tahoma", 8, FontStyle.Bold), SystemBrushes.WindowText, new Point(907, 275), stringformat);

            graphicImage.DrawString(fileInfo.ExamDate, new Font("Tahoma", 8, FontStyle.Bold), SystemBrushes.WindowText, new Point(934, 325), stringformat);
            graphicImage.DrawString(fileInfo.ExamTime, new Font("Tahoma", 8, FontStyle.Bold), SystemBrushes.WindowText, new Point(930, 375), stringformat);
            graphicImage.DrawString(fileInfo.KeyCode, new Font("Tahoma", 8, FontStyle.Bold), SystemBrushes.WindowText, new Point(590, 175), stringformat);
            graphicImage.DrawString(fileInfo.ClassCode, new Font("Tahoma", 8, FontStyle.Bold), SystemBrushes.WindowText, new Point(1085, 200), stringformat);

            graphicImage.DrawString(fileInfo.ExamDuration, new Font("Tahoma", 8, FontStyle.Bold), SystemBrushes.WindowText, new Point(512, 225), stringformat);

            graphicImage.DrawString(fileInfo.Calculator, new Font("Tahoma", 7, FontStyle.Bold), SystemBrushes.WindowText, new Point(1090, 470), stringformat);
            graphicImage.DrawString(fileInfo.Note, new Font("Tahoma", 7, FontStyle.Bold), SystemBrushes.WindowText, new Point(690, 470), stringformat);
            graphicImage.DrawString(fileInfo.LowBook, new Font("Tahoma", 7, FontStyle.Bold), SystemBrushes.WindowText, new Point(1090, 490), stringformat);

            graphicImage.DrawString(fileInfo.Grade, new Font("Tahoma", 8, FontStyle.Bold), SystemBrushes.WindowText, new Point(970, 427), stringformat);

            if (fileInfo.Major?.Length > 20)
                graphicImage.DrawString(fileInfo.Major, new Font("Tahoma", 8, FontStyle.Bold), SystemBrushes.WindowText, new Point(695, 445), stringformat);
            else
                graphicImage.DrawString(fileInfo.Major, new Font("Tahoma", 8, FontStyle.Bold), SystemBrushes.WindowText, new Point(580, 427), stringformat);

            var mainFileName = string.Format("{0}\\ExamPaperAndAttachedFileToImage\\{1}", inputPath, Path.GetFileName(file));
            var newName = string.Format("{0}\\ExamPaperAndAttachedFileToImage\\r-{1}", inputPath, Path.GetFileName(file));

            bitMapImage.Save(newName);
            graphicImage.Dispose();
            bitMapImage.Dispose();

            //delete main file
            File.Delete(file);

            //rename the changed file to main name
            System.IO.File.Move(newName, mainFileName);
        }
        void GenerateImageQuestionForStudents(string fileName, string inputPath, List<string> studentCode)
        {
            var firstFile = string.Format("{0}\\ExamPaperAndAttachedFileToImage\\{1}-00{2}.jpg", inputPath, fileName, 1);
            if (!File.Exists(firstFile)) return;
            var files = Directory.GetFiles(string.Format("{0}\\ExamPaperAndAttachedFileToImage", inputPath));

            if (!Directory.Exists(string.Format("{0}\\QuestionForStudents", inputPath)))
                Directory.CreateDirectory(string.Format("{0}\\QuestionForStudents", inputPath));

            foreach (var stCode in studentCode)
            {
                foreach (var file in files)
                {
                    var destPath_ = string.Format("{0}\\QuestionForStudents\\{1}-{2}", inputPath, stCode, Path.GetFileName(file));
                    File.Copy(file, destPath_);
                }
            }
            Directory.Delete(string.Format("{0}\\ExamPaperAndAttachedFileToImage", inputPath), true);
        }
        void FillBlankFieldsForEachStudent(string inputPath, string classId, List<ExamStudentDTO> studentsList)
        {
            var directory = string.Format("{0}\\QuestionForStudents", inputPath);
            if (!Directory.Exists(directory)) return;
            var files = Directory.GetFiles(string.Format("{0}\\QuestionForStudents", inputPath));

            foreach (var file in files.Where(x => !x.Contains(string.Format("{0}-0010", classId)) && x.Contains(string.Format("{0}-001", classId))))
            {
                var studentCode = Path.GetFileName(file).Split('-')[0];
                var student = studentsList.FirstOrDefault(x => x.StudentCode == studentCode);

                Bitmap bitMapImage = new System.Drawing.Bitmap(file);
                Graphics graphicImage = Graphics.FromImage(bitMapImage);
                graphicImage.SmoothingMode = SmoothingMode.AntiAlias;

                StringFormat stringformat = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                stringformat.Alignment = StringAlignment.Near;
                stringformat.LineAlignment = StringAlignment.Center;

                graphicImage.DrawString(student.SeatHeader, new Font("Tahoma", 7, FontStyle.Bold), SystemBrushes.WindowText, new Point(695, 195), stringformat);
                graphicImage.DrawString(student.StudentCode, new Font("Tahoma", 8, FontStyle.Bold), SystemBrushes.WindowText, new Point(560, 275), stringformat);

                if (student.FirstName?.Length > 20)
                    graphicImage.DrawString(student.FirstName, new Font("Tahoma", 8, FontStyle.Bold), SystemBrushes.WindowText, new Point(695, 345), stringformat);
                else
                    graphicImage.DrawString(student.FirstName, new Font("Tahoma", 8, FontStyle.Bold), SystemBrushes.WindowText, new Point(605, 325), stringformat);

                if (student.LastName?.Length > 20)
                    graphicImage.DrawString(student.LastName, new Font("Tahoma", 8, FontStyle.Bold), SystemBrushes.WindowText, new Point(695, 395), stringformat);
                else
                    graphicImage.DrawString(student.LastName, new Font("Tahoma", 8, FontStyle.Bold), SystemBrushes.WindowText, new Point(540, 375), stringformat);

                graphicImage.DrawString(student.Grade, new Font("Tahoma", 8, FontStyle.Bold), SystemBrushes.WindowText, new Point(970, 427), stringformat);

                if (student.Major?.Length > 20)
                    graphicImage.DrawString(student.Major, new Font("Tahoma", 8, FontStyle.Bold), SystemBrushes.WindowText, new Point(695, 445), stringformat);
                else
                    graphicImage.DrawString(student.Major, new Font("Tahoma", 8, FontStyle.Bold), SystemBrushes.WindowText, new Point(580, 427), stringformat);

                var newName = string.Format("{0}\\QuestionForStudents\\r-{1}", inputPath, Path.GetFileName(file));

                bitMapImage.Save(newName);
                graphicImage.Dispose();
                bitMapImage.Dispose();
                File.Delete(file);
                System.IO.File.Move(newName, file);
            }
        }
        void ExportToPdf(string outPdfPath, string fileName, List<ExamStudentDTO> studentList = null)
        {

            var files = Directory.GetFiles(string.Format("{0}\\QuestionForStudents", outPdfPath))
                .OrderBy(x => Convert.ToInt32((Path.GetFileName(x).Split('-')[0])))
                .ThenBy(x => Convert.ToInt32((Path.GetFileName(x).Split('-')[2]).Split('.')[0])).ToList();



            using (var pdfDoc = new Document())
            {
                PdfWriter.GetInstance(pdfDoc, new FileStream(string.Format("{0}\\{1}_2.pdf", outPdfPath, fileName), FileMode.Create));
                pdfDoc.Open();

                //foreach (var file in files)
                var sortedListBySeatNo = studentList.OrderBy(ord => ord.SeatNumber);
                foreach (var st in sortedListBySeatNo)
                {
                    var selectedFilesByStCode = files.Where(w => Path.GetFileName(w).Split('-')[0] == st.StudentCode);
                    foreach (var file in selectedFilesByStCode)
                    {
                        var pngImg = iTextSharp.text.Image.GetInstance(file);
                        if (true)
                        {
                            pngImg.ScaleAbsolute(pdfDoc.PageSize.Width, pdfDoc.PageSize.Height);
                        }
                        pngImg.SetAbsolutePosition(0, 0);
                        //add to page
                        pdfDoc.Add(pngImg);
                        //start a new page
                        pdfDoc.NewPage();
                    }
                }
                Directory.Delete(string.Format("{0}\\QuestionForStudents", outPdfPath), true);
            }
        }
        void FileToZip(string rootDirectory, string did, string password, string userID)
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.Password = password;
                FileStream stream = null;

                var directory = string.Format("{0}\\{1}_2.pdf", rootDirectory, did);
                var fileName = string.Format("{0}_2.pdf", did);
                var zipFilePath = $"{rootDirectory}\\{did}_Momtahen_{userID}_2.zip";

                using (stream = new FileStream(directory, FileMode.Open, FileAccess.ReadWrite))
                {
                    zip.AddEntry(fileName, stream);
                    zip.Save(zipFilePath);
                }
            }
            //delete all extera files and directory except main zip file          (3)
            Directory.GetFiles(rootDirectory, "*.*").Where(w => !w.Contains(".zip")).ToList().ForEach(file => File.Delete(file));
            //Directory.GetDirectories(rootDirectory).ToList().ForEach(dir => Directory.Delete(dir, true));
        }
        void ZipFileAgain(string rootDirectory, string did, string password, string userID, bool deleteGeneratedPdf = true)
        {
            var files = Directory.GetFiles(string.Format("{0}\\ExamPaperAndAttachedFileToImage", rootDirectory)).ToList()
                .OrderBy(x => Convert.ToInt32((Path.GetFileName(x).Split('-')[1]).Split('.')[0])).ToList();

            var outPdfPath = string.Format("{0}\\ExamPaperAndAttachedFileToImage", rootDirectory);
            var pdfFileName = string.Format("{0}\\ExamPaperAndAttachedFileToImage\\{1}_1.pdf", rootDirectory, did);
            var zipedfileName = string.Format("{0}\\{1}_Momtahen_{2}_1.zip", rootDirectory, did, userID);

            var finalPDF_FileName = string.Format("{0}\\{1}_Momtahen_{2}_1.pdf", rootDirectory, did, userID);


            //make a pdf file and add all jpg file in Directory ...         (1)
            using (var pdfDoc = new Document())
            {
                PdfWriter.GetInstance(pdfDoc, new FileStream(pdfFileName, FileMode.Create));
                pdfDoc.Open();
                foreach (var file in files)

                {
                    var pngImg = iTextSharp.text.Image.GetInstance(file);
                    if (true)
                    {
                        pngImg.ScaleAbsolute(pdfDoc.PageSize.Width, pdfDoc.PageSize.Height);
                    }
                    pngImg.SetAbsolutePosition(0, 0);
                    //add to page
                    pdfDoc.Add(pngImg);
                    //start a new page
                    pdfDoc.NewPage();
                }
            }
            //Zip pdf file          (2)
            var pdfName = $"{did}_1.pdf";
            using (ZipFile zip = new ZipFile())
            {
                zip.Password = password;
                FileStream stream = null;
                using (stream = new FileStream(pdfFileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    zip.AddEntry(pdfName, stream);
                    zip.Save(zipedfileName);
                }
            }


            System.IO.File.Move(pdfFileName, finalPDF_FileName);

            //delete all extera files and directory except main zip file          (3)
            Directory.Delete(outPdfPath, true);
            //Directory.GetFiles(rootDirectory, "*.*").Where(w => !w.Contains("_Momtahen_")).ToList().ForEach(file => File.Delete(file));
            //Directory.GetDirectories(rootDirectory).ToList().ForEach(dir => Directory.Delete(dir, true));
        }

        //@@@@@@@@@@@@@@@@@@@@@@ change template of question teacher side //@@@@@@@@@@@@@@@@@@@@@@


    }
}
