using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Adobe;
using System.Data;
using System.IO;
using Telerik.Web.UI;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class Show_AllFilesCount : System.Web.UI.Page
    {
        public static DataTable dtnotExistRecords = new DataTable();
        public static DataTable dtnotExistFolder = new DataTable();
        CommonBusiness cmb = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
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
            Session[sessionNames.menuID] = menuId;
            AccessControl1.MenuId = menuId;
            AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();

            if (!IsPostBack)
            {
                rcb_Term.DataTextField = "Term";
                rcb_Term.DataValueField = "Term";
                rcb_Term.DataSource = cmb.getActiveTerm_AdobeConnection();
                rcb_Term.DataBind();
               
                //Uri MyUrl = Request.UrlReferrer;
                //if (MyUrl.LocalPath.ToString() == "/Adobe/CMS/MainPage.aspx")
                if (dtnotExistRecords.Columns.Count == 0)
                {
                    dtnotExistRecords.Columns.Add("Class_Code");
                    dtnotExistRecords.Columns.Add("Session");
                    dtnotExistRecords.Columns.Add("FileDate");
                    dtnotExistRecords.Columns.Add("LowLength");
                }
                else
                    if (dtnotExistRecords.Rows.Count > 0)
                        dtnotExistRecords.Clear();
                if (dtnotExistFolder.Columns.Count==0)
                {
                    dtnotExistFolder.Columns.Add("Class_Code");
                    dtnotExistFolder.Columns.Add("Session");
                    dtnotExistFolder.Columns.Add("FileDate");
                }
                else
                    if (dtnotExistFolder.Rows.Count > 0)
                        dtnotExistFolder.Clear();
                
            }
        }

        protected void btn_ShowCount_Click(object sender, EventArgs e)
        {
            AssetBusiness asset = new AssetBusiness();
            DataTable dtAsset = new DataTable();
           
            DataRow dtnotExistRecordsrow = dtnotExistRecords.NewRow();

            if (dtnotExistFolder.Rows.Count > 0)
                dtnotExistFolder.Clear();
            if (dtnotExistRecords.Rows.Count > 0)
                dtnotExistRecords.Clear();
            DataRow dtnotExistFolderRow = dtnotExistFolder.NewRow();
            int notExist = 0, mp3Count = 0, ExistFolder = 0, NotExistmp3Count = 0, mp3Length = 0, LowLengthCorrect=0;

            dtAsset = asset.GetFileCount(date_input_1.Value.ToString(), date_input_2.Value.ToString(), rcb_Term.SelectedValue);
            lblAllCount.Text = dtAsset.Rows.Count.ToString();
            for (int i = 0; i < dtAsset.Rows.Count; i++)
            {
                string d = dtAsset.Rows[i]["FileDate"].ToString().Replace('/', '-');
                string path = Server.MapPath("../content/" + dtAsset.Rows[i]["Term"].ToString() + "/" + dtAsset.Rows[i]["Class_Code"].ToString() + "/" + d);
                if (Directory.Exists(path) == false)
                {
                    notExist++;
                    dtnotExistFolderRow["Class_Code"] = dtAsset.Rows[i]["Class_Code"].ToString();
                    dtnotExistFolderRow["Session"] = dtAsset.Rows[i]["Session"].ToString();
                    dtnotExistFolderRow["FileDate"] = dtAsset.Rows[i]["FileDate"].ToString();
                    dtnotExistFolder.Rows.Add(dtnotExistFolderRow);
                    dtnotExistFolderRow = dtnotExistFolder.NewRow();
                }
                else
                {
                    ExistFolder++;
                    DirectoryInfo di = new DirectoryInfo(path);
                    // Get a reference to each file in that directory.
                    FileInfo[] fiArr = di.GetFiles("mp3.zip");
                    if (fiArr.Count() > 0)
                    {

                        foreach (FileInfo f in fiArr)
                            if (f.Length > 1000000)
                            {
                                mp3Count++;
                            }
                            else
                            {
                                if (!bool.Parse(dtAsset.Rows[i]["IsArchive"].ToString()))
                                {
                                    mp3Length++;
                                    dtnotExistRecordsrow["Class_Code"] = dtAsset.Rows[i]["Class_Code"].ToString();
                                    dtnotExistRecordsrow["Session"] = dtAsset.Rows[i]["Session"].ToString();
                                    dtnotExistRecordsrow["FileDate"] = dtAsset.Rows[i]["FileDate"].ToString();
                                    dtnotExistRecordsrow["LowLength"] = "1";
                                    dtnotExistRecords.Rows.Add(dtnotExistRecordsrow);
                                    dtnotExistRecordsrow = dtnotExistRecords.NewRow();
                                }
                                else
                                {
                                    mp3Count++;
                                    LowLengthCorrect++;
                                }
                            }
                    }
                    else
                    {
                        NotExistmp3Count++;
                        dtnotExistRecordsrow["Class_Code"] = dtAsset.Rows[i]["Class_Code"].ToString();
                        dtnotExistRecordsrow["Session"] = dtAsset.Rows[i]["Session"].ToString();
                        dtnotExistRecordsrow["FileDate"] = dtAsset.Rows[i]["FileDate"].ToString();
                        dtnotExistRecordsrow["LowLength"] = "0";
                        dtnotExistRecords.Rows.Add(dtnotExistRecordsrow);
                        dtnotExistRecordsrow = dtnotExistRecords.NewRow();
                    }
                }
            }
            lblExistFolder.Text = ExistFolder.ToString();
            lblRecordCount.Text = mp3Count.ToString();
            lblNotExistFolder.Text = notExist.ToString();
            lblNotExistRecords.Text = NotExistmp3Count.ToString();
            lbl_Mp3Length.Text = mp3Length.ToString();
            lbl_LowLengthCorrect.Text = LowLengthCorrect.ToString();
            grd_Records.DataSource = dtnotExistRecords;
            grd_Records.DataBind();
            Grd_Folders.DataSource = dtnotExistFolder;
            Grd_Folders.DataBind();
        }

        protected void grd_Records_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            Label lblsize = (Label)e.Item.FindControl("lbl_Desc");
            if (e.Item is GridDataItem)
            {
                GridDataItem itemAmount = (GridDataItem)e.Item;
                TableCell fd = (TableCell)itemAmount["LowLength"];
                if (fd.Text == "1")
                {
                    lblsize.Text = "کم حجم";
                    
                }
            }
        }

        protected void grd_Records_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grd_Records.DataSource = dtnotExistRecords;
        }

        protected void Grd_Folders_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            Grd_Folders.DataSource = dtnotExistFolder;
        }

        protected void ExportToExcelImg_Click(object sender, ImageClickEventArgs e)
        {
            string alternateText = (sender as ImageButton).AlternateText;
            grd_Records.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), alternateText);
            grd_Records.ExportSettings.IgnorePaging = true;
            grd_Records.ExportSettings.ExportOnlyData = true;
            grd_Records.ExportSettings.OpenInNewWindow = true;
            grd_Records.ExportSettings.UseItemStyles = true;
            grd_Records.ExportSettings.FileName = "RecordsReport-" + DateTime.Now.ToShortDateString();
            grd_Records.MasterTableView.ExportToExcel();
        }

        protected void ExportToExcelImg2_Click(object sender, ImageClickEventArgs e)
        {
            string alternateText = (sender as ImageButton).AlternateText;
            Grd_Folders.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), alternateText);
            Grd_Folders.ExportSettings.IgnorePaging = true;
            Grd_Folders.ExportSettings.ExportOnlyData = true;
            Grd_Folders.ExportSettings.OpenInNewWindow = true;
            Grd_Folders.ExportSettings.UseItemStyles = true;
            Grd_Folders.ExportSettings.FileName = "FoldersReport-" + DateTime.Now.ToShortDateString();
            Grd_Folders.MasterTableView.ExportToExcel();
        }
    }
}