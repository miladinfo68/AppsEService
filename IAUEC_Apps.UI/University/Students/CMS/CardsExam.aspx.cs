using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Education;
using IAUEC_Apps.Business.university.Students;

using IAUEC_Apps.DTO.University.Students;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System;

using System.Data;
using System.Globalization;
using System.IO;

using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.University.Students.CMS
{
    public partial class CardsExam : System.Web.UI.Page
    {
        CardQuizTemporaryStudentsBusiness CQTSB = new CardQuizTemporaryStudentsBusiness();
        public static CardsQuizTemporaryStudentsDTO  CQTS = new CardsQuizTemporaryStudentsDTO();
        //CommonDAO dao = new CommonDAO();
        DataTable dtTerm = new DataTable();
        DataTable dtDepartman = new DataTable();
        DataTable dtDaneshkade = new DataTable();
        DataTable dtCooperation = new DataTable();
        DataTable dtVaziatNomre = new DataTable();
        DataTable dtField = new DataTable();
        CommonBusiness CB = new CommonBusiness();
        EducationReportBusiness ERB = new EducationReportBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            StiWebViewer1.Visible = false;
            if (!IsPostBack)
            {
                dtTerm=CB.SelectAllTerm();
                ddl_Term.DataTextField = "tterm";
                ddl_Term.DataSource = dtTerm;
                ddl_Term.DataBind();
                ddl_Term.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Term.Items[ddl_Term.Items.Count - 1].Selected = true;
                if (Request.QueryString["Term"] != null)
                {
                    ddl_Term.SelectedValue = Request.QueryString["Term"].ToString();
                }
                dtDaneshkade=CB.SelectAllDaneshkade();
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

                dtField = CB.SelectAllField();
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dtField;
                ddl_Field.DataBind();
                ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Field.Items[ddl_Field.Items.Count - 1].Selected = true;
                if (Request.QueryString["Field"] != null)
                {
                    ddl_Field.SelectedValue = Request.QueryString["Field"].ToString();
                }
                ddl_Degree.Items.Add(new ListItem("کاردانی", "2"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی", "1"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ناپیوسته", "3"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ارشد پیوسته", "4"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ارشد ناپیوسته", "5"));
                ddl_Degree.Items.Add(new ListItem("دکتری تخصصی", "7"));
                ddl_Degree.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Degree.Items[ddl_Degree.Items.Count - 1].Selected = true;
                if (Request.QueryString["Degree"] != null)
                {
                    ddl_Degree.SelectedValue = Request.QueryString["Degree"].ToString();
                }
                ddl_Dorpar.Items.Add(new ListItem("دوره ای", "1"));
                ddl_Dorpar.Items.Add(new ListItem("پاره وقت", "2"));
                ddl_Dorpar.Items.Add(new ListItem("طرح معلمان", "3"));
                ddl_Dorpar.Items.Add(new ListItem("قراردادی", "4"));
                ddl_Dorpar.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Dorpar.Items[ddl_Dorpar.Items.Count - 1].Selected = true;
                if (Request.QueryString["Education"] != null)
                {
                    ddl_Dorpar.SelectedValue = Request.QueryString["Education"].ToString();
                }
                ddl_Sex.Items.Add(new ListItem("مرد", "1"));
                ddl_Sex.Items.Add(new ListItem("زن", "2"));
                ddl_Sex.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Sex.Items[ddl_Sex.Items.Count - 1].Selected = true;
                if (Request.QueryString["Sex"] != null)
                {
                    ddl_Sex.SelectedValue = Request.QueryString["Sex"].ToString();
                }
                CQTS.Salvorod = txt_SalVorod.Text;
                DateTime dtMiladi = new DateTime();
                dtMiladi = DateTime.Now;
                PersianCalendar p = new PersianCalendar();
                txt_TarikhSodor.Text = p.GetYear(dtMiladi).ToString() + "/" + p.GetMonth(dtMiladi).ToString() + "/" + p.GetDayOfMonth(dtMiladi).ToString();

                if (Request.QueryString["Semat"] != null)
                {
                    txt_Semat.Text = Request.QueryString["Semat"].ToString();
                }
                if (Request.QueryString["FamilySemat"] != null)
                {
                    txt_FamilySemat.Text = Request.QueryString["FamilySemat"].ToString();
                }
                if (Request.QueryString["TarikhSodor"] != null)
                {
                    txt_TarikhSodor.Text = Request.QueryString["TarikhSodor"].ToString();
                }
            }
            if (Session["stcode"] != null)
            {
              txt_stCode.Text = Session["stcode"].ToString();
            }          
        }

        protected void ddl_Daneshkade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Daneshkade.SelectedValue == "0")
            {
                dtField = CB.SelectAllField();
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dtField;
                ddl_Field.DataBind();
                ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
            }
            else
            {
                DataTable dtField = new DataTable();
                dtField = ERB.GetReshByDaneshkade(int.Parse(ddl_Daneshkade.SelectedValue.ToString()));
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dtField;
                ddl_Field.DataBind();
                ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Field.Items[ddl_Field.Items.Count - 1].Selected = true;
            }
        }

        protected void ddl_Field_SelectedIndexChanged(object sender, EventArgs e)
        {
            CQTS.Field = int.Parse(ddl_Field.SelectedValue);
        }

        protected void ddl_Degree_SelectedIndexChanged(object sender, EventArgs e)
        {
            CQTS.Degree = int.Parse(ddl_Degree.SelectedValue);
        }

        protected void ddl_Dorpar_SelectedIndexChanged(object sender, EventArgs e)
        {
            CQTS.Dorpar = int.Parse(ddl_Dorpar.SelectedValue);
        }

        protected void ddl_Sex_SelectedIndexChanged(object sender, EventArgs e)
        {
            CQTS.Sex = int.Parse(ddl_Sex.SelectedValue);
        }

        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {
            CQTS.Term = ddl_Term.SelectedValue;
        }

        protected void btn_ShowKart_Click(object sender, EventArgs e)
        {

            if (ddl_Term.SelectedValue == null || ddl_Term.SelectedValue == "0")
            {
                rwd.RadAlert("لطفا ترم را انتخاب نمایید", 0, 100, "پیام", "");
            }
            else
            {
                if (txt_stCode.Text==null || txt_stCode.Text==string.Empty)
                {
                    txt_stCode.Text = "0";
                }
                if (ddl_Daneshkade.SelectedValue == null)
                {
                    ddl_Daneshkade.SelectedValue = "0";
                }
                if (ddl_Degree.SelectedValue == null)
                {
                    ddl_Degree.SelectedValue = "0";
                }
                if (ddl_Dorpar.SelectedValue == null)
                {
                    ddl_Dorpar.SelectedValue = "0";
                }
                if (ddl_Field.SelectedValue == null)
                {
                    ddl_Field.SelectedValue = "0";
                }
                if (ddl_Sex.SelectedValue == null)
                {
                    ddl_Sex.SelectedValue = "0";
                }
                if (txt_TarikhSodor.Text == null)
                {
                    txt_TarikhSodor.Text = "0";
                }
                if (txt_SalVorod.Text == null)
                {
                    CQTS.Salvorod = "0";
                }
                if (chk_ControlCheck.Checked == true)
                {
                     lbl_CtrlCheck.Text = "1";
                }
                else
                {
                    lbl_CtrlCheck.Text = "0";
                }
                if (txt_Semat.Text == null || txt_Semat.Text == string.Empty)
                {
                    txt_Semat.Text = "0";
                }
                if (txt_FamilySemat.Text == null || txt_FamilySemat.Text == string.Empty)
                {
                    txt_FamilySemat.Text = "0";
                }
                int mojaz =1;
                int typeaction = 1;
                DataTable dt = CQTSB.CardQuizStudents(int.Parse(ddl_Daneshkade.SelectedValue) , int.Parse(ddl_Field.SelectedValue) , int.Parse(ddl_Degree.SelectedValue)  , int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Sex.SelectedValue) , txt_SalVorod.Text ,int.Parse(txt_BedehiAz.Text) ,int.Parse(txt_BedehiTa.Text), txt_TarikhSodor.Text , txt_Etebar.Text , txt_Semat.Text , txt_FamilySemat.Text ,mojaz , ddl_Term.SelectedValue , int.Parse(lbl_CtrlCheck.Text), typeaction, CQTS.stcode);
                if (dt.Rows.Count == 0)
                {
                    rwd.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام" , "");
                }
                else
                {
                    //Report...................
                }
            }
        }

        protected void btn_ShowKartMovaghat_Click(object sender, EventArgs e)
        {
            img_ExportToExcel.Visible = false;
            img_ExportToExcel1.Visible = false;
            img_ExportToExcel2.Visible = false;
            if (ddl_Term.SelectedValue == null || ddl_Term.SelectedValue == "0")
            {
                rwd.RadAlert("لطفا ترم را انتخاب نمایید", 0, 100, "پیام", "");
            }
            else
            {
                if (txt_stCode.Text == null || txt_stCode.Text == string.Empty)
                {
                    txt_stCode.Text = "0";
                }
                if (ddl_Daneshkade.SelectedValue == null)
                {
                    ddl_Daneshkade.SelectedValue = "0";
                }
                if (ddl_Degree.SelectedValue == null)
                {
                    ddl_Degree.SelectedValue = "0";
                }
                if (ddl_Dorpar.SelectedValue == null)
                {
                    ddl_Dorpar.SelectedValue = "0";
                }
                if (ddl_Field.SelectedValue == null)
                {
                    ddl_Field.SelectedValue = "0";
                }
                if (ddl_Sex.SelectedValue == null)
                {
                    ddl_Sex.SelectedValue = "0";
                }
                if (txt_TarikhSodor.Text == null || txt_TarikhSodor.Text == string.Empty)
                {
                    txt_TarikhSodor.Text = "0";
                }
                if (txt_SalVorod.Text == null || txt_SalVorod.Text == string.Empty)
                {
                    txt_SalVorod.Text = "0";
                }
                if (lbl_CtrlCheck.Text != string.Empty)
                {
                    lbl_CtrlCheck.Text = string.Empty;
                }
                if (chk_ControlCheck.Checked == true)
                {
                    lbl_CtrlCheck.Text = "1";
                }
                else
                {
                    lbl_CtrlCheck.Text = "0";
                }
                if (txt_Semat.Text == null || txt_Semat.Text == string.Empty)
                {
                    txt_Semat.Text = "0";
                }
                if (txt_FamilySemat.Text == null || txt_FamilySemat.Text == string.Empty)
                {
                    txt_FamilySemat.Text = "0";
                }
                if (txt_Etebar.Text == null || txt_Etebar.Text == string.Empty)
                {
                    txt_Etebar.Text = "0";
                }
                int mojaz = 1;
                int typeaction;
                if (chk_SearchStu.Checked == true)
                {
                    typeaction = 4;
                }
                else
                {
                    typeaction = 1;
                }
                if (typeaction == 4 && txt_stCode.Text == "0")
                {
                    rwd.RadAlert("لطفا شماره دانشجویی را وارد نمایید", 0, 100, "پیام", "");
                }
                else
                {
                    DataTable dt = CQTSB.CardQuizStudents(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Sex.SelectedValue), txt_SalVorod.Text, int.Parse(txt_BedehiAz.Text), int.Parse(txt_BedehiTa.Text), txt_TarikhSodor.Text, txt_Etebar.Text, txt_Semat.Text, txt_FamilySemat.Text, mojaz, ddl_Term.SelectedValue, int.Parse(lbl_CtrlCheck.Text), typeaction, txt_stCode.Text);
                    if (dt.Rows.Count == 0)
                    {
                        rwd.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                    }
                    else
                    {
                        //Report...................
                        img_ExportToExcel.Visible = true;
                        this.StiWebViewer1.ResetReport();
                        StiWebViewer1.Visible = true;
                        StiReport rpt = new StiReport();
                        rpt.Load(Server.MapPath("../Report/ReportTemporaryCard.mrt"));
                        rpt.ReportCacheMode = StiReportCacheMode.On;
                        rpt.Dictionary.Databases.Clear();
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                        rpt.Compile();
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@Danesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@idresh"].ParameterValue = int.Parse(ddl_Field.SelectedValue);
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@kardan"].ParameterValue = int.Parse(ddl_Degree.SelectedValue);
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@dorpar"].ParameterValue = int.Parse(ddl_Dorpar.SelectedValue);
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue);
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@sal_v"].ParameterValue = txt_SalVorod.Text;
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@bed"].ParameterValue = int.Parse(txt_BedehiAz.Text);
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@bed2"].ParameterValue = int.Parse(txt_BedehiTa.Text);
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@TarikhSodor"].ParameterValue = txt_TarikhSodor.Text;
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@ModatEtabar"].ParameterValue = txt_Etebar.Text;
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@Semat"].ParameterValue = txt_Semat.Text;
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@FamilySemat"].ParameterValue = txt_FamilySemat.Text;
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@mojaz"].ParameterValue = mojaz;
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@term"].ParameterValue = ddl_Term.SelectedValue;
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@ctrl_check"].ParameterValue = int.Parse(lbl_CtrlCheck.Text);
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@typeaction"].ParameterValue = typeaction;
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@stu1"].ParameterValue = txt_stCode.Text;
                        ((StiSqlSource)rpt.Dictionary.DataSources["[Students].[SP_CardQuiz]"]).CommandTimeout = 60000;
                        rpt.RegData(dt);
                        StiWebViewer1.Report = rpt;
                        StiWebViewer1.Visible = true;
                    }
                    if (Session["stcode"] != null)
                    {
                        Session["stcode"] = null;
                    }
                }
            }
        }

        protected void btn_ShowKartMovaghat2_Click(object sender, EventArgs e)
        {
            img_ExportToExcel.Visible = false;
            img_ExportToExcel1.Visible = false;
            img_ExportToExcel2.Visible = false;
            if (ddl_Term.SelectedValue == null || ddl_Term.SelectedValue == "0")
            {
                rwd.RadAlert("لطفا ترم را انتخاب نمایید", 0, 100, "پیام", "");
            }
            if (ddl_Daneshkade.SelectedValue == null || ddl_Daneshkade.SelectedValue == "0")
            {
                rwd.RadAlert("لطفا دانشکده را انتخاب کنید", 0, 100, "پیام", "");
            }
            else
            {
                if (txt_stCode.Text == null || txt_stCode.Text == string.Empty)
                {
                    txt_stCode.Text = "0";
                }
                if (ddl_Daneshkade.SelectedValue == null)
                {
                    ddl_Daneshkade.SelectedValue = "0";
                }
                if (ddl_Degree.SelectedValue == null)
                {
                    ddl_Degree.SelectedValue = "0";
                }
                if (ddl_Dorpar.SelectedValue == null)
                {
                    ddl_Dorpar.SelectedValue = "0";
                }
                if (ddl_Field.SelectedValue == null)
                {
                    ddl_Field.SelectedValue = "0";
                }
                if (ddl_Sex.SelectedValue == null)
                {
                    ddl_Sex.SelectedValue = "0";
                }
                if (txt_TarikhSodor.Text == null || txt_TarikhSodor.Text == string.Empty)
                {
                    txt_TarikhSodor.Text = "0";
                }
                if (txt_SalVorod.Text == null || txt_SalVorod.Text == string.Empty)
                {
                    txt_SalVorod.Text = "0";
                }
                if (chk_ControlCheck.Checked == true)
                {
                    lbl_CtrlCheck.Text = "1";
                }
                else
                {
                    lbl_CtrlCheck.Text = "0";
                }
                if (txt_Semat.Text == null || txt_Semat.Text == string.Empty)
                {
                    txt_Semat.Text = "0";
                }
                if (txt_FamilySemat.Text == null || txt_FamilySemat.Text == string.Empty)
                {
                    txt_FamilySemat.Text = "0";
                }
                int mojaz = 1;
                int typeaction;
                if (chk_SearchStu.Checked == true)
                {
                    typeaction = 4;
                }
                else
                {
                    typeaction = 1;
                }
                if (typeaction == 4 && txt_stCode.Text == "0")
                {
                    rwd.RadAlert("لطفا شماره دانشجویی را وارد نمایید", 0, 100, "پیام", "");
                }
                else
                {
                    DataTable dt = CQTSB.CardQuizStudents(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Sex.SelectedValue), txt_SalVorod.Text, int.Parse(txt_BedehiAz.Text), int.Parse(txt_BedehiTa.Text), txt_TarikhSodor.Text, txt_Etebar.Text, txt_Semat.Text, txt_FamilySemat.Text, mojaz, ddl_Term.SelectedValue, int.Parse(lbl_CtrlCheck.Text), typeaction, txt_stCode.Text);
                    if (dt.Rows.Count == 0)
                    {
                        rwd.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                    }
                    else
                    {
                        //Report...................
                        img_ExportToExcel1.Visible = true;
                        this.StiWebViewer1.ResetReport();
                        StiWebViewer1.Visible = true;
                        StiReport rpt = new StiReport();
                        rpt.Load(Server.MapPath("../Report/ReportTemporaryCard2.mrt"));
                        rpt.ReportCacheMode = StiReportCacheMode.On;
                        rpt.Dictionary.Databases.Clear();
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                        rpt.Compile();
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@Danesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@idresh"].ParameterValue = int.Parse(ddl_Field.SelectedValue);
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@kardan"].ParameterValue = int.Parse(ddl_Degree.SelectedValue);
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@dorpar"].ParameterValue = int.Parse(ddl_Dorpar.SelectedValue);
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue);
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@sal_v"].ParameterValue = txt_SalVorod.Text;
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@bed"].ParameterValue = int.Parse(txt_BedehiAz.Text);
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@bed2"].ParameterValue = int.Parse(txt_BedehiTa.Text);
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@TarikhSodor"].ParameterValue = txt_TarikhSodor.Text;
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@ModatEtabar"].ParameterValue = txt_Etebar.Text;
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@Semat"].ParameterValue = txt_Semat.Text;
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@FamilySemat"].ParameterValue = txt_FamilySemat.Text;
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@mojaz"].ParameterValue = mojaz;
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@term"].ParameterValue = ddl_Term.SelectedValue;
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@ctrl_check"].ParameterValue = int.Parse(lbl_CtrlCheck.Text);
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@typeaction"].ParameterValue = typeaction;
                        rpt.CompiledReport.DataSources["[Students].[SP_CardQuiz]"].Parameters["@stu1"].ParameterValue = txt_stCode.Text;
                        ((StiSqlSource)rpt.Dictionary.DataSources["[Students].[SP_CardQuiz]"]).CommandTimeout = 60000;
                        rpt.RegData(dt);
                        StiWebViewer1.Report = rpt;
                        StiWebViewer1.Visible = true;
                    }

                    if (Session["stcode"] != null)
                    {
                        Session["stcode"] = null;
                    }
                    if (txt_FamilySemat.Text == "0")
                    {
                        txt_FamilySemat.Text = string.Empty;
                    }
                    if (txt_stCode.Text == "0")
                    {
                        txt_stCode.Text = string.Empty;
                    }
                    if (txt_SalVorod.Text == "0")
                    {
                        txt_SalVorod.Text = string.Empty;
                    }
                }
            }
        }

        protected void RadAjaxManager1_AjaxRequest1(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "Rebind")
            {
                txt_stCode.Text = Session["stcode"].ToString();
            }
        }

        protected void btn_ShowAx_Click(object sender, EventArgs e)
        {
            img_ExportToExcel.Visible = false;
            img_ExportToExcel1.Visible = false;
            img_ExportToExcel2.Visible = false;
            if (ddl_Term.SelectedValue == null || ddl_Term.SelectedValue == "0")
            {
                rwd.RadAlert("لطفا ترم را انتخاب نمایید", 0, 100, "پیام", "");
            }
            if (ddl_Daneshkade.SelectedValue == null || ddl_Daneshkade.SelectedValue == "0")
            {
                rwd.RadAlert("لطفا دانشکده را انتخاب کنید", 0, 100, "پیام", "");
            }
            else
            {
                if (txt_stCode.Text == null || txt_stCode.Text == string.Empty)
                {
                    txt_stCode.Text = "0";
                }
                if (ddl_Daneshkade.SelectedValue == null)
                {
                    ddl_Daneshkade.SelectedValue = "0";
                }
                if (ddl_Degree.SelectedValue == null)
                {
                    ddl_Degree.SelectedValue = "0";
                }
                if (ddl_Dorpar.SelectedValue == null)
                {
                    ddl_Dorpar.SelectedValue = "0";
                }
                if (ddl_Field.SelectedValue == null)
                {
                    ddl_Field.SelectedValue = "0";
                }
                if (ddl_Sex.SelectedValue == null)
                {
                    ddl_Sex.SelectedValue = "0";
                }
                if (txt_SalVorod.Text == null || txt_SalVorod.Text==string.Empty)
                {
                    txt_SalVorod.Text = "0";
                }
                if (chk_ControlCheck.Checked == true)
                  {
                    lbl_CtrlCheck.Text="1";
                  }
                    else
                  {
                    lbl_CtrlCheck.Text = "0";
                  }
                int mojaz = 1;
                DataTable dt = CQTSB.GetListStudentsNotImage(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Sex.SelectedValue), txt_SalVorod.Text, int.Parse(txt_BedehiAz.Text), int.Parse(txt_BedehiTa.Text), mojaz, ddl_Term.SelectedValue,int.Parse(lbl_CtrlCheck.Text), txt_stCode.Text);
                if (dt.Rows.Count == 0)
                {
                    rwd.RadAlert("رکوردی وجود ندارد", 0, 100, "پیام", "");
                }
                else
                {
                    //Report ..................................
                    img_ExportToExcel2.Visible = true;
                    this.StiWebViewer1.ResetReport();
                    StiWebViewer1.Visible = true;
                    StiReport rpt = new StiReport();
                    rpt.Load(Server.MapPath("../Report/ReportNotImage.mrt"));
                    rpt.ReportCacheMode = StiReportCacheMode.On;
                    rpt.Dictionary.Databases.Clear();
                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.ReportConnection.ToString()));
                    rpt.Compile();
                    rpt.CompiledReport.DataSources["[Students].[SP_NotImage]"].Parameters["@iddanesh"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                    rpt.CompiledReport.DataSources["[Students].[SP_NotImage]"].Parameters["@idresh"].ParameterValue = int.Parse(ddl_Field.SelectedValue);
                    rpt.CompiledReport.DataSources["[Students].[SP_NotImage]"].Parameters["@kardan"].ParameterValue = int.Parse(ddl_Degree.SelectedValue);
                    rpt.CompiledReport.DataSources["[Students].[SP_NotImage]"].Parameters["@dorpar"].ParameterValue = int.Parse(ddl_Dorpar.SelectedValue);
                    rpt.CompiledReport.DataSources["[Students].[SP_NotImage]"].Parameters["@sex"].ParameterValue = int.Parse(ddl_Sex.SelectedValue);
                    rpt.CompiledReport.DataSources["[Students].[SP_NotImage]"].Parameters["@sal_v"].ParameterValue = txt_SalVorod.Text;
                    rpt.CompiledReport.DataSources["[Students].[SP_NotImage]"].Parameters["@bed"].ParameterValue = int.Parse(txt_BedehiAz.Text);
                    rpt.CompiledReport.DataSources["[Students].[SP_NotImage]"].Parameters["@bed2"].ParameterValue = int.Parse(txt_BedehiTa.Text);
                    rpt.CompiledReport.DataSources["[Students].[SP_NotImage]"].Parameters["@mojaz"].ParameterValue = mojaz;
                    rpt.CompiledReport.DataSources["[Students].[SP_NotImage]"].Parameters["@term"].ParameterValue = ddl_Term.SelectedValue;
                    rpt.CompiledReport.DataSources["[Students].[SP_NotImage]"].Parameters["@ctrl_check"].ParameterValue = int.Parse(lbl_CtrlCheck.Text);
                    rpt.CompiledReport.DataSources["[Students].[SP_NotImage]"].Parameters["@stu1"].ParameterValue = txt_stCode.Text;
                    rpt.RegData(dt);
                    StiWebViewer1.Report = rpt;
                    StiWebViewer1.Visible = true;
                }
                if (Session["stcode"] != null)
                {
                    Session["stcode"] = null;
                }
                if (txt_FamilySemat.Text == "0")
                {
                    txt_FamilySemat.Text = string.Empty;
                }
                if (txt_stCode.Text == "0")
                {
                    txt_stCode.Text = string.Empty;
                }
                if (txt_SalVorod.Text == "0")
                {
                    txt_SalVorod.Text = string.Empty;
                }
            }
         }

        protected void btn_stCode_Click(object sender, EventArgs e)
        {
            if (txt_stCode.Text == null || txt_stCode.Text == string.Empty)
            {
                txt_stCode.Text = "0";
            }
            if (txt_TarikhSodor.Text == null || txt_TarikhSodor.Text == string.Empty)
            {
                txt_TarikhSodor.Text = "0";
            }
            if (txt_SalVorod.Text == null || txt_SalVorod.Text == string.Empty)
            {
                txt_SalVorod.Text = "0";
            }
            if (lbl_CtrlCheck.Text != string.Empty)
            {
                lbl_CtrlCheck.Text = string.Empty;
            }
            if (chk_ControlCheck.Checked == true)
            {
                lbl_CtrlCheck.Text = "1";
            }
            else
            {
                lbl_CtrlCheck.Text = "0";
            }
            if (txt_Semat.Text == null || txt_Semat.Text == string.Empty)
            {
                txt_Semat.Text = "0";
            }
            if (txt_FamilySemat.Text == null || txt_FamilySemat.Text == string.Empty)
            {
                txt_FamilySemat.Text = "0";
            }
            Session["Page"] = 3;
            RadWindowManager windowManager = new RadWindowManager();
            RadWindow widnow1 = new RadWindow();
            // Set the window properties   
            widnow1.NavigateUrl = ("../../../university/Students/CMS/SearchOfStudent.aspx?" + "Degree" + "=" + ddl_Degree.SelectedValue + "&" + "Field" + "=" + ddl_Field.SelectedValue + "&" + "Sex" + "=" + ddl_Sex.SelectedValue + "&" + "Daneshkade" + "=" + ddl_Daneshkade.SelectedValue + "&" + "Education" + "=" + ddl_Dorpar.SelectedValue + "&" + "SalVorod" + "=" + txt_SalVorod.Text + "&" + "Semat" + "=" + txt_Semat.Text + "&" + "FamilySemat" + "=" + txt_FamilySemat.Text + "&" + "TarikhSodor" + "=" + txt_TarikhSodor.Text + "&" + "Term" + "="+ ddl_Term.SelectedValue);
            widnow1.ID = "RadWindow1";
            widnow1.VisibleOnPageLoad = true; // Set this property to True for showing window from code 
            widnow1.Height = 600;
            widnow1.Width = 1100;
            windowManager.Windows.Add(widnow1);
            windowManager.Height = 600;
            windowManager.Width = 1100;
            ContentPlaceHolder mpContentPlaceHolder;
            mpContentPlaceHolder = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
            mpContentPlaceHolder.Controls.Add(widnow1);
        }

        protected void img_ExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            if (txt_stCode.Text == null || txt_stCode.Text == string.Empty)
            {
                txt_stCode.Text = "0";
            }
            if (ddl_Daneshkade.SelectedValue == null)
            {
                ddl_Daneshkade.SelectedValue = "0";
            }
            if (ddl_Degree.SelectedValue == null)
            {
                ddl_Degree.SelectedValue = "0";
            }
            if (ddl_Dorpar.SelectedValue == null)
            {
                ddl_Dorpar.SelectedValue = "0";
            }
            if (ddl_Field.SelectedValue == null)
            {
                ddl_Field.SelectedValue = "0";
            }
            if (ddl_Sex.SelectedValue == null)
            {
                ddl_Sex.SelectedValue = "0";
            }
            if (txt_TarikhSodor.Text == null || txt_TarikhSodor.Text == string.Empty)
            {
                txt_TarikhSodor.Text = "0";
            }
            if (txt_SalVorod.Text == null || txt_SalVorod.Text == string.Empty)
            {
                txt_SalVorod.Text = "0";
            }
            if (lbl_CtrlCheck.Text != string.Empty)
            {
                lbl_CtrlCheck.Text = string.Empty;
            }
            if (chk_ControlCheck.Checked == true)
            {
                lbl_CtrlCheck.Text = "1";
            }
            else
            {
                lbl_CtrlCheck.Text = "0";
            }
            if (txt_Semat.Text == null || txt_Semat.Text == string.Empty)
            {
                txt_Semat.Text = "0";
            }
            if (txt_FamilySemat.Text == null || txt_FamilySemat.Text == string.Empty)
            {
                txt_FamilySemat.Text = "0";
            }
            int mojaz = 1;
            int typeaction;
            if (chk_SearchStu.Checked == true)
            {
                typeaction = 4;
            }
            else
            {
                typeaction = 1;
            }
            DataTable dt = CQTSB.CardQuizStudents(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Sex.SelectedValue), txt_SalVorod.Text, int.Parse(txt_BedehiAz.Text), int.Parse(txt_BedehiTa.Text), txt_TarikhSodor.Text, txt_Etebar.Text, txt_Semat.Text, txt_FamilySemat.Text, mojaz, ddl_Term.SelectedValue, int.Parse(lbl_CtrlCheck.Text), typeaction, txt_stCode.Text);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                grd_Show1.DataSource = dt;
                grd_Show1.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=CardQuizTemporary.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    ////gv_Show.AllowPaging = false;
                    ////this.BindGrid();

                    //gv_Show.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in grd_Show1.HeaderRow.Cells)
                    {
                        cell.BackColor = grd_Show1.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in grd_Show1.Rows)
                    {
                        //row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = grd_Show1.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = grd_Show1.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                }

                    grd_Show1.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
          }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
        protected void img_ExportToExcel1_Click(object sender, ImageClickEventArgs e)
        {
            if (txt_stCode.Text == null || txt_stCode.Text == string.Empty)
            {
                txt_stCode.Text = "0";
            }
            if (ddl_Daneshkade.SelectedValue == null)
            {
                ddl_Daneshkade.SelectedValue = "0";
            }
            if (ddl_Degree.SelectedValue == null)
            {
                ddl_Degree.SelectedValue = "0";
            }
            if (ddl_Dorpar.SelectedValue == null)
            {
                ddl_Dorpar.SelectedValue = "0";
            }
            if (ddl_Field.SelectedValue == null)
            {
                ddl_Field.SelectedValue = "0";
            }
            if (ddl_Sex.SelectedValue == null)
            {
                ddl_Sex.SelectedValue = "0";
            }
            if (txt_TarikhSodor.Text == null || txt_TarikhSodor.Text == string.Empty)
            {
                txt_TarikhSodor.Text = "0";
            }
            if (txt_SalVorod.Text == null || txt_SalVorod.Text == string.Empty)
            {
                txt_SalVorod.Text = "0";
            }
            if (chk_ControlCheck.Checked == true)
            {
                lbl_CtrlCheck.Text = "1";
            }
            else
            {
                lbl_CtrlCheck.Text = "0";
            }
            if (txt_Semat.Text == null || txt_Semat.Text == string.Empty)
            {
                txt_Semat.Text = "0";
            }
            if (txt_FamilySemat.Text == null || txt_FamilySemat.Text == string.Empty)
            {
                txt_FamilySemat.Text = "0";
            }
            int mojaz = 1;
            int typeaction;
            if (chk_SearchStu.Checked == true)
            {
                typeaction = 4;
            }
            else
            {
                typeaction = 1;
            }
            DataTable dt = CQTSB.CardQuizStudents(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Sex.SelectedValue), txt_SalVorod.Text, int.Parse(txt_BedehiAz.Text), int.Parse(txt_BedehiTa.Text), txt_TarikhSodor.Text, txt_Etebar.Text, txt_Semat.Text, txt_FamilySemat.Text, mojaz, ddl_Term.SelectedValue, int.Parse(lbl_CtrlCheck.Text), typeaction, txt_stCode.Text);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                grd_Show2.DataSource = dt;
                grd_Show2.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=CardQuizTemporary.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    ////gv_Show.AllowPaging = false;
                    ////this.BindGrid();

                    //gv_Show.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in grd_Show2.HeaderRow.Cells)
                    {
                        cell.BackColor = grd_Show2.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in grd_Show1.Rows)
                    {
                        //row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = grd_Show2.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = grd_Show2.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    grd_Show2.RenderControl(hw);

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
            if (txt_stCode.Text == null || txt_stCode.Text == string.Empty)
            {
                txt_stCode.Text = "0";
            }
            if (ddl_Daneshkade.SelectedValue == null)
            {
                ddl_Daneshkade.SelectedValue = "0";
            }
            if (ddl_Degree.SelectedValue == null)
            {
                ddl_Degree.SelectedValue = "0";
            }
            if (ddl_Dorpar.SelectedValue == null)
            {
                ddl_Dorpar.SelectedValue = "0";
            }
            if (ddl_Field.SelectedValue == null)
            {
                ddl_Field.SelectedValue = "0";
            }
            if (ddl_Sex.SelectedValue == null)
            {
                ddl_Sex.SelectedValue = "0";
            }
            if (txt_SalVorod.Text == null || txt_SalVorod.Text == string.Empty)
            {
                txt_SalVorod.Text = "0";
            }
            if (chk_ControlCheck.Checked == true)
            {
                lbl_CtrlCheck.Text = "1";
            }
            else
            {
                lbl_CtrlCheck.Text = "0";
            }
            int mojaz = 1;
            DataTable dt = CQTSB.GetListStudentsNotImage(int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Field.SelectedValue), int.Parse(ddl_Degree.SelectedValue), int.Parse(ddl_Dorpar.SelectedValue), int.Parse(ddl_Sex.SelectedValue), txt_SalVorod.Text, int.Parse(txt_BedehiAz.Text), int.Parse(txt_BedehiTa.Text), mojaz, ddl_Term.SelectedValue, int.Parse(lbl_CtrlCheck.Text), txt_stCode.Text);
            if (dt.Rows.Count == 0)
            {

            }
            else
            {
                grd_Show3.DataSource = dt;
                grd_Show3.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=NotImage.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    ////gv_Show.AllowPaging = false;
                    ////this.BindGrid();

                    //gv_Show.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in grd_Show3.HeaderRow.Cells)
                    {
                        cell.BackColor = grd_Show3.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in grd_Show3.Rows)
                    {
                        //row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = grd_Show3.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = grd_Show3.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    grd_Show3.RenderControl(hw);

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
