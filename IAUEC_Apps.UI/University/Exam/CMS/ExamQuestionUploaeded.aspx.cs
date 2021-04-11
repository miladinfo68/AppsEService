using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAUEC_Apps.Business.university.Exam;
using IAUEC_Apps.Business.Common;
using Telerik.Web.UI;
using Telerik.Web.UI.GridExcelBuilder;
using System.Drawing;
using System.Configuration;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class ExamQuestionUploaeded : System.Web.UI.Page
    {

        ExamBusiness Ebusiness = new ExamBusiness();
        CommonBusiness cmnb = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_Karbar] == null)
                Response.Redirect("~/CommonUI/loginRequestCMS.aspx");
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
                DataTable dtdanesh = new DataTable();
                dtdanesh = Ebusiness.GetAllDaneshkade();
                ddl_Danesh.DataSource = dtdanesh;
                ddl_Danesh.DataTextField = "namedanesh";
                ddl_Danesh.DataValueField = "id";
                ddl_Danesh.DataBind();
                ddl_Danesh.Items.Add(new ListItem("همه", "0"));
                //ddl_Danesh.Items[ddl_Danesh.Items.Count - 1].Selected = true;
                
                UserAccessBusiness uacb = new UserAccessBusiness();
                int daneshID = uacb.GetDaneshIDByRoleID(int.Parse(Session["RoleID"].ToString()));
                if (daneshID > 0)
                {

                    if (ddl_Danesh.Items.FindByValue(daneshID.ToString()) != null)
                        ddl_Danesh.Items.FindByValue(daneshID.ToString()).Selected = true;
                    else
                        ddl_Danesh.Items[ddl_Danesh.Items.Count - 1].Selected = true;
                    //ddl_Danesh.SelectedValue = daneshID.ToString();
                    ddl_Danesh.Enabled = false;
                    btn_Save.Visible = false;
                }
                else
                {
                    ddl_Danesh.SelectedValue = daneshID.ToString();
                }
                int status = 0;
                string term = null;
                if (ddl_status.SelectedValue != "")
                    status = int.Parse(ddl_status.SelectedValue);
                if (ddlTerm.SelectedValue != "")
                    term = ddlTerm.SelectedValue;
                DataTable dt = new DataTable();
                dt = Ebusiness.GetExamQuestionUploaded(int.Parse(ddl_Danesh.SelectedValue.ToString()),status, term);
                grd_ExamQuestion.DataSource = dt;
                if (dt.Rows.Count > 0)
                {
                    
                        grd_ExamQuestion.DataBind();
                        GridFilterMenu menu = grd_ExamQuestion.FilterMenu;
                        int im = 0;
                        if (menu.Items.Count > 3)
                        {
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
                    rwm.RadAlert("سوالی ارسال نشده است", null, 100, "", "");

                ddlTerm.DataSource = cmnb.SelectAllTerm();
                ddlTerm.DataTextField = "tterm";
                ddlTerm.DataValueField = "tterm";
                ddlTerm.DataBind();
                ddlTerm.Items.Insert(0, new ListItem { Value = "", Text = "انتخاب کنید" });
            }
        }



        protected void btn_Save_Click(object sender, EventArgs e)
        {
            int status=0;
            string term = null;
            if (ddl_status.SelectedValue != "")
                status = int.Parse(ddl_status.SelectedValue);
            if (ddlTerm.SelectedValue != "")
                term = ddlTerm.SelectedValue;
            DataTable dt = new DataTable();
            dt = Ebusiness.GetExamQuestionUploaded(int.Parse(ddl_Danesh.SelectedValue.ToString()),status, term);
            grd_ExamQuestion.DataSource = dt;
            grd_ExamQuestion.DataBind();
            if (dt.Rows.Count > 0)
            {
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 55, "مشاهده گزارش سوالات آپلود شده");
            }
            else
            {
                rwm.RadAlert("سوالی ارسال نشده است", null, 100, "", "");
            }

        }

        protected void grd_ExamQuestion_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            int status = 0;
            string term = null;
            if (ddl_status.SelectedValue != "")
                status = int.Parse(ddl_status.SelectedValue);
            if (ddlTerm.SelectedValue != "")
                term = ddlTerm.SelectedValue;
            DataTable dt = new DataTable();
            dt = Ebusiness.GetExamQuestionUploaded(int.Parse(ddl_Danesh.SelectedValue.ToString()),status, term);
            grd_ExamQuestion.DataSource = dt;
            GridFilterMenu menu = grd_ExamQuestion.FilterMenu;
            int im = 0;
            if (menu.Items.Count > 3)
            {
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

        protected void grd_ExamQuestion_ExcelMLWorkBookCreated(object sender, Telerik.Web.UI.GridExcelBuilder.GridExcelMLWorkBookCreatedEventArgs e)
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
            style.InteriorStyle.Color = System.Drawing.Color.FromArgb(237,225,242);
            style.FontStyle.FontName = "Tahoma";
            style.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Center;
            e.WorkBook.Styles.Add(style);
            StyleElement style2 = new StyleElement("Style2");
            style2.AlignmentElement.HorizontalAlignment = HorizontalAlignmentType.Center;
            style2.InteriorStyle.Pattern = InteriorPatternType.Solid;
            style2.InteriorStyle.Color = System.Drawing.Color.FromArgb(193, 153, 210);
            style2.FontStyle.FontName = "Tahoma";
            e.WorkBook.Styles.Add(style2);
            
        }

        protected void btn_excel_Click(object sender, EventArgs e)
        {


            grd_ExamQuestion.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), "ExcelML");
            grd_ExamQuestion.ExportSettings.IgnorePaging = true;
            grd_ExamQuestion.ExportSettings.ExportOnlyData = true;
            grd_ExamQuestion.ExportSettings.OpenInNewWindow = true;
            grd_ExamQuestion.ExportSettings.UseItemStyles = true;
            grd_ExamQuestion.ExportSettings.FileName = "ExamQuestion" ;
            grd_ExamQuestion.MasterTableView.ExportToExcel();
        }

        protected void grd_ExamQuestion_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "view_history")
            {

                CommonBusiness cmb = new CommonBusiness();
                lst_history.DataSource = cmb.GetUserLogByModifyId(int.Parse(e.CommandArgument.ToString()), 8);
                lst_history.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

            }
            //if (e.CommandName == "SMS")
            //{
            //    try
            //    {
            //        ExamBusiness examBusiness = new ExamBusiness();
            //        DataTable dtResault = examBusiness.GetMobileProfByDid(int.Parse(e.CommandArgument.ToString()));

            //        string Text = "استاد ارجمند به استحضار می رساند پاکت های پاسخ نامه کد مشخصه " + e.CommandArgument.ToString() + " آماده تحویل می باشد، خواهشمند است با مدیر محترم دانشکده ارتباط و اقدام لازم بعمل آورید.  " + "\r\n" + "اداره امتحانات واحد الکترونیکی دانشگاه آزاد اسلامی";


            //        if (dtResault.Rows[0]["mobile"].ToString() != null || dtResault.Rows[0]["mobile"].ToString() != "")
            //        {
            //            lbl_Resault.Text = cmnb.SendSMSByMobile(dtResault.Rows[0]["mobile"].ToString(), Text, username, pass, source, uri);
            //            string codeAsanak = lbl_Resault.Text.Substring(1, (lbl_Resault.Text.Length) - 2);
            //            lbl_Status.Text = cmnb.ShowStatusSMS(codeAsanak, username, pass, uriStatus);
            //            if (lbl_Status.Text.Substring(12, (lbl_Status.Text.Length) - 15) == "NotFound")
            //            {
            //                string ss = "-1";
            //                int status = Convert.ToInt32(ss);
            //                DataTable dt = new DataTable();
            //                dt = cmnb.GetMessage(ss);
            //                string messageStatus = dt.Rows[0][0].ToString();
            //                //cmnb.LogStatusMessage(dtResault.Rows[0]["code_ostad"].ToString(), codeAsanak, dtResault.Rows[0]["mobile"].ToString(), status, messageStatus, int.Parse(IdAppMessage));
            //            }
            //            else
            //            {
            //                string ss = (lbl_Status.Text.Substring(32, (lbl_Status.Text.Length) - 104));
            //                int status = Convert.ToInt32(ss);
            //                DataTable dt = new DataTable();
            //                dt = cmnb.GetMessage(ss);
            //                string messageStatus = dt.Rows[0][0].ToString();
            //                // cmnb.LogStatusMessage(dtResault.Rows[0]["code_ostad"].ToString(), codeAsanak, dtResault.Rows[0]["mobile"].ToString(), status, messageStatus, int.Parse(IdAppMessage));
            //            }
            //        }
            //        rwm.RadAlert("ارسال پیامک برای کد مشخه"+e.CommandArgument.ToString()+"با موفقیت انجام شد",null,null,"پیام","");
            //    }
            //    catch
            //    {
            //        rwm.RadAlert("خطا در ارسال پیامک برای کد مشخه" + e.CommandArgument.ToString() , null, null, "پیام", "");
            //    }
            //}
        }

        protected void grd_ExamQuestion_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                HiddenField status = e.Item.FindControl("status") as HiddenField;
                //Button btn_sms = e.Item.FindControl("btn_sms") as Button;
                //if (status.Value != "5")
                //    btn_sms.Visible = false;


                if(string.IsNullOrEmpty(status.Value) || Convert.ToInt32(status.Value) == 0)
                {
                    Button btn_history = (Button)e.Item.FindControl("btn_history");
                    if (btn_history != null)
                        btn_history.Visible = false;
                }
            }
        }

        
    }
}