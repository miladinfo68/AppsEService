using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DAO.Email;
using IAUEC_Apps.DTO.EmailClasses;
using System.Net.Mail;
using System.Net;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.Email;

namespace IAUEC_Apps.Business.Email
{
   public class Email_ClassBusiness
    {
        
         Email_ClassDAO emDAO = new Email_ClassDAO();
         ActiveDirectoryBusiness ADB = new ActiveDirectoryBusiness();

         public void Create_Email(Email_Class Email_Class)
         {
             emDAO.Create_Email(Email_Class);
         }
        public List<Email_Class> CheckEmailStudent_ByStcode(string stcode)
        {
         
            Email_Class em=new Email_Class();
           
            DataTable dt = new DataTable();
            dt = emDAO.CheckEmailStudent_ByStcode(stcode);
            List<Email_Class> emList = new List<Email_Class>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                em.CEMAIL = dt.Rows[i]["CEmail"].ToString();
                em.ConnectType = int.Parse(dt.Rows[i]["ConnectType"].ToString());
                em.Date_Save = DateTime.Parse(dt.Rows[i]["Date_Save"].ToString());
                em.Email_Address = dt.Rows[i]["Email_Address"].ToString();
                em.Stcode = dt.Rows[i]["Stcode"].ToString();
                em.UpdateEmail = bool.Parse(dt.Rows[i]["UpdateEmail"].ToString());
                em.id = int.Parse(dt.Rows[i]["Id"].ToString());
                emList.Add(em);
            }
            return emList;
        }
        public bool CheckEmailName(string Email)
        {
            return emDAO.CheckEmailName(Email);
        }
        public List<Email_Class> GiveList_Status_Zero()
        {
           
            DataTable dt = new DataTable();
            dt = emDAO.GiveList_Status_Zero();
            Email_Class em = new Email_Class();
            List<Email_Class> emList = new List<Email_Class>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
                em.Date_Save = DateTime.Parse(dt.Rows[i]["Date_Save"].ToString());
                em.Email_Address = dt.Rows[i]["Email_Address"].ToString();
                em.Stcode = dt.Rows[i]["Stcode"].ToString();
              
                emList.Add(em);
            }
            return emList;
        }
        public DataTable GetAllStudentEmailInfo(int status)
        {
            return emDAO.GetAllStudentEmailInfo(status);
        }
        public DataTable GiveList_Status(int status)
        {
          
            return emDAO.GetRequestListByStatus(status);
        }
        public List<Email_Class> GiveStudent_byStcode1(string stcode)
        {
            Email_Class em = new Email_Class();
            List<Email_Class> emList = new List<Email_Class>();
           
            DataTable dt = new DataTable();
            dt = emDAO.GiveStudent_byStcode1(stcode);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                em.CEMAIL = dt.Rows[i]["CEmail"].ToString();
                em.ConnectType = int.Parse(dt.Rows[i]["ConnectType"].ToString());
                em.Date_Save = DateTime.Parse(dt.Rows[i]["Date_Save"].ToString());
                em.Email_Address = dt.Rows[i]["Email_Address"].ToString();
                em.Stcode = dt.Rows[i]["Stcode"].ToString();
                em.UpdateEmail = bool.Parse(dt.Rows[i]["UpdateEmail"].ToString());
                emList.Add(em);
            }
            return emList;
        }
        public int GetConnectTypeByStcode(string stcode)
        {
         
            return emDAO.GetConnectTypeByStcode(stcode);
        }
        public DataTable GetStudentInfoFromAmozesh(string stcode)
        {
            return emDAO.GetStudentInfoFromAmozesh(stcode);
        }
        //public bool SendEmail(string stcode, string MailText)
        //{
        //    Email_ClassBusiness emb = new Email_ClassBusiness();
        //    List<Email_Class> emLST = new List<Email_Class>();
        //    emLST = emb.GiveStudent_byStcode1(stcode);
        //    string to = emLST[0].CEMAIL;
        //    //string Name = dt.Rows[0]["name"].ToString();
        //    string Username = "noreply";
        //    string Password = "A_rohani";
        //    string SmtpServer = "mail.iauec.ac.ir";
        //    string From = Username + "@iauec.local";
        //    SmtpClient s = new SmtpClient(SmtpServer);
        //    s.UseDefaultCredentials = false;
        //    s.Credentials = new NetworkCredential(From, Password);
        //    s.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    MailMessage m = new MailMessage(From, to, "سیستم ثبت رایان نامه واحد الکترونیکی", MailText);
        //    m.IsBodyHtml = true;
        //    s.Send(m);
        //    return true;
        //}
        public void UpdateSecondEmail_fsf2(string stcode, string Email)
        {
          
            emDAO.UpdateSecondEmail_fsf2(stcode, Email);
        }
        public void Update_Request(string reqId, string Description, int status)
        {
            emDAO.Update_Request(reqId, Description, status);
        }
        public Email_Class Email_Reg_Byid(int RequestID)
        {
            DataTable dt=new DataTable();
            Email_Class em = new Email_Class();
          
            dt= emDAO.Email_Reg_Byid(RequestID);
          
                em.Stcode = dt.Rows[0]["Stcode"].ToString();
                em.CEMAIL = dt.Rows[0]["CEmail"].ToString();
                em.ConnectType = int.Parse(dt.Rows[0]["ConnectType"].ToString());
                em.Date_Save =Convert.ToDateTime(dt.Rows[0]["Date_Save"].ToString());
                em.Email_Address = dt.Rows[0]["Email_Address"].ToString();
                em.Mobile = dt.Rows[0]["s_mobile"].ToString();
                em.UpdateEmail =bool.Parse(dt.Rows[0]["UpdateEmail"].ToString());
          
            return em;
        }

        /// <summary>
        /// زمانیکه ایمیل ساخته شد ، در هنگام تایید ، نیاز است کمله عبور دانشجو،
        /// به کلمه اصلی تغییر کند
        /// </summary>
        /// <returns></returns>
        public bool ChangePassEmail_AfterCreateEmail(string stcode,string reqid)
        {
          
            // add Reset pass
            DataTable DT = GetStudentInfoFromAmozesh(stcode);
            Email_ClassBusiness emb = new Email_ClassBusiness();
           
            string UserName = DT.Rows[0]["Email_Address"].ToString();
            string Password = DT.Rows[0]["Password"].ToString();
            // وقتی پسورد را می خواهیم از دیتابیس بخوانیم ، نیاز به دکد کردن داریم
            //EncryptionClass ThisKey = new EncryptionClass(Password);
            Password = CommonBusiness.DecryptPass(Password);
            return ADB.ResetPassword(UserName, Password, reqid);
       
        }


        public string GetPasswordByStcode(string stcode)
        {

            // add Reset pass
            DataTable DT = GetStudentInfoFromAmozesh(stcode);
            string UserName = DT.Rows[0]["Email_Address"].ToString();
            string Password = DT.Rows[0]["Password"].ToString();
            // وقتی پسورد را می خواهیم از دیتابیس بخوانیم ، نیاز به دکد کردن داریم
            //EncryptionClass ThisKey = new EncryptionClass(Password);
            Password = CommonBusiness.DecryptPass(Password);
            return Password;         

        }

        public void UpdateSecurityCode(string stcode, string securitycode)
        {
            emDAO.UpdateSecurityCode(stcode, securitycode);
        }

        public void UpdateEmailPass(string stcode, string password)
        {
            emDAO.UpdateEmailPass(stcode, password);
        }
 


    }
}
