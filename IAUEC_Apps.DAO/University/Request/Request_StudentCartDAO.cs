using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DAC.Connections;



namespace IAUEC_Apps.DAO.University.Request
{
    public class Request_StudentCartDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);

        #region Create
        public void InsertIntoPayinterm(string stcode, int serial_pay, int idtypepay, decimal amount, string datefish, string datesabt)
        {
            SqlCommand cmdpay = new SqlCommand();
            cmdpay.Connection = conn;
            cmdpay.CommandText = "Request.SP_InsertIntoPayinterm";
            cmdpay.CommandType = CommandType.StoredProcedure;
            cmdpay.Parameters.AddWithValue("@stcode", stcode);
            cmdpay.Parameters.AddWithValue("@serial_pay", serial_pay);
            cmdpay.Parameters.AddWithValue("@idtypepay", idtypepay);
            cmdpay.Parameters.AddWithValue("@amount", amount);
            cmdpay.Parameters.AddWithValue("@datefish", datefish);
            cmdpay.Parameters.AddWithValue("@datesabt", datesabt);
            try
            {
                conn.Open();
                cmdpay.ExecuteNonQuery();
                conn.Close();
                cmdpay.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsertInToStudentLog(string stcode, DateTime LogDate, string LogTime, int LogTypeID)
        {
            SqlCommand cmdi = new SqlCommand();
            cmdi.Connection = conn;
            cmdi.CommandType = CommandType.StoredProcedure;
            cmdi.CommandText = "Request.SP_InsertInToStudentLog";
            cmdi.Parameters.AddWithValue("@stcode", stcode);
            cmdi.Parameters.AddWithValue("@LogDate", LogDate);
            cmdi.Parameters.AddWithValue("@LogTime", LogTime);
            cmdi.Parameters.AddWithValue("@LogTypeID", LogTypeID);
            try
            {
                conn.Open();
                cmdi.ExecuteNonQuery();
                conn.Close();
                cmdi.Dispose();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public int InsertInToStudentRequest(string stcode, int RequestTypeID, int RequestLogID, string Erae_Be, string MashmulNumber, int isOnline)
        {
            SqlCommand cmds = new SqlCommand();
            cmds.Connection = conn;
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.CommandText = "Request.SP_InsertInToStudentRequest";
            cmds.Parameters.AddWithValue("@stcode", stcode);
            cmds.Parameters.AddWithValue("@RequestTypeID", RequestTypeID);
            cmds.Parameters.AddWithValue("@RequestLogID", RequestLogID);
            cmds.Parameters.AddWithValue("@Erae_Be", Erae_Be);
            cmds.Parameters.AddWithValue("@MashmulNumber", MashmulNumber);
            cmds.Parameters.AddWithValue("@isOnline", isOnline);

            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                //int reqId = Convert.ToInt32(cmds.ExecuteScalar());
                int reqId = int.Parse(cmds.ExecuteScalar().ToString());
                conn.Close();
                cmds.Dispose();
                return reqId;


            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw ex;

            }
        }
        public bool CheckStCodeIsExistInStudentRequest(string stcode, int RequestTypeID, int RequestLogID)
        {
            SqlCommand cmds = new SqlCommand();
            cmds.Connection = conn;
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.CommandText = "[Request].[SP_CheckStCodeIsExistInStudentRequest]";
            cmds.Parameters.AddWithValue("@stcode", stcode);
            cmds.Parameters.AddWithValue("@RequestTypeID", RequestTypeID);
            cmds.Parameters.AddWithValue("@RequestLogID", RequestLogID);

            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                //int reqId = Convert.ToInt32(cmds.ExecuteScalar());
                var reqId1= cmds.ExecuteScalar();
                int reqId=0;
                if (reqId1!=null)
                     reqId= int.Parse(reqId1.ToString());
             
                conn.Close();
                cmds.Dispose();
                return reqId>0;


            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw ex;

            }
        }
        #endregion
        #region Read

        public DataTable Get_StudentAddress(string stcode, int param)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Request.SP_Get_StudentAddress";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@param", param);
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

        public DataTable GetstHasCartReq(string stcode)
        {
            SqlCommand cmdhas = new SqlCommand();
            cmdhas.Connection = conn;
            cmdhas.CommandText = "Request.SP_GetStHasCartReq";
            cmdhas.CommandType = CommandType.StoredProcedure;
            cmdhas.Parameters.AddWithValue("@stcode", stcode);
            DataTable dthas = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdhas.ExecuteReader();
                dthas.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdhas.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dthas;
        }

        public DataTable GetRequestStatus(string stcode)
        {
            SqlCommand cmdcrs = new SqlCommand();
            cmdcrs.Connection = conn;
            cmdcrs.CommandType = CommandType.StoredProcedure;
            cmdcrs.CommandText = "Request.SP_GetCartRequestState";
            cmdcrs.Parameters.AddWithValue("@stcode", stcode);
            DataTable dtcrs = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdcrs.ExecuteReader();
                dtcrs.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdcrs.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dtcrs;

        }
        public bool CheckOrderId(int serial_pay, int idtypepay)
        {
            SqlCommand cmdchk = new SqlCommand();
            cmdchk.CommandType = CommandType.StoredProcedure;
            cmdchk.CommandText = "Request.SP_CheckOrderID";

            cmdchk.Connection = conn;
            cmdchk.Parameters.AddWithValue("@serial_pay", serial_pay);
            cmdchk.Parameters.AddWithValue("@idtypepay", idtypepay);
            try
            {
                conn.Open();
                long oID = long.Parse(cmdchk.ExecuteScalar().ToString());
                if (oID == 0)
                {
                    conn.Close();
                    cmdchk.Dispose();
                    return false;
                }
                else
                {
                    conn.Close();
                    cmdchk.Dispose();
                    return true;
                }

            }
            catch (Exception)
            {

                throw;
            }


        }

        public DataTable GetCartRequest(int RequestLogID)
        {
            SqlCommand cmdc = new SqlCommand();
            cmdc.Connection = conn;
            cmdc.CommandType = CommandType.StoredProcedure;
            cmdc.CommandText = "Request.SP_GetCartRequest";
            cmdc.Parameters.AddWithValue("@RequestLogID", RequestLogID);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdc.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdc.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        public DataTable GetStudentInfo(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_Get_StudentInfo";
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
        public DataTable GetStudentChildren(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_GetStudentChildren";
            cmd.Parameters.AddWithValue("@StudentCode", stcode);
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
        public DataTable GetStudentInfoAmoozeshyar(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "amoozeshyar.SP_Get_StudentInfo";
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

        public bool CheckEraeBeExist(string stcode, int requestTypeID, int requestLogID, string erae_Be)
        {
            SqlCommand cmds = new SqlCommand();
            cmds.Connection = conn;
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.CommandText = "Request.SP_CheckEraeBeExist";
            cmds.Parameters.AddWithValue("@stcode", stcode);
            cmds.Parameters.AddWithValue("@RequestTypeID", requestTypeID);
            cmds.Parameters.AddWithValue("@RequestLogID", requestLogID);
            cmds.Parameters.AddWithValue("@Erae_Be", erae_Be);

            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                int resultCount = int.Parse(cmds.ExecuteScalar().ToString());
                conn.Close();
                cmds.Dispose();
                return resultCount > 0;


            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw ex;

            }
        }


        #endregion
        #region Update


        public void UpdateAddressm(string stcode)
        {
            SqlCommand cmdupaddm = new SqlCommand();
            cmdupaddm.Connection = conn;
            cmdupaddm.CommandText = "Request.SP_UpdateAddressm";
            cmdupaddm.CommandType = CommandType.StoredProcedure;
            cmdupaddm.Parameters.AddWithValue("@stcode", stcode);

            try
            {
                conn.Open();
                cmdupaddm.ExecuteNonQuery();
                conn.Close();
                cmdupaddm.Dispose();
            }
            catch (Exception)
            { throw; }

        }

        public void UpdateStudentPOstNumber(string stcode, string PostNumber, int RequestTypeID, int StudentRequestID)
        {
            SqlCommand cmdpost = new SqlCommand();
            cmdpost.Connection = conn;
            cmdpost.CommandText = "Request.SP_UpdateCodMarsulePosti";
            cmdpost.CommandType = CommandType.StoredProcedure;
            cmdpost.Parameters.AddWithValue("@stcode", stcode);
            cmdpost.Parameters.AddWithValue("@PostNumber", PostNumber);
            cmdpost.Parameters.AddWithValue("@RequestTypeID", RequestTypeID);
            cmdpost.Parameters.AddWithValue("@StudentRequestID", StudentRequestID);

            try
            {
                conn.Open();
                cmdpost.ExecuteNonQuery();
                conn.Close();
                cmdpost.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UpdateStudentRequestLogID(string stcode, int RequestLogID, int RequestTypeID, int StudentRequestID)
        {
            SqlCommand cmdUst = new SqlCommand();
            cmdUst.Connection = conn;
            cmdUst.CommandText = "Request.SP_UpdateStReqLogID";
            cmdUst.CommandType = CommandType.StoredProcedure;
            cmdUst.Parameters.AddWithValue("@RequestLogID", RequestLogID);
            cmdUst.Parameters.AddWithValue("@stcode", stcode);
            cmdUst.Parameters.AddWithValue("@RequestTypeID", RequestTypeID);
            cmdUst.Parameters.AddWithValue("@StudentRequestID", StudentRequestID);
            try
            {
                conn.Open();
                cmdUst.ExecuteNonQuery();
                conn.Close();
                cmdUst.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateAddres(string stcode, string newtel, string newaddress, string codeposti)
        {
            SqlCommand cma = new SqlCommand();
            cma.Connection = conn;
            cma.CommandType = CommandType.StoredProcedure;
            cma.CommandText = "Request.SP_UpdateAddressTel";
            cma.Parameters.AddWithValue("@stcode", stcode);
            cma.Parameters.AddWithValue("@newtel", newtel);
            cma.Parameters.AddWithValue("@code_posti", codeposti);
            cma.Parameters.AddWithValue("@newaddress", newaddress);

            try
            {
                conn.Open();
                cma.ExecuteNonQuery();
                conn.Close();
                cma.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
        }



        public void UpdateUpdateStReqTypeLogID(string stcode, int RequestTypeID, int RequestLogID)
        {
            SqlCommand cmu = new SqlCommand();
            cmu.Connection = conn;
            cmu.CommandType = CommandType.StoredProcedure;
            cmu.CommandText = "Request.SP_UpdateStReqTypeID";
            cmu.Parameters.AddWithValue("@stcode", stcode);
            cmu.Parameters.AddWithValue("@RequestTypeID", RequestTypeID);
            cmu.Parameters.AddWithValue("@RequestLogID", RequestLogID);
            try
            {
                conn.Open();
                cmu.ExecuteNonQuery();
                conn.Close();
                cmu.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Delete
        public void DeleteCard(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Request.SP_DeleteCard";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.CommandType = CommandType.StoredProcedure;
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

        public void UpdatePayment(int currentRequestId, int newRequestId)
        {
            SqlCommand cma = new SqlCommand();
            cma.Connection = conn;
            cma.CommandType = CommandType.StoredProcedure;
            cma.CommandText = "[Request].[SP_UpdatePayment]";
            cma.Parameters.AddWithValue("@currentRequestId", currentRequestId);
            cma.Parameters.AddWithValue("@newRequestId", newRequestId);

            try
            {
                conn.Open();
                cma.ExecuteNonQuery();
                conn.Close();
                cma.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UpdateAmount(int currentRequestId, int newRequestId)
        {
            SqlCommand cma = new SqlCommand();
            cma.Connection = conn;
            cma.CommandType = CommandType.StoredProcedure;
            cma.CommandText = "[Request].[SP_UpdateAmount]";
            cma.Parameters.AddWithValue("@currentRequestId", currentRequestId);
            cma.Parameters.AddWithValue("@newRequestId", newRequestId);

            try
            {
                conn.Open();
                cma.ExecuteNonQuery();
                conn.Close();
                cma.Dispose();

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
