
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class checkOutReasonReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                checkUserAccess();
                setGridSource_byPerson();

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

        private void setGridSource_reasonIteration()
        {
            Business.university.Request.CheckOutRequestBusiness r = new Business.university.Request.CheckOutRequestBusiness();
            var dt = r.getCheckoutFrequencyIteration();
            grdReport.DataSource = dt;
            grdReport.DataBind();
        }
        private void setGridSource_byPerson()
        {

            Business.university.Request.CheckOutRequestBusiness r = new Business.university.Request.CheckOutRequestBusiness();
            var dt = r.getCheckoutFrequencyAllStudents();
            grdReason.DataSource = dt;
            grdReason.DataBind();
        }

        private void setGridSource_byDepartment()
        {
            Business.university.Request.CheckOutRequestBusiness r = new Business.university.Request.CheckOutRequestBusiness();
            var dt = r.getCheckoutFrequency_ByDepartment();
            grdDanesh.DataSource = dt;
            grdDanesh.DataBind();
        }
        private void setGridSource_byLevel()
        {

            Business.university.Request.CheckOutRequestBusiness r = new Business.university.Request.CheckOutRequestBusiness();
            var dt = r.getCheckoutFrequency_ByLevel();
            grdLevel.DataSource = dt;
            grdLevel.DataBind();
        }

        private void setGridSource_byEnterYear()
        {
            Business.university.Request.CheckOutRequestBusiness r = new Business.university.Request.CheckOutRequestBusiness();
            var dt = r.getCheckoutFrequency_ByEnterYear();
            grdEnter.DataSource = dt;
            grdEnter.DataBind();
        }

        protected void drpReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdReason.Visible = drpReportType.SelectedItem.Value == "st";
            grdReport.Visible = drpReportType.SelectedItem.Value == "rsn";
            grdLevel.Visible = drpReportType.SelectedItem.Value == "lvl";
            grdDanesh.Visible = drpReportType.SelectedItem.Value == "dnsh";
            grdEnter.Visible = drpReportType.SelectedItem.Value == "ent";
            switch (drpReportType.SelectedItem.Value)
            {
                case "st":

                    setGridSource_byPerson();
                    break;
                case "rsn":
                    setGridSource_reasonIteration();
                    break;
                case "dnsh":
                    setGridSource_byDepartment();
                    break;
                case "lvl":
                    setGridSource_byLevel();
                    break;
                case "ent":
                    setGridSource_byEnterYear();
                    break;
            }
        }


        protected void btnExcel_Click(object sender, ImageClickEventArgs e)
        {
            System.Data.DataSet ds = new DataSet();
            Business.university.Request.CheckOutRequestBusiness r = new Business.university.Request.CheckOutRequestBusiness();
            bool allInOne = true;
            if (!allInOne)
            {


                switch (drpReportType.SelectedItem.Value)
                {
                    case "st":
                        ds.Tables.Add(r.getCheckoutFrequencyAllStudents());
                        ds.Tables[ds.Tables.Count - 1].TableName = "علت انصراف هر دانشجو";
                        break;
                    case "rsn":
                        ds.Tables.Add(r.getCheckoutFrequencyIteration());
                        ds.Tables[ds.Tables.Count - 1].TableName = "فراوانی در هر دسته بندی";

                        break;
                    case "dnsh":
                        ds.Tables.Add(r.getCheckoutFrequency_ByDepartment());
                        ds.Tables[ds.Tables.Count - 1].TableName = "به تفکیک دانشکده";
                        break;
                    case "lvl":
                        ds.Tables.Add(r.getCheckoutFrequency_ByLevel());
                        ds.Tables[ds.Tables.Count - 1].TableName = "به تفکیک مقطع";
                        break;
                    case "ent":
                        ds.Tables.Add(r.getCheckoutFrequency_ByEnterYear());
                        ds.Tables[ds.Tables.Count - 1].TableName = "به تفکیک ورودی";
                        break;
                }
            }
            else
            {
                ds.Tables.Add(r.getCheckoutFrequencyAllStudents());
                ds.Tables[ds.Tables.Count - 1].TableName = "علت انصراف هر دانشجو";

                ds.Tables.Add(r.getCheckoutFrequencyIteration());
                ds.Tables[ds.Tables.Count - 1].TableName = "فراوانی در هر دسته بندی";

                ds.Tables.Add(r.getCheckoutFrequency_ByDepartment());
                ds.Tables[ds.Tables.Count - 1].TableName = "به تفکیک دانشکده";

                ds.Tables.Add(r.getCheckoutFrequency_ByLevel());
                ds.Tables[ds.Tables.Count - 1].TableName = "به تفکیک مقطع";

                ds.Tables.Add(r.getCheckoutFrequency_ByEnterYear());
                ds.Tables[ds.Tables.Count - 1].TableName = "به تفکیک ورودی";
            }
            createExcel(ds, string.Format("{0} _  {1}", DateTime.Now.ToPeString().Replace("/", "-"), "گزارش علت انصراف دانشجویان"));
        }

        private void createExcel(DataTable source)
        {

            if (source.Rows.Count <= 0) return;
            int c = 0;
            string fileName = "report" + DateTime.Now.Millisecond.ToString() + ".xlsx";
            string filePath = Path.GetTempFileName();
            string ExcelFilePath = Server.MapPath("../" + fileName);

            Microsoft.Office.Interop.Excel.Application Excel = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbooks.Add();//,workbookName="";
            // single worksheet
            Microsoft.Office.Interop.Excel._Worksheet Worksheet = Excel.ActiveSheet;
            int ColumnsCount = source.Columns.Count;
            object[] Header = new object[ColumnsCount];

            for (int i = 0; i < ColumnsCount; i++)
                Header[i] = source.Columns[i].ColumnName;

            Microsoft.Office.Interop.Excel.Range HeaderRange = Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[1, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[1, ColumnsCount]));
            HeaderRange.Value = Header;
            HeaderRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
            HeaderRange.Font.Bold = true;

            // DataCells
            int RowsCount = source.Rows.Count;
            object[,] Cells = new object[RowsCount, ColumnsCount];

            for (int j = 0; j < RowsCount; j++)
                for (int i = 0; i < ColumnsCount; i++)
                    Cells[j, i] = source.Rows[j][i];

            Worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[2, 1]), (Microsoft.Office.Interop.Excel.Range)(Worksheet.Cells[RowsCount + 1, ColumnsCount])).Value = Cells;

            if (ExcelFilePath != null && ExcelFilePath != "")
            {
                try
                {
                    byte[] bytes;
                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        MemoryStream ms = new MemoryStream();
                        fs.CopyTo(ms);
                        bytes = System.IO.File.ReadAllBytes(filePath);
                    }
                    //Worksheet.SaveAs(ExcelFilePath);
                    //c++;
                    //Excel.Quit();
                    c++;
                    Response.Clear();
                    c++;
                    byte[] Content = bytes;// System.IO.File.ReadAllBytes(ExcelFilePath);
                    c++;
                    Response.ContentType = "text/csv"; // "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    c++;
                    Response.AddHeader("content-disposition", "attachment;  filename=" + "گزارش علت انصراف دانشجویان_" + drpReportType.SelectedItem.Text + ".xlsx");
                    c++;
                    Response.BufferOutput = true;
                    //Response.BinaryWrite(byteExcel);
                    Response.OutputStream.Write(Content, 0, Content.Length);

                    c++;
                    HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                    c++;
                    HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                    c++;
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    c++;

                    System.IO.File.Delete(ExcelFilePath);
                    c++;


                }
                catch (Exception ex)
                {

                }
            }
            else    // no filepath is given
            {
                Excel.Visible = true;
            }
        }

        private void createExcel_(DataSet dsSource, string excelFileName)
        {
            if (dsSource.Tables.Count <= 0) return;
            string filePath = Path.GetTempFileName();

            using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {

                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);//*


                //foreach (DataTable source in dsSource.Tables)
                DataTable source = dsSource.Tables[0];
                {
                    if (source.Rows.Count <= 0) return;

                    Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets()); //*

                    #region Sheet
                    Sheet presentSheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = source.TableName }; //*

                    sheets.Append(presentSheet); //*
                                                 //sheetData.Append(presentSheet);
                    int ColumnsCount = source.Columns.Count;
                    object[] Header = new object[ColumnsCount];

                    for (int i = 0; i < ColumnsCount; i++)
                        Header[i] = source.Columns[i].ColumnName;



                    Row headerRow = new Row();

                    foreach (object column in Header)
                    {
                        Cell cell = new Cell();
                        cell.DataType = CellValues.String;
                        cell.CellValue = new CellValue(column.ToString());
                        //headerStyle(document, cell);
                        headerRow.AppendChild(cell);
                    }
                    sheetData.AppendChild(headerRow);

                    int RowsCount = source.Rows.Count;


                    for (int r = 0; r < RowsCount; r++)
                    {
                        Row newRow = new Row();
                        for (int c = 0; c < ColumnsCount; c++)
                        {

                            Cell cell = new Cell();
                            cell.DataType = CellValues.String;
                            cell.CellValue = new CellValue(source.Rows[r][c].ToString());
                            //itemStyle(document, cell);

                            newRow.AppendChild(cell);
                        }
                        sheetData.AppendChild(newRow);
                    }
                    workbookPart.Workbook.Save();
                    #endregion

                }
            }
            byte[] Content;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                MemoryStream ms = new MemoryStream();
                fs.CopyTo(ms);
                Content = System.IO.File.ReadAllBytes(filePath);
            }
            Response.Clear();

            Response.ContentType = "text/csv";
            Response.AddHeader("content-disposition", "attachment;  filename=" + excelFileName + ".xlsx");
            Response.BufferOutput = true;

            Response.OutputStream.Write(Content, 0, Content.Length);

            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest();



        }

        private void createExcel(DataSet dsSource, string excelFileName)
        {
            if (dsSource.Tables.Count <= 0) return;
            string filePath = Path.GetTempFileName();

            using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                Sheets sheets = workbookPart.Workbook.AppendChild<Sheets>(new Sheets());


                var StylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
                Stylesheet _styleSheet = addStyles();
                StylesPart.Stylesheet = _styleSheet;
                StylesPart.Stylesheet.Save();
                UInt32 sheetID = 1;
                foreach (DataTable source in dsSource.Tables)
                {
                    if (source.Rows.Count <= 0) continue;
                    WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                    var sheetData = new SheetData();
                    worksheetPart.Worksheet = new Worksheet(sheetData);//*

                    #region Sheet
                    Sheet presentSheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = sheetID, Name = source.TableName }; //*
                    sheetID++;
                    sheets.Append(presentSheet); //*
                                                 //sheetData.Append(presentSheet);
                    int ColumnsCount = source.Columns.Count;
                    object[] Header = new object[ColumnsCount];

                    for (int i = 0; i < ColumnsCount; i++)
                        Header[i] = source.Columns[i].ColumnName;



                    Row headerRow = new Row();

                    foreach (object column in Header)
                    {
                        Cell cell = new Cell();
                        cell.DataType = CellValues.String;
                        cell.CellValue = new CellValue(column.ToString());
                        headerStyle(cell);
                        headerRow.AppendChild(cell);
                    }
                    sheetData.AppendChild(headerRow);

                    int RowsCount = source.Rows.Count;


                    for (int r = 0; r < RowsCount; r++)
                    {
                        Row newRow = new Row();
                        for (int c = 0; c < ColumnsCount; c++)
                        {

                            Cell cell = new Cell();
                            cell.DataType = CellValues.String;
                            cell.CellValue = new CellValue(source.Rows[r][c].ToString());
                            itemStyle(cell);

                            newRow.AppendChild(cell);
                        }
                        sheetData.AppendChild(newRow);
                    }
                    workbookPart.Workbook.Save();
                    #endregion

                }
            }
            byte[] Content;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                MemoryStream ms = new MemoryStream();
                fs.CopyTo(ms);
                Content = System.IO.File.ReadAllBytes(filePath);
            }
            Response.Clear();

            Response.ContentType = "text/csv";
            Response.AddHeader("content-disposition", "attachment;  filename=" + excelFileName + ".xlsx");
            Response.BufferOutput = true;

            Response.OutputStream.Write(Content, 0, Content.Length);

            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest();

            //System.IO.File.Delete(ExcelFilePath);


        }
        //private void addStyles(SpreadsheetDocument document)
        //{
        //    var f1 = AddFont_Item(new Fonts());
        //    AddCellFormat(document.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats, f1);


        //    var f2 = AddFont_Header(document.WorkbookPart.WorkbookStylesPart.Stylesheet.Fonts);
        //    AddCellFormat(document.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats, f2);
        //}
        //private void _addStyles(Stylesheet style)
        //{
        //    style.Fonts = new Fonts();
        //    style.CellFormats = new CellFormats();
        //    var f1 = AddFont_Item(style.Fonts);
        //    var f2 = AddFont_Header(style.Fonts);
        //    AddCellFormat(style.CellFormats, f1);
        //    AddCellFormat(style.CellFormats, f2);
        //}
        private Stylesheet addStyles()
        {
            Stylesheet style = new Stylesheet();
            Fonts fnt = new Fonts();
            CellFormats frmt = new CellFormats();


            var f1 = AddFont_Item();
            fnt.Append(f1);
            var cf1=AddCellFormat(fnt);

            var f2 = AddFont_Header();
            fnt.Append(f2);
            var cf2 = AddCellFormat(fnt);

            frmt.Append(cf1);
            frmt.Append(cf2);

            style.Append(fnt);
            style.Append(frmt);
            return style;
        }

        private void headerStyle(Cell c)
        {
            c.StyleIndex = (UInt32)(1);// (document.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats.Elements<CellFormat>().Count() - 1);
        }
        private void itemStyle(Cell c)
        {
            c.StyleIndex = (UInt32)(0);// (document.WorkbookPart.WorkbookStylesPart.Stylesheet.CellFormats.Elements<CellFormat>().Count() - 1);
        }
        private Font AddFont_Header()
        {

            Font fontHeader = new Font();
            Bold bold = new Bold();
            DocumentFormat.OpenXml.Spreadsheet.FontSize fontSize = new DocumentFormat.OpenXml.Spreadsheet.FontSize() { Val = 12D };
            Color color = new Color() { Rgb = new HexBinaryValue("0x00660a") };
            FontName fontName = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme = new FontScheme() { Val = FontSchemeValues.Minor };

            fontHeader.Append(bold);
            fontHeader.Append(fontSize);
            fontHeader.Append(color);
            fontHeader.Append(fontName);
            fontHeader.Append(fontFamilyNumbering);
            fontHeader.Append(fontScheme);
            
            //fs.Append(fontHeader);
            return fontHeader;
        }
        private Font AddFont_Item()
        {
            Font fontItem = new Font();
            DocumentFormat.OpenXml.Spreadsheet.FontSize fontSize = new DocumentFormat.OpenXml.Spreadsheet.FontSize() { Val = 11D };
            Color color = new Color() { Rgb = new HexBinaryValue("0x000000") };
            FontName fontName = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme = new FontScheme() { Val = FontSchemeValues.Minor };

            fontItem.Append(fontSize);
            fontItem.Append(color);
            fontItem.Append(fontName);
            fontItem.Append(fontFamilyNumbering);
            fontItem.Append(fontScheme);
            
            return fontItem;
        }
        private CellFormat AddCellFormat(Fonts fs)
        {
            //return;
            CellFormat _cellFormat = new CellFormat() { FontId = (UInt32)(fs.Elements<Font>().Count() - 1), FillId = 0, BorderId = 0, FormatId = 0, ApplyFill = true };
            return _cellFormat;
            //if (cf == null)
            //    cf = new CellFormats();
            //cf.Append(cellFormat2);
        }
    }
}