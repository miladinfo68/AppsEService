using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Exam;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System.Data;
using IAUEC_Apps.Business.university.Education;

namespace IAUEC_Apps.UI.University.Exam.CMS
{
    public partial class ExamListByPlace : System.Web.UI.Page
    {
        ExamBusiness ExamBusiness = new ExamBusiness();
        
        CommonBusiness CB = new CommonBusiness();
        EducationReportBusiness EBUSINESS = new EducationReportBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
                    Session[sessionNames.menuID] = menuId;
                }
                AccessControl1.MenuId = Session[sessionNames.menuID].ToString();
                AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
                DataTable dterm = new DataTable();
                dterm = CB.SelectAllTerm();
                ddl_Term.DataTextField = "tterm";
                ddl_Term.DataSource = dterm;
                ddl_Term.DataBind();
                ddl_Term.Items.Insert(0, new ListItem("انتخاب کنید", "0"));
                DataTable dtExamPlace = new DataTable();
                dtExamPlace = ExamBusiness.Get_ExamNameCity(int.Parse(Session[sessionNames.userID_Karbar].ToString()));

                ddl_ExamPlace2.DataSource = dtExamPlace;
                ddl_ExamPlace2.DataTextField = "Name_City";
                ddl_ExamPlace2.DataValueField = "Name_City";
                ddl_ExamPlace2.DataBind();
                ddl_ExamPlace2.Items.Insert(0, new ListItem("انتخاب کنید", "0"));
                //ddl_ExamPlace2.Items.Add(new ListItem("انتخاب کنید", "0"));
                //ddl_ExamPlace2.Items[ddl_ExamPlace2.Items.Count - 1].Selected = true;

                DataTable dtdanesh = new DataTable();
                dtdanesh = ExamBusiness.GetAllDaneshkade();
                ddl_Danesh.DataSource = dtdanesh;
                ddl_Danesh.DataTextField = "namedanesh";
                ddl_Danesh.DataValueField = "id";
                ddl_Danesh.DataBind();
                ddl_Danesh.Items.Add(new ListItem("همه", "0"));
                ddl_Danesh.Items[ddl_Danesh.Items.Count - 1].Selected = true;


                Bind_ddlDateOfExam(ddl_Term.SelectedValue.ToString());
                //DataTable dtExamDate = new DataTable();
                //dtExamDate = ExamBusiness.Get_Exam_dateexamByTerm(ddl_Term.SelectedValue.ToString());
                //ddl_ExamDate.DataSource = dtExamDate;
                //ddl_ExamDate.DataTextField = "dateexam";
                //ddl_ExamDate.DataValueField = "dateexam";
                //ddl_ExamDate.DataBind();
                //ddl_ExamDate.Items.Add(new ListItem("انتخاب کنید", "0"));
                //ddl_ExamDate.Items[ddl_ExamDate.Items.Count - 1].Selected = true;





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

        protected void btn_ExamListByPlace_Click(object sender, EventArgs e)
        {
            string s = "";
            s = "1";
            try
            {
                s = "2";
                if (ddl_ExamPlace2.SelectedIndex > 0 && ddl_Term.SelectedIndex > 0) //!= "0" || ddl_ExamDate.SelectedValue.ToString() != "0")
                {
                    s = "3";
                    string ExamPlace = ddl_ExamPlace2.SelectedValue.ToString();//ddl_ExamPlace2 Drp shahrha
                    DataTable dt3 = new DataTable();
                    s = "4";
                    s = "5";
                    var selectedTerm = ddl_Term.SelectedValue.ToString();
                    dt3 = ExamBusiness.Get_ExamReportsByPlace(ExamPlace, int.Parse(ddl_Danesh.SelectedValue.ToString()), ddl_ExamDate.SelectedValue.ToString(), selectedTerm);
                    s = "6";
                    if (dt3.Rows.Count > 0)
                    {
                        this.StiWebViewer2.ResetReport();
                        s = "7";
                        StiWebViewer2.Visible = true;
                        StiReport rpt = new StiReport();
                        s = "8";
                        rpt.Load(Server.MapPath("../Reports/ExamPresentListByPlace1.mrt"));
                        rpt.Dictionary.Databases.Clear();
                        s = "9";
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Supplementary", CB.ReportConnection.ToString()));
                        s = "10";
                        rpt.Compile();
                        s = "11";
                        rpt.CompiledReport.DataSources["[Exam].[SP_ExamReportsByPlace]"].Parameters["@Name_City"].ParameterValue = ExamPlace;
                        rpt.CompiledReport.DataSources["[Exam].[SP_ExamReportsByPlace]"].Parameters["@iddanesh"].ParameterValue = int.Parse(ddl_Danesh.SelectedValue.ToString());
                        rpt.CompiledReport.DataSources["[Exam].[SP_ExamReportsByPlace]"].Parameters["@dateexam"].ParameterValue = ddl_ExamDate.SelectedValue.ToString();
                        rpt.CompiledReport.DataSources["[Exam].[SP_ExamReportsByPlace]"].Parameters["@term"].ParameterValue = selectedTerm;
                        s = "12";
                        rpt.RegData(dt3);
                        rpt.ReportCacheMode = StiReportCacheMode.On;
                        s = "13";
                        StiWebViewer2.Report = rpt;
                        StiWebViewer2.Visible = true;
                        s = "14";
                        CB.InsertIntoUserLog(int.Parse(Session[sessionNames.userID_Karbar].ToString()), DateTime.Now.ToShortTimeString(), int.Parse(Session[sessionNames.appID_Karbar].ToString()), 51, "مشاهده گزارش لیست دانشجویان بر اساس محل امتحان به تفکیک سانس امتحان");

                    }
                    else
                    {
                        StiWebViewer2.Visible = false;
                        rwmValidations.RadAlert("رکوردی موجود نیست", null, 100, "پیام", "");
                    }
                }
                else
                {

                    rwmValidations.RadAlert("کلیه گزینه های ستاره دار انتخاب گردند", null, 100, "پیام", "");
                }

            }
            catch(Exception x)
            {
                var msg = x.Message;
                Response.Write(s);
            }
        }

        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind_ddlDateOfExam(ddl_Term.SelectedValue.ToString());

            //DataTable dtExamDate = new DataTable();
            //dtExamDate = ExamBusiness.Get_Exam_dateexamByTerm(ddl_Term.SelectedValue.ToString());
            //ddl_ExamDate.DataSource = dtExamDate;
            //ddl_ExamDate.DataTextField = "dateexam";
            //ddl_ExamDate.DataValueField = "dateexam";
            //ddl_ExamDate.DataBind();
            //ddl_ExamDate.Items.Add(new ListItem("انتخاب کنید", "0"));

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

    }
}