using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IAUEC_Apps.Business.Common;
using System.Data;
using IAUEC_Apps.Business.Email;
using IAUEC_Apps.DTO.EmailClasses;
using IAUEC_Apps.DAO.CommonDAO;

namespace IAUEC_Apps.Business.Common
{
   public class UserAccessBusiness
    {
       AppsUserAccess AppsUserAccessDAO = new AppsUserAccess();
       
       public string ReturnTimeToken(DateTime datetime )
       {
           string code;
      
           byte[] b_dt = Encoding.UTF8.GetBytes(datetime.AddDays(1).ToString());
           code = Convert.ToBase64String(b_dt);
           return code;
       }

       public bool IsTimeTokenValid(string token)
       {

           byte[] b_dt = Convert.FromBase64String(token);
           string s_dt = Encoding.UTF8.GetString(b_dt);
            DateTime dt_Valid = Convert.ToDateTime(s_dt);

         if (DateTime.Compare(DateTime.Now, dt_Valid) <= 0)
         {
             return true;
         }
         else
         {
             return false;
         }
       }
        public string EncryptPass(String PasswordPlainText)
        {
            string plainText = PasswordPlainText.Trim();
            string cipherText = "";                 // encrypted text
            string passPhrase = "Pas5pr@se";        // can be any string
            string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
       
            EncryptionClass ThisKey =
            new EncryptionClass(passPhrase, initVector);
            cipherText = ThisKey.Encrypt(plainText);
            return cipherText;

        }
        public string DecryptPass(String CipherText)
        {
            string plainText = "";
            string cipherText = CipherText;                 // encrypted text
            string passPhrase = "Pas5pr@se";        // can be any string
            string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes

            EncryptionClass ThisKey =
            new EncryptionClass(passPhrase, initVector);
            plainText = ThisKey.Decrypt(cipherText);

            return plainText;

        }

        public DataTable Get_AllAppIdByUserId(int UserId)
        {
            return AppsUserAccessDAO.Get_AllAppIdByUserId(UserId);
        }
        public DataTable Get_MenuPermissionByMenuId(int menuId)
        {
            return AppsUserAccessDAO.Get_MenuPermissionByMenuId(menuId);
        }
        public int GetDaneshIDByRoleID(int RoleId)
        {
            UserAccessSection uc = new UserAccessSection();
            return uc.GetDaneshIDByRoleID( RoleId);

        }
    }
}
