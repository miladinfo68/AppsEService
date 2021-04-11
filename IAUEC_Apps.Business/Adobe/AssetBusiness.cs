using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.CommonDAO;
using IAUEC_Apps.DTO.AdobeClasses;
using System.Data;

namespace IAUEC_Apps.Business.Adobe
{
    public class AssetBusiness
    {
        AssetDAO assetDAO = new AssetDAO();
        ClassDAO classDAO = new ClassDAO();

        public List<AssetDTO> Show_Asset_List_ByClassCode(string ClassCode, string term)
        {
            return assetDAO.Show_Asset_List_ByClassCode(ClassCode, term);
        }
        public List<AssetDTO> Show_All_assetList()
        {
            DataTable dt = new DataTable();
            List<AssetDTO> assetList = new List<AssetDTO>();
            dt = assetDAO.Show_All_assetList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AssetDTO asset = new AssetDTO();
                asset.AssetID = int.Parse(dt.Rows[i]["AssetID"].ToString());
                asset.FileName = dt.Rows[i]["FileName"].ToString();
                asset.FileDate = dt.Rows[i]["FileDate"].ToString();
                asset.Class_Code = dt.Rows[i]["Class_Code"].ToString();
                asset.Term = dt.Rows[i]["Term"].ToString();
                assetList.Add(asset);
            }
            return assetList;
        }
        public List<AssetDTO> GetAllAssetListByTerm(string term, bool IsArchive)
        {
            DataTable dt = new DataTable();
            List<AssetDTO> assetList = new List<AssetDTO>();
            dt = assetDAO.GetAllAssetByTerm(term, IsArchive);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AssetDTO asset = new AssetDTO();
                asset.AssetID = int.Parse(dt.Rows[i]["AssetID"].ToString());
                asset.FileName = dt.Rows[i]["FileName"].ToString();
                asset.FileDate = dt.Rows[i]["FileDate"].ToString();
                asset.Class_Code = dt.Rows[i]["Class_Code"].ToString();
                asset.Term = dt.Rows[i]["Term"].ToString();
                assetList.Add(asset);
            }
            return assetList;
        }

        public List<AssetDTO> GetAssetListByTerm(string term, bool IsArchive)
        {
            DataTable dt = new DataTable();
            List<AssetDTO> assetList = new List<AssetDTO>();
            dt = assetDAO.GetAssetByTerm(term, IsArchive);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AssetDTO asset = new AssetDTO();
                asset.AssetID = int.Parse(dt.Rows[i]["AssetID"].ToString());
                asset.FileDate = dt.Rows[i]["FileDate"].ToString();
                asset.Class_Code = (dt.Rows[i]["Class_Code"].ToString());
                asset.Term = dt.Rows[i]["Term"].ToString();
                assetList.Add(asset);
            }
            return assetList;
        }
        public List<AssetDTO> GetAssetListBySize(string term, bool IsArchive, int filesize)
        {
            DataTable dt = new DataTable();
            List<AssetDTO> assetList = new List<AssetDTO>();
            dt = assetDAO.GetAssetBySize(term, IsArchive, filesize);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AssetDTO asset = new AssetDTO();
                asset.AssetID = int.Parse(dt.Rows[i]["AssetID"].ToString());
                asset.FileDate = dt.Rows[i]["FileDate"].ToString();
                asset.Class_Code = (dt.Rows[i]["Class_Code"].ToString());
                asset.Term = dt.Rows[i]["Term"].ToString();
                asset.FileSize = int.Parse(dt.Rows[i]["filesize"].ToString());
                assetList.Add(asset);
            }
            return assetList;
        }
        public List<CMSViewFilesDTO> GetAllAssetByTermAndDaneshId(string term, int DaneshId)
        {
            DataTable dt = new DataTable();

            List<CMSViewFilesDTO> assetList = new List<CMSViewFilesDTO>();
            dt = assetDAO.GetAllAssetByTermAndDaneshId(term, DaneshId);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CMSViewFilesDTO asset = new CMSViewFilesDTO();
                asset.Class_Code = dt.Rows[i]["Class_Code"].ToString();
                asset.Term = dt.Rows[i]["Term"].ToString();
                asset.nameresh = dt.Rows[i]["nameresh"].ToString();
                asset.namedanesh = dt.Rows[i]["namedanesh"].ToString();
                if (dt.Rows[i]["iddanesh"].ToString() != "")
                {
                    asset.iddanesh = int.Parse(dt.Rows[i]["iddanesh"].ToString());
                }

                asset.URL_File = dt.Rows[i]["URL_File"].ToString();
                asset.FileDate = dt.Rows[i]["FileDate"].ToString();
                asset.namedars = dt.Rows[i]["namedars"].ToString();
                asset.AssetID = int.Parse(dt.Rows[i]["AssetID"].ToString());
                assetList.Add(asset);
            }
            return assetList;
        }
        public void GetAllUrlsByTerm1(string term, int day, string filedate)
        {
            DataTable dt = new DataTable();

            dt = classDAO.GetAllClassesByTerm(term, day);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataTable dtClasses = assetDAO.GetAllAssetBydid(dt.Rows[i][0].ToString(), filedate);

                for (int j = 0; j < dtClasses.Rows.Count; j++)
                {

                    AssetDTO asset = new AssetDTO();
                    // asset.AssetID = int.Parse(dt.Rows[i]["AssetID"].ToString());
                    asset.Class_Code = (dtClasses.Rows[j]["code"].ToString());
                    asset.URL_File = "/" + dtClasses.Rows[j]["_val"].ToString() + "/";
                    asset.Term = term;
                    asset.Session = dtClasses.Rows[j]["session"].ToString();
                    asset.Fee = 3000;
                    asset.FileSize = double.Parse(dtClasses.Rows[j]["storage"].ToString());
                    asset.FileName = dtClasses.Rows[j]["name"].ToString();
                    asset.FileTime = dtClasses.Rows[j]["DATE_CREATED"].ToString();
                    asset.FileDate = dtClasses.Rows[j]["Shamsi Date"].ToString();
                    asset.IsSynch = true;
                    asset.link_view = "";
                    assetDAO.InsertIntoAssetFromServer(asset);


                }

            }



        }
        public void GetAllUrlsByTerm(string term, int day, string filedate)
        {
            DataTable dt = new DataTable();


            DataTable dtClasses = assetDAO.GetAllAssetByTime(filedate);

            for (int j = 0; j < dtClasses.Rows.Count; j++)
            {

                AssetDTO asset = new AssetDTO();
                // asset.AssetID = int.Parse(dt.Rows[i]["AssetID"].ToString());
                asset.Class_Code = (dtClasses.Rows[j]["code"].ToString());
                asset.URL_File = "/" + dtClasses.Rows[j]["_val"].ToString() + "/";
                asset.Term = term;
                asset.Session = dtClasses.Rows[j]["session"].ToString();
                asset.Fee = 3000;
                asset.FileSize = double.Parse(dtClasses.Rows[j]["storage"].ToString());
                asset.FileName = dtClasses.Rows[j]["name"].ToString();
                asset.FileTime = dtClasses.Rows[j]["DATE_CREATED"].ToString();
                asset.FileDate = dtClasses.Rows[j]["Shamsi Date"].ToString();
                asset.IsSynch = true;
                asset.link_view = "";
                assetDAO.InsertIntoAssetFromServer(asset);


            }





        }
        public void GetAllUrlsByTerm(string term, string filedate, string todate)
        {
            DataTable dt = new DataTable();


            DataTable dtClasses = assetDAO.GetAllAssetByTime(filedate, todate);

            for (int j = 0; j < dtClasses.Rows.Count; j++)
            {
                try
                {
                    AssetDTO asset = new AssetDTO();
                    // asset.AssetID = int.Parse(dt.Rows[i]["AssetID"].ToString());
                    asset.Class_Code = (dtClasses.Rows[j]["code"].ToString());
                    asset.URL_File = "/" + dtClasses.Rows[j]["_val"].ToString() + "/";
                    asset.Term = term;
                    asset.Session = dtClasses.Rows[j]["session"].ToString();
                    switch (term)
                    {
                        case "94-95-1":
                        case "94-95-2":
                        case "95-96-1":
                        case "95-96-2":
                            asset.Fee = 3000;
                            break;
                        case "96-97-1":
                        case "96-97-2":
                        case "97-98-1":
                        case "97-98-2":
                            asset.Fee = 5000;
                            break;
                        default:
                            asset.Fee = 10000;
                            break;

                    }


                    asset.FileSize = double.Parse(dtClasses.Rows[j]["storage"].ToString());
                    asset.FileName = dtClasses.Rows[j]["name"].ToString();
                    asset.FileTime = dtClasses.Rows[j]["DATE_CREATED"].ToString();
                    asset.FileDate = dtClasses.Rows[j]["Shamsi Date"].ToString();
                    asset.IsSynch = true;
                    asset.link_view = "";
                    asset.scoid = long.Parse(dtClasses.Rows[j]["scoid"].ToString());
                    assetDAO.InsertIntoAssetFromServer(asset);
                }
                catch(Exception ex)
                {

                }


            }





        }
        public DataTable GetFileCount(string fdate, string tdate, string term)
        {
            DataTable dt = new DataTable();

            dt = assetDAO.GetFileCount(fdate, tdate, term);

            return dt;

        }
        public void set_archive(string Class_Code, string FileDate, string Term, bool IsArchive)
        {
            assetDAO.Set_Archive(Class_Code, FileDate, Term, IsArchive);
        }
    }
}
