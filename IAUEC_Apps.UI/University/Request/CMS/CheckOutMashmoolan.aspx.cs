using IAUEC_Apps.Business.university.Request;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class CheckOutMashmoolan : System.Web.UI.Page
    {
        CheckOutMashmoolanBusiness _mahmoolanB = new CheckOutMashmoolanBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            DataTable dtRequest = _mahmoolanB.GetMashmoolFareghOk();
            grdRequestList.DataSource = dtRequest;
            grdRequestList.DataBind();
        }

        protected void grdRequestList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName=="print")
            {
                int reqId = Convert.ToInt32(e.CommandArgument);
                bool flag =_mahmoolanB.UpdateMashmoolStatusByReqId(reqId);
                BindGrid();
            }
        }
    }
}