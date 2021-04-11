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
    public partial class Delete0KB : System.Web.UI.Page
    {
        AssetBusiness assetB = new AssetBusiness();
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();
            foreach (GridDataItem grd in grd_DeleteInformationClass.Items)
            {
                TableCell fd = (TableCell)grd["fd"];
                TableCell classcode = (TableCell)grd["classcode"];
                TableCell term = (TableCell)grd["term"];
                CheckBox check = (CheckBox)grd.FindControl("chk");
                if (check.Checked)
                {
                    string d = fd.Text.ToString().Replace('/', '-');
                    string path = Server.MapPath("../content/" + term.Text + "/" + classcode.Text + "/" + d);
                    System.IO.Directory.Delete(path, true);
                    cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), (int)IAUEC_Apps.DTO.eventEnum.حذف_فایل_های_0_KB, classcode.Text + " - " + fd.Text);
                }
             
            }
            foreach (GridDataItem item in grd_DeleteInformationClass.MasterTableView.Items)
            {
               
                CheckBox check = (CheckBox)item.FindControl("chk");
                if (!check.Checked)
                    
                    item.Visible = false;
            }
     
            grd_DeleteInformationClass.ExportSettings.FileName = "Last-file-Deleted-" + DateTime.Now.ToString();
            grd_DeleteInformationClass.MasterTableView.ExportToExcel();

        }
        protected void headerChkbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox headerCheckBox = (sender as CheckBox);
            foreach (GridDataItem dataItem in grd_DeleteInformationClass.MasterTableView.Items)
            {
                (dataItem.FindControl("chk") as CheckBox).Checked = headerCheckBox.Checked;
                dataItem.Selected = headerCheckBox.Checked;
            }
        }

        protected void cmb_Term_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();
            List<AssetDTO> DTO = new List<AssetDTO>();
            DTO = assetB.GetAssetListByTerm(cmb_Term.SelectedValue, false);
            DataTable dt = new DataTable();
            dt.Columns.Add("fd", Type.GetType("System.String"));
            dt.Columns.Add("classcode", Type.GetType("System.String"));
            dt.Columns.Add("term", Type.GetType("System.String"));
            //dt.Columns.Add("AssetID", Type.GetType("System.String"));
            dt.Columns.Add("FileSize", Type.GetType("System.String"));
            dt.Columns.Add("FileDate", Type.GetType("System.String"));
            DataRow dtRow = dt.NewRow();
            //
            DataTable dtmp3 = new DataTable();
            dtmp3.Columns.Add("fd", Type.GetType("System.String"));
            dtmp3.Columns.Add("classcode", Type.GetType("System.String"));
            dtmp3.Columns.Add("term", Type.GetType("System.String"));
            //dtmp3.Columns.Add("AssetID", Type.GetType("System.String"));
            dtmp3.Columns.Add("FileSize", Type.GetType("System.String"));
            dtmp3.Columns.Add("FileDate", Type.GetType("System.String"));
            DataRow dtmp3row = dtmp3.NewRow();
            for (int i = 0; i < DTO.Count; i++)
            {
                string fd = DTO[i].FileDate;
                string classcode = DTO[i].Class_Code.ToString();
                string term = DTO[i].Term.ToString();
                string d = fd.Replace('/', '-');
                string assetId = DTO[i].AssetID.ToString();
                string path = Server.MapPath("../content/" + term + "/" + classcode + "/" + d);


                if (Directory.Exists(path))
                {
                 
                    DirectoryInfo di = new DirectoryInfo(path);
                  
                    // Get a reference to each file in that directory.
                    FileInfo[] fiArr = di.GetFiles("mp3.zip");
                    if (di.Exists)
                    {
                        foreach (FileInfo f in fiArr)
                            if (f.Length < 1000000)
                            {
                                dtRow["fd"] = d.ToString();
                                dtRow["classcode"] =classcode.ToString();
                                dtRow["term"] = term.ToString();
                                //dtRow["AssetID"] = assetId.ToString();
                                dtRow["FileSize"] = Math.Ceiling(decimal.Parse(f.Length.ToString()) / 1024);
                                dtRow["FileDate"] = DTO[i].FileDate;
                                dt.Rows.Add(dtRow);
                                dtRow = dt.NewRow();
                                //System.IO.Directory.Delete(path, true);
                            }
                        FileInfo[] fiArrmp3 = di.GetFiles("*.mp3");
                        foreach (FileInfo f in fiArrmp3)
                        {
                            dtmp3row["fd"] = d.ToString();
                            dtmp3row["classcode"] = classcode.ToString();
                            dtmp3row["term"] = term.ToString();
                            //dtmp3row["AssetID"] = assetId.ToString();
                            dtmp3row["FileSize"] = Math.Ceiling(decimal.Parse(f.Length.ToString()) / 1024);
                            dtmp3row["FileDate"] = DTO[i].FileDate;
                            dtmp3.Rows.Add(dtmp3row);
                            dtmp3row = dtmp3.NewRow();
                            //System.IO.Directory.Delete(path, true);
                        }
                    }
                }

            }
            grd_DeleteInformationClass.DataSource = dt;
            grd_DeleteInformationClass.DataBind();
            grd_NoZipFile.DataSource = dtmp3;
            grd_NoZipFile.DataBind();
        }

        protected void btn_DeleteArchive_Click(object sender, EventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();
            foreach (GridDataItem grd in grd_DeleteInformationClass.Items)
            {
                HiddenField fd = (HiddenField)grd.FindControl("filedate");
                TableCell classcode = (TableCell)grd["classcode"];
                TableCell term = (TableCell)grd["term"];
                CheckBox check = (CheckBox)grd.FindControl("chk");
                if (check.Checked)
                {
                    assetB.set_archive(classcode.Text,fd.Value,term.Text,true);
                    cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), (int)IAUEC_Apps.DTO.eventEnum.حذف_فایل_های_0_KB, classcode.Text+" - "+ fd.Value);
                }

            }
         
            List<AssetDTO> DTO = new List<AssetDTO>();
            DTO = assetB.GetAssetListByTerm(cmb_Term.SelectedValue, false);
            DataTable dt = new DataTable();
            dt.Columns.Add("fd", Type.GetType("System.String"));
            dt.Columns.Add("classcode", Type.GetType("System.String"));
            dt.Columns.Add("term", Type.GetType("System.String"));
            //dt.Columns.Add("AssetID", Type.GetType("System.String"));
            dt.Columns.Add("FileSize", Type.GetType("System.String"));
            dt.Columns.Add("FileDate", Type.GetType("System.String"));
            DataRow dtRow = dt.NewRow();
            for (int i = 0; i < DTO.Count; i++)
            {
                string fd = DTO[i].FileDate;
                string classcode = DTO[i].Class_Code.ToString();
                string term = DTO[i].Term.ToString();
                string d = fd.Replace('/', '-');
                string assetId = DTO[i].AssetID.ToString();
                string path = Server.MapPath("../content/" + term + "/" + classcode + "/" + d);


                if (Directory.Exists(path))
                {
                    DirectoryInfo di = new DirectoryInfo(path);
                    // Get a reference to each file in that directory.
                    FileInfo[] fiArr = di.GetFiles("mp3.zip");
                    foreach (FileInfo f in fiArr)
                        if (f.Length < 1000000)
                        {
                            dtRow["fd"] = d.ToString();
                            dtRow["classcode"] = classcode.ToString();
                            dtRow["term"] = term.ToString();
                            //dtRow["AssetID"] = assetId.ToString();
                            dtRow["FileSize"] = Math.Ceiling(decimal.Parse(f.Length.ToString()) / 1024);
                            dtRow["FileDate"] = DTO[i].FileDate;
                            dt.Rows.Add(dtRow);
                            dtRow = dt.NewRow();
                            //System.IO.Directory.Delete(path, true);
                        }
                }

            }
            grd_DeleteInformationClass.MasterTableView.Dispose();
            grd_DeleteInformationClass.DataSource = dt;
            grd_DeleteInformationClass.DataBind();
        }

        protected void grd_DeleteInformationClass_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();
            List<AssetDTO> DTO = new List<AssetDTO>();
            DTO = assetB.GetAssetListByTerm(cmb_Term.SelectedValue, false);
            DataTable dt = new DataTable();
            dt.Columns.Add("fd", Type.GetType("System.String"));
            dt.Columns.Add("classcode", Type.GetType("System.String"));
            dt.Columns.Add("term", Type.GetType("System.String"));
            //dt.Columns.Add("AssetID", Type.GetType("System.String"));
            dt.Columns.Add("FileSize", Type.GetType("System.String"));
            dt.Columns.Add("FileDate", Type.GetType("System.String"));
            DataRow dtRow = dt.NewRow();
            for (int i = 0; i < DTO.Count; i++)
            {
                string fd = DTO[i].FileDate;
                string classcode = DTO[i].Class_Code.ToString();
                string term = DTO[i].Term.ToString();
                string d = fd.Replace('/', '-');
                string assetId = DTO[i].AssetID.ToString();
                string path = Server.MapPath("../content/" + term + "/" + classcode + "/" + d);


                if (Directory.Exists(path))
                {
                    DirectoryInfo di = new DirectoryInfo(path);
                    // Get a reference to each file in that directory.
                    FileInfo[] fiArr = di.GetFiles("mp3.zip");
                    foreach (FileInfo f in fiArr)
                        if (f.Length < 1000000)
                        {
                          
                            dtRow["fd"] = d.ToString();
                            dtRow["classcode"] = classcode.ToString();
                            dtRow["term"] = term.ToString();
                            //dtRow["AssetID"] = assetId.ToString();
                            dtRow["FileSize"] = Math.Ceiling(decimal.Parse(f.Length.ToString()) / 1024);
                            dtRow["FileDate"] = DTO[i].FileDate;
                            dt.Rows.Add(dtRow);
                            dtRow = dt.NewRow();
                            //System.IO.Directory.Delete(path, true);
                        }
                }

            }
            grd_DeleteInformationClass.DataSource = dt;
       
        }

        protected void grd_NoZipFile_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();
            List<AssetDTO> DTO = new List<AssetDTO>();
            DTO = assetB.GetAssetListByTerm(cmb_Term.SelectedValue, false);
            DataTable dtmp3 = new DataTable();
            dtmp3.Columns.Add("fd", Type.GetType("System.String"));
            dtmp3.Columns.Add("classcode", Type.GetType("System.String"));
            dtmp3.Columns.Add("term", Type.GetType("System.String"));
            //dtmp3.Columns.Add("AssetID", Type.GetType("System.String"));
            dtmp3.Columns.Add("FileSize", Type.GetType("System.String"));
            dtmp3.Columns.Add("FileDate", Type.GetType("System.String"));
            DataRow dtmp3row = dtmp3.NewRow();
            for (int i = 0; i < DTO.Count; i++)
            {
                string fd = DTO[i].FileDate;
                string classcode = DTO[i].Class_Code.ToString();
                string term = DTO[i].Term.ToString();
                string d = fd.Replace('/', '-');
                string assetId = DTO[i].AssetID.ToString();
                string path = Server.MapPath("../content/" + term + "/" + classcode + "/" + d);


                if (Directory.Exists(path))
                {
                    DirectoryInfo di = new DirectoryInfo(path);
                    // Get a reference to each file in that directory.
                    
                    FileInfo[] fiArrmp3 = di.GetFiles("*.mp3");
                    foreach (FileInfo f in fiArrmp3)
                        {
                            dtmp3row["fd"] = d.ToString();
                            dtmp3row["classcode"] = classcode.ToString();
                            dtmp3row["term"] = term.ToString();
                            //dtmp3row["AssetID"] = assetId.ToString();
                            dtmp3row["FileSize"] = Math.Ceiling(decimal.Parse(f.Length.ToString()) / 1024);
                            dtmp3row["FileDate"] = DTO[i].FileDate;
                            dtmp3.Rows.Add(dtmp3row);
                            dtmp3row = dtmp3.NewRow();
                            //System.IO.Directory.Delete(path, true);
                        }
                }
                grd_NoZipFile.DataSource = dtmp3;
            }
        }

        protected void btn_del_Click(object sender, EventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();
            foreach (GridDataItem grd in grd_NoZipFile.Items)
            {
                TableCell fd = (TableCell)grd["fd"];
                TableCell classcode = (TableCell)grd["classcode"];
                TableCell term = (TableCell)grd["term"];
                CheckBox check = (CheckBox)grd.FindControl("chk0");
                if (check.Checked)
                {
                    string d = fd.Text.ToString().Replace('/', '-');
                    string path = Server.MapPath("../content/" + term.Text + "/" + classcode.Text + "/" + d);
                    System.IO.Directory.Delete(path, true);
                    cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), (int)IAUEC_Apps.DTO.eventEnum.حذف_فایل_های_0_KB, classcode.Text + " - " + fd.Text);
                }

            }
            foreach (GridDataItem item in grd_NoZipFile.MasterTableView.Items)
            {
                CheckBox check = (CheckBox)item.FindControl("chk0");
                if (!check.Checked)
                    item.Visible = false;
            }

            grd_NoZipFile.ExportSettings.FileName = "Last-file-Deleted-" + DateTime.Now.ToString();
            grd_NoZipFile.MasterTableView.ExportToExcel();

        }

        protected void btn_AddFailedList_Click(object sender, EventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();
            foreach (GridDataItem grd in grd_NoZipFile.Items)
            {
                HiddenField fd = (HiddenField)grd.FindControl("filedate");
                TableCell classcode = (TableCell)grd["classcode"];
                TableCell term = (TableCell)grd["term"];
                CheckBox check = (CheckBox)grd.FindControl("chk0");
                if (check.Checked)
                {
                    assetB.set_archive(classcode.Text, fd.Value, term.Text, true);
                    cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), (int)IAUEC_Apps.DTO.eventEnum.حذف_فایل_های_0_KB, classcode.Text + " - " + fd.Value);
                }

            }
            List<AssetDTO> DTO = new List<AssetDTO>();
            DTO = assetB.GetAssetListByTerm(cmb_Term.SelectedValue, false);
            DataTable dtmp3 = new DataTable();
            dtmp3.Columns.Add("fd", Type.GetType("System.String"));
            dtmp3.Columns.Add("classcode", Type.GetType("System.String"));
            dtmp3.Columns.Add("term", Type.GetType("System.String"));
            //dtmp3.Columns.Add("AssetID", Type.GetType("System.String"));
            dtmp3.Columns.Add("FileSize", Type.GetType("System.String"));
            dtmp3.Columns.Add("FileDate", Type.GetType("System.String"));
            DataRow dtmp3row = dtmp3.NewRow();
            for (int i = 0; i < DTO.Count; i++)
            {
                string fd = DTO[i].FileDate;
                string classcode = DTO[i].Class_Code.ToString();
                string term = DTO[i].Term.ToString();
                string d = fd.Replace('/', '-');
                string assetId = DTO[i].AssetID.ToString();
                string path = Server.MapPath("../content/" + term + "/" + classcode + "/" + d);


                if (Directory.Exists(path))
                {
                    DirectoryInfo di = new DirectoryInfo(path);
                    // Get a reference to each file in that directory.

                    FileInfo[] fiArrmp3 = di.GetFiles("*.mp3");
                    foreach (FileInfo f in fiArrmp3)
                        {
                            dtmp3row["fd"] = d.ToString();
                            dtmp3row["classcode"] = classcode.ToString();
                            dtmp3row["term"] = term.ToString();
                            //dtmp3row["AssetID"] = assetId.ToString();
                            dtmp3row["FileSize"] = Math.Ceiling(decimal.Parse(f.Length.ToString()) / 1024);
                            dtmp3row["FileDate"] = DTO[i].FileDate;
                            dtmp3.Rows.Add(dtmp3row);
                            dtmp3row = dtmp3.NewRow();
                            //System.IO.Directory.Delete(path, true);
                        }
                }
                grd_NoZipFile.DataSource = dtmp3;
            }
        }

       
       
    }
}