using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Request;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class RejectImageEditReport : System.Web.UI.Page
    {
        EditPersonalInformationBusiness Ebusiness = new EditPersonalInformationBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void img_ExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            grd_RejectEditImage.ExportSettings.FileName = "RejectedEditImage";
            grd_RejectEditImage.ExportSettings.IgnorePaging = true;
            grd_RejectEditImage.MasterTableView.ExportToExcel();
        }

        protected void grd_RejectEditImage_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = Ebusiness.GetReportEditImageRequest(5);
            if (dt.Rows.Count > 0)
            {
                grd_RejectEditImage.DataSource = dt;
                img_ExportToExcel.Visible = true;

            }
            else
            {
                grd_RejectEditImage.Visible = false;
                img_ExportToExcel.Visible = false;
            }

        }
    }
}