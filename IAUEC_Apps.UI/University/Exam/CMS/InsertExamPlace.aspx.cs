using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Exam;
using Telerik.Web.UI;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class InsertExamPlace : System.Web.UI.Page
    {
        ExamBusiness eBusiness = new ExamBusiness();
        CommonBusiness cmnb = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            if (txt_Address.Text != "" && txt_City.Text != "")
            {
                eBusiness.InsertExamPlaceAddress(txt_City.Text, txt_Address.Text);
                DataTable dt = new DataTable();
                dt = eBusiness.GetAllExamPlaceAddress();
                grd_ExamPlace.DataSource = dt;
                grd_ExamPlace.DataBind();
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 41, "ثبت شهر امتحانات");
                txt_Address.Text = "";
                txt_City.Text = "";
                rwm.RadAlert("با موفقیت ثبت گردید", null, 100, "پیام", "");
            }
            else
            {
                rwm.RadAlert("نام شهر و آدرس باید وارد گردد", null, 100, "خطا", "");
            }
        }

        protected void grd_ExamPlace_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = eBusiness.GetAllExamPlaceAddress();
            grd_ExamPlace.DataSource = dt;
        }

        protected void grd_ExamPlace_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Edits")
            {
                RadWindowManager windowManager = new RadWindowManager();
                RadWindow widnow1 = new RadWindow();
                widnow1.NavigateUrl = "../CMS/EditExamPlace.aspx?params=" + e.CommandArgument.ToString();
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
                dt = eBusiness.GetAllExamPlaceAddress();
                btn_Edit.CommandArgument = dt.Rows[e.Item.ItemIndex]["ID"].ToString();
                btn_Edit.CommandName = "Edits";

            }
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "Rebind")
            {
                DataTable dt = new DataTable();
                dt = eBusiness.GetAllExamPlaceAddress();
                grd_ExamPlace.DataSource = dt;
                grd_ExamPlace.DataBind();
            }
        }
    }
}