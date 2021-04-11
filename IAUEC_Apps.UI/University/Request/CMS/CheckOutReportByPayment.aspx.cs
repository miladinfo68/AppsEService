using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using IAUEC_Apps.Business.university.Request;
using xi = Telerik.Web.UI.ExportInfrastructure;
using Telerik.Web.UI.GridExcelBuilder;
using System.Drawing;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class CheckOutReportByPayment : System.Web.UI.Page
    {
        CheckOutReportByTypeBusiness business = new CheckOutReportByTypeBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                checkAccess();
                IAUEC_Apps.UI.Helper.CustomHelper.FilterPersian(grdResult);
                checkRollbackPayments();
            }
        }
        private void checkRollbackPayments()
        {
            RequestPaymentBusiness Pb = new RequestPaymentBusiness();
            Pb.checkRollbackPayments(DTO.University.Request.PayType.stamp);
        }

        private void checkAccess()
        {
            string mId = Request.QueryString["id"].ToString();
            string[] id = mId.ToString().Split(new char[] { '@' });
            string menuId = "";
            for (int i = 0; i < id[1].Length; i++)
            {
                string s = id[1].Substring(i + 1, 1);
                if (s != "-")
                    menuId += s;
                else
                    break;
                Session[sessionNames.menuID] = menuId;
            }
            AccessControl.MenuId = Session[sessionNames.menuID].ToString();
            AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindGrid();
            grdResult.DataBind();

        }

        protected void grdResult_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (txtEdate.Text != string.Empty && txtSdate.Text != string.Empty)
            {
                bindGrid();
            }
            IAUEC_Apps.UI.Helper.CustomHelper.ExcuteFilter(grdResult);
        }

        private void bindGrid()
        {
            var dt = business.GetAllRequestByPayment(txtSdate.Text, txtEdate.Text);
            grdResult.DataSource = dt;
            btnExcel.Visible = dt.Rows.Count > 0;
        }

        protected void grdResult_ExcelMLWorkBookCreated(object sender, Telerik.Web.UI.GridExcelBuilder.GridExcelMLWorkBookCreatedEventArgs e)
        {
            int r = 0;
            foreach (RowElement row in e.WorkBook.Worksheets[0].Table.Rows)
            {


                for (int i = 0; i < row.Cells.Count; i++)
                {
                    if (r != 0)
                    {
                        if (r % 2 == 0)
                            row.Cells[i].StyleValue = "Style1";
                        else
                            row.Cells[i].StyleValue = "Style2";
                    }
                    else
                        row.Cells[i].StyleValue = "styleHeader";
                }
                r++;

            }

            StyleElement styleHeader = new StyleElement("styleHeader");
            styleHeader.InteriorStyle.Pattern = InteriorPatternType.Solid;
            styleHeader.InteriorStyle.Color = System.Drawing.Color.White;
            styleHeader.FontStyle.FontName = "Tahoma";
            styleHeader.FontStyle.Bold = true;
            styleHeader.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Center;
            e.WorkBook.Styles.Add(styleHeader);
            StyleElement style = new StyleElement("Style1");
            style.InteriorStyle.Pattern = InteriorPatternType.Solid;
            style.InteriorStyle.Color = System.Drawing.Color.FromArgb(162, 226, 255);
            style.FontStyle.FontName = "Tahoma";
            style.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Center;
            e.WorkBook.Styles.Add(style);
            StyleElement style2 = new StyleElement("Style2");
            style2.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Center;
            style2.InteriorStyle.Pattern = InteriorPatternType.Solid;
            style2.InteriorStyle.Color = System.Drawing.Color.FromArgb(217, 243, 255);
            style2.FontStyle.FontName = "Tahoma";
            e.WorkBook.Styles.Add(style2);
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            grdResult.DataSource = IAUEC_Apps.UI.Helper.CustomHelper.ConvertDataGridToDataTable(grdResult);
            grdResult.DataBind();
            grdResult.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), "ExcelML");
            grdResult.ExportSettings.IgnorePaging = false;
            grdResult.ExportSettings.ExportOnlyData = true;
            grdResult.ExportSettings.OpenInNewWindow = true;
            grdResult.ExportSettings.UseItemStyles = true;
            grdResult.MasterTableView.ExportToExcel();
        }

    }
}