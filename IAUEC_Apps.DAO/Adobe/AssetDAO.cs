using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.DAC.Connections;
using IAUEC_Apps.DTO.AdobeClasses;

namespace IAUEC_Apps.DAO.CommonDAO
{
   public class AssetDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
       
        //SqlConnection vc_newconn = new SqlConnection(new AdobeConnection().vc_971);
        #region Read
        public List<AssetDTO> Show_Asset_List_ByClassCode(string classCode,string term)//oksargolChangeTypeClassCodeok
        {
            List<AssetDTO> assetList = new List<AssetDTO>();
           
            SqlCommand cmdAsset = new SqlCommand();
            cmdAsset.Connection = conn;
            cmdAsset.CommandType = CommandType.StoredProcedure;
            cmdAsset.CommandText = "Adobe.SP_Get_AssetList_ByClassCode";
            cmdAsset.Parameters.AddWithValue("@classCode", classCode);
            cmdAsset.Parameters.AddWithValue("@term", term);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;

            rdr = cmdAsset.ExecuteReader();
            dt.Load(rdr);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AssetDTO asset = new AssetDTO();
                asset.AssetID = int.Parse(dt.Rows[i]["AssetID"].ToString());
                asset.FileName = dt.Rows[i]["FileName"].ToString();
                asset.FileDate = dt.Rows[i]["FileDate"].ToString();
                asset.Class_Code = (dt.Rows[i]["Class_Code"].ToString());
                asset.Term = dt.Rows[i]["Term"].ToString();
                assetList.Add(asset);
            }
                conn.Close();
                return assetList;

        }
        public DataTable Show_All_assetList()
        {
            List<AssetDTO> assetList = new List<AssetDTO>();

            SqlCommand cmdAsset = new SqlCommand();
            cmdAsset.Connection = conn;
            cmdAsset.CommandType = CommandType.StoredProcedure;
            cmdAsset.CommandText = "Adobe.SP_GetAllAsset";
           
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;

            rdr = cmdAsset.ExecuteReader();
            dt.Load(rdr);
            return dt;
        }

        public DataTable GetAllAssetByTerm(string term, bool IsArchive)
        {
           

            SqlCommand cmdAssetByTerm = new SqlCommand();
            cmdAssetByTerm.Connection = conn;
            cmdAssetByTerm.CommandType = CommandType.StoredProcedure;
            cmdAssetByTerm.CommandText = "Adobe.SP_GetAllAssetByTerm";

            cmdAssetByTerm.Parameters.AddWithValue("@term", term);
            cmdAssetByTerm.Parameters.AddWithValue("@IsArchive", IsArchive);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;

            rdr = cmdAssetByTerm.ExecuteReader();
            dt.Load(rdr);
            conn.Close();
            return dt;
        }
        public DataTable GetAssetByTerm(string term, bool IsArchive)
        {


            SqlCommand cmdAssetByTerm = new SqlCommand();
            cmdAssetByTerm.Connection = conn;
            cmdAssetByTerm.CommandType = CommandType.StoredProcedure;
            cmdAssetByTerm.CommandText = "Adobe.SP_Get_AssetByTerm";

            cmdAssetByTerm.Parameters.AddWithValue("@term", term);
            cmdAssetByTerm.Parameters.AddWithValue("@IsArchive", IsArchive);
    
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;

            rdr = cmdAssetByTerm.ExecuteReader();
            dt.Load(rdr);
            conn.Close();
            return dt;
        }
        public DataTable GetAssetBySize(string term, bool IsArchive, int FileSize)
        {


            SqlCommand cmdAssetByTerm = new SqlCommand();
            cmdAssetByTerm.Connection = conn;
            cmdAssetByTerm.CommandType = CommandType.StoredProcedure;
            cmdAssetByTerm.CommandText = "Adobe.SP_Get_AssetBySize";

            cmdAssetByTerm.Parameters.AddWithValue("@term", term);
            cmdAssetByTerm.Parameters.AddWithValue("@IsArchive", IsArchive);
            cmdAssetByTerm.Parameters.AddWithValue("@FileSize", FileSize);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;

            rdr = cmdAssetByTerm.ExecuteReader();
            dt.Load(rdr);
            conn.Close();
            return dt;
        }
        public DataTable GetAllAssetBydid(string did, string filedate)
        {
            SqlCommand cmdAssetByTerm = new SqlCommand();

            SqlConnection vc_newconn = new SqlConnection();
            Adobe.SettingDAO setting = new Adobe.SettingDAO();
            cmdAssetByTerm.Connection = vc_newconn;
            string termJary = System.Configuration.ConfigurationManager.AppSettings["Term"];
            cmdAssetByTerm.Connection.ConnectionString = setting.getAdobeConnectionString(termJary);


           
          //cmdAssetByTerm.Connection = connNew;
            cmdAssetByTerm.CommandType = CommandType.StoredProcedure;
            cmdAssetByTerm.CommandText = "R_GetRecordOfMeeting";
            cmdAssetByTerm.Parameters.AddWithValue("@date", filedate);
            cmdAssetByTerm.Parameters.AddWithValue("@did", did);

            vc_newconn.Open();
            //connNew.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;

            rdr = cmdAssetByTerm.ExecuteReader();
            dt.Load(rdr);
            vc_newconn.Close();
           //connNew.Close();
            return dt;
        }
        public DataTable GetAllAssetByTime(string filedate)
        {
            SqlCommand cmdAssetByTerm = new SqlCommand();

            SqlConnection vc_newconn = new SqlConnection();
            Adobe.SettingDAO setting = new Adobe.SettingDAO();
            cmdAssetByTerm.Connection = vc_newconn;
            string termJary = System.Configuration.ConfigurationManager.AppSettings["Term"];
            cmdAssetByTerm.Connection.ConnectionString = setting.getAdobeConnectionString(termJary);


            
            //cmdAssetByTerm.Connection = connNew;
            cmdAssetByTerm.CommandType = CommandType.StoredProcedure;
            cmdAssetByTerm.CommandText = "R_GetRecordOfMeeting2";
            cmdAssetByTerm.Parameters.AddWithValue("@date", filedate);
           // cmdAssetByTerm.Parameters.AddWithValue("@did", did);

            vc_newconn.Open();
            //connNew.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;

            rdr = cmdAssetByTerm.ExecuteReader();
            dt.Load(rdr);
            vc_newconn.Close();
            //connNew.Close();
            return dt;
        }
        public DataTable GetAllAssetByTime(string filedate,string todate)
        {
            SqlCommand cmdAssetByTerm = new SqlCommand();
            //cmdAssetByTerm.Connection = vcconn;

            SqlConnection vc_newconn = new SqlConnection();
            Adobe.SettingDAO setting = new Adobe.SettingDAO();
            cmdAssetByTerm.Connection = vc_newconn;
            string termJary = System.Configuration.ConfigurationManager.AppSettings["Term"];
            cmdAssetByTerm.Connection.ConnectionString = setting.getAdobeConnectionString(termJary);



            
            cmdAssetByTerm.CommandType = CommandType.StoredProcedure;
            cmdAssetByTerm.CommandText = "R_GetRecordOfMeeting2";
            cmdAssetByTerm.Parameters.AddWithValue("@date", filedate);
            cmdAssetByTerm.Parameters.AddWithValue("@todate", todate);
            // cmdAssetByTerm.Parameters.AddWithValue("@did", did);

            vc_newconn.Open();
            //connNew.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;

            rdr = cmdAssetByTerm.ExecuteReader();
            dt.Load(rdr);
            vc_newconn.Close();
            //connNew.Close();
            return dt;
        }
        public DataTable GetAllAssetByTermAndDaneshId(string term,int daneshId)
        {
            SqlCommand cmdAssetByTerm = new SqlCommand();
            cmdAssetByTerm.Connection = conn;
            cmdAssetByTerm.CommandType = CommandType.StoredProcedure;
            cmdAssetByTerm.CommandText = "Adobe.SP_ViewFiles";
            cmdAssetByTerm.Parameters.AddWithValue("@term", term);
            cmdAssetByTerm.Parameters.AddWithValue("@daneshID", daneshId);

            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;

            rdr = cmdAssetByTerm.ExecuteReader();
            dt.Load(rdr);
            conn.Close();
            return dt;
        }
        public DataTable GetFileCount(string fdate, string tdate,string term)
        {
            SqlCommand cmdAssetByTerm = new SqlCommand();
            cmdAssetByTerm.Connection = conn;
            cmdAssetByTerm.CommandType = CommandType.StoredProcedure;
            cmdAssetByTerm.CommandText = "Adobe.SP_Get_AssetBydate";
            cmdAssetByTerm.Parameters.AddWithValue("@fdate", fdate);
            cmdAssetByTerm.Parameters.AddWithValue("@tdate", tdate);
            cmdAssetByTerm.Parameters.AddWithValue("@term", term);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;

            rdr = cmdAssetByTerm.ExecuteReader();
            dt.Load(rdr);
            conn.Close();
            return dt;
        }
        #endregion

        #region create
        public void InsertIntoAssetFromServer(AssetDTO asset)//oksargolChangeTypeClassCode
        {

            SqlCommand cmdAssetByTerm = new SqlCommand();
            cmdAssetByTerm.Connection = conn;
            cmdAssetByTerm.CommandType = CommandType.StoredProcedure;
            cmdAssetByTerm.CommandText = "Adobe.SP_Insert_Asset";

            cmdAssetByTerm.Parameters.AddWithValue("@Class_Code", asset.Class_Code);
            cmdAssetByTerm.Parameters.AddWithValue("@URL_File", asset.URL_File);
            cmdAssetByTerm.Parameters.AddWithValue("@Term", asset.Term);
            cmdAssetByTerm.Parameters.AddWithValue("@Session", asset.Session);
            cmdAssetByTerm.Parameters.AddWithValue("@Fee", asset.Fee);
            cmdAssetByTerm.Parameters.AddWithValue("@FileSize", asset.FileSize);
            cmdAssetByTerm.Parameters.AddWithValue("@FileName", asset.FileName);
            cmdAssetByTerm.Parameters.AddWithValue("@FileTime", asset.FileTime);
            cmdAssetByTerm.Parameters.AddWithValue("@FileDate", asset.FileDate);
            cmdAssetByTerm.Parameters.AddWithValue("@IsSynch", asset.IsSynch);
            cmdAssetByTerm.Parameters.AddWithValue("@Link_View", asset.link_view);
            cmdAssetByTerm.Parameters.AddWithValue("@scoid", asset.scoid);
            conn.Open();         
            cmdAssetByTerm.ExecuteNonQuery();
            conn.Close();
          
        }
        #endregion

        #region Update
        public void Set_Archive(string Class_Code, string FileDate, string Term, bool IsArchive)//oksargolChangeTypeClassCode
        {
            SqlCommand cmdAsset = new SqlCommand();
            cmdAsset.Connection = conn;
            cmdAsset.CommandType = CommandType.StoredProcedure;
            cmdAsset.CommandText = "Adobe.SP_Set_Archive";
            cmdAsset.Parameters.AddWithValue("@Class_Code", Class_Code);
            cmdAsset.Parameters.AddWithValue("@FileDate", FileDate);
            cmdAsset.Parameters.AddWithValue("@Term", Term);
            cmdAsset.Parameters.AddWithValue("@IsArchive", IsArchive);
            conn.Open();
            cmdAsset.ExecuteNonQuery();
            conn.Close();
        }
        #endregion

    }
}
