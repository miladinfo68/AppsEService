using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class InsertRejectDescription : System.Web.UI.Page
    {
        RequestStudentCartBusiness CartBusiness = new RequestStudentCartBusiness();
        RequestGovahiBusiness GovahiBusiness = new RequestGovahiBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Reject_Click(object sender, EventArgs e)
        {
            CommonBusiness cmnb = new CommonBusiness();

            string parameters = Request.QueryString["params"];
            string[] p = parameters.Split(new char[] { ',' });
            Session["stcode"] = p[0];
            Session["StudentRequestID"] = p[1].ToString();
            CartBusiness.UpdateStudentRequestLogID(Session["stcode"].ToString(), 26, 3, int.Parse(Session["StudentRequestID"].ToString()));
            GovahiBusiness.UpdateStudentPOstNumber(Session["stcode"].ToString(), txt_Rad.Text, 3, int.Parse(Session["StudentRequestID"].ToString()));
            cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 31, Session["stcode"].ToString());
            ScriptManager.RegisterStartupScript(this, GetType(), "btn_RejectRequest", "CloseModal();", true);
        }
    }
}