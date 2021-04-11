using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.GraduateAffair;
using IAUEC_Apps.DTO.University.Education;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Telerik.Web.UI;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Education;

namespace IAUEC_Apps.UI.University.Education.CMS
{
    public partial class SearchOfStudent : System.Web.UI.Page
    {
        public static RegisterOfStudentDTO RSD = new RegisterOfStudentDTO();
        UniversityBusiness UB = new UniversityBusiness();
        GraduateReportBusiness GraduateBusiness = new GraduateReportBusiness();
        DataTable dt = new DataTable();
        EducationReportBusiness ERB = new EducationReportBusiness();
        CommonBusiness cb = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dtField = cb.SelectAllField();
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dtField;
                ddl_Field.DataBind();
                ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Field.Items[ddl_Field.Items.Count - 1].Selected = true;

                ddl_Degree.Items.Add(new ListItem("کاردانی", "2"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی", "1"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ناپیوسته", "3"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ارشد پیوسته", "4"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ارشد ناپیوسته", "5"));
                ddl_Degree.Items.Add(new ListItem("دکتری تخصصی", "7"));
                ddl_Degree.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Degree.Items[ddl_Degree.Items.Count - 1].Selected = true;

                ddl_StatusStu.Items.Add(new ListItem("نامعلوم", "0"));
                ddl_StatusStu.Items.Add(new ListItem("عادی", "1"));
                ddl_StatusStu.Items.Add(new ListItem("مهمان از", "2"));
                ddl_StatusStu.Items.Add(new ListItem("انصراف تغییر رشته", "3"));
                ddl_StatusStu.Items.Add(new ListItem("انتقال از", "4"));
                ddl_StatusStu.Items.Add(new ListItem("انصراف با اطلاع", "5"));
                ddl_StatusStu.Items.Add(new ListItem("انصراف ماده 51", "6"));
                ddl_StatusStu.Items.Add(new ListItem("فارغ التحصیل", "7"));
                ddl_StatusStu.Items.Add(new ListItem("اخراج آموزشی", "8"));
                ddl_StatusStu.Items.Add(new ListItem("اخراج انضباطی", "9"));
                ddl_StatusStu.Items.Add(new ListItem("اخراج از واحد های تهران", "10"));
                ddl_StatusStu.Items.Add(new ListItem("محروم", "11"));
                ddl_StatusStu.Items.Add(new ListItem("فوت", "12"));
                ddl_StatusStu.Items.Add(new ListItem("شهید", "13"));
                ddl_StatusStu.Items.Add(new ListItem("مهمان سازمان", "14"));
                ddl_StatusStu.Items.Add(new ListItem("عدم مراجعه", "15"));
                ddl_StatusStu.Items.Add(new ListItem("اخراج از کل واحدها", "16"));
                ddl_StatusStu.Items.Add(new ListItem("تسویه حساب مدرک معادل", "17"));
                ddl_StatusStu.Items.Add(new ListItem("انتخاب کنید", "18"));
                ddl_StatusStu.Items[ddl_StatusStu.Items.Count - 1].Selected = true;

                if ((!string.IsNullOrWhiteSpace(Request.QueryString["Degree"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["Field"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["Education"].ToString())) || (!string.IsNullOrWhiteSpace(Request.QueryString["Sex"].ToString()) || (!string.IsNullOrWhiteSpace(Request.QueryString["salvorud"].ToString()))))
                {
                    lbl_Degree.Text = Request.QueryString["Degree"].ToString();
                    lbl_Education.Text = Request.QueryString["Education"].ToString();
                    lbl_Field.Text = Request.QueryString["Field"].ToString();
                    lbl_Sex.Text = Request.QueryString["Sex"].ToString();
                    lbl_Salvorud.Text = Request.QueryString["salvorud"].ToString();
                }
            }
        }

        protected void grd_Student_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {

                Session["stcode"] = e.CommandArgument;
                ClientScript.RegisterStartupScript(Page.GetType(), "Select", "CloseAndRebind();", true);
            }
        }

        protected void grd_Student_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = UB.GetTop50Student();
            grd_Student.DataSource = dt;
        }

        protected void btn_stCode_Click(object sender, EventArgs e)
        {
            if (txt_Family.Text == null || txt_Family.Text == string.Empty)
            {
                txt_Family.Text = "0";
            }
            if (txt_IdMeli.Text == null || txt_IdMeli.Text == string.Empty)
            {
                txt_IdMeli.Text = "0";
            }
            if (txt_NameEp.Text == null || txt_NameEp.Text == string.Empty)
            {
                txt_NameEp.Text = "0";
            }
            if (txt_StCode.Text == null || txt_StCode.Text == string.Empty)
            {
                txt_StCode.Text = "0";
            }
            DataTable dtResault = ERB.GetInfoAllStudent(txt_StCode.Text, txt_Family.Text, txt_NameEp.Text, txt_IdMeli.Text, int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_StatusStu.SelectedValue),int.Parse(ddl_Field.SelectedValue));
            if (dtResault.Rows.Count > 0)
            {
                grd_Student.DataSource = dtResault;
                grd_Student.DataBind();

            }
            else
            {
                rwd.RadAlert("رکوردی با مشخصات وارد شده یافت نشد", 0, 100, "پیام", "");
            }
            if (txt_Family.Text == "0")
            {
                txt_Family.Text = string.Empty;
            }
            if (txt_IdMeli.Text == "0")
            {
                txt_IdMeli.Text = string.Empty;
            }
            if (txt_NameEp.Text == "0")
            {
                txt_NameEp.Text = string.Empty;
            }
            if (txt_StCode.Text == "0")
            {
                txt_StCode.Text = string.Empty;
            }
        }

        protected void ddl_Field_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Field = ddl_Field.SelectedValue;
        }

        protected void ddl_StatusStu_SelectedIndexChanged(object sender, EventArgs e)
        {
           string StatusStu = ddl_StatusStu.SelectedValue;
        }

        protected void ddl_Degree_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Degree = ddl_Degree.SelectedValue;
        }
    }
}