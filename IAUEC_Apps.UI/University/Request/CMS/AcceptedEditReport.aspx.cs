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
    public partial class AcceptedEditReport : System.Web.UI.Page
    {
        EditPersonalInformationBusiness EBusiness = new EditPersonalInformationBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void img_ExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            grd_AcceptEdit.ExportSettings.FileName = "AcceptEditReport";
            grd_AcceptEdit.ExportSettings.IgnorePaging = true;
            grd_AcceptEdit.MasterTableView.ExportToExcel();
        }

        protected void grd_AcceptEdit_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dtrej = new DataTable();
            dtrej = EBusiness.GetReportEditRequest(7);
            if (dtrej.Rows.Count > 0)
            {
                grd_AcceptEdit.DataSource = dtrej;
                img_ExportToExcel.Visible = true;
            }
            else
            {
                grd_AcceptEdit.Visible = false;
                img_ExportToExcel.Visible = false;
            }
        }
    }
}