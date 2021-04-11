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

namespace IAUEC_Apps.UI.University.GraduateAffair.CMS
{
    public partial class THRMarkaz : System.Web.UI.Page
    {
        private Excel.Application _app;
        private Excel.Workbooks _books;
        private Excel.Workbook _book;
        protected Excel.Sheets _sheets;
        protected Excel.Worksheet _sheet;
        GraduateReportBusiness GraduateBusiness = new GraduateReportBusiness();
        CommonBusiness cmb = new CommonBusiness();
        const string stCode = "studentCode";

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
                setGrdViewSource();
                GridFilterMenu menu = grd_view.FilterMenu;
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
        }
        protected void OpenExcelWorkbook(string fileName)
        {
            _app = new Excel.Application();

            if (_book == null)
            {
                _books = _app.Workbooks;
                _book = _books.Open(fileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                _sheets = _book.Worksheets;
            }
        }
        protected void CloseExcelWorkbook()
        {

            _book.SaveAs(Server.MapPath("../../../Report-far/Far_excel/" + "tehranmarkaz-" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + "-" + DateTime.Now.Hour + ".xls"), Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //_book.SaveAs("d:\\test"+DateTime.Now.ToShortDateString()+"-"+DateTime.Now.ToShortTimeString() + ".xls", Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            _book.Close(false, Type.Missing, Type.Missing);
        }
        protected void NAR(object o)
        {

            try
            {
                if (o != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
            }

            finally
            {
                o = null;
            }
        }
        public DataTable GetDataTable(int sheet)
        {
            DataTable dtExcelTable = new DataTable();
            dtExcelTable = GraduateBusiness.GetTHRMarkaz(sheet, 1);
            return dtExcelTable;
        }
        protected MemoryStream DataTableToExcelXlsx()
        {
            DataTable dtstcode = new DataTable();
            dtstcode.Columns.Add("stcode", typeof(string));
            DataRow dr;
            var pack = new OfficeOpenXml.ExcelPackage();
            var result = new MemoryStream();
            string[] c1 = new string[]{"شماره دانشجویی","نام","نام خانوادگی","جنسیت","نام پدر","سال ورود","نیمسال ورود(مهر/بهمن)","شماره شناسنامه","کدملی","رشته","گرایش","مقطع","سیستم آموزشی","وضعیت دانشجو","نوع پذیرش","ترم انتقالی","مبدا انتقالی از ","محل تولد","محل صدور","تاریخ تولد","نظام وظیفه","ردیف قبولی","رتبه قبولی","نمره کل قبولی","آخرین مدرک","محل اخذ آخرین مدرک","رشته مقطع پایه","ملیت","نوع سهمیه"
            ,"تاریخ فراغت","ترم فراغت","تاریخ دفاع","درجه دفاع","تلفن","موبایل","آدرس","استان","شهر","پاسپورت","کدپستی","تاریخ ثبت نام","تاریخ شروع به تحصیل","ایمیل","مقطع فراغت از تحصیل","تاریخ اخذ آخرین مدرک", "تعدادکل واحدهای قبولی", "تعدادکل واحدهای موثر", "تعدادکل واحدهای انتخابی", "امتیازکل", "معدل کل" ,"شماره داوطلبی"};
            string[] c2 = new string[] { "شماره دانشجویی", "شماره ترم", "سال و نیمسال", "وضعیت ترم", "مقصد مهمان به (در صورت مهمان به بودن وضعیت ترم)", "نوع ترم(جبرانی / جبرانی معرفی به استاد/انتقالی از)", "واحد انتخابی", "واحد موثر", "واحد قبولی", "امتیاز", "معدل", "وضعیت مشروطی" };
            string[] c3 = new string[] { "شماره دانشجویی", "سال و نیمسال", "کد درس", "نام درس", "نوع درس", "واحد نظری", "واحد عملی", "نمره", "امتیاز", "وضعیت نمره", "وضعیت درس(عادی/مهمان به)" };
            string[] c4 = new string[] { "شماره دانشجویی", "تاریخ معادلسازی", "ترم معادلسازی", "نام دانشگاه قبلی", "مقطع قبلی", "رشته قبلی", "نوع دانشگاه قبلی", "جمع واحد", "قبولی", "جمع امتیاز" };
            string[] c5 = new string[] { "شماره دانشجویی", "کد درس معادلسازی", "نام درس", "نوع درس", "نمره", "وضعیت نمره", "امتیاز" };
            string[] Cname = new string[] { "moshakhasate fardi", "moshakhase termi", "moshakhasate doros", "moshakhasate terme moadelsazi", "moshakhase doros moadelsazi" };

            for (int sh = 1; sh < 6; sh++)
            {
                var dt = GetDataTable(sh);


                var ws = pack.Workbook.Worksheets.Add(Cname[sh - 1]);

                int counter = 0;
                switch (sh)
                {
                    case 1:
                        counter = 51;
                        break;
                    case 2:
                        counter = 12;
                        break;
                    case 3:
                        counter = 11;
                        break;
                    case 4:
                        counter = 10;
                        break;
                    case 5:
                        counter = 7;
                        break;
                }
                //int col = 1;
                //int row = 1;

                for (int col = 0; col < counter; col++)
                {

                    if (sh == 1)
                        ws.Cells[1, col + 1].Value = c1[col].ToString();
                    if (sh == 2)
                        ws.Cells[1, col + 1].Value = c2[col].ToString();
                    if (sh == 3)
                        ws.Cells[1, col + 1].Value = c3[col].ToString();
                    if (sh == 4)
                        ws.Cells[1, col + 1].Value = c4[col].ToString();
                    if (sh == 5)
                        ws.Cells[1, col + 1].Value = c5[col].ToString();
                    int r = 2;
                    if (dt.Rows.Count > 0)
                    {
                        for (int row = 0; row < dt.Rows.Count; row++)
                        {
                            ws.Cells[r, col + 1].Value = dt.Rows[row][col].ToString();
                            if (sh == 1 && col == 0)
                            {
                                dr = dtstcode.NewRow();
                                dr["stcode"] = dt.Rows[row][0].ToString();
                                dtstcode.Rows.Add(dr);
                            }


                            r++;
                        }
                    }
                }


            }

            GraduateBusiness.UpdateConvertExcel_Far_Stcode(dtstcode);
            pack.SaveAs(result);
            return result;
        }

        protected void grd_view_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grd_view.DataSource = GraduateBusiness.GetTHRMarkaz(0, 2);
            GridFilterMenu menu = grd_view.FilterMenu;
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

        protected void btnSearchStudent_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_stcode.Text.ToString().Trim()))
                setGrdShowStudentInfSource(txt_stcode.Text.ToString().Trim());
            else
            {
                //لطفا شماره دانشجویی را وارد فرمایید
            }

        }

        private void setGrdShowStudentInfSource(string studentCode)
        {
            ViewState[stCode] = "";
            DataTable dtSearchStudent = getDtSearchStudent(studentCode);
            grdShowStudentInf.DataSource = dtSearchStudent;
            grdShowStudentInf.DataBind();
            dvStudentInf.Visible = true;
            grdShowStudentInf.MasterTableView.GetColumn("btnRemove_DB").Display = false;
            grdShowStudentInf.MasterTableView.GetColumn("btnAdd_DB").Display = false;
            grdShowStudentInf.MasterTableView.GetColumn("btnRemove_Excel").Display = false;
            //btnRemove_DB.Visible = false;
            //btnRemove_Excel.Visible = false;
            if (dtSearchStudent.Rows.Count == 1)
            {
                ViewState[stCode] = studentCode;
                if (dtSearchStudent.Rows[0]["hdnregindb"].ToString() == "1")
                {
                    grdShowStudentInf.MasterTableView.GetColumn("btnRemove_DB").Display = true;
                }
                else if (dtSearchStudent.Rows[0]["hdnregindb"].ToString() == "0")
                {

                    grdShowStudentInf.MasterTableView.GetColumn("btnAdd_DB").Display = true;
                }

                //btnRemove_DB.Visible = dtSearchStudent.Rows[0]["hdnregindb"].ToString().Trim() == "1";


                if (dtSearchStudent.Rows[0]["hdninExcel"] != DBNull.Value)
                    if (dtSearchStudent.Rows[0]["hdninExcel"].ToString() != "False")
                        grdShowStudentInf.MasterTableView.GetColumn("btnRemove_Excel").Display = true;
                //btnRemove_Excel.Visible = dtSearchStudent.Rows[0]["hdninExcel"].ToString().Trim() == "1" || dtSearchStudent.Rows[0]["hdninExcel"].ToString().Trim().ToLower() == "true";

            }
            //else
            //    Button1.Visible = true;
        }

        private DataTable getDtSearchStudent(string studentCode)
        {
            DataTable dtSearchStudent = GraduateBusiness.getGraduatedStudentBystCode(studentCode);// new DataTable();//get dt from amozesh.dbo.Far_PersonalInfo by stCode
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("name");
            dtResult.Columns.Add("field");
            dtResult.Columns.Add("regInDB");
            dtResult.Columns.Add("inExcel");
            //dtResult.Columns.Add("regInDB");
            dtResult.Columns.Add("hdninExcel");
            dtResult.Columns.Add("hdnregInDB");
            foreach (DataRow dr in dtSearchStudent.Rows)
            {
                DataRow drNew = dtResult.NewRow();
                drNew["name"] = dr["name"] + "  " + dr["family"];
                drNew["field"] = dr["nameresh"] + (dr["gera"] != DBNull.Value && dr["gera"].ToString().Trim() != "" ? "  -  " + dr["gera"] : "");
                drNew["inexcel"] = dr["Is_ConvertExcel"] == DBNull.Value ? "خیر" : dr["Is_ConvertExcel"].ToString() == "0" || dr["Is_ConvertExcel"].ToString().Trim().ToLower() != "true" ? "خیر" : "بله";
                drNew["regInDB"] = dr["isInTehran"].ToString() == "1" ? "بله" : "خیر";
                drNew["hdnRegInDB"] = dr["isInTehran"];
                drNew["hdninexcel"] = dr["Is_ConvertExcel"];
                dtResult.Rows.Add(drNew);

            }

            return dtResult;
        }

        protected void grdShowStudentInf_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "removeExcel":
                    hdnConfirmWhat.Value = e.CommandName;
                    MsgConf.Text = "آیا از درخواست صدور مجدد فایل اکسل برای این دانشجو اطمینان دارید؟";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "radconfirm();", true);
                   

                    break;
                case "removeThr":

                    hdnConfirmWhat.Value = e.CommandName;
                   
                    MsgConf.Text = "آیا از حذف این دانشجو ار لیست فارغ التحصیلان دانشگاه تهران مرکز اطمینان دارید؟";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "radconfirm();", true);
                    break;
                case "addThr":
                    AddStudentToTehran();
                    break;
            }
        }

        private void AddStudentToTehran()
        {
            GraduateBusiness.InsertFar_TehranMarkaz(txt_stcode.Text);
            cmb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 95, txt_stcode.Text, 0);
            setGrdShowStudentInfSource(ViewState[stCode].ToString());
            setGrdViewSource();
        }

        private void removeStudentFromTehran()
        {
            if (!string.IsNullOrEmpty(ViewState[stCode].ToString()))
            {
                if (!GraduateBusiness.deleteStudentFromThrMrkz(ViewState[stCode].ToString()))
                {
                    msgAlert.Text = "متاسفانه در زمان حذف دانشجو از لیست تهران مرکز ، خطایی پیش آمده است. لطفا مجددا تلاش کنید";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "radAlert();", true);
                }
                setGrdShowStudentInfSource(ViewState[stCode].ToString());
                setGrdViewSource();
            }
        }

        private void clearExcelColumn()
        {
            if (!string.IsNullOrEmpty(ViewState[stCode].ToString()))
            {
                if (!GraduateBusiness.updateExcelStatusToNotConverted(ViewState[stCode].ToString()))
                {

                    msgAlert.Text = "متاسفانه در زمان تغییر وضعیت ، خطایی پیش آمده است. لطفا مجددا تلاش کنید";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "radAlert();", true);
                    //msg=متاسفانه در زمان تغییر وضعیت ، خطایی پیش آمده است. لطفا مجددا تلاش کنید

                }
                setGrdShowStudentInfSource(ViewState[stCode].ToString());
                setGrdViewSource();
            }
        }

        private void setGrdViewSource()
        {
            DataTable dt = GraduateBusiness.GetTHRMarkaz(0, 2);
            grd_view.DataSource = dt;
            grd_view.DataBind();
            btn_excel.Visible = dt.Rows.Count > 0;
        }

        protected void btn_excel_Click(object sender, EventArgs e)
        {
            var ms = DataTableToExcelXlsx();

            ms.WriteTo(HttpContext.Current.Response.OutputStream);
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + "tehranmarkaz -" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + " - " + DateTime.Now.Hour + ".xls");
            HttpContext.Current.Response.StatusCode = 200;
            HttpContext.Current.Response.End();
            cmb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 96, "", 0);
            setGrdShowStudentInfSource(txt_stcode.Text);
            setGrdViewSource();
        }
        
        protected void radBtnYes_Click(object sender, EventArgs e)
        {
            switch (hdnConfirmWhat.Value)
            {
                case "removeExcel":
                   clearExcelColumn();

                    break;
                case "removeThr":
                    removeStudentFromTehran();
                    break;
            }
        }
    }
}