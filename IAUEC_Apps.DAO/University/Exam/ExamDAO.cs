using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DTO.University.Exam;
using IAUEC_Apps.DAC.Connections;
using System.ComponentModel;

namespace IAUEC_Apps.DAO.University.Exam
{
    public class ExamDAO
    {

        SqlConnection con = new SqlConnection(new SuppConnection().Supp_con);
        SqlConnection con_hr = new SqlConnection(new HrConnection().HR_con);
        #region Read

        public DataTable ExamAnswerSheetbyDid(string did, int ExaminerID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_ExamAnswerSheetbyDid";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@did", did);
            cmd.Parameters.AddWithValue("@ExaminerID", ExaminerID);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }

        public DataTable getAllStudentOFProfessor(long professorID, int did, string term = "")
        {
            SqlCommand cmd = new SqlCommand("exam.sp_getAllStudentsOFProfessor", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProfessorCode", professorID);
            cmd.Parameters.AddWithValue("@did", did);
            cmd.Parameters.AddWithValue("@term", term);
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return dt;
        }

        public bool GetIdgroupBydid(string did)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "exam.SP_GetIdgroupBydid";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@did", did);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
                if (dt.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }

        public DataTable GetOstadPermision(int code_ostad)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Exam.SP_GetOstadPermision";
            cmd.Parameters.AddWithValue("@code_ostad", code_ostad);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }

        public DataTable QuiezPaperForDL(int ExaminerID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_QuiezPaperForDL";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExaminerID", ExaminerID);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }
        public DataTable QuiezPaperForDLBySaat(int ExaminerID, string saat, bool isExamCanceledBefore = false)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            //cmd.CommandText = isExamCanceledBefore ? "[Exam].[SP_QuiezPaperCanceled]" : "[Exam].[SP_QuiezPaperForDLBySaat]";
            cmd.CommandText = "[Exam].[SP_QuiezPaperForDLBySaat]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExaminerID", ExaminerID);
            cmd.Parameters.AddWithValue("@saat", saat);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }

        //#################################
        //################################# added by jalali 1396/03/29
        public DataTable GetSystemAvailability(int appID, int userKind, int userStatus)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "dbo.stp_GetSysAvailabilityByParams";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@appID", appID);
            cmd.Parameters.AddWithValue("@userKind", userKind);
            cmd.Parameters.AddWithValue("@userStatus", userStatus);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception x)
            {
                //throw;
                return null;
            }
            return dt;
        }
        //#################################
        //################################# 
        public DataTable AnswerSheetForDLBySaat(int ExaminerID, string saat)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[SP_AnswerSheetForDLBySaat]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExaminerID", ExaminerID);
            cmd.Parameters.AddWithValue("@saat", saat);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }
        public DataTable AnswerSheetForDLByDate_Saat(int ExaminerID, string saat, string date)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[SP_AnswerSheetForDLByDate_Saat]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExaminerID", ExaminerID);
            cmd.Parameters.AddWithValue("@saat", saat);
            cmd.Parameters.AddWithValue("@date", date);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }

        public DataTable DLExamQuestionsPermissionByParams(int examPlaceID, string date, string saat)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[SP_DLExamQuestionsPermissionByParams]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@examPlaceID", examPlaceID);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@saat", saat);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }


        public DataTable GetAllDidsByInputFilters(string term = null, int examPlaceID = -1, int did = -1, string date = "", string saat = "")
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[SP_GetAllDidsByInputFilters]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@currentTerm", term);
            cmd.Parameters.AddWithValue("@cityId", examPlaceID);
            cmd.Parameters.AddWithValue("@did", did);
            cmd.Parameters.AddWithValue("@examDate", date);
            cmd.Parameters.AddWithValue("@examTime", saat);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception x) { }
            return dt;
        }
        public void InsertIntoChangedExamQuestionsDate(List<ExamQuestionInfo> dataList)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[SP_InsertExamQuestionsCancled]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            var prms = new DataTable();

            try
            {
                //columns header names
                prms.Columns.Add("ExamQsId", typeof(int));
                prms.Columns.Add("Term", typeof(string));
                prms.Columns.Add("CityId", typeof(int));
                prms.Columns.Add("Did", typeof(int));
                prms.Columns.Add("ExamDate", typeof(string));
                prms.Columns.Add("ExamTime", typeof(string));
                prms.Columns.Add("ProfCode", typeof(decimal));
                prms.Columns.Add("ProfName", typeof(string));
                prms.Columns.Add("CourseCode", typeof(int));
                prms.Columns.Add("CourseName", typeof(string));
                prms.Columns.Add("NewExamDate", typeof(string));
                prms.Columns.Add("NewExamTime", typeof(string));
                prms.Columns.Add("Q2Status", typeof(byte));
                prms.Columns.Add("Q2Address", typeof(string));
                prms.Columns.Add("Q2Password", typeof(string));
                prms.Columns.Add("Calculator", typeof(bool));
                prms.Columns.Add("Note", typeof(bool));
                prms.Columns.Add("MinuteExamTime", typeof(int));
                prms.Columns.Add("TemplateDownloaded", typeof(bool));
                prms.Columns.Add("Ans1", typeof(bool));
                prms.Columns.Add("Ans2", typeof(bool));
                prms.Columns.Add("Ans3", typeof(bool));
                prms.Columns.Add("LowBook", typeof(bool));
                prms.Columns.Add("BookName", typeof(string));
                prms.Columns.Add("SaveDate", typeof(string));
                prms.Columns.Add("LastModifyDate", typeof(string));
                prms.Columns.Add("FirstUploadDate", typeof(string));
                prms.Columns.Add("ReciveDateExamSheet", typeof(string));
                prms.Columns.Add("KeyCode", typeof(string));
                prms.Columns.Add("ApproveNewHeader", typeof(bool));
                prms.Columns.Add("RejectText", typeof(string));
                prms.Columns.Add("TraceNumber", typeof(string));



                //prms.Columns.Add("Status", typeof(short));

                foreach (var item in dataList)
                {
                    var dr = prms.NewRow();
                    dr["ExamQsId"] = !string.IsNullOrWhiteSpace(item.ExamQuestionID.ToString()) ? (object)item.ExamQuestionID : (object)DBNull.Value;
                    dr["Term"] = item.Term;
                    dr["CityId"] = item.CityId;
                    dr["Did"] = int.Parse(item.Did);
                    dr["ExamDate"] = item.ExamDate;
                    dr["ExamTime"] = item.ExamTime;
                    dr["ProfCode"] = item.ProfCode;
                    dr["ProfName"] = item.ProfName;
                    dr["CourseCode"] = item.CourseCode;
                    dr["CourseName"] = item.CourseName;
                    dr["NewExamDate"] = item.NewExamDate;
                    dr["NewExamTime"] = item.NewExamTime;
                    dr["Q2Status"] = item.Q2Status;
                    dr["Q2Address"] = item.Q2Address;
                    dr["Q2Password"] = item.Q2Password;
                    dr["Calculator"] = item.Calculator;
                    dr["Note"] = item.Note;
                    dr["MinuteExamTime"] = item.MinuteExamTime;
                    dr["TemplateDownloaded"] = item.TemplateDownloaded;
                    dr["Ans1"] = item.AnswerSheet1;
                    dr["Ans2"] = item.AnswerSheet2;
                    dr["Ans3"] = item.AnswerSheet3;
                    dr["LowBook"] = item.LowBook;
                    dr["BookName"] = item.BookName;
                    dr["SaveDate"] = item.SaveDate;
                    dr["LastModifyDate"] = item.LastModifyDate;
                    dr["FirstUploadDate"] = item.FirstUploadDate;
                    dr["ReciveDateExamSheet"] = item.ReciveDateExamSheet;
                    dr["KeyCode"] = item.KeyCode;
                    dr["ApproveNewHeader"] = item.ApproveNewHeader;
                    dr["RejectText"] = item.RejectText;
                    dr["TraceNumber"] = item.TraceNumber;

                    prms.Rows.Add(dr);
                }
                cmd.Parameters.Add("@examQCancled", SqlDbType.Structured).Value = prms;
                con.Open();
                var cc = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception x)
            {
                throw x;
            }
        }


        public void UpdateExamQuestionsCancled(int qId = -1, string address = null, string password = null, int status = -1, int cityIdQ2 = -1, string note = "")
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[SP_UpdateExamQuestionsCancled]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@examQId", qId);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@cityIdQ2", cityIdQ2);
            cmd.Parameters.AddWithValue("@note", note);
            try
            {
                con.Open();
                var xx = cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception x)
            {
            }

        }


        public void AddOrUpdate_DLExamQuestionsPermission(List<ExamQuestionInfo> dataList, string stcode = null, string ipAddress = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[AddOrUpdate_DLExamQuestionsPermission]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            var prms = new DataTable();

            try
            {
                //columns header names
                prms.Columns.Add("ExamQuestionID", typeof(int));
                prms.Columns.Add("Term", typeof(string));
                prms.Columns.Add("ProfCode", typeof(decimal));
                prms.Columns.Add("ProfName", typeof(string));
                prms.Columns.Add("Did", typeof(string));
                prms.Columns.Add("CourseName", typeof(string));
                prms.Columns.Add("ExamDate", typeof(string));
                prms.Columns.Add("ExamTime", typeof(string));
                prms.Columns.Add("CityId", typeof(int));
                prms.Columns.Add("IsActive", typeof(bool));

                foreach (var item in dataList)
                {
                    var dr = prms.NewRow();
                    dr["ExamQuestionID"] = !string.IsNullOrWhiteSpace(item.ID.ToString()) ? (object)item.ID : (object)DBNull.Value;
                    dr["Term"] = item.Term;
                    dr["ProfCode"] = item.ProfCode;
                    dr["ProfName"] = item.ProfName;
                    dr["Did"] = item.Did;
                    dr["CourseName"] = item.CourseName;
                    dr["ExamDate"] = item.ExamDate;
                    dr["ExamTime"] = item.ExamTime;
                    dr["CityId"] = item.CityId;
                    dr["IsActive"] = item.IsActive;
                    prms.Rows.Add(dr);
                }
                cmd.Parameters.Add("@examQP", SqlDbType.Structured).Value = prms;

                //cmd.Parameters.AddWithValue("@stcode", stcode);
                //cmd.Parameters.AddWithValue("@ipAddress", ipAddress);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception x)
            {
                throw x;
            }
        }


        public DataTable GetExamQuestionsUploadedByDate_Saat(string saat, string date, string Term)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[SP_GetExamQuestionUploadedByDate_Saat]";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@saat", saat);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@term", Term);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception x)
            { throw x; }
            return dt;
        }
        public DataTable GetSaatExamByDateExam(string dateExam = "-1", string examTerm = "-1", bool isCanceledExamBefore = false)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[SP_Exam_saatexamByDateExam]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dateexam", dateExam);
            cmd.Parameters.AddWithValue("@examTerm", examTerm);
            cmd.Parameters.AddWithValue("@isCanceledExamBefore", isCanceledExamBefore);

            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }

        public DataTable GetExamQuestionsByDateAndExaminerId(string dateExam, int examinerId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[SP_GetExamQuestionsByDateAndExaminerId]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamDate", dateExam);
            cmd.Parameters.AddWithValue("@ExaminerId", examinerId);

            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }

        public DataTable GetCourseList()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[dbo].[SP_class_Interm]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@term", "");
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            return dt;
        }
        public DataTable GetCourseListForTerms(string terms)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[dbo].[SP_CourseListInTerms]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Terms", terms);
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            return dt;
        }
        public DataTable zarfiat_per_cityes(string saatexam, string dateexam, int ID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[SP_zarfiat_per_cityes]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@saatexam", saatexam);
            cmd.Parameters.AddWithValue("@dateexam", dateexam);
            cmd.Parameters.AddWithValue("@ID", ID);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }
        public DataTable GetExamQuestionUploaded(int iddanesh=0, int status=0, string term=null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_GetExamQuestionUploaded";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@iddanesh", iddanesh);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@term", term);

            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception x) { throw x; }
            finally { if (con.State == ConnectionState.Open) con.Close(); }
            return dt;
        }
        public DataTable GetPreviusExamQuestions(int course = 0, string term = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_GetPreviusExamQuestions";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@course", course);
            cmd.Parameters.AddWithValue("@term", term);

            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception x) { throw x; }
            return dt;
        }
        public DataTable GetmaxminExamplace(string Examplace, int did)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_GetmaxminExamplace";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamPlace", Examplace);
            cmd.Parameters.AddWithValue("@did", did);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;

        }

        public DataTable GetExamQuestionsbyDid(string did, string term, int? cityId = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Exam].[SP_GetExamQuestionsbyDid]";
            cmd.Parameters.AddWithValue("@did", did);
            cmd.Parameters.AddWithValue("@term", term);
            cmd.Parameters.AddWithValue("@cityId", cityId);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }
        public DataTable GetExamQuestionById(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Exam.SP_GetExamQuestionById";
            cmd.Parameters.AddWithValue("@id", id);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }

        //public DataTable ShowQueizPaperByDid(int did  ,string examDate="-1" ,int? cityId=null)
        public DataTable ShowQueizPaperByDid(string did, int? cityId = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Exam.SP_ShowQueizPaperByDid";
            cmd.Parameters.AddWithValue("@did", did);
            //cmd.Parameters.AddWithValue("@dateExam", examDate);
            cmd.Parameters.AddWithValue("@cityId", cityId);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception x)
            { throw x; }
            return dt;
        }

        public DataTable GetAllDidsToTransferToLms(string term = null, string dateExam = null, string timeExam = null, string did = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Exam.SP_GetAllDidsToTransferToLms";
            cmd.Parameters.AddWithValue("@term", term);
            cmd.Parameters.AddWithValue("@dateExam", dateExam);
            cmd.Parameters.AddWithValue("@timeExam", timeExam);
            cmd.Parameters.AddWithValue("@did", did);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception x)
            { throw x; }
            return dt;
        }

        public bool RemoveExamQuestionAttachment(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Exam.SP_RemoveExamQuestionAttachment";
            cmd.Parameters.AddWithValue("@QuestionId", id);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //###################################
        public bool UpdateExamQuestion_Status(int did, int status, string term)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Exam].[SP_ExamQuestionUpdateStatus]";
            cmd.Parameters.AddWithValue("@did", did);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@term", term);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //###################################

        public bool UpdateExamQuestions(int id, int did = 0, string address = null, int status = 0, string dateSaved = null, string password = null, string term = null,
            bool calc = false, bool note = false, int examTime = 0, string attachmentAddress = null, string rejectDesc = null, bool tempDownload = false,
            bool answerSheet1 = false, bool answerSheet2 = false, bool answerSheet3 = false, string lastModifiedDate = null, bool lawBook = false, string bookName = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Exam.SP_UpdateExamQuestions";
            cmd.Parameters.AddWithValue("@QuestionId", id);
            if (did > 0)
                cmd.Parameters.AddWithValue("@Did", did);
            if (!string.IsNullOrEmpty(address))
                cmd.Parameters.AddWithValue("@Address", did);
            if (status > 0)
                cmd.Parameters.AddWithValue("@Status", status);
            if (!string.IsNullOrEmpty(dateSaved))
                cmd.Parameters.AddWithValue("@DateSaved", dateSaved);
            if (!string.IsNullOrEmpty(password))
                cmd.Parameters.AddWithValue("@Password", password);
            if (!string.IsNullOrEmpty(term))
                cmd.Parameters.AddWithValue("@Term", term);
            if (calc)
                cmd.Parameters.AddWithValue("@Calc", calc);
            if (note)
                cmd.Parameters.AddWithValue("@Note", note);
            if (examTime > 0)
                cmd.Parameters.AddWithValue("@ExamTime", examTime);
            if (!string.IsNullOrEmpty(attachmentAddress))
                cmd.Parameters.AddWithValue("@AttachmentAddress", attachmentAddress);
            if (!string.IsNullOrEmpty(rejectDesc))
                cmd.Parameters.AddWithValue("@RejectDesc", rejectDesc);
            if (tempDownload)
                cmd.Parameters.AddWithValue("@TemplateDownloaded", tempDownload);
            if (answerSheet1)
                cmd.Parameters.AddWithValue("@AnswerSheet1", answerSheet1);
            if (answerSheet2)
                cmd.Parameters.AddWithValue("@AnswerSheet2", answerSheet2);
            if (answerSheet3)
                cmd.Parameters.AddWithValue("@AnswerSheet3", answerSheet3);
            if (!string.IsNullOrEmpty(lastModifiedDate))
                cmd.Parameters.AddWithValue("@LastModifiedDate", lastModifiedDate);
            if (lawBook)
                cmd.Parameters.AddWithValue("@LawBook", lawBook);
            if (!string.IsNullOrEmpty(dateSaved))
                cmd.Parameters.AddWithValue("@bookName", bookName);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataTable ShowQuiezPaper(int iddanesh, int act)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_ShowQuiezPaper";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@iddanesh", iddanesh);
            cmd.Parameters.AddWithValue("@act", act);

            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception x)
            { throw x; }
            return dt;

        }

        public DataTable ShowQuiezPaperHeader(int iddanesh, int status, string date, string time)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_ShowQuiezPaperHeader";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@iddanesh", iddanesh);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@time", time);

            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;

        }

        public void SetApproveNewHeader(int id ,bool? val=null )
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_SetApproveNewHeader";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@val", val);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
        }


        public void InsertIntoExamQuestion(string did, int ExamTime, bool HasCal, bool HasNote, bool ans1, bool ans2, bool ans3, bool LawBook, string BookName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_InsertIntoExamQuestion";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@did", did);
            cmd.Parameters.AddWithValue("@ExamTime", ExamTime);
            cmd.Parameters.AddWithValue("@HasCal", HasCal);
            cmd.Parameters.AddWithValue("@HasNote", HasNote);
            cmd.Parameters.AddWithValue("@ans1", ans1);
            cmd.Parameters.AddWithValue("@ans2", ans2);
            cmd.Parameters.AddWithValue("@ans3", ans3);
            cmd.Parameters.AddWithValue("@LawBook", LawBook);
            cmd.Parameters.AddWithValue("@BookName", BookName);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception xx)
            { throw xx; }

        }

        public DataTable GetDidByCodeOstad(int idostad)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_GetDidByCodeOstad";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idostad", idostad);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();

            }
            catch (Exception x)
            { throw x; }
            return dt;
        }
        //#############################################
        public DataTable GetClassesForOstadByCodeOstad(decimal codeostad, string family, string term)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[SP_GetClassListforOstad_ByCodeOstad]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codeost", codeostad);
            cmd.Parameters.AddWithValue("@ostFamily", family);
            cmd.Parameters.AddWithValue("@term", term);//************
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();

            }
            catch (Exception x)
            { throw x; }
            return dt;
        }



        //#############################################

        public DataTable GetMinStartRange()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_GetMinStartRange";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }


        public DataTable MahalBargozariSans(string dateexam, string saatexam, string cityID, int iddanesh, string selectedTerm)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Exam.SP_MahalBargozariSans";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@dateexam", dateexam);
            cmd.Parameters.AddWithValue("@saatexam", saatexam);
            //cmd.Parameters.AddWithValue("@Name_City", Name_City);
            cmd.Parameters.AddWithValue("@CityID", cityID);
            cmd.Parameters.AddWithValue("@iddanesh", iddanesh);
            cmd.Parameters.AddWithValue("@tterm", selectedTerm);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }

        public DataTable GetAllDaneshkade()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[SP_SelectAllDaneshkade]";
            cmd.Parameters.AddWithValue("@daneshID", 0);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }


        public DataTable GetDidWithoutSeat()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_GetDidWithoutSeat";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();

            }
            catch (Exception)
            { throw; }
            return dt;
        }

        public DataTable ListExaminerExamPlace(string term = "", int examinerId = 0)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_ListExaminerExamPlace";
            cmd.CommandType = CommandType.StoredProcedure;
            if (examinerId > 0)
            {
                cmd.Parameters.AddWithValue("@ExaminerId", examinerId);
                cmd.Parameters.AddWithValue("@Term", term);
            }

            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            return dt;
        }


        public DataTable GetExamPlaceClassByID(int ID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_GetExamPlaceClassByID";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", ID);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();

            }
            catch (Exception)
            { throw; }
            return dt;
        }


        public DataTable GetAllExamPlceClasses()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_GetAllExamPlceClasses";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }
        public DataTable GetAllExamPlceClassesForExaminer(int examinerId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_GetAllExamPlceClassesForExaminer";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExaminerID", examinerId);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception x)
            { throw x; }
            return dt;
        }

        public DataTable StInMojazCart(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_StInMojazCart";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", stcode);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }

        public DataTable CardQuizStudents(string stCode, string Term)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_CardQuiz";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", stCode);
            //     cmd.Parameters.AddWithValue("@term", Term);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable GetExamPlaceAddressByID(int ID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_GetExamPlaceAddressByID";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", ID);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }
        public DataTable GetAllExamPlaceAddress()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Exam.SP_GetStudents_Exam_Place";
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }
        public DataTable GetExamPlaceBySeatAndCity(int SeatNumber, int CityID)
        {

            SqlDataReader dr2;
            string exPlace;

            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = con;
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "Exam.SP_GetExamPlaceBySeatAndCity";
            cmd2.Parameters.AddWithValue("@seatnumber", SeatNumber);
            cmd2.Parameters.AddWithValue("@CityID", CityID);
            DataTable dt2 = new DataTable();

            try
            {
                con.Open();
                dr2 = cmd2.ExecuteReader();
                dt2.Load(dr2);


                con.Close();
                cmd2.Dispose();
                return dt2;
            }
            catch (Exception)
            {
                throw;
            }


        }
        //checked
        public bool CheckSeatNumberByTerm(int seat, string dateexam, string saatexam, string city)
        {
            try
            {

                SqlCommand cmdSeat = new SqlCommand();
                cmdSeat.CommandType = CommandType.StoredProcedure;
                cmdSeat.CommandText = "Exam.Sp_CheckSeatNumberByTerm";
                cmdSeat.Connection = con;

                cmdSeat.Parameters.AddWithValue("@seat", seat);
                cmdSeat.Parameters.AddWithValue("@dateexam", dateexam);
                cmdSeat.Parameters.AddWithValue("@saatexam", saatexam);
                cmdSeat.Parameters.AddWithValue("@city", city);

                DataTable dt = new DataTable();
                SqlDataReader drSeat;

                con.Open();
                drSeat = cmdSeat.ExecuteReader();

                if (drSeat.HasRows)
                {
                    con.Close();
                    cmdSeat.Dispose();
                    return true;

                }
                else
                {
                    con.Close();
                    cmdSeat.Dispose();
                    return false;

                }




            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool CheckSeatIsAssigned(string did, string stcode, string cityName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_CheckSeatIsAssigned";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Did", did);
            cmd.Parameters.AddWithValue("@StCode", stcode);
            cmd.Parameters.AddWithValue("@CityName", cityName);
            DataTable dtname = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dtname.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            if (dtname.Rows.Count > 0 && Convert.ToInt32(dtname.Rows[0][0]) > 0)
                return true;
            else
                return false;
        }

        public int GetFilledSeats(string cityName, string examDate, string examTime)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_GetFilledSeats";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@examDate", examDate);
            cmd.Parameters.AddWithValue("@examTime", examTime);
            cmd.Parameters.AddWithValue("@CityName", cityName);
            DataTable dtname = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dtname.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            if (dtname.Rows.Count > 0)
                return Convert.ToInt32(dtname.Rows[0][0]);
            else
                return 0;
        }
        //checked
        public DataTable Get_ExamNameCity(int ExaminerID)
        {
            SqlCommand cmdexamcityName = new SqlCommand();
            cmdexamcityName.Connection = con;
            cmdexamcityName.CommandText = "Exam.SP_Exam_NameCity";
            cmdexamcityName.CommandType = CommandType.StoredProcedure;
            cmdexamcityName.Parameters.Add("@ExaminerID", ExaminerID);
            DataTable dtname = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdexamcityName.ExecuteReader();
                dtname.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdexamcityName.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dtname;
        }


        public DataTable GetAllExamPlaceCities(int cityID = -1)
        {
            SqlCommand cmdexamcityName = new SqlCommand();
            cmdexamcityName.Connection = con;
            cmdexamcityName.CommandText = "[Exam].[SP_GetAllExamPlaceCities]";
            cmdexamcityName.CommandType = CommandType.StoredProcedure;
            cmdexamcityName.Parameters.Add("@examPlaceID", cityID);
            DataTable dtname = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdexamcityName.ExecuteReader();
                dtname.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdexamcityName.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dtname;
        }



        public DataTable GetCityNameFilterByExaminerExamPlace()
        {
            SqlCommand cmdexamcityName = new SqlCommand();
            cmdexamcityName.Connection = con;
            cmdexamcityName.CommandText = "Exam.Sp_GetCityNameFilterByExaminerExamPlace";
            cmdexamcityName.CommandType = CommandType.StoredProcedure;
            DataTable dtname = new DataTable();

            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdexamcityName.ExecuteReader();
                dtname.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdexamcityName.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dtname;
        }
        public DataTable GetExaminer(bool notParticipantOnly = false)
        {
            SqlCommand cmdexamcityName = new SqlCommand();
            cmdexamcityName.Connection = con;
            cmdexamcityName.CommandText = "Exam.SP_GetExaminer";
            cmdexamcityName.CommandType = CommandType.StoredProcedure;
            if (notParticipantOnly)
                cmdexamcityName.Parameters.AddWithValue("@NotParticipant", notParticipantOnly);
            DataTable dtname = new DataTable();

            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdexamcityName.ExecuteReader();
                dtname.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdexamcityName.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dtname;
        }

        public DataTable ListAllExamClassParticipants(int examinerId = 0, int id = 0)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_ListAllExamClassParticipants";
            cmd.CommandType = CommandType.StoredProcedure;
            if (examinerId > 0)
                cmd.Parameters.AddWithValue("@ExaminerID", examinerId);
            if (id > 0)
                cmd.Parameters.AddWithValue("@id", id);
            DataTable dtname = new DataTable();

            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dtname.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception x)
            {
                throw x;
            }
            return dtname;

        }

        public void AddOrUpdateExamClassParticipants(int id = 0, string examinerName = null, string examinerUserName = null
            , int? examinerPlaceId = null, int? examinerId = null, string term = null, string password = null, string ePass = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Exam.SP_AddOrUpdateExamClassParticipants";
            cmd.Connection = con;

            if (id > 0)
                cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@ExaminerName", examinerName);
            cmd.Parameters.AddWithValue("@ExaminerUserName", examinerUserName);
            cmd.Parameters.AddWithValue("@ExaminerPlaceId", examinerPlaceId);
            cmd.Parameters.AddWithValue("@ExaminerId", examinerId);
            cmd.Parameters.AddWithValue("@Term", term);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.AddWithValue("@ePass", ePass);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            cmd.Dispose();
        }
        public void DeleteExamClassParticipants(int examinerId = 0, int id = 0)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Exam.SP_DeleteExamClassParticipants";
            if (examinerId > 0)
                cmd.Parameters.AddWithValue("@ExaminerID", examinerId);
            if (id > 0)
                cmd.Parameters.AddWithValue("@id", id);
            cmd.Connection = con;


            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            cmd.Dispose();
        }

        public DataTable GetExaminerUser()
        {
            SqlCommand cmdexamcityName = new SqlCommand();
            cmdexamcityName.Connection = con;
            cmdexamcityName.CommandText = "Exam.SP_GetExaminerUser";
            cmdexamcityName.CommandType = CommandType.StoredProcedure;
            DataTable dtname = new DataTable();

            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdexamcityName.ExecuteReader();
                dtname.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdexamcityName.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dtname;
        }
        //checked
        public void AssignSeatNumberToStudent(int seat, string stcode, string did, string city, string ExamPlace)
        {
            try
            {

                SqlCommand cmdSeat = new SqlCommand();
                cmdSeat.CommandType = CommandType.StoredProcedure;
                cmdSeat.CommandText = "Exam.Sp_AssignSeatToStudent";
                cmdSeat.Connection = con;

                cmdSeat.Parameters.AddWithValue("@SeatNumber", seat);
                cmdSeat.Parameters.AddWithValue("@stcode", stcode);
                cmdSeat.Parameters.AddWithValue("@ClassID", did);
                cmdSeat.Parameters.AddWithValue("@city", city);
                cmdSeat.Parameters.AddWithValue("@place", ExamPlace);

                con.Open();
                cmdSeat.ExecuteNonQuery();
                con.Close();
                cmdSeat.Dispose();


            }
            catch (Exception)
            {

                throw;
            }

        }

        //checked
        public DataTable GetAllStudentByClassInDate(int did)
        {
            try
            {

                SqlCommand cmdStudent = new SqlCommand();
                cmdStudent.CommandType = CommandType.StoredProcedure;
                cmdStudent.CommandText = "Exam.Sp_StudentListByClass";
                cmdStudent.Connection = con;

                cmdStudent.Parameters.AddWithValue("@did", did);


                DataTable dt = new DataTable();
                SqlDataReader drStudent;

                con.Open();
                drStudent = cmdStudent.ExecuteReader();
                dt.Load(drStudent);
                drStudent.Dispose();
                con.Close();
                cmdStudent.Dispose();

                return dt;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public DataTable GetAllStudentByClassInDate(int did, string city)
        {
            try
            {
                SqlCommand cmdStudent = new SqlCommand();
                cmdStudent.CommandType = CommandType.StoredProcedure;
                cmdStudent.CommandText = "Exam.Sp_StudentListByClass";
                cmdStudent.Connection = con;
                cmdStudent.Parameters.AddWithValue("@did", did);
                cmdStudent.Parameters.AddWithValue("@city", city);
                DataTable dt = new DataTable();
                SqlDataReader drStudent;
                con.Open();
                drStudent = cmdStudent.ExecuteReader();
                dt.Load(drStudent);
                drStudent.Dispose();
                con.Close();
                cmdStudent.Dispose();
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //checked
        public DataTable GetAllClassInDate(string dateexam, string saatexam)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Exam.Sp_ClassOrderedList";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@dateexam", dateexam);
            cmd.Parameters.AddWithValue("@saatexam", saatexam);

            DataTable dt = new DataTable();
            SqlDataReader dr;

            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Dispose();
                con.Close();
                cmd.Dispose();
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable ListAllStudentsAndDID(string examDate, string examTime, int cityId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EXAM.ListAllStudentsAndDIDByCityAndDateTime";
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@examDate", examDate);
            cmd.Parameters.AddWithValue("@examTime", examTime);
            cmd.Parameters.AddWithValue("@cityId", cityId);

            DataTable dt = new DataTable();
            SqlDataReader dr;

            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Dispose();
                con.Close();
                cmd.Dispose();
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //checked
        public DataTable Get_Exam_saatexam(bool isCanceledExamBefore = false)
        {
            SqlCommand getexamsaat = new SqlCommand();
            getexamsaat.Connection = con;
            getexamsaat.CommandText = "Exam.SP_Exam_saatexam";
            getexamsaat.CommandType = CommandType.StoredProcedure;
            getexamsaat.Parameters.AddWithValue("@isCanceledExamBefore", isCanceledExamBefore);
            DataTable dtEaxmsaat = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = getexamsaat.ExecuteReader();
                dtEaxmsaat.Load(rdr);
                rdr.Dispose();
                con.Close();
                getexamsaat.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dtEaxmsaat;
        }
        public DataTable Get_Exam_saatexamByTerm(string term)
        {
            SqlCommand getexamsaat = new SqlCommand();
            getexamsaat.Connection = con;
            getexamsaat.CommandText = "Exam.SP_Exam_saatexamByTerm";
            getexamsaat.CommandType = CommandType.StoredProcedure;
            getexamsaat.Parameters.AddWithValue("@term", term);
            DataTable dtEaxmsaat = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = getexamsaat.ExecuteReader();
                dtEaxmsaat.Load(rdr);
                rdr.Dispose();
                con.Close();
                getexamsaat.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dtEaxmsaat;
        }
        //checked
        public DataTable Get_Exam_dateexam()
        {
            SqlCommand getexamdate = new SqlCommand();
            getexamdate.Connection = con;
            getexamdate.CommandText = "Exam.SP_Exam_dateexam";
            getexamdate.CommandType = CommandType.StoredProcedure;
            DataTable dtEaxmdate = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = getexamdate.ExecuteReader();
                dtEaxmdate.Load(rdr);
                rdr.Dispose();
                con.Close();
                getexamdate.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dtEaxmdate;

        }
        public DataTable Get_Exam_dateexam(string term, bool isCanceledExamBefore = false)
        {
            SqlCommand getexamdate = new SqlCommand();
            getexamdate.Connection = con;
            getexamdate.CommandText = "Exam.SP_Exam_dateexamByTerm";
            getexamdate.CommandType = CommandType.StoredProcedure;
            getexamdate.Parameters.AddWithValue("@term", term);
            getexamdate.Parameters.AddWithValue("@isCanceledExamBefore", isCanceledExamBefore);
            DataTable dtEaxmdate = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = getexamdate.ExecuteReader();
                dtEaxmdate.Load(rdr);
                rdr.Dispose();
                con.Close();
                getexamdate.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dtEaxmdate;

        }
        public DataTable Get_Exam_dateexamByTerm(string term)
        {
            SqlCommand getexamdate = new SqlCommand();
            getexamdate.Connection = con;
            getexamdate.CommandText = "Exam.SP_Exam_dateexamByTerm";
            getexamdate.CommandType = CommandType.StoredProcedure;
            getexamdate.Parameters.AddWithValue("@term", term);
            DataTable dtEaxmdate = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = getexamdate.ExecuteReader();
                dtEaxmdate.Load(rdr);
                rdr.Dispose();
                con.Close();
                getexamdate.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dtEaxmdate;

        }



        public DataTable Get_Exam_dateexamByDay(string date)
        {
            SqlCommand getexamdate = new SqlCommand();
            getexamdate.Connection = con;
            getexamdate.CommandText = "[Exam].[SP_Exam_saatexamByDateExam]";
            getexamdate.CommandType = CommandType.StoredProcedure;
            getexamdate.Parameters.AddWithValue("@dateexam", date);
            DataTable dtEaxmdate = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = getexamdate.ExecuteReader();
                dtEaxmdate.Load(rdr);
                rdr.Dispose();
                con.Close();
                getexamdate.Dispose();
            }
            catch (Exception x)
            {
                throw x;
            }
            return dtEaxmdate;

        }



        //checked
        public DataTable Get_ExamReportsByParams(string Examdate, string ExamTime, int ExamPlace, string Prof, string did, int field, int iddanesh, string selectedTerm)
        {
            SqlCommand cmdReport = new SqlCommand();
            cmdReport.CommandTimeout = 300;
            cmdReport.CommandText = "[Exam].[SP_ExamReportsByParams]";
            cmdReport.CommandType = CommandType.StoredProcedure;
            cmdReport.Connection = con;
            cmdReport.Parameters.AddWithValue("@dateexam", Examdate);
            cmdReport.Parameters.AddWithValue("@saatexam", ExamTime);
            cmdReport.Parameters.AddWithValue("@examPlaceId", ExamPlace);
            cmdReport.Parameters.AddWithValue("@ostadid", Prof);
            cmdReport.Parameters.AddWithValue("@did", did);
            cmdReport.Parameters.AddWithValue("@idresh", field);
            //cmdReport.Parameters.AddWithValue("@codedars", 0);
            cmdReport.Parameters.AddWithValue("@iddanesh", iddanesh);
            //cmdReport.Parameters.AddWithValue("@nameCity", nameCity);
            cmdReport.Parameters.AddWithValue("@tterm", selectedTerm);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdReport.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdReport.Dispose();
            }
            catch (Exception exx)
            {
                throw exx;
            }


            return dt;
        }



        public DataTable GetAllSansCoutCity(string term = "", string userId = "-1")
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Exam.SP_Exam_ReportPlaceSans";
            cmd.Parameters.AddWithValue("@Term", term);
            cmd.Parameters.AddWithValue("@UserId", int.Parse(userId));
            cmd.Connection = con;
            DataTable dt = new DataTable();
            SqlDataReader dr;

            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Dispose();
                con.Close();
                cmd.Dispose();
                return dt;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public DataTable GetDidByDateAndSans(string date, string sans, string term)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Exam.SP_GetDidByDateAndSans";
            cmd.Parameters.AddWithValue("@Term", term);
            cmd.Parameters.AddWithValue("@Sans", sans);
            cmd.Parameters.AddWithValue("@Date", date);
            cmd.Connection = con;
            DataTable dt = new DataTable();
            SqlDataReader dr;

            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Dispose();
                con.Close();
                cmd.Dispose();
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetSansByDate(string date, string term)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Exam.SP_GetSansByDate";
            cmd.Parameters.AddWithValue("@Term", term);
            cmd.Parameters.AddWithValue("@Date", date);
            cmd.Connection = con;
            DataTable dt = new DataTable();
            SqlDataReader dr;

            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                dr.Dispose();
                con.Close();
                cmd.Dispose();
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //checked
        public DataTable Get_ExamReportsByPlace(string ExamPlace, int iddanesh, string dateexam, string selectedTerm)
        {
            SqlCommand cmdReport = new SqlCommand();
            cmdReport.CommandText = "Exam.SP_ExamReportsByPlace";
            cmdReport.CommandTimeout = 0;
            cmdReport.CommandType = CommandType.StoredProcedure;
            cmdReport.Connection = con;
            cmdReport.Parameters.AddWithValue("@name_city", ExamPlace);
            cmdReport.Parameters.AddWithValue("@iddanesh", iddanesh);
            cmdReport.Parameters.AddWithValue("@dateexam", dateexam);
            cmdReport.Parameters.AddWithValue("@term", selectedTerm);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader drreport;
                drreport = cmdReport.ExecuteReader();
                dt.Load(drreport);
                drreport.Dispose();
                con.Close();
                cmdReport.Dispose();

            }
            catch (Exception x)
            { throw x; }
            return dt;

        }
        //checked
        public DataTable Get_zarfiat_per_city(string saatexam, string dateexam, int ID)
        {
            SqlCommand cmzarfiat = new SqlCommand();
            cmzarfiat.Connection = con;
            cmzarfiat.CommandText = "Exam.SP_zarfiat_per_cityes"; //"Exam.SP_zarfiat_per_city";
            cmzarfiat.CommandType = CommandType.StoredProcedure;
            cmzarfiat.Parameters.AddWithValue("@saatexam", saatexam);
            cmzarfiat.Parameters.AddWithValue("@dateexam", dateexam);
            cmzarfiat.Parameters.AddWithValue("@ID", ID);
            DataTable zarfiatdt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmzarfiat.ExecuteReader();
                zarfiatdt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmzarfiat.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return zarfiatdt;
        }

        //checked
        public DataTable Get_existcoursecode(int courseCode)
        {
            SqlCommand cmexist = new SqlCommand();
            cmexist.Connection = con;
            cmexist.CommandText = "Exam.SP_existcoursecode";
            cmexist.CommandType = CommandType.StoredProcedure;
            cmexist.Parameters.AddWithValue("@coursecode", courseCode);
            DataTable dtexist = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmexist.ExecuteReader();
                dtexist.Load(rdr);
                rdr.Dispose();
                return dtexist;
                cmexist.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //checked
        public DataTable Get_ZarfiateBaghimande(string did)
        {
            SqlCommand cmdzarfbaghi = new SqlCommand();
            cmdzarfbaghi.Connection = con;
            cmdzarfbaghi.CommandText = "Exam.SP_zarfiatebaghimande";
            cmdzarfbaghi.CommandType = CommandType.StoredProcedure;
            cmdzarfbaghi.Parameters.AddWithValue("@did", did);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdzarfbaghi.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdzarfbaghi.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        public DataTable Get_ZarfiateBaghimande(string did, string cityId)
        {
            SqlCommand cmdzarfbaghi = new SqlCommand();
            cmdzarfbaghi.Connection = con;
            cmdzarfbaghi.CommandText = "Exam.SP_zarfiatebaghimande";
            cmdzarfbaghi.CommandType = CommandType.StoredProcedure;
            cmdzarfbaghi.Parameters.AddWithValue("@did", did);
            cmdzarfbaghi.Parameters.AddWithValue("@city", cityId);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdzarfbaghi.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdzarfbaghi.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }


        public DataTable Get_ZarfiateBaghimande22(string did, string cityId)
        {
            SqlCommand cmdzarfbaghi = new SqlCommand();
            cmdzarfbaghi.Connection = con;
            cmdzarfbaghi.CommandText = "Exam.SP_zarfiatebaghimande_22";
            cmdzarfbaghi.CommandType = CommandType.StoredProcedure;
            cmdzarfbaghi.Parameters.AddWithValue("@did", did);
            cmdzarfbaghi.Parameters.AddWithValue("@cityId", cityId);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdzarfbaghi.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdzarfbaghi.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        //checked
        public DataTable Get_ExamPlaceName()
        {
            SqlCommand cmexamplace = new SqlCommand();
            cmexamplace.Connection = con;
            cmexamplace.CommandText = "Exam.SP_ExamPlaceName";
            cmexamplace.CommandType = CommandType.StoredProcedure;
            DataTable dtexplace = new DataTable();

            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmexamplace.ExecuteReader();
                dtexplace.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmexamplace.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            return dtexplace;
        }
        public DataTable Get_ExamPlaceName(string cityName)
        {
            SqlCommand cmexamplace = new SqlCommand();
            cmexamplace.Connection = con;
            cmexamplace.CommandText = "Exam.SP_ExamPlaceName";
            cmexamplace.CommandType = CommandType.StoredProcedure;
            DataTable dtexplace = new DataTable();
            cmexamplace.Parameters.AddWithValue("@city", cityName);
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmexamplace.ExecuteReader();
                dtexplace.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmexamplace.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            return dtexplace;
        }
        //checked
        public DataTable Get_ListClassCityByDate(int iddanesh, string tterm, string ExamDate, string ExamTime, int examinerID = -1)
        {
            SqlCommand cmdList = new SqlCommand();
            cmdList.Connection = con;
            cmdList.CommandText = "Exam.Sp_GetListClassCityByDate";
            cmdList.CommandType = CommandType.StoredProcedure;
            cmdList.Parameters.AddWithValue("@iddanesh", iddanesh);
            cmdList.Parameters.AddWithValue("@tterm", tterm);
            cmdList.Parameters.AddWithValue("@dateexam", ExamDate);
            cmdList.Parameters.AddWithValue("@saatexam", ExamTime);
            cmdList.Parameters.AddWithValue("@examinerID", examinerID);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdList.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdList.Dispose();
            }
            catch (Exception x)
            {
                throw x;
            }

            return dt;
        }


        //checked
        public int Insert_ExamClassSaved(string did, int ExamPlaceID, int StartRange, int EndRange, string City_Name)
        {
            int porshode1;
            SqlCommand cmdexamclassSave = new SqlCommand();
            cmdexamclassSave.Connection = con;
            cmdexamclassSave.CommandText = "Exam.SP_Ins_ExamClassSaved";
            cmdexamclassSave.CommandType = CommandType.StoredProcedure;
            cmdexamclassSave.Parameters.AddWithValue("@did", did);
            cmdexamclassSave.Parameters.AddWithValue("@ExamPlaceID", ExamPlaceID);
            cmdexamclassSave.Parameters.AddWithValue("@StartRange", StartRange);
            cmdexamclassSave.Parameters.AddWithValue("@EndRange", EndRange);
            cmdexamclassSave.Parameters.AddWithValue("@City_Name", City_Name);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                porshode1 = (int)cmdexamclassSave.ExecuteScalar();
                con.Close();
                cmdexamclassSave.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return porshode1;
        }
        //checked
        public int Get_EndRange(int ExamPlaceID)
        {
            int Endrangeid;
            SqlCommand cmdEndRange = new SqlCommand();
            cmdEndRange.Connection = con;
            cmdEndRange.CommandText = "Exam.SP_EndRange";
            cmdEndRange.CommandType = CommandType.StoredProcedure;
            cmdEndRange.Parameters.AddWithValue("@ExamPlaceID", ExamPlaceID);
            SqlParameter returnParameter = cmdEndRange.Parameters.Add("@EndRange", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            try
            {
                con.Open();
                cmdEndRange.ExecuteNonQuery();
                Endrangeid = (int)returnParameter.Value;
                con.Close();
                cmdEndRange.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return Endrangeid;

        }
        //checked
        public int Get_StartRange(int ExamPlaceID, string did)
        {
            int Startrangeid;
            SqlCommand cmdStartRange = new SqlCommand();
            cmdStartRange.Connection = con;
            cmdStartRange.CommandText = "Exam.SP_StartRange";
            cmdStartRange.CommandType = CommandType.StoredProcedure;
            cmdStartRange.Parameters.AddWithValue("@ExamPlaceID", ExamPlaceID);
            cmdStartRange.Parameters.AddWithValue("@did", did);
            SqlParameter returnParameter1 = cmdStartRange.Parameters.Add("@StartRange", SqlDbType.Int);
            returnParameter1.Direction = ParameterDirection.ReturnValue;
            try
            {
                con.Open();
                cmdStartRange.ExecuteNonQuery();
                Startrangeid = (int)returnParameter1.Value;
                con.Close();
                cmdStartRange.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return Startrangeid;


        }

        //checked
        public DataTable Get_SaatDateExam_perID(string did)
        {
            SqlCommand cmdSaatDateExamPerId = new SqlCommand();
            cmdSaatDateExamPerId.CommandText = "Exam.SP_SaatDateExam_perID";
            cmdSaatDateExamPerId.Connection = con;
            cmdSaatDateExamPerId.CommandType = CommandType.StoredProcedure;
            cmdSaatDateExamPerId.Parameters.AddWithValue("@did", did);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdSaatDateExamPerId.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdSaatDateExamPerId.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        //checked
        public DataTable Get_MaxEndRange(string saatexam, string dateexam, int ExamPlaceId)
        {
            SqlCommand cmdmax = new SqlCommand();
            cmdmax.Connection = con;
            cmdmax.CommandText = "Exam.SP_Max_EndRange";
            cmdmax.CommandType = CommandType.StoredProcedure;
            cmdmax.Parameters.AddWithValue("@saatexam", saatexam);
            cmdmax.Parameters.AddWithValue("@dateexam", dateexam);
            cmdmax.Parameters.AddWithValue("@ExamPlaceID", ExamPlaceId);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdmax.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdmax.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;

        }
        //checked
        public DataTable Check_Noduplicate_did(string ClassID)
        {
            SqlCommand cmdnodop = new SqlCommand();
            cmdnodop.CommandText = "Exam.SP_NO_duplicate_did";
            cmdnodop.Connection = con;
            cmdnodop.CommandType = CommandType.StoredProcedure;
            cmdnodop.Parameters.AddWithValue("@ClassID", ClassID);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdnodop.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdnodop.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable Check_Noduplicate_did(string ClassID, string CityName)
        {
            SqlCommand cmdnodop = new SqlCommand();
            cmdnodop.CommandText = "Exam.SP_NO_duplicate_did";
            cmdnodop.Connection = con;
            cmdnodop.CommandType = CommandType.StoredProcedure;
            cmdnodop.Parameters.AddWithValue("@ClassID", ClassID);
            cmdnodop.Parameters.AddWithValue("@CityName", CityName);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdnodop.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdnodop.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        //checked
        /// <summary>
        /// این متد برای تشخیص اینکه آیا برای این کد کلاس تخصیص صندلی ایجاد شده یا نه، به کار میرود
        /// </summary>
        /// <param name="did">The did.</param>
        /// <returns></returns>
        public DataTable Get_ExistClass(string did)
        {
            SqlCommand cmdexist = new SqlCommand();
            cmdexist.CommandText = "Exam.SP_ExistClass";
            cmdexist.CommandType = CommandType.StoredProcedure;
            cmdexist.Connection = con;
            cmdexist.Parameters.AddWithValue("@did", did);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdexist.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdexist.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        //checked
        /// <summary>
        /// این متد جزئیات یک کد درس را بر می گرداند
        /// </summary>
        /// <param name="did">The did.</param>
        /// <returns></returns>
        public DataTable Get_did_detail(string did)
        {
            SqlCommand cmddiddetail = new SqlCommand();
            cmddiddetail.Connection = con;
            cmddiddetail.CommandText = "Exam.SP_select_did_detail";
            cmddiddetail.CommandType = CommandType.StoredProcedure;
            cmddiddetail.Parameters.AddWithValue("@did", did);
            cmddiddetail.Parameters.AddWithValue("@ID", 1);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmddiddetail.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmddiddetail.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            return dt;

        }
        //checked
        /// <summary>
        /// این متد بر اساس تاریخ امتحان و کد درس ، شماره دانشجویی را بر می گرداند
        /// </summary>
        /// <param name="did">The did.</param>
        /// <param name="saatexam">The saatexam.</param>
        /// <param name="dateexam">The dateexam.</param>
        /// <returns></returns>
        public DataTable Get_Stcode(int did, string saatexam, string dateexam)
        {
            SqlCommand cmdselst = new SqlCommand();
            cmdselst.Connection = con;
            cmdselst.CommandText = "Exam.SP_select_stcode";
            cmdselst.CommandType = CommandType.StoredProcedure;
            cmdselst.Parameters.AddWithValue("@did", did);
            cmdselst.Parameters.AddWithValue("@saatexam", saatexam);
            cmdselst.Parameters.AddWithValue("@dateexam", dateexam);
            cmdselst.Parameters.AddWithValue("@ID", 1);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdselst.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdselst.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;

        }


        public DataTable GetStudentByDidAndExamPlace(string did, int idexamPlace)
        {
            SqlCommand cmdselst = new SqlCommand();
            cmdselst.Connection = con;
            cmdselst.CommandText = "Exam.SP_GetStudentByDidAndExamPlace";
            cmdselst.CommandType = CommandType.StoredProcedure;
            cmdselst.Parameters.AddWithValue("@did", did);
            cmdselst.Parameters.AddWithValue("@ID", idexamPlace);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdselst.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdselst.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;

        }
        //checked
        /// <summary>
        /// این متد تعداد دانشجو را بر می گرداند
        /// </summary>
        /// <param name="did">The did.</param>
        /// <param name="saatexam">The saatexam.</param>
        /// <param name="dateexam">The dateexam.</param>
        /// <returns></returns>
        public int Get_tedad_daneshju(int did, string saatexam, string dateexam)
        {
            int tedad_daneshju;
            SqlCommand cmdgetstcode = new SqlCommand();
            cmdgetstcode.Connection = con;
            cmdgetstcode.CommandText = "Exam.SP_tedad_daneshju";
            cmdgetstcode.CommandType = CommandType.StoredProcedure;
            cmdgetstcode.Parameters.AddWithValue("@did", did);
            cmdgetstcode.Parameters.AddWithValue("@saatexam", saatexam);
            cmdgetstcode.Parameters.AddWithValue("@dateexam", dateexam);
            cmdgetstcode.Parameters.AddWithValue("@ID", 1);
            SqlParameter returnParameter4 = cmdgetstcode.Parameters.Add("@tedad", SqlDbType.Int);
            returnParameter4.Direction = ParameterDirection.ReturnValue;
            try
            {
                con.Open();
                cmdgetstcode.ExecuteScalar();
                tedad_daneshju = (int)returnParameter4.Value;
                cmdgetstcode.Dispose();
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return tedad_daneshju;
        }

        //checked
        /// <summary>
        ///این متد تعداد کلاس را بر می گرداند
        /// </summary>
        /// <param name="did">The did.</param>
        /// <returns></returns>
        public int Get_tedad_class(int did)
        {
            int tedad_class;
            SqlCommand cmdtedadClasss = new SqlCommand();
            cmdtedadClasss.Connection = con;
            cmdtedadClasss.CommandText = "Exam.SP_tedad_class";
            cmdtedadClasss.CommandType = CommandType.StoredProcedure;
            cmdtedadClasss.Parameters.AddWithValue("@did", did);
            SqlParameter returnParameter5 = cmdtedadClasss.Parameters.Add("@tedadclass", SqlDbType.Int);
            returnParameter5.Direction = ParameterDirection.ReturnValue;
            try
            {
                con.Open();
                cmdtedadClasss.ExecuteNonQuery();
                tedad_class = (int)returnParameter5.Value;
                con.Close();
                cmdtedadClasss.Dispose();
            }
            catch (Exception)
            {
                throw;
            }

            return tedad_class;
        }
        //checked
        /// <summary>
        /// جزئیات کلاس های ذخیره شده را بر می گرداند
        /// </summary>
        /// <param name="did">The did.</param>
        /// <returns></returns>
        public DataTable Get_ExamClassSavedDetail(string did)
        {
            SqlCommand cmdsaveddetail = new SqlCommand();
            cmdsaveddetail.Connection = con;
            cmdsaveddetail.CommandText = "Exam.SP_select_examclasssaved_detail";
            cmdsaveddetail.CommandType = CommandType.StoredProcedure;
            cmdsaveddetail.Parameters.AddWithValue("@did", did);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdsaveddetail.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdsaveddetail.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        public DataTable Get_ExamClassSavedDetail(string did, string cityName)
        {
            SqlCommand cmdsaveddetail = new SqlCommand();
            cmdsaveddetail.Connection = con;
            cmdsaveddetail.CommandText = "Exam.SP_select_examclasssaved_detail";
            cmdsaveddetail.CommandType = CommandType.StoredProcedure;
            cmdsaveddetail.Parameters.AddWithValue("@did", did);
            cmdsaveddetail.Parameters.AddWithValue("@cityName", cityName);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdsaveddetail.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdsaveddetail.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        public DataTable CheckIsClassSetRange(string did)
        {
            SqlCommand cmdsaveddetail = new SqlCommand();
            cmdsaveddetail.Connection = con;
            cmdsaveddetail.CommandText = "Exam.SP_Check_IsClassSetRange";
            cmdsaveddetail.CommandType = CommandType.StoredProcedure;
            cmdsaveddetail.Parameters.AddWithValue("@did", did);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdsaveddetail.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdsaveddetail.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable CheckIsClassSetRange(string did, int zarf=0, int cityId=-1)
        {
            SqlCommand cmdsaveddetail = new SqlCommand();
            cmdsaveddetail.Connection = con;
            cmdsaveddetail.CommandText = "Exam.SP_Check_IsClassSetRange";
            cmdsaveddetail.CommandType = CommandType.StoredProcedure;
            cmdsaveddetail.Parameters.AddWithValue("@did", did);
            cmdsaveddetail.Parameters.AddWithValue("@zarf", zarf);
            cmdsaveddetail.Parameters.AddWithValue("@cityId", cityId);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdsaveddetail.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdsaveddetail.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        public DataTable CheckIsClassSetRange(List<string> classIDs, int cityId=-1)
        {
            SqlCommand cmdsaveddetail = new SqlCommand();
            cmdsaveddetail.Connection = con;
            cmdsaveddetail.CommandText = "Exam.SP_Check_IsClassSetRangeForList";
            cmdsaveddetail.CommandType = CommandType.StoredProcedure;

           // PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(int));
            DataTable table = new DataTable();
            table.Columns.Add("classID", typeof(string));
            foreach (string classID in classIDs)
            {
                DataRow row = table.NewRow();
                row["classID"] = classID;
                table.Rows.Add(row);
            }

            cmdsaveddetail.Parameters.Add("@classIDs", SqlDbType.Structured);
            cmdsaveddetail.Parameters["@classIDs"].Direction = ParameterDirection.Input;
            cmdsaveddetail.Parameters["@classIDs"].TypeName = "[dbo].[TVP_ClassIDs]";
            cmdsaveddetail.Parameters["@classIDs"].Value = table;
            cmdsaveddetail.Parameters.AddWithValue("@cityId",  cityId);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdsaveddetail.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdsaveddetail.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }


        public DataTable CheckExamPlaceOverlap(int startRange, int endRange, string cityName, string term, int examPlaceId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_CheckExamPlaceOverlap";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StartRange", startRange);
            cmd.Parameters.AddWithValue("@EndRange", endRange);
            cmd.Parameters.AddWithValue("@CityName", cityName);
            cmd.Parameters.AddWithValue("@Term", term);
            cmd.Parameters.AddWithValue("@ExamPlaceId", examPlaceId);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        public DataTable GetExamPaperStatus(string did)
        {
            SqlCommand cmdsaveddetail = new SqlCommand();
            cmdsaveddetail.Connection = con;
            cmdsaveddetail.CommandText = "Exam.SP_GetExamPaperStatus";
            cmdsaveddetail.CommandType = CommandType.StoredProcedure;
            cmdsaveddetail.Parameters.AddWithValue("@did", did);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdsaveddetail.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdsaveddetail.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
            return dt;


        }

        //checked
        /// <summary>
        /// این متد شماره صندلی را چک می کند تا به صورت رندوم شماره تکراری تخصیص داده نشود
        /// </summary>
        /// <param name="saatexam">The saatexam.</param>
        /// <param name="dateexam">The dateexam.</param>
        /// <returns></returns>
        public DataTable Get_No_DuplicateSeatNumber(string saatexam, string dateexam, int seatnumber, int classid)
        {
            SqlCommand cmdnodup = new SqlCommand();
            cmdnodup.Connection = con;
            cmdnodup.CommandText = "Exam.SP_No_Duplicate_seatNumber";
            cmdnodup.CommandType = CommandType.StoredProcedure;
            cmdnodup.Parameters.AddWithValue("@saatexam", saatexam);
            cmdnodup.Parameters.AddWithValue("@dateexam", dateexam);
            cmdnodup.Parameters.AddWithValue("@seatnumber", seatnumber);
            cmdnodup.Parameters.AddWithValue("@classid", classid);

            DataTable dtn = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdnodup.ExecuteReader();
                dtn.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdnodup.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dtn;
        }



        //checked
        /// <summary>
        /// این متد کلاس های امتحان را بر اساس کد درس استخراج می نماید
        /// </summary>
        /// <param name="did">The did.</param>
        /// <returns></returns>
        public DataTable Get_ExamPlaceName(int did)
        {
            SqlCommand cmdexampName = new SqlCommand();
            cmdexampName.Connection = con;
            cmdexampName.CommandText = "Exam.SP_ExamPlaceNames";
            cmdexampName.CommandType = CommandType.StoredProcedure;
            cmdexampName.Parameters.AddWithValue("@did", did);
            DataTable dtpname = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdexampName.ExecuteReader();
                dtpname.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdexampName.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dtpname;
        }

        public DataTable Get_ExamdetailbyDid(string did, string term = null, int? cityId = null)
        {
            SqlCommand cmdexampName = new SqlCommand();
            cmdexampName.Connection = con;
            cmdexampName.CommandText = "Exam.SP_ExamdetailbyDid";
            cmdexampName.CommandType = CommandType.StoredProcedure;
            cmdexampName.Parameters.AddWithValue("@did", did);
            cmdexampName.Parameters.AddWithValue("@term", term);
            cmdexampName.Parameters.AddWithValue("@cityId", cityId);
            DataTable dtpname = new DataTable();
            SqlDataReader rdr = null;
            try
            {
                if (con.State == ConnectionState.Open) con.Close();
                con.Open();
                rdr = cmdexampName.ExecuteReader();
                dtpname.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdexampName.Dispose();
            }
            catch (Exception x)
            {                
            }
            return dtpname;
        }

        //ramezanian
        public DataTable GetMobileProfByDid(string did, string Term)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[SP_GetMobileProfByDid]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@did", did);
            cmd.Parameters.AddWithValue("@term", Term);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch
            {
                //throw;
                return null;
            }
            return dt;
        }
        public DataTable SendEmailSMSToExaminer(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[SP_SendEmailSMSToExaminer]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch
            {
                throw;
            }
            return dt;
        }
        public DataTable GetExamClassParticipantsByUserName(string userName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[Exam.GetExamClassParticipantsByUserName]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userName", userName);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch
            {
                throw;
            }
            return dt;
        }
        public PollDTO GetPollById(int pollId)
        {
            var res = new PollDTO();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[GetPollById]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PollId", pollId);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch
            {
                throw;
            }
            if (dt.Rows.Count > 0)
            {
                res.Description = dt.Rows[0]["Description"].ToString();
                res.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                res.Term = dt.Rows[0]["Term"].ToString();
                res.Title = dt.Rows[0]["Title"].ToString();
                res.PollType = Convert.ToInt32(dt.Rows[0]["PollType"]);
                res.NeedComment = Convert.ToBoolean(dt.Rows[0]["NeedComment"]);
                if (dt.Rows[0]["FromDate"] != DBNull.Value)
                    res.FromDate = Convert.ToDateTime(dt.Rows[0]["FromDate"]);
                if (dt.Rows[0]["ToDate"] != DBNull.Value)
                    res.ToDate = Convert.ToDateTime(dt.Rows[0]["ToDate"]);
            }
            return res;
        }

        public PollDTO GetPollByTypeAndTerm(int type, string term)
        {
            var res = new PollDTO();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[GetPollByTypeAndTerm]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PollType", type);
            cmd.Parameters.AddWithValue("@PollTerm", term);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch
            {
                throw;
            }
            if (dt.Rows.Count > 0)
            {
                res.Description = dt.Rows[0]["Description"].ToString();
                res.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                res.Term = dt.Rows[0]["Term"].ToString();
                res.Title = dt.Rows[0]["Title"].ToString();
                res.NeedComment = Convert.ToBoolean(dt.Rows[0]["NeedComment"]);
                res.PollType = Convert.ToInt32(dt.Rows[0]["PollType"]);
                if (dt.Rows[0]["FromDate"] != DBNull.Value)
                    res.FromDate = Convert.ToDateTime(dt.Rows[0]["FromDate"]);
                if (dt.Rows[0]["ToDate"] != DBNull.Value)
                    res.ToDate = Convert.ToDateTime(dt.Rows[0]["ToDate"]);
            }
            return res;
        }

        public List<PollQuestionDTO> GetQuestionsOfPoll(int pollId)
        {
            var res = new List<PollQuestionDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[GetQuestionsOfPoll]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PollId", pollId);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch
            {
                throw;
            }
            foreach (DataRow row in dt.Rows)
            {
                res.Add(new PollQuestionDTO
                {
                    Id = Convert.ToInt32(row["Id"]),
                    NeedComment = Convert.ToBoolean(row["NeedComment"]),
                    PollId = Convert.ToInt32(row["PollId"]),
                    Question = row["Question"].ToString(),
                    PollOptions = GetOptionsOfPollQuestion(Convert.ToInt32(row["Id"]))
                });
            }
            return res;
        }
        public List<PollOptionDTO> GetOptionsOfPollQuestion(int questionId)
        {
            var res = new List<PollOptionDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[GetOptionsOfPollQuestion]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@QuestionId", questionId);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch
            {
                throw;
            }
            foreach (DataRow row in dt.Rows)
            {
                res.Add(new PollOptionDTO
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Option = row["Option"].ToString(),
                    Point = Convert.ToInt32(row["Point"]),
                    PollQuestionId = Convert.ToInt32(row["PollQuestionId"])
                });
            }
            return res;
        }
        public List<PollAnswerDTO> GetUserPollAnswer(int userId, int pollId, string target)
        {
            var res = new List<PollAnswerDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[GetUserPollAnswer]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@PollId", pollId);
            cmd.Parameters.AddWithValue("@TargetObject", target);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch
            {
                throw;
            }
            foreach (DataRow row in dt.Rows)
            {
                res.Add(new PollAnswerDTO
                {
                    Comment = row["Comment"].ToString(),
                    Id = Convert.ToInt32(row["Id"]),
                    PollOptionId = Convert.ToInt32(row["PollOptionId"]),
                    TargetObject = row["TargetObject"].ToString(),
                    UserId = Convert.ToInt32(row["UserId"])
                });
            }
            return res;
        }
        public PollQuestionDTO GetQuestionByOptionId(int optionId)
        {
            var res = new PollQuestionDTO();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[SP_GetQuestionByOptionId]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PollOptionId", optionId);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch
            {
                throw;
            }
            if (dt.Rows.Count > 0)
                res = new PollQuestionDTO
                {
                    Id = Convert.ToInt32(dt.Rows[0]["Id"]),
                    NeedComment = Convert.ToBoolean(dt.Rows[0]["NeedComment"]),
                    PollId = Convert.ToInt32(dt.Rows[0]["PollId"]),
                    Question = dt.Rows[0]["Question"].ToString(),
                    PollOptions = GetOptionsOfPollQuestion(Convert.ToInt32(dt.Rows[0]["Id"]))
                };
            return res;
        }

        public List<PollDTO> GetAllPolls()
        {
            var res = new List<PollDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[SP_GetAllPolls]";
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch
            {
                throw;
            }
            foreach (DataRow row in dt.Rows)
            {
                var item = new PollDTO();
                item.Description = row["Description"].ToString();
                item.Id = Convert.ToInt32(row["Id"]);
                item.Term = row["Term"].ToString();
                item.Title = row["Title"].ToString();
                item.NeedComment = Convert.ToBoolean(row["NeedComment"]);
                if (row["FromDate"] != DBNull.Value)
                    item.FromDate = Convert.ToDateTime(row["FromDate"]);
                if (row["ToDate"] != DBNull.Value)
                    item.ToDate = Convert.ToDateTime(row["ToDate"]);
                res.Add(item);
            }
            return res;
        }

        public DataTable GetPollAnswersByTermAndCityId(string term, int cityId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[SP_GetPollAnswersByTermAndCityId]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term", term);
            cmd.Parameters.AddWithValue("@CityId", cityId);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch
            {
                throw;
            }
            return dt;
        }
        public List<PollAnswerDTO> GetPollAnswersByTermAndCityId(string term, int cityId, bool showDetails)
        {
            var res = new List<PollAnswerDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[SP_GetPollAnswersByTermAndCityId]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term", term);
            cmd.Parameters.AddWithValue("@CityId", cityId);
            cmd.Parameters.AddWithValue("@ShowDetails", showDetails);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch
            {
                throw;
            }

            foreach (DataRow row in dt.Rows)
            {
                res.Add(new PollAnswerDTO
                {
                    Comment = row["Comment"].ToString(),
                    Id = Convert.ToInt32(row["Id"]),
                    PollOptionId = Convert.ToInt32(row["PollOptionId"]),
                    TargetObject = row["TargetObject"].ToString(),
                    UserId = Convert.ToInt32(row["UserId"])
                });
            }
            return res;
        }
        public PollCommentDTO GetPollComment(int pollId, int targetId)
        {
            var res = new PollCommentDTO();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[SP_GetPollComment]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PollId", pollId);
            cmd.Parameters.AddWithValue("@TargetId", targetId.ToString());
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch
            {
                throw;
            }
            if (dt.Rows.Count > 0)
            {
                res.Comment = dt.Rows[0]["Comment"].ToString();
                res.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                res.PollId = Convert.ToInt32(dt.Rows[0]["PollId"]);
                res.TargetObject = dt.Rows[0]["TargetObject"].ToString();
                res.UserId = Convert.ToInt32(dt.Rows[0]["UserId"]);
            }
            return res;
        }
        public int CheckPollExistForTerm(int pollId, string term, int pollType)
        {
            var res = new PollCommentDTO();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[SP_CheckPollExistForTerm]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PollId", pollId);
            cmd.Parameters.AddWithValue("@Term", term);
            if (pollType > 0)
                cmd.Parameters.AddWithValue("@PollType", pollType);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch
            {
                throw;
            }
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["Id"]);
            }
            return 0;
        }
        public DataTable GetPollAnswersByQuestion(int questionId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[SP_GetPollAnswersByQuestion]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@QuestionId", questionId);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch
            {
                throw;
            }
            return dt;
        }
        public DataTable GetStudentInfo(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[SP_GetStudentInfo]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", stcode);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmd.Dispose();
            }
            catch(Exception ex)
            {
                throw;
            }
            return dt;
        }

        public DataTable GetProfessorInfoByProfessorCode(string code)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con_hr;
            cmd.CommandText = "[HR].[SP_GetInfoPeopleByProfessorCode]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProfCode", code);
            DataTable dt = new DataTable();
            try
            {
                con_hr.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con_hr.Close();
                cmd.Dispose();
            }
            catch
            {
                throw;
            }
            return dt;
        }


        #endregion

        #region Create

        public void InsertExamPlaceClass(string ExamPlace, int StartRange, int EndRange, string City_Name, int CityID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_InsertExamPlaceClass";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamPlace", ExamPlace);
            cmd.Parameters.AddWithValue("@StartRange", StartRange);
            cmd.Parameters.AddWithValue("@EndRange", EndRange);
            cmd.Parameters.AddWithValue("@City_Name", City_Name);
            cmd.Parameters.AddWithValue("@CityID", CityID);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
        }

        public void Insert_ExaminerInfo(int ExaminerID, int ExamPlaceID, string ExaminerName, string Mobile, string Email, string StartDate, string EndDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_Insert_ExaminerInfo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExaminerID", ExaminerID);
            cmd.Parameters.AddWithValue("@ExamPlaceID", ExamPlaceID);
            cmd.Parameters.AddWithValue("@ExaminerName", ExaminerName);
            cmd.Parameters.AddWithValue("@Mobile", Mobile);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@StartDate", StartDate);
            cmd.Parameters.AddWithValue("@EndDate", EndDate);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
        }

        public void InsertExamPlaceAddress(string cityname, string address)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_InsertExamPlaceAddress";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name_City", cityname);
            cmd.Parameters.AddWithValue("@Address", address);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //checked
        /// <summary>
        /// این متد شماره صندلی را در جدول ذخیره می کند
        /// </summary>
        /// <param name="StudentCode">The student code.</param>
        /// <param name="ClassID">The class identifier.</param>
        /// <param name="seatnumber">The seatnumber.</param>
        /// <param name="City">The city.</param>
        /// <param name="ExamPlace">The exam place.</param>
        public void Insert_ExamSeat(string StudentCode, int ClassID, int seatnumber, string City)
        {
            SqlCommand cmdins = new SqlCommand();
            cmdins.Connection = con;
            cmdins.CommandText = "Exam.SP_insert_ExamSeat";
            cmdins.CommandType = CommandType.StoredProcedure;
            cmdins.Parameters.AddWithValue("@StudentCode", StudentCode);
            cmdins.Parameters.AddWithValue("@ClassID", ClassID);
            cmdins.Parameters.AddWithValue("@SeatNumber", seatnumber);
            cmdins.Parameters.AddWithValue("@City", City);

            try
            {
                con.Open();
                cmdins.ExecuteNonQuery();
                con.Close();
                cmdins.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //checked
        /// <summary>
        /// این متد گزارش امتحانات را بر اساس ساعت و تاریخ و محل بر می گرداند
        /// </summary>
        /// <param name="Examdate">The examdate.</param>
        /// <param name="ExamTime">The exam time.</param>
        /// <param name="ExamPlace">The exam place.</param>
        /// <returns></returns>
        public DataTable Get_ExamReports(string Examdate, string ExamTime, string ExamPlaceId, int iddanesh, string selectedTerm)
        {
            SqlCommand cmdRep = new SqlCommand();
            cmdRep.CommandText = "Exam.SP_ExamReports";
            cmdRep.Connection = con;
            cmdRep.CommandType = CommandType.StoredProcedure;
            cmdRep.Parameters.AddWithValue("@dateexam", Examdate);
            cmdRep.Parameters.AddWithValue("@saatexam", ExamTime);
            //cmdRep.Parameters.AddWithValue("@Name_City", ExamPlace);
            cmdRep.Parameters.AddWithValue("@examPlaceId", ExamPlaceId);
            cmdRep.Parameters.AddWithValue("@iddanesh", iddanesh);
            cmdRep.Parameters.AddWithValue("@term", selectedTerm);


            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdRep.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdRep.Dispose();
            }
            catch (Exception)
            {
                //throw;
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
            return dt;

        }
        //checked
        /// <summary>
        ///این متد کد درسهای ترم جاری را بر می گرداند
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Exam_Did_Interm(string term)
        {
            SqlCommand cmdDidInterm = new SqlCommand();
            cmdDidInterm.Connection = con;
            cmdDidInterm.CommandText = "Exam.SP_Exam_did_Interm";
            cmdDidInterm.CommandType = CommandType.StoredProcedure;
            cmdDidInterm.Parameters.AddWithValue("@term", term);
            DataTable dtid = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdDidInterm.ExecuteReader();
                dtid.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdDidInterm.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dtid;

        }
        //checked
        /// <summary>
        /// این متد رشته ها را بر می گرداند
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Exam_Fresh()
        {
            SqlCommand cmdfresh = new SqlCommand();
            cmdfresh.Connection = con;
            cmdfresh.CommandText = "Exam.SP_Exam_fresh";
            cmdfresh.CommandType = CommandType.StoredProcedure;
            DataTable dtfresh = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdfresh.ExecuteReader();
                dtfresh.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdfresh.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dtfresh;

        }
        /// <summary>
        /// مشخصات اساتید را بر اساس ترم جاری بر می گرداند
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Exam_Ostad(string term)
        {
            SqlCommand cmdExOst = new SqlCommand();
            cmdExOst.Connection = con;
            cmdExOst.CommandText = "Exam.SP_Exam_Ostad";
            cmdExOst.CommandType = CommandType.StoredProcedure;
            cmdExOst.Parameters.AddWithValue("@term", term);
            DataTable dtex = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdExOst.ExecuteReader();
                dtex.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdExOst.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dtex;
        }

        //checked
        /// <summary>
        /// این متد مشخصات کامل محل امتحان را استخراج می نماید 
        /// </summary>
        /// <returns></returns>
        public DataTable Get_St_ExamPlace()
        {
            SqlCommand cmdexPlace = new SqlCommand();
            cmdexPlace.Connection = con;
            cmdexPlace.CommandText = "Exam.SP_GetStudents_Exam_Place";
            cmdexPlace.CommandType = CommandType.StoredProcedure;
            DataTable dtexPlace = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdexPlace.ExecuteReader();
                dtexPlace.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdexPlace.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dtexPlace;
        }

        //checked
        /// <summary>
        /// این متد نام شهر امتحان دانشجویان را بر می گرداند
        /// </summary>
        /// <param name="stcode">The stcode.</param>
        /// <returns></returns>
        public DataTable Get_Student_CityName(string stcode, bool? change = null)
        {
            SqlCommand cmdGetCity = new SqlCommand();
            cmdGetCity.Connection = con;
            cmdGetCity.CommandText = "Exam.SP_Exam_Get_StudentCityName";
            cmdGetCity.CommandType = CommandType.StoredProcedure;
            cmdGetCity.Parameters.AddWithValue("@stcode", stcode);
            cmdGetCity.Parameters.AddWithValue("@change", change);
            DataTable dtst = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdGetCity.ExecuteReader();
                dtst.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdGetCity.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dtst;
        }
        //checked
        /// <summary>
        ///تعداد دانشجویان یک محل امتحانی را بر می گرداند
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public DataTable Get_st_in_Examplace(int id, string term = "")
        {
            SqlCommand cmdstid = new SqlCommand();
            cmdstid.Connection = con;
            cmdstid.CommandType = CommandType.StoredProcedure;
            cmdstid.CommandText = "Exam.SP_Get_st_in_Examplace";
            cmdstid.Parameters.AddWithValue("@Id", id);
            cmdstid.Parameters.AddWithValue("@Term", term);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr;
                rdr = cmdstid.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                con.Close();
                cmdstid.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        public bool AddOrUpdateExamPaper(int examPaperId = 0, string trackNumber = null, int examPlaceId = 0, string examDate = null,
            string examTime = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_AddOrUpdateExamPaper";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamPaperId", examPaperId);
            cmd.Parameters.AddWithValue("@ExamPlaceId", examPlaceId);
            cmd.Parameters.AddWithValue("@ExamDate", examDate);
            cmd.Parameters.AddWithValue("@ExamTime", examTime);
            cmd.Parameters.AddWithValue("@TrackNumber", trackNumber);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool AddOrUpdatePollAnswer(PollAnswerDTO answer)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_AddOrUpdatePollAnswer";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", answer.Id);
            cmd.Parameters.AddWithValue("@PollOptionId", answer.PollOptionId);
            cmd.Parameters.AddWithValue("@TargetObject", answer.TargetObject);
            cmd.Parameters.AddWithValue("@UserId", answer.UserId);
            if (!string.IsNullOrEmpty(answer.Comment))
                cmd.Parameters.AddWithValue("@Comment", answer.Comment);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool AddOrUpdatePollComment(PollCommentDTO comment)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_AddOrUpdatePollComment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", comment.Id);
            cmd.Parameters.AddWithValue("@PollId", comment.PollId);
            cmd.Parameters.AddWithValue("@TargetObject", comment.TargetObject);
            cmd.Parameters.AddWithValue("@UserId", comment.UserId);
            cmd.Parameters.AddWithValue("@Comment", comment.Comment);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool AddOrUpdatePoll(string title, string term, string description, bool needComment, DateTime? fromDate = null, DateTime? toDate = null, int pollId = 0, int pollType = 0)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_AddOrUpdatePoll";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", pollId);
            cmd.Parameters.AddWithValue("@Title", title);
            cmd.Parameters.AddWithValue("@Term", term);
            cmd.Parameters.AddWithValue("@Description", description);
            cmd.Parameters.AddWithValue("@NeedComment", needComment);
            cmd.Parameters.AddWithValue("@FromDate", fromDate);
            cmd.Parameters.AddWithValue("@ToDate", toDate);
            cmd.Parameters.AddWithValue("@PollType", pollType);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool CopyPoll(int pollId, string term)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_CopyPoll";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", pollId);
            cmd.Parameters.AddWithValue("@Term", term);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool AddOrUpdatePollQuestion(PollQuestionDTO question)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_AddOrUpdatePollQuestion";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", question.Id);
            cmd.Parameters.AddWithValue("@PollId", question.PollId);
            cmd.Parameters.AddWithValue("@NeedComment", question.NeedComment);
            cmd.Parameters.AddWithValue("@Question", question.Question);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool AddOrUpdatePollOption(PollOptionDTO option)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_AddOrUpdatePollOption";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", option.Id);
            cmd.Parameters.AddWithValue("@Option", option.Option);
            cmd.Parameters.AddWithValue("@Point", option.Point);
            cmd.Parameters.AddWithValue("@PollQuestionId", option.PollQuestionId);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Update

        public void UpdateOstadPermission(int code_Ostad)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_UpdateOstadPermission";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@code_Ostad", code_Ostad);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
        }

        public void TemplateDownloaded(string did, bool tdl)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_TemplateDownloaded";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@did", did);
            cmd.Parameters.AddWithValue("@tdl", tdl);
            //cmd.Parameters.AddWithValue("@KeyCode", keycode);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception x)
            { throw x; }
        }

        public void GenerateExamKeyCode(string did)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[SP_GenerateExamKeyCode]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@did", did);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
        }


        public void UploadAttachment(string did, string AttachAddress)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_UploadAttachment";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@did", did);
            cmd.Parameters.AddWithValue("@AttachAddress", AttachAddress);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
        }



        public void UploadExamFile(string Address, string Password, string did, int status)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Exam.SP_UploadExamFile";
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@did", did);
            //cmd.Parameters.AddWithValue("@firstDateUpload", firstDateUpload);
            cmd.Parameters.AddWithValue("@Status", status);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
        }

        public void UpdateExamOption(string did, int ExamTime, bool HasCal, bool HasNote, bool Ans1, bool Ans2, bool Ans3, bool LawBook, string BookName, int? Q2CityId = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_UpdateExamOption";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@did", did);
            cmd.Parameters.AddWithValue("@Examtime", ExamTime);
            cmd.Parameters.AddWithValue("@HasCal", HasCal);
            cmd.Parameters.AddWithValue("@HasNote", HasNote);
            cmd.Parameters.AddWithValue("@Ans1", Ans1);
            cmd.Parameters.AddWithValue("@Ans2", Ans2);
            cmd.Parameters.AddWithValue("@Ans3", Ans3);
            cmd.Parameters.AddWithValue("@LawBook", LawBook);
            cmd.Parameters.AddWithValue("@BookName", BookName);
            cmd.Parameters.AddWithValue("@Q2CityId", Q2CityId);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
        }
        //#############################################
        //#############################################

        public void UpdateQueizStatus(int Status, string Did, string RejectDesc, string address, string attachmentAddress, string Term)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_UpdateQueizStatusForAttachFile";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@did", Did);
            cmd.Parameters.AddWithValue("@RejectDesc", RejectDesc);
            cmd.Parameters.AddWithValue("@Address", address);
            cmd.Parameters.AddWithValue("@AttachmentAddress", attachmentAddress);
            cmd.Parameters.AddWithValue("@term", Term);
            try
            {
                con.Open();
                var a = cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
        }




        public void UpdateQueizStatus(int Status, string Did, string RejectDesc, string Term, bool? approvedHeader = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_UpdateQueizStatus";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Status", Status);
            cmd.Parameters.AddWithValue("@did", Did);
            cmd.Parameters.AddWithValue("@RejectDesc", RejectDesc);
            cmd.Parameters.AddWithValue("@term", Term);
            cmd.Parameters.AddWithValue("@approvedHeader", approvedHeader);
            try
            {
                con.Open();
                var a = cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
        }

        //#############################################
        //#############################################


        public void UpdateExamPlaceClass(string ExamPlace, int StartRange, int EndRange, int ExamPlaceID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_UpdateExamPlaceClass";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamPlace", ExamPlace);
            cmd.Parameters.AddWithValue("@StartRange", StartRange);
            cmd.Parameters.AddWithValue("@EndRange", EndRange);
            cmd.Parameters.AddWithValue("@ExamPlaceID", ExamPlaceID);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
        }

        public void UpdateExamPlaceClass(string ExamPlace, int StartRange, int EndRange, int ExamPlaceID, string CityName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_UpdateExamPlaceClass";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ExamPlace", ExamPlace);
            cmd.Parameters.AddWithValue("@StartRange", StartRange);
            cmd.Parameters.AddWithValue("@EndRange", EndRange);
            cmd.Parameters.AddWithValue("@ExamPlaceID", ExamPlaceID);
            cmd.Parameters.AddWithValue("@CityName", CityName);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
        }

        public void UpdateExamPlaceAddress(string nameCity, string address, int id, bool isActive = false)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_UpdateExamPlaceAddress";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name_City", nameCity);
            cmd.Parameters.AddWithValue("@Address", address);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@IsActive", isActive);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
        }

        /// <summary>
        /// این متد تعداد دانشجوهایی که حوزه امتحانی تهران غرب را انتخاب کرده اند نشان می دهد
        /// </summary>
        /// <param name="examPlaceID"></param>
        /// <returns></returns>

        public int GetCountOfStudentInTehranWestZone(int examPlaceID)
        {
            SqlCommand cmdw = new SqlCommand();
            cmdw.Connection = con;
            cmdw.CommandText = "[Exam].[SP_GetCountOfStudentInTehranWestZone]";
            cmdw.CommandType = CommandType.StoredProcedure;
            cmdw.Parameters.AddWithValue("@ExamPlaceID", examPlaceID);
            var res = 0;

            try
            {
                con.Open();
                res = int.Parse(cmdw.ExecuteScalar().ToString());
                con.Close();
                cmdw.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return res;
        }





        //checked
        /// <summary>
        /// این متد محل امتحان دانشجویان را بروزرسانی می کند
        /// </summary>
        /// <param name="stcode">The stcode.</param>
        /// <param name="Id_ExamPlace">The id_ exam place.</param>
        public void Update_Webmelli_Student_ExamPlace(string stcode, int Id_ExamPlace)
        {
            SqlCommand cmdw = new SqlCommand();
            cmdw.Connection = con;
            cmdw.CommandText = "Exam.SP_Exam_Update_webmeli_students_ExamPlace";
            cmdw.CommandType = CommandType.StoredProcedure;
            cmdw.Parameters.AddWithValue("@stcode", stcode);
            cmdw.Parameters.AddWithValue("@ID_EXAM_PLACE", Id_ExamPlace);

            try
            {
                con.Open();
                cmdw.ExecuteNonQuery();
                con.Close();
                cmdw.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Update_answersheetStatus(int ans, string did)
        {
            SqlCommand cmdw = new SqlCommand();
            cmdw.Connection = con;
            cmdw.CommandText = "Exam.SP_Update_answersheetStatus";
            cmdw.CommandType = CommandType.StoredProcedure;
            cmdw.Parameters.AddWithValue("@ans", ans);
            cmdw.Parameters.AddWithValue("@did", did);

            try
            {
                con.Open();
                cmdw.ExecuteNonQuery();
                con.Close();
                cmdw.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }


        //#############################stp_UpdateSysAvailabilityByParams
        //#############################
        public bool UpdateSysAvailabilityByParams(int appID, Byte userKind, Byte userStatus, string fromDate, string startTime, string toDate, string endTime)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[dbo].[stp_UpdateSysAvailabilityByParams]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@appID", appID);
            cmd.Parameters.AddWithValue("@userKind", userKind);
            cmd.Parameters.AddWithValue("@userStatus", userStatus);
            cmd.Parameters.AddWithValue("@fromDate", fromDate);
            cmd.Parameters.AddWithValue("@startTime", startTime ?? "");
            cmd.Parameters.AddWithValue("@toDate", toDate);
            cmd.Parameters.AddWithValue("@endTime", endTime ?? "");
            try
            {
                con.Open();
                var a = cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception x)
            {
                //throw;
                return false;
            }
        }
        public bool SetSecretariatReceived(string trackNumber = null, string examDate = null, int placeId = 0)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Exam].[SP_SetSecretariatReceived]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TrackNumber", trackNumber);
            cmd.Parameters.AddWithValue("@ExamDate", examDate);
            cmd.Parameters.AddWithValue("@ExamPlaceId", placeId);
            cmd.Parameters.AddWithValue("@SecretariatReceiveDate", DateTime.Now);
            try
            {
                con.Open();
                var a = cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Delete
        public void DeleteFromExamSeat(string saatexam, string dateexam)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_DeleteFromExamSeat";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@saatexam", saatexam);
            cmd.Parameters.AddWithValue("@dateexam", dateexam);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteFromExamSeat(string saatexam, string dateexam, string cityName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_DeleteFromExamSeat";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@saatexam", saatexam);
            cmd.Parameters.AddWithValue("@dateexam", dateexam);
            cmd.Parameters.AddWithValue("@cityName", cityName);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteExamClassSavedById(int Id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_Delete_ExamClassSavedById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);


            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeletePoll(int pollId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_DeletePoll";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PollId", pollId);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeletePollQuestion(int questionId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_DeletePollQuestion";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@QuestionId", questionId);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeletePollOption(int optionId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Exam.SP_DeletePollOption";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OptionId", optionId);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

    }
}
