using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DAC.Connections;
using IAUEC_Apps.DTO.University.Graduate;

namespace IAUEC_Apps.DAO.University.GraduateAffair
{
    public class GraduateFormsDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);

        #region get
        public DataTable GetStudentInfo(GraduateFormsDTO gfd)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            cmd.CommandText = "[Graduate].[SP_GetStudentInfo]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stCode", gfd.stCode);
            cmd.Parameters.AddWithValue("@family", gfd.family);
            cmd.Parameters.AddWithValue("@iddMeli", gfd.iddMeli);
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public DataTable searchStudentInfo_FeraghatDocument(string stcode, string family)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            cmd.CommandText = "[Graduate].[SP_GetStudentList_GraduateDocument]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stCode", stcode);
            cmd.Parameters.AddWithValue("@family", family);
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public DataTable getAllStudentRequest(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            cmd.CommandText = "[Graduate].[sp_GetStudentRequests]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stCode", stcode);
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public DataTable getInquiry(string stcode,string nationalCode,int inquiryType)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            cmd.CommandText = "[request].[sp_selectStudentDocumentInquiry]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stCode", stcode);
            cmd.Parameters.AddWithValue("@nationalCode", nationalCode);
            cmd.Parameters.AddWithValue("@inquiryType", inquiryType);
            try
            {
                if(conn.State== ConnectionState.Closed)
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }
        public void deleteInquiry(int inquiryID)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            cmd.CommandText = "[request].[sp_deleteInquiry]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@inquiryID", inquiryID);
            try
            {
                if(conn.State== ConnectionState.Closed)
                conn.Open();
                 cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public int insertInquiry(Inquiry inquiry)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "[request].[sp_insertStudentDocumentInquiry]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@documentAcceptDate", inquiry.documentAcceptDate);
            cmd.Parameters.AddWithValue("@inquiryType", inquiry.inquiryType);
            cmd.Parameters.AddWithValue("@letterDate", inquiry.letterDate);
            cmd.Parameters.AddWithValue("@letterNumber", inquiry.letterNumber);
            cmd.Parameters.AddWithValue("@note", inquiry.note);
            cmd.Parameters.AddWithValue("@stcode", inquiry.stcode);
            cmd.Parameters.AddWithValue("@toPresentTo", inquiry.toPeresentTo);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                int id=Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                inquiry.inquiryID = id;
            }
            catch(Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return  inquiry.inquiryID;
        }


        public int updateInquiry(Inquiry inquiry)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "[request].[sp_updateStudentDocumentInquiry]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@inquiryType", inquiry.inquiryType);
            cmd.Parameters.AddWithValue("@letterDate", inquiry.letterDate);
            cmd.Parameters.AddWithValue("@letterNumber", inquiry.letterNumber);
            cmd.Parameters.AddWithValue("@note", inquiry.note);
            cmd.Parameters.AddWithValue("@stcode", inquiry.stcode);
            cmd.Parameters.AddWithValue("@toPresentTo", inquiry.toPeresentTo);
            cmd.Parameters.AddWithValue("@id", inquiry.inquiryID);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                int id = Convert.ToInt32(cmd.ExecuteNonQuery());
                conn.Close();
                inquiry.inquiryID = id;
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return inquiry.inquiryID;
        }


        public DataTable getStudentFeraghatDocument(string stcode)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandText = "request.SP_getStudentInfo_GraduateDocument";
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.Parameters.AddWithValue("@stcode", stcode);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr=cmn.ExecuteReader();
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
        public DataTable getDocumentArchive()
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandText = "request.sp_getDocArchive";
            cmn.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr=cmn.ExecuteReader();
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
        public DataTable getDocumentArchive_naghs()
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandText = "request.sp_getDocumentArchive_Naghs";
            cmn.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr=cmn.ExecuteReader();
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

        public bool UpdateFeraghatTahsil_GraduateDocument(string stcode,Int64 studentRequest,bool stamp,bool hasPaymentReceipt,int mashmul,string specialTips,bool mashmulChanged,
            string docNumMovaghat,string docNumDanesh,string serialMovaghat,string serialDanesh)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandText = "request.SP_UpdateFeraghatTahsil_GraduateDocument";
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.Parameters.AddWithValue("@stcode",stcode);
            cmn.Parameters.AddWithValue("@reqID",studentRequest);
            cmn.Parameters.AddWithValue("@Stamp",stamp);
            cmn.Parameters.AddWithValue("@PaymentReceipt",hasPaymentReceipt);
            cmn.Parameters.AddWithValue("@mashmul",mashmul);
            cmn.Parameters.AddWithValue("@SpecialTips",specialTips);
            cmn.Parameters.AddWithValue("@mashmulChanged", mashmulChanged);
            cmn.Parameters.AddWithValue("@docNumMovaghat", docNumMovaghat);
            cmn.Parameters.AddWithValue("@docNumDanesh", docNumDanesh);
            cmn.Parameters.AddWithValue("@serialMovaghat", serialMovaghat);
            cmn.Parameters.AddWithValue("@serialDanesh", serialDanesh);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                int i = cmn.ExecuteNonQuery();
                conn.Close();
                return i > 0;
            }
            catch(Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
            }
        }

        public int insertDocArchiveId(Int64 studentRequest)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandText = "request.sp_getArchiveCode_stampReceipt";
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.Parameters.AddWithValue("@studentRequestId", studentRequest);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                int archiveId = Convert.ToInt32(cmn.ExecuteScalar());
                conn.Close();
                return archiveId;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return 0;
            }
        }
        public void freeDocArchiveId(Int64 studentRequest)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandText = "[Request].[sp_freeDocArchiveId]";
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.Parameters.AddWithValue("@studentRequestId", studentRequest);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                cmn.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public DataTable GetStatusReportInfo(GraduateFormsDTO gfd)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            cmd.CommandText = "[Graduate].[SP_getFormVaziatInfo]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stCode", gfd.stCode);
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public DataTable GetDraftReportInfo(GraduateFormsDTO gfd)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            cmd.CommandText = "[Graduate].[SP_getFormDraftInfo]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stCode", gfd.stCode);
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public DataTable GetCoursePassesReportInfo(GraduateFormsDTO gfd)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            cmd.CommandText = "[Graduate].[SP_getCoursePassedInfo]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stCode", gfd.stCode);
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        
        public DataTable GetMarkListReportInfo(GraduateFormsDTO gfd)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            cmd.CommandText = "[Graduate].[SP_getFormMarkListInfo]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stCode", gfd.stCode);
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
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
