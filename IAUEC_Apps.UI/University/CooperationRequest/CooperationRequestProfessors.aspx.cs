using IAUEC_Apps.Business.university.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAUEC_Apps.DTO.University.Faculty;
using System.Configuration;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.University.Support;
using Telerik.Web.UI.Grid;
using Telerik.Web.UI;
using System.Drawing;
using Telerik.Web.UI.GridExcelBuilder;

namespace IAUEC_Apps.UI.University.Faculty.CMS
{
    public partial class CooperationRequestProfessors : System.Web.UI.Page
    {
        public static PassProfessorDTO PPD = new PassProfessorDTO();
        InsertToSida ITS = new InsertToSida();
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();
        CommonBusiness ProfCommonBusiness = new CommonBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                getProfregAvailability();
        }

        protected void PersiaFiltering()
        {
            GridFilterMenu menu = grd_Show.FilterMenu;
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
        protected void grd_Show_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Session[sessionNames.roleID].ToString() == ((int)DTO.RoleEnums.مدیر_ارشد).ToString() || Session[sessionNames.roleID].ToString() == ((int)DTO.RoleEnums.کارشناس_فنی).ToString())
            {
                DataTable dt = FRB.GetInfoPeo(3);

                grd_Show.DataSource = dt;
                PersiaFiltering();
                btnShowProfregModal.Visible = true;
            }
            else if (Session[sessionNames.roleID].ToString() == ((int)DTO.RoleEnums.مدیر_کل_امور_پژوهشی).ToString() || Session[sessionNames.roleID].ToString() == ((int)DTO.RoleEnums.کارشناس_پژوهش).ToString())
            {
                // اگر پژوهش بود
                DataTable dt = FRB.GetInfoPeo(2);
                grd_Show.DataSource = dt;
                PersiaFiltering();
            }
            else if (Session[sessionNames.roleID].ToString() == ((int)DTO.RoleEnums.کارشناس_کارگزینی_هیات_علمی).ToString() ||
                Session[sessionNames.roleID].ToString() == ((int)DTO.RoleEnums.مسئول_حق_التدریس).ToString() || 
                Session[sessionNames.roleID].ToString() == ((int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی).ToString() || 
                Session[sessionNames.roleID].ToString() == ((int)DTO.RoleEnums.معاون_آموزشی_و_تحصیلات_تکمیلی).ToString() ||
                Session[sessionNames.roleID].ToString() == ((int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی).ToString())
            //if (Session["RoleID"].ToString() == "2" || Session["RoleID"].ToString() == "3")
            {
                //اگر آموزش بود هم آموزش هم پژوهش
                DataTable dt = FRB.GetInfoPeo(1);
                grd_Show.DataSource = dt;
                PersiaFiltering();
                btnShowProfregModal.Visible = true;
            }

        }

        private void getProfregAvailability()
        {
            var sysStatus = ProfCommonBusiness.GetSystemAvailability(13, 2, 0);
            bool sysIsOpen = true;
            bool canChangeProfregStatus = Session[sessionNames.roleID].ToString() == ((int)DTO.RoleEnums.مدیر_ارشد).ToString()
                || Session[sessionNames.roleID].ToString() == ((int)DTO.RoleEnums.کارشناس_فنی).ToString()
                || Session[sessionNames.roleID].ToString() == ((int)DTO.RoleEnums.کارشناس_کارگزینی_هیات_علمی).ToString()
                || Session[sessionNames.roleID].ToString() == ((int)DTO.RoleEnums.مسئول_حق_التدریس).ToString()
                || Session[sessionNames.roleID].ToString() == ((int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی).ToString()
                || Session[sessionNames.roleID].ToString() == ((int)DTO.RoleEnums.معاون_آموزشی_و_تحصیلات_تکمیلی).ToString()
                || Session[sessionNames.roleID].ToString() == ((int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی).ToString();
            if (sysStatus.Rows.Count > 0)
            {
                if (Convert.ToBoolean(sysStatus.Rows[0]["IsSysOpen"]) == true && Convert.ToBoolean(sysStatus.Rows[0]["isOpen"]) == false)//در بازه زمانی مشخص شده هستیم و سامانه در وضعیت بسته است
                {
                    sysIsOpen = false;
                }
                txtChangeProfregStatus_Fromdate.Text = sysStatus.Rows[0]["fromdate"] == DBNull.Value ? "" : sysStatus.Rows[0]["fromdate"].ToString();
                txtChangeProfregStatus_Todate.Text = sysStatus.Rows[0]["todate"] == DBNull.Value ? "" : sysStatus.Rows[0]["todate"].ToString();
            }

            switch (sysIsOpen)
            {
                case true:
                    lblProfregStatus.Text = "سامانه ثبت اطلاعات اساتید باز است";
                    lblProfregStatus.ForeColor = Color.SpringGreen;
                    btnOpenProfReg.Visible = false;
                    btnShowProfregModal.Visible = canChangeProfregStatus;
                    btnShowProfregModal.Text = "بستن سامانه ثبت اطلاعات اساتید";
                    break;
                case false:
                    lblProfregStatus.Text = "سامانه ثبت اطلاعات اساتید از تاریخ " + sysStatus.Rows[0]["FromDate"].ToString() + " تا تاریخ " + sysStatus.Rows[0]["ToDate"].ToString() + " بسته است";
                    lblProfregStatus.ForeColor = Color.Red;

                    btnOpenProfReg.Visible = canChangeProfregStatus;
                    btnShowProfregModal.Visible = canChangeProfregStatus;
                    btnShowProfregModal.Text = "تغییر بازه زمانی";
                    break;
            }
            mdlblProfregStatus.Text = lblProfregStatus.Text;

            //else
            //{
            //    btnOpenProfReg.Visible = false;
            //    btnShowProfregModal.Text = "بستن سامانه ثبت اطلاعات اساتید";
            //}
        }

        protected void grd_Show_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                Session["Page"] = 2;
                string[] index = e.CommandArgument.ToString().Split(new char[] { ',' });
                int Code_Ostad = int.Parse(index[0].ToString());
                PPD.mobile = index[8].ToString();
                lbl_Code.Text = Convert.ToString(Code_Ostad);
                if (e.CommandName == "Detail")
                {
                    DataTable dtProf = FRB.GetInfoPeoByFilter(Code_Ostad);
                    Session["hrId_HireRequest"] = dtProf.Rows[0]["ID"].ToString();
                    Response.Redirect("ShowDetailInfoPeople.aspx?ID=" + dtProf.Rows[0]["ID"].ToString());
                }
            }
        }

        protected void grd_Show_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            Label lbl_Faculty = e.Item.FindControl("lbl_Faculty") as Label;
            DataTable dt;

            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                if (Session["RoleID"].ToString() == "1")
                {
                    //dt = FRB.GetInfoPeo(3);

                    if (Convert.ToInt32(((DataRowView)item.DataItem)["Request"]) == 2)
                    {
                        e.Item.BackColor = Color.Cornsilk;
                    }
                    //dt.Rows[l]["Cooperation"].ToString()
                    if (Convert.ToInt32(((DataRowView)item.DataItem)["Request"]) == 1)
                    {
                        e.Item.BackColor = Color.Bisque;
                    }
                }
                //else if (Session["RoleID"].ToString() == "9" || Session["RoleID"].ToString() == "10")
                //{
                //    dt = FRB.GetInfoPeo(2);
                //}
                //else
                //{
                //    dt = FRB.GetInfoPeo(1);
                //}

                //if (dt.Rows.Count > 0)
                //{
                //if (((DataRowView)item.DataItem)["martabeh"].ToString() == "-2")
                //{
                //    lbl_Faculty.Text = "خیر";
                //}
                //else
                //{
                //    lbl_Faculty.Text = "بله";
                //}
                //}
            }
        }

        protected void grd_Show_ExcelMLWorkBookCreated(object sender, Telerik.Web.UI.GridExcelBuilder.GridExcelMLWorkBookCreatedEventArgs e)
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

        protected void btn_excel_Click(object sender, EventArgs e)
        {
            grd_Show.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), "ExcelML");
            grd_Show.ExportSettings.FileName = DateTime.Now.ToPeString().Replace("/","-")+" _  لیست متقاضیان همکاری با واحد";
            grd_Show.ExportSettings.IgnorePaging = true;
            grd_Show.ExportSettings.ExportOnlyData = true;
            grd_Show.ExportSettings.OpenInNewWindow = true;
            grd_Show.ExportSettings.UseItemStyles = true;
            grd_Show.MasterTableView.ExportToExcel();
        }

        //close profreg service or change the date
        protected void mdbtnChangeProfregStatus_Click(object sender, EventArgs e)
        {
            ProfCommonBusiness.UpdateSystemAvailability(13, 2, 0, false, txtChangeProfregStatus_Fromdate.Text, txtChangeProfregStatus_Todate.Text);
            setLog(DTO.eventEnum.باز_یا_بسته_کردن_سیستم_ثبت_اطلاعات_اساتید, "بستن سیستم ثبت اطلاعات اساتید از تاریخ " + txtChangeProfregStatus_Fromdate.Text + " تا تاریخ " + txtChangeProfregStatus_Todate.Text);
            getProfregAvailability();
        }

        protected void btnShowProfregModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", "showModalChangeProfregStatus();", true);
        }

        protected void btnOpenProfreg_Click(object sender, EventArgs e)
        {
            ProfCommonBusiness.UpdateSystemAvailability(13, 2, 0, true, "", "");
            setLog(DTO.eventEnum.باز_یا_بسته_کردن_سیستم_ثبت_اطلاعات_اساتید, "باز کردن سیستم ثبت اطلاعات اساتید ");
            getProfregAvailability();
        }
        private void setLog(DTO.eventEnum eventType, string Description)
        {
            int userId;//کاربری که لاگین کرده
            //eventType//کد کاری که انجام شده. 
            int appId;//کد قسمتی از برنامه که الان توش هستیم. یعنی کارگزینی  -  13
            int modifyId;//کد درخواست ویرایش شده. ویرایش اطلاعات فردی،اطلاعات تماس و ...
            userId = int.Parse(Session[sessionNames.userID_Karbar].ToString());
            appId = 13;
            ProfCommonBusiness.InsertIntoUserLog(userId, DateTime.Now.ToString("HH:mm"), appId, (int)eventType, Description, 0);
        }
    }
}
