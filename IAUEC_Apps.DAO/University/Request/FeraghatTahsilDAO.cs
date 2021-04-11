using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAC.Connections;
using IAUEC_Apps.DTO.University.Request;

namespace IAUEC_Apps.DAO.University.Request
{
    public class FeraghatTahsilDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);

        public int UpdateMadarekStatus(FeraghatTahsilDTO oFeraghat)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_UpdateFeraghatMadarekStatus]";
            cmd.Parameters.AddWithValue("@Id", oFeraghat.Id);
            cmd.Parameters.AddWithValue("@Riznomarat", oFeraghat.RizNomarat);
            cmd.Parameters.AddWithValue("@GovahiMovaghat", oFeraghat.GovahiMovaghat);
            cmd.Parameters.AddWithValue("@Daneshnameh", oFeraghat.DaneshNameh);
            cmd.Parameters.AddWithValue("@stcode", oFeraghat.Stcode);
            cmd.Parameters.AddWithValue("@CheckOutRequestId", oFeraghat.StudentRequestId);
            cmd.Parameters.AddWithValue("@sendRiz", oFeraghat.sendRiz);
            cmd.Parameters.AddWithValue("@sendGovahi", oFeraghat.sendGovahi);
            cmd.Parameters.AddWithValue("@sendDanesh", oFeraghat.sendDanesh);

            cmd.Parameters.AddWithValue("@DateRizNomreDeliver", oFeraghat.DateRizNomarat);
            cmd.Parameters.AddWithValue("@DateGovahiDeliver", oFeraghat.DateGovahiMovaghat);
            cmd.Parameters.AddWithValue("@DateDaneshDeliver", oFeraghat.DateDaneshNameh);
            
            cmd.Parameters.AddWithValue("@DateSodoorRizNomre", oFeraghat.dateSodoorRizNomre);
            cmd.Parameters.AddWithValue("@DateSodoorGovahi", oFeraghat.dateSodoorGovahi);
            cmd.Parameters.AddWithValue("@DateSodoorDaneshname", oFeraghat.dateSodoorDaneshname);

            cmd.Parameters.AddWithValue("@DateVoroodRizNomre", oFeraghat.dateVoroodRizNomre);
            cmd.Parameters.AddWithValue("@DateVoroodGovahi", oFeraghat.dateVoroodGovahi);
            cmd.Parameters.AddWithValue("@DateVoroodDaneshname", oFeraghat.dateVoroodDaneshname);

            cmd.Parameters.AddWithValue("@DateErsalRizNomre", oFeraghat.dateErsalRizNomre);


            int Id = 0;
            DataTable dt= new DataTable();
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

                throw;
            }
            if (dt.Rows.Count>0)
            {
                Id = Convert.ToInt32(dt.Rows[0][0]);
            }
            return Id;
        }

        public void setMadrakArchiveCode(int studentRequestID,int archiveTypeID)
        {
            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandText = "[Request].[sp_setArchiveCode_Madrak]";
            //cmd.CommandText = "[Request].[sp_getArchiveCode_Madrak]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@studentRequestId", studentRequestID);
            cmd.Parameters.AddWithValue("@docType", archiveTypeID);
            try
            {
                if (conn.State == ConnectionState.Closed)
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

        public FeraghatTahsilDTO GetMadarekStatus(int reqId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Request.SP_GetMadarekStatus";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@reqId", reqId.ToString());

            FeraghatTahsilDTO oFeraghat = null;

            try
            {
                using (DataTable dt = new DataTable())
                {
                    conn.Open();
                    SqlDataReader rdr;
                    rdr = cmd.ExecuteReader();
                    dt.Load(rdr);
                    conn.Close();
                    if (dt.Rows.Count > 0)
                    {
                        oFeraghat = new FeraghatTahsilDTO();

                        oFeraghat.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                        oFeraghat.RizNomarat = Convert.ToInt32(dt.Rows[0]["RizNomarat"]);
                        oFeraghat.DaneshNameh = Convert.ToInt32(dt.Rows[0]["DaneshNameh"]);
                        oFeraghat.GovahiMovaghat = Convert.ToInt32(dt.Rows[0]["GovahiMovaghat"]);
                        oFeraghat.Stcode = dt.Rows[0]["Stcode"] as string;
                        oFeraghat.StudentRequestId = Convert.ToInt32(dt.Rows[0]["CheckOutRequestId"]);
                        oFeraghat.DateDaneshNameh = (dt.Rows[0]["DateDaneshDeliver"]==DBNull.Value?"": dt.Rows[0]["DateDaneshDeliver"].ToString());
                        oFeraghat.DateGovahiMovaghat= (dt.Rows[0]["DateGovahiDeliver"] == DBNull.Value ? "" : dt.Rows[0]["DateGovahiDeliver"].ToString());
                        oFeraghat.DateRizNomarat = (dt.Rows[0]["DateRizNomreDeliver"] == DBNull.Value ? "" : dt.Rows[0]["DateRizNomreDeliver"].ToString());

                        oFeraghat.dateVoroodDaneshname = (dt.Rows[0]["DateVoroodDaneshname"] == DBNull.Value ? "" : dt.Rows[0]["DateVoroodDaneshname"].ToString()); 
                        oFeraghat.dateVoroodGovahi = (dt.Rows[0]["DateVoroodGovahi"] == DBNull.Value ? "" : dt.Rows[0]["DateVoroodGovahi"].ToString());
                        oFeraghat.dateVoroodRizNomre = (dt.Rows[0]["DateVoroodRizNomre"] == DBNull.Value ? "" : dt.Rows[0]["DateVoroodRizNomre"].ToString()); 

                        oFeraghat.dateSodoorDaneshname = (dt.Rows[0]["DateSodoorDaneshname"] == DBNull.Value ? "" : dt.Rows[0]["DateSodoorDaneshname"].ToString()); 
                        oFeraghat.dateSodoorGovahi = (dt.Rows[0]["DateSodoorGovahi"] == DBNull.Value ? "" : dt.Rows[0]["DateSodoorGovahi"].ToString()); 
                        oFeraghat.dateSodoorRizNomre = (dt.Rows[0]["DateSodoorRizNomre"] == DBNull.Value ? "" : dt.Rows[0]["DateSodoorRizNomre"].ToString());

                        oFeraghat.dateErsalRizNomre = (dt.Rows[0]["DateErsalRizNomre"] == DBNull.Value ? "" : dt.Rows[0]["DateErsalRizNomre"].ToString());

                        //oFeraghat.archiveCode_movaghat = Convert.ToInt64(dt.Rows[0]["archiveCode"]==DBNull.Value?0: dt.Rows[0]["archiveCode"]);
                        //oFeraghat.archiveCode_daneshname = oFeraghat.archiveCode_movaghat;// Convert.ToInt64(dt.Rows[0]["archiveCode"].ToString());
                        //oFeraghat.archiveCode_rizNomre = oFeraghat.archiveCode_movaghat;// Convert.ToInt64(dt.Rows[0]["archiveCode"].ToString());
                        oFeraghat.archiveCode_movaghat = Convert.ToInt64(dt.Rows[0]["govahiMovaghat_ID"] == DBNull.Value ? 0 : dt.Rows[0]["govahiMovaghat_ID"]);
                        oFeraghat.archiveCode_daneshname = Convert.ToInt64(dt.Rows[0]["daneshname_ID"] == DBNull.Value ? 0 : dt.Rows[0]["daneshname_ID"]);
                        oFeraghat.archiveCode_rizNomre = Convert.ToInt64(dt.Rows[0]["riznomre_ID"] == DBNull.Value ? 0 : dt.Rows[0]["riznomre_ID"]);

                    }
                }
            }
            catch (Exception)
            {

                throw;

            }
            return oFeraghat;
        }


        public DataTable getStudentInfo(FeraghatTahsilDTO oFeraghat)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            cmd.CommandText = "[Request].[SP_Get_AllStudentInfoByFamily]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stCode", oFeraghat.Stcode);
            cmd.Parameters.AddWithValue("@family", oFeraghat.family);
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



        public DataTable GetDateOfDef(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            cmd.CommandText = "[Request].[SP_GetCheckOutpajoohesh_DefDate]";
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
        public DataTable getSmsStatus(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            cmd.CommandText = "[Request].[SP_GetSmsStatus]";
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

        public DataTable getLoanInfo(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            cmd.CommandText = "[Request].[SP_Get_StudentLoanInfo]";
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

        public DataTable getMashmoolInfo(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();

            cmd.CommandText = "[Request].[SP_IsMashmool]";
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
        public int getMaleInfo(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
          
            cmd.CommandText = "[Request].[SP_IsMale]";
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
            if (dt.Rows.Count > 0)
            {
                return  Convert.ToInt32(dt.Rows[0][0]) ;
            }
                  
                return -1;
           
        }

        public void UpdateSmsStatus(string stcode,int index,bool isSend)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "[Request].[SP_updateMadrakSmsStatus]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stCode", stcode);
            cmd.Parameters.AddWithValue("@index", index);
            cmd.Parameters.AddWithValue("@send", isSend);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
                throw;
            }
        }
        public int ExistSignatureRiz(int ReqId)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            cmd.CommandText = "[Request].[SP_ExistSignature_Riz]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ReqId", ReqId);

            int YYY;
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr); 
                conn.Close();
                YYY = Convert.ToInt32(dt.Rows[0][0]);
            }
            catch
            {
                throw;
            }
            return YYY;
        }
        public int ExistSignatureGovahi(int ReqId)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            cmd.CommandText = "[Request].[SP_ExistSignature_Govahi]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ReqId", ReqId);

            int YYY;
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
                YYY = Convert.ToInt32(dt.Rows[0][0]);
            }
            catch
            {
                throw;
            }
            return YYY;
        }
        public int ExistSignatureDanesh(int ReqId)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            cmd.CommandText = "[Request].[SP_ExistSignature_Danesh]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ReqId", ReqId);

            int YYY;
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
                YYY = Convert.ToInt32(dt.Rows[0][0]);
            }
            catch
            {
                throw;
            }
            return YYY;
        }
    }
}
