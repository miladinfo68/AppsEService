using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Education;
using IAUEC_Apps.Business.university.Faculty;
using System.Configuration;
using Telerik.Web.UI;
using Telerik.Web.UI.GridExcelBuilder;
using System.Text.RegularExpressions;

namespace IAUEC_Apps.UI.University.CooperationRequest
{
    public partial class CooperationRequest : System.Web.UI.Page
    {
        CommonBusiness CB = new CommonBusiness();
        DataTable dtDepartman = new DataTable();
        DataTable dtDaneshkade = new DataTable();
        EducationReportBusiness ERB = new EducationReportBusiness();
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();
        CommonBusiness ECB = new CommonBusiness();
       

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
                }
                Session[sessionNames.menuID] = menuId;

                dtDaneshkade = CB.SelectAllDaneshkade();
                ddl_Daneshkade.DataTextField = "namedanesh";
                ddl_Daneshkade.DataValueField = "id";
                ddl_Daneshkade.DataSource = dtDaneshkade;
                ddl_Daneshkade.DataBind();
                // ddl_Daneshkade.Items.Insert(0, "انتخاب کنید");
                ddl_Daneshkade.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Daneshkade.Items[ddl_Daneshkade.Items.Count - 1].Selected = true;

                if (Session["RoleID"].ToString() == "15" || Session["RoleID"].ToString() == "26")
                {
                    ddl_Daneshkade.SelectedValue = "2";
                    ddl_Daneshkade.Enabled = false;
                    DataTable dt = new DataTable();
                    dt = CB.GetAllDepartman(int.Parse(ddl_Daneshkade.SelectedValue));
                    RCB.DataSource = dt;
                    RCB.DataTextField = "namegroup";
                    RCB.DataValueField = "id";
                    RCB.DataBind();
                }
                else if (Session["RoleID"].ToString() == "17" || Session["RoleID"].ToString() == "28")
                {
                    ddl_Daneshkade.SelectedValue = "1";
                    ddl_Daneshkade.Enabled = false;
                    DataTable dt = new DataTable();
                    dt = CB.GetAllDepartman(int.Parse(ddl_Daneshkade.SelectedValue));
                    RCB.DataSource = dt;
                    RCB.DataTextField = "namegroup";
                    RCB.DataValueField = "id";
                    RCB.DataBind();
                }
                else if (Session["RoleID"].ToString() == "16" || Session["RoleID"].ToString() == "27")
                {
                    ddl_Daneshkade.SelectedValue = "3";
                    ddl_Daneshkade.Enabled = false;
                    DataTable dt = new DataTable();
                    dt = CB.GetAllDepartman(int.Parse(ddl_Daneshkade.SelectedValue));
                    RCB.DataSource = dt;
                    RCB.DataTextField = "namegroup";
                    RCB.DataValueField = "id";
                    RCB.DataBind();
                }
                else if (Session["RoleID"].ToString() == "66" || Session["RoleID"].ToString() == "67")
                {
                    ddl_Daneshkade.SelectedValue = "8";
                    ddl_Daneshkade.Enabled = false;
                    DataTable dt = new DataTable();
                    dt = CB.GetAllDepartman(int.Parse(ddl_Daneshkade.SelectedValue));
                    RCB.DataSource = dt;
                    RCB.DataTextField = "namegroup";
                    RCB.DataValueField = "id";
                    RCB.DataBind();
                }
                else
                {
                    ddl_Daneshkade.SelectedValue = "0";
                }
            }
        }

        protected void btn_Show_Click(object sender, EventArgs e)
        {
            getProfessorRequests();
            grd_Show.DataBind();
        }

        protected void ddl_Daneshkade_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = CB.GetAllDepartman(int.Parse(ddl_Daneshkade.SelectedValue));
            RCB.DataSource = dt;
            RCB.DataTextField = "namegroup";
            RCB.DataValueField = "id";
            RCB.DataBind();
        }

        protected void grd_Show_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                grd_Show.ExportSettings.IgnorePaging = true;
                string[] index = e.CommandArgument.ToString().Split(new char[] { ',' });
                lbl_CodeOstad.Text = index[0].ToString();
                lbl_mobile.Text = index[5].ToString();
                lbl_CodeMeli.Text= index[6].ToString();
                confirmMessage.Text = "آیا از تایید استاد و انتقال به سیدا اطمینان دارید";
                string script = "function f(){radopen(null, 'RadWindow1'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "customConfirmOpener", script, true);
            }
            if (e.CommandName == "Select2")
            {
                Session["Page"] = 5;
                grd_Show.ExportSettings.IgnorePaging = true;
                string[] index = e.CommandArgument.ToString().Split(new char[] { ',' });
                lbl_CodeOstad.Text = index[0].ToString();
                lbl_CodeMeli.Text = index[6].ToString();
                Response.Redirect("ShowDetailInfo.aspx?ID=" + int.Parse(index[0].ToString()));
            }

        }

        protected void rbConfirm_OK1_Click(object sender, EventArgs e)
        {
            DataTable dt = FRB.InsertToFostadSida(int.Parse(lbl_CodeOstad.Text),lbl_CodeMeli.Text.Trim());
            if (dt.Rows.Count == 0)
            {
                rwd.RadAlert("کد ملی تکراری می باشد", 0, 100, "پیام", "");
            }
            else
            {
                rwd.RadAlert("مشخصات استاد در سیدا ثبت گردید", 0, 100, "پیام", "");
                //CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToString("HH:mm"), 13, 73, "تایید نهایی دانشکده" + lbl_CodeOstad.Text);
                int Status = 4;
                FRB.UpdateInfoPeople(int.Parse(lbl_CodeOstad.Text), Status);
                setLog(DTO.eventEnum.تایید_دانشکده, int.Parse(lbl_CodeOstad.Text), "");
                DataTable dt1 = new DataTable();
                dt1 = ECB.GetAppIDMessage(0, 13, 1, 4);
                
                string smsStatusText; bool sentSMS;
                lbl_Resault.Text = CB.sendSMS(lbl_mobile.Text, string.Format("{0}\r\n{1}\r\n{2} {3} {4} {5}\r\n {6} \r\n{7} \r\n{8}",
                    dt1.Rows[0]["Text"].ToString(), " شما می توانید با استفاده از ", "نام کاربری: ", dt.Rows[0]["code_Ostad"].ToString(), "رمز عبور: ", CommonBusiness.DecryptPass(dt.Rows[0]["password_ost"].ToString()), " وارد سامانه خدمات الکترونیکی به آدرس ", "service.iauec.ac.ir ", " شوید"), out sentSMS, out smsStatusText);

                getProfessorRequests();
                grd_Show.DataBind();
            }
        }

        protected void grd_Show_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            switch (e.DetailTableView.Name)
            {
                case "info":
                    {
                        string ID = dataItem.GetDataKeyValue("ID").ToString();
                        e.DetailTableView.DataSource = FRB.DepartmanProf(int.Parse(ID));
                        break;
                    }
            }
        }


        private void getProfessorRequests()
        {
            grd_Show.Visible = false;
            if (RCB.CheckedItems.Count > 0)
            {
                if (Session["Field"] != "")
                {
                    Session["Field"] = "";
                }
                if (Session["Resault"] != "")
                {
                    Session["Resault"] = "";
                }
                lbl_Final.Text = string.Empty;
                string Resault = "";
                DataTable dt = new DataTable();
                if (ddl_Daneshkade.SelectedValue == "0")
                {
                    rwd.RadAlert("لطفا دانشکده را انتخاب کنید", 0, 100, "پیام", "");
                }

                for (int i = 0; i <= RCB.CheckedItems.Count; i++)
                {
                    //dt.Rows.Add(int.Parse(RCB.CheckedItems[i].Value));
                    if (i != RCB.CheckedItems.Count)
                    {
                        Resault += RCB.CheckedItems[i].Value + " " + ",";
                    }
                    else
                        Resault += ")";

                }
                Session["Resault"] = Resault;
                lbl_Final.Text = Resault.Replace(",)", "");
                Session["Field"] = lbl_Final.Text;
                dt = FRB.GetDepartmanProf(lbl_Final.Text);
                if (dt.Rows.Count == 0)
                {
                    rwd.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                }
                else
                {
                    grd_Show.Visible = true;
                    grd_Show.DataSource = dt;
                    PersiaFiltering();

                }
            }

        }

        protected void grd_Show_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Column is GridExpandColumn)
                {
                    (e.Column as GridExpandColumn).ButtonType = GridExpandColumnType.ImageButton;
                    (e.Column as GridExpandColumn).ExpandImageUrl = "Image/plus.png";
                    (e.Column as GridExpandColumn).CollapseImageUrl = "Image/minus.png";
                }
            }
        }

        protected void RCB_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadComboBox combo = (RadComboBox)sender;
            Session["combo"] = combo.SelectedValue;
            //this.RadGrid1.Rebind();
        }

        protected void RCB_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string selectedValue = e.Context["RCBValue"].ToString();
        }

        protected void RCB_ItemChecked(object sender, RadComboBoxItemEventArgs e)
        {
            Session["Text"] = e.Item;
        }
        protected void PersiaFiltering()
        {
            GridFilterMenu menu = grd_Show.FilterMenu;
            if (menu.Items.Count > 3)
            {
                int im = 0;
                while (im < menu.Items.Count)
                {
                    if (menu.Items[im].Text == "NoFilter" || menu.Items[im].Text == "Contains" ||
                        menu.Items[im].Text == "EqualTo")
                    {
                        im++;
                    }
                    else
                    {
                        menu.Items.RemoveAt(im);
                    }
                }
                foreach (RadMenuItem item in menu.Items)
                {
                    //change the text for the "StartsWith" menu item  
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

        protected void grd_Show_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            getProfessorRequests();


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
            //grd_Show.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), "ExcelML");
            grd_Show.ExportSettings.IgnorePaging = false;
            grd_Show.ExportSettings.ExportOnlyData = true;
            grd_Show.ExportSettings.OpenInNewWindow = true;
            grd_Show.ExportSettings.UseItemStyles = true;
            grd_Show.MasterTableView.ExportToExcel();
        }


        private void setLog(DTO.eventEnum eventType, int OstadCode, string Description)
        {
            int userId;//کاربری که لاگین کرده
            //eventType//کد کاری که انجام شده. 
            int appId;//کد قسمتی از برنامه که الان توش هستیم. یعنی کارگزینی  -  13
            string description;//توضیحات اختیاری
            int modifyId;//کد درخواست ویرایش شده. ویرایش اطلاعات فردی،اطلاعات تماس و ...
            userId = int.Parse(Session[sessionNames.userID_Karbar].ToString());
            appId = 13;
            modifyId = OstadCode;
            description = Description;
            CB.InsertIntoUserLog(userId, DateTime.Now.ToString("HH:mm"), appId, (int)eventType, description, modifyId);
        }
    }
}
