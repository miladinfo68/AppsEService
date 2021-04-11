using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DTO.University.Request;
using System.Data.SqlClient;
using IAUEC_Apps.DAC.Connections;
using System.Data;

namespace IAUEC_Apps.DAO.University.Request
{
    public class CheckOutNaghsDAO : IDisposable
    {
        private SqlConnection _conn = null;

        public CheckOutNaghsDAO()
        {
            _conn = new SqlConnection(new SuppConnection().Supp_con);
        }

        #region Create
        public int InsertNewNaghs(CheckOutNaghsDTO oNaghs)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _conn;
            cmd.CommandText = "Request.[SP_InsertNaghsAndUpdateReq]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentRequestID", oNaghs.StudentRequestId);
            cmd.Parameters.AddWithValue("@stcode", oNaghs.StCode);
            cmd.Parameters.AddWithValue("@Erae_Be", oNaghs.Erae_Be);
            cmd.Parameters.AddWithValue("@RequestLogID", oNaghs.RequestLogId);
            cmd.Parameters.AddWithValue("@SubmitDate", oNaghs.SubmitDate);
            cmd.Parameters.AddWithValue("@NaghsMessage", oNaghs.NaghsMessage);
            cmd.Parameters.AddWithValue("@ResolveDate", oNaghs.ResolveDate);
            cmd.Parameters.AddWithValue("@ResolveMessage", oNaghs.ResolveMessage);
            int naghsId = 0;
            try
            {
                _conn.Open();
                naghsId = Convert.ToInt32(cmd.ExecuteScalar());
                _conn.Close();

            }
            catch (Exception)
            {
                throw;
            }
            return naghsId;
        }
        public int InsertOdat(CheckOutNaghsDTO oNaghs)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _conn;
            cmd.CommandText = "Request.[SP_InsertOdatAndUpdateReq]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentRequestID", oNaghs.StudentRequestId);
            cmd.Parameters.AddWithValue("@stcode", oNaghs.StCode);
            cmd.Parameters.AddWithValue("@Erae_Be", oNaghs.Erae_Be);
            cmd.Parameters.AddWithValue("@RequestLogID", oNaghs.RequestLogId);
            cmd.Parameters.AddWithValue("@SubmitDate", oNaghs.SubmitDate);
            cmd.Parameters.AddWithValue("@NaghsMessage", oNaghs.NaghsMessage);
            cmd.Parameters.AddWithValue("@ResolveDate", oNaghs.ResolveDate);
            cmd.Parameters.AddWithValue("@ResolveMessage", oNaghs.ResolveMessage);
            int naghsId = 0;
            try
            {
                _conn.Open();
                naghsId = Convert.ToInt32(cmd.ExecuteScalar());
                _conn.Close();

            }
            catch (Exception)
            {
                throw;
            }
            return naghsId;
        }
        #endregion create
        public int InsertNaghs_Article(CheckOutNaghsDTO oNaghs)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _conn;
            cmd.CommandText = "Request.[SP_InsertNaghsArticle]";
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@StudentRequestID", oNaghs.StudentRequestId);
            cmd.Parameters.AddWithValue("@stcode", oNaghs.StCode);
            //  cmd.Parameters.AddWithValue("@Erae_Be", oNaghs.Erae_Be);
            cmd.Parameters.AddWithValue("@RequestLogID", oNaghs.RequestLogId);
            cmd.Parameters.AddWithValue("@SubmitDate", oNaghs.SubmitDate);
            cmd.Parameters.AddWithValue("@NaghsMessage", oNaghs.NaghsMessage);
            cmd.Parameters.AddWithValue("@ResolveDate", oNaghs.ResolveDate);
            cmd.Parameters.AddWithValue("@ResolveMessage", oNaghs.ResolveMessage);
            int naghsId = 0;
            var dt = new DataTable();
            try
            {
                _conn.Open();
                var rd = cmd.ExecuteReader();
                dt.Load(rd);
                naghsId = Convert.ToInt32(dt.Rows[0][0]);
                _conn.Close();

            }
            catch (Exception)
            {
                throw;
            }
            return naghsId;
        }

        public int InsertNaghs(CheckOutNaghsDTO oNaghs)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _conn;
            cmd.CommandText = "Request.[SP_InsertNaghs]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentRequestID", oNaghs.StudentRequestId);
            cmd.Parameters.AddWithValue("@stcode", oNaghs.StCode);
            cmd.Parameters.AddWithValue("@Erae_Be", oNaghs.Erae_Be);
            cmd.Parameters.AddWithValue("@RequestLogID", oNaghs.RequestLogId);
            cmd.Parameters.AddWithValue("@SubmitDate", oNaghs.SubmitDate);
            cmd.Parameters.AddWithValue("@NaghsMessage", oNaghs.NaghsMessage);
            cmd.Parameters.AddWithValue("@ResolveDate", oNaghs.ResolveDate);
            cmd.Parameters.AddWithValue("@ResolveMessage", oNaghs.ResolveMessage);
            int naghsId = 0;
            var dt = new DataTable();
            try
            {
                if (_conn.State == ConnectionState.Closed)
                    _conn.Open();
                var rd = cmd.ExecuteReader();
                dt.Load(rd);
                naghsId = Convert.ToInt32(dt.Rows[0][0]);
                _conn.Close();

            }
            catch (Exception)
            {
                if (_conn.State == ConnectionState.Open)
                    _conn.Close();
                throw;
            }
            return naghsId;
        }
        public int UpdateStudentRequest(StudentRequest oNaghs)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _conn;
            cmd.CommandText = "Request.[SP_UpdateStudentRequest]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentRequestID", oNaghs.StudentRequestId);
            cmd.Parameters.AddWithValue("@stcode", oNaghs.StCode);
            cmd.Parameters.AddWithValue("@Erae_Be", oNaghs.Erae_Be);
            cmd.Parameters.AddWithValue("@RequestLogID", oNaghs.RequestLogId);
            cmd.Parameters.AddWithValue("@HasStamp", oNaghs.HasStamp);
            var dt = new DataTable();
            try
            {
                if (_conn.State == ConnectionState.Closed)
                    _conn.Open();
                var rd = cmd.ExecuteNonQuery();
                _conn.Close();
                return rd;
            }
            catch (Exception)
            {
                if (_conn.State == ConnectionState.Open)
                    _conn.Close();
                return -1;
                throw;
            }
        }

        public void Delete_Naghs(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _conn;
            cmd.CommandText = "[Request].[SP_DeleteNaghsByStcode]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", stcode);

            try
            {
                _conn.Open();
                cmd.ExecuteNonQuery();

                _conn.Close();

            }
            catch (Exception)
            {
                throw;
            }

        }


        #region Read
        public DataTable GetAllNaghsByReqId(int StudentRequestId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _conn;
            cmd.CommandText = "Request.SP_GetAllNaghsByReqId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StudentRequestId", StudentRequestId);

            DataTable dt = new DataTable();

            try
            {
                _conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                _conn.Close();

            }
            catch (Exception)
            {

                throw;

            }
            return dt;
        }

        public DataTable GetAllNaghsByStatusId(int currentStatus)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _conn;
            cmd.CommandText = "Request.SP_GetAllNaghsByStatusId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@currentStatus", currentStatus);

            DataTable dt = new DataTable();

            try
            {
                _conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                _conn.Close();

            }
            catch (Exception)
            {

                throw;

            }
            return dt;
        }

        public int HasNaghs(string issuerID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _conn;
            cmd.CommandText = "Request.SP_HasNaghs";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", issuerID);

            int count = 0;

            try
            {
                _conn.Open();

                count = Convert.ToInt32(cmd.ExecuteScalar());

                _conn.Close();

            }
            catch (Exception)
            {

                throw;

            }
            return count;
        }
        public DataTable GetNaghsByStCode(string issuerID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _conn;
            cmd.CommandText = "[Request].[SP_GetNaghsByStCode]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", issuerID);

            DataTable dt = new DataTable();

            try
            {
                _conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                _conn.Close();

            }
            catch (Exception ex)
            {

                throw;

            }
            return dt;
        }

        public DataTable GetNaghsMessage(string issuerID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _conn;
            cmd.CommandText = "Request.SP_GetNaghsMessage";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", issuerID);

            var dt = new DataTable();

            try
            {
                _conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                _conn.Close();

            }
            catch (Exception)
            {

                throw;

            }
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }

        }

        //public DataTable GetAllNaghsViewModelByReqId(int StudentRequestId)
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = _conn;
        //    cmd.CommandText = "Request.SP_GetAllNaghsViewModelByReqId";
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@StudentRequestId", StudentRequestId);

        //    DataTable dt = new DataTable();

        //    try
        //    {
        //        _conn.Open();
        //        SqlDataReader rdr;
        //        rdr = cmd.ExecuteReader();
        //        dt.Load(rdr);
        //        _conn.Close();

        //    }
        //    catch (Exception)
        //    {

        //        throw;

        //    }
        //    return dt;
        //}

        #endregion Read

        #region Update

        public int AddResolveMessage(int naghsId, string message)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _conn;
            cmd.CommandText = "Request.SP_AddResolveMessage";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@naghsId", naghsId);
            cmd.Parameters.AddWithValue("@message", message);

            int count = 0;

            try
            {
                _conn.Open();
                count = cmd.ExecuteNonQuery();
                _conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return count;

        }

        public DataTable GetNaghsIdByReqId(int reqId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _conn;
            cmd.CommandText = "[Request].[SP_GetNaghsIdByReqId]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ReqId", reqId);


            DataTable dt = new DataTable();

            try
            {
                _conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                _conn.Close();

            }
            catch (Exception)
            {

                throw;

            }
            return dt;
        }
        public DataTable GetallNotResolvedNaghsByReqId(int reqId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _conn;
            cmd.CommandText = "[Request].[SP_GetAllNotResolvedNaghsByReqID]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ReqId", reqId);


            DataTable dt = new DataTable();

            try
            {
                _conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                _conn.Close();

            }
            catch (Exception)
            {

                throw;

            }
            return dt;
        }

        public int ResolveNaghsById(int naghsId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _conn;
            cmd.CommandText = "Request.SP_ResolveNaghsById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@naghsId", naghsId);

            int count = 0;

            try
            {
                _conn.Open();
                count = cmd.ExecuteNonQuery();
                _conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return count;
        }
        public int ResolveNaghsByMessage(int RequestId,int RequestLogId, string message)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _conn;
            cmd.CommandText = "Request.SP_ResolveNaghsByMessage";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RequestId", RequestId);
            cmd.Parameters.AddWithValue("@RequestLogId",RequestLogId);
            cmd.Parameters.AddWithValue("@message", message);
            int count = 0;

            try
            {
                _conn.Open();
                count = cmd.ExecuteNonQuery();
                _conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return count;
        }
        #endregion Update

        #region Delete
        public void DeleteNaghs(int naghsId, int Erae_be, int reqLogId, int reqId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _conn;
            cmd.CommandText = "[Request].[SP_DeleteNaghsAndUpdateRequest]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@naghsId", naghsId);
            cmd.Parameters.AddWithValue("@Erae_Be", Erae_be);
            cmd.Parameters.AddWithValue("@RequestLogId", reqLogId);
            cmd.Parameters.AddWithValue("@reqId", reqId);

            try
            {
                _conn.Open();
                cmd.ExecuteNonQuery();
                _conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        public void Dispose()
        {
            if (this._conn != null)
            {
                this._conn.Dispose();
                this._conn = null;
            }
        }





    }
}
