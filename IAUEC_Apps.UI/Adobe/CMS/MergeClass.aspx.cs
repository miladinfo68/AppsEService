using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.AdobeClasses;
using System.IO;

namespace IAUEC_Apps.UI.Adobe.CMS
{
    public partial class MergeClass : System.Web.UI.Page
    {
        ClassBusiness clsB = new ClassBusiness();
        DataTable dt = new DataTable();
        DataTable mergeClassDT = new DataTable();
        UniversityBusiness unvB = new UniversityBusiness();
        List<ClassListDTO> clss = new List<ClassListDTO>();
        ClassListDTO mrgClass = new ClassListDTO();

        string nimsal;
        string code;
        string name;


        protected void Page_Load(object sender, EventArgs e)
        {
            dt = unvB.GetNimsalJary();
            nimsal = dt.Rows[0]["nimsal"].ToString();
            ViewState.Add("nimsal", nimsal);
            if (!IsPostBack)
            {
                Session["clss"] = clss;
                ViewState.Add("code", 0);
                ViewState.Add("name", 0);
                mergeClassDT = clsB.getMergeClass(nimsal);
                ViewState.Add("mergeClassDT", mergeClassDT);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtName.Text == string.Empty && txtCode.Text == string.Empty) //at least one text box should have a value for search
            {
                string msg = "حداقل بايد يکي از باکس ها را پر کنيد";
                RadWindowManager1.RadAlert(msg, 0, 100, " پیام سیستم", "");
                clearAll();
            }
            else
            {
                clss = (List<ClassListDTO>)Session["clss"];
                divclasses.Visible = true;
                divtitle.Visible = true;
                grdList.DataSource = clss;
                grdList.DataBind();

                code = txtCode.Text;
                if (code == string.Empty)
                {
                    code = "0";
                }
                name = txtName.Text;
                nimsal = ViewState["nimsal"].ToString();

                ViewState.Add("code", code);
                ViewState.Add("name", name);

                bindInfoGrid();
            }
        }

        protected void btnMerge_Click(object sender, EventArgs e)
        {
            //if (cmbOstad.SelectedIndex > 0)
            //{
            if (grdInfo.Items.Count != 0)
            {
                DataTable chkMrg;
                chkMrg = clsB.CheckMergeCode(txtmergeCode.Text);
                string chkCode = chkMrg.Rows[0]["CHK"].ToString();
                if (chkCode=="2")
                {


                    if (Convert.ToInt32(txtSessionCount.Text) >= 1 && Convert.ToInt32(txtSessionCount.Text) <= 20)
                    {

                        rvSessionCount.IsValid = true;
                        RequiredFieldValidator1.IsValid = true;

                        mrgClass = (ClassListDTO)Session["mrgClass"];
                        mrgClass.MergeCode = txtmergeCode.Text;
                        mrgClass.CourseCode = txtCodeDars.Text;
                        mrgClass.SessionCount = Convert.ToInt32(txtSessionCount.Text);
                        mrgClass.FirstSession = txtFirstSession.Text;
                        mrgClass.ProfID = (int)ViewState["profCode"];
                        mrgClass.ClassStartTime = RadTimePicker1.DateInput.Text.Substring(11, 5).Replace('-', ':');
                        mrgClass.ClassEndTime = RadTimePicker2.DateInput.Text.Substring(11, 5).Replace('-', ':');

                        Session["mrgClass"] = mrgClass;

                        DataTable dtMergeCheck = new DataTable();
                        dtMergeCheck = clsB.CheckMergeCode(mrgClass);

                        if (dtMergeCheck.Rows.Count == 0)
                        {
                            merge(0);

                            clearList();
                        }
                        else
                        {
                            radConfirm.VisibleOnPageLoad = true;
                        }
                    }
                    else
                    {
                        rvSessionCount.IsValid = false;
                    }
                }
                else
                {
                    string msg = "کدی که برای ادغام انتخاب  کرده اید تکراریست ";
                    RadWindowManager1.RadAlert(msg, 0, 100, " پیام سیستم", "");
                }
            }
            else
            {
                string msg = "کلاسی را برای ادغام انتخاب نکرده اید";
                RadWindowManager1.RadAlert(msg, 0, 100, " پیام سیستم", "");
            }
            //}
            //else
            //{
            //    RequiredFieldValidator1.IsValid = false;
            //}
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            DataTable excel = new DataTable();
            nimsal = ViewState["nimsal"].ToString();
            excel = clsB.getMergeClass(nimsal);
            var ms = DataTableToExcelXlsx(excel);

            ms.WriteTo(HttpContext.Current.Response.OutputStream);
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + "MergeClass -" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + " - " + DateTime.Now.Hour + ".xls");
            HttpContext.Current.Response.StatusCode = 200;
            HttpContext.Current.Response.End();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        protected void btnConfirmOk_Click(object sender, EventArgs e)
        {
            merge(1);
            radConfirm.VisibleOnPageLoad = false;
            clearList();
        }

        protected void btnConfirmCancel_Click(object sender, EventArgs e)
        {
            radConfirm.VisibleOnPageLoad = false;
        }

        protected void grdInfo_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                List<ClassListDTO> check = new List<ClassListDTO>();
                GridDataItem dataItem = (GridDataItem)e.Item;
                clss = (List<ClassListDTO>)Session["clss"];
                mrgClass.ClassCode = e.CommandArgument.ToString();
                mrgClass.CourseCode = dataItem["codedars"].Text;
                mrgClass.CourseName = dataItem["namedars"].Text;
                mrgClass.Semester = ViewState["nimsal"].ToString();
                Session["mrgClass"] = mrgClass;

                int index = clss.FindIndex(x => x.ClassCode == mrgClass.ClassCode);
                if (index == -1)
                {
                    clss.Add(mrgClass);
                    Session["clss"] = clss;

                    grdList.DataSource = clss;
                    grdList.DataBind();
                }
                else
                {
                    string msg = "کلاس مورد نظر قبلا در لیست ادغام قرار گرفته است";
                    RadWindowManager1.RadAlert(msg, 0, 100, " پیام سیستم", "");
                }
            }
            if (e.CommandName == "del")
            {
                nimsal = ViewState["nimsal"].ToString();
                string code = e.CommandArgument.ToString();
                int val = clsB.DeleteFromMergeClass(nimsal, Convert.ToInt32(code));
                if (val != -1)
                {
                    string msg = "کلاس مورد نظر با موفقیت حذف شد";
                    RadWindowManager1.RadAlert(msg, 0, 100, " پیام سیستم", "");
                }
                else
                {
                    string msg = "کلاس های ادغام شده در کمترین حالت قرار دارد لذا قادر به حذف کلاس نیستید";
                    RadWindowManager1.RadAlert(msg, 0, 100, " پیام سیستم", "");
                }
                bindInfoGrid();

            }
        }

        protected void grdInfo_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (txtCode.Text != "" || txtName.Text != "")
            {
                code = ViewState["code"].ToString();
                name = ViewState["name"].ToString();
                nimsal = ViewState["nimsal"].ToString();
                dt = clsB.Show_Class_List_ByTerm(nimsal, Convert.ToInt32(code), name);
                grdInfo.DataSource = dt;
            }
        }

        protected void grdList_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            clss = (List<ClassListDTO>)Session["clss"];
            grdList.DataSource = clss;
        }

        protected void grdList_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                clss = (List<ClassListDTO>)Session["clss"];
                int i = clss.FindIndex(x => x.ClassCode.ToString() == e.CommandArgument.ToString());
                clss.RemoveAt(i);

                Session["clss"] = clss;

                grdList.DataSource = clss;
                grdList.DataBind();
            }
        }

        protected void grdInfo_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem itemAmount = (GridDataItem)e.Item;
                DataRowView rowView = (DataRowView)e.Item.DataItem;
                string str = (rowView["Merge_Code"].ToString());
                Button btnAdd = (Button)itemAmount.FindControl("btnAdd");
                Button btnDel = (Button)itemAmount.FindControl("btnDelete");
                if (str == string.Empty)
                {
                    btnAdd.Enabled = true;
                    btnDel.Enabled = false;
                }
                else
                {
                    btnAdd.Enabled = false;
                    btnDel.Enabled = true;
                }
            }
        }

        protected void drpDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            mrgClass = (ClassListDTO)Session["mrgClass"];
            mrgClass.ClassDay = Convert.ToInt32(drpDay.SelectedValue);
            Session["mrgClass"] = mrgClass;
        }

        protected void cmbOstad_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //try
            //{
            //    mrgClass = (ClassListDTO)Session["mrgClass"];
            //    mrgClass.ProfID = Convert.ToInt32(cmbOstad.SelectedValue);
            //    Session["mrgClass"] = mrgClass;
            //}
            //catch (Exception)
            //{

            //}
        }

        protected void vldTimes_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (RadTimePicker2.SelectedTime <= RadTimePicker1.SelectedTime)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        private void merge(int IsExist)
        {
            clss = (List<ClassListDTO>)Session["clss"];
            mrgClass = (ClassListDTO)Session["mrgClass"];
            //int classCode = (int)ViewState["classCode"];
            //mrgClass.ProfID = (int)Session["idOstad"];
            //nimsal = ViewState["nimsal"].ToString();
            //string mergeCode = ViewState["mergeCode"].ToString();
            //string count_sessions = ViewState["count_sessions"].ToString();
            //string date_first_session = ViewState["date_first_session"].ToString();
            //mrgClass.ClassDay = (int)ViewState["DayID"];
            //string stime = ViewState["stime"].ToString();
            //string etime = ViewState["etime"].ToString();

            foreach (var item in clss)
            {
                item.MergeCode = mrgClass.MergeCode;
                item.CourseCode = mrgClass.CourseCode;
                item.CourseName = mrgClass.CourseName;
                item.FirstSession = mrgClass.FirstSession;
                item.SessionCount = mrgClass.SessionCount;
                item.ProfID = mrgClass.ProfID;
                item.Semester = mrgClass.Semester;
                item.ClassDay = mrgClass.ClassDay;
                item.ClassStartTime = mrgClass.ClassStartTime;
                item.ClassEndTime = mrgClass.ClassEndTime;
                item.ClassCount = clss.Count();
            }

            if (clss.Count > 1 || IsExist == 1)
            {
                string temp = clss[clss.Count - 1].CourseName;

                for (int i = 0; i < clss.Count; i++)
                {
                    if (i != clss.Count - 1)
                    {

                        temp += " " + clss[i].ClassCode + " و ";
                    }
                    else
                    {
                        temp += " " + clss[i].ClassCode;
                    }
                }

                foreach (ClassListDTO item in clss)
                {
                    item.ClassName = temp;
                    clsB.MergeClass(item);
                }
                mergeClassDT = clsB.getMergeClass(nimsal);
                ViewState.Add("mergeClassDT", mergeClassDT);
                string msg = "کلاس ها با موفقیت ادغام شدند";
                RadWindowManager1.RadAlert(msg, 0, 100, " پیام سیستم", "");
                List<ClassListDTO> empty = new List<ClassListDTO>();
                grdList.DataSource = empty;
                grdList.DataBind();
                //cmbOstad.SelectedIndex = 0;
                txtmergeCode.Text = string.Empty;
                txtFirstSession.Text = string.Empty;
                txtSessionCount.Text = string.Empty;

                nimsal = ViewState["nimsal"].ToString();

                bindInfoGrid();
            }
            else if (clss.Count == 1)
            {
                string msg = "برای ادغام کردن کلاس، حداقل باید دو کلاس انتخاب کنید";
                RadWindowManager1.RadAlert(msg, 0, 100, " پیام سیستم", "");
            }
            else
            {
                string msg = "کلاسی را برای ادغام انتخاب نکرده اید";
                RadWindowManager1.RadAlert(msg, 0, 100, " پیام سیستم", "");
            }
        }

        protected MemoryStream DataTableToExcelXlsx(DataTable dt)
        {
            DataTable dtstcode = new DataTable();
            dtstcode.Columns.Add("codeclass", typeof(string));
            var pack = new OfficeOpenXml.ExcelPackage();
            var result = new MemoryStream();

            string[] c2 = new string[] { "کد کلاس", "ترم", "کد ادغامی", "کد درس", "نام کلاس", "تاریخ اولین برگزاری", "تعداد جلسات" };

            var ws = pack.Workbook.Worksheets.Add("اطلاعات ادغام شده");

            for (int col = 0; col < 7; col++)
            {


                ws.Cells[1, col + 1].Value = c2[col].ToString();

                int r = 2;
                if (dt.Rows.Count > 0)
                {
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        ws.Cells[r, col + 1].Value = dt.Rows[row][col].ToString();
                        r++;
                    }
                }
            }
            pack.SaveAs(result);
            return result;
        }

        private void bindInfoGrid()
        {
            name = ViewState["name"].ToString();
            code = ViewState["code"].ToString();
            dt = clsB.Show_Class_List_ByTerm(nimsal, Convert.ToInt32(code), name);
            grdInfo.DataSource = dt;
            grdInfo.DataBind();
            GridFilterMenu menu = grdInfo.FilterMenu;
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

        private void bindProfGrid(bool isNeedDataSource)
        {
            DataTable prof = new DataTable();
            if (!isNeedDataSource)
            {
                prof = clsB.getActiveProfName();
                ViewState.Add("prof", prof);
                grdProf.DataSource = prof;
                grdProf.DataBind();
                GridFilterMenu menu = grdProf.FilterMenu;
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
            else
            {
                prof = (DataTable)ViewState["prof"];
                grdProf.DataSource = prof;
            }
        }

        private void clearAll()
        {
            rdwProf.VisibleOnPageLoad = false;
            clss.Clear();
            divclasses.Visible = false;
            grdList.DataSource = clss;
            grdList.DataBind();
            grdInfo.DataSource = clss;
            grdInfo.DataBind();
            txtCode.Text = string.Empty;
            txtName.Text = string.Empty;
            divtitle.Visible = false;
            txtmergeCode.Text = string.Empty;
            txtFirstSession.Text = string.Empty;
            txtSessionCount.Text = string.Empty;
            RadTimePicker1.Clear();
            RadTimePicker2.Clear();

            drpDay.SelectedIndex = 0;
            txtCodeDars.Text = string.Empty;
            RadTimePicker1.Clear();
            RadTimePicker2.Clear();
        }

        private void clearList()
        {
            rdwProf.VisibleOnPageLoad = false;
            txtProf.Text = string.Empty;
            clss.Clear();
            grdList.DataSource = clss;
            grdList.DataBind();
            txtmergeCode.Text = string.Empty;
            txtFirstSession.Text = string.Empty;
            txtSessionCount.Text = string.Empty;
            drpDay.SelectedIndex = 0;
            txtCodeDars.Text = string.Empty;
            RadTimePicker1.Clear();
            RadTimePicker2.Clear();
        }

        protected void btnProf_Click(object sender, EventArgs e)
        {
            rdwProf.VisibleOnPageLoad = true;
            bindProfGrid(false);
        }

        protected void grdProf_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            bindProfGrid(true);
        }

        protected void grdProf_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Choose")
            {
                string profName;
                int profCode;

                GridDataItem item = (GridDataItem)e.Item;
                profName = item["name"].Text + ' ' + item["family"].Text;
                rdwProf.VisibleOnPageLoad = false;
                txtProf.Text = profName;
                profCode = Convert.ToInt32(item["code_ostad"].Text);
                ViewState.Add("profCode", profCode);
            }
        }
    }
}