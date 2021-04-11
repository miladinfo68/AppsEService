using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using IAUEC_Apps.DAC.Connections;
using IAUEC_Apps.DTO.Helper;
using IAUEC_Apps.DTO.University.Request;


namespace IAUEC_Apps.DAO.University.Request
{
    public class Request_GovahiDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
        #region read
        public DataTable GetAmountForPay(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Request.SP_GetAmountForPay";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", stcode);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;
        }

        public bool CanPay(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Request].[SP_CanPay]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", stcode);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return Convert.ToInt32(dt.Rows[0][0]) > 0;
        }

        public DataTable GetMojazGovahi(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Request.SP_GetMojazGovahi";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", stcode);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        public DataTable GetPayment(int RequestTypeID)
        {
            SqlCommand cmdGetP = new SqlCommand();
            cmdGetP.Connection = conn;
            cmdGetP.CommandText = "Request.SP_GetPayment";
            cmdGetP.CommandType = CommandType.StoredProcedure;
            cmdGetP.Parameters.AddWithValue("@RequestTypeID", RequestTypeID);
            DataTable dtp = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdGetP.ExecuteReader();
                dtp.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdGetP.Dispose();

            }
            catch (Exception)
            { throw; }
            return dtp;
        }

        public DataTable GetAcceptedGovahiReport(int RequestTypeID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Request.SP_GetAcceptedGovahiReport";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@RequestTypeID", RequestTypeID);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
            return dt;

        }


        public DataTable GetGovahiReport(int RequestLogID)
        {
            SqlCommand cmdgov = new SqlCommand();
            cmdgov.Connection = conn;
            cmdgov.CommandType = CommandType.StoredProcedure;
            cmdgov.CommandText = "Request.SP_GetGovahiReport";
            cmdgov.Parameters.AddWithValue("@RequestLogID", RequestLogID);
            DataTable dtgov = new DataTable();

            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdgov.ExecuteReader();
                dtgov.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdgov.Dispose();

            }
            catch (Exception)
            { throw; }
            return dtgov;
        }

       
        public DataTable GetRequestByRequestID(int StudentRequestID)
        {
            SqlCommand cmdid = new SqlCommand();
            cmdid.Connection = conn;
            cmdid.CommandText = "Request.SP_GetRequestByRequestID";
            cmdid.CommandType = CommandType.StoredProcedure;
            cmdid.Parameters.AddWithValue("@StudentRequestID", StudentRequestID);
            DataTable dtID = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdid.ExecuteReader();
                dtID.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdid.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dtID;
        }
        public DataTable GetGovahiStatus(string stcode)
        {
            SqlCommand cmdgov = new SqlCommand();
            cmdgov.Connection = conn;
            cmdgov.CommandText = "Request.SP_GetStGovahiRequest";
            cmdgov.CommandType = CommandType.StoredProcedure;
            cmdgov.Parameters.AddWithValue("@stcode", stcode);
            DataTable dtgov = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdgov.ExecuteReader();
                dtgov.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdgov.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dtgov;
        }

        public DataTable GetGovahiRequest(int RequestLogID1, int RequestLogID2)
        {
            SqlCommand cmgo = new SqlCommand();
            cmgo.Connection = conn;
            cmgo.CommandText = "[Request].[SP_GetGovahiRequest]";
            cmgo.CommandType = CommandType.StoredProcedure;
            cmgo.Parameters.AddWithValue("@RequestLogID1", RequestLogID1);
            cmgo.Parameters.AddWithValue("@RequestLogID2", RequestLogID2);
            DataTable dtgo = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmgo.ExecuteReader();
                dtgo.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmgo.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dtgo;
        }

        public DataTable GetStRegisterd(string stcode)
        {
            SqlCommand cmdreg = new SqlCommand();
            cmdreg.Connection = conn;
            cmdreg.CommandText = "Request.SP_GetstudentInTerm";
            cmdreg.CommandType = CommandType.StoredProcedure;
            cmdreg.Parameters.AddWithValue("@stcode", stcode);
            DataTable dtsr = new DataTable();

            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdreg.ExecuteReader();
                dtsr.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdreg.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dtsr;
        }
        public DataTable GetStRegisterd2(string stcode)
        {
            SqlCommand cmdreg = new SqlCommand();
            cmdreg.Connection = conn;
            cmdreg.CommandText = "Resource_Control.SP_GetstudentInDefenceTerm";
            cmdreg.CommandType = CommandType.StoredProcedure;
            cmdreg.Parameters.AddWithValue("@stcode", stcode);
            DataTable dtsr = new DataTable();

            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdreg.ExecuteReader();
                dtsr.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdreg.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dtsr;
        }

        public DataTable GetBedehkar(string stcode)
        {
            SqlCommand cmdbed = new SqlCommand();
            cmdbed.Connection = conn;
            cmdbed.CommandText = "Request.SP_FindBedehkar";
            cmdbed.CommandType = CommandType.StoredProcedure;
            cmdbed.Parameters.AddWithValue("@stcode", stcode);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdbed.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdbed.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }



        #endregion

        #region write
        //public void InsertMashmulNumber(string MashmulNumber,int StudentRequestID,string MahmulTarikh)
        //{
        //    SqlCommand cmdInsMash = new SqlCommand();
        //    cmdInsMash.Connection = conn;
        //    cmdInsMash.CommandText = "Request.SP_InsertMashmulNumber";
        //    cmdInsMash.CommandType = CommandType.StoredProcedure;
        //    cmdInsMash.Parameters.AddWithValue("@mashmulNumber", MashmulNumber);
        //    cmdInsMash.Parameters.AddWithValue("@StudentRequestID", StudentRequestID);
        //    cmdInsMash.Parameters.AddWithValue("@MashmulTarikh",MahmulTarikh);
        //    try 
        //    {
        //        conn.Open();
        //        cmdInsMash.ExecuteNonQuery();
        //        conn.Close();
        //        cmdInsMash.Dispose();
        //    }
        //    catch(Exception)
        //    { throw; }
        //}

        public DataTable getMashmulStatus(string stCode)
        {
            SqlCommand cmdGetMash = new SqlCommand();
            DataTable dt = new DataTable();
            cmdGetMash.Connection = conn;
            cmdGetMash.CommandText = "[Request].[SP_getMilitaryStatus]";
            cmdGetMash.CommandType = CommandType.StoredProcedure;
            cmdGetMash.Parameters.AddWithValue("@stCode", stCode);
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdGetMash.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        public void InsertGovahiIntoStrequest(string stcode, string date_sabt, string time_sabt, string sharh_ara_bekoja, int TypeGovahi)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_InsertEraeBe";
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@date_sabt", date_sabt);
            cmd.Parameters.AddWithValue("@time_sabt", time_sabt);
            cmd.Parameters.AddWithValue("@sharh_ara_bekoja", sharh_ara_bekoja);
            cmd.Parameters.AddWithValue("@TypeGovahi", TypeGovahi);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

        #region update

        public void UpdatePymentDetail(long OrderID, string Date, int PaymentID, int Amount, string RequestKey)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Request.SP_UpdatePymentDetail";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderID", OrderID);
            cmd.Parameters.AddWithValue("@Date", Date);
            cmd.Parameters.AddWithValue("@PaymentID", PaymentID);
            cmd.Parameters.AddWithValue("@Amount", Amount);
            cmd.Parameters.AddWithValue("@RequestKey", RequestKey);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                cmd.Dispose();

            }
            catch (Exception)
            { throw; }
        }

        public void UpdateEraeBe(string EraeBe, int StudentRequestID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Request.SP_UpdateEraeBe";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EraeBe", EraeBe);
            cmd.Parameters.AddWithValue("@StudentRequestID", StudentRequestID);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
        }

        #endregion
        #region Delete
        public void DeleteGovahiRequest(int ReqId, int PaymentId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Request.SP_DeleteGovahiRequest";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ReqId", ReqId);
            cmd.Parameters.AddWithValue("@PaumentID", PaymentId);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                cmd.Dispose();
            }
            catch (Exception)
            { throw; }
        }



        public void DeleteFromStudentReqByReqID(int RequestID)
        {
            SqlCommand cmdDel = new SqlCommand();
            cmdDel.Connection = conn;
            cmdDel.CommandText = "Request.SP_DeleteFromtbl_StudentRequest";
            cmdDel.CommandType = CommandType.StoredProcedure;
            cmdDel.Parameters.AddWithValue("@RequestId", RequestID);
            try
            {
                conn.Open();
                cmdDel.ExecuteNonQuery();
                conn.Close();
                cmdDel.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        public List<GovahiRequest> PaymentByPastPayments(string listOfRequest, string stCode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Request].[SP_PaymentByPastPayments]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@listOfRequest", listOfRequest);
            cmd.Parameters.AddWithValue("@stCode", stCode);
            var dt = new DataTable();
            try
            {
                conn.Open();
                var rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(rd);
                conn.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            var resualt = new List<GovahiRequest>();
            resualt.AddRange(dt.ConvertDataTableToList<GovahiRequest>());
            return resualt;

        }


        public List<GovahiRequest> UpdateRollBackingPastPayment(int currentReqId, int trasferReqId, int fourteenReqId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Request].[SP_UpdateRollBackingPastPayment]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@currentReqId", currentReqId);
            cmd.Parameters.AddWithValue("@trasferReqId", trasferReqId);
            cmd.Parameters.AddWithValue("@fourteenReqId", fourteenReqId);
            var dt = new DataTable();
            try
            {
                conn.Open();
                var rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(rd);
                conn.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            var resualt = new List<GovahiRequest>();
            resualt.AddRange(dt.ConvertDataTableToList<GovahiRequest>());
            return resualt;

        }

        public List<GovahiRequest> PaymentStatus(string listOfRequest, string stCode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Request].[SP_PaymentStatus]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@listOfRequest", listOfRequest);
            cmd.Parameters.AddWithValue("@stCode", stCode);
            var dt = new DataTable();
            try
            {
                conn.Open();
                var rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(rd);
                conn.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            var resualt = new List<GovahiRequest>();
            resualt.AddRange(dt.ConvertDataTableToList<GovahiRequest>());
            return resualt;

        }
     

    }
}
