using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Exam;
using Microsoft.Reporting.WebForms;
using System.Data;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Education;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class ExamPlaceListSans : System.Web.UI.Page
    {
        ExamBusiness ExamBusiness = new ExamBusiness();
        CommonBusiness CB = new CommonBusiness();
      
        EducationReportBusiness ebusiness = new EducationReportBusiness();
        /// <summary>
        /// با لود شدن صفحه دراپ دان تاریخ و ساعت امتحان از داده ها پر می شود
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
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
                AccessControl1.MenuId = menuId;
                AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();

                DataTable dterm = new DataTable();
                dterm = CB.SelectAllTerm();
                ddl_Term.DataTextField = "tterm";
                ddl_Term.DataSource = dterm;
                ddl_Term.DataBind();
                BindCityList();//##############################
                ddl_Term.Items.Insert(0, new ListItem("انتخاب کنید..", "0"));

                DataTable dtdanesh = new DataTable();
                dtdanesh = ExamBusiness.GetAllDaneshkade();
                ddl_Danesh.DataSource = dtdanesh;
                ddl_Danesh.DataTextField = "namedanesh";
                ddl_Danesh.DataValueField = "id";
                ddl_Danesh.DataBind();
                ddl_Danesh.Items.Add(new ListItem("همه", "0"));
                ddl_Danesh.Items[ddl_Danesh.Items.Count - 1].Selected = true;
                UserAccessBusiness uacb = new UserAccessBusiness();
                int daneshID = uacb.GetDaneshIDByRoleID(int.Parse(Session["RoleID"].ToString()));
                if (daneshID > 0)
                {

                    if (ddl_Danesh.Items.FindByValue(daneshID.ToString()) != null)
                        ddl_Danesh.Items.FindByValue(daneshID.ToString()).Selected = true;
                    //ddl_Danesh.SelectedValue = daneshID.ToString();
                    ddl_Danesh.Enabled = false;
                }
                else
                {
                    ddl_Danesh.SelectedValue = daneshID.ToString();
                }
                //if (Session["RoleID"].ToString() == "15" || Session["RoleID"].ToString() == "26")
                //{
                //    ddl_Danesh.SelectedValue = "2";
                //    ddl_Danesh.Enabled = false;
                //}
                //else if (Session["RoleID"].ToString() == "17" || Session["RoleID"].ToString() == "28")
                //{
                //    ddl_Danesh.SelectedValue = "1";
                //    ddl_Danesh.Enabled = false;
                //}
                //else if (Session["RoleID"].ToString() == "16" || Session["RoleID"].ToString() == "27")
                //{
                //    ddl_Danesh.SelectedValue = "3";
                //    ddl_Danesh.Enabled = false;
                //}
                //else
                //{
                //    ddl_Danesh.SelectedValue = "0";

                //}
            }


        }





        protected void BindCityList()
        {
            //var cityName = ExamBusiness.GetAllExamPlaceAddress().AsEnumerable()
            //    .Where(w => w.Field<bool>("IsActive"))
            //    .Select(s => new { Name = s.Field<string>("Name_City"), Id = s.Field<decimal>("ID") });
            var cityName = ExamBusiness.Get_ExamNameCity(Convert.ToInt32(Session[sessionNames.userID_Karbar]));
            ddl_shahr.DataSource = cityName;
            ddl_shahr.DataTextField = "Name_City";
            ddl_shahr.DataValueField = "ID";
            ddl_shahr.DataBind();
            if (cityName.Rows.Count == 1)
            {
                ddl_shahr.SelectedIndex = 1;
                ddl_shahr.Enabled = false;
            }
        }










        /// <summary>
        /// لیست محل برگزاری امتحان بر اساس سانس نمایش داده می شود
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnExamPlaceList_Click(object sender, EventArgs e)
        {
            int iddanesh = int.Parse(ddl_Danesh.SelectedValue.ToString());
            this.StiWebViewer1.ResetReport();
            string Examdate = ddl_ExamDate.SelectedValue.ToString();
            string ExamTime = ddl_ExamTime.SelectedValue.ToString();
            var cityID = ddl_shahr.SelectedValue.ToString();
            var selectedTerm = ddl_Term.SelectedValue.ToString();
            DataTable dt = new DataTable();
            dt = ExamBusiness.MahalBargozariSans(Examdate, ExamTime, cityID, iddanesh, selectedTerm);

            if (dt.Rows.Count > 0)
            {
                StiWebViewer1.Visible = true;
                StiReport rpt = new StiReport();
                rpt.Load(Server.MapPath("../Reports/ExamPlaceListmahal22.mrt"));
                rpt.Dictionary.Databases.Clear();
                rpt.Dictionary.Databases.Add(new StiSqlDatabase("Supplementary", CB.ReportConnection.ToString()));
                rpt.Compile();
                rpt.CompiledReport.DataSources["[Exam].[SP_MahalBargozariSans]"].Parameters["@saatexam"].ParameterValue = ExamTime;
                rpt.CompiledReport.DataSources["[Exam].[SP_MahalBargozariSans]"].Parameters["@dateexam"].ParameterValue = Examdate;
                rpt.CompiledReport.DataSources["[Exam].[SP_MahalBargozariSans]"].Parameters["@iddanesh"].ParameterValue = iddanesh;
                rpt.CompiledReport.DataSources["[Exam].[SP_MahalBargozariSans]"].Parameters["@cityID"].ParameterValue = cityID;//"تهران";
                rpt.CompiledReport.DataSources["[Exam].[SP_MahalBargozariSans]"].Parameters["@tterm"].ParameterValue = selectedTerm;
                rpt.RegData(dt);
                rpt.ReportCacheMode = StiReportCacheMode.On;
                StiWebViewer1.Report = rpt;
                StiWebViewer1.Visible = true;
                CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 53, "مشاهده لیست محل برگزاری بر اساس سانس");
            }

            else
            {
                StiWebViewer1.Visible = false;
                rwmValidations.RadAlert("رکوردی موجود نیست", null, 100, "پیام", "");
            }


        }

        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_ddlDateOfExam(ddl_Term.SelectedValue.ToString());
            Bind_ddlSaatExam(ddl_Term.SelectedValue.ToString(), "");
            /*
            DataTable dtExamDate = new DataTable();
            dtExamDate = ExamBusiness.Get_Exam_dateexamByTerm(ddl_Term.SelectedValue.ToString());
            ddl_ExamDate.DataSource = dtExamDate;
            ddl_ExamDate.DataTextField = "dateexam";
            ddl_ExamDate.DataValueField = "dateexam";
            ddl_ExamDate.DataBind();
            ddl_ExamDate.Items.Add(new ListItem("انتخاب کنید", "0"));            


            DataTable dtExamTime = new DataTable();
            dtExamTime = ExamBusiness.Get_Exam_saatexamByTerm(ddl_Term.SelectedValue.ToString());
            ddl_ExamTime.DataSource = dtExamTime;
            ddl_ExamTime.DataTextField = "saatexam";
            ddl_ExamTime.DataValueField = "saatexam";
            ddl_ExamTime.DataBind();
            ddl_ExamTime.Items.Add(new ListItem("انتخاب کنید", "0"));
            */
        }

        protected void ddl_ExamDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_ddlSaatExam("", ddl_ExamDate.SelectedValue.ToString());
        }

        void Bind_ddlDateOfExam(string term = "")
        {
            //if (!string.IsNullOrEmpty(term))
            //{
                DataTable dtExamDate = new DataTable();
                dtExamDate = ExamBusiness.Get_Exam_dateexamByTerm(term);
                ddl_ExamDate.DataSource = dtExamDate;
                ddl_ExamDate.DataTextField = "dateexam";
                ddl_ExamDate.DataValueField = "dateexam";
                ddl_ExamDate.DataBind();
                ddl_ExamDate.Items.Insert(0, new ListItem("انتخاب کنید" ,"0"));
                //ddl_ExamDate.Items.Add(new ListItem("انتخاب کنید","0" ));
            //}
        }


        void Bind_ddlSaatExam(string term = "", string date = "")
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(term))
                dt = ExamBusiness.Get_Exam_saatexamByTerm(term);
            else if (!string.IsNullOrEmpty(date))
                dt = ExamBusiness.GetSaatExamByDateExam(date); //GetSaatExamByDateExam

            if (dt != null)
            {
                ddl_ExamTime.DataSource = dt;
                ddl_ExamTime.DataTextField = "saatexam";
                ddl_ExamTime.DataValueField = "saatexam";
                ddl_ExamTime.DataBind();
                //ddl_ExamTime.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_ExamTime.Items.Insert(0, new ListItem("انتخاب کنید", "0"));
            }
        }

    }
}