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
    public partial class StCardPaymentReport : System.Web.UI.Page
    {
        RequestGovahiBusiness GBusiness = new RequestGovahiBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            checkRollbackPayments();
        }

        private void checkRollbackPayments()
        {
            RequestPaymentBusiness Pb = new RequestPaymentBusiness();
            Pb.checkRollbackPayments(DTO.University.Request.PayType.studentCard);
            
        }

        protected void img_ExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            grd_PayReport.ExportSettings.FileName = "CardsPaymentReport";
            grd_PayReport.ExportSettings.IgnorePaging = true;
            grd_PayReport.MasterTableView.ExportToExcel();

        }

        protected void grd_PayReport_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dt;
            dt = GBusiness.GetPayment(1);
            if (dt.Rows.Count > 0)
            {
                grd_PayReport.DataSource = dt;
                img_ExportToExcel.Visible = true;
            }
            else
            {
                grd_PayReport.Visible = false;
                img_ExportToExcel.Visible = false;
            }
        }
    }
}