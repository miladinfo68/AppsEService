using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Exam;
using System.Data;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Education;
namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class ChairExamPresentList : System.Web.UI.Page
    {
        ExamBusiness ExamBusiness = new ExamBusiness();       
        CommonBusiness cmnb = new CommonBusiness();
        EducationReportBusiness eduBussiness = new EducationReportBusiness();
     
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

                DataTable dtExamPlace = new DataTable();
                //dtExamPlace = ExamBusiness.Get_ExamNameCity(int.Parse(Session[sessionNames.userID_Karbar].ToString()));
                //ddl_ExamPlace2.DataSource = dtExamPlace;
                //ddl_ExamPlace2.DataTextField = "Name_City";
                //ddl_ExamPlace2.DataValueField = "Name_City";
                //ddl_ExamPlace2.DataBind();
                //ddl_ExamPlace2.Items.Insert(0, new ListItem("انتخاب کنید", "0"));

                var cityName = ExamBusiness.Get_ExamNameCity(Convert.ToInt32(Session[sessionNames.userID_Karbar]));
                ddl_ExamPlace2.DataSource = cityName;
                ddl_ExamPlace2.DataTextField = "Name_City";
                ddl_ExamPlace2.DataValueField = "ID";
                ddl_ExamPlace2.DataBind();
                ddl_ExamPlace2.Items.Insert(0, new ListItem("انتخاب کنید", "0"));
                if (cityName.Rows.Count == 1)
                {
                    ddl_ExamPlace2.SelectedIndex = 1;
                    ddl_ExamPlace2.Enabled = false;
                }




                DataTable dterm = new DataTable();
                dterm = cmnb.SelectAllTerm();
                ddl_Term.DataTextField = "tterm";
                ddl_Term.DataValueField = "tterm";
                ddl_Term.DataSource = dterm;
                ddl_Term.DataBind();
                ddl_Term.Items.Insert(0, new ListItem("انتخاب کنید", "0"));

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
                    ddl_Danesh.SelectedValue = daneshID.ToString();
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

        /// <summary>
        /// با فشردن این کلید به تفکیک شهر، لیست شماره صندلی تخصیص داده شده نمایش داده می شود
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnChairExamListByPlace_Click(object sender, EventArgs e)
        {
            if (ddl_ExamPlace2.SelectedIndex > 0 && ddl_Term.SelectedIndex>0)//&& ddl_ExamDate.SelectedIndex > 0 && ddl_ExamTime.SelectedIndex > 0)
            {
                this.StiWebViewer1.ResetReport();
                int iddanesh = int.Parse(ddl_Danesh.SelectedValue.ToString());
                var selectedTerm = ddl_Term.SelectedValue.ToString();
                string ExamPlaceId = ddl_ExamPlace2.SelectedValue.ToString();
                string Examdate = ddl_ExamDate.SelectedValue.ToString();
                string ExamTime = ddl_ExamTime.SelectedValue.ToString();
                DataTable dt = new DataTable();
                dt = ExamBusiness.Get_ExamReports(Examdate, ExamTime, ExamPlaceId, iddanesh ,selectedTerm);
                if (dt.Rows.Count > 0)
                {
                    StiWebViewer1.Visible = true;
                    StiReport rpt = new StiReport();
                    rpt.Load(Server.MapPath("../Reports/ChairExamPresentList1.mrt"));
                    rpt.Dictionary.Databases.Clear();
                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Supplementary", cmnb.ReportConnection.ToString()));
                    rpt.Compile();
                    rpt.CompiledReport.DataSources["[Exam].[SP_ExamReports]"].Parameters["@saatexam"].ParameterValue = ddl_ExamTime.SelectedValue.ToString();
                    rpt.CompiledReport.DataSources["[Exam].[SP_ExamReports]"].Parameters["@dateexam"].ParameterValue = ddl_ExamDate.SelectedValue.ToString();
                    rpt.CompiledReport.DataSources["[Exam].[SP_ExamReports]"].Parameters["@examPlaceId"].ParameterValue = ddl_ExamPlace2.SelectedValue.ToString();
                    rpt.CompiledReport.DataSources["[Exam].[SP_ExamReports]"].Parameters["@iddanesh"].ParameterValue = int.Parse(ddl_Danesh.SelectedValue.ToString());
                    rpt.CompiledReport.DataSources["[Exam].[SP_ExamReports]"].Parameters["@term"].ParameterValue = selectedTerm;
                    rpt.RegData(dt);
                    rpt.ReportCacheMode = StiReportCacheMode.On;
                    StiWebViewer1.Report = rpt;
                    StiWebViewer1.Visible = true;
                    cmnb.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 49, "مشاهده گزارش لیست شماره صندلی");
                }
                else
                {
                    StiWebViewer1.Visible = false;
                    rwmValidations.RadAlert("رکوردی موجود نیست", null, 100, "پیام", "");
                }
            }
            else
            {
                rwmValidations.RadAlert("کلیه گزینه ها انتخاب گردد", null, 100, "پیام", "");
            }

        }

        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {

            Bind_ddlDateOfExam(ddl_Term.SelectedValue.ToString());
            Bind_ddlSaatExam(ddl_Term.SelectedValue.ToString(), "");

        }

        protected void ddl_ExamDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_ddlSaatExam("", ddl_ExamDate.SelectedValue.ToString());
        }

        protected void ddl_ExamPlace2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtExamDate = new DataTable();
            Bind_ddlDateOfExam(ddl_Term.SelectedValue.ToString());
            Bind_ddlSaatExam(ddl_Term.SelectedValue.ToString());            
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
                ddl_ExamDate.Items.Insert(0, new ListItem("انتخاب کنید", "0"));
            //}
        }


        void Bind_ddlSaatExam(string term = "", string date = "")
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(term))
                dt = ExamBusiness.Get_Exam_saatexamByTerm(term);
            else if (!string.IsNullOrEmpty(date))
                dt = ExamBusiness.GetSaatExamByDateExam(date); //GetSaatExamByDateExam

            //if (dt != null)
            //{
                ddl_ExamTime.DataSource = dt;
                ddl_ExamTime.DataTextField = "saatexam";
                ddl_ExamTime.DataValueField = "saatexam";
                ddl_ExamTime.DataBind();
                ddl_ExamTime.Items.Insert(0, new ListItem("انتخاب کنید", "0"));
            //}

        }


        //protected void BindCityList()
        //{
        //    var cityName =ExamBusiness.GetAllExamPlaceAddress().AsEnumerable()
        //        .Where(w => w.Field<bool>("IsActive"))
        //        .Select(s => new { Name = s.Field<string>("Name_City"), Id = s.Field<decimal>("ID") });
        //    ddl_shahr.DataSource = cityName;
        //    ddl_shahr.DataTextField = "Name";
        //    ddl_shahr.DataValueField = "Id";
        //    ddl_shahr.DataBind();
        //}


    }
}