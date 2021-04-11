using IAUEC_Apps.DAC.Connections;
using IAUEC_Apps.DTO.Evaluation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IAUEC_Apps.DAO.Evaluation
{
    public class EvaluationDAO
    {
        public static readonly string Exam_Term = ConfigurationManager.AppSettings["Exam_Term"];
        #region ConnectionString

        SqlConnection con = new SqlConnection(new SuppConnection().Supp_con);
        #endregion

        public bool IsQuestionExist()
        {
            var res = new List<QuestionDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Evaluation].[GetQuestions]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term", Exam_Term);
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
            return dt.Rows.Count != 0;
        }
        public List<QuestionDTO> GetQuestions()
        {
            var res = new List<QuestionDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Evaluation].[GetQuestions]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term", Exam_Term);
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
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            foreach (DataRow row in dt.Rows)
            {
                res.Add(new QuestionDTO
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Text = row["Text"].ToString(),
                    Description = row["Description"].ToString(),
                    Term = row["Term"].ToString(),
                    IsLastQuestion = row["IsLastQuestion"].ToString() == "True" ? Convert.ToBoolean(row["IsLastQuestion"]) : false,
                    AnswerOfQuestions = GetAnswersOfQuestion(Convert.ToInt32(row["Id"])),
                    SubQuestionDTOs = GetSubQuestionByQuestionId(Convert.ToInt32(row["Id"]))
                });
            }
            return res;
        }
        public List<AnswerOfQuestionDTO> GetAnswersOfQuestion(int questionId)
        {
            var res = new List<AnswerOfQuestionDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Evaluation].[GetAnswersOfQuestion]";
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
                res.Add(new AnswerOfQuestionDTO
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Text = row["Text"].ToString(),
                    Description = row["Description"].ToString(),
                    QuestionId = Convert.ToInt32(row["QuestionId"])
                });
            }
            return res;
        }



        public List<SubQuestionDTO> GetSubQuestionByQuestionId(int questionId)
        {
            var res = new List<SubQuestionDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Evaluation].[GetSubQuestionByQuestionId]";
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
                res.Add(new SubQuestionDTO
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Text = row["Text"].ToString(),
                    QuestionId = Convert.ToInt32(row["QuestionId"])
                });
            }
            return res;
        }

        public bool IsUserevaluated(int userId)
        {
            var res = new List<QuestionDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Evaluation].[GetAnswersOfQuestionByUserId]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@Term", Exam_Term);
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
            return dt.Rows.Count != 0;
        }
        public bool InsertStudentAnswer(StudentAnswerOfQuestionDTO studentAnswerOfQuestion)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Evaluation].[InsertStudentAnswerOfQuestion]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@QuestionId", studentAnswerOfQuestion.QuestionId);
            cmd.Parameters.AddWithValue("@UserId", studentAnswerOfQuestion.UserId);
            cmd.Parameters.AddWithValue("@AnswerOfQuestionId", studentAnswerOfQuestion.AnswerId);
            cmd.Parameters.AddWithValue("@Term ", studentAnswerOfQuestion.Term);
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

        public bool InsertStudentAnswers(List<StudentAnswerOfQuestionDTO> model)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Evaluation].[InsertStudentAnswerOfQuestion]";
            cmd.CommandType = CommandType.StoredProcedure;
            using (var table = new DataTable())
            {

                table.Columns.Add("questionId", typeof(int));
                table.Columns.Add("subQuestionId", typeof(int));
                table.Columns.Add("answerId", typeof(int));
                table.Columns.Add("term", typeof(string));
                table.Columns.Add("userId", typeof(string));

                foreach (var answer in model)
                {
                    var newRow = table.NewRow();
                    newRow["questionId"] = answer.QuestionId;
                    newRow["subQuestionId"] = answer.SubQuestionId;
                    newRow["answerId"] = answer.AnswerId;
                    newRow["term"] = answer.Term;
                    newRow["userId"] = answer.UserId;
                    table.Rows.Add(newRow);
                }

                var pList = new SqlParameter("@studentAnswerT", SqlDbType.Structured);

                pList.TypeName = "[Evaluation].[UT_StudentAnswer]";

                pList.Value = table;

                cmd.Parameters.Add(pList);

            }
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
                con.Close();
                return false;
            }

            //using (var table = new DataTable())
            //{

            //    table.Columns.Add("questionId", typeof(int));
            //    table.Columns.Add("subQuestionId", typeof(int));
            //    table.Columns.Add("answerId", typeof(int));
            //    table.Columns.Add("term", typeof(string));
            //    table.Columns.Add("userId", typeof(string));

            //    foreach (var answer in model)
            //    {
            //        table.Rows.Add("questionId" + answer.QuestionId);
            //        table.Rows.Add("subQuestionId" + answer.SubQuestionId);
            //        table.Rows.Add("answerId" + answer.AnswerId);
            //        table.Rows.Add("term" + answer.Term);
            //        table.Rows.Add("userId" + answer.UserId);
            //    }

            //    var pList = new SqlParameter("@studentAnswerT", SqlDbType.Structured);

            //    pList.TypeName = "[Evaluation].[UT_StudentAnswer]";

            //    pList.Value = table;

            //    cmd.Parameters.Add(pList);

            //}
        }

        public bool InsertStudentComment(string comment, int userId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[Evaluation].[InsertStudentComment]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Comment", comment);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@Term", Exam_Term);
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

        public string SellectTermJary()
        {
            return Exam_Term;
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = con;
            //cmd.CommandText = "[dbo].[GetTermJary]";
            //cmd.CommandType = CommandType.StoredProcedure;
            //DataTable dt = new DataTable();
            //try
            //{
            //    con.Open();
            //    SqlDataReader rdr;
            //    rdr = cmd.ExecuteReader();
            //    dt.Load(rdr);
            //    con.Close();

            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //return dt.Rows[0]["termCode"].ToString();
        }

        
    }
}
