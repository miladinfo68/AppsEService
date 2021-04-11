using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.university.Education;
using IAUEC_Apps.DTO.University.Education;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using IAUEC_Apps.Business.Common;
using OfficeOpenXml.Style;
using System.Drawing;

//using ClosedXML.Excel;
//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Spreadsheet; 


namespace IAUEC_Apps.UI.University.Education.CMS
{
    public partial class BarnameHaftegi : System.Web.UI.Page
    {
       // CommonDAO dao = new CommonDAO();
        CommonBusiness cb = new CommonBusiness();
        UniversityBusiness UB = new UniversityBusiness();
        DataTable dtTerm = new DataTable();
        DataTable dtDaneshkade = new DataTable();
        DataTable dtDegree = new DataTable();
        DataTable dtField = new DataTable();
        DataTable dtLocationClass = new DataTable();
        DataTable dtDepartman = new DataTable();
        public static BarnameHaftegiDTO BHD = new BarnameHaftegiDTO();
        EducationReportBusiness ERB = new EducationReportBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            //ramezanian
            StiWebViewer1.Visible = false;
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
                    Session[sessionNames.menuID] = menuId;
                }
                AccessControl1.MenuId = Session[sessionNames.menuID].ToString();
                AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
                //
                {
                    dtTerm = cb.SelectAllTerm();
                    ddl_Term.DataTextField = "tterm";
                    ddl_Term.DataSource = dtTerm;
                    ddl_Term.DataBind();
                    ddl_Term.Items.Add(new ListItem("انتخاب کنید", "0"));
                    ddl_Term.Items[ddl_Term.Items.Count - 1].Selected = true;
                    //ddl_Term.Items.Insert(0, "انتخاب کنید");
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
                    ddl_Daneshkade.Items.Insert(0, "انتخاب کنید");
                    ddl_Daneshkade.Items.Add(new ListItem("انتخاب کنید", "0"));
                    ddl_Daneshkade.Items[ddl_Daneshkade.Items.Count - 1].Selected = true;

                    ddl_Degree.Items.Add(new ListItem("کاردانی", "2"));
                    ddl_Degree.Items.Add(new ListItem("کارشناسی", "1"));
                    ddl_Degree.Items.Add(new ListItem("کارشناسی ناپیوسته", "3"));
                    ddl_Degree.Items.Add(new ListItem("کارشناسی ارشد پیوسته", "4"));
                    ddl_Degree.Items.Add(new ListItem("کارشناسی ارشد ناپیوسته", "5"));
                    ddl_Degree.Items.Add(new ListItem("دکتری تخصصی", "6"));
                    //ddl_Degree.Items.Insert(0, "انتخاب کنید");
                    ddl_Degree.Items.Add(new ListItem("انتخاب کنید", "0"));
                    ddl_Degree.Items[ddl_Degree.Items.Count - 1].Selected = true;
                    //BHD.Degree = ddl_Degree.SelectedValue;
                    dtField = cb.SelectAllField();
                    ddl_Field.DataTextField = "nameresh";
                    ddl_Field.DataValueField = "id";
                    ddl_Field.DataSource = dtField;
                    ddl_Field.DataBind();
                    ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
                    ddl_Field.Items[ddl_Field.Items.Count - 1].Selected = true;
                    //BHD.Field = ddl_Field.SelectedValue;
                    dtDepartman = cb.GetAllDepartman();
                    ddl_Departman.DataTextField = "namegroup";
                    ddl_Departman.DataValueField = "id";
                    ddl_Departman.DataSource = dtDepartman;
                    ddl_Departman.DataBind();
                    //ddl_Departman.Items.Insert(0, "انتخاب کنید");
                    ddl_Departman.Items.Add(new ListItem("انتخاب کنید", "0"));
                    ddl_Departman.Items[ddl_Departman.Items.Count - 1].Selected = true;
                    //BHD.Departman = ddl_Departman.SelectedValue;
                    dtLocationClass = ERB.SelectAllLocatoionClass();
                    ddl_LocationClass.DataTextField = "name_mahal";
                    ddl_LocationClass.DataValueField = "id";
                    ddl_LocationClass.DataSource = dtLocationClass;
                    ddl_LocationClass.DataBind();
                    //ddl_LocationClass.Items.Insert(0, "انتخاب کنید");
                    ddl_LocationClass.Items.Add(new ListItem("انتخاب کنید ", "0"));
                    ddl_LocationClass.Items[ddl_LocationClass.Items.Count - 1].Selected = true;
                    //BHD.LocationClass = ddl_LocationClass.SelectedValue;
                }
            }
        }
        //ramezanian
        protected void ddl_Day_SelectedIndexChanged(object sender, EventArgs e)
        {
            BHD.Day = ddl_Day.SelectedValue.ToString();
        }
        //ramezanian
        protected void ddl_Field_SelectedIndexChanged(object sender, EventArgs e)
        {
            BHD.Field = ddl_Field.SelectedValue;
        }
        //ramezanian
        protected void ddl_Departman_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Departman.SelectedValue == "0")
            {
                BHD.Departman = ddl_Departman.SelectedValue.ToString();
                dtField = cb.SelectAllField();
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dtField;
                ddl_Field.DataBind();
                ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Field.Items[ddl_Field.Items.Count - 1].Selected = true;
            }
            else
            {
                BHD.Departman = ddl_Departman.SelectedValue.ToString();
                dtField = UB.GetFieldByDepartman(int.Parse(BHD.Departman));
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dtField;
                ddl_Field.DataBind();
                ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Field.Items[ddl_Field.Items.Count - 1].Selected = true;
            }
        }
        //ramezanian
        protected void ddl_Degree_SelectedIndexChanged(object sender, EventArgs e)
        {

            BHD.Degree = ddl_Degree.SelectedValue;
        }
        //ramezanian
        protected void ddl_Term_SelectedIndexChanged(object sender, EventArgs e)
        {

            BHD.Term = ddl_Term.SelectedValue;
        }
        //ramezanian
        protected void ddl_LocationClass_SelectedIndexChanged(object sender, EventArgs e)
        {

            BHD.LocationClass = ddl_LocationClass.SelectedValue;
        }
        //ramezanian
        protected void ddl_Daneshkade_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (ddl_Daneshkade.SelectedValue == "0")
            {
                BHD.Daneshkade = ddl_Daneshkade.SelectedValue;
                dtField = ERB.SelectAllField();
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dtField;
                ddl_Field.DataBind();
                ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Field.Items[ddl_Field.Items.Count - 1].Selected = true;
                BHD.Field = ddl_Field.SelectedValue;
                dtDepartman = cb.GetAllDepartman();
                ddl_Departman.DataTextField = "namegroup";
                ddl_Departman.DataValueField = "id";
                ddl_Departman.DataSource = dtDepartman;
                ddl_Departman.DataBind();
                ddl_Departman.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Departman.Items[ddl_Departman.Items.Count - 1].Selected = true;
                //ddl_Departman.Items.Insert(0, "انتخاب کنید");

                BHD.Departman = ddl_Departman.SelectedValue;
            }
            else
            {
                BHD.Daneshkade = ddl_Daneshkade.SelectedValue;
                dt = cb.GetAllDepartman(int.Parse(BHD.Daneshkade));
                ddl_Departman.DataSource = dt;
                ddl_Departman.DataTextField = "namegroup";
                ddl_Departman.DataValueField = "id";
                ddl_Departman.DataBind();
                ddl_Departman.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Departman.Items[ddl_Departman.Items.Count - 1].Selected = true;
                DataTable dtField = new DataTable();
                dtField = cb.GetReshByDaneshkade(int.Parse(BHD.Daneshkade));
                ddl_Field.DataTextField = "nameresh";
                ddl_Field.DataValueField = "id";
                ddl_Field.DataSource = dtField;
                ddl_Field.DataBind();
                ddl_Field.Items.Add(new ListItem("انتخاب کنید", "0"));
                ddl_Field.Items[ddl_Field.Items.Count - 1].Selected = true;
            }
        }

        protected void btn_ShowReport_Click(object sender, EventArgs e)
        {
            img_ExportToExcel.Visible = false;
            DataTable dtResault = new DataTable();
            if (ddl_Term.SelectedValue == null || ddl_Term.SelectedValue == "0")
            {
                RadWindowManager1.RadAlert("لطفا ترم مورد نظر را انتخاب بفرمایید", 0, 100, "پیام", "");
            }
            else
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
                    timeEnd = "  :  ";
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
                if (ddl_LocationClass.SelectedValue == null)
                {
                    ddl_LocationClass.SelectedValue = "0";
                }

                if (ddl_Field.SelectedValue == null)
                {
                    ddl_Field.SelectedValue = "0";
                }
                if (ddl_Departman.SelectedValue == null)
                {
                    ddl_Departman.SelectedValue = "0";
                }

                if (ddl_Degree.SelectedValue == null)
                {
                    ddl_Degree.SelectedValue = "0";
                }
                if (ddl_Daneshkade.SelectedValue == null)
                {
                    ddl_Daneshkade.SelectedValue = "0";
                }
                dtResault = ERB.GetListBarnameHaftegi(ddl_Term.SelectedValue, int.Parse(ddl_Day.SelectedValue), int.Parse(ddl_LocationClass.SelectedValue), int.Parse(ddl_Field.SelectedValue), int.Parse(ddl_Departman.SelectedValue), int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Degree.SelectedValue), timeStart, timeEnd);

                if (dtResault.Rows.Count == 0)
                {
                    RadWindowManager1.RadAlert("رکوردی وجود ندارد", 0, 100, "پیغام", "");
                }
                else
                {
                    img_ExportToExcel.Visible = true;
                    this.StiWebViewer1.ResetReport();
                    StiWebViewer1.Visible = true;
                    StiReport rpt = new StiReport();
                    rpt.Load(Server.MapPath("../Report/ReportWeeklyPlan1.mrt"));
                    rpt.ReportCacheMode = StiReportCacheMode.On;
                    rpt.Dictionary.Databases.Clear();
                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", (cb.ReportConnection).ToString()));
                    rpt.Compile();
                    rpt.CompiledReport.DataSources["[Education].[SP_WeeklyPlan]"].Parameters["@Term"].ParameterValue = ddl_Term.SelectedValue;
                    rpt.CompiledReport.DataSources["[Education].[SP_WeeklyPlan]"].Parameters["@Day"].ParameterValue = int.Parse(ddl_Day.SelectedValue);
                    rpt.CompiledReport.DataSources["[Education].[SP_WeeklyPlan]"].Parameters["@Field"].ParameterValue = int.Parse(ddl_Field.SelectedValue);
                    rpt.CompiledReport.DataSources["[Education].[SP_WeeklyPlan]"].Parameters["@LocationClass"].ParameterValue = int.Parse(ddl_LocationClass.SelectedValue);
                    rpt.CompiledReport.DataSources["[Education].[SP_WeeklyPlan]"].Parameters["@Departman"].ParameterValue = int.Parse(ddl_Departman.SelectedValue);
                    rpt.CompiledReport.DataSources["[Education].[SP_WeeklyPlan]"].Parameters["@Daneshkade"].ParameterValue = int.Parse(ddl_Daneshkade.SelectedValue);
                    rpt.CompiledReport.DataSources["[Education].[SP_WeeklyPlan]"].Parameters["@Degree"].ParameterValue = int.Parse(ddl_Degree.SelectedValue);
                    rpt.CompiledReport.DataSources["[Education].[SP_WeeklyPlan]"].Parameters["@SaatSt1"].ParameterValue = timeStart;
                    rpt.CompiledReport.DataSources["[Education].[SP_WeeklyPlan]"].Parameters["@SaatSt2"].ParameterValue = timeEnd;
                    rpt.RegData(dtResault);
                    //rpt.Dictionary.Synchronize();
                    //rpt.Show();
                    StiWebViewer1.Report = rpt;
                    StiWebViewer1.Visible = true;
                    //rpt.Print(true);
                }
            }
        }
        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
        //       server control at run time. */
        //}

        protected void img_ExportToExcel_Click(object sender, ImageClickEventArgs e)
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
                timeEnd = "  :  ";
            }
            else
            {
                DateTime d = RTP2.SelectedDate.Value;
                timeEnd = d.ToString("HH:mm");
            }
            DataTable dt = ERB.GetListBarnameHaftegi(ddl_Term.SelectedValue, int.Parse(ddl_Day.SelectedValue), int.Parse(ddl_LocationClass.SelectedValue), int.Parse(ddl_Field.SelectedValue), int.Parse(ddl_Departman.SelectedValue), int.Parse(ddl_Daneshkade.SelectedValue), int.Parse(ddl_Degree.SelectedValue), timeStart, timeEnd);
            if (dt.Rows.Count == 0)
            {
            }
            else
            {
                gv_Show.DataSource = dt;
                gv_Show.DataBind();
                try
                {

                    var pck = new OfficeOpenXml.ExcelPackage();
                    var ws = pck.Workbook.Worksheets.Add("Mailing List");
                    gv_Show.HeaderStyle.Font.Bold = true;     // SET HEADER AS BOLD.
                    using (var rng = ws.Cells["A1:N1"])
                    {
                        rng.Style.Font.Bold = true;
                        rng.Style.WrapText = true;
                        rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(237, 237, 237));
                        rng.Style.Font.Size = 12;
                    }
                    ws.Cells["A1"].Value = "مشخصه";
                    ws.Cells["B1"].Value = "نیمسال";
                    ws.Cells["C1"].Value = "دانشکده";
                    ws.Cells["D1"].Value = "گروه";
                    ws.Cells["E1"].Value = "رشته";
                    ws.Cells["F1"].Value = "درس";
                    ws.Cells["G1"].Value = "نام استاد";
                    ws.Cells["H1"].Value = "روز";
                    ws.Cells["I1"].Value = "ساعت";
                    ws.Cells["J1"].Value = "ساعت شروع";
                    ws.Cells["K1"].Value = "ظرفیت کلاس";
                    ws.Cells["L1"].Value = "مرد";
                    ws.Cells["M1"].Value = "زن";
                    ws.Cells["N1"].Value = "ظرفیت پر شده کلاس";
                    ws.Cells["O1"].Value = "ظرفیت پر شده کلاس";
                    ws.Row(200).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Row(200).Style.Font.Name = "B Nazanin";
                    ws.Cells["A2"].LoadFromDataTable(dt, false);
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;  filename=MailingList.xlsx");
                    Response.BinaryWrite(pck.GetAsByteArray());
                }
                catch (Exception ex)
                {
                    //log error
                }
                Response.End();
            }
        }
    }
    }

        
