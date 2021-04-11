using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Exam;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using Microsoft.Reporting.WebForms;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Education;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class ExamPlaceList : System.Web.UI.Page
    {
        ExamBusiness ExamBusiness = new ExamBusiness();
        
        CommonBusiness CB = new CommonBusiness();
        EducationReportBusiness ebusiness = new EducationReportBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {

            try
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
                    ddl_Term.Items.Insert(0, new ListItem("انتخاب کنید"));

                    Bind_ddlDateOfExam(ddl_Term.SelectedValue.ToString());
                    Bind_ddlSaatExam("", ddl_ExamDate.SelectedValue.ToString());


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
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        /// <summary>
        /// با فشردن این کلید محل آزمون ها نمایش داده می شود
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnExamPlaceList_Click(object sender, EventArgs e)
        {
            this.StiWebViewer1.ResetReport();
            string tterm = ddl_Term.SelectedValue.ToString();
            int iddanesh = int.Parse(ddl_Danesh.SelectedValue.ToString());
            string Examdate = "0";
            string ExamTime = "0";
            int examinerID = int.Parse(Session[sessionNames.userID_Karbar].ToString());

            if (ddl_ExamDate.SelectedIndex > 0)
                Examdate = ddl_ExamDate.SelectedValue;

            if (ddl_ExamTime.SelectedIndex > 0)
                ExamTime = ddl_ExamTime.SelectedValue;

            DataTable dt = new DataTable();
            dt = ExamBusiness.Get_ListClassCityByDate(iddanesh, tterm, Examdate, ExamTime , examinerID);

            if (dt.Rows.Count > 0)
            {
                StiWebViewer1.Visible = true;   
                StiReport rpt = new StiReport();
                rpt.Load(Server.MapPath("../Reports/ExamClassList1.mrt"));
                rpt.Dictionary.Databases.Clear();
                rpt.Dictionary.Databases.Add(new StiSqlDatabase("Supplementary", CB.ReportConnection.ToString()));
                rpt.Compile();
                rpt.CompiledReport.DataSources["[Exam].[Sp_GetListClassCityByDate]"].Parameters["@iddanesh"].ParameterValue = int.Parse(ddl_Danesh.SelectedValue.ToString());
                rpt.CompiledReport.DataSources["[Exam].[Sp_GetListClassCityByDate]"].Parameters["@tterm"].ParameterValue = tterm;
                rpt.CompiledReport.DataSources["[Exam].[Sp_GetListClassCityByDate]"].Parameters["@dateexam"].ParameterValue = Examdate;
                rpt.CompiledReport.DataSources["[Exam].[Sp_GetListClassCityByDate]"].Parameters["@saatexam"].ParameterValue = ExamTime;
                rpt.CompiledReport.DataSources["[Exam].[Sp_GetListClassCityByDate]"].Parameters["@examinerID"].ParameterValue = examinerID;
                rpt.RegData(dt);
                rpt.ReportCacheMode = StiReportCacheMode.On;
                StiWebViewer1.Report = rpt;
                StiWebViewer1.Visible = true;
                CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 52, "مشاهده گزارش لیست محل برگزاری");
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


    }
}