using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Office.Interop.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using DataTable = System.Data.DataTable;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.Tuitional
{
    public partial class insertTuition : System.Web.UI.Page
    {

        [Serializable]
        private class indexColumn
        {
            public int columnID { get; set; }
            public string columnName { get; set; }
            public string columnText { get; set; }
            public string selectedInExcel { get; set; }
        }
        const string vs_SheetColumn = "excelColumns", vs_ExcelFile = "excelFile", pathFolder = "~/University/Tuitional/insert";
        const int maxNumberOfExcelColumns = 30;
        const int firstRow = 3;

        Dictionary<int, List<indexColumn>> dicSheet_Column;
        protected void Page_Load(object sender, EventArgs e)
        {
            accessControl();
            if (!IsPostBack)
            {
                ViewState[vs_SheetColumn] = "";
                if (Directory.Exists(Server.MapPath(pathFolder)))
                {
                    string[] files = Directory.GetFiles(Server.MapPath(pathFolder));

                    foreach (string file in files)
                    {
                        try
                        {
                            File.SetAttributes(file, FileAttributes.Normal);
                            File.Delete(file);
                        }
                        catch { }
                    }
                    Directory.Delete(Server.MapPath(pathFolder));
                }
            }
        }

        private void accessControl()
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
            }
            AccessControl.MenuId = menuId;
            Session[sessionNames.menuID] = menuId;
            AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();
        }

        protected void btnUploadExcel_Click(object sender, EventArgs e)
        {
            if (txtIcanNumber.Text != "")
            {
                if (uploadFile.UploadedFiles.Count > 0)
                {
                    UploadedFile excelFile = uploadFile.UploadedFiles[0];
                    saveExcel(excelFile);
                    parseExcelFile(excelFile);

                    btnReset.Visible = true;
                    btnInsertTuition.Visible = true;
                    btnOpenCartAccess.Visible = true;
                    btnUploadExcel.Visible = false;
                    uploadFile.Visible = false;
                    drpSheets.Visible = true;
                    lblSheets.Visible = true;
                    grdColumns.Visible = true;
                    txtIcanNumber.Enabled = false;
                }
            }
            else
            {
                showAlert("لطفا ابتدا شماره نامه آیکن را وارد نموده و پس از انتخاب مجدد فایل اکسل، دکمه آپلود فایل را فشار دهید.");
            }
        }

        private void saveExcel(UploadedFile excelFile)
        {
            if (!Directory.Exists(Server.MapPath(pathFolder)))
            {
                Directory.CreateDirectory(Server.MapPath(pathFolder));
            }
            string fileNameToSave = pathFolder + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + excelFile.FileName;
            excelFile.SaveAs(Server.MapPath(fileNameToSave));
            ViewState[vs_ExcelFile] = fileNameToSave;

        }
        private string getExcel()
        {

            return Server.MapPath(ViewState[vs_ExcelFile].ToString());

        }

        private void parseExcelFile(object excelFile)
        {
            var xApp = new Microsoft.Office.Interop.Excel.Application();
            xApp.Visible = false;
            var xBook = xApp.Workbooks.Open(getExcel());
            int sheetCount = xBook.Worksheets.Count;
            DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("sheetnumber");
            dt.Columns.Add("sheetText");
            DataRow drSelect = dt.NewRow();
            drSelect["sheetnumber"] = 0;
            drSelect["sheetText"] = "انتخاب کنید";
            dt.Rows.Add(drSelect);
            dicSheet_Column = new Dictionary<int, List<indexColumn>>();
            for (int i = 1; i <= sheetCount; i++)
            {
                var xSheet = (Microsoft.Office.Interop.Excel.Worksheet)xBook.Worksheets.Item[i];
                if (xSheet.Visible == XlSheetVisibility.xlSheetVisible)
                {
                    DataRow dr = dt.NewRow();
                    dr["sheetnumber"] = i;
                    dr["sheetText"] = xSheet.Name;
                    dt.Rows.Add(dr);
                    dicSheet_Column.Add(i, getNewIndexColumnList());
                }
            }
            ViewState[vs_SheetColumn] = dicSheet_Column;
            drpSheets.DataSource = dt;
            drpSheets.DataBind();
            xBook.Close(false);
            xApp.Quit();
        }

        private List<indexColumn> getNewIndexColumnList()
        {
            List<indexColumn> myIndexColumns = new List<indexColumn>();
            myIndexColumns.Add(new indexColumn { columnID = 0, columnName = "select", columnText = "انتخاب کنید", selectedInExcel = "" });
            myIndexColumns.Add(new indexColumn { columnID = 1, columnName = "stcode", columnText = "کد دانشجویی", selectedInExcel = "" });
            myIndexColumns.Add(new indexColumn { columnID = 2, columnName = "shah_fix", columnText = "شهریه  تایید شده بنیاد", selectedInExcel = "" });
            //myIndexColumns.Add(new indexColumn { columnID = 3, columnName = "shah_var", columnText = "شهریه متغیر تایید شده", selectedInExcel = "" });
            return myIndexColumns;
        }

        private void parseSheet(int sheetID)
        {
            if (sheetID == 0)
                return;
            var xApp = new Microsoft.Office.Interop.Excel.Application();
            xApp.Visible = false;
            var xBook = xApp.Workbooks.Open(getExcel());
            var xSheet = (Microsoft.Office.Interop.Excel.Worksheet)xBook.Worksheets.Item[sheetID];
            int columnCount = maxNumberOfExcelColumns;

            columnCount = xSheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell, Type.Missing).Column;

            DataTable dt = new DataTable();
            dt.Columns.Add("rowNum");
            dt.Columns.Add("sheetColumn");
            lblTerm.Text = " ترم: " + Convert.ToString((xSheet.Cells[1, 1] as Microsoft.Office.Interop.Excel.Range).Value2);

            for (int cc = 1; cc < columnCount; cc++)
            {
                if ((xSheet.Cells[firstRow, cc] as Microsoft.Office.Interop.Excel.Range).Value2 == null)
                {
                    cc = columnCount;
                    break;
                }
                DataRow dr = dt.NewRow();
                dr["rowNum"] = cc;
                dr["sheetColumn"] = Convert.ToString((xSheet.Cells[firstRow, cc] as Microsoft.Office.Interop.Excel.Range).Value2);
                dt.Rows.Add(dr);
            }
            grdColumns.DataSource = dt;
            grdColumns.DataBind();
            xBook.Close(false);
            xApp.Quit();
        }

        private bool setColumnNameSource(int sheetID, DropDownList dropDown, string t = "")
        {
            Dictionary<int, List<indexColumn>> sheetColumns = ViewState[vs_SheetColumn] as Dictionary<int, List<indexColumn>>;
            List<indexColumn> AllColumns;
            bool existSheet = sheetColumns.TryGetValue(sheetID, out AllColumns);
            if (existSheet)
            {
                var unAssignedColumns = AllColumns.Where(c => c.selectedInExcel == "" || c.selectedInExcel == t);
                dropDown.DataSource = unAssignedColumns.ToList();
                dropDown.DataTextField = "columnText";
                dropDown.DataValueField = "columnID";
                dropDown.DataBind();

                if (t != "")
                {
                    var si = unAssignedColumns.Where(c => c.selectedInExcel == t).FirstOrDefault();
                    if (si != null)
                    {
                        dropDown.SelectedValue = si.columnID.ToString();
                        return true;
                    }
                }
            }
            return false;

        }


        protected void drpSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            parseSheet(Convert.ToInt32(drpSheets.SelectedItem.Value));
        }


        protected void grdColumns_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                DropDownList drp = (DropDownList)e.Item.FindControl("drpListColumn");
                System.Web.UI.WebControls.Button btn = (System.Web.UI.WebControls.Button)e.Item.FindControl("btnChangeColumn");


                if(setColumnNameSource(Convert.ToInt32(drpSheets.SelectedItem.Value), drp, btn.CommandArgument))
                {
                    e.Item.BackColor = System.Drawing.Color.Yellow;

                }
                else
                {
                    e.Item.BackColor = System.Drawing.Color.White;

                }
            }
        }

        protected void grdColumns_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                string excelColumn = e.CommandArgument.ToString();
                DropDownList drp = (DropDownList)e.Item.FindControl("drpListColumn");
                int i = Convert.ToInt32(drp.SelectedItem.Value);

                Dictionary<int, List<indexColumn>> sheetColumns = ViewState[vs_SheetColumn] as Dictionary<int, List<indexColumn>>;
                List<indexColumn> AllColumns;
                bool existSheet = sheetColumns.TryGetValue(Convert.ToInt32(drpSheets.SelectedItem.Value), out AllColumns);
                if (existSheet)
                {
                    AllColumns.Where(c => c.selectedInExcel == excelColumn).ToList().ForEach(c => c.selectedInExcel = "");
                    if (i > 0)
                    {
                        AllColumns.FindLast(a => a.columnID == i).selectedInExcel = excelColumn;
                    }
                    sheetColumns[Convert.ToInt32(drpSheets.SelectedItem.Value)] = AllColumns;
                    ViewState[vs_SheetColumn] = sheetColumns;
                    parseSheet(Convert.ToInt32(drpSheets.SelectedItem.Value));
                }
            }
        }

        protected void btnInsertTuition_Click(object sender, EventArgs e)
        {
            string s = "";
            message.InnerText = "";
            Dictionary<int, List<indexColumn>> sheetColumns = ViewState[vs_SheetColumn] as Dictionary<int, List<indexColumn>>;
            int minIndex = sheetColumns.Keys.Min();
            int maxIndex = sheetColumns.Keys.Max();
            for (int i = minIndex; i <= maxIndex; i++)
            {
                List<indexColumn> AllColumns;
                bool existSheet = sheetColumns.TryGetValue(i, out AllColumns);
                if (existSheet)
                {
                    var unSelectedColumns = AllColumns.Where(c => c.selectedInExcel == "" && c.columnID > 0).ToList();

                    if (unSelectedColumns.Count > 0)
                    {
                        for (int j = 0; j < unSelectedColumns.Count; j++)
                        {
                            s += "ستون " + unSelectedColumns.ToList()[j].columnText + " در شیت " + drpSheets.Items[i].Text + ",";
                        }
                    }
                }
            }
            if (s != "")
                showMessage("لطفا برای " + s + " ستون معادلی را از لیست ستون های اکسل مشخص فرمایید سپس دکمه ثبت در پایگاه داده را فشار دهید.");

            else
            {
                for (int i = minIndex; i <= maxIndex; i++)
                {
                    InsertTuitions(i);
                }
                showAlert("شهریه ها به درستی در پایگاه داده درج شدند");
                resetForm();
            }
        }

        private void resetForm()
        {
            btnReset.Visible = false;
            btnInsertTuition.Visible = false;
            btnUploadExcel.Visible = true;
            btnOpenCartAccess.Visible = false;
            uploadFile.Visible = true;
            drpSheets.Visible = false;
            lblSheets.Visible = false;
            grdColumns.Visible = false;
            txtIcanNumber.Enabled = false;
        }

        private void showMessage(string msg)
        {
            message.InnerText = msg;
        }

        private void showAlert(string msg)
        {
            rAlert.RadAlert(msg, null, null, "پیام", "");
        }

        protected void btnOpenCartAccess_Click(object sender, EventArgs e)
        {
            string s = "";
            message.InnerText = "";
            Dictionary<int, List<indexColumn>> sheetColumns = ViewState[vs_SheetColumn] as Dictionary<int, List<indexColumn>>;
            int minIndex = sheetColumns.Keys.Min();
            int maxIndex = sheetColumns.Keys.Max();
            for (int i = minIndex; i <= maxIndex; i++)
            {
                List<indexColumn> AllColumns;
                bool existSheet = sheetColumns.TryGetValue(i, out AllColumns);
                if (existSheet)
                {
                    var unSelectedColumns = AllColumns.Where(c => c.selectedInExcel == "" && c.columnID > 0).ToList();

                    if (unSelectedColumns.Count > 0)
                    {
                        for (int j = 0; j < unSelectedColumns.Count; j++)
                        {
                            s += "ستون " + unSelectedColumns.ToList()[j].columnText + " در شیت " + drpSheets.Items[i].Text + ",";
                        }
                    }
                }
            }
            if (s != "")
                showMessage("لطفا برای " + s + " ستون معادلی را از لیست ستون های اکسل مشخص فرمایید سپس دکمه ثبت در پایگاه داده را فشار دهید.");

            else
            {
                for (int i = minIndex; i <= maxIndex; i++)
                {
                    openCartAccess(i);
                }
                showAlert("عملیات باز شدن دسترسی کارت ورود به جلسه انجام شد. شما می توانید با مراجعه به فایل اکسل و مشاهده ستون اضافه شده، نتیجه را مشاهده فرمایید");
                resetForm();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            resetForm();
            drpSheets.DataSource = null;
            drpSheets.DataBind();
            grdColumns.DataSource = null;
            grdColumns.DataBind();
            ViewState[vs_SheetColumn] = "";
            System.IO.File.Delete(MapPath(ViewState[vs_ExcelFile].ToString()));
            ViewState[vs_ExcelFile] = "";
            message.InnerText = "";
        }

        private void InsertTuitions(int sheetID)
        {
            var xApp = new Microsoft.Office.Interop.Excel.Application();
            xApp.Visible = false;
            var xBook = xApp.Workbooks.Open(getExcel());
            Dictionary<int, List<indexColumn>> sheetColumns = ViewState[vs_SheetColumn] as Dictionary<int, List<indexColumn>>;
            List<indexColumn> AllColumns;
            bool existSheet = sheetColumns.TryGetValue(sheetID, out AllColumns);
            if (existSheet)
            {
                try
                {

                    Business.Common.CommonBusiness CB = new Business.Common.CommonBusiness();
                    var sheet = (Microsoft.Office.Interop.Excel.Worksheet)xBook.Worksheets.Item[sheetID];
                    int lastRow = sheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell, Type.Missing).Row;
                    int stcodeColumnNumber = Convert.ToInt32(AllColumns.FindLast(c => c.columnName == "stcode").selectedInExcel);
                    int tuitionColumnNumber = Convert.ToInt32(AllColumns.FindLast(c => c.columnName == "shah_fix").selectedInExcel);
                    string term = Convert.ToString((sheet.Cells[1, 1] as Microsoft.Office.Interop.Excel.Range).Value2);
                    string userName = Session[sessionNames.userName_Karbar].ToString();
                    string fishNumber = txtIcanNumber.Text;
                    for (int i = firstRow + 1; i < lastRow; i++)
                    {

                        var stcode = (sheet.Cells[i, stcodeColumnNumber] as Microsoft.Office.Interop.Excel.Range).Value2;
                        var amount = (sheet.Cells[i, tuitionColumnNumber] as Microsoft.Office.Interop.Excel.Range).Value2;
                        CB.insertTuitional(userName, Convert.ToString(stcode), fishNumber, Convert.ToDecimal(amount), Convert.ToString(term));
                    }
                    (sheet.Cells[1, 1] as Microsoft.Office.Interop.Excel.Range).Value2 = null;
                    xBook.Close(false);
                    xApp.Quit();

                }
                catch (Exception ex)
                {
                    showAlert(ex.Message);
                }
            }
        }


        private void openCartAccess(int sheetID)
        {
            var xApp = new Microsoft.Office.Interop.Excel.Application();
            xApp.Visible = false;
            var xBook = xApp.Workbooks.Open(getExcel());
            Dictionary<int, List<indexColumn>> sheetColumns = ViewState[vs_SheetColumn] as Dictionary<int, List<indexColumn>>;
            List<indexColumn> AllColumns;
            bool existSheet = sheetColumns.TryGetValue(sheetID, out AllColumns);
            if (existSheet)
            {
                try
                {

                    Business.Common.CommonBusiness CB = new Business.Common.CommonBusiness();
                    var sheet = (Microsoft.Office.Interop.Excel.Worksheet)xBook.Worksheets.Item[sheetID];
                    int lastRow = sheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell, Type.Missing).Row;
                    int lastCol = 1;
                    int tempLast = sheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell, Type.Missing).Column;
                    for (int col = 1; col < tempLast; col++)
                    {
                        if ((sheet.Cells[3, col] as Microsoft.Office.Interop.Excel.Range).Value2 == null)
                            break;
                        else
                            lastCol++;

                    }
                    int stcodeColumnNumber = Convert.ToInt32(AllColumns.FindLast(c => c.columnName == "stcode").selectedInExcel);
                    bool res;
                    for (int i = firstRow + 1; i < lastRow; i++)
                    {

                        var stcode = (sheet.Cells[i, stcodeColumnNumber] as Microsoft.Office.Interop.Excel.Range).Value2;
                        res = CB.openCartAccess(Convert.ToString(stcode));
                        (sheet.Cells[i, lastCol + 1] as Microsoft.Office.Interop.Excel.Range).Value2 = "دسترسی باز " + (res ? "شد" : "نشد");
                    }
                    (sheet.Cells[1, 1] as Microsoft.Office.Interop.Excel.Range).Value2 = null;
                    xBook.Close(false);
                    xApp.Quit();

                }
                catch (Exception ex)
                {
                    showAlert(ex.Message);
                }
            }
        }
    }
}