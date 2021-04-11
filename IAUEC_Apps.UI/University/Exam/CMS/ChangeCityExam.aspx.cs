using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Exam;
using System.Data;
using IAUEC_Apps.Business.Common;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class ChangeCityExam : System.Web.UI.Page
    {
        /// <summary>
        ///ایجاد نموده ایم ExamBusiness یک شئ از کلاس
        /// </summary>
        ExamBusiness ExamBusiness = new ExamBusiness();
        CommonBusiness cmnb = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt = ExamBusiness.Get_St_ExamPlace();
                ddl_City.DataTextField = "Name_City";
                ddl_City.DataValueField = "ID";
                ddl_City.DataSource = dt;
                ddl_City.DataBind();
                ddl_City.Items.Insert(0, new ListItem("انتخاب کنید","0"));
                //ddl_City.Items.Add(new ListItem("انتخاب کنید", "0"));
                //ddl_City.Items[ddl_City.Items.Count - 1].Selected = true;
                ddl_City2.DataTextField = "Name_City";
                ddl_City2.DataValueField = "ID";
                ddl_City2.DataSource = dt;
                ddl_City2.DataBind();
                ddl_City2.Items.Insert(0, new ListItem("همه شهرها", "All"));
                ddl_City2.Items.Insert(0, new ListItem("انتخاب کنید" ,"0"));
                //ddl_City2.Items.Add(new ListItem("انتخاب کنید", "0"));
                //ddl_City2.Items[ddl_City2.Items.Count - 1].Selected = true;
                DataTable dterm = new DataTable();
                dterm = cmnb.SelectAllTerm();
                ddlTerm.DataTextField = "tterm";
                ddlTerm.DataSource = dterm;
                ddlTerm.DataBind();
                ddlTerm.Items.Insert(0, new ListItem("انتخاب کنید","0"));
            }
        }

        /// <summary>
        /// با فشردن کلید اعمال تغییرات و انتخاب شهر جدید، شهر امتحان دانشجو تغییر می یابد
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_City.SelectedIndex > 0)
                {
                    ExamBusiness.Update_Webmelli_Student_ExamPlace(txt_StudentNumber.Text, int.Parse(ddl_City.SelectedValue));
                    cmnb.InsertIntoUserLog(int.Parse(Session["UserId"].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session["AppId"].ToString()), 46, txt_StudentNumber.Text);
                    CleanAndHideUserInfo();
                    rwm.RadAlert("تغییر با موفقیت انجام شد", null, 100, "پیام", "");
                }
                else
                    rwm.RadAlert("شهر محل امتحان را انتخاب کنید.", null, 100, "پیام", "");
            }
            catch
            {
                rwm.RadAlert("خطا در اعمال تغییر", null, 100, "پیام", "");
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// با فشردن کلید نمایش شهر فعلی، شهر فعلی امتحان دانشجو نمایش داده می شود
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtst = new DataTable();
                //dtst = ExamBusiness.Get_Student_CityName(txt_StudentNumber.Text);
                //var stInfo = cmnb.GetStudentInfoByStcode(txt_StudentNumber.Text);
                var stInfo = ExamBusiness.GetStudentInfo(txt_StudentNumber.Text);
                if (stInfo.Rows.Count > 0 && !string.IsNullOrEmpty(stInfo.Rows[0]["Name_City"].ToString()))
                {
                    lbl_City.ForeColor = System.Drawing.Color.Empty;
                    lbl_City.Text = stInfo.Rows[0]["Name_City"].ToString();
                }
                else
                {
                    //CleanAndHideUserInfo();
                    //rwm.RadAlert("محلی انتخاب نشده است", null, 100, "پیام", "");
                    lbl_City.Text = "محلی انتخاب نشده است";
                    lbl_City.ForeColor = System.Drawing.Color.DarkRed;
                }
                lblFullName.Text = stInfo.Rows[0]["name"] + " " + stInfo.Rows[0]["family"].ToString();
                lblStcode.Text = stInfo.Rows[0]["stcode"].ToString();
                lblField.Text = stInfo.Rows[0]["nameresh"].ToString();
                pnlStudentInfo.Visible = true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void CleanAndHideUserInfo()
        {
            lbl_City.Text = string.Empty;
            lblFullName.Text = string.Empty;
            lblStcode.Text = string.Empty;
            lblField.Text = string.Empty;
            ddl_City.SelectedValue = "0";
            pnlStudentInfo.Visible = false;
        }

        /// <summary>
        /// با فشردن کلید نمایش تعداد، تعداد دانشجویان هر شهر نمایش داده می شود
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Button4_Click(object sender, EventArgs e)
        {
            var cityId = ddl_City2.SelectedItem.Value == "All" ? "-1" :
                (ddl_City2.SelectedItem.Value == "0" ? "0" : ddl_City2.SelectedItem.Value);
            if (cityId == "0") return;

            var term = ddlTerm.SelectedValue.ToString();
            int intCityId = int.Parse(cityId);

            try
            {
                DataTable dt = new DataTable();
                dt = ExamBusiness.Get_st_in_Examplace(intCityId, term);
                lbl_Count.Visible = true;
                lbl_Count.Text = dt.Rows[0]["ct"].ToString();
                cmnb.InsertIntoUserLog(int.Parse(Session["UserId"].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session["AppId"].ToString()), 50, "مشاهده تعداد دانشجو در هر شهر");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}