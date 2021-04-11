using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.CommonDAO;
using IAUEC_Apps.DTO.AdobeClasses;
using System.Data;

namespace IAUEC_Apps.Business.Adobe
{
   public class Reports
    {
       public List<ReportDownloadReqDTO> Get_SelectedAsset_NotPay(string Stcode)
       {
           DownloadRequestDAO dnlDAO=new DownloadRequestDAO();
           DataTable dt = new DataTable();
           dt = dnlDAO.Get_SelectedAsset_NotPay(Stcode);
           List<ReportDownloadReqDTO> lst = new List<ReportDownloadReqDTO>();
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               ReportDownloadReqDTO dnlDTO = new ReportDownloadReqDTO();
               dnlDTO.RequestID = double.Parse(dt.Rows[i]["RequestID"].ToString());
               dnlDTO.StCode = Stcode;
               dnlDTO.Fee = int.Parse(dt.Rows[i]["Fee"].ToString());
               dnlDTO.FileName = dt.Rows[i]["FileName"].ToString();
               dnlDTO.Session = dt.Rows[i]["Session"].ToString();
               dnlDTO.Class_Code = (dt.Rows[i]["AssetID"].ToString());
               dnlDTO.AssetClassCode = (dt.Rows[i]["AssetClassCode"].ToString());
               dnlDTO.Namedars= dt.Rows[i]["namedars"].ToString();
               dnlDTO.Term = dt.Rows[i]["Term"].ToString();
               dnlDTO.RowId = dt.Rows[i]["RowId"].ToString();
               dnlDTO.FileDate = dt.Rows[i]["FileDate"].ToString();
               lst.Add(dnlDTO);
           }
           return lst;
       }
    }
}
