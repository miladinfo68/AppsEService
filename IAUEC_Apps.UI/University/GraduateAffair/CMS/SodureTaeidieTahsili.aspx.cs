using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IAUEC_Apps.Business.university.GraduateAffair;
using Microsoft.Reporting.WebForms;
using Telerik.Web.UI;
using Telerik.ReportViewer.WebForms;
using Telerik.Reporting;
using Stimulsoft.Report;
using IAUEC_Apps.Business.university.Request;
using Stimulsoft.Report.Dictionary;
using IAUEC_Apps.Business.Common;


namespace IAUEC_Apps.UI.University.GraduateAffair.CMS
{
    public partial class SodureTaeidieTahsili : System.Web.UI.Page
    {

        GraduateReportBusiness GraduateBusiness = new GraduateReportBusiness();
        RequestStudentCartBusiness CBusiness = new RequestStudentCartBusiness();
        CommonBusiness CB = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StiWebViewer1.Visible = false;
                if (Session["stcode"] != null)
                {
                    txt_StudentNumber.Text = Session["stcode"].ToString();
                    DataTable dt = new DataTable();
                    dt = GraduateBusiness.GetStudentInfoByStcode(Session["stcode"].ToString());
                    lbl_FatherName.Text = dt.Rows[0]["namep"].ToString();
                    lbl_Id.Text = dt.Rows[0]["idd"].ToString();
                    lbl_LastName.Text = dt.Rows[0]["family"].ToString();
                    lbl_maghta.Text = dt.Rows[0]["magh"].ToString() + "-" + dt.Rows[0]["dorpar"].ToString();
                    lbl_Name.Text = dt.Rows[0]["name"].ToString();
                    lbl_Reshte.Text = dt.Rows[0]["nameresh"].ToString();
                    DataTable dtgovahi = new DataTable();
                    dtgovahi = GraduateBusiness.GetStudentGovahiByStcode(Session["stcode"].ToString());
                    grd_Govahi.DataSource = dtgovahi;
                    grd_Govahi.DataBind();
                    div_Sabt.Visible = true;

                }
                ddl_Govahi.Items.Add(new ListItem("تاییدیه مدرک گواهینامه موقت", "4"));
                ddl_Govahi.Items.Add(new ListItem("تاییدیه مدرک دانشنامه", "6"));
                ddl_Govahi.Items.Insert(0, new ListItem("انتخاب کنید"));
                txt_StudentNumber.Focus();
            }

        }



        protected void grd_Govahi_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "ShowReport")
            {
                Session["id"] = e.CommandArgument.ToString();
                StiReport rpt = new StiReport();
                rpt.Load(Server.MapPath("../Reports/taeidtahsiliReport.mrt"));
                rpt.Dictionary.Databases.Clear();
                rpt.Dictionary.Databases.Add(new StiSqlDatabase("Supplementary", CB.ReportConnection.ToString()));
                rpt.Compile();
                rpt.CompiledReport.DataSources["SP_GetStudentPic"].Parameters["@stcode"].ParameterValue = Session["stcode"].ToString();
                rpt.CompiledReport.DataSources["[Graduate].[SP_GetStudentGovahiByID]"].Parameters["@id"].ParameterValue = Session["id"].ToString();
                rpt.CompiledReport.DataSources["[Graduate].[Sp_GetStSodorgovahidate]"].Parameters["@stcode"].ParameterValue = Session["stcode"].ToString();
                rpt.CompiledReport.DataSources["[Graduate].[Sp_GetStSodorgovahidate]"].Parameters["@idgovahi"].ParameterValue = int.Parse(Session["id"].ToString());
                this.StiWebViewer1.ResetReport(); 
                StiWebViewer1.Report = rpt;
                StiWebViewer1.Visible = true;

            }
            else if (e.CommandName == "DeleteGovahi")
            {
                if (e.Item is GridDataItem)
                {

                    GraduateBusiness.DeleteTaeidieTahsili(int.Parse(e.CommandArgument.ToString()));
                    DataTable dtgovahi = new DataTable();
                    dtgovahi = GraduateBusiness.GetStudentGovahiByStcode(Session["stcode"].ToString());
                    grd_Govahi.DataSource = dtgovahi;
                    grd_Govahi.DataBind();
                    rwm.RadAlert("با موفقیت حذف گردید", null, 100, "حذف گواهی", "");

                }
            }

        }

        protected void a_Search_ServerClick(object sender, EventArgs e)
        {
            Session["Page"] = 1;
            Response.Redirect("SearchStudentByParams.aspx");

        }

        protected void btn_Enter_Click(object sender, EventArgs e)
        {
            if (txt_StudentNumber.Text != "")
            {
                txt_StudentNumber.Text = txt_StudentNumber.Text;
                DataTable dt = new DataTable();
                //Bahrami
                dt = GraduateBusiness.GetStudentInfoByStcode(txt_StudentNumber.Text);
                lbl_FatherName.Text = dt.Rows[0]["namep"].ToString();
                lbl_Id.Text = dt.Rows[0]["idd"].ToString();
                lbl_LastName.Text = dt.Rows[0]["family"].ToString();
                lbl_maghta.Text = dt.Rows[0]["magh"].ToString();
                lbl_Name.Text = dt.Rows[0]["name"].ToString();
                lbl_Reshte.Text = dt.Rows[0]["idresh"].ToString();
                DataTable dtgovahi = new DataTable();
                //Bahrami
                dtgovahi = GraduateBusiness.GetStudentGovahiByStcode(txt_StudentNumber.Text);
                grd_Govahi.DataSource = dtgovahi;
                grd_Govahi.DataBind();
                div_Sabt.Visible = true;

            }
        }

        protected void grd_Govahi_UpdateCommand(object sender, GridCommandEventArgs e)
        {

            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            if (txt_StudentNumber.Text != "" || txt_Date.Text != "" || txt_Koja.Text != "" || txt_LtterNo.Text != "")
            {
                int rowid = GraduateBusiness.InsertTaeidieTahsili(txt_StudentNumber.Text, int.Parse(ddl_Govahi.SelectedValue.ToString()), txt_LtterNo.Text, txt_Date.Text, txt_Koja.Text);
                if (rowid == 0)
                {
                    txt_LtterNo.Text = "";
                    txt_Koja.Text = "";
                    txt_Date.Text = "";
                    ddl_Govahi.Items.Add(new ListItem("تاییدیه مدرک گواهینامه موقت", "4"));
                    ddl_Govahi.Items.Add(new ListItem("تاییدیه مدرک دانشنامه", "6"));
                    ddl_Govahi.Items.Insert(0, new ListItem("انتخاب کنید"));
                    ddl_Govahi.SelectedIndex = 1;
                    rwm.RadAlert("دانشجو در لیست فارغ التحصیلان ثبت نشده است", null, 100, "خطا", "");
                }

                else
                {
                    DataTable dtgovahi = new DataTable();
                    dtgovahi = GraduateBusiness.GetStudentGovahiByStcode(Session["stcode"].ToString());
                    grd_Govahi.DataSource = dtgovahi;
                    grd_Govahi.DataBind();
                    txt_LtterNo.Text = "";
                    txt_Koja.Text = "";
                    txt_Date.Text = "";
                    ddl_Govahi.Items.Add(new ListItem("تاییدیه مدرک گواهینامه موقت", "4"));
                    ddl_Govahi.Items.Add(new ListItem("تاییدیه مدرک دانشنامه", "6"));
                    ddl_Govahi.Items.Insert(0, new ListItem("انتخاب کنید"));
                    ddl_Govahi.SelectedIndex = 1;
                    rwm.RadAlert("با موفقیت ثبت گردید", null, 100, "ثبت گواهی", "");
                }

            }

            else
            {
                rwm.RadAlert("کلیه فیلدها باید تکمیل گردد", null, 100, "خطا", "");

            }
        }

        protected void grd_Govahi_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (Session["stcode"] != null)
            {
                DataTable dtgovahi = new DataTable();
                dtgovahi = GraduateBusiness.GetStudentGovahiByStcode(Session["stcode"].ToString());
                grd_Govahi.DataSource = dtgovahi;
            }

        }






    }
}