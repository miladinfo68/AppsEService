using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System;

namespace IAUEC_Apps.UI.University.LMS
{
    public partial class LmsMain : System.Web.UI.Page
    {



        public string EncryptString(string plainText, byte[] key, byte[] iv)
        {
            // Instantiate a new Aes object to perform string symmetric encryption
            Aes encryptor = Aes.Create();

            encryptor.Mode = CipherMode.CBC;

            // Set key and IV
            byte[] aesKey = new byte[32];
            Array.Copy(key, 0, aesKey, 0, 32);
            encryptor.Key = aesKey;
            encryptor.IV = iv;

            // Instantiate a new MemoryStream object to contain the encrypted bytes
            MemoryStream memoryStream = new MemoryStream();

            // Instantiate a new encryptor from our Aes object
            ICryptoTransform aesEncryptor = encryptor.CreateEncryptor();

            // Instantiate a new CryptoStream object to process the data and write it to the 
            // memory stream
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write);

            // Convert the plainText string into a byte array
            byte[] plainBytes = Encoding.ASCII.GetBytes(plainText);

            // Encrypt the input plaintext string
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);

            // Complete the encryption process
            cryptoStream.FlushFinalBlock();

            // Convert the encrypted data from a MemoryStream to a byte array
            byte[] cipherBytes = memoryStream.ToArray();

            // Close both the MemoryStream and the CryptoStream
            memoryStream.Close();
            cryptoStream.Close();

            // Convert the encrypted byte array to a base64 encoded string
            string cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);

            // Return the encrypted data as a string
            return cipherText;
        }

        public string DecryptString(string cipherText, byte[] key, byte[] iv)
        {
            // Instantiate a new Aes object to perform string symmetric encryption
            Aes encryptor = Aes.Create();

            encryptor.Mode = CipherMode.CBC;

            // Set key and IV
            byte[] aesKey = new byte[32];
            Array.Copy(key, 0, aesKey, 0, 32);
            encryptor.Key = aesKey;
            encryptor.IV = iv;

            // Instantiate a new MemoryStream object to contain the encrypted bytes
            MemoryStream memoryStream = new MemoryStream();

            // Instantiate a new encryptor from our Aes object
            ICryptoTransform aesDecryptor = encryptor.CreateDecryptor();

            // Instantiate a new CryptoStream object to process the data and write it to the 
            // memory stream
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Write);

            // Will contain decrypted plaintext
            string plainText = String.Empty;

            try
            {
                // Convert the ciphertext string into a byte array
                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                // Decrypt the input ciphertext string
                cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);

                // Complete the decryption process
                cryptoStream.FlushFinalBlock();

                // Convert the decrypted data from a MemoryStream to a byte array
                byte[] plainBytes = memoryStream.ToArray();

                // Convert the decrypted byte array to string
                plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
            }
            finally
            {
                // Close both the MemoryStream and the CryptoStream
                memoryStream.Close();
                cryptoStream.Close();
            }

            // Return the decrypted data as a string
            return plainText;
        }













        CommonBusiness cmbusiness = new CommonBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {
            form1.Action = System.Configuration.ConfigurationManager.AppSettings["LMS_link"].ToString();
            if (Session["LogStatus"].ToString() == "0-0")
            {
                LogStatus.Value = Session["LogStatus"].ToString();
                ScriptManager.RegisterStartupScript(this, GetType(), "submitform", "submitform();", true);
            }

            else
            {
                LogStatus.Value = Session["LogStatus"].ToString();
                 string key = "b3afc5fd20e3637160da4f9cab6c8072";
                 string IV = "a214ee38a470c5974c10498b7152ca39";

               // string key = "b2afc5fd22e3637260da4f9cab6c8272";
              //  string IV = "a224ee38a470c5974c20498b7252ca39";
                string userName ="", pass="";
                //یک کارمندو مدیرگروه 2- استاد 3- دانشجو 4- ممتحن 
                switch (Session["UserType_lms"].ToString())
                {
                   case "1":
                        if (Session[sessionNames.roleID].ToString() == "33")
                        {
                            userName = "emtehanat1";
                            pass = Session["p"].ToString();
                        }
                        else if (Session[sessionNames.roleID].ToString() == "32")
                        {
                            userName = "adminsupport1";
                            pass = Session["p"].ToString();
                        }
                        else
                        {
                            userName = Session[sessionNames.user_Karbar].ToString();
                            pass = Session["p"].ToString();
                        }
                         
                        break;
                    case "2":
                        userName = "ins" + Session[sessionNames.userID_StudentOstad].ToString();
                        pass = Session["Password"].ToString();
                        break;
                    case "3":
                        userName =  Session[sessionNames.userID_StudentOstad].ToString();
                        pass = Session["Password"].ToString();
                        break;
                    case "4":
                        userName = "ins" + Session[sessionNames.user_Karbar].ToString();
                        pass = Session["p"].ToString();
                        break;
                }


               // var ddd = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
               // string message = "970225966;1990079997;"+ddd;

               // //string message = "99900999;0012244511;2020-05-12 15:20:01";
               // string password = "3sc3RLrpd17";

               // // Create sha256 hash
               // SHA256 mySHA256 = SHA256Managed.Create();
               // byte[] key123 = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(password));
               // string author = "zxcvbnmasdfghjkl";
               // // Convert a C# string to a byte array  
               // //byte[] bytes = Encoding.ASCII.GetBytes(author);

               // // Create secret IV
               // byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
               //// iv = bytes;
               // string encrypted = this.EncryptString(message, key123, iv);

               // string decrypted = this.DecryptString(encrypted, key123, iv);

               // var u = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
               // var d = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
               // Console.WriteLine(encrypted);
               // Console.WriteLine(decrypted);



                UserName.Value = EncryptionClass.EncryptRJ(userName, key, IV);
                Password.Value = EncryptionClass.EncryptRJ(pass, key, IV);
                ScriptManager.RegisterStartupScript(this, GetType(), "submitform", "submitform();", true);
            }
            

        }
    }
}