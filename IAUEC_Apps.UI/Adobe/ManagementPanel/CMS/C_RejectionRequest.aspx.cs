using IAUEC_Apps.Business.Adobe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.Adobe.ManagementPanel.CMS
{
    public partial class C_RejectionRequest : System.Web.UI.Page
    {
        ManagementPanelBusiness MPB = new ManagementPanelBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {           
            if(!IsPostBack)
            {          
                if (HttpUtility.ParseQueryString(this.ClientQueryString).Get("Id") != null)                
                    lbl_Id.Text = HttpUtility.ParseQueryString(this.ClientQueryString).Get("Id");                    
                else
                    Response.Redirect("C_Request.aspx");
            }
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("C_Request.aspx");
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            if(txt_Detail.Text=="")
                RadWindowManager1.RadAlert("لطفا دلایل عدم پذیرش درخواست را بیان کنید", 500, 200, "پیام", "");
            else
            {
                MPB.Customers_ClassName_RejectReason(int.Parse(lbl_Id.Text), txt_Detail.Text);// دلیل رد درخواست
                MPB.Update_Customers_ClassName_Rejection(-1, int.Parse(lbl_Id.Text));// رد درخواست

                Response.Redirect("C_Request.aspx");
            }
        }




    }
}