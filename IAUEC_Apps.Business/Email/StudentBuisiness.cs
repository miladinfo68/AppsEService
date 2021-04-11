using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.Email;
using IAUEC_Apps.DTO.EmailClasses;
using IAUEC_Apps.Business.Email;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;
using IAUEC_Apps.DTO.CommonClasses;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.Business.Email
{
   public class StudentBuisiness
    {
        StudentDAO st = new StudentDAO();
        ActiveDirectoryBusiness ActiveDirectoryBusiness = new ActiveDirectoryBusiness();
        public DataTable GetStSecurityCode(string stcode)
        {
            return st.GetStSecurityCode(stcode);
        }
        public string GetMobileByStcode(string stcode)
        {
            return st.GetMobileByStcode(stcode);
        }
        public DataTable GetMobileByRequestID(int RequestID)
        {
            return st.GetMobileByRequestID(RequestID);
        }
        public string GetMobileByStcode1(string stcode)
        {
            return st.GetMobileByStcode1(stcode);
        }
        public DataTable GetstFromStudentSupInfo(string stcode)
        {
            return st.GetstFromStudentSupInfo(stcode);
        }

        public bool CheckUser(string user, string pass)
        {
            return st.CheckUser(user,pass);
        }
        public void Update_UserEmailFsf2(string emailaddress)
        {
            st.Update_UserEmailFsf2(emailaddress);
        }

        public DataTable GetAllStHaveEmail(string stcode)
        {
            return st.GetAllStHaveEmail(stcode);
          
        }
        public DataTable GetEmailRequestStatus(string stcode)
        {
            return st.GetEmailRequestStatus(stcode);
        }
        public List<StudentDTO> Giveinfo(string stcode)
        {
            DataTable dt = new DataTable();
            dt = st.Giveinfo(stcode);
            StudentDTO stDTO = new StudentDTO();
            List<StudentDTO> stList = new List<StudentDTO>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                stDTO.stcode = dt.Rows[i]["stcode"].ToString();
                stDTO.family = dt.Rows[i]["family"].ToString();
                stDTO.idd = dt.Rows[i]["idd"].ToString();
                stDTO.idd_Meli = dt.Rows[i]["idd_Meli"].ToString();
                stDTO.magh = dt.Rows[i]["MAGH"].ToString();
                stDTO.namep = dt.Rows[i]["namep"].ToString();
                stDTO.name = dt.Rows[i]["name"].ToString();
                stDTO.nameresh = dt.Rows[i]["nameresh"].ToString();
                stList.Add(stDTO);
            }
            return stList;
        }
        public bool CreateUser_ActiveDirectory(string stcode,out string error)
        {
            try
            {
                ActiveDirectoryDTO ActiveDirectoryDTO = new ActiveDirectoryDTO();
                Email_ClassBusiness emb = new Email_ClassBusiness();

                DataTable DT = emb.GetStudentInfoFromAmozesh(stcode);
                string level;
                if (DT.Rows[0]["magh"].ToString() == "1")
                    level = "کارشناسی";
                else if (DT.Rows[0]["magh"].ToString() == "5")
                    level = "ارشد";
                else
                    level = "دکتری";

                ActiveDirectoryDTO.SamAccountName = DT.Rows[0]["Email_Address"].ToString();
                ActiveDirectoryDTO.Uid = DT.Rows[0]["Email_Address"].ToString();
                ActiveDirectoryDTO.GivenName = DT.Rows[0]["name"].ToString();
                ActiveDirectoryDTO.DisplayName = DT.Rows[0]["name"].ToString() + " " + DT.Rows[0]["family"].ToString();
                ActiveDirectoryDTO.Name = DT.Rows[0]["name"].ToString();
                ActiveDirectoryDTO.sn = DT.Rows[0]["family"].ToString();//+ DT.Rows[0]["family"].ToString();
                ActiveDirectoryDTO.Description = stcode;
                ActiveDirectoryDTO.Company = DT.Rows[0]["nameresh"].ToString();// +"-" + Field;           
                ActiveDirectoryDTO.Department = level;
                ActiveDirectoryDTO.Password = "t123@456";
                ActiveDirectoryDTO.FieldCode = int.Parse(DT.Rows[0]["fresh_ID"].ToString());
                return ActiveDirectoryBusiness.CreateUser(ActiveDirectoryDTO, out error);

            }
            catch(Exception ex)
            {
                error="error112:"+ex.Message;
                return false;
            }



            //Email_ClassBusiness emb = new Email_ClassBusiness();
            //DataTable DT =emb.GiveStudent_Active(stcode);
            //string Field;
            //if (DT.Rows[0]["magh"].ToString() == "1")
            //    Field = "کارشناسی";
            //else
            //    Field = "ارشد";
            //string ldapPath = "192.168.1.2/OU=rdv,OU=iauec,DC=iauec,DC=LOCAL";
            //string oGUID = string.Empty;

            //try
            //{
            //    string connectionPrefix = "LDAP://" + ldapPath;
            //    DirectoryEntry dirEntry = new DirectoryEntry(connectionPrefix);
            //    dirEntry.Username = "administrator";
            //    dirEntry.Password = "";
            //    oGUID = dirEntry.Guid.ToString(); // وقتی بخواهیم چک کنیم که آیا به درستی وصل شده به سرور

            //    DirectoryEntry newUser = dirEntry.Children.Add("CN=" + DT.Rows[0]["Email_Address"].ToString(), "user");
            //    newUser.Properties["samAccountName"].Value = DT.Rows[0]["Email_Address"].ToString();
            //    newUser.Properties["uid"].Value = DT.Rows[0]["Email_Address"].ToString();
            //    newUser.Properties["givenName"].Value = DT.Rows[0]["name"].ToString();
            //    newUser.Properties["description"].Value = stcode;
            //    newUser.Properties["company"].Value = DT.Rows[0]["nameresh"].ToString() + "-" + Field;
            //    newUser.Properties["displayName"].Value = DT.Rows[0]["family"].ToString(); //Farsi               
            //    newUser.Properties["name"].Value = DT.Rows[0]["family"].ToString() + DT.Rows[0]["family"].ToString();
            //    newUser.CommitChanges();
            //    oGUID = newUser.Guid.ToString();
            //    //newUser.Invoke("SetPassword", new object[] { DT.Rows[0]["Password"].ToString() });
            //    newUser.Invoke("SetPassword", new object[] { "t123@456" });
            //    newUser.CommitChanges();
            //    newUser.Properties["userAccountControl"].Value = 0x200;
            //    newUser.CommitChanges();
            //    dirEntry.Close();
            //    newUser.Close();
            //    return true;
            //}
            //catch (System.DirectoryServices.DirectoryServicesCOMException E)
            //{
            //    oGUID = E.Message.ToString();
            //    return false;
            //}
        }
        public void InsertIntoStudentSupInfo(string stcode, string name, string family)
        {
            st.InsertIntoStudentSupInfo(stcode, name, family);
        }
        public void Update_Mobile(string stcode, string Mobile)
        {
            st.Update_Mobile(stcode, Mobile);
        }
    }
}
