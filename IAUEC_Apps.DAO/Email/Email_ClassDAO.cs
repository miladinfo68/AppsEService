using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DTO.EmailClasses;
using IAUEC_Apps.DAC.Connections;
using System.Data;
using System.Data.SqlClient;

namespace IAUEC_Apps.DAO.Email
{
     public class Email_ClassDAO
    {
         SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
         #region Create

         public void Create_Email(Email_Class Email_Class)
         {
             conn.Open();
             SqlCommand cmd = new SqlCommand("[Email].[SP_Email_InsertIntoEmailReg]", conn);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.Add("@STCODE", SqlDbType.VarChar);
             cmd.Parameters["@STCODE"].Value = Email_Class.Stcode;
             cmd.Parameters.Add("@EMAIL_ADDRESS", SqlDbType.VarChar);
             cmd.Parameters["@EMAIL_ADDRESS"].Value = Email_Class.Email_Address;
             cmd.Parameters.Add("@PASSWORD", SqlDbType.VarChar);
             cmd.Parameters["@PASSWORD"].Value = Email_Class.Password;
             cmd.Parameters.Add("@CEmail", SqlDbType.VarChar);
             cmd.Parameters["@CEmail"].Value = Email_Class.CEMAIL;
             cmd.Parameters.Add("@ConnectType", SqlDbType.VarChar);
             cmd.Parameters["@ConnectType"].Value = Email_Class.ConnectType;
             cmd.Parameters.Add("@UpdateEmail", SqlDbType.Bit);
             cmd.Parameters["@UpdateEmail"].Value = Email_Class.UpdateEmail;
             cmd.Parameters.Add("@Mobile", SqlDbType.VarChar);
             cmd.Parameters["@Mobile"].Value = Email_Class.Mobile;
             cmd.ExecuteNonQuery();
             cmd.Connection.Close();
             cmd.Dispose();
             conn.Close();
         }
         #endregion

         #region Read

         public DataTable GetTextMessage(int type, int AppID, int Category, int status)
         {
             SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "dbo.SP_GetConnectTextByStatus";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@Category", Category);
            cmd.Parameters.AddWithValue("@AppID", AppID);
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
        
         public DataTable CheckEmailStudent_ByStcode(string stcode)
         {
             SqlDataReader rdr = null;
             DataTable dt = new DataTable();
             conn.Open();
             SqlCommand cmd = new SqlCommand("Email.SP_EMAIL_REG", conn);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.Add("@STCODE", SqlDbType.VarChar);
             cmd.Parameters["@STCODE"].Value = stcode;
             rdr = cmd.ExecuteReader();
             dt.Load(rdr);
             rdr.Dispose();
             cmd.Connection.Close();
             cmd.Dispose();
             conn.Close();
             return dt;
         }
         public bool CheckEmailName(string Email)
         {
             SqlDataReader rdr = null;
             bool Check = false;
             conn.Open();
             SqlCommand cmd = new SqlCommand("Email.SP_Email_CheckEmailIsUnique", conn);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.Add("@EMAIL_ADDRESS", SqlDbType.VarChar);
             cmd.Parameters["@EMAIL_ADDRESS"].Value = Email;
             rdr = cmd.ExecuteReader();
             if (rdr.Read())
             {
                 int EmailCount = int.Parse(rdr[0].ToString());
                 if (EmailCount > 0)
                     Check = true;
             }
             rdr.Dispose();
             cmd.Connection.Close();
             cmd.Dispose();
             conn.Close();
             return Check;
         }
         public DataTable GiveList_Status_Zero()
         {
             SqlDataReader rdr = null;
             DataTable dt = new DataTable();
             conn.Open();
             SqlCommand cmd = new SqlCommand("Email.SP_Email_GetStudentUnknownStatus", conn);
             cmd.CommandType = CommandType.StoredProcedure;
             rdr = cmd.ExecuteReader();
             dt.Load(rdr);
             rdr.Dispose();
             cmd.Connection.Close();
             cmd.Dispose();
             conn.Close();
             return dt;
         }
         public DataTable GetAllStudentEmailInfo(int status)
         {
             SqlCommand cmdinf=new SqlCommand();
             cmdinf.Connection=conn;
             cmdinf.CommandText="Email.SP_GetStEmailInfo";
             cmdinf.CommandType=CommandType.StoredProcedure;
             cmdinf.Parameters.AddWithValue("@status",status);
             DataTable dt=new DataTable();

             try
             {
                 conn.Open();
                 SqlDataReader rdr;
                 rdr = cmdinf.ExecuteReader();
                 dt.Load(rdr);
                 rdr.Dispose();
                 conn.Close();
                 cmdinf.Dispose();

             }
             catch(Exception)
             {
                 throw;
             }
             return dt;
         }
         public DataTable GetRequestListByStatus(int status)
         {
             SqlDataReader rdr = null;
             DataTable dt = new DataTable();
             conn.Open();
             SqlCommand cmd = new SqlCommand("Email.SP_Email_GetEmailByStatus", conn);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.Add("@status", SqlDbType.Int);
             cmd.Parameters["@status"].Value = status;
             rdr = cmd.ExecuteReader();
             dt.Load(rdr);
             rdr.Dispose();
             cmd.Connection.Close();
             cmd.Dispose();
             conn.Close();
             return dt;
         }
         public DataTable GiveStudent_byStcode1(string stcode)
         {
             SqlDataReader rdr = null;
             DataTable dt = new DataTable();
             conn.Open();
             SqlCommand cmd = new SqlCommand("Email.SP_EMAIL_REG", conn);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.Add("@STCODE", SqlDbType.VarChar);
             cmd.Parameters["@STCODE"].Value = stcode;
             rdr = cmd.ExecuteReader();
             dt.Load(rdr);
             rdr.Dispose();
             cmd.Connection.Close();
             cmd.Dispose();
             conn.Close();
             return dt;
         }
         public int GetConnectTypeByStcode(string stcode)
         {
             int ConnectType = 0;
             SqlDataReader rdr = null;
             DataTable dt = new DataTable();
             conn.Open();
             SqlCommand cmdConnect = new SqlCommand("Email.SP_Email_GetConnectTextByStcode", conn);
             cmdConnect.CommandType = CommandType.StoredProcedure;
             cmdConnect.Parameters.Add("@stcode", SqlDbType.VarChar);
             cmdConnect.Parameters["@stcode"].Value = stcode;
             rdr = cmdConnect.ExecuteReader();
             dt.Load(rdr);
             ConnectType = int.Parse(dt.Rows[0]["ConnectType"].ToString());
             rdr.Dispose();
             cmdConnect.Connection.Close();
             cmdConnect.Dispose();
             conn.Close();
             return ConnectType;
         }
         public DataTable GetStudentInfoFromAmozesh(string stcode)
         {
             SqlDataReader rdr = null;
             DataTable dt = new DataTable();
             conn.Open();
             SqlCommand cmd = new SqlCommand("Email.SP_EMAIL_RegComplete", conn);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.Add("@STCODE", SqlDbType.VarChar);
             cmd.Parameters["@STCODE"].Value = stcode;
             rdr = cmd.ExecuteReader();
             dt.Load(rdr);
             rdr.Dispose();
             cmd.Connection.Close();
             cmd.Dispose();
             conn.Close();
             return dt;
         }
         public DataTable Email_Reg_Byid(int RequestID)
         {
             SqlDataReader rdr = null;
             DataTable dt = new DataTable();
             conn.Open();
             SqlCommand cmd = new SqlCommand("Email.SP_Email_Reg_Byid", conn);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.Add("@id", SqlDbType.Int);
             cmd.Parameters["@id"].Value = RequestID;
             rdr = cmd.ExecuteReader();
             dt.Load(rdr);
             rdr.Dispose();
             cmd.Connection.Close();
             cmd.Dispose();
             conn.Close();
             return dt;
         }

         #endregion

         #region Update
         //Create By Bahrami
         public void UpdateEmailPass(string stcode,string password)
         {
             SqlCommand cmdup=new SqlCommand();
             cmdup.Connection=conn;
             cmdup.CommandText="Email.SP_UpdateEmailPass";
             cmdup.CommandType=CommandType.StoredProcedure;
             cmdup.Parameters.AddWithValue("@stcode",stcode);
             cmdup.Parameters.AddWithValue("@Password",password);
             try 
             {
                 conn.Open();
                 cmdup.ExecuteNonQuery();
                 cmdup.Dispose();
                 conn.Close();
             }
             catch(Exception)
             {
                 throw;
             }
 
         }


         //Create BY Bahrami
         public void UpdateSecurityCode(string stcode,string securitycode)
         {
             SqlCommand cmdSec = new SqlCommand();
             cmdSec.Connection = conn;
             cmdSec.CommandText = "Email.SP_UpdateSecurityCode";
             cmdSec.CommandType = CommandType.StoredProcedure;
             cmdSec.Parameters.AddWithValue("@SecurityCode",securitycode);
             cmdSec.Parameters.AddWithValue("@stcode",stcode);

             try
             {
                 conn.Open();
                 cmdSec.ExecuteReader();
                 cmdSec.Dispose();
                 conn.Close();
             }
                 catch(Exception)
             { throw; }
         }



         public void UpdateSecondEmail_fsf2(string stcode, string Email)
         {
             conn.Open();
             DataTable dt = new DataTable();
             SqlCommand cmd = new SqlCommand("Email.SP_Email_UpdateEmailfsf2", conn);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.Add("@STCODE", SqlDbType.VarChar);
             cmd.Parameters["@STCODE"].Value = stcode;
             cmd.Parameters.Add("@CEmail", SqlDbType.VarChar);
             cmd.Parameters["@CEmail"].Value = Email;
             cmd.ExecuteNonQuery();
             cmd.Connection.Close();
             cmd.Dispose();
             conn.Close();
         }      
         public void Update_Request(string RequestId, string Description, int status)
         {
             conn.Open();
             DataTable dt = new DataTable();
             SqlCommand cmd = new SqlCommand("Email.SP_Email_UpdateRequestStatus", conn);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.AddWithValue("@id", RequestId);
             cmd.Parameters.AddWithValue("@status", status);
             cmd.Parameters.AddWithValue("@DESCRIPTION", Description);
             cmd.ExecuteNonQuery();
             cmd.Connection.Close();
             cmd.Dispose();
             conn.Close();
         }
       
         #endregion
    }
}
