using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Education;
using IAUEC_Apps.DTO.University.Education;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System.IO;
using IAUEC_Apps.Business.Common;


namespace IAUEC_Apps.UI.University.Education.CMS
{
    public partial class ListClass : System.Web.UI.Page
    {
       // CommonDAO dao = new CommonDAO();
        CommonBusiness cb = new CommonBusiness();
        public static ListClassDTO LCD = new ListClassDTO();
        EducationReportBusiness ERB = new EducationReportBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            StiWebViewer1.Visible = false;
            DataTable dtTerm = new DataTable();
            DataTable dtDaneshkade = new DataTable();
            DataTable dtDegree = new DataTable();
            DataTable dtField = new DataTable();
            DataTable dtLocationClass = new DataTable();
            //ramezanian
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
                dtTerm = cb.SelectAllTerm();
                ddl_Term.DataTextField = "tterm";
                ddl_Term.DataSource = dtTerm;
                ddl_Term.DataBind();
                ddl_Term.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Term.Items[ddl_Term.Items.Count - 1].Selected = true;
                ddl_Day.Items.Add(new ListItem("شنبه", "1"));
                ddl_Day.Items.Add(new ListItem("یکشنبه", "2"));
                ddl_Day.Items.Add(new ListItem("دوشنبه", "3"));
                ddl_Day.Items.Add(new ListItem("سه شنبه", "4"));
                ddl_Day.Items.Add(new ListItem("چهارشنبه", "5"));
                ddl_Day.Items.Add(new ListItem("پنج شنبه", "6"));
                ddl_Day.Items.Add(new ListItem("جمعه", "7"));
                ddl_Day.Items.Add(new ListItem("با گروه", "8"));
                ddl_Day.Items.Add(new ListItem("گروه بندی", "9"));
                //ddl_Day.Items.Insert(0, "انتخاب کنید");
                ddl_Day.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Day.Items[ddl_Day.Items.Count - 1].Selected = true;

                dtDaneshkade = cb.SelectAllDaneshkade();
                ddl_Daneshkade.DataTextField = "namedanesh";
                ddl_Daneshkade.DataValueField = "id";
                ddl_Daneshkade.DataSource = dtDaneshkade;
                ddl_Daneshkade.DataBind();
                //ddl_Daneshkade.Items.Insert(0, "انتخاب کنید");
                ddl_Daneshkade.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Daneshkade.Items[ddl_Daneshkade.Items.Count - 1].Selected = true;
                ddl_Degree.Items.Add(new ListItem("کاردانی", "2"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی", "1"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ناپیوسته", "3"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ارشد پیوسته", "4"));
                ddl_Degree.Items.Add(new ListItem("کارشناسی ارشد ناپیوسته", "5"));
                ddl_Degree.Items.Add(new ListItem("دکتری تخصصی", "7"));
                ddl_Degree.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Degree.Items[ddl_Degree.Items.Count - 1].Selected = true;

                //dtDegree = ERB.SelectAllDegree();
                //ddl_Degree.DataTextField = "name";
                //ddl_Degree.DataValueField = "id_sida";
                //ddl_Degree.DataSource = dtDegree;
                //ddl_Degree.DataBind();
                //ddl_Degree.Items.Add(new ListItem("انتخاب کنید", "0"));
                //ddl_Degree.Items[ddl_Degree.Items.Count - 1].Selected = true;

                dtField = cb.SelectAllField();
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dtField;
                ddl_Field.DataBind();
                ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Field.Items[ddl_Field.Items.Count - 1].Selected = true;
                //ddl_Field.Items.Insert(0, "انتخاب کنید");
            }
        }
        //ramezanian
        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {
            LCD.Term = ddl_Term.SelectedValue;
        }
        //ramezanian
        protected void ddl_Day_SelectedIndexChanged(object sender, EventArgs e)
        {
            LCD.Day = ddl_Day.SelectedValue;
        }
        //ramezanian
        protected void ddl_Daneshkade_SelectedIndexChanged(object sender, EventArgs e)
        {
            LCD.Daneshkade = ddl_Daneshkade.SelectedValue;
            DataTable dt = new DataTable();
            if (ddl_Daneshkade.SelectedValue == "0")
            {
                dt = ERB.SelectAllField();
            }
            else if (ddl_Daneshkade.SelectedValue != null)
            {
                dt = cb.GetReshByDaneshkade(int.Parse(LCD.Daneshkade));
            }
            ddl_Field.DataSource = dt;
            ddl_Field.DataTextField = "nameresh";
            ddl_Field.DataValueField = "id";
            ddl_Field.DataBind();
            ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
            ddl_Field.Items[ddl_Field.Items.Count - 1].Selected = true;
            LCD.Field = ddl_Field.SelectedValue;
        }
        //ramezanian
        protected void ddl_Degree_SelectedIndexChanged(object sender, EventArgs e)
        {
            LCD.Degree = ddl_Degree.SelectedValue;
        }
        //ramezanian
        protected void ddl_Field_SelectedIndexChanged(object sender, EventArgs e)
        {
            LCD.Field = ddl_Field.SelectedValue;
        }
        //ramezanian
        protected void btn_ReportListClass_Click(object sender, EventArgs e)
        {
            DataTable dtResault = new DataTable();
            if (ddl_Term.SelectedValue == null || ddl_Term.SelectedValue == "0")
            {
                RadWindowManager1.RadAlert("لطفا ترم را انتخاب بفرمایید", 0, 100, "پیغام", "");
            }
            else
            {
                if (rdb_ListClassDay.Checked == true)
                {
                    img_ExportToExcel1.Visible = false;
                    img_ExportToExcel2.Visible = false;
                    img_ExportToExcel3.Visible = false;
                    img_ExportToExcel4.Visible = false;
                    img_ExportToExcel5.Visible = false;
                    string timeStart;
                    string timeEnd;

                    if (RTP1.SelectedDate == null)
                    {
                        timeStart = "  :  ";
                    }
                    else
                    {
                        DateTime dstart = RTP1.SelectedDate.Value;
                        timeStart = dstart.ToString("HH:mm");
                    }
                    if (RTP2.SelectedDate == null)
                    {
                        timeEnd = "24:00";
                    }
                    else
                    {
                        DateTime d = RTP2.SelectedDate.Value;
                        timeEnd = d.ToString("HH:mm");
                    }
                    if (ddl_Day.SelectedValue == null)
                    {
                        ddl_Day.SelectedValue = "0";
                    }
                    if (ddl_Daneshkade.SelectedValue == null)
                    {
                        ddl_Daneshkade.SelectedValue = "0";
                    }
                    if (ddl_Field.SelectedValue == null)
                    {
                        ddl_Field.SelectedValue = "0";
                    }
                    if (ddl_Degree.SelectedValue == null)
                    {
                        ddl_Degree.SelectedValue = "0";
                    }

                    dtResault = ERB.GetListClassBarAsasRuz(ddl_Term.SelectedValue.ToString(), Convert.ToInt32(ddl_Day.SelectedValue.ToString()), timeStart, timeEnd, Convert.ToInt32(ddl_Daneshkade.SelectedValue.ToString()), Convert.ToInt32(ddl_Field.SelectedValue.ToString()), int.Parse(ddl_Degree.SelectedValue));

                    if (dtResault.Rows.Count == 0)
                    {
                        RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیغام", "");
                    }
                    else
                    {
                        img_ExportToExcel1.Visible = true;
                        this.StiWebViewer1.ResetReport();
                        StiReport rpt = new StiReport();
                        rpt.Load(Server.MapPath("../Report/ReportClassListByDay.mrt"));
                        rpt.Dictionary.Databases.Clear();
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", cb.ReportConnection.ToString()));
                        rpt.Compile();
                        rpt.CompiledReport.DataSources["[Education].[SP_ListClassByDay]"].Parameters["@Day"].ParameterValue = int.Parse(ddl_Day.SelectedValue.ToString());
                        rpt.CompiledReport.DataSources["[Education].[SP_ListClassByDay]"].Parameters["@Saatstart"].ParameterValue = timeStart;
                        rpt.CompiledReport.DataSources["[Education].[SP_ListClassByDay]"].Parameters["@Saatend"].ParameterValue = timeEnd;
                        rpt.CompiledReport.DataSources["[Education].[SP_ListClassByDay]"].Parameters["@Daneshkade"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue.ToString());
                        rpt.CompiledReport.DataSources["[Education].[SP_ListClassByDay]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue.ToString());
                        rpt.CompiledReport.DataSources["[Education].[SP_ListClassByDay]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue.ToString());
                        rpt.CompiledReport.DataSources["[Education].[SP_ListClassByDay]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                        rpt.RegData(dtResault);
                        rpt.Dictionary.Synchronize();
                        //rpt.Show();
                        StiWebViewer1.Report = rpt;
                        StiWebViewer1.Visible = true;
                        //rpt.Print(true);
                    }

                }

                else if (rdb_ListClassDelete.Checked == true)
                {
                    img_ExportToExcel1.Visible = false;
                    img_ExportToExcel2.Visible = false;
                    img_ExportToExcel3.Visible = false;
                    img_ExportToExcel4.Visible = false;
                    img_ExportToExcel5.Visible = false;
                    string timeStart;
                    string timeEnd;

                    if (RTP1.SelectedDate == null)
                    {
                        timeStart = "  :  ";
                    }
                    else
                    {
                        DateTime dstart = RTP1.SelectedDate.Value;
                        timeStart = dstart.ToString("HH:mm");
                    }
                    if (RTP2.SelectedDate == null)
                    {
                        timeEnd = "24:00";
                    }
                    else
                    {
                        DateTime d = RTP2.SelectedDate.Value;
                        timeEnd = d.ToString("HH:mm");
                    }
                    if (ddl_Field.SelectedValue == null || ddl_Field.SelectedValue == "")
                    {
                        ddl_Field.SelectedValue = "0";
                    }
                    if (ddl_Daneshkade.SelectedValue == null || ddl_Daneshkade.SelectedValue == "")
                    {
                        ddl_Daneshkade.SelectedValue = "0";
                    }
                    if (ddl_Degree.SelectedValue == null)
                    {
                        ddl_Degree.SelectedValue = "0";
                    }
                    if (ddl_Day.SelectedValue == null)
                    {
                        ddl_Day.SelectedValue = "0";
                    }

                    dtResault = ERB.GetListClassHazfi(ddl_Term.SelectedValue.ToString(), Convert.ToInt32(ddl_Day.SelectedValue.ToString()), timeStart, timeEnd, Convert.ToInt32(ddl_Daneshkade.SelectedValue.ToString()), Convert.ToInt32(ddl_Field.SelectedValue.ToString()), int.Parse(ddl_Degree.SelectedValue));

                    if (dtResault.Rows.Count == 0)
                    {
                        RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیغام", "");
                    }
                    else
                    {
                        img_ExportToExcel2.Visible = true;
                        this.StiWebViewer1.ResetReport();
                        StiWebViewer1.Visible = true;
                        StiReport rpt = new StiReport();
                        rpt.Load(Server.MapPath("../Report/ReportListClassRemoval.mrt"));
                        rpt.Dictionary.Databases.Clear();
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", cb.ReportConnection.ToString()));
                        rpt.Compile();
                        rpt.CompiledReport.DataSources["[Education].[SP_ListClassRemoval]"].Parameters["@Day"].ParameterValue = int.Parse(ddl_Day.SelectedValue.ToString());
                        rpt.CompiledReport.DataSources["[Education].[SP_ListClassRemoval]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                        rpt.CompiledReport.DataSources["[Education].[SP_ListClassRemoval]"].Parameters["@Daneshkade"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue.ToString());
                        rpt.CompiledReport.DataSources["[Education].[SP_ListClassRemoval]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue.ToString());
                        rpt.CompiledReport.DataSources["[Education].[SP_ListClassRemoval]"].Parameters["@Saatstart"].ParameterValue = timeStart;
                        rpt.CompiledReport.DataSources["[Education].[SP_ListClassRemoval]"].Parameters["@Saatend"].ParameterValue = timeEnd;
                        rpt.CompiledReport.DataSources["[Education].[SP_ListClassRemoval]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue.ToString());
                        //rpt.CompiledReport.DataSources["[Education].[SP_ReportListKlasBarAsasRuz]"].Parameters["@Degree"].ParameterValue = Convert.ToInt32(LCD.Degree.ToString());
                        rpt.RegData(dtResault);
                        rpt.Dictionary.Synchronize();
                        //rpt.Show();
                        StiWebViewer1.Report = rpt;
                        StiWebViewer1.Visible = true;
                        //rpt.Print(true);
                    }

                    
                }
                else if (rdb_ListClassTarikh.Checked == true)
                {
                    img_ExportToExcel1.Visible = false;
                    img_ExportToExcel2.Visible = false;
                    img_ExportToExcel3.Visible = false;
                    img_ExportToExcel4.Visible = false;
                    img_ExportToExcel5.Visible = false;
                    string timeStart;
                    string timeEnd;

                    if (RTP1.SelectedDate == null)
                    {
                        timeStart = "  :  ";
                    }
                    else
                    {
                        DateTime dstart = RTP1.SelectedDate.Value;
                        timeStart = dstart.ToString("HH:mm");
                    }
                    if (RTP2.SelectedDate == null)
                    {
                        timeEnd = "24:00";
                    }
                    else
                    {
                        DateTime d = RTP2.SelectedDate.Value;
                        timeEnd = d.ToString("HH:mm");
                    }

                    string FromDate = txt_AzTarikhTahvilNomre.Text;
                    string ToDate = txt_TaTarikhTahvilNomre.Text;

                    if (ddl_Daneshkade.SelectedValue == null || ddl_Daneshkade.SelectedValue == "")
                    {
                        ddl_Daneshkade.SelectedValue = "0";
                    }
                    if (ddl_Field.SelectedValue == null || ddl_Field.SelectedValue == "")
                    {
                        ddl_Field.SelectedValue = "0";
                    }
                    if (ddl_Degree.SelectedValue == null)
                    {
                        ddl_Degree.SelectedValue = "0";
                    }
                    if (ddl_Day.SelectedValue == null)
                    {
                        ddl_Day.SelectedValue = "0";
                    }
                    if ((txt_AzTarikhTahvilNomre.Text == "") && (txt_TaTarikhTahvilNomre.Text == ""))
                    {
                        dtResault = ERB.GetListklasTarikhNomre(ddl_Term.SelectedValue.ToString(), Convert.ToInt32(ddl_Daneshkade.SelectedValue.ToString()), "13  /  /  ", "13  /  /  ", Convert.ToInt32(ddl_Field.SelectedValue.ToString()), Convert.ToInt32(ddl_Day.SelectedValue.ToString()), timeStart, timeEnd, int.Parse(ddl_Degree.SelectedValue));
                    }
                    if ((txt_AzTarikhTahvilNomre.Text == ""))
                    {
                        dtResault = ERB.GetListklasTarikhNomre(ddl_Term.SelectedValue.ToString(), Convert.ToInt32(ddl_Daneshkade.SelectedValue.ToString()), "13  /  /  ", txt_TaTarikhTahvilNomre.Text, Convert.ToInt32(ddl_Field.SelectedValue.ToString()), Convert.ToInt32(ddl_Day.SelectedValue.ToString()), timeStart, timeEnd, int.Parse(ddl_Degree.SelectedValue));
                    }
                    if ((txt_TaTarikhTahvilNomre.Text == ""))
                    {
                        dtResault = ERB.GetListklasTarikhNomre(ddl_Term.SelectedValue.ToString(), Convert.ToInt32(ddl_Daneshkade.SelectedValue.ToString()),txt_AzTarikhTahvilNomre.Text,  "13  /  /  ", Convert.ToInt32(ddl_Field.SelectedValue.ToString()), Convert.ToInt32(ddl_Day.SelectedValue.ToString()), timeStart, timeEnd, int.Parse(ddl_Degree.SelectedValue));
                    }
                    if (txt_AzTarikhTahvilNomre.Text != "" && txt_TaTarikhTahvilNomre.Text != "")
                    {
                        dtResault = ERB.GetListklasTarikhNomre(ddl_Term.SelectedValue.ToString(), Convert.ToInt32(ddl_Daneshkade.SelectedValue.ToString()), txt_AzTarikhTahvilNomre.Text, txt_TaTarikhTahvilNomre.Text, Convert.ToInt32(ddl_Field.SelectedValue.ToString()), Convert.ToInt32(ddl_Day.SelectedValue.ToString()), timeStart, timeEnd, int.Parse(ddl_Degree.SelectedValue));
                    }
                        if (dtResault.Rows.Count == 0)
                        {
                            RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیغام", "");
                        }
                        else
                        {
                            img_ExportToExcel3.Visible = true;
                            this.StiWebViewer1.ResetReport();
                            StiWebViewer1.Visible = true;
                            StiReport rpt = new StiReport();
                            rpt.Load(Server.MapPath("../Report/ReportDateDeliveryScore.mrt"));
                            rpt.Dictionary.Databases.Clear();
                            rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", cb.ReportConnection.ToString()));
                            rpt.Compile();
                            rpt.CompiledReport.DataSources["[Education].[SP_DateDeliveryScore]"].Parameters["@Day"].ParameterValue = int.Parse(ddl_Day.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Education].[SP_DateDeliveryScore]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                            rpt.CompiledReport.DataSources["[Education].[SP_DateDeliveryScore]"].Parameters["@Daneshkade"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Education].[SP_DateDeliveryScore]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue.ToString());
                            if (txt_TaTarikhTahvilNomre.Text == "" && txt_AzTarikhTahvilNomre.Text == "")
                            {
                                rpt.CompiledReport.DataSources["[Education].[SP_DateDeliveryScore]"].Parameters["@FromDate"].ParameterValue = "13  /  /  ";
                                rpt.CompiledReport.DataSources["[Education].[SP_DateDeliveryScore]"].Parameters["@ToDate"].ParameterValue = "13  /  /  ";
                            }
                            if (txt_TaTarikhTahvilNomre.Text == "")
                            {
                                rpt.CompiledReport.DataSources["[Education].[SP_DateDeliveryScore]"].Parameters["@ToDate"].ParameterValue = "13  /  /  ";
                                rpt.CompiledReport.DataSources["[Education].[SP_DateDeliveryScore]"].Parameters["@FromDate"].ParameterValue = txt_AzTarikhTahvilNomre.Text;
                            }
                            if (txt_AzTarikhTahvilNomre.Text == "")
                            {
                                rpt.CompiledReport.DataSources["[Education].[SP_DateDeliveryScore]"].Parameters["@FromDate"].ParameterValue = "13  /  /  ";
                                rpt.CompiledReport.DataSources["[Education].[SP_DateDeliveryScore]"].Parameters["@ToDate"].ParameterValue = txt_TaTarikhTahvilNomre.Text;
                            }
                            if (txt_AzTarikhTahvilNomre.Text!="" && txt_TaTarikhTahvilNomre.Text!="")
                            {
                                rpt.CompiledReport.DataSources["[Education].[SP_DateDeliveryScore]"].Parameters["@FromDate"].ParameterValue = txt_AzTarikhTahvilNomre.Text;
                                rpt.CompiledReport.DataSources["[Education].[SP_DateDeliveryScore]"].Parameters["@ToDate"].ParameterValue = txt_TaTarikhTahvilNomre.Text;
                            }
                            rpt.CompiledReport.DataSources["[Education].[SP_DateDeliveryScore]"].Parameters["@Saatstart"].ParameterValue = timeStart;
                            rpt.CompiledReport.DataSources["[Education].[SP_DateDeliveryScore]"].Parameters["@Saatend"].ParameterValue = timeEnd;
                            rpt.CompiledReport.DataSources["[Education].[SP_DateDeliveryScore]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue.ToString());
                            ((StiSqlSource)rpt.Dictionary.DataSources["[Education].[SP_DateDeliveryScore]"]).CommandTimeout = 60000;
                            rpt.RegData(dtResault);
                            rpt.Dictionary.Synchronize();
                            //rpt.Show();
                            StiWebViewer1.Report = rpt;
                            StiWebViewer1.Visible = true;
                            //rpt.Print(true);
                        }
                    }

                    //LCD.Daneshkade = LCD.Field =string.Empty;               
                else if (rdb_ListHadClass.Checked == true)
                {
                    img_ExportToExcel1.Visible = false;
                    img_ExportToExcel2.Visible = false;
                    img_ExportToExcel3.Visible = false;
                    img_ExportToExcel4.Visible = false;
                    img_ExportToExcel5.Visible = false;
                    string timeStart;
                    string timeEnd;

                    if (RTP1.SelectedDate == null)
                    {
                        timeStart = "  :  ";
                    }
                    else
                    {
                        DateTime dstart = RTP1.SelectedDate.Value;
                        timeStart = dstart.ToString("HH:mm");
                    }
                    if (RTP2.SelectedDate == null)
                    {
                        timeEnd = "24:00";
                    }
                    else
                    {
                        DateTime d = RTP2.SelectedDate.Value;
                        timeEnd = d.ToString("HH:mm");
                    }
                    if (ddl_Daneshkade.SelectedValue == null || ddl_Daneshkade.SelectedValue == "")
                    {
                        ddl_Daneshkade.SelectedValue = "0";
                    }
                    if (ddl_Field.SelectedValue == null || ddl_Field.SelectedValue == "")
                    {
                        ddl_Field.SelectedValue = "0";
                    }
                    if (ddl_Degree.SelectedValue == null)
                    {
                        ddl_Degree.SelectedValue = "0";
                    }
                    if (ddl_Day.SelectedValue == null)
                    {
                        ddl_Day.SelectedValue = "0";
                    }
                    if (txt_ZarfiyatKamtar.Text == string.Empty)
                    {
                        txt_ZarfiyatKamtar.Text = "0";
                    }
                    if (txt_ZarfiyatBishtar.Text == string.Empty)
                    {
                        txt_ZarfiyatBishtar.Text = "0";
                    }
                    if (txt_TypeDivision.Text == string.Empty)
                    {
                        txt_TypeDivision.Text = "0";
                    }
                    int MinCapacity = int.Parse(txt_ZarfiyatKamtar.Text);
                    int MaxCapacity = int.Parse(txt_ZarfiyatBishtar.Text);
                    int Vahed = int.Parse(txt_TypeDivision.Text);
                    dtResault = ERB.GetListHadeNesabKlas(ddl_Term.SelectedValue.ToString(), int.Parse(txt_ZarfiyatKamtar.Text), int.Parse(txt_ZarfiyatBishtar.Text), int.Parse(txt_TypeDivision.Text), Convert.ToInt32(ddl_Daneshkade.SelectedValue.ToString()), Convert.ToInt32(ddl_Field.SelectedValue.ToString()), timeStart,timeEnd, Convert.ToInt32(ddl_Day.SelectedValue.ToString()), int.Parse(ddl_Degree.SelectedValue));
                    if (dtResault.Rows.Count == 0)
                    {
                        RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیغام", "");
                    }
                    else
                    {
                        img_ExportToExcel4.Visible = true;
                        this.StiWebViewer1.ResetReport();
                        StiWebViewer1.Visible = true;
                        StiReport rpt = new StiReport();
                        rpt.Load(Server.MapPath("../Report/ReportListClassIsNotEnough.mrt"));
                        rpt.Dictionary.Databases.Clear();
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", cb.ReportConnection.ToString()));
                        rpt.Compile();
                        rpt.CompiledReport.DataSources["[Education].[SP_ListClassIsNotEnough]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                        rpt.CompiledReport.DataSources["[Education].[SP_ListClassIsNotEnough]"].Parameters["@MinCapacity"].ParameterValue = MinCapacity;
                        rpt.CompiledReport.DataSources["[Education].[SP_ListClassIsNotEnough]"].Parameters["@MaxCapacity"].ParameterValue = MaxCapacity;
                        rpt.CompiledReport.DataSources["[Education].[SP_ListClassIsNotEnough]"].Parameters["@Vahed"].ParameterValue = Vahed;
                        rpt.CompiledReport.DataSources["[Education].[SP_ListClassIsNotEnough]"].Parameters["@Daneshkade"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue.ToString());
                        rpt.CompiledReport.DataSources["[Education].[SP_ListClassIsNotEnough]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue.ToString());
                        rpt.CompiledReport.DataSources["[Education].[SP_ListClassIsNotEnough]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue.ToString());
                        rpt.CompiledReport.DataSources["[Education].[SP_ListClassIsNotEnough]"].Parameters["@Day"].ParameterValue = int.Parse(ddl_Day.SelectedValue.ToString());
                        rpt.CompiledReport.DataSources["[Education].[SP_ListClassIsNotEnough]"].Parameters["@Saatstart"].ParameterValue = timeStart;
                        rpt.CompiledReport.DataSources["[Education].[SP_ListClassIsNotEnough]"].Parameters["@Saatend"].ParameterValue = timeEnd;
                        //rpt.CompiledReport.DataSources["[Education].[SP_ReportListKlasBarAsasRuz]"].Parameters["@Degree"].ParameterValue = Convert.ToInt32(LCD.Degree.ToString());
                        rpt.RegData(dtResault);
                        rpt.Dictionary.Synchronize();
                        //rpt.Show();
                        StiWebViewer1.Report = rpt;
                        StiWebViewer1.Visible = true;
                        //rpt.Print(true);
                    }
                    if (txt_ZarfiyatKamtar.Text == "0")
                    {
                        txt_ZarfiyatKamtar.Text = string.Empty;
                    }
                    if (txt_ZarfiyatBishtar.Text == "0")
                    {
                        txt_ZarfiyatBishtar.Text = string.Empty;
                    }
                    if (txt_TypeDivision.Text == "0")
                    {
                        txt_TypeDivision.Text = string.Empty;
                    }
                    LCD.Daneshkade = LCD.Field = string.Empty;
                }
                else if (rdb_ListTadakhol.Checked == true)
                {
                    img_ExportToExcel1.Visible = false;
                    img_ExportToExcel2.Visible = false;
                    img_ExportToExcel3.Visible = false;
                    img_ExportToExcel4.Visible = false;
                    img_ExportToExcel5.Visible = false;
                    string timeStart;
                    string timeEnd;

                    if (RTP1.SelectedDate == null)
                    {
                        timeStart = "  :  ";
                    }
                    else
                    {
                        DateTime dstart = RTP1.SelectedDate.Value;
                        timeStart = dstart.ToString("HH:mm");
                    }
                    if (RTP2.SelectedDate == null)
                    {
                        timeEnd = "24:00";
                    }
                    else
                    {
                        DateTime d = RTP2.SelectedDate.Value;
                        timeEnd = d.ToString("HH:mm");
                    }
                    if (ddl_Daneshkade.SelectedValue == null || ddl_Daneshkade.SelectedValue == "")
                    {
                        ddl_Daneshkade.SelectedValue = "0";
                    }
                    if (ddl_Field.SelectedValue == null || ddl_Field.SelectedValue == "")
                    {
                        ddl_Field.SelectedValue = "0";
                    }
                    string AzSaat;
                    string TaSaat;

                    if (RTP3.SelectedDate == null)
                    {
                        AzSaat = "  :  ";
                    }
                    else
                    {
                        DateTime dstart = RTP3.SelectedDate.Value;
                        AzSaat = dstart.ToString("HH:mm");
                    }
                    if (RTP4.SelectedDate == null)
                    {
                        TaSaat = "  :  ";
                    }
                    else
                    {
                        DateTime d = RTP4.SelectedDate.Value;
                        TaSaat = d.ToString("HH:mm");
                    }
                    if (ddl_Degree.SelectedValue == null)
                    {
                        ddl_Degree.SelectedValue = "0";
                    }
                    if (ddl_Day.SelectedValue == null)
                    {
                        ddl_Day.SelectedValue = "0";
                    }
                    if (AzSaat == "  :  " || TaSaat == "  :  ")
                    {
                        RadWindowManager1.RadAlert("آیتم از ساعت ، تا ساعت  را انتخاب کنید", 0, 100, "پیام", "");
                    }
                    else
                    {
                        dtResault = ERB.GetListTadakholSaat(ddl_Term.SelectedValue.ToString(), AzSaat, TaSaat, timeStart, timeEnd, Convert.ToInt32(ddl_Daneshkade.SelectedValue.ToString()), Convert.ToInt32(ddl_Field.SelectedValue.ToString()), Convert.ToInt32(ddl_Day.SelectedValue.ToString()), int.Parse(ddl_Degree.SelectedValue));
                        if (dtResault.Rows.Count == 0)
                        {
                            RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیغام", "");
                        }
                        else
                        {
                            this.StiWebViewer1.ResetReport();
                            StiWebViewer1.Visible = true;
                            StiReport rpt = new StiReport();
                            rpt.Load(Server.MapPath("../Report/ReportListClassSpecificTimes.mrt"));
                            rpt.Dictionary.Databases.Clear();
                            rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", cb.ReportConnection.ToString()));
                            rpt.Compile();
                            rpt.CompiledReport.DataSources["[Education].[SP_ListClassSpecificTimes]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                            rpt.CompiledReport.DataSources["[Education].[SP_ListClassSpecificTimes]"].Parameters["@AzSaat"].ParameterValue = AzSaat;
                            rpt.CompiledReport.DataSources["[Education].[SP_ListClassSpecificTimes]"].Parameters["@TaSaat"].ParameterValue = TaSaat;
                            rpt.CompiledReport.DataSources["[Education].[SP_ListClassSpecificTimes]"].Parameters["@Saatstart"].ParameterValue = timeStart;
                            rpt.CompiledReport.DataSources["[Education].[SP_ListClassSpecificTimes]"].Parameters["@Saatend"].ParameterValue = timeEnd;
                            rpt.CompiledReport.DataSources["[Education].[SP_ListClassSpecificTimes]"].Parameters["@Daneshkade"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Education].[SP_ListClassSpecificTimes]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Education].[SP_ListClassSpecificTimes]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue.ToString());
                            rpt.CompiledReport.DataSources["[Education].[SP_ListClassSpecificTimes]"].Parameters["@Day"].ParameterValue = int.Parse(ddl_Day.SelectedValue.ToString());
                            //rpt.CompiledReport.DataSources["[Education].[SP_ReportListKlasBarAsasRuz]"].Parameters["@Degree"].ParameterValue = Convert.ToInt32(LCD.Degree.ToString());
                            rpt.RegData(dtResault);
                            rpt.Dictionary.Synchronize();
                            //rpt.Show();
                            StiWebViewer1.Report = rpt;
                            StiWebViewer1.Visible = true;
                            //rpt.Print(true);
                        }
                    }
                    //LCD.Daneshkade = LCD.Field = string.Empty;
                }
                //else if (rdb_MoghayeratBaTaghvim.Checked == true)
                //{

                //}
            }
        }

        protected void img_ExportToExcel1_Click(object sender, ImageClickEventArgs e)
        {
            string timeStart;
            string timeEnd;

            if (RTP1.SelectedDate == null)
            {
                timeStart = "  :  ";
            }
            else
            {
                DateTime dstart = RTP1.SelectedDate.Value;
                timeStart = dstart.ToString("HH:mm");
            }
            if (RTP2.SelectedDate == null)
            {
                timeEnd = "24:00";
            }
            else
            {
                DateTime d = RTP2.SelectedDate.Value;
                timeEnd = d.ToString("HH:mm");
            }
            DataTable dt = ERB.GetListClassBarAsasRuz(ddl_Term.SelectedValue.ToString(), Convert.ToInt32(ddl_Day.SelectedValue.ToString()), timeStart, timeEnd, Convert.ToInt32(ddl_Daneshkade.SelectedValue.ToString()), Convert.ToInt32(ddl_Field.SelectedValue.ToString()), int.Parse(ddl_Degree.SelectedValue));
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                gv_Show.DataSource = dt;
                gv_Show.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportClassListByDay.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    ////gv_Show.AllowPaging = false;
                    ////this.BindGrid();

                    //gv_Show.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in gv_Show.HeaderRow.Cells)
                    {
                        cell.BackColor = gv_Show.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in gv_Show.Rows)
                    {
                        //row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = gv_Show.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = gv_Show.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    gv_Show.RenderControl(hw);

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
        protected void img_ExportToExcel2_Click(object sender, ImageClickEventArgs e)
        {
            string timeStart;
            string timeEnd;

            if (RTP1.SelectedDate == null)
            {
                timeStart = "  :  ";
            }
            else
            {
                DateTime dstart = RTP1.SelectedDate.Value;
                timeStart = dstart.ToString("HH:mm");
            }
            if (RTP2.SelectedDate == null)
            {
                timeEnd = "24:00";
            }
            else
            {
                DateTime d = RTP2.SelectedDate.Value;
                timeEnd = d.ToString("HH:mm");
            }
            DataTable dt= ERB.GetListClassHazfi(ddl_Term.SelectedValue.ToString(), Convert.ToInt32(ddl_Day.SelectedValue.ToString()),timeStart, timeEnd, Convert.ToInt32(ddl_Daneshkade.SelectedValue.ToString()), Convert.ToInt32(ddl_Field.SelectedValue.ToString()), int.Parse(ddl_Degree.SelectedValue));
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportListClassRemoval.xls");
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

        protected void img_ExportToExcel3_Click(object sender, ImageClickEventArgs e)
        {
            string timeStart;
            string timeEnd;

            if (RTP1.SelectedDate == null)
            {
                timeStart = "  :  ";
            }
            else
            {
                DateTime dstart = RTP1.SelectedDate.Value;
                timeStart = dstart.ToString("HH:mm");
            }
            if (RTP2.SelectedDate == null)
            {
                timeEnd = "24:00";
            }
            else
            {
                DateTime d = RTP2.SelectedDate.Value;
                timeEnd = d.ToString("HH:mm");
            }
            DataTable dt;
            if ((txt_AzTarikhTahvilNomre.Text == "") || (txt_TaTarikhTahvilNomre.Text == ""))
            {
                dt = ERB.GetListklasTarikhNomre(ddl_Term.SelectedValue.ToString(), Convert.ToInt32(ddl_Daneshkade.SelectedValue.ToString()), "13  /  /  ", "13  /  /  ", Convert.ToInt32(ddl_Field.SelectedValue.ToString()), Convert.ToInt32(ddl_Day.SelectedValue.ToString()), timeStart, timeEnd, int.Parse(ddl_Degree.SelectedValue));
            }
            if ((txt_AzTarikhTahvilNomre.Text == ""))
            {
                dt = ERB.GetListklasTarikhNomre(ddl_Term.SelectedValue.ToString(), Convert.ToInt32(ddl_Daneshkade.SelectedValue.ToString()), txt_AzTarikhTahvilNomre.Text, "13  /  /  ", Convert.ToInt32(ddl_Field.SelectedValue.ToString()), Convert.ToInt32(ddl_Day.SelectedValue.ToString()), timeStart, timeEnd, int.Parse(ddl_Degree.SelectedValue));
            }
            if ((txt_TaTarikhTahvilNomre.Text == ""))
            {
                dt = ERB.GetListklasTarikhNomre(ddl_Term.SelectedValue.ToString(), Convert.ToInt32(ddl_Daneshkade.SelectedValue.ToString()), "13  /  /  ", txt_TaTarikhTahvilNomre.Text, Convert.ToInt32(ddl_Field.SelectedValue.ToString()), Convert.ToInt32(ddl_Day.SelectedValue.ToString()), timeStart, timeEnd, int.Parse(ddl_Degree.SelectedValue));
            }
            else
            {
                dt = ERB.GetListklasTarikhNomre(ddl_Term.SelectedValue.ToString(), Convert.ToInt32(ddl_Daneshkade.SelectedValue.ToString()), txt_AzTarikhTahvilNomre.Text, txt_TaTarikhTahvilNomre.Text, Convert.ToInt32(ddl_Field.SelectedValue.ToString()), Convert.ToInt32(ddl_Day.SelectedValue.ToString()), timeStart, timeEnd, int.Parse(ddl_Degree.SelectedValue));
            }
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                GridView2.DataSource = dt;
                GridView2.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportDateDeliveryScore.xls");
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
                        cell.BackColor = GridView2.HeaderStyle.BackColor;
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

        protected void img_ExportToExcel4_Click(object sender, ImageClickEventArgs e)
        {
            if (txt_ZarfiyatKamtar.Text==string.Empty)
            {
                txt_ZarfiyatKamtar.Text = "0";
            }
            if (txt_ZarfiyatBishtar.Text == string.Empty)
            {
                txt_ZarfiyatBishtar.Text = "0";
            }
            if (txt_TypeDivision.Text == string.Empty)
            {
                txt_TypeDivision.Text = "0";
            }
            string timeStart;
            string timeEnd;

            if (RTP1.SelectedDate == null)
            {
                timeStart = "  :  ";
            }
            else
            {
                DateTime dstart = RTP1.SelectedDate.Value;
                timeStart = dstart.ToString("HH:mm");
            }
            if (RTP2.SelectedDate == null)
            {
                timeEnd = "24:00";
            }
            else
            {
                DateTime d = RTP2.SelectedDate.Value;
                timeEnd = d.ToString("HH:mm");
            }
            DataTable dt = ERB.GetListHadeNesabKlas(ddl_Term.SelectedValue.ToString(), int.Parse(txt_ZarfiyatKamtar.Text), int.Parse(txt_ZarfiyatBishtar.Text), int.Parse(txt_TypeDivision.Text), Convert.ToInt32(ddl_Daneshkade.SelectedValue.ToString()), Convert.ToInt32(ddl_Field.SelectedValue.ToString()), timeStart, timeEnd, Convert.ToInt32(ddl_Day.SelectedValue.ToString()), int.Parse(ddl_Degree.SelectedValue));
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                GridView3.DataSource = dt;
                GridView3.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportListClassIsNotEnough.xls");
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

        protected void img_ExportToExcel5_Click(object sender, ImageClickEventArgs e)
        {
            string timeStart;
            string timeEnd;

            if (RTP1.SelectedDate == null)
            {
                timeStart = "  :  ";
            }
            else
            {
                DateTime dstart = RTP1.SelectedDate.Value;
                timeStart = dstart.ToString("HH:mm");
            }
            if (RTP2.SelectedDate == null)
            {
                timeEnd = "24:00";
            }
            else
            {
                DateTime d = RTP2.SelectedDate.Value;
                timeEnd = d.ToString("HH:mm");
            }
            string AzSaat;
            string TaSaat;

            if (RTP1.SelectedDate == null)
            {
                AzSaat = "  :  ";
            }
            else
            {
                DateTime dstart = RTP1.SelectedDate.Value;
                AzSaat = dstart.ToString("HH:mm");
            }
            if (RTP2.SelectedDate == null)
            {
                TaSaat = "  :  ";
            }
            else
            {
                DateTime d = RTP2.SelectedDate.Value;
                TaSaat = d.ToString("HH:mm");
            }
            DataTable dt = ERB.GetListTadakholSaat(ddl_Term.SelectedValue.ToString(), AzSaat, TaSaat, timeStart, timeEnd, Convert.ToInt32(ddl_Daneshkade.SelectedValue.ToString()), Convert.ToInt32(ddl_Field.SelectedValue.ToString()), Convert.ToInt32(ddl_Day.SelectedValue.ToString()), int.Parse(ddl_Degree.SelectedValue));
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                GridView4.DataSource = dt;
                GridView4.DataBind();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=ReportListClassSpecificTimes.xls");
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
    }
}



