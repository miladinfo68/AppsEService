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
    public class DownloadRequestDAO
    {
        SqlConnection conn = new SqlConnection(new SuppConnection().Supp_con);
        #region Create
        public void Create_DownloadRequest(DownloadRequestDTO dnlReq)
        {
            SqlCommand cmdc = new SqlCommand();
            cmdc.Connection = conn;
            cmdc.CommandType = CommandType.StoredProcedure;
            cmdc.CommandText = "Adobe.SP_Insert_Tbl_DownloadRequest";
            cmdc.Parameters.AddWithValue("@Stcode", dnlReq.StCode);
            cmdc.Parameters.AddWithValue("@AssetId", dnlReq.Class_Code);
            cmdc.Parameters.AddWithValue("@LinkClick", dnlReq.Link_Click);
            //cmdc.Parameters.AddWithValue("@TermClass", dnlReq.Term); 
            conn.Open();
            cmdc.ExecuteNonQuery();
            conn.Close();

        }
        #endregion
        #region Read

        public List<DownloadRequestDTO> GetRequestDownloadByOrderID(long OrderID)
        {
            List<DownloadRequestDTO> RequestList = new List<DownloadRequestDTO>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Adobe.SP_Get_RequestDownloadByOrderID";
            cmd.Parameters.AddWithValue("@OrderID", OrderID);

            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DownloadRequestDTO asset = new DownloadRequestDTO();
                asset.RequestID = int.Parse(dt.Rows[i]["RequestID"].ToString());
                asset.OrderID = long.Parse(dt.Rows[i]["OrderID"].ToString());

                RequestList.Add(asset);
            }
            conn.Close();
            return RequestList;



        }
        public DataTable GetStudentDownloadRequest(string stcode, string Term)
        {
            SqlCommand cmddl = new SqlCommand();
            cmddl.Connection = conn;
            cmddl.CommandText = "Adobe.SP_GetStudentDownloadRequest";
            cmddl.CommandType = CommandType.StoredProcedure;
            cmddl.Parameters.AddWithValue("@stcode",stcode);
            cmddl.Parameters.AddWithValue("@Term",Term);
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlDataReader rdr;
                rdr = cmddl.ExecuteReader();
                dt.Load(rdr);
                conn.Close();
            }
            catch(Exception)
            {
                throw;
            }
            return dt;
        }
        public DataTable Get_Selected_DetailPayment(string stcode , string orderId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Adobe.SP_Get_DetailPayment";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@orderId", orderId);

            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            conn.Close();
            return dt;
        }
        public DataTable Get_SelectedAsset_NotPay(string stcode)//oksargolChangeTypeClassCode
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Adobe.SP_Get_SelectedAsset";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            conn.Close();
            return dt;
        }

        public List<AssetDTO> GetValidAssets(string stcode)//oksargolChangeTypeClassCode
        {
            List<AssetDTO> assetList = new List<AssetDTO>();

            SqlCommand cmdAsset = new SqlCommand();
            cmdAsset.Connection = conn;
            cmdAsset.CommandType = CommandType.StoredProcedure;
            cmdAsset.CommandText = "Adobe.SP_Get_ValidAsset";
            cmdAsset.Parameters.AddWithValue("@stcode", stcode);

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
                asset.namedars = dt.Rows[i]["namedars"].ToString();
                asset.FileDate = dt.Rows[i]["FileDate"].ToString();
                asset.TimeCode = dt.Rows[i]["TimeCode"].ToString();
                asset.URL_File = dt.Rows[i]["URL_File"].ToString();
                asset.FileDate = dt.Rows[i]["FileDate"].ToString();
                asset.Class_Code =(dt.Rows[i]["AssetClassCode"].ToString());
                asset.RowId = dt.Rows[i]["RowId"].ToString();
                asset.Term = dt.Rows[i]["Term"].ToString();
                assetList.Add(asset);
            }
            conn.Close();
            return assetList;

        }
        public DataTable Get_DownloadedFiles_ByStcode(string stcode)//oksargolChangeTypeClassCode
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Adobe.SP_Get_DownloadedFiles_ByStcode";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            conn.Close();
            return dt;
        }
        public DataTable Check_PayedAsset(string stcode, int ClassCode)//oksargolChangeTypeClassCode
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Adobe.SP_Check_PayedAsset";
            cmd.Parameters.AddWithValue("@stcode", stcode);
            cmd.Parameters.AddWithValue("@AssetID", ClassCode);
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            dt.Load(rdr);
            conn.Close();
            return dt;
        }
        #endregion
        #region Delete
        public void DeleteDownloadReq(int RequestID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Adobe.SP_Delete_Tbl_DownloadRequest";
            cmd.Parameters.AddWithValue("@RequestID", RequestID);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        
        #endregion

        #region update

        public void UpdateRequestDownload(long RequestID, string timecode, string refId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Adobe.SP_UpdateDownloadRequestTime";
            cmd.Parameters.AddWithValue("@RequestID", RequestID);
            cmd.Parameters.AddWithValue("@RefID", refId);
            cmd.Parameters.AddWithValue("@TimeCode", timecode);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }


        #endregion
    }
}
