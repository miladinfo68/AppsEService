using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Adobe;
using Telerik.Web.UI;
using System.IO;
using System.Drawing;
using System.Configuration;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class ShowFiles : System.Web.UI.Page
    {
        CommonBusiness cmb = new CommonBusiness();
        AssetBusiness assetB = new AssetBusiness();
        ClassBusiness clsB = new ClassBusiness();
        protected CommonUI.AccessControl AccessControl1;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                rcb_Term.DataTextField = "Term";
                rcb_Term.DataValueField = "Term";
                rcb_Term.DataSource = cmb.getActiveTerm_AdobeConnection();
                rcb_Term.DataBind();
                rcb_Term.Items.Insert(0, new RadComboBoxItem("انتخاب نمایید"));
                string termSelected;
                if (rcb_Term.SelectedIndex == 0)
                {
                    termSelected = ConfigurationManager.AppSettings["Term"].ToString();
                }
                else
                {
                    termSelected = rcb_Term.SelectedValue;
                }
                grd_ShowFile.DataSource = assetB.GetAllAssetListByTerm(termSelected,false);
                grd_ShowFile.DataBind();
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
               Session[sessionNames.menuID] = menuId.ToString();
            }
           
            AccessControl1 = (CommonUI.AccessControl)LoadControl("../../CommonUI/AccessControl.ascx");
            Page.Controls.Add(AccessControl1);
            AccessControl1.MenuId = Session[sessionNames.menuID].ToString();
            AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
           
        }

        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            string termSelected;
            if (rcb_Term.SelectedIndex == 0)
            {
                termSelected = ConfigurationManager.AppSettings["Term"].ToString();
            }
            else
            {
                termSelected = rcb_Term.SelectedValue;
            }
            grd_ShowFile.DataSource = assetB.GetAllAssetListByTerm(termSelected,false);
            //AccessControl1.MenuId = Session["MenuId"].ToString();
            //AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
            //AccessControl1 = (CommonUI.AccessControl)LoadControl("../../CommonUI/AccessControl.ascx");
            //Page.Controls.Add(AccessControl1);
          
        }

        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            Label lblsize = (Label)e.Item.FindControl("lbl_FileSize");
            if (e.Item is GridDataItem)
            {

                GridDataItem itemAmount = (GridDataItem)e.Item;
                TableCell fd = (TableCell)itemAmount["FileDate"];
                TableCell classcode = (TableCell)itemAmount["Class_Code"];
                TableCell term = (TableCell)itemAmount["Term"];
                Button btn_flv =(Button)e.Item.FindControl("btn_Download_flv");
                Button btn_mp3 = (Button)e.Item.FindControl("btn_Download_mp3");
                Button btn_avi = (Button)e.Item.FindControl("btn_Download_avi");

                string d = fd.Text.Replace('/', '-');
                 string path = Server.MapPath("../content/" + term.Text + "/" + classcode.Text + "/" + d);
                 if (Directory.Exists(path) == false)
                 {
                     fd.ForeColor = Color.Red;
                     btn_flv.Enabled = false;
                     btn_mp3.Enabled = false;
                     btn_avi.Enabled = false;
                     btn_flv.BackColor = Color.Wheat;
                     btn_flv.ForeColor = Color.Black;
                     btn_mp3.BackColor = Color.Wheat;
                     btn_mp3.ForeColor = Color.Black;
                     btn_avi.BackColor=Color.Wheat;
                     btn_avi.ForeColor = Color.Black;
                 }
                 else
                 {
                     
                    
                     DirectoryInfo di = new DirectoryInfo(path);
                     // Get a reference to each file in that directory.
                     FileInfo[] fiArr = di.GetFiles("mp3.zip");              
                     if (fiArr.Count()>0)
                     {
                         btn_mp3.BackColor = Color.DarkSeaGreen;
                         btn_mp3.ForeColor = Color.White;
                         foreach (FileInfo f in fiArr)
                             lblsize.Text = " mp3:" + f.Length / 1024;

                     }
                     else
                     {
                         btn_mp3.BackColor = Color.Wheat;
                         btn_mp3.ForeColor = Color.Black;
                         btn_mp3.Enabled = false;
                     }
                   
                     FileInfo[] fiArrf = di.GetFiles("flv.zip");
                     if (fiArrf.Count()>0)
                     {
                         btn_flv.BackColor = Color.DarkSeaGreen;
                         btn_flv.ForeColor = Color.White;
                         foreach (FileInfo f in fiArrf)
                             lblsize.Text += "--flv:" + f.Length / 1024;
                    
                     }
                     else
                     {
                         btn_flv.BackColor = Color.Wheat;
                         btn_flv.ForeColor = Color.Black;
                         btn_flv.Enabled = false;
                     }
                     FileInfo[] fiArrff = di.GetFiles("avi.zip");
                     if (fiArrff.Count()>0)
                     {
                         btn_avi.BackColor = Color.DarkSeaGreen;
                         btn_avi.ForeColor = Color.White;
                         foreach (FileInfo f in fiArrff)
                             lblsize.Text += "--avi:" + f.Length / 1024;
                     }
                     else
                     {
                         btn_avi.BackColor = Color.Wheat;
                         btn_avi.ForeColor = Color.Black;
                         btn_avi.Enabled = false;
                     }
                    
                    
                 }
            }
        }

      

        protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();
          
            if (e.CommandName == "flv")
            {
                GridDataItem itemAmount = (GridDataItem)e.Item;
                TableCell fd = (TableCell)itemAmount["FileDate"];
                TableCell classcode = (TableCell)itemAmount["Class_Code"];
                TableCell term = (TableCell)itemAmount["Term"];
                string d = fd.Text.Replace('/', '-');
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 24, "");

                SendFileToUser(Server.MapPath("../content/" + term.Text + "/" + classcode.Text + "/" + d + "/" + "flv.zip"));

            }
            
            if (e.CommandName == "mp3")
            {
                GridDataItem itemAmount = (GridDataItem)e.Item;
                TableCell fd = (TableCell)itemAmount["FileDate"];
                TableCell classcode = (TableCell)itemAmount["Class_Code"];
                TableCell term = (TableCell)itemAmount["Term"];
                string d = fd.Text.Replace('/', '-');
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 23, "");

                SendFileToUser(Server.MapPath("../content/" + term.Text + "/" + classcode.Text + "/" + d + "/" + "mp3.zip"));


            }
            if (e.CommandName == "avi")
            {
                GridDataItem itemAmount = (GridDataItem)e.Item;
                TableCell fd = (TableCell)itemAmount["FileDate"];
                TableCell classcode = (TableCell)itemAmount["Class_Code"];
                TableCell term = (TableCell)itemAmount["Term"];
                string d = fd.Text.Replace('/', '-');
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 25, "");

                SendFileToUser(Server.MapPath("../content/" + term.Text + "/" + classcode.Text + "/" + d + "/" + "avi.zip"));


            }
        }

        private void SendFileToUser(string strFileFullPath)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(strFileFullPath);

            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            //Response.ContentType = "application/octet-stream";          
            Response.ContentType = "application/zip";
            Response.WriteFile(file.FullName);
            Response.End();


        }

        protected void rcbTerm_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grd_ShowFile.DataSource = assetB.GetAllAssetListByTerm(rcb_Term.SelectedValue,false);
            grd_ShowFile.DataBind();
        }
    }
}