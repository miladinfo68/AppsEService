using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.DTO.AdobeClasses;
using System.IO;
using System.Data;
using IAUEC_Apps.Business.Common;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class ReportByFileSize : System.Web.UI.Page
    {
        AssetBusiness assetB = new AssetBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string mId = Request.QueryString["id"].ToString();
                string[] id = mId.ToString().Split(new char[] { '@' });
                string menuId = "";
                for (int i = 0; i < id[1].Length; i++)
                {
                    string s = id[1].Substring(i + 1, 1);
                    if (s != "-")
                        menuId += s;
                    else
                        break;
                }

                AccessControl1.MenuId = menuId;
                Session[sessionNames.menuID] = menuId;
                AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
                if (!IsPostBack)
                {
                    CommonBusiness cmb = new CommonBusiness();
                    cmb_Term.DataSource = cmb.getActiveTerm_AdobeConnection();
                    cmb_Term.DataTextField = "Term";
                    cmb_Term.DataValueField = "Term";

                    cmb_Term.DataBind();
                }
            }
            catch
            {
            }
        }
        protected void headerChkbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox headerCheckBox = (sender as CheckBox);
            foreach (GridDataItem dataItem in grd_FileList.MasterTableView.Items)
            {
                (dataItem.FindControl("chk") as CheckBox).Checked = headerCheckBox.Checked;
                dataItem.Selected = headerCheckBox.Checked;
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();
            List<AssetDTO> DTO = new List<AssetDTO>();

            DTO = assetB.GetAssetListBySize(cmb_Term.SelectedValue, false,int.Parse(txt_FileSize.Text));
            //DataTable dt = new DataTable();
            //dt.Columns.Add("fd", Type.GetType("System.String"));
            //dt.Columns.Add("classcode", Type.GetType("System.String"));
            //dt.Columns.Add("term", Type.GetType("System.String"));
            ////dt.Columns.Add("AssetID", Type.GetType("System.String"));
            //dt.Columns.Add("FileSize", Type.GetType("System.String"));
            //dt.Columns.Add("FileDate", Type.GetType("System.String"));
            //DataRow dtRow = dt.NewRow();
            //for (int i = 0; i < DTO.Count; i++)
            //{
            //    string fd = DTO[i].FileDate;
            //    string classcode = DTO[i].Class_Code.ToString();
            //    string term = DTO[i].Term.ToString();
            //    string d = fd.Replace('/', '-');
            //    string assetId = DTO[i].AssetID.ToString();
            //    string path = Server.MapPath("../content/" + term + "/" + classcode + "/" + d);


            //    if (Directory.Exists(path))
            //    {
            //        DirectoryInfo di = new DirectoryInfo(path);
            //        // Get a reference to each file in that directory.
            //        FileInfo[] fiArr = di.GetFiles("mp3.zip");

            //        foreach (FileInfo f in fiArr)
            //            if (f.Length < (int.Parse(txt_FileSize.Text) * 1024))
            //            {
            //                dtRow["fd"] = d.ToString();
            //                dtRow["classcode"] = classcode.ToString();
            //                dtRow["term"] = term.ToString();
            //                //dtRow["AssetID"] = assetId.ToString();
            //                dtRow["FileSize"] = Math.Ceiling(decimal.Parse(f.Length.ToString()) / 1024);
            //                dtRow["FileDate"] = DTO[i].FileDate;
            //                dt.Rows.Add(dtRow);
            //                dtRow = dt.NewRow();
            //                //System.IO.Directory.Delete(path, true);
            //            }
            //    }
            //}
            grd_FileList.DataSource = DTO;
            grd_FileList.DataBind();
        }

        protected void grd_FileList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                CommonBusiness cmnb = new CommonBusiness();
                List<AssetDTO> DTO = new List<AssetDTO>();
                if (txt_FileSize.Text != "0")
                {
                    assetB.GetAssetListBySize(cmb_Term.SelectedValue, false, int.Parse(txt_FileSize.Text));
                    //DataTable dt = new DataTable();
                    //dt.Columns.Add("fd", Type.GetType("System.String"));
                    //dt.Columns.Add("classcode", Type.GetType("System.String"));
                    //dt.Columns.Add("term", Type.GetType("System.String"));
                    //dt.Columns.Add("FileSize", Type.GetType("System.String"));
                    //dt.Columns.Add("FileDate", Type.GetType("System.String"));
                    //DataRow dtRow = dt.NewRow();
                    //for (int i = 0; i < DTO.Count; i++)
                    //{
                    //    string fd = DTO[i].FileDate;
                    //    string classcode = DTO[i].Class_Code.ToString();
                    //    string term = DTO[i].Term.ToString();
                    //    string d = fd.Replace('/', '-');
                    //    string assetId = DTO[i].AssetID.ToString();
                    //    string path = Server.MapPath("../content/" + term + "/" + classcode + "/" + d);


                    //    if (Directory.Exists(path))
                    //    {
                    //        DirectoryInfo di = new DirectoryInfo(path);
                    //        // Get a reference to each file in that directory.
                    //        FileInfo[] fiArr = di.GetFiles("mp3.zip");

                    //        foreach (FileInfo f in fiArr)
                    //            if (f.Length < (int.Parse(txt_FileSize.Text) * 1024) * 1024)
                    //            {
                    //                dtRow["fd"] = d.ToString();
                    //                dtRow["classcode"] = classcode.ToString();
                    //                dtRow["term"] = term.ToString();
                    //                //dtRow["AssetID"] = assetId.ToString();
                    //                dtRow["FileSize"] = Math.Ceiling(decimal.Parse(f.Length.ToString()) / 1024);
                    //                dtRow["FileDate"] = DTO[i].FileDate;
                    //                dt.Rows.Add(dtRow);
                    //                dtRow = dt.NewRow();
                    //                //System.IO.Directory.Delete(path, true);
                    //            }
                    //    }
                    //}
                    grd_FileList.DataSource = DTO;
                }
            }
            catch
            {
            }
        }

        protected void btn_AddFailedList_Click(object sender, EventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();
            foreach (GridDataItem grd in grd_FileList.Items)
            {
                HiddenField fd = (HiddenField)grd.FindControl("filedate");
                TableCell classcode = (TableCell)grd["Class_Code"];
                TableCell term = (TableCell)grd["term"];
                CheckBox check = (CheckBox)grd.FindControl("chk");
                if (check.Checked)
                {
                    assetB.set_archive((classcode.Text), fd.Value, term.Text, true);
                    cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 20, "");
                }

            }
     
            List<AssetDTO> DTO = new List<AssetDTO>();

            assetB.GetAssetListBySize(cmb_Term.SelectedValue, false, int.Parse(txt_FileSize.Text));
            //DataTable dt = new DataTable();
            //dt.Columns.Add("fd", Type.GetType("System.String"));
            //dt.Columns.Add("classcode", Type.GetType("System.String"));
            //dt.Columns.Add("term", Type.GetType("System.String"));
            ////dt.Columns.Add("AssetID", Type.GetType("System.String"));
            //dt.Columns.Add("FileSize", Type.GetType("System.String"));
            //dt.Columns.Add("FileDate", Type.GetType("System.String"));
            //DataRow dtRow = dt.NewRow();
            //for (int i = 0; i < DTO.Count; i++)
            //{
            //    string fd = DTO[i].FileDate;
            //    string classcode = DTO[i].Class_Code.ToString();
            //    string term = DTO[i].Term.ToString();
            //    string d = fd.Replace('/', '-');
            //    string assetId = DTO[i].AssetID.ToString();
            //    string path = Server.MapPath("../content/" + term + "/" + classcode + "/" + d);


            //    if (Directory.Exists(path))
            //    {
            //        DirectoryInfo di = new DirectoryInfo(path);
            //        // Get a reference to each file in that directory.
            //        FileInfo[] fiArr = di.GetFiles("mp3.zip");

            //        foreach (FileInfo f in fiArr)
            //            if (f.Length < (int.Parse(txt_FileSize.Text) * 1024))
            //            {
            //                dtRow["fd"] = d.ToString();
            //                dtRow["classcode"] = classcode.ToString();
            //                dtRow["term"] = term.ToString();
            //                //dtRow["AssetID"] = assetId.ToString();
            //                dtRow["FileSize"] = Math.Ceiling(decimal.Parse(f.Length.ToString()) / 1024);
            //                dtRow["FileDate"] = DTO[i].FileDate;
            //                dt.Rows.Add(dtRow);
            //                dtRow = dt.NewRow();
            //                //System.IO.Directory.Delete(path, true);
            //            }
            //    }
            //}
            grd_FileList.MasterTableView.Dispose();
            grd_FileList.DataSource = DTO;
            grd_FileList.DataBind();
        }

        protected void ExportToExcelImg1_Click(object sender, ImageClickEventArgs e)
        {
            string alternateText = (sender as ImageButton).AlternateText;
            grd_FileList.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), alternateText);
            grd_FileList.ExportSettings.IgnorePaging = true;
            grd_FileList.ExportSettings.ExportOnlyData = true;
            grd_FileList.ExportSettings.OpenInNewWindow = true;
            grd_FileList.ExportSettings.UseItemStyles = true;
            grd_FileList.ExportSettings.FileName = "Report-ByFileSize-" + DateTime.Now.ToShortDateString();
            grd_FileList.MasterTableView.ExportToExcel();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //string alternateText = (sender as ImageButton).AlternateText;
                //grd_FileList.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), alternateText);
                //grd_FileList.ExportSettings.IgnorePaging = true;
                //grd_FileList.ExportSettings.ExportOnlyData = true;
                //grd_FileList.ExportSettings.OpenInNewWindow = true;
                //grd_FileList.ExportSettings.UseItemStyles = true;
                grd_FileList.ExportSettings.FileName = "Report-ByFileSize-" + DateTime.Now.ToShortDateString();
                grd_FileList.ExportSettings.IgnorePaging = true;
                grd_FileList.MasterTableView.ExportToExcel();
            }
            catch
            {
            }
        }

       
    }
}