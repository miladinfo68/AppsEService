using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAC.Connections;

namespace IAUEC_Apps.DAO.University.Education
{
   public class EducationReportDAO
    {
       SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
       SqlConnection con2 = new SqlConnection(new AmozeshConnection().con_sida);
       SqlConnection con3 = new SqlConnection(new SuppConnection().UserAccess_con);
         #region Read
       //ramezanian
       /// <summary>
       /// این متد کل اطلاعات مکان کلاس را بر می گرداند
       /// </summary>
       /// <returns></returns>
       public DataTable SelectAllLocationClass()
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandText = "[Education].[SP_LocationClass]";
           cmd.CommandType = CommandType.StoredProcedure;
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();

           }
           catch(Exception)
           {
               throw;
           }
           return dt;

       }
       //ramezanian
       /// <summary>
       /// این متد شماره کلاس را بر اساس مکان کلاس برمی گرداند
       /// </summary>
       /// <param name="LocationClass">The LocationClass.</param>
       /// <returns></returns>

       public DataTable SelectRowLocationClass(int LocationClass)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandText = "[Education].[SP_NumberClass]";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@LocationClass", LocationClass);
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();
           }
           catch(Exception)
           {
               throw;
           }
           return dt;
       }

      //ramezanian
       /// <summary>
       /// گزارش لیست کلاس بر اساس شماره کلاس را بر می گرداند
       /// </summary>
       /// <param name="NumberClass">The Number Class.</param>
       /// <param name="Term">The Term</param>
       /// <param name="LocationClass">The Location Class</param>
       /// <returns></returns>
       public DataTable SelectListClass(int Number, string Term, int Location) 
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandText = "[Education].[SP_PrintListClass]";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@Number", Number);
           cmd.Parameters.AddWithValue("@Term", Term);
           cmd.Parameters.AddWithValue("@Location", Location);
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();
           }
           catch (Exception)
           {
               throw;
           }
           return dt;
       }

       //ramezanian
       /// <summary>
       /// کل اطلاعات مقاطع تحصیلی را بر می گرداند
       /// </summary>
       /// <returns></returns>
       public DataTable SelectAllDegree()
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandText = "Education.SP_SelectAllDegree";
           cmd.CommandType = CommandType.StoredProcedure;
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();
           }
           catch(Exception)
           {
               throw;
           }
           return dt;
       }
       //ramezanian
       /// <summary>
       /// کل اطلاعات رشته را بر می گرداند
       /// </summary>
       /// <returns></returns>
       public DataTable SelectAllField()
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandText = "Education.SP_SelectAllnameresh";
           cmd.CommandType = CommandType.StoredProcedure;
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();
           }
           catch(Exception)
           {
               throw;
           }
           return dt;
       }
       //ramezanian
       /// <summary>
       /// گزارش لیست کلاس بر اساس روز را برمی گرداند
       /// </summary>
       /// <param name="Term">The Term.</param>
       /// <param name="Day">The Day.</param>
       /// <param name="Saatstart">The Saat Start.</param>
       /// <param name="Saatend">The Saatend.</param>
       /// <param name="Daneshkade">The Daneshkade.</param>
       /// <param name="Field">The Field.</param>
       /// <returns></returns>
       public DataTable GetListClassBarAsasRuz(string Term, int Day, string Saatstart, string Saatend, int Daneshkade, int Field, int Degree)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandText = "[Education].[SP_ListClassByDay]";
           cmd.CommandType=CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@Term", Term);
           cmd.Parameters.AddWithValue("@Day", Day);
           cmd.Parameters.AddWithValue("@Saatstart", Saatstart);
           cmd.Parameters.AddWithValue("@Saatend", Saatend);
           cmd.Parameters.AddWithValue("@Daneshkade", Daneshkade);
           cmd.Parameters.AddWithValue("@Field", Field);
           cmd.Parameters.AddWithValue("@Degree", Degree);
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();
           }
           catch(Exception)
           {
               throw;
           }
           return dt;

       }

       //ramezanian
       /// <summary>
       ///  گزارش لیست کلاس هایی که حذف توسط گروه داشته اند را بر می گرداند
       /// </summary>
       /// <param name="Term">The Term.</param>
       /// <param name="Daneshkade">The Daneshkade.</param>
       /// <param name="Field">The Field.</param>
       /// <returns></returns>
       public DataTable GetListClassHazfi(string Term, int Day, string Saatstart, string Saatend, int Daneshkade, int Field, int Degree)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandText = "[Education].[SP_ListClassRemoval]";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@Term", Term);
           cmd.Parameters.AddWithValue("@Day", Day);
           cmd.Parameters.AddWithValue("@Saatstart", Saatstart);
           cmd.Parameters.AddWithValue("@Saatend", Saatend);
           cmd.Parameters.AddWithValue("@Daneshkade", Daneshkade);
           cmd.Parameters.AddWithValue("@Field", Field);
           cmd.Parameters.AddWithValue("@Degree", Degree);
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();

           }
           catch(Exception)
           {
               throw;
           }
           return dt;
       }
       //ramezanian
       /// <summary>
       /// گزارش لیست کلاس بر اساس تاریخ تحویل نمره را بر می گرداند
       /// </summary>
       /// <param name="Term">The Term.</param>
       /// <param name="Daneshkade">The Daneshkade.</param>
       /// <param name="FromDate">The Form Date.</param>
       /// <param name="ToDate">The TO Date.</param>
       /// <param name="Field">The Field.</param>
       /// <returns></returns>

       public DataTable GetListklassTarikhTahvilNomre(string Term, int Daneshkade, string FromDate, string ToDate, int Field, int Day, string Saatstart, string Saatend, int Degree)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandTimeout = 1000;
           cmd.CommandText = "[Education].[SP_DateDeliveryScore]";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@Term", Term);
           cmd.Parameters.AddWithValue("@Daneshkade", Daneshkade);
           cmd.Parameters.AddWithValue("@FromDate", FromDate);
           cmd.Parameters.AddWithValue("@ToDate", ToDate);
           cmd.Parameters.AddWithValue("@Field", Field);
           cmd.Parameters.AddWithValue("@Degree", Degree);
           cmd.Parameters.AddWithValue("@Day", Day);
           cmd.Parameters.AddWithValue("@Saatstart", Saatstart);
           cmd.Parameters.AddWithValue("@Saatend", Saatend);
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();

           }
           catch (Exception)
           {
               throw;
           }
           return dt;
       }
       //ramezanian
       /// <summary>
       /// گزارش لیست تداخل کلاس با ساعت خاص را بر می گرداند 
       /// </summary>
       /// <param name="Term">The Term.</param>
       /// <param name="AzSaat">The Az Saat.</param>
       /// <param name="TaSaat">The Ta Saat.</param>
       /// <param name="Saatstart">The Saat Start.</param>
       /// <param name="Saatend">The Saat End.</param>
       /// <param name="Daneshkade">The Daneshkade.</param>
       /// <param name="Field">The Field.</param>
       /// <returns></returns>
       public DataTable GetListTadakholBaSaatKhas(string Term, string AzSaat, string TaSaat, string Saatstart, string Saatend, int Daneshkade, int Field, int Day, int Degree)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandText = "[Education].[SP_ListClassSpecificTimes]";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@Term", Term);
           cmd.Parameters.AddWithValue("@AzSaat", AzSaat);
           cmd.Parameters.AddWithValue("@TaSaat", TaSaat);
           cmd.Parameters.AddWithValue("@Saatstart", Saatstart);
           cmd.Parameters.AddWithValue("@Saatend", Saatend);
           cmd.Parameters.AddWithValue("@Daneshkade", Daneshkade);
           cmd.Parameters.AddWithValue("@Field", Field);
           cmd.Parameters.AddWithValue("@Degree", Degree);
           cmd.Parameters.AddWithValue("@Day", Day);
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();
           }
           catch (Exception)
           {
               throw;
           }
           return dt;
       }
       //ramezanian
       /// <summary>
       /// گزارش لیست کلاس هایی که به حد نصاب نرسیده اند را بر می گرداند
       /// </summary>
       /// <param name="Term">The Term.</param>
       /// <param name="MinCapacity">The Min Capacity.</param>
       /// <param name="Daneshkade">The Daneshkade.</param>
       /// <param name="Field">The Field.</param>
       /// <returns></returns>
       public DataTable GetListClassToLimitSupply(string Term, int MinCapacity, int MaxCapacity, int Vahed, int Daneshkade, int Field, string Saatstart, string Saatend, int Day, int Degree)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandText = "[Education].[SP_ListClassIsNotEnough]";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@Term", Term);
           cmd.Parameters.AddWithValue("@MinCapacity", MinCapacity);
           cmd.Parameters.AddWithValue("@MaxCapacity", MaxCapacity);
           cmd.Parameters.AddWithValue("@Vahed", Vahed);
           cmd.Parameters.AddWithValue("@Daneshkade", Daneshkade);
           cmd.Parameters.AddWithValue("@Field", Field);
           cmd.Parameters.AddWithValue("@Saatstart", Saatstart);
           cmd.Parameters.AddWithValue("@Saatend", Saatend);
           cmd.Parameters.AddWithValue("@Degree", Degree);
           cmd.Parameters.AddWithValue("@Day", Day);
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();
           }
           catch(Exception)
           {
               throw;
           }
           return dt; 
       }
       //ramezanian
       /// <summary>
       /// گزارش لیست مغایرت نمرات ثبت شده توسط آموزش و دایره امتحانات را بر می گرداند
       /// </summary>
       /// <param name="AzMoshakhase">The Az Moshakhase.</param>
       /// <param name="TaMoshakhase">The Ta Moshakhase.</param>
       /// <param name="Field">The Field.</param>
       /// <param name="Term">The Term.</param>
       /// <returns></returns>
       public DataTable GetListMoghayerat(int AzMoshakhase, int TaMoshakhase, int Field, string Term)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandText = "[Education].[SP_DatesContrastScore]";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@AzMoshakhase", AzMoshakhase);
           cmd.Parameters.AddWithValue("@TaMoshakhase", TaMoshakhase);
           cmd.Parameters.AddWithValue("@Field", Field);
           cmd.Parameters.AddWithValue("@Term", Term);
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();
           }
           catch (Exception)
           {
               throw;
           }
           return dt;
       }
       //ramezanian
       /// <summary>
       /// گزارش لیست دانشجویان عدم مجوز ثبت نام را بر می گرداند
       /// </summary>
       /// <param name="StCode">The Student Code.</param>
       /// <param name="Degree">The Degree.</param>
       /// <param name="Education">The Education.</param>
       /// <param name="Sex">The Sex.</param>
       /// <param name="SalVorud">The Sal Vorud.</param>
       /// <returns></returns>
       public DataTable GetListAdamMojavez(string Term ,string StCode, int Degree, int Education, int Sex, string SalVorud , int Field)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandText = "[Education].[SP_NotRegistrationLicense]";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@StCode", StCode);
           cmd.Parameters.AddWithValue("@Degree", Degree);
           cmd.Parameters.AddWithValue("@Education", Education);
           cmd.Parameters.AddWithValue("@Sex",Sex );
           cmd.Parameters.AddWithValue("@SalVorud", SalVorud);
           cmd.Parameters.AddWithValue("@Field", Field);
           cmd.Parameters.AddWithValue("@Term", Term);
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();
           }
           catch(Exception)
           {
               throw;
           }
           return dt;

       }
       //ramezanian
       /// <summary>
       /// کل دپارتمان ها را بر می گرداند
       /// </summary>
       /// <returns></returns>
       public DataTable GetSelectAllDepartman()
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandText = "[Education].[SP_SelectAllDepartman]";
           cmd.CommandType = CommandType.StoredProcedure;
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();
           }
           catch(Exception)
           {
               throw;
           }
           return dt;
       }
       //ramezanian
       /// <summary>
       /// گزارش برنامه هفتگی را بر می گرداند
       /// </summary>
       /// <param name="Term">The Term.</param>
       /// <param name="Day">The Day.</param>
       /// <param name="LocationClass">The Location Class</param>
       /// <param name="Field">The Field.</param>
       /// <param name="Departman">The Departman</param>
       /// <param name="Daneshkade">The Daneshkade</param>
       /// <param name="Degree">The Degree</param>
       /// <returns></returns>
       public DataTable GetListBarnameHaftegi(string Term, int Day, int LocationClass, int Field, int Departman, int Daneshkade, int Degree , string SaatStart , string SaatEnd)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandTimeout = 3000;
           cmd.CommandText = "[Education].[SP_WeeklyPlan]";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@Term", Term);
           cmd.Parameters.AddWithValue("@Day", Day);
           cmd.Parameters.AddWithValue("@LocationClass", LocationClass);
           cmd.Parameters.AddWithValue("@Field", Field);
           cmd.Parameters.AddWithValue("@Departman", Departman);
           cmd.Parameters.AddWithValue("@Daneshkade", Daneshkade);
           cmd.Parameters.AddWithValue("@Degree", Degree);
           cmd.Parameters.AddWithValue("@SaatSt1", SaatStart);
           cmd.Parameters.AddWithValue("@SaatSt2", SaatEnd);
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr=cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();
           }
           catch(Exception)
           {
               throw;
           }
           return dt;
       }
       //ramezanian
       /// <summary>
       /// همه لیست دروس را بر می گرداند
       /// </summary>
       /// <returns></returns>
       public DataTable GetListDorus()
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandText = "Education.SP_SelectAllLesson";
           cmd.CommandType = CommandType.StoredProcedure;
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();
           }
           catch (Exception)
           {
               throw;
           }
           return dt;
       }
       //ramezanian
       /// <summary>
       /// لیست دروس قبولی بر اساس درس و رشته را بر می گرداند
       /// </summary>
       /// <param name="Field">The Field.</param>
       /// <param name="Lesson">The lesson.</param>
       /// <returns></returns>
       public DataTable GetListDorusGhabuli(string Term,int Field, int Lesson, int Geraesh, int Degree)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandText = "[Education].[SP_AcceptedListLesson]";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@Term", Term);
           cmd.Parameters.AddWithValue("@Lesson", Lesson);
           cmd.Parameters.AddWithValue("@Field", Field);
           cmd.Parameters.AddWithValue("@Graesh", Geraesh);
           cmd.Parameters.AddWithValue("@Degree", Degree);
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();
           }
           catch(Exception)
           {
               throw;
           }
           return dt;
       }
       //ramezanian
       /// <summary>
       /// لیست دروس قبولی بر اساس ترم- درس - رشته را بر می گرداند
       /// </summary>
       /// <param name="Term">The Term.</param>
       /// <param name="Field">The Field.</param>
       /// <param name="Lesson">The Lesson.</param>
       /// <returns></returns>
       public DataTable GetListDorusGhabuliBarAsasTerm(string Term, int Field, int Lesson, int Geraesh ,int Degree)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandText = "[Education].[SP_AcceptedListLessonByTerm]";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@Term", Term);
           cmd.Parameters.AddWithValue("@Lesson", Lesson);
           cmd.Parameters.AddWithValue("@Field", Field);
           cmd.Parameters.AddWithValue("@Graesh", Geraesh);
           cmd.Parameters.AddWithValue("@Degree",Degree );
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();
           }
           catch (Exception)
           {
               throw;
           }
           return dt;
       }
       //ramezanian
       /// <summary>
       /// لیست افرادی که گواهی اشتغال به تحصیل را می خواهند را بر می گرداند
       /// </summary>
       /// <param name="FromDate">The FromDate</param>
       /// <param name="ToDate">The ToDate</param>
       /// <param name="Term">The Term.</param>
       /// <returns></returns>
       public DataTable GetListEshteghalBeTahsil(string FromDate, string ToDate, string Term)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandText = "Education.SP_GovahiEshteghal";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@FromDate", FromDate);
           cmd.Parameters.AddWithValue("@ToDate", ToDate);
           cmd.Parameters.AddWithValue("@Term", Term);
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();
           }
           catch (Exception)
           {
               throw;
           }
           return dt;
       }
       public DataTable GetReshByDaneshkade(int Daneshkade)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandText = "Education.SP_SelectReshByDaneshkade";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@Daneshkade", Daneshkade);
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();
           }
           catch(Exception)
           {
               throw;
           }
           return dt;
       }

       public DataTable GetLessonByField(int Field)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandText = "Education.SP_SelectAllLessonByField";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@Field", Field);
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();
           }
           catch (Exception)
           {
               throw;
           }
           return dt;
       }
       public DataTable GetNameClass(int NumberClass)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandText = "[Education].[SP_GetNameClass]";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@id", NumberClass);
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();

           }
           catch(Exception)
           {
               throw;
           }
           return dt;
       }
       public DataTable GetNameField(int Field)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandText = "[Education].[SP_GetNameField]";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@Field", Field);
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();

           }
           catch (Exception)
           {
               throw;
           }
           return dt;
       }
       public DataTable GetNameLesson(int Lesson)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandText = "[Education].[GetNameLesson]";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@Lesson", Lesson);
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();

           }
           catch (Exception)
           {
               throw;
           }
           return dt;
       }
       public DataTable SelectFieldByDegree(int degree)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandText = "Education.SP_GetFieldByDegreeAndTerm";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@Degree", degree);
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();
           }
           catch (Exception)
           {
               throw;
           }
           return dt;
       }
       public DataTable GetGeraeshByField(int Field)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandText = "Education.SP_GetGeraeshByField";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@Field", Field);
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();
           }
           catch (Exception)
           {
               throw;
           }
           return dt;
       }
       public DataTable Getthesis(string Term , int Daneshkade)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandText = "Education.SP_Thesis";
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@Term", Term);
           cmd.Parameters.AddWithValue("@Daneshkade", Daneshkade);
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();
           }
           catch (Exception)
           {
               throw;
           }
           return dt;
       }
       public DataTable GetInfoAllStudent(string StCode, string Family, string NameEp, string IdMeli, int Degree, int StatusStu, int Field)
       {
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = con2;
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.CommandText = "dbo.SP_GetInfoStudentByFilter";
           cmd.Parameters.AddWithValue("@Stcode",StCode );
           cmd.Parameters.AddWithValue("@Family",Family );
           cmd.Parameters.AddWithValue("@NameEp",NameEp );
           cmd.Parameters.AddWithValue("@Degree",Degree );
           cmd.Parameters.AddWithValue("@IdMeli", IdMeli);
           cmd.Parameters.AddWithValue("@StatusStu",StatusStu);
           cmd.Parameters.AddWithValue("@Field", Field);
           DataTable dt = new DataTable();
           try
           {
               con2.Open();
               SqlDataReader rdr;
               rdr = cmd.ExecuteReader();
               dt.Load(rdr);
               con2.Close();
           }
           catch
           {
               throw;
           }
           return dt;
       } 
         #endregion
    }
}
