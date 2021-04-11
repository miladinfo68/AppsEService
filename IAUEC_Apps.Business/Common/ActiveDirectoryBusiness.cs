using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using MyConnections;
using System.DirectoryServices;
using System.Data;
using IAUEC_Apps.DTO.CommonClasses;
using System.DirectoryServices.AccountManagement;
using IAUEC_Apps.Business.Email;


namespace IAUEC_Apps.Business.Common
{

    /// <summary>
    /// this Class 
    /// All Act in Active Directory be in here
    /// 
    /// </summary>
    /// 

    public class ActiveDirectoryBusiness
    {
        MyConnections.ActiveConnection ac = new ActiveConnection();

        public bool ResetPassword(string UserName, string Password)
        {                
            try
            {
                
                DirectoryEntry dirEntry = ConnectToActive();
                DirectorySearcher deSearch = new DirectorySearcher();

                deSearch.SearchRoot = dirEntry;

               // deSearch.Filter = string.Format("(&(objectCategory=person)(anr={0}))", UserName.Trim());
                deSearch.Filter = string.Format("(&(objectCategory=person)(cn={0}))", UserName.Trim());
                // "(&(objectClass=user) (cn=a_rohani))";
                
                SearchResult result = deSearch.FindOne();

                if (result != null)
                {
                   
                    DirectoryEntry deUser = ConnectToActiveByPath(result.Path);
                    // DirectoryEntry deUser = new DirectoryEntry(result.Path);

                    deUser.Invoke("SetPassword", new object[] { Password });

                    deUser.Properties["LockOutTime"].Value = 0; //unlock account

                    deUser.CommitChanges();
                    dirEntry.Close();
                    deUser.Close();
                    return true;

                }
                else
                {
                    return false;
                }
               
            }
            catch (System.DirectoryServices.DirectoryServicesCOMException E)
            {
                return false;
            }
        }
        public bool ResetPassword(string UserName, string Password, string reqid)
        {
            Email_ClassBusiness emB = new Email_ClassBusiness();
            try
            {
                // In Future change Under line Code    
                DirectoryEntry dirEntry = ConnectToActive();
                DirectorySearcher deSearch = new DirectorySearcher();

                deSearch.SearchRoot = dirEntry;

                deSearch.Filter = string.Format("(&(objectCategory=person)(anr={0}))", UserName.Trim());
                // "(&(objectClass=user) (cn=a_rohani))";

                SearchResult result = deSearch.FindOne();
               
                if (result != null)
                {
                   // emB.Update_Request(reqid, "find", 5);
                    DirectoryEntry deUser = ConnectToActiveByPath(result.Path);
                  // DirectoryEntry deUser = new DirectoryEntry(result.Path);

                    deUser.Invoke("SetPassword", new object[] { Password });

                    deUser.Properties["LockOutTime"].Value = 0; //unlock account

                    deUser.CommitChanges();
                    dirEntry.Close();
                    deUser.Close();
                    return true;

                }
                else
                   // emB.Update_Request(reqid, "notfind", 5);
                    return false;

            }
            catch (System.DirectoryServices.DirectoryServicesCOMException E)
            {
               // emB.Update_Request(reqid, E.Message, 5);
                return false;
            }
        }
        public bool CreateUser(ActiveDirectoryDTO ActiveDirectoryDTO,out string error) 
        {                    
            string oGUID = string.Empty;
            error = "";
            try
            {
                DirectoryEntry dirEntry = ConnectToActiveForSearchStudent(ActiveDirectoryDTO.FieldCode);          
               // oGUID = dirEntry.Guid.ToString(); 

                DirectoryEntry newUser = dirEntry.Children.Add("CN=" + ActiveDirectoryDTO.SamAccountName, "user");
                newUser.Properties["samAccountName"].Value = ActiveDirectoryDTO.SamAccountName;
                newUser.Properties["userPrincipalName"].Value = ActiveDirectoryDTO.SamAccountName + "@iauec.local";
                newUser.Properties["uid"].Value = ActiveDirectoryDTO.Uid;
                newUser.Properties["givenName"].Value = ActiveDirectoryDTO.Name.Trim();
                newUser.Properties["displayName"].Value = CreateDisplayName(ActiveDirectoryDTO);
                newUser.Properties["name"].Value = ActiveDirectoryDTO.Name.Trim();
                newUser.Properties["sn"].Value = ActiveDirectoryDTO.sn.Trim();
                newUser.Properties["description"].Value = ActiveDirectoryDTO.Description.Trim();
                newUser.Properties["company"].Value = ActiveDirectoryDTO.Company.Trim();
                newUser.Properties["department"].Value = ActiveDirectoryDTO.Department.Trim();
                error += "136,";
                newUser.CommitChanges();
                error += "138,";
                oGUID = newUser.Guid.ToString();
                error += "140,";
                newUser.Invoke("SetPassword", new object[] { "t123@456" });
                error += "142,";
                newUser.CommitChanges();
                error += "144,";
                newUser.Properties["userAccountControl"].Value = 0x200;
                error += "146,";
                newUser.CommitChanges();
                error += "148,";
                dirEntry.Close();
                newUser.Close();
                return true;
            }
            catch (System.DirectoryServices.DirectoryServicesCOMException E)
            {
                oGUID = E.Message.ToString();
                error += "156,"+E.Message;
                return false;
            }
            catch(Exception ex)
            {

                error +="162,"+ ex.Message;
                return false;
            }
        }




        public bool Get_FindUser_SamAccountName(string UserName)
        {
            DirectoryEntry dirEntry=ConnectToActive();        

            try
            {

                DirectorySearcher deSearch = new DirectorySearcher();

                deSearch.SearchRoot = dirEntry;

                deSearch.Filter = string.Format("(&(objectCategory=person)(anr={0}))", UserName.Trim());
                // "(&(objectClass=user) (cn=a_rohani))";

                SearchResult result = deSearch.FindOne();

                if (result != null)
                {
                    DirectoryEntry deUser = new DirectoryEntry(result.Path);
                    
                    deUser.Close();
                    return true;
                }
                else
                    return false;


            }
            catch (System.DirectoryServices.DirectoryServicesCOMException E)
            {
              
                return false;
            }
        }
        public int Get_FindUser_DisplayNameByFieldCode(ActiveDirectoryDTO ActiveDirectoryDTO)
        {
            DirectoryEntry dirEntry = ConnectToActiveForSearchStudent(ActiveDirectoryDTO.FieldCode);

            try
            {

                DirectorySearcher deSearch = new DirectorySearcher();

                deSearch.SearchRoot = dirEntry;

                deSearch.Filter = string.Format("(&(objectCategory=person)(anr={0}))", ActiveDirectoryDTO.DisplayName);
                // "(&(objectClass=user) (cn=a_rohani))";

                int result = deSearch.FindAll().Count;

                if (result != null)
                {
                    //DirectoryEntry deUser = new DirectoryEntry(result.Path);

                    //deUser.Close();
                    return result;
                }
                else
                    return 0;


            }
            catch (System.DirectoryServices.DirectoryServicesCOMException E)
            {

                return -1;
            }
        }


       

        public string CreateDisplayName(ActiveDirectoryDTO ActiveDirectoryDTO)
        {
            string fname = ActiveDirectoryDTO.Name;

            for (int i = 0; i <= Get_FindUser_DisplayNameByFieldCode(ActiveDirectoryDTO);i++ )
            {
                fname = fname + " ";
                
            }
            ActiveDirectoryDTO.DisplayName = fname + " " + ActiveDirectoryDTO.sn;
            return ActiveDirectoryDTO.DisplayName;
         
        
        }

        public DirectoryEntry ConnectToActive()
        {
            return ac.ConnectToActive();
          
            
        }
        public DirectoryEntry ConnectToActiveStudent()
        {
            return ac.ConnectToActiveStudent();
            

        }
        public DirectoryEntry ConnectToActiveForSearchStudent(int fieldCode)
        {
            return ac.ConnectToActiveForSearchStudent(fieldCode);
         
           

        }
        public DirectoryEntry ConnectToActiveByPath(string path)
        {
            return ac.ConnectToActiveByPath(path);
           
         

        }


    }
}
