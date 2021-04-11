using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.Business.Common;
using Telerik.Web.UI;
using IAUEC_Apps.DTO.AdobeClasses;

namespace IAUEC_Apps.UI.Adobe.Pages
{
    public partial class Archive : System.Web.UI.Page
    {
        CommonBusiness cmb = new CommonBusiness();
        AssetBusiness assetB = new AssetBusiness();
        ClassBusiness clsB = new ClassBusiness();
        DownloadRequestDTO dnlreqDTO = new DownloadRequestDTO();
        DownloadRequestBusiness dnlreq = new DownloadRequestBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                rcbTerm.DataTextField = "tterm";
                rcbTerm.DataValueField = "tterm";
                rcbTerm.DataSource = cmb.GetAllTermByStudent(Session[sessionNames.userID_StudentOstad].ToString());
                rcbTerm.DataBind();
                rcbTerm.Items.Insert(0, new RadComboBoxItem("انتخاب نمایید"));
                

            }

        }

        protected void rcbTerm_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadListView2.DataSource = clsB.Show_Class_List(Session[sessionNames.userID_StudentOstad].ToString(), rcbTerm.SelectedValue);
            RadListView2.DataBind();
            foreach (RadListViewItem lvi in RadListView2.Items)
            {
                RadListView lstAsset = (RadListView)lvi.FindControl("RadListView3");
                Label ClassCode = (Label)lvi.FindControl("ClassCode");
                lstAsset.DataSource = assetB.Show_Asset_List_ByClassCode(ClassCode.Text, rcbTerm.SelectedValue);
                lstAsset.DataBind();
                if (RadListView2.Items.Count>0)
                {
                    btn_Select.Visible = true;
                }
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
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
                            lblstcode.Text = Session[sessionNames.userID_StudentOstad].ToString();
                            dnlreqDTO.StCode = lblstcode.Text;
                            dnlreqDTO.Class_Code = ClassCode.Text;
                            dnlreqDTO.Link_Click = false;
                           // dnlreqDTO.Term = rcbTerm.SelectedValue.ToString();
                            dnlreq.Create_DownloadRequest(dnlreqDTO);
                            cmnb.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_StudentOstad].ToString()), 2, ClassCode.Text + "-" + lbl_FDate.Text);
                            counter++;
                            Session[sessionNames.userID_StudentOstad] = lblstcode.Text;

                        }
                    }

                }

                Response.Redirect("ConfirmCheckList.aspx");
            }
        }
    }
}