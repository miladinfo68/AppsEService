using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using IAUEC_Apps.Business.Adobe;
using System.Net;
using System.IO;
using System.Drawing;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.Adobe.Pages
{
    public partial class DownloadFile : System.Web.UI.Page
    {
        CommonBusiness cmnb = new CommonBusiness(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    lbl_User.Text = Session[sessionNames.userID_StudentOstad].ToString();
                    DownloadRequestBusiness dnlB = new DownloadRequestBusiness();
                    lstDownload.DataSource = dnlB.GetValidAssets(lbl_User.Text);
                    lstDownload.DataBind();
                    Session[sessionNames.userID_StudentOstad] = lbl_User.Text;
                }
            }
            catch
            {
            }
        }


        protected void lstDownload_ItemCommand(object sender, RadListViewCommandEventArgs e)
        {
            if (e.CommandName == "dnlbtnFlv")
            {

                     Label lblterm = (Label)e.ListViewItem.FindControl("lbl_Term");
                    Label lblClassCode = (Label)e.ListViewItem.FindControl("lbl_ClassCode");
                    Label lblFileDate = (Label)e.ListViewItem.FindControl("lbl_Date");
                    string fd = lblFileDate.Text.Replace('/', '-');
                    
                   SendFileToUser(Server.MapPath("../content/" + lblterm.Text + "/" + lblClassCode.Text + "/" + fd + "/" + "flv.zip"));
                   cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 5, lblClassCode.Text + "-" + lblFileDate.Text);
                  
            }
            if (e.CommandName == "dnlbtnavi")
            {

                    Label lblterm = (Label)e.ListViewItem.FindControl("lbl_Term");
                    Label lblClassCode = (Label)e.ListViewItem.FindControl("lbl_ClassCode");
                    Label lblFileDate = (Label)e.ListViewItem.FindControl("lbl_Date");
                    string fd = lblFileDate.Text.Replace('/', '-');
                    SendFileToUser(Server.MapPath("../content/" + lblterm.Text + "/" + lblClassCode.Text + "/" + fd + "/" + "avi.zip"));
                    cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 6, lblClassCode.Text + "-" + lblFileDate.Text);     
            }
            if (e.CommandName == "dnlbtnMp3")
            {

                     Label lblterm = (Label)e.ListViewItem.FindControl("lbl_Term");
                    Label lblClassCode = (Label)e.ListViewItem.FindControl("lbl_ClassCode");
                    Label lblFileDate = (Label)e.ListViewItem.FindControl("lbl_Date");
                    string fd = lblFileDate.Text.Replace('/', '-');
             
                   SendFileToUser(Server.MapPath("../content/" + lblterm.Text + "/" + lblClassCode.Text + "/" + fd + "/" + "mp3.zip"));
                   cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 4, lblClassCode.Text + "-" + lblFileDate.Text);                 
                
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

        protected void lstDownload_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            if (e.Item.ItemType == RadListViewItemType.DataItem || e.Item.ItemType == RadListViewItemType.AlternatingItem)
            {
                Label lblterm = (Label)e.Item.FindControl("lbl_Term");
                Label lblClassCode = (Label)e.Item.FindControl("lbl_ClassCode");
                Label lblFileDate = (Label)e.Item.FindControl("lbl_Date");
                Button btnmp3 = (Button)e.Item.FindControl("btn_Download_mp3");
                Button btnflv = (Button)e.Item.FindControl("btn_Download_flv");
                Button btnavi = (Button)e.Item.FindControl("btn_Download_avi");
                string fd = lblFileDate.Text.Replace('/', '-');

                btnflv.BackColor = Color.Wheat;
                btnflv.ForeColor = Color.Black;
                btnavi.BackColor = Color.Wheat;
                btnavi.ForeColor = Color.Black;
                btnmp3.BackColor = Color.Wheat;
                btnmp3.ForeColor = Color.Black;
                if (Directory.Exists(Server.MapPath("../content/" + lblterm.Text + "/" + lblClassCode.Text + "/" + fd + "/")))
                {
                    DirectoryInfo di = new DirectoryInfo(Server.MapPath("../content/" + lblterm.Text + "/" + lblClassCode.Text + "/" + fd + "/"));



                    // Get a reference to each file in that directory.


                    FileInfo[] fiArr = di.GetFiles("flv.zip");
                    if (fiArr.Count() > 0)
                    {
                        foreach (FileInfo f in fiArr)
                            if (f.Length > 10000000)
                            {
                                btnflv.Enabled = true;
                                btnflv.BackColor = Color.DarkSeaGreen;
                                btnflv.ForeColor = Color.White;
                            }
                    }


                    // Get a reference to each file in that directory.
                    FileInfo[] fiArrm = di.GetFiles("mp3.zip");
                    if (fiArrm.Count() > 0)
                    {
                        foreach (FileInfo f in fiArrm)
                            if (f.Length > 1000000)
                            {
                                btnmp3.Enabled = true;
                                btnmp3.BackColor = Color.DarkSeaGreen;
                                btnmp3.ForeColor = Color.White;
                            }
                    }

                    FileInfo[] fiArravi = di.GetFiles("avi.zip");
                    if (fiArravi.Count() > 0)
                    {
                        foreach (FileInfo f in fiArravi)
                            if (f.Length > 5000000)
                            {
                                btnavi.Enabled = true;
                                btnavi.BackColor = Color.DarkSeaGreen;
                                btnavi.ForeColor = Color.White;
                            }
                    }
                }
            }
        }
    }
}