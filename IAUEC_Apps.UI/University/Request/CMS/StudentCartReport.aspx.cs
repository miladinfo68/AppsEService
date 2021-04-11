using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Request;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class StudentCartReport : System.Web.UI.Page
    {
        RequestGovahiBusiness GBusiness = new RequestGovahiBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void grd_CardReport_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtgov = new DataTable();
            dtgov = GBusiness.GetAcceptedGovahiReport(1);
            if (dtgov.Rows.Count > 0)
            {
                grd_CardReport.DataSource = dtgov;
                img_ExportToExcel.Visible = true;

            }
            else
            {
                grd_CardReport.Visible = false;
                img_ExportToExcel.Visible = false;


            }
        }

        protected void img_ExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            grd_CardReport.ExportSettings.FileName = "CartReport";
            grd_CardReport.ExportSettings.IgnorePaging = true;
            grd_CardReport.MasterTableView.ExportToExcel();

        }
    }
}