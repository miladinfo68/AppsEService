using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Exam;
using System.Data;
using Telerik.Web.UI;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.University.Exam.CMS.TestForms
{
    public partial class InsertExamPlaceClasses : System.Web.UI.Page
    {
        ExamBusiness Ebusiness = new ExamBusiness();
        CommonBusiness cmnb = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCityList();
            }
        }

        protected void BindCityList()
        {
            //var cityName = Ebusiness.GetAllExamPlaceAddress().AsEnumerable()
            //    .Where(w => w.Field<bool>("IsActive"))
            //    .Select(s => new { Name = s.Field<string>("Name_City"), Id = s.Field<decimal>("ID") });
            //ddl_City.DataSource = cityName;
            //ddl_City.DataTextField = "Name";
            //ddl_City.DataValueField = "Id";
            //ddl_City.DataBind();

            //var cityName = examBusiness.GetAllExamPlaceAddress().AsEnumerable()
            //    .Where(w => w.Field<bool>("IsActive"))
            //    .Select(s => new { Name = s.Field<string>("Name_City"), Id = s.Field<decimal>("ID") });

            var cityName = Ebusiness.Get_ExamNameCity(Convert.ToInt32(Session[sessionNames.userID_Karbar]));
            ddl_City.DataSource = cityName;
            ddl_City.DataTextField = "Name_City";
            ddl_City.DataValueField = "ID";
            ddl_City.DataBind();
            ddl_City.Items.Insert(0, new ListItem { Text = "انتخاب نمایید", Value = "-1" });
            
            if (cityName.Rows.Count == 1)
            {
                ddl_City.SelectedIndex = 1;
                ddl_City.Enabled = false;
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            if (ddl_City.SelectedValue.ToString() != "" && txt_ClassName.Text != "" && txt_Start.Text != "" && txt_End.Text != "" && ddl_City.SelectedItem.Value != "-1")
            {
                if (!Ebusiness.CheckExamPlaceOverlap(int.Parse(txt_Start.Text), int.Parse(txt_End.Text), ddl_City.SelectedItem.Text))
                {
                    Ebusiness.InsertExamPlaceClass(txt_ClassName.Text, int.Parse(txt_Start.Text), int.Parse(txt_End.Text), ddl_City.SelectedItem.Text, int.Parse(ddl_City.SelectedItem.Value));
                    cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 43, "ثبت محل امتحانات");
                    txt_ClassName.Text = "";
                    txt_End.Text = "";
                    txt_Start.Text = "";
                    DataTable dt = new DataTable();
                    dt = Ebusiness.GetAllExamPlceClassesForExaminer(int.Parse(Session[sessionNames.userID_Karbar].ToString()));//.GetAllExamPlceClasses();
                    grd_ExamPlace.DataSource = dt;
                    grd_ExamPlace.DataBind();
                    rwm.RadAlert("با موفقیت ثبت شد", null, 100, "پیام", "");
                }
                else
                    rwm.RadAlert("بازه انتخابی تداخل دارد", null, 100, "خطا", "");
            }
            else
            {
                rwm.RadAlert("تمام فیلدها تکمیل گردد و شهر انتخاب شود", null, 100, "خطا", "");
            }
        }

        protected void grd_ExamPlace_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = Ebusiness.GetAllExamPlceClassesForExaminer(int.Parse(Session[sessionNames.userID_Karbar].ToString()));//.GetAllExamPlceClasses();
            grd_ExamPlace.DataSource = dt;
        }

        protected void btn_Edit_Click(object sender, EventArgs e)
        {

        }

        protected void grd_ExamPlace_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Edits")
            {
                RadWindowManager windowManager = new RadWindowManager();
                RadWindow widnow1 = new RadWindow();
                widnow1.NavigateUrl = "../CMS/TestForms/EditExamPlaceClass.aspx?params=" + e.CommandArgument.ToString();
                widnow1.ID = "RadWindow1";
                windowManager.Width = System.Web.UI.WebControls.Unit.Pixel(800);
                windowManager.Height = System.Web.UI.WebControls.Unit.Pixel(400);
                widnow1.VisibleOnPageLoad = true;
                windowManager.Windows.Add(widnow1);
                ContentPlaceHolder mpContentPlaceHolder;
                mpContentPlaceHolder =
                 (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                mpContentPlaceHolder.Controls.Add(widnow1);
            }
        }

        protected void grd_ExamPlace_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                DataTable dt = new DataTable();
                Button btn_Edit = e.Item.FindControl("btn_Edit") as Button;
                dt = Ebusiness.GetAllExamPlceClassesForExaminer(int.Parse(Session[sessionNames.userID_Karbar].ToString()));//.GetAllExamPlceClasses();
                btn_Edit.CommandArgument = dt.Rows[e.Item.ItemIndex]["ExamPlaceID"].ToString();
                btn_Edit.CommandName = "Edits";

            }
        }
        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "Rebind")
            {

                DataTable dt = new DataTable();
                dt = Ebusiness.GetAllExamPlceClassesForExaminer(int.Parse(Session[sessionNames.userID_Karbar].ToString()));
                grd_ExamPlace.DataSource = dt;
                grd_ExamPlace.DataBind();
            }
        }
    }
}