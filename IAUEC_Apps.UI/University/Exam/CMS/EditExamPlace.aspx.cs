using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using DocumentFormat.OpenXml.Wordprocessing;
using IAUEC_Apps.Business.university.Exam;
using IAUEC_Apps.Business.Common;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class EditExamPlace : System.Web.UI.Page
    {
        ExamBusiness ebusiness = new ExamBusiness();
        CommonBusiness cmnb = new CommonBusiness();
        public int ID;
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_Karbar] == null)
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx",false);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[sessionNames.userID_Karbar] == null)
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx",false);
            if (!IsPostBack)
            {
                ID = int.Parse(Request.QueryString["params"].ToString());
                DataTable dt = new DataTable();
                dt = ebusiness.GetExamPlaceAddressByID(ID);
                txt_Address.Text = dt.Rows[0]["Address"].ToString();
                txt_City.Text = dt.Rows[0]["Name_City"].ToString();
                drpIsActive.Items.Add(new ListItem("فعال","True"));
                drpIsActive.Items.Add(new ListItem("غیرفعال","False"));
                var isActive = dt.Rows[0]["IsActive"].ToString();
                drpIsActive.SelectedValue = !string.IsNullOrEmpty(isActive) ? isActive : "False";



            }

        }

        protected void btn_Edit_Click(object sender, EventArgs e)
        {
            if (txt_Address.Text == "" || txt_City.Text == "")
            {
                rwm.RadAlert("نام شهر و آدرس باید وارد گردد", null, 100, "خطا", "");
            }
            else
            {
                ID = int.Parse(Request.QueryString["params"].ToString());
                ebusiness.UpdateExamPlaceAddress(txt_City.Text, txt_Address.Text, ID, Convert.ToBoolean(drpIsActive.SelectedValue));
                cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 42, "ویرایش شهر امتحانات");

                ScriptManager.RegisterStartupScript(this, GetType(), "btn_Edit", "CloseModal();", true);
            }
        }
    }
}