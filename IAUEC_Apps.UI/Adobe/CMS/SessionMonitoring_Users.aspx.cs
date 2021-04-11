using IAUEC_Apps.Business.Adobe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class SessionMonitoring_Users : System.Web.UI.Page
    {
        SessionMonitoringBusiness SessionMonitoringBusiness = new SessionMonitoringBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                string SCO_ID = HttpUtility.ParseQueryString(this.ClientQueryString).Get("SCO_ID");
                string MeetingName = HttpUtility.ParseQueryString(this.ClientQueryString).Get("NAME");


                if (SCO_ID == null)
                    SCO_ID = "";
                else
                {
                    
                    DataTable OnlineDT = new DataTable();
                    OnlineDT = SessionMonitoringBusiness.GetMeetingOnlineUser(SCO_ID);
                    RadGrid1.DataSource = OnlineDT;
                    RadGrid1.DataBind();
                }

                if (SCO_ID == null)
                    lbl_MeeingName.Text = "نام جلسه: نامشخص";
                else
                    lbl_MeeingName.Text = "نام جلسه: " + MeetingName;

            }
          
        }
    }
}