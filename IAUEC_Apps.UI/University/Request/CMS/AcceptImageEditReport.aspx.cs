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
    public partial class AcceptImageEditReport : System.Web.UI.Page
    {
        EditPersonalInformationBusiness Ebusiness = new EditPersonalInformationBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void img_ExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            grd_AcceptEditImage.ExportSettings.FileName = "AcceptEditImageReport";
            grd_AcceptEditImage.ExportSettings.IgnorePaging = true;
            grd_AcceptEditImage.MasterTableView.ExportToExcel();
        }

        protected void grd_AcceptEditImage_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = Ebusiness.GetReportEditImageRequest(7);
            if (dt.Rows.Count > 0)
            {
                grd_AcceptEditImage.DataSource = dt;
                img_ExportToExcel.Visible = true;
            }
            else
            {
                img_ExportToExcel.Visible = false;
            }

        }
    }
}