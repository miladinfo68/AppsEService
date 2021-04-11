using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.DAO.CommonDAO;
using System.Data;


namespace IAUEC_Apps.Business.Adobe
{
   public class DownloadRequestBusiness
    {
       DownloadRequestDAO dnlDAO = new DownloadRequestDAO();
       Common.UserAccessBusiness uab = new Common.UserAccessBusiness();

       public void Create_DownloadRequest(DownloadRequestDTO dnlReq)
       {
           dnlDAO.Create_DownloadRequest(dnlReq);
       }
       public void DeleteDownloadReq(int RequestID)
       {
           dnlDAO.DeleteDownloadReq(RequestID);
       }

       public void UpdateRequestDownload(long OrderId, string refId)
       {
           string timecode = uab.ReturnTimeToken(DateTime.Now);
           List<DownloadRequestDTO> RequestList = dnlDAO.GetRequestDownloadByOrderID(OrderId);
           foreach (var item in RequestList)
           {
               dnlDAO.UpdateRequestDownload(item.RequestID, timecode, refId);
           }

       }

       public List<AssetDTO> GetValidAssets(string stcode)
       {
           List<AssetDTO> ValidList = dnlDAO.GetValidAssets(stcode);
           //List<AssetDTO> assetList = new List<AssetDTO>();
           //foreach (var item in ValidList)
           //{

           //    if (uab.IsTimeTokenValid(item.TimeCode))
           //    {
           //        assetList.Add(item);
           //    }

           //}
           return ValidList;

       }
       public List<ReportDownloadReqDTO> Get_DownloadedFiles_ByStcode(string stcode)
       {
           DataTable dt = new DataTable();
           List<ReportDownloadReqDTO> lstdnlDTO = new List<ReportDownloadReqDTO>();
          
           dt = dnlDAO.Get_DownloadedFiles_ByStcode(stcode);
           for (int i = 0; i < dt.Rows.Count; i++)
           { 
               ReportDownloadReqDTO dnlDTO = new ReportDownloadReqDTO();
               dnlDTO.AssetClassCode = dt.Rows[i]["Class_Code"].ToString();
               dnlDTO.FileDate = dt.Rows[i]["FileDate"].ToString();
               dnlDTO.Session = dt.Rows[i]["Session"].ToString();
               dnlDTO.Term = dt.Rows[i]["Term"].ToString();
               dnlDTO.SaveDate = Convert.ToDateTime(dt.Rows[i]["SaveDate"].ToString()).ToShortDateString();
               lstdnlDTO.Add(dnlDTO);
           }
           return lstdnlDTO;
       }

       public DataTable Check_PayedAsset(string stcode, int AssetId)
       {
           return dnlDAO.Check_PayedAsset(stcode, AssetId);
       }
       public bool GetSelectedAssetWithoutPayment(string stcode)
       {

           if (dnlDAO.Get_SelectedAsset_NotPay(stcode).Rows.Count > 0)
           {
               return true;
           }
           else
           {
               return false;
           }
       }

        public DataTable Get_Selected_DetailPayment(string stcode, string orderId)
        {
            return dnlDAO.Get_Selected_DetailPayment(stcode, orderId);
        }
       
       public DataTable GetStudentDownloadRequest(string stcode,string Term)
       {
          return dnlDAO.GetStudentDownloadRequest(stcode,Term);
       }

    }
}
