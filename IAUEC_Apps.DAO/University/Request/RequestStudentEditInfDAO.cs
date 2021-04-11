using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using IAUEC_Apps.DAC.Connections;

namespace IAUEC_Apps.DAO.University.Request
{ 
    public class RequestStudentEditInfDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
        SqlConnection conn_log = new SqlConnection(new SuppConnection().log_con);
        #region read


        public DataTable getStudentInfoFromInitialRegistration(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "request.sp_getStudentInfoFromInitialRegistration";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", stcode);
            DataTable dt = new DataTable();
            try
            {
                if(conn.State== ConnectionState.Closed)
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
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }

        public DataTable getStudentInfoFromInitialRegistration_International(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "request.sp_getStudentInfoFromInitialRegistration_International";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", stcode);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
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
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return dt;
        }


        public void updateStudentInfo(string stcode,int editPersonalID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "request.sp_updateStudentInf";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@EditPersonalInformationID", editPersonalID);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                cmd.ExecuteNonQuery();
               
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        public void updateStudentChild(string stcode, int editPersonalID,int childID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "request.sp_updateStudentChild";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@EditPersonalInformationID", editPersonalID);
            cmd.Parameters.AddWithValue("@childID", childID);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
        public void insertStudentChild(string stcode, int editPersonalID_name, int editPersonalID_family, int editPersonalID_gender, int editPersonalID_birthday)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "request.sp_insertStudentChild";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@editPersonalID_name", editPersonalID_name);
            cmd.Parameters.AddWithValue("@editPersonalID_family", editPersonalID_family);
            cmd.Parameters.AddWithValue("@editPersonalID_gender", editPersonalID_gender);
            cmd.Parameters.AddWithValue("@editPersonalID_birthday", editPersonalID_birthday);
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                cmd.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public DataTable getallrequest(int requesttype)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from Request.Tbl_StudentRequest where RequestTypeID="+ requesttype;
            cmd.CommandType = CommandType.Text;
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
        public DataTable GetMaxEvents(int EventID,string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn_log;
            cmd.CommandText = "[dbo].[SP_GetMaxEvents]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Event",EventID);
            cmd.Parameters.AddWithValue("@stcode", stcode);
            DataTable dt = new DataTable();
            try
            {
                conn_log.Open();
                SqlDataReader rdr;
                rdr=cmd.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn_log.Close();
                cmd.Dispose();
            }
            catch(Exception)
            { throw; }
            return dt;
        }

       

        public DataTable PixChangeRequestedEdit()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Request.SP_PixChangeRequestedEdit";
            cmd.CommandType = CommandType.StoredProcedure;
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
            catch(Exception)
            { throw; }
            return dt;

        }



        public DataTable GetReportEditImageRequest(int RequestLogID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "[Request].[SP_GetReportEditImageRequest]";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RequestLogID",RequestLogID);
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
            catch(Exception)
            { throw; }
            return dt;
        }
        public DataTable GetReportEditRequest(int RequestLogID)
        {
            SqlCommand cmdgetr = new SqlCommand();
            cmdgetr.Connection = conn;
            cmdgetr.CommandText = "Request.SP_GetReportEditRequest";
            cmdgetr.CommandType = CommandType.StoredProcedure;
            cmdgetr.Parameters.AddWithValue("@RequestLogID",RequestLogID);
            DataTable dt=new DataTable();
            try 
            {
                conn.Open();
                SqlDataReader rdr;
                rdr=cmdgetr.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdgetr.Dispose();
            }
            catch(Exception)
            {
                throw;

            }
            return dt;
        }


        public DataTable GetStudentMobile(string stcode)
        {
            SqlCommand cmdmob = new SqlCommand();
            cmdmob.Connection = conn;
            cmdmob.CommandText = "Request.SP_GetMobile";
            cmdmob.CommandType = CommandType.StoredProcedure;
            DataTable dtmob = new DataTable();
            cmdmob.Parameters.AddWithValue("@stcode",stcode);
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdmob.ExecuteReader();
                dtmob.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdmob.Dispose();
            }
            catch(Exception)
            {
                throw;
            }
            return dtmob;
        }

        public DataTable GetStudentRequestID(string stcode,int RequestLogID,int RequestTypeID)
        {
            SqlCommand cmdmax = new SqlCommand();
            cmdmax.Connection = conn;
            cmdmax.CommandText = "Request.SP_StudentRequestID";
            cmdmax.CommandType = CommandType.StoredProcedure;
            cmdmax.Parameters.AddWithValue("@stcode",stcode);
            cmdmax.Parameters.AddWithValue("@RequestTypeID",RequestTypeID);
            cmdmax.Parameters.AddWithValue("@RequestLogID",RequestLogID);
            DataTable dtmax = new DataTable();
            try 
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdmax.ExecuteReader();
                dtmax.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdmax.Dispose();
            }
            catch(Exception)
            { throw; }
            return dtmax;
        }

        public DataTable GetEditedIdOfStcode(string stcode)
        {
            SqlCommand cmdedit = new SqlCommand();
            cmdedit.Connection = conn;
            cmdedit.CommandText = "Request.SP_GetEditeIDandSTcode";
            cmdedit.CommandType = CommandType.StoredProcedure;
            cmdedit.Parameters.AddWithValue("@stcode",stcode);
            DataTable dtnedit=new DataTable();
            try 
            {
                conn.Open();
                SqlDataReader rdr;
                rdr=cmdedit.ExecuteReader();
                dtnedit.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdedit.Dispose();
            }
            catch(Exception)
            {
                throw;
            }
            return dtnedit;
        }

        public DataTable GetStEditRequest(string stcode)
        {
            SqlCommand cmder = new SqlCommand();
            cmder.Connection = conn;
            cmder.CommandText = "Request.SP_GetEditeRequestStatus";
            cmder.CommandType = CommandType.StoredProcedure;
            cmder.Parameters.AddWithValue("@stcode",stcode);
            DataTable dtre = new DataTable();
            try 
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmder.ExecuteReader();
                dtre.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmder.Dispose();

            }
            catch(Exception)
            {
                throw;
            }
            return dtre;
        }

        public DataTable GetStudentPic(string stcode)
        {
            SqlCommand cmdpic = new SqlCommand();
            cmdpic.Connection = conn;
            cmdpic.CommandText = "Request.SP_GetPersonalpic";
            cmdpic.CommandType = CommandType.StoredProcedure;
            cmdpic.Parameters.AddWithValue("@stcode",stcode);
            DataTable dtp=new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr=cmdpic.ExecuteReader();
                dtp.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdpic.Dispose();
            }
            catch(Exception)
            {
                throw;
            }
            return dtp;
        }

        public DataTable GetSTudentEditRequest(string stcode)
        {
            SqlCommand cmdstedit = new SqlCommand();
            cmdstedit.Connection = conn;
            cmdstedit.CommandType = CommandType.StoredProcedure;
            cmdstedit.CommandText = "Request.SP_GetSTudentEditRequest";
            cmdstedit.Parameters.AddWithValue("@stcode",stcode);
            DataTable dt = new DataTable();
            try 
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdstedit.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdstedit.Dispose();
            }
            catch(Exception)
            {
                throw;
            }
            return dt;
        }

        public DataTable GetStcodeEditeRequest(string stcode)
        {
            SqlCommand cmdg = new SqlCommand();
            cmdg.Connection = conn;
            cmdg.CommandText = "Request.SP_StcodeEditeRequest";
            cmdg.CommandType = CommandType.StoredProcedure;
            cmdg.Parameters.AddWithValue("@stcode",stcode);
            DataTable dt=new DataTable();
            try 
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdg.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdg.Dispose();
            }
            catch(Exception)
            {
                throw;
            }
            return dt;
        }

       


        public DataTable GetEditRequestInf(string stcode,int EditedID)
        {
            SqlCommand cmde = new SqlCommand();
            cmde.Connection = conn;
            cmde.CommandType = CommandType.StoredProcedure;
            cmde.CommandText = "Request.SP_GetEditRequestInf";
            cmde.Parameters.AddWithValue("@stcode",stcode);
            cmde.Parameters.AddWithValue("@EditedID",EditedID);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmde.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmde.Dispose();
            }
            catch(Exception)
            {
                throw;
            }
            return dt;
        }



        public DataTable GetStPersonalInf(string stcode)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Request.SP_GetStPersonalInf";
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
            catch(Exception)
            {
                throw;
            }
            return dt;
        }
        //create by Bahrami
        public DataTable GetPicEditRequest()
        {
            SqlCommand cmdg = new SqlCommand();
            cmdg.Connection = conn;
            cmdg.CommandText = "Request.SP_GetPicEditRequest";
            cmdg.CommandType = CommandType.StoredProcedure;
            DataTable dtp = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdg.ExecuteReader();
                dtp.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdg.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return dtp;
        }
        public DataTable GetEditRequest()
        {
            SqlCommand cmdg = new SqlCommand();
            cmdg.Connection = conn;
            cmdg.CommandText = "Request.SP_GetStcodeEditRequest";
            cmdg.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try 
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmdg.ExecuteReader();
                dt.Load(rdr);
                rdr.Dispose();
                conn.Close();
                cmdg.Dispose();

            }
            catch(Exception)
            {
                throw;
            }
            return dt;
        }

        #endregion
        # region Write
        public void insertCreateDateRequest(string stcode,string createdate,int EventId)
        {
            SqlCommand cmdi = new SqlCommand();
            cmdi.Connection = conn;
            cmdi.CommandText = "[dbo].[SP_insertCreateDateRequest]";
            cmdi.CommandType = CommandType.StoredProcedure;
            cmdi.Parameters.AddWithValue("@stcode", stcode);
            cmdi.Parameters.AddWithValue("@CreateDate", createdate);
            cmdi.Parameters.AddWithValue("@Event", EventId);
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
        public void InsertShahrestanOstanFSF2(string Ostan,string Shahrestan)
        {
            SqlCommand cmdi=new SqlCommand();
            cmdi.Connection=conn;
            cmdi.CommandText="Request.SP_InsertIntoFSF2OstanShahrestan";
            cmdi.CommandType=CommandType.StoredProcedure;
            cmdi.Parameters.AddWithValue("@Ostan",Ostan);
            cmdi.Parameters.AddWithValue("@Shahrestan",Shahrestan);
            try
            { 
                conn.Open();
                cmdi.ExecuteNonQuery();
                conn.Close();
                cmdi.Dispose();
            }
            catch(Exception)
            {
                throw;
            }
 
        }

        public void InsertIntoStudentRequest(string stcode,int RequestTypeID, int RequestLogID)
        {
            SqlCommand cmdedit=new SqlCommand();
            cmdedit.Connection=conn;
            cmdedit.CommandText="Request.SP_InsertIntoRequestTbl";
            cmdedit.CommandType=CommandType.StoredProcedure;
            cmdedit.Parameters.AddWithValue("@stcode",stcode);
            cmdedit.Parameters.AddWithValue("@RequestTypeID",RequestTypeID);
            cmdedit.Parameters.AddWithValue("@RequestLogID",RequestLogID);
          
            try
            {
                conn.Open();
                cmdedit.ExecuteNonQuery();
                conn.Close();
                cmdedit.Dispose();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public void InsertImageIntoEditePerInfo(string stcode, int EditedID, byte[] PersonalImage, int RequestLogID, int StudentRequestID)
        {
            SqlCommand cmdEI = new SqlCommand();
            cmdEI.Connection = conn;
            cmdEI.CommandText = "Request.SP_InsertPhoto";
            cmdEI.CommandType = CommandType.StoredProcedure;
            cmdEI.Parameters.AddWithValue("@stcode",stcode);
            cmdEI.Parameters.AddWithValue("@EditedID", EditedID);
            cmdEI.Parameters.AddWithValue("@PersonalImage", PersonalImage);
            cmdEI.Parameters.AddWithValue("@RequestLogID",RequestLogID);
            cmdEI.Parameters.AddWithValue("@StudentRequestID", StudentRequestID);
            try
            {
                conn.Open();
                cmdEI.ExecuteNonQuery();
                conn.Close();
                cmdEI.Dispose();
            }
            catch(Exception)
            {
                throw;
            }
        }



        public int InsertIntoEditPersonalInf(string stcode, string NewContent, int EditedID, int RequestLogID, int StudentRequestID)
        {
            SqlCommand cmdi = new SqlCommand();
            cmdi.Connection = conn;
            cmdi.CommandText = "Request.SP_InsertIntoTblEditPersonalInfo";
            cmdi.CommandType = CommandType.StoredProcedure;
            cmdi.Parameters.AddWithValue("@stcode", stcode);
            cmdi.Parameters.AddWithValue("@NewContent", NewContent);
            cmdi.Parameters.AddWithValue("@EditedID", EditedID);
            cmdi.Parameters.AddWithValue("@RequestLogID",RequestLogID);
            cmdi.Parameters.AddWithValue("@StudentRequestID", StudentRequestID);
            int editID = 0;
            try
            {
                conn.Open();
                editID=Convert.ToInt32(cmdi.ExecuteScalar());
                conn.Close();
                cmdi.Dispose();
            }
            catch(Exception)
            {
                throw;
            }
            return editID;
        }



        #endregion
        #region update

        public void UpdatePhotoinSida(byte[] stu_pic,string stcode)
        {
            SqlCommand cmd=new SqlCommand();
            cmd.Connection=conn;
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText="Request.SP_UpdatePhotoinSida";
            cmd.Parameters.AddWithValue("@stu_pic",stu_pic);
            cmd.Parameters.AddWithValue("@stcode",stcode);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                cmd.Dispose();
            }
            catch(Exception)
            { throw; }
        }


        public void UpdateimageRequest(byte[] personalimage, int StudentRequestID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Request.SP_UpdateimageRequest";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@personalimage", personalimage);
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

        public void UpdateStudentEditeRequestLogID(string stcode,int RequestLogID,int RequestTypeID)
        {
            SqlCommand cmdupdst=new SqlCommand();
            cmdupdst.Connection=conn;
            cmdupdst.CommandText="Request.SP_UpdateSTEditReqLogID";
            cmdupdst.CommandType=CommandType.StoredProcedure;
            cmdupdst.Parameters.AddWithValue("@stcode",stcode);
            cmdupdst.Parameters.AddWithValue("@RequestLogID",RequestLogID);
            cmdupdst.Parameters.AddWithValue("@RequestTypeID",RequestTypeID);
            
            try 
            {
                conn.Open();
                cmdupdst.ExecuteNonQuery();
                conn.Close();
                cmdupdst.Dispose();
            }
            catch(Exception)
            {
                throw;
            }
        }
        public void UpdateStLogID(string stcode,int EditedID, int RequestLogID)
        {
            SqlCommand cmdrequp=new SqlCommand();
            cmdrequp.Connection=conn;
            cmdrequp.CommandType=CommandType.StoredProcedure;
            cmdrequp.CommandText="Request.SP_UpdateLogIDstReq";
            cmdrequp.Parameters.AddWithValue("@stcode",stcode);
            cmdrequp.Parameters.AddWithValue("@EditedID",EditedID);
            cmdrequp.Parameters.AddWithValue("@RequestLogID",RequestLogID);
            try 
            {
                conn.Open();
                cmdrequp.ExecuteNonQuery();
                conn.Close();
                cmdrequp.Dispose();

            }
            catch(Exception)
            {
                throw;
            }
        }

        public void UpdateTozihat(string stcode, int EditedID, string Tozihat)
        {
            SqlCommand cmdtoz = new SqlCommand();
            cmdtoz.Connection = conn;
            cmdtoz.CommandText = "Request.SP_InsertTozihatEditPersInf";
            cmdtoz.CommandType = CommandType.StoredProcedure;
            cmdtoz.Parameters.AddWithValue("@stcode",stcode);
            cmdtoz.Parameters.AddWithValue("@EditedID",EditedID);
            cmdtoz.Parameters.AddWithValue("@Tozihat",Tozihat);
            try 
            {
                conn.Open();
                cmdtoz.ExecuteNonQuery();
                conn.Close();
                cmdtoz.Dispose();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public void UpdateEditPersonalLogID(string stcode, int EditedID, int RequestLogID)
        {
            SqlCommand cmdupe = new SqlCommand();
            cmdupe.Connection = conn;
            cmdupe.CommandText = "Request.SP_UpdateEditPersInfLogID";
            cmdupe.CommandType = CommandType.StoredProcedure;
            cmdupe.Parameters.AddWithValue("@stcode",stcode);
            cmdupe.Parameters.AddWithValue("@EditedID",EditedID);
            cmdupe.Parameters.AddWithValue("@RequestLogID",RequestLogID);
            try
            {
                conn.Open();
                cmdupe.ExecuteNonQuery();
                conn.Close();
                cmdupe.Dispose();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public void UpdateIsOk(string stcode, int EditedID,int RequestLogID,string Tozihat)
        {
            SqlCommand cmdup = new SqlCommand();
            cmdup.Connection = conn;
            cmdup.CommandType = CommandType.StoredProcedure;
            cmdup.CommandText = "Request.SP_UpdateIsOK";
            cmdup.Parameters.AddWithValue("@stcode",stcode);
            cmdup.Parameters.AddWithValue("@EditedID",EditedID);
            cmdup.Parameters.AddWithValue("@RequestLogID", RequestLogID);
            cmdup.Parameters.AddWithValue("@Tozihat",Tozihat);
            try
            {
                conn.Open();
                cmdup.ExecuteNonQuery();
                conn.Close();
                cmdup.Dispose();

            }
            catch(Exception)
            {
                throw;
            }
        }

        public void updateEditedStuImage(string stcode,byte[] PersonalImage)
        {
            SqlCommand cmdpics = new SqlCommand();
            cmdpics.Connection = conn;
            cmdpics.CommandText = "Request.SP_Updateimage";
            cmdpics.CommandType = CommandType.StoredProcedure;
            cmdpics.Parameters.AddWithValue("@stcode",stcode);
            cmdpics.Parameters.AddWithValue("@PersonalImage",PersonalImage);
            try
            {
                conn.Open();
                cmdpics.ExecuteNonQuery();
                conn.Close();
                cmdpics.Dispose();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public void UpdateEditedInfoFSF2(string stcode,string tel,string address,string codeposti,int Ostan,int Shahrestan,string mobile)
        {
            SqlCommand cmdu = new SqlCommand();
            cmdu.Connection = conn;
            cmdu.CommandText = "Request.SP_UpdateEditedInfoFSF2";
            cmdu.CommandType = CommandType.StoredProcedure;
            cmdu.Parameters.AddWithValue("@stcode",stcode);
            cmdu.Parameters.AddWithValue("@tel",tel);
            cmdu.Parameters.AddWithValue("@addressd",address);
            cmdu.Parameters.AddWithValue("@codeposti",codeposti);
            cmdu.Parameters.AddWithValue("@Ostan",Ostan);
            cmdu.Parameters.AddWithValue("@Shahrestan",Shahrestan);
            cmdu.Parameters.AddWithValue("@mobile", mobile);
            try
            {
                conn.Open();
                cmdu.ExecuteNonQuery();
                conn.Close();
                cmdu.Dispose();
            }
            catch(Exception ex)
            {
                throw;
            }
        }



        #endregion

    }
}
