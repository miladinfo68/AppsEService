using IAUEC_Apps.Business.university.Education;
using IAUEC_Apps.Business.university.Faculty;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.University.Faculty;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.University.Faculty.CMS
{
    public partial class ArzeshyabyAsatid : System.Web.UI.Page
    {
        
        public static TeacherEvalutionDTO TED = new TeacherEvalutionDTO();
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();
        EducationReportBusiness ERB = new EducationReportBusiness();
        DataTable dtGroup = new DataTable();
        DataTable dtTerm = new DataTable();
        DataTable dtDars = new DataTable();
        DataTable dt = new DataTable();
        CommonBusiness CB = new CommonBusiness();
        int Order;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtTerm = CB.SelectAllTerm();
                ddl_Term.DataTextField = "tterm";
                ddl_Term.DataSource = dtTerm;
                ddl_Term.DataBind();
                ddl_Term.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Term.Items[ddl_Term.Items.Count - 1].Selected = true;
                if (Request.QueryString["Term"] != null)
                {
                    ddl_Term.SelectedValue = Request.QueryString["Term"].ToString();
                }
                int iddanesh = 0;
                if (Session["RoleID"].ToString() == "51" || Session["RoleID"].ToString() == "17")
                    iddanesh = 1;
                if (Session["RoleID"].ToString() == "52" || Session["RoleID"].ToString() == "16")
                    iddanesh = 3;
                if (Session["RoleID"].ToString() == "53" || Session["RoleID"].ToString() == "15")
                    iddanesh = 2;
                 
                        
                dtGroup = FRB.GetAllGroup(iddanesh);
                ddl_CodeGroup.DataTextField = "namegroup";
                ddl_CodeGroup.DataValueField = "id";
                ddl_CodeGroup.DataSource = dtGroup;
                ddl_CodeGroup.DataBind();
                ddl_CodeGroup.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_CodeGroup.Items[ddl_CodeGroup.Items.Count - 1].Selected = true;
                if (Request.QueryString["Departman"] != null)
                {
                    ddl_CodeGroup.SelectedValue = Request.QueryString["Departman"].ToString();

                }
                dtDars = ERB.GetListDorus();
                ddl_CodeDras.DataTextField = "namedars";
                ddl_CodeDras.DataValueField = "dcode";
                ddl_CodeDras.DataSource = dtDars;
                ddl_CodeDras.DataBind();
                ddl_CodeDras.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_CodeDras.Items[ddl_CodeDras.Items.Count - 1].Selected = true;
                if (Request.QueryString["Lesson"] != null)
                {
                    ddl_CodeDras.SelectedValue = Request.QueryString["Lesson"].ToString();

                }
            }
            if (Session["code_ostad"] != null)
                txt_CodeOstad.Text = Session["code_ostad"].ToString();
        }

        protected void ddl_CodeGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            TED.Departman = ddl_CodeGroup.SelectedValue;
        }

        protected void ddl_CodeDras_SelectedIndexChanged(object sender, EventArgs e)
        {
            TED.Lesson = ddl_CodeDras.SelectedValue;
        }

        protected void btn_EvalutionProf_Click(object sender, EventArgs e)
        {
            if (TED.Term == null)
            {
                RadWindowManager1.RadAlert("لطفا ترم را انتخاب بفرمایید", 0, 100, "پیام", "");
            }
            else
            {
                if (txt_CodeOstad.Text == "")
                {
                    txt_CodeOstad.Text = "0";
                }
                if (TED.Departman == null)
                {
                    TED.Departman = "0";
                }
                if (TED.Lesson == null)
                {
                    TED.Lesson = "0";
                }
                Order = 1;
                dt = FRB.GetEvalutionAllProf(TED.Term, int.Parse(txt_CodeOstad.Text), int.Parse(TED.Departman), int.Parse(TED.Lesson), Order);
                if (dt.Rows.Count == 0)
                {
                    RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                }
                else
                {
                    this.StiWebViewer1.ResetReport();
                    StiReport rpt = new StiReport();
                    rpt.Load(Server.MapPath("../Report/ReportEvalutionProf.mrt"));
                    rpt.ReportCacheMode = StiReportCacheMode.On;
                    rpt.Dictionary.Databases.Clear();
                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                    rpt.Compile();
                    rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProf]"].Parameters["@Term"].ParameterValue = TED.Term;
                    rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProf]"].Parameters["@CodeOstad"].ParameterValue = int.Parse(txt_CodeOstad.Text);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProf]"].Parameters["@Lesson"].ParameterValue = int.Parse(TED.Lesson);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProf]"].Parameters["@Departman"].ParameterValue = int.Parse(TED.Departman);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProf]"].Parameters["@Order"].ParameterValue = Order;
                    ((StiSqlSource)rpt.Dictionary.DataSources["[Faculty].[SP_EvalutionProf]"]).CommandTimeout = 30000;
                    rpt.RegData(dt);
                    rpt.Dictionary.Synchronize();
                    //rpt.Show();
                    StiWebViewer1.Report = rpt;
                    StiWebViewer1.Visible = true;
                    //rpt.Print(true);
                }
                if (txt_CodeOstad.Text == "0" || txt_CodeOstad.Text == "")
                {
                    txt_CodeOstad.Text = "";
                    Session["code_ostad"] = null;
                }

            }
            //txt_CodeOstad.Text = "";
            Session["code_ostad"] = null;
             
        }

        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {
            TED.Term = ddl_Term.SelectedValue;
        }

        protected void btn_Select_Click(object sender, EventArgs e)
        {
            if (TED.Departman == null)
            {
                TED.Departman = "0";
            }
            if (TED.Lesson == null)
            {
                TED.Lesson = "0";
            }
            if (TED.Term == null)
            {
                TED.Term = "0";
            }
            Session["page"] = 2;
            Response.Redirect("SearchProfByParams.aspx?" + "Term" + "=" + TED.Term + "&" + "Lesson" + "=" + TED.Lesson + "&" + "Departman" + "=" + TED.Departman);
        }

        protected void btn_EvalutionProfByItem_Click(object sender, EventArgs e)
        {
            //this.StiWebViewer1.ResetReport();
            if (TED.Term == null)
            {
                RadWindowManager1.RadAlert("لطفا ترم را انتخاب بفرمایید", 0, 100, "پیام", "");
            }
            else
            {
                if (txt_CodeOstad.Text == string.Empty)
                {
                    txt_CodeOstad.Text = "0";
                }
                if (TED.Departman == null)
                {
                    TED.Departman = "0";
                }
                if (TED.Lesson == null)
                {
                    TED.Lesson = "0";
                }
                Order = 1;
                dt = FRB.GetEvalutionProfDividedODR(TED.Term, int.Parse(txt_CodeOstad.Text), int.Parse(TED.Departman), int.Parse(TED.Lesson), Order);
                if (dt.Rows.Count == 0)
                {
                    RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                }
                else
                {
                    this.StiWebViewer1.ResetReport();
                    StiReport rpt = new StiReport();
                    rpt.Load(Server.MapPath("../Report/ReportEvalutionProfDividedODR.mrt"));
                    rpt.Dictionary.Databases.Clear();
                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                    rpt.ReportCacheMode = StiReportCacheMode.On;
                    rpt.Compile();
                    rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODR]"].Parameters["@Term"].ParameterValue = TED.Term;
                    rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODR]"].Parameters["@CodeOstad"].ParameterValue = int.Parse(txt_CodeOstad.Text);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODR]"].Parameters["@Lesson"].ParameterValue = int.Parse(TED.Lesson);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODR]"].Parameters["@Departman"].ParameterValue = int.Parse(TED.Departman);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODR]"].Parameters["@Order"].ParameterValue = Order;
                    ((StiSqlSource)rpt.Dictionary.DataSources["[Faculty].[SP_EvalutionProfDividedODR]"]).CommandTimeout = 30000;
                    rpt.RegData(dt);
                    rpt.Dictionary.Synchronize();
                    //rpt.Show();
                    StiWebViewer1.Report = rpt;
                    StiWebViewer1.Visible = true;
                    //rpt.Print(true);
                }
                if (txt_CodeOstad.Text == "0" || txt_CodeOstad.Text == "")
                {
                    txt_CodeOstad.Text = string.Empty;
                }
            }
        }

        protected void btn_EvalutionProfByItemLesson_Click(object sender, EventArgs e)
        {
            if (TED.Term == string.Empty)
            {
                RadWindowManager1.RadAlert("لطفا ترم را انتخاب بفرمایید", 0, 100, "پیام", "");
            }
            else
            {
                if (txt_CodeOstad.Text == string.Empty)
                {
                    txt_CodeOstad.Text = "0";
                }
                if (TED.Departman == null)
                {
                    TED.Departman = "0";
                }
                if (TED.Lesson == null)
                {
                    TED.Lesson = "0";
                }
                Order = 1;
                dt = FRB.GetEvalutionProfDividedODD(TED.Term, int.Parse(txt_CodeOstad.Text), int.Parse(TED.Departman), int.Parse(TED.Lesson), Order);
                if (dt.Rows.Count == 0)
                {
                    RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                }
                else
                {
                    this.StiWebViewer1.ResetReport();
                    StiReport rpt = new StiReport();
                    rpt.Load(Server.MapPath("../Report/ReportEvalutionProfDividedODD.mrt"));
                    rpt.Dictionary.Databases.Clear();
                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                    rpt.ReportCacheMode = StiReportCacheMode.On;
                    rpt.Compile();
                    rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODD]"].Parameters["@Term"].ParameterValue = TED.Term;
                    rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODD]"].Parameters["@CodeOstad"].ParameterValue = int.Parse(txt_CodeOstad.Text);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODD]"].Parameters["@Lesson"].ParameterValue = int.Parse(TED.Lesson);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODD]"].Parameters["@Departman"].ParameterValue = int.Parse(TED.Departman);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODD]"].Parameters["@Order"].ParameterValue = Order;
                    ((StiSqlSource)rpt.Dictionary.DataSources["[Faculty].[SP_EvalutionProfDividedODD]"]).CommandTimeout = 10000;
                    rpt.RegData(dt);
                    rpt.Dictionary.Synchronize();
                    //rpt.Show();
                    StiWebViewer1.Report = rpt;
                    StiWebViewer1.Visible = true;
                    //rpt.Print(true); 
                }
                if (txt_CodeOstad.Text == "0" || txt_CodeOstad.Text == "")
                {
                    txt_CodeOstad.Text = "";
                    Session["code_ostad"] = null;
                }
            }
            //txt_CodeOstad.Text = "";
            Session["code_ostad"] = null;
        }

        protected void btn_EvalutionAll_Click(object sender, EventArgs e)
        {
            if (TED.Term == null)
            {
                RadWindowManager1.RadAlert("لطفا ترم را انتخاب بفرمایید", 0, 100, "پیام", "");
            }
            else
            {
                if (txt_CodeOstad.Text == string.Empty)
                {
                    txt_CodeOstad.Text = "0";
                }
                if (TED.Departman == null)
                {
                    TED.Departman = "0";
                }
                if (TED.Lesson == null)
                {
                    TED.Lesson = "0";
                }
                //if (chk_NameOstad.Checked == true && chk_ScoreOstad.Checked == false)
                //{
                //    Order = 1;
                //}
                //if (chk_NameOstad.Checked == false && chk_ScoreOstad.Checked == true)
                //{
                //    Order = 2;
                //}
                //if (chk_NameOstad.Checked == true && chk_ScoreOstad.Checked == true)
                //{
                //    Order = 3;
                //}
                //if (chk_NameOstad.Checked == false && chk_ScoreOstad.Checked == false)
                //{
                //    Order = 1;
                //}
                Order = 1;
               dt = FRB.GetEvalutionProfDividedODDQ(TED.Term,int.Parse(txt_CodeOstad.Text),int.Parse(TED.Departman),int.Parse(TED.Lesson),Order);
               if (dt.Rows.Count == 0)
               {
                   RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
               }
               else
               {
                   this.StiWebViewer1.ResetReport();
                   StiReport rpt = new StiReport();
                   rpt.Load(Server.MapPath("../Report/ReportEvalutionProfDividedODDQ.mrt"));
                   rpt.ReportCacheMode = StiReportCacheMode.On;
                   rpt.Dictionary.Databases.Clear();
                   rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                   rpt.Compile();
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODDQ]"].Parameters["@Term"].ParameterValue = TED.Term;
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODDQ]"].Parameters["@CodeOstad"].ParameterValue =int.Parse(txt_CodeOstad.Text);
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODDQ]"].Parameters["@Lesson"].ParameterValue = int.Parse(TED.Lesson);
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODDQ]"].Parameters["@Departman"].ParameterValue = int.Parse(TED.Departman);
                   rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedODDQ]"].Parameters["@Order"].ParameterValue = Order;
                   ((StiSqlSource)rpt.Dictionary.DataSources["[Faculty].[SP_EvalutionProfDividedODDQ]"]).CommandTimeout = 30000;
                   rpt.RegData(dt);
                   rpt.Dictionary.Synchronize();
                   StiWebViewer1.Report = rpt;
                   StiWebViewer1.Visible = true;

               }
               if (txt_CodeOstad.Text == "0" || txt_CodeOstad.Text == "")
               {
                   txt_CodeOstad.Text = "";
                   Session["code_ostad"] = null;
               }

            }
            Session["code_ostad"] = null;
         }

        protected void btn_EvalutionProfByLesson_Click(object sender, EventArgs e)
        {
            if (TED.Term == null)
            {
                RadWindowManager1.RadAlert("لطفا ترم را انتخاب بفرمایید", 0, 100, "پیام", "");
            }
            else
            {
                if (txt_CodeOstad.Text == string.Empty)
                {
                    txt_CodeOstad.Text = "0";
                }
                if (TED.Departman == null)
                {
                    TED.Departman = "0";
                }
                if (TED.Lesson == null)
                {
                    TED.Lesson = "0";
                }
                //if (chk_NameOstad.Checked == true && chk_ScoreOstad.Checked == false)
                //{
                //    Order = 1;
                //}
                //if (chk_NameOstad.Checked == false && chk_ScoreOstad.Checked == true)
                //{
                //    Order = 2;
                //}
                //if (chk_NameOstad.Checked == true && chk_ScoreOstad.Checked == true)
                //{
                //    Order = 3;
                //}
                //if (chk_NameOstad.Checked == false && chk_ScoreOstad.Checked == false)
                //{
                //    Order = 1;
                //}
                Order = 1;
                dt = FRB.GetEvalutionProfDividedDid(TED.Term, int.Parse(txt_CodeOstad.Text), int.Parse(TED.Departman), int.Parse(TED.Lesson), Order);
                if (dt.Rows.Count == 0)
                {
                    RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                }
                else
                {
                    this.StiWebViewer1.ResetReport();
                    StiReport rpt = new StiReport();
                    rpt.Load(Server.MapPath("../Report/ReportEvalutionProfDividedDid.mrt"));
                    rpt.ReportCacheMode = StiReportCacheMode.On;
                    rpt.Dictionary.Databases.Clear();
                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                    rpt.Compile();
                    rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedDid]"].Parameters["@Term"].ParameterValue = TED.Term;
                    rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedDid]"].Parameters["@CodeOstad"].ParameterValue = int.Parse(txt_CodeOstad.Text);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedDid]"].Parameters["@Lesson"].ParameterValue = int.Parse(TED.Lesson);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedDid]"].Parameters["@Departman"].ParameterValue = int.Parse(TED.Departman);
                    rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedDid]"].Parameters["@Order"].ParameterValue = Order;
                    ((StiSqlSource)rpt.Dictionary.DataSources["[Faculty].[SP_EvalutionProfDividedDid]"]).CommandTimeout = 30000;
                    rpt.RegData(dt);
                    rpt.Dictionary.Synchronize();
                    StiWebViewer1.Report = rpt;
                    StiWebViewer1.Visible = true;

                }
                if (txt_CodeOstad.Text == "0" || txt_CodeOstad.Text == "")
                {
                    txt_CodeOstad.Text = "";
                    Session["code_ostad"] = null;
                }

            }
            Session["code_ostad"] = null;
        }
        }

        //protected void btn_EvalutionProfByLesson_Click(object sender, EventArgs e)
        //{
        //    if (TED.Term == null)
        //    {
        //        RadWindowManager1.RadAlert("لطفا ترم را انتخاب بفرمایید", 0, 100, "پیام", "");
        //    }
        //    else
        //    {
        //        if (txt_CodeOstad.Text == string.Empty)
        //        {
        //            txt_CodeOstad.Text = "0";
        //        }
        //        if (TED.Departman == null)
        //        {
        //            TED.Departman = "0";
        //        }
        //        if (TED.Lesson == null)
        //        {
        //            TED.Lesson = "0";
        //        }
        //        //if (chk_NameOstad.Checked == true && chk_ScoreOstad.Checked == false)
        //        //{
        //        //    Order = 1;
        //        //}
        //        //if (chk_NameOstad.Checked == false && chk_ScoreOstad.Checked == true)
        //        //{
        //        //    Order = 2;
        //        //}
        //        //if (chk_NameOstad.Checked == true && chk_ScoreOstad.Checked == true)
        //        //{
        //        //    Order = 3;
        //        //}
        //        //if (chk_NameOstad.Checked == false && chk_ScoreOstad.Checked == false)
        //        //{
        //        //    Order = 1;
        //        //}
        //        Order = 1;
        //        dt = FRB.GetEvalutionProfDividedDid(TED.Term, int.Parse(txt_CodeOstad.Text), int.Parse(TED.Departman), int.Parse(TED.Lesson), Order);
        //        if (dt.Rows.Count == 0)
        //        {
        //            RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
        //        }
        //        else
        //        {
        //            this.StiWebViewer1.ResetReport();
        //            StiReport rpt = new StiReport();
        //            rpt.Load(Server.MapPath("../Report/ReportEvalutionProfDividedDid].mrt"));
        //            rpt.ReportCacheMode = StiReportCacheMode.On;
        //            rpt.Dictionary.Databases.Clear();
        //            rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", dao.ReportConnection.ToString()));
        //            rpt.Compile();

        //            rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedDid]"].Parameters["@Term"].ParameterValue = TED.Term;
        //            rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedDid]"].Parameters["@CodeOstad"].ParameterValue = txt_CodeOstad.Text;
        //            rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedDid]"].Parameters["@Lesson"].ParameterValue = int.Parse(TED.Lesson);
        //            rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedDid]"].Parameters["@Departman"].ParameterValue = int.Parse(TED.Departman);
        //            rpt.CompiledReport.DataSources["[Faculty].[SP_EvalutionProfDividedDid]"].Parameters["@Order"].ParameterValue = Order;
        //            ((StiSqlSource)rpt.Dictionary.DataSources["[Faculty].[SP_EvalutionProfDividedDid]"]).CommandTimeout = 10000;
        //            rpt.RegData(dt);
        //            rpt.Dictionary.Synchronize();
        //            //rpt.Show();
        //            StiWebViewer1.Report = rpt;
        //            StiWebViewer1.Visible = true;
        //            //rpt.Print(true);
        //        }
        //        if (txt_CodeOstad.Text == "0" || txt_CodeOstad.Text == "")
        //        {
        //            txt_CodeOstad.Text = "";
        //            Session["code_ostad"] = null;
        //        }
        //    }
        //    //txt_CodeOstad.Text = "";
        //    //Session["code_ostad"] = null;
        //}
    }
