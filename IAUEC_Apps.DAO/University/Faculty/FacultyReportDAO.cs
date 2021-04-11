using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAC.Connections;
using IAUEC_Apps.DTO.University.Faculty;

namespace IAUEC_Apps.DAO.University.Faculty
{
    
    public class FacultyReportDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
        SqlConnection con2 = new SqlConnection(new AmozeshConnection().con_sida);
        SqlConnection con3 = new SqlConnection(new SuppConnection().UserAccess_con);
        SqlConnection HR_con = new SqlConnection(new HrConnection().HR_con);
     
        #region Read

        public DataTable getBooklet(string term,bool hasBooklet,int idDanesh,int idResh)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "faculty.sp_getBooklet";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@term", term);
            cmd.Parameters.AddWithValue("@tarh", hasBooklet);
            cmd.Parameters.AddWithValue("@danesh", idDanesh);
            cmd.Parameters.AddWithValue("@resh", idResh);
            DataTable dt=new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                conn.Close();
            }
            catch(Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }

        public DataTable getBookletData(int bookletID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "faculty.sp_getBookletData";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bookletID", bookletID);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }


        public DataTable GetAllGroup(int iddanesh)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            cmd.CommandText = "Faculty.SP_SelectallGroup";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@iddanesh",iddanesh);
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
        public DataTable GetDepartmentList(int daneshid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = HR_con;
            cmd.CommandText = "[HR].[SP_SelectDepartmentByDaneshID]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@daneshid", daneshid);
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                HR_con.Open();
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                HR_con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public void DeleteProfDepartmentByDaneshID(int idProf, int daneshid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = HR_con;
            cmd.CommandText = "[HR].[Sp_DeleteProfDepartmentByDaneshID]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@daneshid", daneshid);
            cmd.Parameters.AddWithValue("@idPeople", idProf);
            DataTable dt = new DataTable();
            try
            {
                //SqlDataReader rdr;
                HR_con.Open();
                cmd.ExecuteNonQuery();
                //dt.Load(rdr);
                HR_con.Close();
            }
            catch
            {
                throw;
            }
        }

        public DataTable GetOstadGroupByIdInfoPeople(int InfopeopleId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = HR_con;
            cmd.CommandText = "[dbo].[GetOstadGroupByIdInfoPeople]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idInfoPeople", InfopeopleId);
            DataTable dt = new DataTable();

            try
            {
                SqlDataReader rdr;
                HR_con.Open();
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                HR_con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public DataTable ListAllProf()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Faculty.SP_ListAllProf";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw;
            }
            return dt;
        }

        public DataTable GetNotificationProf(string CodeOstad, string Term, int Daneshkade, int Group, int Cooperation, int order)
        {
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con2;
            cmd1.CommandText = "[Faculty].[SP_NotificationProfessors]";
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@idostad", CodeOstad);
            cmd1.Parameters.AddWithValue("@tterm", Term);
            cmd1.Parameters.AddWithValue("@iddanesh", Daneshkade);
            cmd1.Parameters.AddWithValue("@idgroup", Group);
            cmd1.Parameters.AddWithValue("@idnahveh", Cooperation);
            //cmd.Parameters.AddWithValue("@type_did", did);
            //cmd.Parameters.AddWithValue("@type_eblagh", Eblagh);
            cmd1.Parameters.AddWithValue("@order", order);
            DataTable dt = new DataTable();
            try
            {
                con2.Open();
                SqlDataReader rdr;
                rdr = cmd1.ExecuteReader();
                dt.Load(rdr);
                con2.Close();
            }
            catch(Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable GetTeachingExperienceProf(string CodeOstad , int Daneshkade, int Group, int Cooperation, string AzTerm , string TaTerm)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            cmd.CommandText = "[Faculty].[SP_TeachingExprerienceProfByTerm]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idostad", CodeOstad);
            cmd.Parameters.AddWithValue("@iddanesh", Daneshkade);
            cmd.Parameters.AddWithValue("@idgroup", Group);
            cmd.Parameters.AddWithValue("@idnahveh", Cooperation);
            cmd.Parameters.AddWithValue("@Aztterm", AzTerm);
            cmd.Parameters.AddWithValue("@Tatterm" ,TaTerm );
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
        public DataTable GetEvalutionAllProf(string Term, int CodeOstad, int Departman, int Lesson , int Order)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            cmd.CommandTimeout = 600;
            cmd.CommandText = "[Faculty].[SP_EvalutionProf]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term", Term);
            cmd.Parameters.AddWithValue("@CodeOstad", CodeOstad);
            cmd.Parameters.AddWithValue("@Lesson", Lesson);
            cmd.Parameters.AddWithValue("@Departman", Departman);
            cmd.Parameters.AddWithValue("@Order", Order);
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
        public DataTable GetEvalutionProfDividedODDQ(string Term, int CodeOstad, int Departman, int Lesson, int Order)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            cmd.CommandTimeout = 60;
            cmd.CommandText = "Faculty.SP_EvalutionProfDividedODDQ";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term", Term);
            cmd.Parameters.AddWithValue("@CodeOstad", CodeOstad);
            cmd.Parameters.AddWithValue("@Lesson", Lesson);
            cmd.Parameters.AddWithValue("@Departman", Departman);
            cmd.Parameters.AddWithValue("@Order", Order);
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
        public DataTable GetEvalutionProfDividedODD(string Term, int CodeOstad, int Departman, int Lesson ,int Order)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            cmd.CommandText = "Faculty.SP_EvalutionProfDividedODD";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term", Term);
            cmd.Parameters.AddWithValue("@CodeOstad", CodeOstad);
            cmd.Parameters.AddWithValue("@Lesson", Lesson);
            cmd.Parameters.AddWithValue("@Departman", Departman);
            cmd.Parameters.AddWithValue("@Order", Order);
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
        public DataTable GetEvalutionProfDividedODR(string Term, int CodeOstad, int Departman, int Lesson, int Order)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            cmd.CommandTimeout = 60;
            cmd.CommandText = "Faculty.SP_EvalutionProfDividedODR";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term", Term);
            cmd.Parameters.AddWithValue("@CodeOstad", CodeOstad);
            cmd.Parameters.AddWithValue("@Lesson", Lesson);
            cmd.Parameters.AddWithValue("@Departman", Departman);
            cmd.Parameters.AddWithValue("@Order", Order);
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
        public DataTable GetEvalutionProfDividedDid(string Term, int CodeOstad, int Departman, int Lesson, int Order)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            cmd.CommandText = "[Faculty].[SP_EvalutionProfDividedDid]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term", Term);
            cmd.Parameters.AddWithValue("@CodeOstad", CodeOstad);
            cmd.Parameters.AddWithValue("@Lesson", Lesson);
            cmd.Parameters.AddWithValue("@Departman", Departman);
            cmd.Parameters.AddWithValue("@Order", Order);
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
        public DataTable GetCooperation()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            cmd.CommandText = "Faculty.SP_AllCooperation";
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

        public DataTable GetListProfinCurrentTerm(string Term, int Departman, int Cooperation)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            cmd.CommandText = "Faculty.SP_ListProfCurrentTerm";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            cmd.Parameters.AddWithValue("@Term", Term);
            cmd.Parameters.AddWithValue("@Departman", Departman);
            cmd.Parameters.AddWithValue("@Cooperation", Cooperation);
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
        public DataTable GetAccessCardsProf(string Term, int Departman, int Cooperation)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            cmd.CommandText = "Faculty.SP_AccessCardsProf";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            cmd.Parameters.AddWithValue("@Term", Term);
            cmd.Parameters.AddWithValue("@Departman", Departman);
            cmd.Parameters.AddWithValue("@Cooperation", Cooperation);
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
        public DataTable GetNumberClass()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            cmd.CommandText = "[Faculty].[SP_GetNumberClass]";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                con2.Open();
                rdr=cmd.ExecuteReader();
                dt.Load(rdr);
                con2.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable GetConflictClassByCodeOstad(string Term, int CodeOstad, int Sort , int Day , int NumberClass)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            cmd.CommandText = "[Faculty].[SP_ConflictClassbyCodeOstad]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term",Term );
            cmd.Parameters.AddWithValue("@CodeOstad", CodeOstad);
            cmd.Parameters.AddWithValue("@Sort", Sort);
            cmd.Parameters.AddWithValue("@Day", Day);
            cmd.Parameters.AddWithValue("@NumberClass", NumberClass);
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                con2.Open();
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
        public DataTable GetConflictClassByNumberClass(string Term, string NumberClass, int Sort, int Day)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Faculty].[SP_ConflictClassbyNumberClass]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term", Term);
            cmd.Parameters.AddWithValue("@NumberClass", NumberClass);
            cmd.Parameters.AddWithValue("@Sort", Sort);
            cmd.Parameters.AddWithValue("@Day", Day);
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                conn.Open();
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        //public DataTable GetConflictClassByNumberClass(string Term, string NumberClass, int Sort, int Day)
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = con2;
        //    cmd.CommandText = "[Faculty].[SP_ConflictClassbyNumberClass]";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@Term", Term);
        //    cmd.Parameters.AddWithValue("@NumberClass", NumberClass);
        //    cmd.Parameters.AddWithValue("@Sort", Sort);
        //    cmd.Parameters.AddWithValue("@Day", Day);
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        SqlDataReader rdr;
        //        con2.Open();
        //        rdr = cmd.ExecuteReader();
        //        dt.Load(rdr);
        //        con2.Close();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return dt;
        //}
        public DataTable ReferToProf(string Term, string CodeOstad, string FromDate, string ToDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            cmd.CommandText = "[Faculty].[SP_ReferToMaster]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term", Term);
            cmd.Parameters.AddWithValue("@CodeOstad", CodeOstad);
            cmd.Parameters.AddWithValue("@FromDate", FromDate);
            cmd.Parameters.AddWithValue("@ToDate", ToDate);
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                con2.Open();
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

        public DataTable GetAbsenceButNoCompensationProf(string Term, int Departman, int Daneshkade, int CodeOstad , string FromDate, string ToDate ,int CountAbsence)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            cmd.CommandText = "Faculty.SP_AbsenceButNoCompensation";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term", Term);
            cmd.Parameters.AddWithValue("@Departman", Departman);
            cmd.Parameters.AddWithValue("@Daneshkade", Daneshkade);
            cmd.Parameters.AddWithValue("@CodeOstad", CodeOstad);
            cmd.Parameters.AddWithValue("@CountAbsence", CountAbsence);
            cmd.Parameters.AddWithValue("@FromDate", FromDate);
            cmd.Parameters.AddWithValue("@ToDate", ToDate);
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                con2.Open();
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
        public DataTable GetAbsenceAndCompensationProf(string Term, int Departman, int Daneshkade, int CodeOstad, string FromDate, string ToDate , int CountAbsence , string AzJobrani, string TaJobrani)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            cmd.CommandText = "[Faculty].[Sp_AbsenceAndCompensation]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term", Term);
            cmd.Parameters.AddWithValue("@Departman", Departman);
            cmd.Parameters.AddWithValue("@Daneshkade", Daneshkade);
            cmd.Parameters.AddWithValue("@CodeOstad", CodeOstad);
            cmd.Parameters.AddWithValue("@FromDate", FromDate);
            cmd.Parameters.AddWithValue("@ToDate", ToDate);
            cmd.Parameters.AddWithValue("@CountAbsence", CountAbsence);
            cmd.Parameters.AddWithValue("@AzJobrani", AzJobrani);
            cmd.Parameters.AddWithValue("@TaJobrani", TaJobrani);
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                con2.Open();
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
        public DataTable GetListTuition(string Term, int Cooperation, int Departman, int Daneshkade, int CodeOstad)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            cmd.CommandText = "Faculty.SP_Tuition";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term" , Term );
            cmd.Parameters.AddWithValue("@Cooperation",Cooperation );
            cmd.Parameters.AddWithValue("@Departman", Departman);
            cmd.Parameters.AddWithValue("@Daneshkade", Daneshkade );
            cmd.Parameters.AddWithValue("@CodeOstad", CodeOstad);
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                con2.Open();
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
        public DataTable TeachingExperienceMoreThanADay(string Term, int CodeOsatd, int Daneshkade, int Departman, int Cooperation, int Number)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            cmd.CommandText = "[Faculty].[SP_TeachingExperienceMoreThanADay]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term", Term);
            cmd.Parameters.AddWithValue("@Number", Number);
            cmd.Parameters.AddWithValue("@Departman",Departman );
            cmd.Parameters.AddWithValue("@Cooperation", Cooperation);
            cmd.Parameters.AddWithValue("@Daneshkade", Daneshkade);
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                con2.Open();
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
        public DataTable ShowClockDateExam(string Term)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            cmd.CommandText = "[Faculty].[SP_GetSaatDateExam]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term", Term);
            DataTable ClockExam = new DataTable();
            try
            {
                con2.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                ClockExam.Load(rdr);
                con2.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return ClockExam;
        }
        
        public DataTable GetNameDepartmanAndGroup(int Departman, int Cooperation)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            cmd.CommandText = "Faculty.SP_NameGroupAndDaneshByID";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Cooperation", Cooperation);
            cmd.Parameters.AddWithValue("@Departman",Departman);
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                con2.Open();
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
        public DataTable GetStudentsProbation(string Term, int Degree, int Percent )
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            cmd.CommandText = "Faculty.SP_StudentsProbation";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term", Term);
            cmd.Parameters.AddWithValue("@Degree", Degree);
            cmd.Parameters.AddWithValue("@Percent", Percent);
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                con2.Open();
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
        public DataTable GetStudentsProbationAcceptance(string Term, int Degree, int Percent)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            cmd.CommandText = "[Faculty].[SP_StudentsProbationAcceptance]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term", Term);
            cmd.Parameters.AddWithValue("@Degree", Degree);
            cmd.Parameters.AddWithValue("@Percent", Percent);
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                con2.Open();
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
        public DataTable GetListOfSelectedCoursesTeachers(string CodeOstad, string Term, int Daneshkade, int Group, int Cooperation)
        {
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con2;
            cmd1.CommandText = "[Faculty].[SP_TeachingExprerienceProf]";
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@idostad", CodeOstad);
            cmd1.Parameters.AddWithValue("@Term", Term);
            cmd1.Parameters.AddWithValue("@iddanesh", Daneshkade);
            cmd1.Parameters.AddWithValue("@idgroup", Group);
            cmd1.Parameters.AddWithValue("@idnahveh", Cooperation);
            DataTable dt = new DataTable();
            try
            {
                con2.Open();
                SqlDataReader rdr;
                rdr = cmd1.ExecuteReader();
                dt.Load(rdr);
                con2.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable GetListPayProf(string Term, int Daneshkade, int Field, int Departman, int Cooperation, int CodeOstad, int Zarib)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            cmd.CommandText = "[Faculty].[SP_ListPayTuition]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Zarib", Zarib);
            cmd.Parameters.AddWithValue("@Term", Term );
            cmd.Parameters.AddWithValue("@Departman", Departman );
            cmd.Parameters.AddWithValue("@Daneshkade", Daneshkade );
            cmd.Parameters.AddWithValue("@Field", Field );
            cmd.Parameters.AddWithValue("@Cooperation", Cooperation );
            cmd.Parameters.AddWithValue("@CodeOstad", CodeOstad );
            DataTable dt = new DataTable();
            try
            {
                con2.Open();
                SqlDataReader rdr;
                rdr=cmd.ExecuteReader();
                dt.Load(rdr);
                con2.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }
        public DataTable GetListTuition2(string Term, int Cooperation, int Departman, int Daneshkade, int CodeOstad)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            cmd.CommandText = "Faculty.SP_ListTuition";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term", Term);
            cmd.Parameters.AddWithValue("@Cooperation", Cooperation);
            cmd.Parameters.AddWithValue("@Departman", Departman);
            cmd.Parameters.AddWithValue("@Daneshkade", Daneshkade);
            cmd.Parameters.AddWithValue("@CodeOstad", CodeOstad);
            DataTable dt = new DataTable();
            try
            {
                SqlDataReader rdr;
                con2.Open();
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
        public DataTable GetAcceptMark(string Term , int Did , int CodeOstad ,int daneshID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con2;
            cmd.CommandText = "Faculty.SP_ReportAcceptMark";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term", Term);
            cmd.Parameters.AddWithValue("@CodeOstad", CodeOstad);
            cmd.Parameters.AddWithValue("@Did",Did );
            cmd.Parameters.AddWithValue("@daneshID",daneshID );
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

        public DataTable GetGroupByCode(int Code_Ostad)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = HR_con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "HR.SP_GetGroupByCodeOstad";
            cmd.Parameters.AddWithValue("@CodeOstad", Code_Ostad);
            DataTable dt = new DataTable();
            try
            {
                HR_con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                HR_con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }
        public void InsertUpdateProfGroupStatus(int infopeopleid, string groupid, int isActive)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = HR_con;
            cmd.CommandText = "[HR].[SP_InsertUpdateProfessorGroup]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@infopeopleid", infopeopleid);
            cmd.Parameters.AddWithValue("@idgroup", groupid);
            cmd.Parameters.AddWithValue("@isactive", isActive);

            try
            {
                HR_con.Open();
                cmd.ExecuteNonQuery();
                HR_con.Close();

            }
            catch
            {
                throw;
            }
        }
        public void DELETEProfessorGroupByPeopleid(int infopeopleid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = HR_con;
            cmd.CommandText = "[HR].[SP_DELETEProfessorGroupByPeopleid]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@infopeopleid", infopeopleid);
            try
            {
                HR_con.Open();
                cmd.ExecuteNonQuery();
                HR_con.Close();

            }
            catch
            {
                throw;
            }
        }
        public DataTable GetDaneshkadeByGroup(string Field)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = HR_con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "HR.SP_GetDaneshkadeByGroup";
            cmd.Parameters.AddWithValue("@idgroup", Field);
            DataTable dt = new DataTable();
            try
            {
                HR_con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                HR_con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public DataTable GetDepartmanProf(string idgroup)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = HR_con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "HR.SP_ShowRequestProf";
            cmd.Parameters.AddWithValue("@Group", idgroup);
            DataTable dt = new DataTable();
            try
            {
                HR_con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                HR_con.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }
        #endregion
        #region Update
        public void UpdateStatus( int Type , int CodeOstad , int Check , string Description , int newType)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SP_UpdateDocument";
            cmd.Connection = HR_con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Type", Type);
            cmd.Parameters.AddWithValue("@CodeOstad", CodeOstad);
            cmd.Parameters.AddWithValue("@Checked", Check);
            cmd.Parameters.AddWithValue("@Description", Description);
            cmd.Parameters.AddWithValue("@new", newType);
            try
            {
                HR_con.Open();
                cmd.ExecuteNonQuery();
                HR_con.Close();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateUserStatus(string mobile , int status)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "HR.SP_UpdateUserStatus";
            cmd.Connection = HR_con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Mobile", mobile);
            cmd.Parameters.AddWithValue("@Status", status);
            try
            {
                HR_con.Open();
                cmd.ExecuteNonQuery();
                HR_con.Close();
            }
            catch
            {
                throw;
            }
        }
        public void UpdateDocStatus(string mobile, int status)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[HR].[SP_UpdateDocStatus]";
            cmd.Connection = HR_con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Mobile", mobile);
            cmd.Parameters.AddWithValue("@Status", status);
            try
            {
                HR_con.Open();
                cmd.ExecuteNonQuery();
                HR_con.Close();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateInfoPeople(InfoPeople IP)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[HR].[SP_UpdateInfoPeo]";
            cmd.Connection = HR_con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ID", IP.ID);
            cmd.Parameters.AddWithValue("@name", IP.Name);
            cmd.Parameters.AddWithValue("@family", IP.Family);
            cmd.Parameters.AddWithValue("@name_ep", IP.Fathers_Name);
            cmd.Parameters.AddWithValue("@idd", IP.Idd);
            cmd.Parameters.AddWithValue("@idd_meli", IP.Idd_Meli);
            cmd.Parameters.AddWithValue("@sex", IP.Sex);
            cmd.Parameters.AddWithValue("@sal_tav", IP.Sal_Tav);
            cmd.Parameters.AddWithValue("@mahal_tav", IP.Mahal_Tav);
            cmd.Parameters.AddWithValue("@mahal_sodor", IP.Mahal_Sodor);
            cmd.Parameters.AddWithValue("@id_madrak", IP.Madrak);
            cmd.Parameters.AddWithValue("@idresh", IP.Field);
            cmd.Parameters.AddWithValue("@sal_madrak", IP.Sal_Madrak);
            cmd.Parameters.AddWithValue("@university", IP.Uni);
            cmd.Parameters.AddWithValue("@martabe", IP.Martabe);
            cmd.Parameters.AddWithValue("@payeh", IP.Payeh);
            cmd.Parameters.AddWithValue("@sanavat_tadris", IP.Sanavat_tadris);
            cmd.Parameters.AddWithValue("@type_estekhdam", IP.type_estekhdam);
            cmd.Parameters.AddWithValue("@nahveh_hamk", IP.base_nahveh_hamkari);
            cmd.Parameters.AddWithValue("@uni_khedmat", IP.Uni_Khedmat);
            cmd.Parameters.AddWithValue("@date_hokm", IP.Date_Hokm);
            cmd.Parameters.AddWithValue("@date_runhokm", IP.Date_RunHokm);
            cmd.Parameters.AddWithValue("@number_hokm", IP.Number_Hokm);
            cmd.Parameters.AddWithValue("@marital_status", IP.Marital_Status);
            cmd.Parameters.AddWithValue("@status_nezam", IP.Status_Nezam);
            cmd.Parameters.AddWithValue("@num_bime", IP.Number_Bime);
            cmd.Parameters.AddWithValue("@siba", IP.Siba);
            cmd.Parameters.AddWithValue("@tel_home", IP.Tel_Home);
            cmd.Parameters.AddWithValue("@tel_kar", IP.Tel_Kar);
            cmd.Parameters.AddWithValue("@mobile", IP.Mobile);
            cmd.Parameters.AddWithValue("@add_home", IP.Add_Home);
            cmd.Parameters.AddWithValue("@add_kar", IP.Add_Kar);
            cmd.Parameters.AddWithValue("@code_posti", IP.Code_Posti);
            cmd.Parameters.AddWithValue("@code_ostan_home", IP.Code_Ostan_Home);
            cmd.Parameters.AddWithValue("@code_city_home", IP.Code_City_Home);
            cmd.Parameters.AddWithValue("@code_city_work", IP.Code_City_Work);
            cmd.Parameters.AddWithValue("@code_ostan_work", IP.Code_Ostan_Work);
            cmd.Parameters.AddWithValue("@add_email", IP.Email);
            cmd.Parameters.AddWithValue("@country", IP.Country);
            cmd.Parameters.AddWithValue("@Status", IP.status);
            cmd.Parameters.AddWithValue("@IsRetired", IP.ISRetired);
            cmd.Parameters.AddWithValue("@BoundHour", IP.BoundHour);
            cmd.Parameters.AddWithValue("@textMessage", IP.TextMessage);
            cmd.Parameters.AddWithValue("@Cooperation", IP.Cooperation);
            cmd.Parameters.AddWithValue("@bimeType", IP.Bime_Type);
            cmd.Parameters.AddWithValue("@MablaghHokm", IP.MablaghHokm);
            cmd.Parameters.AddWithValue("@uni_khedmatType", IP.TypeUniKhedmat);
            cmd.Parameters.AddWithValue("@MadrakUniType", IP.TypeUniMadrak);

            try
            {
                HR_con.Open();
                cmd.ExecuteNonQuery();
                HR_con.Close();
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
