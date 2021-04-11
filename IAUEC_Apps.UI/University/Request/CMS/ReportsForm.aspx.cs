using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.DTO.University.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAUEC_Apps.Business.Common;
using Telerik.Web.UI;
using ResourceControl.BLL;
using Telerik.Web.UI.GridExcelBuilder;

namespace IAUEC_Apps.UI.University.Request.CMS
{
    public partial class ReportsForm : System.Web.UI.Page
    {
        CommonBusiness commonBusiness = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CheckOutRequestBusiness reqBusiness = new CheckOutRequestBusiness();
                List<MahaleSodoor> dtVahed = reqBusiness.GetListOfVahed();
                List<Field> dtReshte = GetListOfReshte();
                List<Daneshkade> dtDanesh = GetListOfCollege();
                drpVahed.DataSource = dtVahed;
                drpVahed.DataTextField = "Vahed";
                drpVahed.DataValueField = "Id";
                drpVahed.DataBind();
                drpFild.DataSource = dtReshte;
                drpFild.DataTextField = "field";
                drpFild.DataValueField = "Id";
                drpFild.DataBind();
                drpUniversity.DataSource = dtDanesh;
                drpUniversity.DataTextField = "daneshkade";
                drpUniversity.DataValueField = "Id";
                drpUniversity.DataBind();
                drpMadrak.Items.Add(new Telerik.Web.UI.RadComboBoxItem("گواهی موقت", "1"));
                drpMadrak.Items.Add(new Telerik.Web.UI.RadComboBoxItem("دانشنامه", "2"));
                drpMadrak.Items.Add(new Telerik.Web.UI.RadComboBoxItem("ریز نمرات", "3"));



            }

            //RegPcal1();
            //RegPcal2();
            //RegPcal3();
            //RegPcal4();

        }


        private List<Field> GetListOfReshte()
        {
            var res = new List<Field>();
            var dt = commonBusiness.SelectAllField();
            foreach (DataRow row in dt.Rows)
                res.Add(new Field
                {
                    field = row["nameresh"].ToString(),
                    Id = Convert.ToInt32(row["id"])
                });
            return res;
        }
        private List<Daneshkade> GetListOfCollege()
        {
            var res = new List<Daneshkade>();
            var dt = commonBusiness.SelectAllDaneshkade();
            foreach (DataRow row in dt.Rows)
                res.Add(new Daneshkade
                {
                    daneshkade = row["namedanesh"].ToString(),
                    Id = Convert.ToInt32(row["id"])
                });
            return res;
        }

        private DataTable getReport()
        {
            CheckOutRequestBusiness reqBusiness = new CheckOutRequestBusiness();
            DataTable dt = new DataTable();
            DataTable fields = new DataTable();
            fields.Columns.Add(new DataColumn("FieldId", typeof(int)));
            foreach (Telerik.Web.UI.RadComboBoxItem item in drpFild.Items)
            {
                if (item.Checked)
                {
                    var row = fields.NewRow();
                    row["FieldId"] = Convert.ToInt32(item.Value);
                    fields.Rows.Add(row);
                }
            }
            DataTable danesh = new DataTable();
            danesh.Columns.Add(new DataColumn("FieldId", typeof(int)));
            foreach (Telerik.Web.UI.RadComboBoxItem item in drpUniversity.Items)
            {
                if (item.Checked)
                {
                    var row = danesh.NewRow();
                    row["FieldId"] = Convert.ToInt32(item.Value);
                    danesh.Rows.Add(row);
                }
            }

            DataTable vahedList = new DataTable();
            vahedList.Columns.Add(new DataColumn("FieldId", typeof(int)));
            foreach (Telerik.Web.UI.RadComboBoxItem item in drpVahed.Items)
            {
                if (item.Checked)
                {
                    var row = vahedList.NewRow();
                    row["FieldId"] = Convert.ToInt32(item.Value);
                    vahedList.Rows.Add(row);
                }
            }


            var madrakTypeid = Convert.ToInt32(drpMadrak.SelectedItem.Value);
            int idCaseStatus = Convert.ToInt32(drpStatusCase.SelectedValue.ToString());
            int idMadrakStatus = Convert.ToInt32(drpStatusMadrak.SelectedValue.ToString());

            if (rdvoroodKartabl.Checked == true)
            {

                string sodoorstartDate = txtFromKartabl.Text.ToString().formatDateString();
                string sodoorEndDate = txtToKartabl.Text.ToString().formatDateString();
                dt = reqBusiness.GetListOfCaseInKarTabl(vahedList, sodoorstartDate, sodoorEndDate, fields, danesh, idCaseStatus, idMadrakStatus, madrakTypeid);
                
                grd_Info.Visible = true;
                ExcleExportBtn.Visible = true;
                RegPcal1();
            }
            else if (rdSodoor.Checked == true)
            {

                string sodoorstartDate = TxtFromSodoor.Text.ToString().formatDateString();
                string sodoorEndDate = txtToSodoor.Text.ToString().formatDateString();
                dt = reqBusiness.GetListOfMadarekByDateSodoor(vahedList, sodoorstartDate, sodoorEndDate, fields, danesh, idCaseStatus, idMadrakStatus, madrakTypeid);
                grd_Info.Visible = true;
                ExcleExportBtn.Visible = true;
                RegPcal2();

            }
            else if (rdVoroodMadrak.Checked == true)
            {

                string sodoorstartDate = txtFromVoroodMadrak.Text.ToString().formatDateString();
                string sodoorEndDate = txtToDateVoroodMadrak.Text.ToString().formatDateString();
                dt = reqBusiness.GetListOfMadrakVoroodUni(vahedList, sodoorstartDate, sodoorEndDate, fields, danesh, idCaseStatus, idMadrakStatus, madrakTypeid);
                grd_Info.Visible = true;
                ExcleExportBtn.Visible = true;
                RegPcal3();

            }
            else if (rdExitMadrak.Checked == true)
            {

                string sodoorstartDate = txtFromDateExit.Text.ToString().formatDateString();
                string sodoorEndDate = txtToDateExit.Text.ToString().formatDateString();
                dt = reqBusiness.GetListOfExitCaseFromKartabl(vahedList, sodoorstartDate, sodoorEndDate, fields, danesh, idCaseStatus, idMadrakStatus, madrakTypeid);
                grd_Info.Visible = true;
                ExcleExportBtn.Visible = true;
                RegPcal4();
            }
            else
            {
                var msg = "لطفا یکی از گزینه ها را انتخاب کنید";
                RadWindowManager1.RadAlert(msg, 300, 100, "پیام سیستم", "");
            }
            return dt;
        }

        private void setMenuFilter()
        {
            GridFilterMenu menu = grd_Info.FilterMenu;
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
            grd_Info.Columns[10].Visible = drpStatusMadrak.SelectedItem.Value == "9";
        }

        protected void btnshow_Click(object sender, EventArgs e)
        {
            grd_Info.CurrentPageIndex = 1;
            setMenuFilter();
            grd_Info.DataSource = getReport();
            grd_Info.DataBind();
        }
        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!string.IsNullOrEmpty(txtFromDateExit.Text) && txtFromDateExit.Text.ToGregorian() > DateTime.Now)
            {
                args.IsValid = false;
                return;
            }
            else
            {
                args.IsValid = true;
            }

        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!string.IsNullOrEmpty(txtToKartabl.Text) && txtToKartabl.Text.ToGregorian() > DateTime.Now)
            {
                args.IsValid = false;
                return;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!string.IsNullOrEmpty(txtToSodoor.Text) && txtToSodoor.Text.ToGregorian() > DateTime.Now)
            {
                args.IsValid = false;
                return;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void CustomValidator4_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!string.IsNullOrEmpty(TxtFromSodoor.Text) && TxtFromSodoor.Text.ToGregorian() > DateTime.Now)
            {
                args.IsValid = false;
                return;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void CustomValidator5_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!string.IsNullOrEmpty(txtToDateVoroodMadrak.Text) && txtToDateVoroodMadrak.Text.ToGregorian() > DateTime.Now)
            {
                args.IsValid = false;
                return;
            }
            else
            {
                args.IsValid = true;
            }
        }
        protected void CustomValidator6_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!string.IsNullOrEmpty(txtFromVoroodMadrak.Text) && txtFromVoroodMadrak.Text.ToGregorian() > DateTime.Now)
            {
                args.IsValid = false;
                return;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void CustomValidator7_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!string.IsNullOrEmpty(txtFromKartabl.Text) && txtFromKartabl.Text.ToGregorian() > DateTime.Now)
            {
                args.IsValid = false;
                return;
            }
            else
            {
                args.IsValid = true;
            }
        }
        protected void CustomValidator8_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!string.IsNullOrEmpty(txtToDateExit.Text) && txtToDateExit.Text.ToGregorian() > DateTime.Now)
            {
                args.IsValid = false;
                return;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void grd_Info_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            setMenuFilter();
            grd_Info.DataSource = getReport();
            CurrentPage.CurrentPageNumberValue = grd_Info.MasterTableView.CurrentPageIndex;
            CurrentPage.PageSizeValue = grd_Info.MasterTableView.PageSize;

        }
        public static class CurrentPage
        {
            public static int CurrentPageNumberValue { get; set; }
            public static int PageSizeValue { get; set; }
        }
        protected void grd_Info_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void grd_Info_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }
        private void RegPcal1()
        {
            string scrp1 = "var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_txtFromKartabl', {extraInputID: 'ContentPlaceHolder1_txtFromKartabl',extraInputFormat: 'yyyy/mm/dd'}); ";
            string scrp2 = "var objCal2 = new AMIB.persianCalendar('ContentPlaceHolder1_txtToKartabl', {extraInputID: 'ContentPlaceHolder1_txtToKartabl',extraInputFormat: 'yyyy/mm/dd'}); ";
            ScriptManager.RegisterClientScriptBlock(this, GetType(), ClientID, "setTimeout(function(){" + scrp1 + scrp2 + "}, 500);", true);
        }
        private void RegPcal2()
        {

            string scrp3 = "var objCal3 = new AMIB.persianCalendar('ContentPlaceHolder1_TxtFromSodoor', {extraInputID: 'ContentPlaceHolder1_TxtFromSodoor',extraInputFormat: 'yyyy/mm/dd'}); ";
            string scrp4 = "var objCal4 = new AMIB.persianCalendar('ContentPlaceHolder1_txtToSodoor', {extraInputID: 'ContentPlaceHolder1_txtToSodoor',extraInputFormat: 'yyyy/mm/dd'}); ";

            ScriptManager.RegisterClientScriptBlock(this, GetType(), ClientID, "setTimeout(function(){" + scrp3 + scrp4 + "}, 500);", true);
        }
        private void RegPcal3()
        {

            string scrp5 = "var objCal5 = new AMIB.persianCalendar('ContentPlaceHolder1_txtFromVoroodMadrak', {extraInputID: 'ContentPlaceHolder1_txtFromVoroodMadrak',extraInputFormat: 'yyyy/mm/dd'}); ";
            string scrp6 = "var objCal6 = new AMIB.persianCalendar('ContentPlaceHolder1_txtToDateVoroodMadrak', {extraInputID: 'ContentPlaceHolder1_txtToDateVoroodMadrak',extraInputFormat: 'yyyy/mm/dd'}); ";

            ScriptManager.RegisterClientScriptBlock(this, GetType(), ClientID, "setTimeout(function(){" + scrp5 + scrp6 + "}, 500);", true);
        }
        private void RegPcal4()
        {

            string scrp7 = "var objCal6 = new AMIB.persianCalendar('ContentPlaceHolder1_txtFromDateExit', {extraInputID: 'ContentPlaceHolder1_txtFromDateExit',extraInputFormat: 'yyyy/mm/dd'}); ";
            string scrp8 = "var objCal6 = new AMIB.persianCalendar('ContentPlaceHolder1_txtToDateExit', {extraInputID: 'ContentPlaceHolder1_txtToDateExit',extraInputFormat: 'yyyy/mm/dd'}); ";
            ScriptManager.RegisterClientScriptBlock(this, GetType(), ClientID, "setTimeout(function(){" + scrp7 + scrp8 + "}, 500);", true);
        }


        protected void rdvoroodKartabl_CheckedChanged(object sender, EventArgs e)
        {
            if (rdvoroodKartabl.Checked == true)
            {
                lblMadrak.Visible = true;
                lblVahed.Visible = true;
                drpMadrak.Visible = true;
                drpVahed.Visible = true;
                fromDate.Visible = true;
                txtFromKartabl.Visible = true;
                toDate.Visible = true;
                txtToKartabl.Visible = true;

                fromDateSodoor.Visible = false;
                toDateSodoor.Visible = false;
                TxtFromSodoor.Visible = false;
                txtToSodoor.Visible = false;

                fromDatevoroodmadrak.Visible = false;
                toDateVoroodMadrak.Visible = false;
                txtFromVoroodMadrak.Visible = false;
                txtToDateVoroodMadrak.Visible = false;

                fromDateExit.Visible = false;
                toDateExit.Visible = false;
                txtFromDateExit.Visible = false;
                txtToDateExit.Visible = false;

            }
            else if (rdvoroodKartabl.Checked == false)
            {

                fromDate.Visible = false;
                txtFromKartabl.Visible = false;
                toDate.Visible = false;
                txtToKartabl.Visible = false;
            }
            RegPcal1();



        }

        protected void rdSodoor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSodoor.Checked == true)
            {
                fromDate.Visible = false;
                txtFromKartabl.Visible = false;
                toDate.Visible = false;
                txtToKartabl.Visible = false;

                fromDateSodoor.Visible = true;
                toDateSodoor.Visible = true;
                TxtFromSodoor.Visible = true;
                txtToSodoor.Visible = true;

                fromDatevoroodmadrak.Visible = false;
                toDateVoroodMadrak.Visible = false;
                txtFromVoroodMadrak.Visible = false;
                txtToDateVoroodMadrak.Visible = false;

                fromDateExit.Visible = false;
                toDateExit.Visible = false;
                txtFromDateExit.Visible = false;
                txtToDateExit.Visible = false;
                drpMadrak.Visible = true;
                drpVahed.Visible = true;
                lblMadrak.Visible = true;
                lblVahed.Visible = true;

            }
            else if (rdSodoor.Checked == false)
            {
                fromDateSodoor.Visible = false;
                toDateSodoor.Visible = false;
                TxtFromSodoor.Visible = false;
                txtToSodoor.Visible = false;
            }
            RegPcal2();
        }

        protected void rdVoroodMadrak_CheckedChanged(object sender, EventArgs e)
        {
            if (rdVoroodMadrak.Checked == true)
            {
                fromDate.Visible = false;
                txtFromKartabl.Visible = false;
                toDate.Visible = false;
                txtToKartabl.Visible = false;

                fromDateSodoor.Visible = false;
                toDateSodoor.Visible = false;
                TxtFromSodoor.Visible = false;
                txtToSodoor.Visible = false;

                fromDatevoroodmadrak.Visible = true;
                toDateVoroodMadrak.Visible = true;
                txtFromVoroodMadrak.Visible = true;
                txtToDateVoroodMadrak.Visible = true;

                fromDateExit.Visible = false;
                toDateExit.Visible = false;
                txtFromDateExit.Visible = false;
                txtToDateExit.Visible = false;
                drpMadrak.Visible = true;
                drpVahed.Visible = true;
                lblMadrak.Visible = true;
                lblVahed.Visible = true;

            }
            else if (rdVoroodMadrak.Checked == false)
            {
                fromDatevoroodmadrak.Visible = false;
                toDateVoroodMadrak.Visible = false;
                txtFromVoroodMadrak.Visible = false;
                txtToDateVoroodMadrak.Visible = false;
            }
            RegPcal3();
        }

        protected void rdExitMadrak_CheckedChanged(object sender, EventArgs e)
        {
            if (rdExitMadrak.Checked == true)
            {
                lblMadrak.Visible = true;
                lblVahed.Visible = true;
                fromDate.Visible = false;
                txtFromKartabl.Visible = false;
                toDate.Visible = false;
                txtToKartabl.Visible = false;

                fromDateSodoor.Visible = false;
                toDateSodoor.Visible = false;
                TxtFromSodoor.Visible = false;
                txtToSodoor.Visible = false;

                fromDatevoroodmadrak.Visible = false;
                toDateVoroodMadrak.Visible = false;
                txtFromVoroodMadrak.Visible = false;
                txtToDateVoroodMadrak.Visible = false;

                fromDateExit.Visible = true;
                toDateExit.Visible = true;
                txtFromDateExit.Visible = true;
                txtToDateExit.Visible = true;
                drpMadrak.Visible = true;
                drpVahed.Visible = true;
            }
            else if (rdExitMadrak.Checked == false)
            {

                fromDateExit.Visible = false;
                toDateExit.Visible = false;
                txtFromDateExit.Visible = false;
                txtToDateExit.Visible = false;
            }
            RegPcal4();

        }

        protected void drpStatusCase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpStatusCase.SelectedIndex == 0)
            {
                drpStatusMadrak.Enabled = true;
            }
            else
            {
                drpStatusMadrak.Enabled = false;
            }

        }

        protected void drpStatusMadrak_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpStatusMadrak.SelectedIndex == 0)
            {
                drpStatusCase.Enabled = true;
            }
            else
            {
                drpStatusCase.Enabled = false;
            }

        }

        protected void ExcleExportBtn_Click(object sender, EventArgs e)
        {
            grd_Info.ExportSettings.IgnorePaging = true;
            grd_Info.ExportSettings.ExportOnlyData = true;
            grd_Info.ExportSettings.OpenInNewWindow = true;
            grd_Info.ExportSettings.UseItemStyles = true;
            grd_Info.ExportSettings.FileName = "فرم گزارشات"+"_"+drpMadrak.Text;
            grd_Info.MasterTableView.ExportToExcel();
            
        }

        protected void grd_Info_ExcelMLWorkBookCreated(object sender, Telerik.Web.UI.GridExcelBuilder.GridExcelMLWorkBookCreatedEventArgs e)
        {
            int r = 0;
            foreach (Telerik.Web.UI.GridExcelBuilder.RowElement row in e.WorkBook.Worksheets[0].Table.Rows)
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

        protected void drpMadrak_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ListItem l9 = new ListItem("مدرک ارسال شده", "9");
            ListItem l10 = new ListItem("مدرک ارسال نشده", "10");
            if (drpMadrak.SelectedItem.Value=="3")
            {
                if (drpStatusMadrak.Items.FindByValue("9") == null)
                {
                    drpStatusMadrak.Items.Add(l9);
                }
                if (drpStatusMadrak.Items.FindByValue("10") == null)
                {
                    drpStatusMadrak.Items.Add(l10);
                }
            }
            else
            {
                if (drpStatusMadrak.Items.FindByValue("9") != null)
                {
                    drpStatusMadrak.Items.Remove(l9);
                }
                if (drpStatusMadrak.Items.FindByValue("10") != null)
                {
                    drpStatusMadrak.Items.Remove(l10);
                }
            }
        }

        protected void btnReportCodeBayegani_Click(object sender, EventArgs e)
        {
            string name = DateTime.Now.ToPeString().Replace("/", "") + " - " + "کد بایگانی دانشجویان(جایگزین سیدا)";
            CheckOutRequestBusiness business = new CheckOutRequestBusiness();

            var dt = business.getAllCodeBayegan();
            var ms = DataTableToExcelXlsx(dt, "کد بایگانی دانشجویان");

            ms.WriteTo(Response.OutputStream);
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + name + ".xlsx");
            Response.StatusCode = 200;
            Response.End();
        }

        protected void btnGraduateDocument_Click(object sender, EventArgs e)
        {
            string name = DateTime.Now.ToPeString().Replace("/", "") + " - " + "کد بایگانی مدارک فارغ التحصیلی";
            CheckOutRequestBusiness business = new CheckOutRequestBusiness();

            var dt = business.getAllArchiveID_GraduateDocuments();
            var ms = DataTableToExcelXlsx(dt, "کد بایگانی مدارک");

            ms.WriteTo(Response.OutputStream);
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + name + ".xlsx");
            Response.StatusCode = 200;
            Response.End();
        }

        protected void btnDocArchive_Click(object sender, EventArgs e)
        {
            string name = DateTime.Now.ToPeString().Replace("/", "") + " - " + "کد فیش و تمبر دانشجویان";
            Business.university.GraduateAffair.GraduateFormsBusiness business = new Business.university.GraduateAffair.GraduateFormsBusiness();

            var dt = business.getDocumentArchive();
            var ms = DataTableToExcelXlsx(dt, "کد فیش و تمبر");

            ms.WriteTo(Response.OutputStream);
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + name + ".xlsx");
            Response.StatusCode = 200;
            Response.End();
        }
        protected void btnDocArchive_Nagh_Click(object sender, EventArgs e)
        {
            string name = DateTime.Now.ToPeString().Replace("/", "") + " - " + "نقص فیش و تمبر دانشجویان";

            Business.university.GraduateAffair.GraduateFormsBusiness business = new Business.university.GraduateAffair.GraduateFormsBusiness();

            var dt = business.getDocumentArchive_naghs();
            var ms = DataTableToExcelXlsx(dt, "نقص فیش و تمبر");

            ms.WriteTo(Response.OutputStream);
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + name+ ".xlsx");
            Response.StatusCode = 200;
            Response.End();
        }

        private System.IO.MemoryStream DataTableToExcelXlsx(DataTable dt,string sheetName)
        {
            var pack = new OfficeOpenXml.ExcelPackage();
            var result = new System.IO.MemoryStream();

            var ws = pack.Workbook.Worksheets.Add(sheetName);

            for (int col = 0; col < dt.Columns.Count; col++)
            {


                ws.Cells[1, col + 1].Value = dt.Columns[col].Caption.ToString();
                int r = 2;
                if (dt.Rows.Count > 0)
                {
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        ws.Cells[r, col + 1].Value = dt.Rows[row][col];
                        r++;
                    }
                }
            }
            pack.SaveAs(result);
            return result;
        }
        
    }

}