using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.Business.Adobe;
using Telerik.Web.UI;
using System.Data;
using System.IO;
using System.Configuration;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Request;

namespace IAUEC_Apps.UI.Adobe.Pages
{
    public partial class ClassList : System.Web.UI.Page
    {
        ClassBusiness clsB = new ClassBusiness();
        AssetBusiness assetB = new AssetBusiness();
        DownloadRequestBusiness dnlreq = new DownloadRequestBusiness();
        DownloadRequestDTO dnlreqDTO = new DownloadRequestDTO();
        RequestGovahiBusiness GovahiBusiness = new RequestGovahiBusiness();
        LoginBusiness logBusiness = new LoginBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                DataTable dtbedehi = new DataTable();
                //double bedehi;
                //dtbedehi = GovahiBusiness.GetBedehkar(Session[sessionNames.userID_StudentOstad].ToString());
                //bedehi = Convert.ToDouble((dtbedehi.Rows[0]["bedehi"].ToString()));
                //DataTable dtReg = new DataTable();
                //dtReg = GovahiBusiness.GetStRegisterd(Session[sessionNames.userID_StudentOstad].ToString());
                //DataTable dtnaghs = new DataTable();
                ////dtnaghs = logBusiness.StHasNaghs(Session["user"].ToString());
                DataTable dt = new DataTable();
                dt = logBusiness.GetStIdVaz(Session[sessionNames.userID_StudentOstad].ToString());

                //if (bedehi > 0)
                //{

                //    rwm_Validations.RadAlert("به علت بدهی شما دسترسی ندارید", null, 100, "خطا", "CallBackConfirm2");

                //}
                // if (dtnaghs.Rows.Count > 0)
                //{

                //    rwm_Validations.RadAlert("به دلیل نقص پرونده شما دسترسی ندارید", null, 100, "خطا", "CallBackConfirm2");

                //}

                //if (dtReg.Rows.Count == 0)
                //{

                //    rwm_Validations.RadAlert("به دلیل عدم ثبت نام در ترم جاری شما دسترسی ندارید", null, 100, "خطا", "CallBackConfirm2");

                //}



                 if (dt.Rows[0]["idvazkol"].ToString() == "7")
                {

                    rwm_Validations.RadAlert("فارغ التحصیلان به این بخش دسترسی ندارند", null, 100, "خطا", "CallBackConfirm2");


                }


                else
                {

                    lblstcode.Text = Session[sessionNames.userID_StudentOstad].ToString();
                    RadListView2.DataSource = clsB.Show_Class_List(lblstcode.Text, ConfigurationManager.AppSettings["Term"].ToString());
                    RadListView2.DataBind();
                  
                    foreach (RadListViewItem lvi in RadListView2.Items)
                    {
                        RadListView lstAsset = (RadListView)lvi.FindControl("RadListView3");
                        Label ClassCode = (Label)lvi.FindControl("ClassCode");
                        lstAsset.DataSource = assetB.Show_Asset_List_ByClassCode((ClassCode.Text), ConfigurationManager.AppSettings["Term"].ToString());
                        lstAsset.DataBind();
                        //var Merge = lvi.FindControl("Merge_code");
                        //if (Merge != null)
                        //    btn_Select.Visible = false;
                        if (RadListView2.Items.Count > 0)
                        {
                            btn_Select.Visible = true;
                        }
                    }

                    Session[sessionNames.userID_StudentOstad] = lblstcode.Text;
                }
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            int counter = 0;
            CommonBusiness cmnb = new CommonBusiness();
            foreach (RadListViewItem lvi in RadListView2.Items)
            {

                RadListView lstAsset = (RadListView)lvi.FindControl("RadListView3");
                foreach (RadListViewItem lviAsset in lstAsset.Items)
                {
                    Label ClassCode = (Label)lviAsset.FindControl("lbl_AssetID");
                    CheckBox chk = (CheckBox)lviAsset.FindControl("chk_MeetingDate");
                    Label lbl_FDate = (Label)lviAsset.FindControl("lbl_FDate");
                    if (chk.Checked)
                    {

                        dnlreqDTO.StCode = lblstcode.Text;
                        dnlreqDTO.Class_Code = (ClassCode.Text);
                        dnlreqDTO.Link_Click = false;
                        dnlreq.Create_DownloadRequest(dnlreqDTO);
                        cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 2, ClassCode.Text + "-" + lbl_FDate.Text);
                        counter++;
                    }
                }

            }

            Response.Redirect("ConfirmCheckList.aspx");
        }
        protected void RadListView3_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            if (e.Item.ItemType == RadListViewItemType.DataItem || e.Item.ItemType == RadListViewItemType.AlternatingItem)
            {
                if (e.Item.OwnerListView.ID == "RadListView3")
                {
                    Label AssetIDlbl = (Label)e.Item.FindControl("lbl_AssetID");
                    DataTable dt = new DataTable();
                    Label lbl_Term = (Label)e.Item.FindControl("lbl_Term");
                    Label lblClassCode = (Label)e.Item.FindControl("lbl_ClassCode");
                    Label lblFileDate = (Label)e.Item.FindControl("lbl_FDate");
                    Label lblmsg = (Label)e.Item.FindControl("lbl_Message");
                    Literal ltr = (Literal)e.Item.FindControl("ltr");
                    //<i id="greeni"  visible="false" runat="server" class="fa fa-circle" style="color:green"></i><i id="yellowi"  visible="false" runat="server" class="fa fa-circle" style="color:yellow"></i>
                    string fd = lblFileDate.Text.Replace('/', '-');
                    CheckBox SelectCHK = (CheckBox)e.Item.FindControl("chk_MeetingDate");
                    // string pathmp3 = Server.MapPath("../content/" + lbl_Term.Text + "/" + lblClassCode.Text + "/" + fd) ;
                    //+ "/" + "mp3.zip");
                    ltr.Text = " <i class='fa fa-circle' style='color:Green'></i>";
                    string pathflv = Server.MapPath("../content/" + lbl_Term.Text + "/" + lblClassCode.Text + "/" + fd);
                    bool viewmp3 = false, viewFlv = false, viewavi = false; ;
                    if (Directory.Exists(pathflv) == false)
                    {
                        lblmsg.Text = pathflv;
                        //lblmsg.Visible = true;
                        ltr.Text = " <i class='fa fa-circle' style='color:red'></i>";
                        SelectCHK.Enabled = false;
                    }
                    else
                    {
                        DirectoryInfo di = new DirectoryInfo(Server.MapPath("../content/" + lbl_Term.Text + "/" + lblClassCode.Text + "/" + fd + "/"));
                        // Get a reference to each file in that directory.
                        FileInfo[] fiArr = di.GetFiles("mp3.zip");
                        foreach (FileInfo f in fiArr)
                            if (f.Length > 1000000)
                            {
                                viewmp3 = true;
                            }

                        // Get a reference to each file in that directory.
                        FileInfo[] fiArrflv = di.GetFiles("flv.zip");
                        foreach (FileInfo f in fiArrflv)
                            if (f.Length > 10000000)
                            {
                                viewFlv = true;
                            }
                        FileInfo[] fiArravi = di.GetFiles("avi.zip");
                        foreach (FileInfo f in fiArravi)
                            if (f.Length > 5000000)
                            {
                                viewavi = true;
                            }

                        if ((viewFlv == false && viewmp3 == false && viewavi == false) || viewmp3 == false)
                        {
                            SelectCHK.Enabled = false;

                            ltr.Text = " <i class='fa fa-circle' style='color:red'></i>";
                        }

                    }
                    dt = dnlreq.Check_PayedAsset(lblstcode.Text, Convert.ToInt32(AssetIDlbl.Text));
                    if (dt.Rows.Count > 0)
                    {

                        ltr.Text = " <i class='fa fa-circle' style='color:#FFDE00'></i>";
                        SelectCHK.Enabled = false;
                    }
                }

            }
        }

        protected void btnOtherClasess_Click(object sender, EventArgs e)
        {


            lblstcode.Text = Session[sessionNames.userID_StudentOstad].ToString();
            LinkButton lnk = sender as LinkButton;
            String ClassCode = lnk.Attributes["CustomParameter"].ToString();
            if ((clsB.Show_similarClass_List(ClassCode, lblstcode.Text)).Count != 0)
            {
                string scrp = "function f(){$find(\"" + RadWindow1.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);


                RadListView1.DataSource = clsB.Show_similarClass_List(ClassCode, lblstcode.Text);
                RadListView1.DataBind();
                foreach (RadListViewItem lvi in RadListView1.Items)
                {
                    RadListView lstAsset = (RadListView)lvi.FindControl("radListView4");
                    Label ClassCode1 = (Label)lvi.FindControl("ClassCode1");
                    var classCode = (ClassCode1.Text);
                    var term = ConfigurationManager.AppSettings["Term"].ToString();
                    lstAsset.DataSource = assetB.Show_Asset_List_ByClassCode(classCode, term);
                    lstAsset.DataBind();
                    if (RadListView1.Items.Count > 0)
                    {
                        btn_Select.Visible = true;
                    }
                }
            }
            else
            {
                rwm_Validations.RadAlert("کلاس مشابه در این ترم وجود ندارد", null, 100, "پیام","");


            }

            Session[sessionNames.userID_StudentOstad] = lblstcode.Text;
        }

        protected void radListView4_ItemDataBound(object sender, RadListViewItemEventArgs e)
        {
            if (e.Item.ItemType == RadListViewItemType.DataItem || e.Item.ItemType == RadListViewItemType.AlternatingItem)
            {
                if (e.Item.OwnerListView.ID == "radListView4")
                {
                    Label AssetIDlbl1 = (Label)e.Item.FindControl("lbl_AssetID1");
                    DataTable dt1 = new DataTable();
                    Label lbl_Term1 = (Label)e.Item.FindControl("lbl_Term1");
                    Label lblClassCode1 = (Label)e.Item.FindControl("lbl_ClassCode1");
                    Label lblFileDate1 = (Label)e.Item.FindControl("lbl_FDate1");
                    Label lblmsg1 = (Label)e.Item.FindControl("lbl_Message1");
                    Literal ltr1 = (Literal)e.Item.FindControl("ltr1");
                    //<i id="greeni"  visible="false" runat="server" class="fa fa-circle" style="color:green"></i><i id="yellowi"  visible="false" runat="server" class="fa fa-circle" style="color:yellow"></i>
                    string fd1 = lblFileDate1.Text.Replace('/', '-');
                    CheckBox SelectCHK1 = (CheckBox)e.Item.FindControl("chk_MeetingDate1");
                    // string pathmp3 = Server.MapPath("../content/" + lbl_Term.Text + "/" + lblClassCode.Text + "/" + fd) ;
                    //+ "/" + "mp3.zip");
                    ltr1.Text = " <i class='fa fa-circle' style='color:Green'></i>";
                    string pathflv = Server.MapPath("../content/" + lbl_Term1.Text + "/" + lblClassCode1.Text + "/" + fd1);
                    bool viewmp3 = false, viewFlv = false, viewavi = false; ;
                    if (Directory.Exists(pathflv) == false)
                    {
                        lblmsg1.Text = pathflv;
                        //lblmsg.Visible = true;
                        ltr1.Text = " <i class='fa fa-circle' style='color:red'></i>";
                        SelectCHK1.Enabled = false;
                    }
                    else
                    {
                        DirectoryInfo di = new DirectoryInfo(Server.MapPath("../content/" + lbl_Term1.Text + "/" + lblClassCode1.Text + "/" + fd1 + "/"));
                        // Get a reference to each file in that directory.
                        FileInfo[] fiArr = di.GetFiles("mp3.zip");
                        foreach (FileInfo f in fiArr)
                            if (f.Length > 1000000)
                            {
                                viewmp3 = true;
                            }

                        // Get a reference to each file in that directory.
                        FileInfo[] fiArrflv = di.GetFiles("flv.zip");
                        foreach (FileInfo f in fiArrflv)
                            if (f.Length > 10000000)
                            {
                                viewFlv = true;
                            }
                        FileInfo[] fiArravi = di.GetFiles("avi.zip");
                        foreach (FileInfo f in fiArravi)
                            if (f.Length > 5000000)
                            {
                                viewavi = true;
                            }

                        if ((viewFlv == false && viewmp3 == false && viewavi == false) || viewmp3 == false)
                        {
                            SelectCHK1.Enabled = false;

                            ltr1.Text = " <i class='fa fa-circle' style='color:red'></i>";
                        }

                    }
                    dt1 = dnlreq.Check_PayedAsset(lblstcode.Text, Convert.ToInt32(AssetIDlbl1.Text));
                    if (dt1.Rows.Count > 0)
                    {

                        ltr1.Text = " <i class='fa fa-circle' style='color:#FFDE00'></i>";
                        SelectCHK1.Enabled = false;
                    }
                }

            }
        }

        protected void btn_select1_Click(object sender, EventArgs e)
        {

            int counter = 0;
            CommonBusiness cmnb = new CommonBusiness();
            foreach (RadListViewItem lvi in RadListView1.Items)
            {

                RadListView lstAsset1 = (RadListView)lvi.FindControl("RadListView4");
                foreach (RadListViewItem lviAsset in lstAsset1.Items)
                {
                    Label ClassCode1 = (Label)lviAsset.FindControl("lbl_AssetID1");
                    CheckBox chk1 = (CheckBox)lviAsset.FindControl("chk_MeetingDate1");
                    Label lbl_FDate1 = (Label)lviAsset.FindControl("lbl_FDate1");
                    if (chk1.Checked)
                    {

                        dnlreqDTO.StCode = lblstcode.Text;
                        dnlreqDTO.Class_Code = (ClassCode1.Text);
                        dnlreqDTO.Link_Click = false;
                        dnlreq.Create_DownloadRequest(dnlreqDTO);
                        cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 2, ClassCode1.Text + "-" + lbl_FDate1.Text);
                        counter++;
                    }
                }

            }

            Response.Redirect("ConfirmCheckList.aspx");




        }
    }
}