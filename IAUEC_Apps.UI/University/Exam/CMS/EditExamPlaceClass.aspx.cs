using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Exam;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class EditExamPlaceClass : System.Web.UI.Page
    {
        ExamBusiness Ebusiness = new ExamBusiness();
        CommonBusiness cmnb = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int ID = int.Parse(Request.QueryString["params"].ToString());
                DataTable dt = new DataTable();
                dt = Ebusiness.GetExamPlaceClassByID(ID);
                txt_ClassName.Text = dt.Rows[0]["ExamPlace"].ToString();
                txt_StartRange.Text = dt.Rows[0]["StartRange"].ToString();
                txt_EndRange.Text = dt.Rows[0]["EndRange"].ToString();

                BindCityList(dt.Rows[0]["City_Name"].ToString());
            }
        }

        protected void BindCityList(string currentCityName)
        {
            //var cityName = Ebusiness.GetAllExamPlaceAddress().AsEnumerable()
            //    .Where(w => w.Field<bool>("IsActive"))
            //    .Select(s => new { Name = s.Field<string>("Name_City"), Id = s.Field<decimal>("ID") });
            var cityName = Ebusiness.Get_ExamNameCity(Convert.ToInt32(Session[sessionNames.userID_Karbar]));

            ddlCityName.DataSource = cityName;
            ddlCityName.DataTextField = "Name_City";
            ddlCityName.DataValueField = "ID";
            ddlCityName.DataBind();
            ddlCityName.Items.Insert(0, new ListItem { Text = "انتخاب نمایید", Value = "-1" });
            ddlCityName.SelectedValue = cityName.AsEnumerable().Where(w => w.Field<string>("Name_City") == currentCityName).FirstOrDefault().Field<int>("ID").ToString();

            if (cityName.Rows.Count == 1)
            {
                ddlCityName.Enabled = false;
            }
        }

        protected void btn_Edit_Click(object sender, EventArgs e)
        {
            if (txt_ClassName.Text != "" && txt_EndRange.Text != "" && txt_StartRange.Text != "" && ddlCityName.SelectedItem.Value != "-1")
            {
                int examPlaceId = int.Parse(Request.QueryString["params"].ToString());
                if (!Ebusiness.CheckExamPlaceOverlap(int.Parse(txt_StartRange.Text), int.Parse(txt_EndRange.Text), ddlCityName.SelectedItem.Text, examPlaceId))
                {
                    Ebusiness.UpdateExamPlaceClass(txt_ClassName.Text, int.Parse(txt_StartRange.Text), int.Parse(txt_EndRange.Text), examPlaceId, ddlCityName.SelectedItem.Text);
                    cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 44, "ویرایش محل امتحانات");

                    ScriptManager.RegisterStartupScript(this, GetType(), "btn_Edit", "CloseModal();", true);
                }
                else
                    rwm.RadAlert("بازه انتخابی تداخل دارد", null, 100, "خطا", "");
            }
            else
            {
                rwm.RadAlert("همه فیلدها تکمیل گردد", null, 100, "خطا", "");
            }

        }
    }

}