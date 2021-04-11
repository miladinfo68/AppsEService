using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.DTO.University.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class PajooheshReport : System.Web.UI.Page
    {

        DataTable dt = new DataTable();
        FeraghatTahsilBusiness  business = new FeraghatTahsilBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void btnSerach_Click(object sender, EventArgs e)
        {
            DataTable dt = business.GetDateOfDef(txtStCode.Text);
            if (dt.Rows.Count>0)
            {
               // lblnullSt.Visible = false;
                grdStudent.Visible = true;
            grdStudent.DataSource = business.GetDateOfDef(txtStCode.Text);
            grdStudent.DataBind();
            }
          else
            {
                grdStudent.Visible = false;
                // ScriptManager.RegisterStartupScript(this.GetType(), typeof(string), "Alert", "alert('اطلاعات دفاع برای این دانشجو ثبت نشده است .');", true);
                //     ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert(' اطلاعات دفاع برای این دانشجو ثبت نشده است');", true);
                //  System.Windows.Forms.MessageBox.Show("اطلاعات دفاع برای این دانشجو ثبت نشده است");
                var Msg = "اطلاعات دفاع برای این دانشجو ثبت نشده است";
                //  RadWindowManager1.RadAlert(Msg, 300, 100, "پیام", "closeradwindow1", "");
                //  lblnullSt.Visible = true;
                RadWindowManager1.RadAlert(Msg, 300, 150, "پیام سیستم","");
            }



        }
    }
}