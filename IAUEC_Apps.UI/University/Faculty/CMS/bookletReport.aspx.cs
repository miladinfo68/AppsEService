using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using Telerik.Web.UI.GridExcelBuilder;
using System.IO;
//using IAUEC_Apps.UI.CommonUI;

namespace IAUEC_Apps.UI.University.Faculty.CMS
{
    public partial class bookletReport : System.Web.UI.Page
    {
        public string EmbedSrc { get; set; } = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_Karbar] == null)
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");

            checkUserAccess();
            if (!IsPostBack)
            {
                setDaneshSource();
                setTerm();
                setResh();
            }
        }
        private void checkUserAccess()
        {
            if (Request.QueryString["id"] == null)
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");

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
            }

            AccessControl.MenuId = menuId;
            Session[sessionNames.menuID] = menuId;
            AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();
        }


        private void setDaneshSource()
        {
            Business.Common.CommonBusiness cb = new Business.Common.CommonBusiness();
            var danesh = cb.SelectAllDaneshkade();
            drpdDanesh.DataSource = danesh;
            drpdDanesh.DataValueField = "id";
            drpdDanesh.DataTextField = "namedanesh";
            drpdDanesh.DataBind();
            ListItem l = new ListItem("تمام دانشکده ها", "0");
            drpdDanesh.Items.Insert(0, l);

        }

        private void setTerm()
        {
            Business.Common.CommonBusiness Term = new Business.Common.CommonBusiness();
            var te = Term.SelectAllTerm();
            drpdTerm.DataSource = te;
            drpdTerm.DataValueField = "tterm";
            drpdTerm.DataTextField = "tterm";
            drpdTerm.DataBind();
        }

        private void setResh()
        {
            Business.Common.CommonBusiness Resh = new Business.Common.CommonBusiness();
            var re = Resh.SelectAllField(Convert.ToInt32(drpdDanesh.SelectedItem.Value));
            drpdResh.DataSource = re;
            drpdResh.DataValueField = "id";
            drpdResh.DataTextField = "nameresh";
            drpdResh.DataBind();
            ListItem l = new ListItem("تمام رشته ها", "0");
            drpdResh.Items.Insert(0, l);
            //DropDownListItem l = new DropDownListItem("انتخاب کنید", "0");
            //drpdResh.Items.Insert(0,l);
            //drp.DataSource = re;
            //drp.DataValueField = "id";
            //drp.DataTextField = "nameresh";
            //drp.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            setBookletSource();
            radGridtarh.DataBind();

        }

        private void setBookletSource()
        {
            string term = drpdTerm.SelectedItem.Value;
            string tarh = drpdTarhdars.SelectedItem.Value;
            string dan = drpdDanesh.SelectedItem.Value;
            string resh = drpdResh.SelectedItem.Value;
            IAUEC_Apps.Business.university.Faculty.FacultyReportsBusiness fb = new Business.university.Faculty.FacultyReportsBusiness();
            DataTable dt = fb.getBooklet(term, tarh == "1", dan, resh);
            radGridtarh.DataSource = dt;
        }

        protected void radGridtarh_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "bookletDownload":

                    IAUEC_Apps.Business.university.Faculty.FacultyReportsBusiness fb = new Business.university.Faculty.FacultyReportsBusiness();
                    var bookletData = fb.getBookletData(Convert.ToInt32(e.CommandArgument));
                    if (bookletData.Rows.Count > 0)
                    {
                        try
                        {
                            Response.ContentType = bookletData.Rows[0]["extention"].ToString();// bookletData.Rows[0]["f_type"].ToString();
                            Response.Clear();
                            Response.AddHeader("content-disposition", "attachment; filename=" + bookletData.Rows[0]["ocode"].ToString() + "_" + bookletData.Rows[0]["dcode"].ToString() + "." + bookletData.Rows[0]["extention"].ToString());
                            Response.BinaryWrite(bookletData.Rows[0]["b_data"] as byte[]);
                            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                            HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    break;
            }
        }

        protected void radGridtarh_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            setBookletSource();
        }

        protected void radGridtarh_DataBinding(object sender, EventArgs e)
        {
            switch (drpdTarhdars.SelectedValue)
            {
                case "0":
                    radGridtarh.Columns[0].Visible = false;
                    radGridtarh.Columns[7].Visible = false;
                    radGridtarh.Columns[8].Visible = false;
                    break;
                case "1":
                    radGridtarh.Columns[0].Visible = true;
                    radGridtarh.Columns[7].Visible = true;
                    radGridtarh.Columns[8].Visible = true;

                    break;
            }
        }

        protected void btnExcelReport_Click(object sender, ImageClickEventArgs e)
        {
            radGridtarh.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), "ExcelML");
            radGridtarh.ExportSettings.FileName = "گزارش طرح درس _ "+drpdTerm.SelectedItem.Value;
            radGridtarh.ExportSettings.IgnorePaging = true;
            radGridtarh.ExportSettings.ExportOnlyData = true;
            radGridtarh.ExportSettings.OpenInNewWindow = true;
            radGridtarh.ExportSettings.UseItemStyles = true;
            radGridtarh.MasterTableView.ExportToExcel();
        }

        protected void radGridtarh_ExcelMLWorkBookCreated(object sender, GridExcelMLWorkBookCreatedEventArgs e)
        {
            makeExcel(e);
        }
        private void makeExcel(Telerik.Web.UI.GridExcelBuilder.GridExcelMLWorkBookCreatedEventArgs e)
        {
            int r = 0;
            foreach (Telerik.Web.UI.GridExcelBuilder.RowElement row in e.WorkBook.Worksheets[0].Table.Rows)
            {


                for (int i = 0; i < row.Cells.Count; i++)
                {
                    if (r != 0)
                    {
                        if (r % 2 == 0)
                            row.Cells[i].StyleValue = "lightBlue";
                        else
                            row.Cells[i].StyleValue = "blue";
                    }
                    else
                        row.Cells[i].StyleValue = "styleHeader";
                }
                r++;

            }
            Telerik.Web.UI.GridExcelBuilder.StyleElement styleHeader = new Telerik.Web.UI.GridExcelBuilder.StyleElement("styleHeader");
            styleHeader.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
            styleHeader.InteriorStyle.Color = System.Drawing.Color.White;
            styleHeader.FontStyle.FontName = "Tahoma";
            styleHeader.FontStyle.Bold = true;
            styleHeader.AlignmentElement.HorizontalAlignment = Telerik.Web.UI.GridExcelBuilder.HorizontalAlignmentType.Center;
            e.WorkBook.Styles.Add(styleHeader);
            Telerik.Web.UI.GridExcelBuilder.StyleElement style = new Telerik.Web.UI.GridExcelBuilder.StyleElement("lightBlue");
            style.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
            style.InteriorStyle.Color = System.Drawing.Color.FromArgb(162, 226, 255);
            style.FontStyle.FontName = "Tahoma";
            style.AlignmentElement.HorizontalAlignment = Telerik.Web.UI.GridExcelBuilder.HorizontalAlignmentType.Center;
            e.WorkBook.Styles.Add(style);
            Telerik.Web.UI.GridExcelBuilder.StyleElement style2 = new Telerik.Web.UI.GridExcelBuilder.StyleElement("blue");
            style2.AlignmentElement.HorizontalAlignment = Telerik.Web.UI.GridExcelBuilder.HorizontalAlignmentType.Center;
            style2.InteriorStyle.Pattern = Telerik.Web.UI.GridExcelBuilder.InteriorPatternType.Solid;
            style2.InteriorStyle.Color = System.Drawing.Color.FromArgb(217, 243, 255);
            style2.FontStyle.FontName = "Tahoma";
            e.WorkBook.Styles.Add(style2);
        }
    }
}