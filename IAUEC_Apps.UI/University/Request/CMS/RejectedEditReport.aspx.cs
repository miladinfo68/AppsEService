using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Request;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class RejectedEditReport : System.Web.UI.Page
    {
        EditPersonalInformationBusiness EBusiness = new EditPersonalInformationBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void grd_RejectEdit_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtrej = new DataTable();
            dtrej = EBusiness.GetReportEditRequest(5);
            if (dtrej.Rows.Count > 0)
            {
                grd_RejectEdit.DataSource = dtrej;
                img_ExportToExcel.Visible = true;
            }
            else
            {
                img_ExportToExcel.Visible = false;
                grd_RejectEdit.Visible = false;

            }
        }

        protected void img_ExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            
            grd_RejectEdit.ExportSettings.FileName = "RejectEditReport";
            grd_RejectEdit.ExportSettings.IgnorePaging = true;
            grd_RejectEdit.MasterTableView.ExportToExcel();
        }
    }
}