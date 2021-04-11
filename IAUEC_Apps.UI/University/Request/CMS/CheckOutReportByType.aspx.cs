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
    public partial class CheckOutReportByType : System.Web.UI.Page
    {
        CheckOutReportByTypeBusiness business = new CheckOutReportByTypeBusiness();
        DataTable dt = new DataTable();
        int isOnline;
        int reqType;
        string sDate;
        string eDate;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            setData();
            string sDate = txtSdate.Text;
            string eDate = txtEdate.Text;
            sDate = sDate.Substring(2, sDate.Length - 2);
            eDate = eDate.Substring(2, eDate.Length - 2);
            dt = business.getAllRequestBytype(isOnline, reqType, sDate, eDate);
            bindGrid();
        }

        protected void grdResult_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            setData();
            if (eDate != string.Empty && eDate != string.Empty)
            {
                sDate = sDate.Substring(2, sDate.Length - 2);
                eDate = eDate.Substring(2, eDate.Length - 2);
                dt = business.getAllRequestBytype(isOnline, reqType, sDate, eDate);

                grdResult.DataSource = dt;
            }
        }

        private void setData()
        {
            if (cmbStatus.SelectedValue == "1")
            {
                isOnline = 1;
            }
            else if (cmbStatus.SelectedValue == "2")
            {
                isOnline = 0;
            }
            else
            {
                isOnline = -1;//all request
            }

            if (cmbType.SelectedValue == "1")
            {
                reqType = 15;
            }
            else if (cmbType.SelectedValue == "2")
            {
                reqType = 13;
            }
            else if (cmbType.SelectedValue == "3")
            {
                reqType = 16;
            }
            else if (cmbType.SelectedValue == "4")
            {
                reqType = 14;
            }
            else
            {
                reqType = -1;
            }

            if (cmbStatus.SelectedValue == "3")
            {
                grdResult.MasterTableView.GetColumn("status").Display = true;
            }
            else
            {
                grdResult.MasterTableView.GetColumn("status").Display = false;
            }

            sDate = txtSdate.Text;
            eDate = txtEdate.Text;
        }
        private void bindGrid()
        {
            grdResult.DataSource = dt;
            grdResult.DataBind();

            if (dt.Rows.Count == 0)
            {
                btnExcel.Visible = false;
            }
            else
            {
                btnExcel.Visible = true;
            }

            GridFilterMenu menu = grdResult.FilterMenu;
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
                foreach (RadMenuItem item in menu.Items)
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
            grdResult.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), "ExcelML");
            grdResult.ExportSettings.IgnorePaging = false;
            grdResult.ExportSettings.ExportOnlyData = true;
            grdResult.ExportSettings.OpenInNewWindow = true;
            grdResult.ExportSettings.UseItemStyles = true;
            grdResult.MasterTableView.ExportToExcel();
        }

    }
}