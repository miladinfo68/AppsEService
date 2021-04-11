using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.CooperationRequest
{
    public partial class employmentActionHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            setReportSource();
        }

        private void setReportSource()
        {
            Business.university.CooperationRequest.CooperationRequestBusiness cr = new Business.university.CooperationRequest.CooperationRequestBusiness();
            grdReport.DataSource = cr.getEmploymentActionHistory();
            grdReport.DataBind();
        }
    }
}