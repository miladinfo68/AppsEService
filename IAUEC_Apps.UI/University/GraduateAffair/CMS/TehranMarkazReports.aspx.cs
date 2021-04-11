using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Excel = Microsoft.Office.Interop.Excel;
using IAUEC_Apps.Business.university.GraduateAffair;
using System.Data;
using System.Text;
using System.Drawing;
using IAUEC_Apps.Business.Common;
using System.IO;
using Telerik.Web.UI;
using System.Globalization;
using IAUEC_Apps.Business.university.Request;

namespace IAUEC_Apps.UI.University.GraduateAffair.CMS
{
    public static class UtilityFunction
    {
        public static DateTime StringPersianDateToGerogorianDate(this string stringDate)
        {
            if (!string.IsNullOrWhiteSpace(stringDate))
            {
                var splitStringDate = stringDate.Split('/');
                return new DateTime(Convert.ToInt32(splitStringDate[0]), Convert.ToInt32(splitStringDate[1]), Convert.ToInt32(splitStringDate[2]), new PersianCalendar());
            }
            return new DateTime();
        }
        public static string VahedSodoor(this string idVahed)
        {
            CheckOutRequestBusiness reqBusiness = new CheckOutRequestBusiness();
            if (!string.IsNullOrEmpty(idVahed))
            {
                var dt = reqBusiness.GetNameOfVahedByVahedID(Convert.ToInt32(idVahed));
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["vahed"].ToString() != "Null")
                    {
                        return dt.Rows[0]["vahed"].ToString();
                    }

                }
            }

            return "--";



        }


    }
    public partial class TehranMarkazReports : System.Web.UI.Page
    {
        private Excel.Application _app;
        private Excel.Workbooks _books;
        private Excel.Workbook _book;
        protected Excel.Sheets _sheets;
        protected Excel.Worksheet _sheet;
        GraduateReportBusiness GraduateBusiness = new GraduateReportBusiness();
        CommonBusiness cmb = new CommonBusiness();

        //protected void Page_PreRender(object sender, EventArgs e)
        //{
        //    ScriptManager sm = ScriptManager.GetCurrent(Page);
        //    sm.RegisterScriptControl(grd_view);
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grd_view.DataSource = GraduateBusiness.GetTehranMarkaz();
                grd_view.DataBind();
            }
        }

        protected void CustomFilterBtn_Click(object sender, EventArgs e)
        {
            var startDate = pcal1.Text.StringPersianDateToGerogorianDate();
            var endDate = pcal2.Text.StringPersianDateToGerogorianDate();

            var results = GraduateBusiness.GetTehranMarkaz()
                .AsEnumerable()
                .Where(x => x.Field<string>("ConvertDate").StringPersianDateToGerogorianDate() >= startDate && x.Field<string>("ConvertDate").StringPersianDateToGerogorianDate() <= endDate).ToList();
            grd_view.DataSource = results;
            grd_view.DataBind();

        }






        protected void grd_view_HTMLExporting(object sender, GridHTMLExportingEventArgs e)
        {
            //  e.Styles.Append("@page table .employeeColumn { background-color: #d3d3d3; }");
        }

        protected void grd_view_ItemCreated(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem || e.Item is GridHeaderItem)
            //{
            //    e.Item.Cells[2].CssClass = "employeeColumn";
            //}
        }

        protected void imgBtnExcel_Click(object sender, ImageClickEventArgs e)
        {
            string alternateText = (sender as ImageButton).AlternateText;
            //#region [ XSLX FORMAT ]
            //if (alternateText == "Xlsx" && CheckBox2.Checked)
            //{
            //    grd_view.MasterTableView.GetColumn("EmployeeID").HeaderStyle.BackColor = Color.LightGray;
            //    grd_view.MasterTableView.GetColumn("EmployeeID").ItemStyle.BackColor = Color.LightGray;
            //}
            //#endregion
            //grd_view.ExportSettings.Excel.Format = GridExcelExportFormat.Xlsx;//(GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), alternateText);
            grd_view.ExportSettings.IgnorePaging = true;
            grd_view.ExportSettings.ExportOnlyData = true;
            //grd_view.ExportSettings.OpenInNewWindow = true;

            grd_view.MasterTableView.ExportToExcel();




        }

        protected void grd_view_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grd_view.DataSource = GraduateBusiness.GetTehranMarkaz();

        }

    }
}