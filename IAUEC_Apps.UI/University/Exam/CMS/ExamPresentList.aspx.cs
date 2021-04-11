using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Exam;
using System.Data;
using Microsoft.Reporting.WebForms;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report;
using IAUEC_Apps.Business.Common;
using Stimulsoft.Report.Export;
using IAUEC_Apps.Business.university.Education;


namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class ExamPresentList : System.Web.UI.Page
    {
        /// <summary>
        ///ایجاد نموده ایم ExamBusiness یک شئ از کلاس
        /// </summary>
        ExamBusiness ExamBusiness = new ExamBusiness();
        CommonBusiness CB = new CommonBusiness();
        
        EducationReportBusiness ebusiness = new EducationReportBusiness();
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
                ddl_Term.Items.Insert(0, new ListItem("انتخاب کنید", "0"));

                DataTable dtExamPlace = new DataTable();
                dtExamPlace = ExamBusiness.Get_ExamNameCity(int.Parse(Session[sessionNames.userID_Karbar].ToString()));
                ddl_ExamPlace.DataSource = dtExamPlace;
                ddl_ExamPlace.DataTextField = "Name_City";
                ddl_ExamPlace.DataValueField = "ID";
                ddl_ExamPlace.DataBind();
                ddl_ExamPlace.Items.Insert(0, new ListItem("انتخاب کنید", "0"));
                //ddl_ExamPlace.Items.Add(new ListItem("انتخاب کنید", "0"));
                //ddl_ExamPlace.Items[ddl_ExamPlace.Items.Count - 1].Selected = true;

                DataTable dtField = new DataTable();
                dtField = ExamBusiness.Get_Exam_Fresh();
                ddl_Field.DataSource = dtField;
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataBind();
                ddl_Field.Items.Insert(0, new ListItem("انتخاب کنید", "0"));
                //ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
                //ddl_Field.Items[ddl_Field.Items.Count - 1].Selected = true;

                DataTable dtdanesh = new DataTable();
                dtdanesh = ExamBusiness.GetAllDaneshkade();
                ddl_Danesh.DataSource = dtdanesh;
                ddl_Danesh.DataTextField = "namedanesh";
                ddl_Danesh.DataValueField = "id";
                ddl_Danesh.DataBind();
                ddl_Danesh.Items.Add(new ListItem("همه", "0"));
                ddl_Danesh.Items[ddl_Danesh.Items.Count - 1].Selected = true;

                DataTable dtClass = new DataTable();
                dtClass = ExamBusiness.Get_Exam_Did_Interm(ddl_Term.SelectedValue.ToString());
                ddl_Class.DataSource = dtClass;
                ddl_Class.DataTextField = "did";
                ddl_Class.DataValueField = "did";
                ddl_Class.DataBind();
                ddl_Class.Items.Insert(0, new ListItem("انتخاب کنید", "0"));
                //ddl_Class.Items.Add(new ListItem("انتخاب کنید", "0"));
                //ddl_Class.Items[ddl_Class.Items.Count - 1].Selected = true;

                DataTable dtProf = new DataTable();
                dtProf = ExamBusiness.Get_Exam_Ostad(ddl_Term.SelectedValue.ToString());
                ddl_Professor.DataSource = dtProf;
                ddl_Professor.DataTextField = "OstadName";
                ddl_Professor.DataValueField = "idostad";
                ddl_Professor.DataBind();
                ddl_Professor.Items.Insert(0, new ListItem("انتخاب کنید", "0"));
                //ddl_Professor.Items.Add(new ListItem("انتخاب کنید", "0"));
                //ddl_Professor.Items[ddl_Professor.Items.Count - 1].Selected = true;

                Bind_ddlDateOfExam(ddl_Term.SelectedValue.ToString());
                Bind_ddlSaatExam(ddl_Term.SelectedValue.ToString());


                //ddl_ExamTime.Items.Add(new ListItem("انتخاب کنید", "0"));
                //ddl_ExamTime.Items[ddl_ExamTime.Items.Count - 1].Selected = true;
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

            }


        }

        /// <summary>
        /// با فشردن این کلید صورتجلسه امتحان نمایش داده می شود
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ButtonExamPresent_Click(object sender, EventArgs e)
        {
            if (ddl_ExamPlace.SelectedIndex > 0 && ddl_Term.SelectedIndex > 0)//&& ddl_ExamDate.SelectedIndex > 0 && ddl_ExamTime.SelectedIndex > 0)
            {
                try
                {
                    if (!Page.IsValid) return;
                    string did = "0";
                    if (ddl_Class.SelectedIndex.ToString() != "0")
                        did = ddl_Class.SelectedValue;


                    string Prof = "0";
                    if (ddl_Professor.SelectedIndex.ToString() != "0")
                        Prof = ddl_Professor.SelectedValue.ToString();

                    int field = 0;
                    if (ddl_Field.SelectedIndex.ToString() != "0")
                        field = int.Parse(ddl_Field.SelectedValue.ToString());

                    int ExamPlace = int.Parse(ddl_ExamPlace.SelectedValue);

                    string Examdate = "0";
                    if (ddl_ExamDate.SelectedIndex > 0)
                        Examdate = ddl_ExamDate.SelectedValue;

                    string ExamTime = "0";
                    if (ddl_ExamTime.SelectedIndex > 0)
                        ExamTime = ddl_ExamTime.SelectedValue;


                    string selectedTerm = "0";
                    if (ddl_Term.SelectedIndex > 0)
                        selectedTerm = ddl_Term.SelectedValue;

                    this.StiWebViewer1.ResetReport();

                    DataTable dt2 = new DataTable();
                    dt2 = ExamBusiness.Get_ExamReportsByParams(Examdate, ExamTime, ExamPlace, Prof, did, field, int.Parse(ddl_Danesh.SelectedValue.ToString()),selectedTerm);

                    if (dt2.Rows.Count > 0)
                    {
                        StiWebViewer1.Visible = true;
                        StiReport rpt = new StiReport();
                        rpt.Load(Server.MapPath("../Reports/ExamPresentList1.mrt"));
                        rpt.Dictionary.Databases.Clear();
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Supplementary", CB.ReportConnection.ToString()));
                        rpt.Compile();
                        rpt.CompiledReport.DataSources["[Exam].[SP_ExamReportsByParams]"].Parameters["@did"].ParameterValue = did;
                        rpt.CompiledReport.DataSources["[Exam].[SP_ExamReportsByParams]"].Parameters["@saatexam"].ParameterValue = ExamTime;
                        rpt.CompiledReport.DataSources["[Exam].[SP_ExamReportsByParams]"].Parameters["@dateexam"].ParameterValue = Examdate;
                        rpt.CompiledReport.DataSources["[Exam].[SP_ExamReportsByParams]"].Parameters["@ostadid"].ParameterValue = Prof;
                        rpt.CompiledReport.DataSources["[Exam].[SP_ExamReportsByParams]"].Parameters["@codedars"].ParameterValue = 0;
                        rpt.CompiledReport.DataSources["[Exam].[SP_ExamReportsByParams]"].Parameters["@idresh"].ParameterValue = field;
                        rpt.CompiledReport.DataSources["[Exam].[SP_ExamReportsByParams]"].Parameters["@iddanesh"].ParameterValue = int.Parse(ddl_Danesh.SelectedValue.ToString());
                        rpt.CompiledReport.DataSources["[Exam].[SP_ExamReportsByParams]"].Parameters["@examPlaceId"].ParameterValue = int.Parse(ddl_ExamPlace.SelectedValue);
                        //rpt.CompiledReport.DataSources["[Exam].[SP_ExamReportsByParams]"].Parameters["@namecity"].ParameterValue = ddl_ExamPlace.SelectedItem.Text;
                        rpt.CompiledReport.DataSources["[Exam].[SP_ExamReportsByParams]"].Parameters["@tterm"].ParameterValue = selectedTerm;
                        rpt.RegData(dt2);
                        rpt.ReportCacheMode = StiReportCacheMode.On;
                        StiWebViewer1.Report = rpt;
                        StiWebViewer1.Visible = true;
                        CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 54, "مشاهده صورت جلسه امتحان");
                    }
                    else
                    {
                        StiWebViewer1.Visible = false;
                        rwmValidations.RadAlert("رکوردی موجود نیست", null, 100, "پیام", "");
                    }
                }
                catch (Exception x)
                {
                    throw x;
                }
            }
            else
            {
                rwmValidations.RadAlert("کلیه گزینه ها انتخاب گردد", null, 100, "پیام", "");
            }
        }

        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtClass = new DataTable();
            dtClass = ExamBusiness.Get_Exam_Did_Interm(ddl_Term.SelectedValue.ToString());
            ddl_Class.DataSource = dtClass;
            ddl_Class.DataTextField = "did";
            ddl_Class.DataValueField = "did";
            ddl_Class.DataBind();
            ddl_Class.Items.Insert(0, new ListItem("انتخاب کنید"));
            //ddl_Class.Items.Add(new ListItem("انتخاب کنید", "0"));
            //ddl_Class.Items[ddl_Class.Items.Count - 1].Selected = true;

            DataTable dtProf = new DataTable();
            dtProf = ExamBusiness.Get_Exam_Ostad(ddl_Term.SelectedValue.ToString());
            ddl_Professor.DataSource = dtProf;
            ddl_Professor.DataTextField = "OstadName";
            ddl_Professor.DataValueField = "idostad";
            ddl_Professor.DataBind();
            ddl_Professor.Items.Insert(0, new ListItem("انتخاب کنید"));
            //ddl_Professor.Items.Add(new ListItem("انتخاب کنید", "0"));
            //ddl_Professor.Items[ddl_Professor.Items.Count - 1].Selected = true;

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