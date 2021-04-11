using IAUEC_Apps.DAC.Connections;
using IAUEC_Apps.DTO.AdobeClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace IAUEC_Apps.DAO.Adobe
{
    public class AdobeMasterDAO
    {
        #region LoginToAdobe

        private AdobeConnectDTO Login(AdobeConnectDTO AdobeConnectDto)
        {
            AdobeConnectDTO ACDTO = new DTO.AdobeClasses.AdobeConnectDTO();
            ACDTO.DomainAddress = "kadobe.iauec.ac.ir";
            ACDTO.DomainLogin = "h@razavi.com";
            ACDTO.DomainPassword = "P@ssw0rd";

            ACDTO.FolderName = AdobeConnectDto.FolderName;
            ACDTO.FolderFolderId = AdobeConnectDto.FolderFolderId;
            ACDTO.FolderUrlPath = "Apps" + AdobeConnectDto.FolderUrlPath;
            ACDTO.FolderDataBegin = new DateTime(2015, 01, 01);
            ACDTO.FolderDataEnd = new DateTime(2016, 12, 28);
            
            return ACDTO;
        }


        



        #endregion



        //Insert
        #region Create

        /// <summary>
        /// Create Folder:
        /// Here, Because used Handy URL, Need To Combination Chars & Numbers to Able 
        /// To Create   
        /// </summary>
        /// <param name="Cookie"></param>
        /// <returns></returns>
        public bool CreateFolder(AdobeConnectDTO ACDTO)
        {
            ACDTO = Login(ACDTO);          
            
            bool Resualt = false;
            string serviceUrl = "http://" + ACDTO.DomainAddress + "/api/xml?action=sco-update&type=folder"
                + "&name=" + ACDTO.FolderName
                + "&folder-id=" + ACDTO.FolderFolderId
                + "&date-begin=" + ACDTO.FolderDataBegin.Year.ToString()
                    + "-" + ACDTO.FolderDataBegin.Month.ToString()
                    + "-" + ACDTO.FolderDataBegin.Day.ToString()
                + "&date-end=" + ACDTO.FolderDataEnd.Year.ToString()
                    + "-" + ACDTO.FolderDataEnd.Month.ToString()
                    + "-" + ACDTO.FolderDataEnd.Day.ToString()
                + "&url-path=" + ACDTO.FolderUrlPath
                + "&session=" + ACDTO.DomainCookies.Value;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);
            request.CookieContainer = new CookieContainer();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            XmlReader reader = XmlReader.Create(response.GetResponseStream());
            XmlNodeType z;

            while (reader.Read())
            {
                reader.MoveToFirstAttribute();
                z = reader.NodeType;
                if (reader.Value == "ok")
                    Resualt = true;
            }

            return Resualt;
        }













        #endregion


        public void AddOrUpdateConnectionString(string term, string conString = null, string ConName = null, string DomainName = null, string hpass = null, string vpass = null, string hName = null, string vName = null, string aPass = null)
        {
            SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Adobe.SP_AddOrUpdateSetting";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Term", term);
            cmd.Parameters.AddWithValue("@DomainName", DomainName);
            cmd.Parameters.AddWithValue("@ConName", ConName);
            cmd.Parameters.AddWithValue("@hpass", hpass);
            cmd.Parameters.AddWithValue("@vpass", vpass);
            cmd.Parameters.AddWithValue("@DB_Con", conString);
            cmd.Parameters.AddWithValue("@hName", hName);
            cmd.Parameters.AddWithValue("@vName", vName);
            cmd.Parameters.AddWithValue("@aPass", aPass);
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





    }
}
