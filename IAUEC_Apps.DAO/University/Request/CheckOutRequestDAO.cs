using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using IAUEC_Apps.DTO.University.Request;
using IAUEC_Apps.DAC.Connections;

namespace IAUEC_Apps.DAO.University.Request
{
    public class CheckOutRequestDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
        SqlConnection conn1 = new SqlConnection(new SuppConnection().UserAccess_con);

        public int InsertInToStudentRequest(string stcode, int RequestTypeID, int RequestLogID, string Erae_Be, string MashmulNumber, string CreateDate, string note, int isOnline)
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
            cmds.Parameters.AddWithValue("@CreateDate", CreateDate);
            cmds.Parameters.AddWithValue("@note", note);
            cmds.Parameters.AddWithValue("@isOnline", isOnline);

            try
            {
                conn.Open();
                int reqId = int.Parse(cmds.ExecuteScalar().ToString());
                conn.Close();
                return reqId;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool insertCodeBayegan(string stcode, string codeBayegan)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "[request].[sp_newCodeBaygan]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stCode", stcode);
            cmd.Parameters.AddWithValue("@codeBaygan", codeBayegan);
            try
            {
                int i = 0;
                if(conn.State== ConnectionState.Closed)
                conn.Open();
                i= cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
            }
            
        }
        public DataTable getAllCodeBayegan()
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "[request].[sp_getAllCodeBayganInService]";
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                if(conn.State== ConnectionState.Closed)
                conn.Open();

                SqlDataReader dr= cmd.ExecuteReader();
                dt.Load(dr);
                conn.Close();
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
            
        }
        public string GetSaleVoroodByStCode(string stCode)
        {
            SqlCommand cmds = new SqlCommand();
            cmds.Connection = conn;
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.CommandText = "[Request].[SP_GetSaleVoroodByStCode]";
            cmds.Parameters.AddWithValue("@stcode", stCode);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmds.ExecuteReader();
                dt.Load(rdr);
                conn.Close();

            }
            catch (Exception)
            {
                throw;
            }
            var dReturn = dt.Rows[0][0].ToString();
            return dReturn;
        }
        public int IsMojazCheckOut_144(string stCode)
        {
            SqlCommand cmds = new SqlCommand();
            cmds.Connection = conn;
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.CommandText = "Request.SP_IsMojazFaregh_Vahed_144";
            cmds.Parameters.AddWithValue("@stcode", stCode);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmds.ExecuteReader();
                dt.Load(rdr);
                conn.Close();

            }
            catch (Exception)
            {
                throw;
            }
            var dReturn = Convert.ToInt32(dt.Rows[0][0]);
            return dReturn;
        }
        public int IsMojazCheckOut_146(string stCode)
        {
            SqlCommand cmds = new SqlCommand();
            cmds.Connection = conn;
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.CommandText = "Request.SP_IsMojazFaregh_Vahed_146";
            cmds.Parameters.AddWithValue("@stcode", stCode);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmds.ExecuteReader();
                dt.Load(rdr);
                conn.Close();

            }
            catch (Exception)
            {
                throw;
            }
            var dReturn = Convert.ToInt32(dt.Rows[0][0]);
            return dReturn;
        }
        public int IsMojazCheckOut_148(string stCode)
        {
            SqlCommand cmds = new SqlCommand();
            cmds.Connection = conn;
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.CommandText = "Request.SP_IsMojazFaregh_Vahed_148";
            cmds.Parameters.AddWithValue("@stcode", stCode);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmds.ExecuteReader();
                dt.Load(rdr);
                conn.Close();

            }
            catch (Exception)
            {
                throw;
            }
            var dReturn = Convert.ToInt32(dt.Rows[0][0]);
            return dReturn;
        }
        public bool InsertCheckOutSign(byte[] sign, string userID, int status,string fromDate, string toDate)
        {
            SqlCommand cmds = new SqlCommand();
            cmds.Connection = conn;
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.CommandText = "Request.SP_InsertUpdateCheckOutSigns";
            cmds.Parameters.AddWithValue("@sign", sign);
            cmds.Parameters.AddWithValue("@userID", userID);
            cmds.Parameters.AddWithValue("@status", status);
            cmds.Parameters.AddWithValue("@fromDate", fromDate);
            cmds.Parameters.AddWithValue("@toDate", toDate);

            try
            {
                conn.Open();
                cmds.ExecuteNonQuery();
                conn.Close();
                return true;

            }
            catch (Exception)
            {
                throw;
            }
        }


        public int GetIsBachelor(string stcode)
        {
            int x = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_GetStudentMaghta";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();

                x = Convert.ToInt32(cmd.ExecuteScalar());
                //return 1 or 0
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return x;
        }
        public int Gethavenaghs(string stcode)
        {
            int x = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_GetStudentMaghta";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();

                x = Convert.ToInt32(cmd.ExecuteScalar());
                //return 1 or 0
                conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return x;
        }
        public DataTable GetCheckOutInfoByStCode(string stCode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_GetCheckOutInfoByStCodeForAdmin";
            cmd.Parameters.AddWithValue("@stcode", stCode);
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

                throw;

            }
            return dt;
        }

        public DataTable GetCheckOutInfoByStCodeAndFamily(FeraghatTahsilDTO oFeraghat)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetCheckOutInfoByStCodeAndFamily]";
            cmd.Parameters.AddWithValue("@stcode", oFeraghat.Stcode);
            cmd.Parameters.AddWithValue("@family", oFeraghat.family);
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

                throw;

            }
            return dt;
        }


        public List<MahaleSodoor> GetListOfVahed()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetListOFVahed]";
            List<MahaleSodoor> mhl = new List<MahaleSodoor>();

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

                throw;

            }
            var mahalList = new List<MahaleSodoor>();
            foreach (DataRow item in dt.Rows)
            {
                MahaleSodoor mhlSodoor = new MahaleSodoor();
                mhlSodoor.Id = Convert.ToInt32(item["id"]);
                mhlSodoor.Vahed = item["vahed"] as string;


                mahalList.Add(mhlSodoor);

            }
            return mahalList;
        }

        public List<Field> GetListOfReshte()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetAllField]";
            List<Field> fld = new List<Field>();

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

                throw;

            }
            var fieldList = new List<Field>();
            foreach (DataRow item in dt.Rows)
            {
                Field reshte = new Field();
                reshte.Id = Convert.ToInt32(item["id"]);
                reshte.field = item["nameresh"] as string;


                fieldList.Add(reshte);

            }
            return fieldList;
        }
        public List<Daneshkade> GetListOfDaneshkade()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[dbo].[SP_GetAllDaneshkade]";
            List<Daneshkade> dnsh = new List<Daneshkade>();

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

                throw;

            }
            var DaneshkadehList = new List<Daneshkade>();
            foreach (DataRow item in dt.Rows)
            {
                Daneshkade danesh = new Daneshkade();
                danesh.Id = Convert.ToInt32(item["id"]);
                danesh.daneshkade = item["namedanesh"] as string;


                DaneshkadehList.Add(danesh);

            }
            return DaneshkadehList;
        }
        public DataTable UpdateStatusOfStMsg(int reqId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_UpdateMessageStudent]";
            cmd.Parameters.AddWithValue("@reqID", reqId);
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
                throw;
            }
            return dt;
        }
        public DataTable UpdateStatusOfStMsgUnRead(int reqId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_UpdateMessageStudentUnRead]";
            cmd.Parameters.AddWithValue("@reqID", reqId);
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
                throw;
            }
            return dt;
        }
        public DataTable GetListOFRequestByNextStatusID(int nextstatus)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "[Request].[SP_GetListOFRequestByNextStatusID]";SP_GetListOFRequestByNextStatusID_Temp
            cmd.CommandText = "[Request].[SP_GetListOFRequestByNextStatusID]";
            cmd.Parameters.AddWithValue("@nextstatus", nextstatus);
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
                throw;
            }
            return dt;
        }
        public DataTable GetListOFRequestByNextStatusID_Excel(int nextstatus, int daneshID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetListOFRequestByNextStatusID_Excel]";
            cmd.Parameters.AddWithValue("@nextstatus", nextstatus);
            cmd.Parameters.AddWithValue("@daneshID", daneshID);
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
                throw;
            }
            return dt;
        }

        public DataTable GetListOFRequestByNextStatusID_BythesisFileStatus(int nextstatus, int thesisFileStatus)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetListOFRequestByNextStatusID_BythesisFileStatus]";
            cmd.Parameters.AddWithValue("@nextstatus", nextstatus);
            cmd.Parameters.AddWithValue("@thesisFileStatus", thesisFileStatus);
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
                throw;
            }
            return dt;
        }

        public DataTable GetListOFRequestByNextStatusIDForFaregh(int nextstatus)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "[Request].[SP_GetListOFRequestByNextStatusID]";SP_GetListOFRequestByNextStatusID_Temp
            cmd.CommandText = "[Request].[SP_GetListOFRequestByNextStatusIDForFaregh]";
            cmd.Parameters.AddWithValue("@nextstatus", nextstatus);
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
                throw;
            }
            return dt;
        }
        public DataTable GetStudentInfo(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetCheckOutStudentInfo]";
            cmd.Parameters.AddWithValue("@STCODE", stcode);
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

                throw;

            }
            return dt;
        }
        public DataTable GetStudentMadrakInfo(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetListMadarekTaieedShode";
            cmd.Parameters.AddWithValue("@stcode", stcode);
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

                throw;

            }
            return dt;
        }

        public DataTable getArchiveUserSignByStudentStcode(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[sp_getArchiveUserSignByStudentStcode]";
            cmd.Parameters.AddWithValue("@stcode", stcode);
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

                throw;

            }
            return dt;
        }

        public DataTable GetListOFRequestByCurrentStatusID(int status)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetListOFRequestByStatusID]";
            cmd.Parameters.AddWithValue("@status", status);
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

                throw;

            }
            return dt;
        }

        public int UpdateCheckOutRequest(int reqID, int currentstatus, int nextstatus, string note, string message, string studentmessage, bool isMashmool, int idVahed)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_UpdateCheckOutRequest";
            cmd.Parameters.AddWithValue("@reqID", reqID);
            cmd.Parameters.AddWithValue("@currentstatus", currentstatus);
            cmd.Parameters.AddWithValue("@nextstatus", nextstatus);
            cmd.Parameters.AddWithValue("@note", note);
            cmd.Parameters.AddWithValue("@message", message);
            cmd.Parameters.AddWithValue("@studentmessage", studentmessage);
            cmd.Parameters.AddWithValue("@isMashmool", isMashmool);
            cmd.Parameters.AddWithValue("@idVahed", idVahed);
            try
            {
                conn.Open();
                int reqId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                return reqId;


            }
            catch (Exception)
            {

                throw;

            }
        }

        public int UpdateCheckOutRequest(int reqID, int currentstatus, int nextstatus, string note, string message, string studentmessage, bool isMashmool)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_UpdateCheckOutRequestWithOutVahed";
            cmd.Parameters.AddWithValue("@reqID", reqID);
            cmd.Parameters.AddWithValue("@currentstatus", currentstatus);
            cmd.Parameters.AddWithValue("@nextstatus", nextstatus);
            cmd.Parameters.AddWithValue("@note", note);
            cmd.Parameters.AddWithValue("@message", message);
            cmd.Parameters.AddWithValue("@studentmessage", studentmessage);
            cmd.Parameters.AddWithValue("@isMashmool", isMashmool);
            //cmd.Parameters.AddWithValue("@idVahed", idVahed);
            try
            {
                conn.Open();
                int reqId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                return reqId;


            }
            catch (Exception)
            {

                throw;

            }
        }
        public void UpdateLastUpdate(int reqID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_Update_LastUpdate]";
            cmd.Parameters.AddWithValue("@reqID", reqID);


            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();



            }
            catch (Exception)
            {

                throw;

            }
        }

        public void DeleteCheckOutFromFraghat(int reqID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_DeleteFromFraghat";
            cmd.Parameters.AddWithValue("@reqID", reqID);


            try
            {
                conn.Open();
                int reqId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();



            }
            catch (Exception)
            {

                throw;

            }
        }
        public void UpdateReadyRequest(int reqID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_UpdateReadyRequest";
            cmd.Parameters.AddWithValue("@reqID", reqID);


            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();



            }
            catch (Exception)
            {

                throw;

            }
        }
        public int UpdateCheckOutRequestArchive(int reqID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_UpdateCheckOutRequestArchive";
            cmd.Parameters.AddWithValue("@reqID", reqID);

            try
            {
                conn.Open();
                int reqId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                return reqId;


            }
            catch (Exception)
            {

                throw;

            }
        }
        public int CheckIsReady(int ReqID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_CheckStudentIsReady";
            cmd.Parameters.AddWithValue("@ReqID", ReqID);

            try
            {
                conn.Open();
                int reqId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                return reqId;


            }
            catch (Exception)
            {

                throw;

            }
        }

        public int AddMessage(int reqID, string message)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_CheckOutSendMessage";
            cmd.Parameters.AddWithValue("@reqID", reqID);
            cmd.Parameters.AddWithValue("@message", message);

            try
            {
                conn.Open();
                int reqId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                return reqId;


            }
            catch (Exception)
            {

                throw;

            }
        }

        public int AddStudentMessage(int reqID, string message)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_CheckOutSendMessageStudent";
            cmd.Parameters.AddWithValue("@reqID", reqID);
            cmd.Parameters.AddWithValue("@message", message);

            try
            {
                conn.Open();
                int reqId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                return reqId;


            }
            catch (Exception)
            {

                throw;

            }
        }

        public string GetCheckOutIssuerID(int reqID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_GetCheckOutIssuerID";
            cmd.Parameters.AddWithValue("@reqID", reqID);

            try
            {
                conn.Open();
                string issuerID = cmd.ExecuteScalar() as string;
                conn.Close();
                return issuerID;


            }
            catch (Exception)
            {

                throw;

            }
        }

        public DataTable GetUsers()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn1;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "dbo.SP_Show_Allusers";
            cmd.Parameters.AddWithValue("@AppId", "12");
            DataTable dt = new DataTable();


            try
            {
                conn1.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn1.Close();


            }
            catch (Exception)
            {

                throw;

            }
            return dt;
        }

        //public DataTable GetCheckOutStudentInfoPrint(int reqID)
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = conn;
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "Request.SP_Get_StudentInfo";
        //    cmd.Parameters.AddWithValue("@reqID", reqID);
        //    DataTable dt = new DataTable();


        //    try
        //    {
        //        conn.Open();
        //        SqlDataReader rdr;
        //        rdr = cmd.ExecuteReader();
        //        dt.Load(rdr);
        //        conn.Close();

        //    }
        //    catch (Exception)
        //    {

        //        throw;

        //    }
        //    return dt;
        //}

        public byte[] GetSignByStatus(int reqStatus)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetCheckOutSignByStatus]";
            cmd.Parameters.AddWithValue("@status", reqStatus);
            byte[] img = null;


            try
            {
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    rdr.Read();
                    img = (byte[])rdr["SignImage"];
                }
                conn.Close();

            }
            catch (Exception)
            {

                throw;

            }
            return img;
        }

        public DataTable checkExistingRequest(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.sp_checkExistingRequest";
            cmd.Parameters.AddWithValue("@stcode", stcode);
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
                throw;
            }
            return dt;
        }

        public DataTable GetCheckOutStatusByRoleId(int RoleId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetListOFStatusByRoleID]";
            cmd.Parameters.AddWithValue("@Role", RoleId);
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
                throw;
            }
            return dt;
        }

        public DataTable GetonlineStatus(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetOnlineStatus]";
            cmd.Parameters.AddWithValue("@stcode", stcode);
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

                throw;

            }
            return dt;
        }
        public DataTable GetCheckOutTypes()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_GetCheckOutTypes";
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

                throw;

            }
            return dt;
        }

        public DataTable GetListOFRequestByTypeID(int TypeID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_GetListOFRequestByTypeID";
            cmd.Parameters.AddWithValue("@typeID", TypeID);
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

                throw;

            }
            return dt;
        }

        public bool CheckIsMashmool(string issuerID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_IsMashmool";
            cmd.Parameters.AddWithValue("@stcode", issuerID);

            int x;

            try
            {

                conn.Open();
                x = Convert.ToInt32(cmd.ExecuteNonQuery());
                conn.Close();
            }
            catch (Exception)
            {

                throw;

            }

            if (x > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int CheckIsMashmoolFeraghat(string Student)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_IsMashmool";
            cmd.Parameters.AddWithValue("@stcode", Student);
            int rdrmn;
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdrmn = Convert.ToInt32(cmd.ExecuteScalar());
                //dt.Load(rdr);
                conn.Close();

            }
            catch (Exception)
            {

                throw;

            }
            return rdrmn;
        }

        public DataTable GetUserRoleByUserID(int userID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_GetUserRoleByUserID";
            cmd.Parameters.AddWithValue("@userID", userID);
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

                throw;

            }
            return dt;
        }

        public void UpdatePrintStatus(int reqID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_UpdateCheckOutPrintStatus";
            cmd.Parameters.AddWithValue("@reqID", reqID);

            try
            {

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();


            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetSignByUser(string userID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetCheckOutSignByUserID]";
            cmd.Parameters.AddWithValue("@userID", userID);
            DataTable dt = new DataTable();


            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();


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

        public bool CheckIsMale(string issuerID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_IsMale";
            cmd.Parameters.AddWithValue("@stcode", issuerID);

            try
            {
                int x;
                conn.Open();
                x = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();

                return Convert.ToBoolean(x);

            }
            catch (Exception)
            {

                throw;

            }
        }

        public DataTable GetAllSigns()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_GetAllsigns";

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

                throw;

            }
            return dt;
        }

        public DataTable GetListOFRequestForArchive(int mashmoolanStatus)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_GetListOFRequestForArchiveByMashmoolanStatus";
            cmd.Parameters.AddWithValue("@mashmoolanStatus", mashmoolanStatus);
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

                throw;

            }
            return dt;
        }

        public DataTable GetCheckOutInfoByReqId(int reqID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Request.SP_GetCheckOutInfoByReqId";
            cmd.Parameters.AddWithValue("@reqID", reqID);
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

                throw;

            }
            return dt;
        }

        public DataTable GetListOFRequestByNextStatusAndDaneshId(int nextstatus, int daneshId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetListOFRequestByNextStatusAndDaneshId]";
            // cmd.CommandText = "[Request].[SP_GetListOFRequestByNextStatusAndDaneshId_111111]";
            cmd.Parameters.AddWithValue("@nextstatus", nextstatus);
            cmd.Parameters.AddWithValue("@daneshId", daneshId);
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

                throw;

            }
            return dt;
        }

        public DataTable GetListOFRequestByNextStatusAndArchiveRole(int nextstatus, int roleId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[GetListOFRequestByNextStatusAndArchiveRole]";
            cmd.Parameters.AddWithValue("@nextstatus", nextstatus);
            cmd.Parameters.AddWithValue("@RoleId", roleId);
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

                throw;

            }
            return dt;
        }

        public DataTable GetListOFRequestByCurrentStatusDaneshId(int status, int daneshId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetListOFRequestByCurrentStatusDaneshId]";
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@daneshId", daneshId);
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

                throw;

            }
            return dt;
        }
        public void UpdateStatusInReg(string stcode, int reqId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_UpdateStatusInReg]";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@reqId", reqId);


            try
            {
                conn.Open();
                SqlDataReader rdr;
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception)
            {

                throw;

            }

        }

        public int GetCheckOutStatusByreqID(int reqID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetCheckOutStatusByID]";
            cmd.Parameters.AddWithValue("@reqID", reqID);
            int requestLogID;

            try
            {
                int x;
                conn.Open();
                x = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                //conn.Open();
                //var temp = cmd.ExecuteScalar();
                //  requestLogID = Convert.ToInt32(temp);
                //conn.Close();
                requestLogID = x;

            }
            catch (Exception)
            {

                throw;

            }
            return requestLogID;
        }

        public bool insertThesisFile(string thesisPath_Doc, string thesisPath_PDF, string studentID)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandText = "[request].[sp_insertThesis]";
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.Parameters.AddWithValue("@studentID", studentID);
            cmn.Parameters.AddWithValue("@thesisPath_Doc", thesisPath_Doc);
            cmn.Parameters.AddWithValue("@thesisPath_Pdf", thesisPath_PDF);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                int result = cmn.ExecuteNonQuery();
                conn.Close();
                return result > 0;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
            }
        }

        public DataTable getThesisByStcode(string studentID)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandText = "[request].[sp_getThesisByStcode]";
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.Parameters.AddWithValue("@studentID", studentID);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = cmn.ExecuteReader();
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
        public DataTable getThesisByFiltering(string daneshID, string reshte, string family, string stcode)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandText = "[request].[sp_getThesisByFiltering]";
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.Parameters.AddWithValue("@danesh", daneshID);
            cmn.Parameters.AddWithValue("@resh", reshte);
            cmn.Parameters.AddWithValue("@family", family);
            cmn.Parameters.AddWithValue("@stcode", stcode);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                SqlDataReader dr = cmn.ExecuteReader();
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

        public DataTable GetNameOfVahedByVahedID(int idVahed)
        {
            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandText = "[Request].[SP_GetNameOFVahedByVahedId]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idVahed", idVahed);
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

                throw;

            }
            return dt;
        }

        public bool denyThesis(string studentID)
        {
            SqlCommand cmn = new SqlCommand("", conn);
            cmn.CommandText = "[request].[sp_denyThesis]";
            cmn.CommandType = CommandType.StoredProcedure;
            cmn.Parameters.AddWithValue("@studentID", studentID);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                int result = cmn.ExecuteNonQuery();
                conn.Close();
                return result > 0;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return false;
            }
        }
        public DataTable GetListOFRequestByVahedAndDate(int idVahed, string startDate, string endDate, int MadrakType)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetListOFRequestByVahedSodoorAndVoroodDate]";

            cmd.Parameters.AddWithValue("@idVahed", idVahed);
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            cmd.Parameters.AddWithValue("@MadrakType", MadrakType);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();

            }
            catch (Exception ex)
            {

                throw;

            }
            return dt;
        }

        public void InsertApproveDatebyFaregh(int reqId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_InsertUpdateApproveFaregh]";

            cmd.Parameters.AddWithValue("@StudentRequestId", reqId);



            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertApproveDatebyMali(int reqId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_InsertUpdateApproveMali]";

            cmd.Parameters.AddWithValue("@StudentRequestId", reqId);



            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetListOFApproveList()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetListOFApproveList]";


            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();

            }
            catch (Exception ex)
            {

                throw;

            }
            return dt;
        }
        public void SetSendToPay(List<int> reqId, int userId, string IpAddress)
        {
            using (var dt = new DataTable())
            {
                dt.Columns.Add("reqId", typeof(int));
                foreach (var item in reqId)
                    dt.Rows.Add(item);


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[Request].[SP_InsertDateSendToPay]";

                cmd.Parameters.Add("@ReqId", SqlDbType.Structured).Value = dt;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@IpAddress", IpAddress);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();


                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable getAllArchiveID_GraduateDocuments()
        {
            SqlCommand cmd = new SqlCommand("[Request].[sp_getAllArchiveID_GraduateDocuments]", conn);
            cmd.CommandType = CommandType.StoredProcedure;
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

        public void SetDateApprove(List<int> reqId, int userId, string IpAddress)
        {
            using (var dt = new DataTable())
            {
                dt.Columns.Add("reqId", typeof(int));
                foreach (var item in reqId)
                    dt.Rows.Add(item);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[Request].[SP_InsertDateApprovePay]";

                cmd.Parameters.Add("@ReqId", SqlDbType.Structured).Value = dt;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@IpAddress", IpAddress);



                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();


                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public DataTable GetListOfMadarekByDateSodoor(DataTable idVahed, string startDate, string endDate, DataTable idResh, DataTable iddanesh, int idCaseStatus, int idMadrakStatus, int madrakTypeid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetListOfMadarekByDateSodoor]";

            // cmd.Parameters.AddWithValue("@vahedId", idVahed);
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            //cmd.Parameters.AddWithValue("@idResh", idResh);
            // cmd.Parameters.AddWithValue("@idDanesh", iddanesh);
            cmd.Parameters.AddWithValue("@idCaseStatus", idCaseStatus);
            cmd.Parameters.AddWithValue("@idMadrakStatus", idMadrakStatus);
            cmd.Parameters.Add("@fields", SqlDbType.Structured).Value = idResh;
            cmd.Parameters.Add("@danesh", SqlDbType.Structured).Value = iddanesh;
            cmd.Parameters.Add("@vahed", SqlDbType.Structured).Value = idVahed;
            cmd.Parameters.AddWithValue("@madrakTypeid", madrakTypeid);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();

            }
            catch (Exception ex)
            {

                throw;

            }
            return dt;
        }
        public DataTable GetListOfCaseInKarTabl(DataTable idVahed, string startDate, string endDate, DataTable idResh, DataTable iddanesh, int idCaseStatus, int idMadrakStatus, int madrakTypeid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetListOfCaseInKartabl]";

            // cmd.Parameters.AddWithValue("@vahedId", idVahed);
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            //cmd.Parameters.AddWithValue("@idResh", idResh);
            // cmd.Parameters.AddWithValue("@idDanesh", iddanesh);
            cmd.Parameters.AddWithValue("@idCaseStatus", idCaseStatus);
            cmd.Parameters.AddWithValue("@idMadrakStatus", idMadrakStatus);
            cmd.Parameters.Add("@fields", SqlDbType.Structured).Value = idResh;
            cmd.Parameters.Add("@danesh", SqlDbType.Structured).Value = iddanesh;
            cmd.Parameters.Add("@vahed", SqlDbType.Structured).Value = idVahed;
            cmd.Parameters.AddWithValue("@madrakTypeid", madrakTypeid);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();

            }
            catch (Exception ex)
            {

                throw;

            }
            return dt;
        }
        public DataTable GetListOfMadrakVoroodUni(DataTable idVahed, string startDate, string endDate, DataTable idResh, DataTable iddanesh, int idCaseStatus, int idMadrakStatus, int madrakTypeid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetListOfVoroodUni]";

            // cmd.Parameters.AddWithValue("@vahedId", idVahed);
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            //cmd.Parameters.AddWithValue("@idResh", idResh);
            // cmd.Parameters.AddWithValue("@idDanesh", iddanesh);
            cmd.Parameters.AddWithValue("@idCaseStatus", idCaseStatus);
            cmd.Parameters.AddWithValue("@idMadrakStatus", idMadrakStatus);
            cmd.Parameters.Add("@fields", SqlDbType.Structured).Value = idResh;
            cmd.Parameters.Add("@danesh", SqlDbType.Structured).Value = iddanesh;
            cmd.Parameters.Add("@vahed", SqlDbType.Structured).Value = idVahed;
            cmd.Parameters.AddWithValue("@madrakTypeid", madrakTypeid);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();

            }
            catch (Exception ex)
            {

                throw;

            }
            return dt;
        }
        public DataTable GetListOfExitCaseFromKartabl(DataTable idVahed, string startDate, string endDate, DataTable idResh, DataTable iddanesh, int idCaseStatus, int idMadrakStatus, int madrakTypeid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[Request].[SP_GetListOfExitCaseFromKartabl]";

            // cmd.Parameters.AddWithValue("@vahedId", idVahed);
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            //cmd.Parameters.AddWithValue("@idResh", idResh);
            // cmd.Parameters.AddWithValue("@idDanesh", iddanesh);
            cmd.Parameters.AddWithValue("@idCaseStatus", idCaseStatus);
            cmd.Parameters.AddWithValue("@idMadrakStatus", idMadrakStatus);
            cmd.Parameters.Add("@fields", SqlDbType.Structured).Value = idResh;
            cmd.Parameters.Add("@danesh", SqlDbType.Structured).Value = iddanesh;
            cmd.Parameters.Add("@vahed", SqlDbType.Structured).Value = idVahed;
            cmd.Parameters.AddWithValue("@madrakTypeid", madrakTypeid);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmd.ExecuteReader();
                dt.Load(rdr);
                conn.Close();

            }
            catch (Exception ex)
            {

                throw;

            }
            return dt;
        }

        public void AddSignature(byte[] imageBytes, int identityNumber, int appId)
        {
            var cmd = new SqlCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Request].[SP_AddSignature]"
            };
            cmd.Parameters.AddWithValue("@imageBytes", imageBytes);
            cmd.Parameters.AddWithValue("@identityNumber", identityNumber);
            cmd.Parameters.AddWithValue("@appId", appId);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int exist_IdMelli(string StCode)
        {
            var cmd = new SqlCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Request].[SP_IsIdMelliExist]"
            };
            cmd.Parameters.AddWithValue("@StCode", StCode);
            int rdrmn;

            try
            {
                conn.Open();
                rdrmn = Convert.ToInt32(cmd.ExecuteScalar());

                conn.Close();

                return rdrmn;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable GetSignByLogDate(int State, int reqID)
        {
            var cmd = new SqlCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Request].[SP_GetSignsByLogDate]"
            };
            cmd.Parameters.AddWithValue("@RequestLogID", State);
            cmd.Parameters.AddWithValue("@modifyID", reqID);
            DataTable dt = new DataTable();
            SqlDataReader dr;

            try
            {
                conn.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                conn.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable GetLogDatesignByModifyID(int reqID)
        {
            var cmd = new SqlCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Request].[sp_getArchiveSignDateByModifyID]"
            };

            cmd.Parameters.AddWithValue("@modifyID", reqID);
            DataTable dt = new DataTable();
            SqlDataReader dr;

            try
            {
                conn.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                conn.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable GetfishNaghsIdBystcode(string stcode)
        {
            var cmd = new SqlCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = "[Request].[SP_GetfishNaghsIdByStcode]"
            };

            cmd.Parameters.AddWithValue("@stcode", stcode);
            DataTable dt = new DataTable();
            SqlDataReader dr;

            try
            {
                conn.Open();
                dr = cmd.ExecuteReader();
                dt.Load(dr);
                conn.Close();

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable getStampRecieptDefect(Int64 reqID)
        {
            SqlCommand cmd = new SqlCommand("request.sp_getStampRecieptDefect", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@requestID", reqID);
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
                throw;
            }
            return dt;

        }

        public int insertCheckoutReason(Int64 requestID, string stcode, string reason)
        {
            SqlCommand cmd = new SqlCommand("request.sp_InsertCheckoutReason", conn);
            cmd.Parameters.AddWithValue("@reqID", requestID);
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@reason", reason);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                int id = (int)cmd.ExecuteScalar();
                conn.Close();
                return id;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return 0;
            }
        }
        public int updateCheckoutReasonFrequency(Int64 requestID, string stcode, int frequencyID)
        {
            SqlCommand cmd = new SqlCommand("request.sp_UpdateStudentReasonFrequency", conn);
            cmd.Parameters.AddWithValue("@reqID", requestID);
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@frequency", frequencyID);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                int id = (int)cmd.ExecuteScalar();
                conn.Close();
                return id;
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return 0;
            }
        }
        public DataTable SelectCheckOutReasons(Int64 requestID)
        {
            SqlCommand cmd = new SqlCommand("request.sp_selectCheckOutReasons", conn);
            cmd.Parameters.AddWithValue("@reqID", requestID);
            cmd.CommandType = CommandType.StoredProcedure;
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
        public DataTable SelectFrequencyReasons()
        {
            SqlCommand cmd = new SqlCommand("request.sp_GetAllReasonFrequency", conn);
            cmd.CommandType = CommandType.StoredProcedure;
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

        public DataTable getCheckoutFrequencyIteration()
        {
            SqlCommand cmd = new SqlCommand("request.sp_getCheckOutReasonIteration", conn);
            cmd.CommandType = CommandType.StoredProcedure;
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
        public DataTable getCheckoutFrequencyAllStudents()
        {
            SqlCommand cmd = new SqlCommand("request.sp_getCheckoutReasonAllStudents", conn);
            cmd.CommandType = CommandType.StoredProcedure;
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
        public DataTable getCheckoutFrequencyByDepartment()
        {
            SqlCommand cmd = new SqlCommand("request.sp_getCheckOutReasonByDepartment", conn);
            cmd.CommandType = CommandType.StoredProcedure;
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
        public DataTable getCheckoutFrequencyByLevel()
        {
            SqlCommand cmd = new SqlCommand("request.sp_getCheckOutReasonByLevel", conn);
            cmd.CommandType = CommandType.StoredProcedure;
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
        public DataTable getCheckoutFrequencyByEnterYear()
        {
            SqlCommand cmd = new SqlCommand("request.sp_getCheckOutReasonByEnterYear", conn);
            cmd.CommandType = CommandType.StoredProcedure;
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
    }
}
