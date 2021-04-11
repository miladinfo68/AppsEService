using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.Business.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class deleteNonExistentRecords : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                manageAccessControl();
                setTermSource();
            }
        }

        private void manageAccessControl()
        {
            if (Request.QueryString["id"] == null)
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");
            if (Request.QueryString["id"] != null)
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
                AccessControl.MenuId = menuId;
                Session[sessionNames.menuID] = menuId;
                AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();
            }
        }

        private void setTermSource()
        {
            CommonBusiness cmb = new CommonBusiness();
            cmbTerm.DataSource = cmb.getActiveTerm_AdobeConnection();
            cmbTerm.DataTextField = "Term";
            cmbTerm.DataValueField = "Term";
            cmbTerm.DataBind();
        }

        private void setGridSource()
        {
            if (txtFromDate.Value.ToString() != "" && txtToDate.Value.ToString() != "")
            {
                AssetBusiness asset = new AssetBusiness();
                DataTable dtnotExistRecords = new DataTable();
                var dtAsset = asset.GetFileCount(txtFromDate.Value.ToString(), txtToDate.Value.ToString(), cmbTerm.SelectedItem.Value);
                string date, path;
                dtnotExistRecords.Columns.Add("Class_Code");
                dtnotExistRecords.Columns.Add("term");
                dtnotExistRecords.Columns.Add("FileDate");
                foreach (DataRow dr in dtAsset.Rows)
                {
                    date = dr["FileDate"].ToString().Replace('/', '-');
                    path = Server.MapPath("../content/" + dr["Term"].ToString() + "/" + dr["Class_Code"].ToString() + "/" + date);
                    if (Directory.Exists(path) == true)
                    {
                        DirectoryInfo di = new DirectoryInfo(path);
                        // Get a reference to each file in that directory.
                        FileInfo[] fiArrZip = di.GetFiles("*.zip");
                        if ((di.GetFiles("*.mp3").Count()==0 || di.GetFiles("*.avi").Count() == 0|| di.GetFiles("*.flv").Count() == 0) && fiArrZip.Count()==0 )
                        {
                            DataRow drNotExistRecordsrow = dtnotExistRecords.NewRow();
                            drNotExistRecordsrow["Class_Code"] = dr["Class_Code"].ToString();
                            drNotExistRecordsrow["term"] = dr["term"].ToString();
                            drNotExistRecordsrow["FileDate"] = dr["FileDate"].ToString();
                            dtnotExistRecords.Rows.Add(drNotExistRecordsrow);

                        }
                    }
                }
                grd_NonExistFolder.DataSource = dtnotExistRecords;
            }
        }

        protected void grd_NonExistFolder_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            
            setGridSource();

        }


        protected void headerChkbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox headerCheckBox = (sender as CheckBox);
            foreach (GridDataItem dataItem in grd_NonExistFolder.MasterTableView.Items)
            {
                (dataItem.FindControl("chk") as CheckBox).Checked = headerCheckBox.Checked;
                dataItem.Selected = headerCheckBox.Checked;
            }
        }

        protected void cmbTerm_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            setGridSource();
            grd_NonExistFolder.DataBind();
        }

        protected void btnShowFolders_Click(object sender, EventArgs e)
        {
            setGridSource();
            grd_NonExistFolder.DataBind();

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();
            foreach (GridDataItem grd in grd_NonExistFolder.Items)
            {
                TableCell fd = (TableCell)grd["fileDate"];
                TableCell classcode = (TableCell)grd["class_code"];
                TableCell term = (TableCell)grd["term"];
                CheckBox check = (CheckBox)grd.FindControl("chk");
                if (check.Checked)
                {
                    string d = fd.Text.ToString().Replace('/', '-');
                    string path = Server.MapPath("../content/" + term.Text + "/" + classcode.Text + "/" + d);
                    System.IO.Directory.Delete(path, true);
                    cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), (int)DTO.eventEnum.حذف_فولدرهای_بدون_فایل_ادوبی, classcode.Text+ "/" + d);
                }

            }
            foreach (GridDataItem item in grd_NonExistFolder.MasterTableView.Items)
            {
                CheckBox check = (CheckBox)item.FindControl("chk");
                if (!check.Checked)
                    item.Visible = false;
            }

            grd_NonExistFolder.ExportSettings.FileName = "Last-file-Deleted-" + DateTime.Now.ToString();
            grd_NonExistFolder.MasterTableView.ExportToExcel();

        }
    }
}