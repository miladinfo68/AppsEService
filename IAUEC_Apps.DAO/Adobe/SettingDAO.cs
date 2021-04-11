using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAC.Connections;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Security.Cryptography;

namespace IAUEC_Apps.DAO.Adobe
{
   public class SettingDAO
    {
       SqlConnection supp = new SqlConnection(new SuppConnection().Supp_con);
        #region Read
       public DataTable GetSettingByTerm(string term)
       {
      
           SqlConnection conn = supp;
           SqlDataReader rdr = null;
           DataTable dt = new DataTable();
           conn.Open();
           SqlCommand cmd = new SqlCommand("Adobe.SP_GetSettingByTerm", conn);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@term", SqlDbType.NVarChar);
           cmd.Parameters["@term"].Value = term;
           rdr = cmd.ExecuteReader();
           dt.Load(rdr);
           rdr.Dispose();
           cmd.Connection.Close();
           cmd.Dispose();
           conn.Close();
           return dt;
       }

        public string getAdobeConnectionString(string term)
        {
            string connString = "";
            AdobeMasterDAO adao = new AdobeMasterDAO();
            DataTable dtSetting = GetSettingByTerm(term);
            if (dtSetting.Rows.Count > 0)
                connString = dtSetting.Rows[0]["DB_Con"] == DBNull.Value ? "" : dtSetting.Rows[0]["DB_Con"].ToString();
            connString = Decrypt(connString, true);
            return connString;
        }

        #endregion


        public string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            //Get your key from config file to open the lock!
            var key = ConfigurationManager.AppSettings["SecurityKey"];

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }





    }
}
