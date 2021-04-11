using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAUEC_Apps.Business.university.Request;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class RejectedGovahiReport : System.Web.UI.Page
    {
        RequestGovahiBusiness GBusiness = new RequestGovahiBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void img_ExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            grd_RejectGovahi.ExportSettings.FileName = "RejectGovahiReport";
            grd_RejectGovahi.ExportSettings.IgnorePaging = true;
            grd_RejectGovahi.MasterTableView.ExportToExcel();
        }



        protected void grd_RejectGovahi_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtgov = new DataTable();
            dtgov = GBusiness.GetGovahiReport(5);
            var dtgov1 = GBusiness.GetGovahiReport(26);
            dtgov.Merge(dtgov1);
            if (dtgov.Rows.Count > 0)
            {
                grd_RejectGovahi.DataSource = dtgov;
                img_ExportToExcel.Visible = true;

            }
            else
            {
                grd_RejectGovahi.Visible = false;
                img_ExportToExcel.Visible = false;
            }

        }
    }
}