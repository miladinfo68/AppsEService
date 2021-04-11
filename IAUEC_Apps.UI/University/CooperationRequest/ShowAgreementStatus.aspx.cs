using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IAUEC_Apps.UI.University.CooperationRequest
{
    public partial class ShowAgreementStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                setGridDatasource();
                grdAgreement.DataBind();
                PersiaFiltering();
            }

        }

        protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            setGridDatasource();
            grdAgreement.DataBind();
        }

        private void setGridDatasource()
        {
                Business.university.CooperationRequest.CooperationRequestBusiness bsn = new Business.university.CooperationRequest.CooperationRequestBusiness();
                DataTable dt = bsn.getAllAgreement_Status();
                //if (dt.Rows.Count > 0)
                //{
                    grdAgreement.DataSource = dt;
                //}
            
        }

        protected void PersiaFiltering()
        {
            Telerik.Web.UI.GridFilterMenu menu = grdAgreement.FilterMenu;
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

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            grdAgreement.ExportSettings.Excel.Format = (Telerik.Web.UI.GridExcelExportFormat)Enum.Parse(typeof(Telerik.Web.UI.GridExcelExportFormat), "ExcelML");
            grdAgreement.ExportSettings.IgnorePaging = true;
            grdAgreement.ExportSettings.ExportOnlyData = true;
            grdAgreement.ExportSettings.OpenInNewWindow = true;
            grdAgreement.ExportSettings.UseItemStyles = true;
            grdAgreement.ExportSettings.FileName = "وضعیت تفاهم نامه اساتید";
            grdAgreement.MasterTableView.ExportToExcel();
        }

        protected void grdAgreement_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            setGridDatasource();

        }
    }
}