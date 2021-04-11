using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.CooperationRequest
{
    public partial class DrawEditedFieldGrid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int reqId = Convert.ToInt32(Request.QueryString["reqId"]);
            Business.university.Request.ProfessorRequestBusiness PRB = new Business.university.Request.ProfessorRequestBusiness();
            System.Data.DataTable source = PRB.getCustomizedProfessorEditedFields(reqId);
            detailTree.DataSource = source;
            detailTree.DataBind();
        }
    }
}