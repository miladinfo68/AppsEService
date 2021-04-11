using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.University.Faculty;
using IAUEC_Apps.Business.university.Education;
using IAUEC_Apps.DTO.University.Education;
using IAUEC_Apps.Business.university.Faculty;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Dictionary;

using Telerik.Web.UI;
using System.IO;

namespace IAUEC_Apps.UI.University.Faculty.CMS
{
    public partial class ListEblaghAsatid : System.Web.UI.Page
    {
        
        public static FacultyDTO FD = new FacultyDTO();
        public static BarnameHaftegiDTO BHD = new BarnameHaftegiDTO();
        EducationReportBusiness ERB = new EducationReportBusiness();
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();
        CommonBusiness CB = new CommonBusiness();
        DataTable dtTerm = new DataTable();
        DataTable dtDaneshkade = new DataTable();
        DataTable dtDepartman = new DataTable();
        DataTable dtCooperation = new DataTable();
        DataTable dtResault = new DataTable();
        int order;
        protected void Page_Load(object sender, EventArgs e)
        {
            StiWebViewer1.Visible = false;
            if (!IsPostBack)
            {
                //
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
                //
                dtTerm = CB.SelectAllTerm();
                ddl_Term.DataTextField = "tterm";
                ddl_Term.DataSource = dtTerm;
                ddl_Term.DataBind();
                ddl_Term.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Term.Items[ddl_Term.Items.Count - 1].Selected = true;
                if (Request.QueryString["Term"]!= null)
                {
                    ddl_Term.SelectedValue = Request.QueryString["Term"].ToString();

                }
                dtDaneshkade = CB.SelectAllDaneshkade();
                ddl_Daneshkade.DataTextField = "namedanesh";
                ddl_Daneshkade.DataValueField = "id";
                ddl_Daneshkade.DataSource = dtDaneshkade;
                ddl_Daneshkade.DataBind();
                ddl_Daneshkade.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Daneshkade.Items[ddl_Daneshkade.Items.Count - 1].Selected = true;
                if (Request.QueryString["Daneshkade"] != null)
                {
                    ddl_Daneshkade.SelectedValue = Request.QueryString["Daneshkade"].ToString();

                }
                dtDepartman = CB.GetAllDepartman();
                ddl_Departman.DataTextField = "namegroup";
                ddl_Departman.DataValueField = "id";
                ddl_Departman.DataSource = dtDepartman;
                ddl_Departman.DataBind();
                ddl_Departman.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Departman.Items[ddl_Departman.Items.Count - 1].Selected = true;
                if (Request.QueryString["Departman"] != null)
                {
                    ddl_Departman.SelectedValue = Request.QueryString["Departman"].ToString();

                }
                //dtCooperation = CB.GetAllTypeCooperation();
                //ddl_Cooperation.DataTextField = "name_nahveh";
                //ddl_Cooperation.DataValueField = "nahveh_hamk";
                //ddl_Cooperation.DataSource = dtCooperation;
                //ddl_Cooperation.DataBind();
                ddl_Cooperation.Items.Add(new ListItem("تمام وقت 32 ساعت", "1"));
                ddl_Cooperation.Items.Add(new ListItem("نیمه وقت", "2"));
                ddl_Cooperation.Items.Add(new ListItem("ساعتی-حق التدریس", "3"));
                ddl_Cooperation.Items.Add(new ListItem("تمام وقت طرح مشمولان", "4"));
                ddl_Cooperation.Items.Add(new ListItem("بورسیه دکترا", "5"));
                ddl_Cooperation.Items.Add(new ListItem("کارمند", "6"));
                ddl_Cooperation.Items.Add(new ListItem("تمام وقت 44 ساعت", "7"));
                ddl_Cooperation.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Cooperation.Items[ddl_Cooperation.Items.Count - 1].Selected = true;
                if (Request.QueryString["Cooperation"] != null)
                {
                    ddl_Cooperation.SelectedValue = Request.QueryString["Cooperation"].ToString();
                }
            }
            if (Session["code_ostad"] != null)
            {
                txt_CodeOstad.Text = Session["code_ostad"].ToString();
            }

        }

        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {
            FD.Term = ddl_Term.SelectedValue;
        }

        //protected void btn_Exit_Click(object sender, EventArgs e)
        //{
        //    Session["code_ostad"] = null;
        //    Response.Redirect("FacultyReports.aspx");
        //}

        protected void EblaghAsatid_OnCheckedChanged(object sender, EventArgs e)
        {
         //if (rdb_EblaghAsatid.Checked == true || rdb_EblaghExam.Checked == true)
          //{
          //    rdb_EblaghBarAsasGroup.Visible = true;
          //    rdb_EblaghBarAsasName.Visible = true;
          //}
          //else
         //{
          //    rdb_EblaghBarAsasGroup.Visible = false;
         //    rdb_EblaghBarAsasName.Visible = false;
         //}
        }

        protected void btn_ShowReport_Click(object sender, EventArgs e)
        {
            img_ExportToExcel1.Visible = false;
            img_ExportToExcel2.Visible = false;
            img_ExportToExcel3.Visible = false;
            img_ExportToExcel4.Visible = false;
            img_ExportToExcel5.Visible = false;
            img_ExportToExcel6.Visible = false;
            if (rdb_ListSabegheByTerm.Checked == true)
            {
                if (txt_CodeOstad.Text == string.Empty)
                {
                    txt_CodeOstad.Text = "0";
                }
                if (ddl_Daneshkade.SelectedValue == null)
                {
                    ddl_Daneshkade.SelectedValue = "0";
                }
                if (ddl_Departman.SelectedValue == null)
                {
                    ddl_Departman.SelectedValue = "0";
                }
                if (ddl_Cooperation.SelectedValue == null)
                {
                    ddl_Cooperation.SelectedValue = "0";
                }
                string AzTerm = txt_FromTerm.Text;
                string TaTerm = txt_ToTerm.Text;
                dtResault = FRB.GetTeachingExperienceProf(txt_CodeOstad.Text, int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Departman.SelectedValue), int.Parse(ddl_Cooperation.SelectedValue), AzTerm, TaTerm);

                if (dtResault.Rows.Count == 0)
                {
                    RadWindowManager1.RadAlert("رکوردی یافت نشد ", 0, 100, "پیام", "");
                }
                else
                {
                    img_ExportToExcel1.Visible = true;
                    this.StiWebViewer1.ResetReport();
                    StiWebViewer1.Visible = true;
                    StiReport rpt = new StiReport();
                    rpt.Load(Server.MapPath("../Report/ReportListOfSelectedCoursesTeachersByTerm1.mrt"));
                    rpt.ReportCacheMode = StiReportCacheMode.On;
                    rpt.Dictionary.Databases.Clear();
                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                    rpt.Compile();
                    rpt.CompiledReport.DataSources["[Faculty].[SP_TeachingExprerienceProfByTerm]"].Parameters["@idostad"].ParameterValue = txt_CodeOstad.Text;
                    rpt.CompiledReport.DataSources["[Faculty].[SP_TeachingExprerienceProfByTerm]"].Parameters["@Tatterm"].ParameterValue = TaTerm;
                    rpt.CompiledReport.DataSources["[Faculty].[SP_TeachingExprerienceProfByTerm]"].Parameters["@iddanesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_TeachingExprerienceProfByTerm]"].Parameters["@idgroup"].ParameterValue = int.Parse(ddl_Departman.SelectedValue);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_TeachingExprerienceProfByTerm]"].Parameters["@idnahveh"].ParameterValue = int.Parse(ddl_Cooperation.SelectedValue);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_TeachingExprerienceProfByTerm]"].Parameters["@Aztterm"].ParameterValue = AzTerm;
                    rpt.RegData(dtResault);
                    rpt.Dictionary.Synchronize();
                    //rpt.Show();
                    StiWebViewer1.Report = rpt;
                    StiWebViewer1.Visible = true;
                    //rpt.Print(true);
                }
                if (txt_CodeOstad.Text == "0" || txt_CodeOstad.Text == "")
                {
                    txt_CodeOstad.Text = "";
                    Session["code_ostad"] = null; ;
                }
                //txt_CodeOstad.Text = "";
                Session["code_ostad"] = null;

            }
            else if (ddl_Term.SelectedValue == null || ddl_Term.SelectedValue=="0")
            {
                RadWindowManager1.RadAlert("لطفا ترم مورد نظر را انتخاب بفرمایید", 0, 100, "پیام", "");
            }
            else 
            {
                if (rdb_EblaghAsatid.Checked == true)
                {

                    //if (rdb_EblaghBarAsasGroup.Checked == true)
                    //{
                    //    order = 1;
                    //}
                    //if (rdb_EblaghBarAsasName.Checked == true)
                    //{
                    //    order = 2;
                    //}
                    order = 1;
                    if (txt_CodeOstad.Text == string.Empty)
                    {
                        txt_CodeOstad.Text = "0";
                    }
                    if (ddl_Daneshkade.SelectedValue == null)
                    {
                        ddl_Daneshkade.SelectedValue = "0";
                    }
                    if (ddl_Departman.SelectedValue == null)
                    {
                        ddl_Departman.SelectedValue = "0";
                    }
                    if (ddl_Cooperation.SelectedValue == null)
                    {
                        ddl_Cooperation.SelectedValue = "0";
                    }

                    dtResault = FRB.GetNotificationProf(txt_CodeOstad.Text, ddl_Term.SelectedValue, int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Departman.SelectedValue), int.Parse(ddl_Cooperation.SelectedValue), order);

                    if (dtResault.Rows.Count == 0)
                    {
                        RadWindowManager1.RadAlert("رکوردی یافت نشد ", 0, 100, "پیام", "");
                    }
                    else
                    {
                        img_ExportToExcel2.Visible = true;
                        this.StiWebViewer1.ResetReport();
                        StiWebViewer1.Visible = true;
                        StiReport rpt = new StiReport();
                        rpt.Load(Server.MapPath("../Report/ReportNotificationProfByGroup.mrt"));
                        rpt.ReportCacheMode = StiReportCacheMode.On;
                        rpt.Dictionary.Databases.Clear();
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                        rpt.Compile();
                        rpt.CompiledReport.DataSources["[Faculty].[SP_NotificationProfessors]"].Parameters["@idostad"].ParameterValue = txt_CodeOstad.Text;
                        rpt.CompiledReport.DataSources["[Faculty].[SP_NotificationProfessors]"].Parameters["@tterm"].ParameterValue = ddl_Term.SelectedValue;
                        rpt.CompiledReport.DataSources["[Faculty].[SP_NotificationProfessors]"].Parameters["@iddanesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                        rpt.CompiledReport.DataSources["[Faculty].[SP_NotificationProfessors]"].Parameters["@idgroup"].ParameterValue = int.Parse(ddl_Departman.SelectedValue);
                        rpt.CompiledReport.DataSources["[Faculty].[SP_NotificationProfessors]"].Parameters["@idnahveh"].ParameterValue = int.Parse(ddl_Cooperation.SelectedValue);
                        rpt.CompiledReport.DataSources["[Faculty].[SP_NotificationProfessors]"].Parameters["@order"].ParameterValue = order;
                        rpt.RegData(dtResault);
                        rpt.Dictionary.Synchronize();
                        //rpt.Show();
                        StiWebViewer1.Report = rpt;
                        StiWebViewer1.Visible = true;
                        //rpt.Print(true);
                    }
                    if (txt_CodeOstad.Text == "0" || txt_CodeOstad.Text == "")
                    {
                        txt_CodeOstad.Text = "";
                        Session["code_ostad"] = null; ;
                    }
                    //txt_CodeOstad.Text = "";
                    Session["code_ostad"] = null;
                }

                else if (rdb_EblaghExam.Checked == true)
                {
                    //if (rdb_EblaghBarAsasGroup.Checked == true)
                    //{
                    //    order = 1;
                    //}
                    //if (rdb_EblaghBarAsasName.Checked == true)
                    //{
                    //    order = 2;
                    //}
                    order = 1;
                    if (txt_CodeOstad.Text == string.Empty)
                    {
                        txt_CodeOstad.Text = "0";
                    }
                    if (ddl_Daneshkade.SelectedValue == null)
                    {
                        ddl_Daneshkade.SelectedValue = "0";
                    }
                    if (ddl_Departman.SelectedValue == null)
                    {
                        ddl_Departman.SelectedValue = "0";
                    }
                    if (ddl_Cooperation.SelectedValue == null)
                    {
                        ddl_Cooperation.SelectedValue = "0";
                    }
                    dtResault = FRB.GetNotificationProf(txt_CodeOstad.Text, ddl_Term.SelectedValue, int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Departman.SelectedValue), int.Parse(ddl_Cooperation.SelectedValue), order);

                    if (dtResault.Rows.Count == 0)
                    {
                        RadWindowManager1.RadAlert("رکوردی یافت نشد ", 0, 100, "پیام", "");
                    }
                    else if (chk_koli.Checked == true)
                    {
                        img_ExportToExcel3.Visible = true;
                        this.StiWebViewer1.ResetReport();
                        StiWebViewer1.Visible = true;
                        StiReport rpt = new StiReport();
                        rpt.Load(Server.MapPath("../Report/ReportNotificationReportAll.mrt"));
                        rpt.ReportCacheMode = StiReportCacheMode.On;
                        rpt.Dictionary.Databases.Clear();
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                        rpt.Compile();
                        rpt.CompiledReport.DataSources["[Faculty].[SP_NotificationProfessors]"].Parameters["@idostad"].ParameterValue = txt_CodeOstad.Text;
                        rpt.CompiledReport.DataSources["[Faculty].[SP_NotificationProfessors]"].Parameters["@tterm"].ParameterValue = ddl_Term.SelectedValue;
                        rpt.CompiledReport.DataSources["[Faculty].[SP_NotificationProfessors]"].Parameters["@iddanesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                        rpt.CompiledReport.DataSources["[Faculty].[SP_NotificationProfessors]"].Parameters["@idgroup"].ParameterValue = int.Parse(ddl_Departman.SelectedValue);
                        rpt.CompiledReport.DataSources["[Faculty].[SP_NotificationProfessors]"].Parameters["@idnahveh"].ParameterValue = int.Parse(ddl_Cooperation.SelectedValue);
                        rpt.CompiledReport.DataSources["[Faculty].[SP_NotificationProfessors]"].Parameters["@order"].ParameterValue = order;
                        rpt.RegData(dtResault);
                        rpt.Dictionary.Synchronize();
                        //rpt.Show();
                        StiWebViewer1.Report = rpt;
                        StiWebViewer1.Visible = true;
                        //rpt.Print(true);
                    }
                    else
                    {
                        img_ExportToExcel4.Visible = true;
                        this.StiWebViewer1.ResetReport();
                        StiWebViewer1.Visible = true;
                        StiReport rpt = new StiReport();
                        rpt.Load(Server.MapPath("../Report/ReportNotificationProfessors.mrt"));
                        rpt.ReportCacheMode = StiReportCacheMode.On;
                        rpt.Dictionary.Databases.Clear();
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                        rpt.Compile();
                        rpt.CompiledReport.DataSources["[Faculty].[SP_NotificationProfessors]"].Parameters["@idostad"].ParameterValue = txt_CodeOstad.Text;
                        rpt.CompiledReport.DataSources["[Faculty].[SP_NotificationProfessors]"].Parameters["@tterm"].ParameterValue = ddl_Term.SelectedValue;
                        rpt.CompiledReport.DataSources["[Faculty].[SP_NotificationProfessors]"].Parameters["@iddanesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                        rpt.CompiledReport.DataSources["[Faculty].[SP_NotificationProfessors]"].Parameters["@idgroup"].ParameterValue = int.Parse(ddl_Departman.SelectedValue);
                        rpt.CompiledReport.DataSources["[Faculty].[SP_NotificationProfessors]"].Parameters["@idnahveh"].ParameterValue = int.Parse(ddl_Cooperation.SelectedValue);
                        rpt.CompiledReport.DataSources["[Faculty].[SP_NotificationProfessors]"].Parameters["@order"].ParameterValue = order;
                        rpt.RegData(dtResault);
                        rpt.Dictionary.Synchronize();
                        //rpt.Show();
                        StiWebViewer1.Report = rpt;
                        StiWebViewer1.Visible = true;
                        //rpt.Print(true);
                    }
                    if (txt_CodeOstad.Text == "0" || txt_CodeOstad.Text == "")
                    {
                        txt_CodeOstad.Text = "";
                        Session["code_ostad"] = null; ;
                    }
                    //txt_CodeOstad.Text = "";
                    Session["code_ostad"] = null;
                }
                else if (rdb_TedadVahed.Checked == true)
                {
                    //if (rdb_EblaghBarAsasGroup.Checked == true)
                    //{
                    //    order = 1;
                    //}
                    //if (rdb_EblaghBarAsasName.Checked == true)
                    //{
                    //    order = 2;
                    //}
                    order = 1;
                    if (txt_CodeOstad.Text == string.Empty)
                    {
                        txt_CodeOstad.Text = "0";
                    }
                    if (ddl_Daneshkade.SelectedValue == null)
                    {
                        ddl_Daneshkade.SelectedValue = "0";
                    }
                    if (ddl_Departman.SelectedValue == null)
                    {
                        ddl_Departman.SelectedValue = "0";
                    }
                    if (ddl_Cooperation.SelectedValue == null)
                    {
                        ddl_Cooperation.SelectedValue = "0";
                    }

                    dtResault = FRB.GetListOfSelectedCoursesTeachers(txt_CodeOstad.Text, ddl_Term.SelectedValue, int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Departman.SelectedValue), int.Parse(ddl_Cooperation.SelectedValue));

                    if (dtResault.Rows.Count == 0)
                    {
                        RadWindowManager1.RadAlert("رکوردی یافت نشد ", 0, 100, "پیام", "");
                    }
                    else
                    {
                        //Report ...
                        img_ExportToExcel5.Visible = true;
                        this.StiWebViewer1.ResetReport();
                        StiWebViewer1.Visible = true;
                        StiReport rpt = new StiReport();
                        rpt.Load(Server.MapPath("../Report/ReportListOfSelectedCoursesTeachers.mrt"));
                        rpt.ReportCacheMode = StiReportCacheMode.On;
                        rpt.Dictionary.Databases.Clear();
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                        rpt.Compile();
                        rpt.CompiledReport.DataSources["[Faculty].[SP_TeachingExprerienceProf]"].Parameters["@idostad"].ParameterValue = txt_CodeOstad.Text;
                        rpt.CompiledReport.DataSources["[Faculty].[SP_TeachingExprerienceProf]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                        rpt.CompiledReport.DataSources["[Faculty].[SP_TeachingExprerienceProf]"].Parameters["@iddanesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                        rpt.CompiledReport.DataSources["[Faculty].[SP_TeachingExprerienceProf]"].Parameters["@idgroup"].ParameterValue = int.Parse(ddl_Departman.SelectedValue);
                        rpt.CompiledReport.DataSources["[Faculty].[SP_TeachingExprerienceProf]"].Parameters["@idnahveh"].ParameterValue = int.Parse(ddl_Cooperation.SelectedValue);
                        //rpt.CompiledReport.DataSources["[Faculty].[SP_TeachingExprerienceProf]"].Parameters["@order"].ParameterValue = order;
                        rpt.RegData(dtResault);
                        rpt.Dictionary.Synchronize();
                        //rpt.Show();
                        StiWebViewer1.Report = rpt;
                        StiWebViewer1.Visible = true;
                        //rpt.Print(true);
                    }
                    if (txt_CodeOstad.Text == "0" || txt_CodeOstad.Text == "")
                    {
                        txt_CodeOstad.Text = "";
                        Session["code_ostad"] = null; ;
                    }
                    //txt_CodeOstad.Text = "";
                    Session["code_ostad"] = null;
                }
                else if (rdb_ListSabegheByTerm.Checked == true)
                {


                }
                else if (rdb_ListSavabeghRuz.Checked == true)
                {
                    if (txt_ListSavabeghRuz.Text == string.Empty)
                    {
                        txt_ListSavabeghRuz.Text = "0";
                    }
                    string Number = txt_ListSavabeghRuz.Text;

                    if (txt_CodeOstad.Text == string.Empty)
                    {
                        txt_CodeOstad.Text = "0";
                    }
                    if (ddl_Daneshkade.SelectedValue == null)
                    {
                        ddl_Daneshkade.SelectedValue = "0";
                    }
                    if (ddl_Departman.SelectedValue == null)
                    {
                        ddl_Departman.SelectedValue = "0";
                    }
                    if (ddl_Cooperation.SelectedValue == null)
                    {
                        ddl_Cooperation.SelectedValue = "0";
                    }
                    dtResault = FRB.TeachingExperienceMoreThanADay(ddl_Term.SelectedValue, int.Parse(txt_CodeOstad.Text), int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Departman.SelectedValue), int.Parse(ddl_Cooperation.SelectedValue), int.Parse(txt_ListSavabeghRuz.Text));
                    if (dtResault.Rows.Count== 0)
                    {
                        RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                    }
                    else
                    {
                        img_ExportToExcel6.Visible = true;
                        this.StiWebViewer1.ResetReport();
                        StiWebViewer1.Visible = true;
                        StiReport rpt = new StiReport();
                        rpt.Load(Server.MapPath("../Report/ReportTeachingExperienceMoreThanADay.mrt"));
                        rpt.ReportCacheMode = StiReportCacheMode.On;
                        rpt.Dictionary.Databases.Clear();
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                        rpt.Compile();
                        rpt.CompiledReport.DataSources["[Faculty].[SP_TeachingExperienceMoreThanADay]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                        rpt.CompiledReport.DataSources["[Faculty].[SP_TeachingExperienceMoreThanADay]"].Parameters["@Number"].ParameterValue = int.Parse(Number);
                        rpt.CompiledReport.DataSources["[Faculty].[SP_TeachingExperienceMoreThanADay]"].Parameters["@Departman"].ParameterValue = int.Parse(ddl_Departman.SelectedValue);
                        rpt.CompiledReport.DataSources["[Faculty].[SP_TeachingExperienceMoreThanADay]"].Parameters["@Cooperation"].ParameterValue = int.Parse(ddl_Cooperation.SelectedValue);
                        rpt.CompiledReport.DataSources["[Faculty].[SP_TeachingExperienceMoreThanADay]"].Parameters["@Daneshkade"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                        rpt.RegData(dtResault);
                        rpt.Dictionary.Synchronize();
                        //rpt.Show();
                        StiWebViewer1.Report = rpt;
                        StiWebViewer1.Visible = true;
                        //rpt.Print(true);
                    }
                    if (txt_CodeOstad.Text == "0" || txt_CodeOstad.Text == "")
                    {
                        txt_CodeOstad.Text = "";
                        Session["code_ostad"] = null; ;
                    }
                    //txt_CodeOstad.Text = "";
                    Session["code_ostad"] = null;
                }
            }
        }

        protected void ddl_Daneshkade_SelectedIndexChanged(object sender, EventArgs e)
        {
            FD.Daneshkade = ddl_Daneshkade.SelectedValue;
            DataTable dt = new DataTable();
            if (ddl_Daneshkade.SelectedValue == "0")
            {
                dtDepartman = CB.GetAllDepartman();
                ddl_Departman.DataTextField = "namegroup";
                ddl_Departman.DataValueField = "id";
                ddl_Departman.DataSource = dtDepartman;
                ddl_Departman.DataBind();
                ddl_Departman.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Departman.Items[ddl_Departman.Items.Count - 1].Selected = true;
                FD.Departman = ddl_Departman.SelectedValue;
            }
            else
            {
                //Session["Daneshkade"] = ddl_Daneshkade.SelectedValue;
                FD.Daneshkade = ddl_Daneshkade.SelectedValue;
                dt = CB.GetAllDepartman(int.Parse(FD.Daneshkade));
                ddl_Departman.DataSource = dt;
                ddl_Departman.DataTextField = "namegroup";
                ddl_Departman.DataValueField = "id";
                ddl_Departman.DataBind();
                ddl_Departman.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Departman.Items[ddl_Departman.Items.Count - 1].Selected = true;
            }
        }

        protected void ddl_Cooperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            FD.Cooperation = ddl_Cooperation.SelectedValue;
        }

        protected void ddl_Departman_SelectedIndexChanged(object sender, EventArgs e)
        {
            FD.Departman = ddl_Departman.SelectedValue;
        }

        protected void btn_SelectCodeOstad_Click(object sender, EventArgs e)
        {
            Session["page"] = 1;
            if (ddl_Daneshkade.SelectedValue == null)
            {
                ddl_Daneshkade.SelectedValue = "0";
            }
            if (ddl_Departman.SelectedValue == null)
            {
                ddl_Departman.SelectedValue = "0";
            }
            if (ddl_Cooperation.SelectedValue == null)
            {
                ddl_Cooperation.SelectedValue = "0";
            }
            if (ddl_Term.SelectedValue==null)
            {
                ddl_Term.SelectedValue = "0";
            }
            //Response.Redirect("SearchProfByParams.aspx?"+"Term"+"="+ddl_Term.SelectedValue+"&"+"Daneshkade"+"="+ddl_Daneshkade.SelectedValue+"&"+"Departman"+"="+ddl_Departman.SelectedValue+"&"+"Cooperation"+"="+ddl_Cooperation.SelectedValue);
            RadWindowManager windowManager = new RadWindowManager();
            RadWindow widnow1 = new RadWindow();
            // Set the window properties   
            widnow1.NavigateUrl = ("../../../university/Faculty/CMS/SearchProfByParams.aspx?" + "Term" + "=" + ddl_Term.SelectedValue + "&" + "Daneshkade" + "=" + ddl_Daneshkade.SelectedValue + "&" + "Departman" + "=" + ddl_Departman.SelectedValue + "&" + "Cooperation" + "=" + ddl_Cooperation.SelectedValue);
            widnow1.ID = "RadWindow1";
            widnow1.VisibleOnPageLoad = true; // Set this property to True for showing window from code 
            widnow1.Height = 600;
            widnow1.Width = 1200;
            windowManager.Windows.Add(widnow1);
            windowManager.Height = 600;
            windowManager.Width = 1200;
            ContentPlaceHolder mpContentPlaceHolder;
            mpContentPlaceHolder = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
            mpContentPlaceHolder.Controls.Add(widnow1);    
        }
        protected void RadAjaxManager1_AjaxRequest1(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "Rebind")
            {
                txt_CodeOstad.Text = Session["code_ostad"].ToString();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        protected void img_ExportToExcel1_Click(object sender, ImageClickEventArgs e)
        {
            if (txt_CodeOstad.Text == string.Empty)
            {
                txt_CodeOstad.Text = "0";
            }
            dtResault = FRB.GetTeachingExperienceProf(txt_CodeOstad.Text, int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Departman.SelectedValue), int.Parse(ddl_Cooperation.SelectedValue), txt_FromTerm.Text, txt_ToTerm.Text);
            if (dtResault.Rows.Count == 0)
            {
            }
            else
            {
                GridView1.DataSource = dtResault;
                GridView1.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportListOfSelectedCoursesTeachersByTerm.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    ////gv_Show.AllowPaging = false;
                    ////this.BindGrid();

                    //gv_Show.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in GridView1.HeaderRow.Cells)
                    {
                        cell.BackColor = GridView1.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        //row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = GridView1.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    GridView1.RenderControl(hw);

                    //style to format numbers to string
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void img_ExportToExcel2_Click(object sender, ImageClickEventArgs e)
        {
            if (txt_CodeOstad.Text == string.Empty)
            {
                txt_CodeOstad.Text = "0";
            }
            order = 1;
            dtResault = FRB.GetNotificationProf(txt_CodeOstad.Text, ddl_Term.SelectedValue, int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Departman.SelectedValue), int.Parse(ddl_Cooperation.SelectedValue), order);
            if (dtResault.Rows.Count == 0)
            {
            }
            else
            {
                GridView2.DataSource = dtResault;
                GridView2.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportNotificationProfByGroup.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    ////gv_Show.AllowPaging = false;
                    ////this.BindGrid();

                    //gv_Show.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in GridView2.HeaderRow.Cells)
                    {
                        cell.BackColor = GridView1.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in GridView2.Rows)
                    {
                        //row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = GridView2.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = GridView2.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    GridView2.RenderControl(hw);

                    //style to format numbers to string
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void img_ExportToExcel3_Click(object sender, ImageClickEventArgs e)
        {
            if (txt_CodeOstad.Text == string.Empty)
            {
                txt_CodeOstad.Text = "0";
            }
            order = 1;
            dtResault = FRB.GetNotificationProf(txt_CodeOstad.Text, ddl_Term.SelectedValue, int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Departman.SelectedValue), int.Parse(ddl_Cooperation.SelectedValue), order);
            if (dtResault.Rows.Count == 0)
            {
            }
            else
            {
                GridView3.DataSource = dtResault;
                GridView3.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportNotificationAll.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    ////gv_Show.AllowPaging = false;
                    ////this.BindGrid();

                    //gv_Show.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in GridView3.HeaderRow.Cells)
                    {
                        cell.BackColor = GridView3.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in GridView3.Rows)
                    {
                        //row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = GridView3.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = GridView3.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    GridView3.RenderControl(hw);

                    //style to format numbers to string
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void img_ExportToExcel4_Click(object sender, ImageClickEventArgs e)
        {
            if (txt_CodeOstad.Text == string.Empty)
            {
                txt_CodeOstad.Text = "0";
            }
            order = 1;
            dtResault = FRB.GetNotificationProf(txt_CodeOstad.Text, ddl_Term.SelectedValue, int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Departman.SelectedValue), int.Parse(ddl_Cooperation.SelectedValue), order);
            if (dtResault.Rows.Count == 0)
            {
            }
            else
            {
                GridView4.DataSource = dtResault;
                GridView4.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportNotificationProfessor.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    ////gv_Show.AllowPaging = false;
                    ////this.BindGrid();

                    //gv_Show.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in GridView4.HeaderRow.Cells)
                    {
                        cell.BackColor = GridView4.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in GridView4.Rows)
                    {
                        //row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = GridView4.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = GridView4.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    GridView4.RenderControl(hw);

                    //style to format numbers to string
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void img_ExportToExcel5_Click(object sender, ImageClickEventArgs e)
        {
            if (txt_CodeOstad.Text == string.Empty)
            {
                txt_CodeOstad.Text = "0";
            }
            dtResault = FRB.GetListOfSelectedCoursesTeachers(txt_CodeOstad.Text, ddl_Term.SelectedValue, int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Departman.SelectedValue), int.Parse(ddl_Cooperation.SelectedValue));
            if (dtResault.Rows.Count == 0)
            {
            }
            else
            {
                GridView5.DataSource = dtResault;
                GridView5.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportListOfSelectedCoursesTeachers.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    ////gv_Show.AllowPaging = false;
                    ////this.BindGrid();

                    //gv_Show.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in GridView5.HeaderRow.Cells)
                    {
                        cell.BackColor = GridView5.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in GridView5.Rows)
                    {
                        //row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = GridView5.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = GridView5.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    GridView1.RenderControl(hw);

                    //style to format numbers to string
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void img_ExportToExcel6_Click(object sender, ImageClickEventArgs e)
        {
            if (txt_CodeOstad.Text == string.Empty)
            {
                txt_CodeOstad.Text = "0";
            }
            dtResault = FRB.TeachingExperienceMoreThanADay(ddl_Term.SelectedValue, int.Parse(txt_CodeOstad.Text), int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Departman.SelectedValue), int.Parse(ddl_Cooperation.SelectedValue), int.Parse(txt_ListSavabeghRuz.Text));
            if (dtResault.Rows.Count == 0)
            {
            }
            else
            {
                GridView6.DataSource = dtResault;
                GridView6.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportTeachingExperienceMoreThanADay.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    ////gv_Show.AllowPaging = false;
                    ////this.BindGrid();

                    //gv_Show.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in GridView6.HeaderRow.Cells)
                    {
                        cell.BackColor = GridView6.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in GridView6.Rows)
                    {
                        //row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = GridView6.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = GridView6.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    GridView6.RenderControl(hw);

                    //style to format numbers to string
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        } 
    }
}