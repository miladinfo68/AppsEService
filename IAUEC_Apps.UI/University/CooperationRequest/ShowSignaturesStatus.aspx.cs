using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.CooperationRequest
{
    public partial class ShowSignaturesStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Business.university.CooperationRequest.CooperationRequestBusiness bsn = new Business.university.CooperationRequest.CooperationRequestBusiness();
                grdSignatures.DataSource = bsn.getSignature_Status();
                PersiaFiltering();
            }
        }

        protected void grdSignatures_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            Business.university.CooperationRequest.CooperationRequestBusiness bsn = new Business.university.CooperationRequest.CooperationRequestBusiness();
            grdSignatures.DataSource = bsn.getSignature_Status();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            grdSignatures.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), "ExcelML");
            grdSignatures.ExportSettings.IgnorePaging = true;
            grdSignatures.ExportSettings.ExportOnlyData = true;
            grdSignatures.ExportSettings.OpenInNewWindow = true;
            grdSignatures.ExportSettings.UseItemStyles = true;
            grdSignatures.ExportSettings.FileName = "وضعیت امضای اساتید";
            grdSignatures.MasterTableView.ExportToExcel();
        }

        protected void PersiaFiltering()
        {
            Telerik.Web.UI.GridFilterMenu menu = grdSignatures.FilterMenu;
            if (menu.Items.Count > 3)
            {
                int im = 0;
                while (im < menu.Items.Count)
                {
                    if (menu.Items[im].Text == "NoFilter" || menu.Items[im].Text == "Contains" || menu.Items[im].Text == "EqualTo")
                    {
                        im++;
                    }
                    else
                    {
                        menu.Items.RemoveAt(im);
                    }
                }
                foreach (Telerik.Web.UI.RadMenuItem item in menu.Items)
                {    //change the text for the "StartsWith" menu item  
                    if (item.Text == "NoFilter")
                    {
                        item.Text = "حذف فیلتر";
                        //item.Remove();
                    }
                    if (item.Text == "Contains")
                    {
                        item.Text = "شامل";
                        //item.Remove();
                    }
                    if (item.Text == "EqualTo")
                    {
                        item.Text = "مساوی با";
                        //item.Remove();
                    }

                }
            }
        }
    }
}