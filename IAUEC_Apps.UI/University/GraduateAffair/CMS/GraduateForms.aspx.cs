using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAUEC_Apps.DTO.University.Graduate;
using IAUEC_Apps.Business.university.GraduateAffair;
using IAUEC_Apps.Business.university.Request;
using Stimulsoft;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using IAUEC_Apps.Business.Common;

//using IAUEC_Apps.Business.Common;
//using IAUEC_Apps.Business.university.Request;
//using IAUEC_Apps.DAO.University.Request;


//using IAUEC_Apps.Business.university.GraduateAffair;

namespace IAUEC_Apps.UI.University.GraduateAffair.CMS
{
    public partial class GraduateForms : System.Web.UI.Page
    {
        GraduateFormsBusiness GFB = new GraduateFormsBusiness();
        GraduateFormsDTO GFD = new GraduateFormsDTO();
        CommonBusiness CB = new CommonBusiness();
        //CommonDAO dao = new CommonDAO();

        DataTable dt = new DataTable();
        DataTable dr = new DataTable();

        int type = 0;

        string stCode;
        string family;
        string iddMeli;
        int flag;
        string printDocumentType, printDocument_Stcode;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlInfo.Visible = false;
                grdResults.Visible = false;
                if (Session["printDocument_Type"] != null)
                {
                    printDocument_Stcode = Session["printDocument_Stcode"].ToString();
                    printDocumentType = Session["printDocument_Type"].ToString();
                    ViewState["stCode"] = printDocument_Stcode;
                    ViewState["family"] = "";
                    ViewState["iddMeli"] = "";
                    BindGrid();
                    grdResults.SelectedIndex = 0;
                    showStdInfo();
                    drpForms.SelectedValue = printDocumentType == "daneshname" ? "8" : "7";
                }
                else
                {
                    printDocumentType = "";
                    printDocument_Stcode = "";
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            dvPayanShowType.Visible = false;
            pnlInfo.Visible = false;
            StiWebViewer1.Visible = false;
            if (txtFamily.Text == string.Empty && txtStCode.Text == string.Empty && txtIddMeli.Text == string.Empty) //at least one text box should have a value for search
            {
                showMessage("حداقل بايد يکي از باکس ها را پر کنيد");
            }
            else
            {
                grdResults.Visible = true;

                stCode = txtStCode.Text;
                family = txtFamily.Text;
                iddMeli = txtIddMeli.Text;
                ViewState.Add("stCode", stCode);
                ViewState.Add("family", family);
                ViewState.Add("iddMeli", iddMeli);
                

                BindGrid();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlInfo.Visible = false;
            grdResults.Visible = false;
            StiWebViewer1.Visible = false;
            txtStCode.Text = string.Empty;
            txtFamily.Text = string.Empty;
        }

        protected void grdResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            showStdInfo();
            StiWebViewer1.Visible = false;

            divLoanInfo.Visible = false;
            drpForms.SelectedIndex = 0;
        }

        protected void grdResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //paging in data grid view
            grdResults.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void grdResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //select row in datagrid view
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            e.Row.Attributes["ondblclick"] = ClientScript.GetPostBackClientHyperlink(this.grdResults, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "براي انتخاب سطر روی آن کليک کنيد.";
        }

        private void BindGrid()
        {
            DataTable dtTemp = new DataTable();
            

            GFD.stCode = ViewState["stCode"].ToString();
            GFD.family = ViewState["family"].ToString();
            GFD.iddMeli = ViewState["iddMeli"].ToString();

            dt = GFB.getStudentInfo(GFD);
            DataView dv = dt.DefaultView;
            DataRow[] dr = dv.ToTable().Select("isfaregh=1");
            dt.Rows.Clear();
            dt.AcceptChanges();
            if (dr.Length > 0)
                dt = dr.CopyToDataTable();
            ViewState.Add("dtCurrentStudent", dt);
            dtTemp = dt.Copy();
            dtTemp.Columns.RemoveAt(7);
            grdResults.DataSource = dtTemp;
            grdResults.DataBind();
        }
        //;;
        protected void showStdInfo()
        {
            dt = (DataTable)ViewState["dtCurrentStudent"];
            try
            {

                //To show selected item's name in edit label
                stCode = grdResults.SelectedRow.Cells[3].Text;
                string varName = grdResults.SelectedRow.Cells[4].Text;
                string varfamily = grdResults.SelectedRow.Cells[5].Text;
                string varFatherName = grdResults.SelectedRow.Cells[6].Text;
                string varSSN = grdResults.SelectedRow.Cells[7].Text;
                string varReshte = grdResults.SelectedRow.Cells[8].Text;
                string varMaghta = grdResults.SelectedRow.Cells[9].Text;
                //string varFaregh = dt.Rows[grdResults.SelectedIndex][7].ToString();

                ViewState.Add("stCode", stCode);

                lblName.Text = varName;
                lblFamily.Text = varfamily;
                lblFatherName.Text = varFatherName;
                lblSSN.Text = varSSN;
                lblReshte.Text = varReshte;
                lblMaghta.Text = varMaghta;

                //if (Convert.ToInt32(varFaregh) == 0)
                //{
                //    btnGraduateDraft.Enabled = false;
                //    btnGraduateStatus.Enabled = false;
                //    btnCoursePassesByType.Enabled = false;
                //    btnMarkList.Enabled = false;
                //}
                //else
                //{
                //    btnGraduateDraft.Enabled = true;
                //    btnGraduateStatus.Enabled = true;
                //    btnCoursePassesByType.Enabled = true;
                //    btnMarkList.Enabled = true;
                //}
                pnlInfo.Visible = true;
            }
            catch (Exception)
            {
                pnlInfo.Visible = false;
                showMessage("داده ای موجود نیست");
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (Session["printDocument_Type"] != null)
            {
                Session.Remove("printDocument_Stcode");
                Session.Remove("printDocument_Type");
            }
            StiReport rpt = new StiReport();
            switch (Convert.ToInt32(drpForms.SelectedValue))
            {
                case 1:
                    StiWebViewer1.Visible = true;
                    StiWebViewer1.ResetReport();

                    stCode = (string)ViewState["stCode"];
                    GFD.stCode = stCode;

                    dr = GFB.getStatusReportInfo(GFD);
                    rpt.Load(Server.MapPath("../Reports/GraduateStatus.mrt"));
                    rpt.Dictionary.Databases.Clear();
                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.SupplementaryReportConnection.ToString()));
                    rpt.Compile();
                    rpt.CompiledReport.DataSources["[Graduate].[SP_getFormVaziatInfo]"].Parameters["@stcode"].ParameterValue = stCode;
                    rpt.RegData(dr);
                    StiWebViewer1.Report = rpt;
                    break;
                case 2:
                    StiWebViewer1.Visible = true;
                    StiWebViewer1.ResetReport();

                    stCode = (string)ViewState["stCode"];
                    GFD.stCode = stCode;

                    dr = GFB.getDrafReportInfo(GFD);
                    rpt.Load(Server.MapPath("../Reports/GraduateDraft.mrt"));
                    rpt.Dictionary.Databases.Clear();
                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.SupplementaryReportConnection.ToString()));
                    rpt.Compile();
                    rpt.CompiledReport.DataSources["[Graduate].[SP_getFormDraftInfo]"].Parameters["@stcode"].ParameterValue = stCode;
                    rpt.CompiledReport.DataSources["[request].[SP_Get_StudentLoanInfo]"].Parameters["@stcode"].ParameterValue = stCode;
                    rpt.RegData(dr);
                    StiWebViewer1.Report = rpt;
                    break;
                case 3://ریزنمره
                    var SFD = GFB.getStudentFeraghatDocument(ViewState["stCode"].ToString());
                    if (SFD.dateRiznomreErsal != null && SFD.dateRiznomreErsal.Trim() != "" && SFD.dateRiznomreErsal.Trim() != "-")
                    {
                        showMessage("برای این دانشجو در تاریخ " + SFD.dateRiznomreErsal + " ریز نمره ارسال شده است.");
                    }

                    StiWebViewer1.Visible = true;
                    StiWebViewer1.ResetReport();

                    stCode = (string)ViewState["stCode"];
                    GFD.stCode = stCode;


                    rpt.Load(Server.MapPath("../Reports/FinalWorkbook.mrt"));
                    rpt.Dictionary.Databases.Clear();
                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.SupplementaryReportConnection.ToString()));
                    rpt.Compile();
                    rpt.CompiledReport.DataSources["[Graduate].[SP_getFormMarkListInfo]"].Parameters["@stcode"].ParameterValue = stCode;
                    rpt.CompiledReport.DataSources["[Graduate].[SP_getPayanFormMarkListInfo]"].Parameters["@stcode"].ParameterValue = stCode;
                    rpt.CompiledReport.DataSources["[Graduate].[SP_getPayanFormMarkListInfo]"].Parameters["@multiPayan"].ParameterValue = (ddl_PayanType.SelectedItem.Value == "1" ? 1 : 0);
                    rpt.CompiledReport.DataSources["[dbo].[SP_GetStudentPic]"].Parameters["@stcode"].ParameterValue = stCode;

                    StiWebViewer1.Report = rpt;
                    setLog(Convert.ToInt32(stCode));
                    break;
                case 4:

                    StiWebViewer1.Visible = true;
                    StiWebViewer1.ResetReport();

                    stCode = (string)ViewState["stCode"];
                    GFD.stCode = stCode;

                    dr = GFB.getCourseReportInfo(GFD);
                    rpt.Load(Server.MapPath("../Reports/CorsePassesByType.mrt"));
                    rpt.Dictionary.Databases.Clear();
                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.SupplementaryReportConnection.ToString()));
                    rpt.Compile();
                    rpt.CompiledReport.DataSources["[Graduate].[SP_getCoursePassedInfo]"].Parameters["@stcode"].ParameterValue = stCode;
                    rpt.CompiledReport.DataSources["[dbo].[SP_GetStudentPic]"].Parameters["@stcode"].ParameterValue = stCode;
                    rpt.RegData(dr);
                    StiWebViewer1.Report = rpt;
                    break;
                case 5://استعلام گواهی موقت
                case 6://استعلام دانشنامه

                    type = Convert.ToInt32(drpForms.SelectedValue);
                    if (chkRizNomre.Checked && type==6)
                    {
                        type = 61;
                    }
                    StiWebViewer1.Visible = true;
                    StiWebViewer1.ResetReport();

                    stCode = (string)ViewState["stCode"];
                    GFD.stCode = stCode;

                    dr = GFB.getCourseReportInfo(GFD);
                    rpt.Load(Server.MapPath("../Reports/TaeedTahsili.mrt"));
                    rpt.Dictionary.Databases.Clear();
                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.SupplementaryReportConnection.ToString()));
                    rpt.Compile();
                    rpt.CompiledReport.DataSources["Graduate.SP_getFormTaeedieTahsili"].Parameters["@stcode"].ParameterValue = stCode;
                    rpt.CompiledReport.DataSources["Graduate.SP_getFormTaeedieTahsili"].Parameters["@Type"].ParameterValue = type;
                    rpt.CompiledReport.DataSources["Graduate.SP_getFormTaeedieTahsili"].Parameters["@InquiryID"].ParameterValue = drpInquiry.SelectedItem.Value;
                    rpt.CompiledReport.DataSources["[dbo].[SP_GetStudentPic]"].Parameters["@stcode"].ParameterValue = stCode;

                    //vam dar
                    rpt.RegData(dr);
                    StiWebViewer1.Report = rpt;
                    break;
                case 7://گواهینامه موقت
                    var SFD_G = GFB.getStudentFeraghatDocument(ViewState["stCode"].ToString());
                    if (SFD_G.dateGovahiSodur != null && SFD_G.dateGovahiSodur.Trim() != "" && SFD_G.dateGovahiSodur.Trim() != "-")
                    {
                        showMessage("برای این دانشجو در تاریخ " + SFD_G.dateGovahiSodur + " گواهینامه موقت ارسال شده است.");
                    }
                    DataTable dt;
                    CheckOutRefahBusiness refah = new CheckOutRefahBusiness();
                    dt = refah.GetAllDebitByStcode(ViewState["stCode"].ToString());
                    if (dt.Rows.Count != 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            if (dt.Rows[0]["DebitTypeID"].ToString() == "1")
                            {
                                divLoanInfo.Visible = true;
                                break;
                            }
                        }

                        DateTime date;
                        Boolean flag = DateTime.TryParse(txtPayDate.Text,
                                  new System.Globalization.CultureInfo("fa-IR"),
                                  System.Globalization.DateTimeStyles.None,
                                  out date);
                        if (!flag)
                        {
                            revPayDate.IsValid = false;
                        }
                    }
                    else
                    {
                        divLoanInfo.Visible = false;
                        clearTextBox();
                    }
                    StiWebViewer1.Visible = true;
                    StiWebViewer1.ResetReport();

                    stCode = (string)ViewState["stCode"];
                    GFD.stCode = stCode;

                    dr = GFB.getCourseReportInfo(GFD);
                    rpt.Load(Server.MapPath("../Reports/GovahiMovaghatPayanTahsil.mrt"));
                    rpt.Dictionary.Databases.Clear();
                    rpt.Dictionary.Databases.Add(new StiSqlDatabase("Connection1", CB.SupplementaryReportConnection.ToString()));
                    rpt.Compile();

                    rpt.CompiledReport.DataSources["[Graduate].[SP_getFormGovahiMovaghatPayanTahsilat]"].Parameters["@stcode"].ParameterValue = ViewState["stCode"].ToString();
                    rpt.CompiledReport.DataSources["[Graduate].[SP_getFormGovahiMovaghatPayanTahsilat]"].Parameters["@date"].ParameterValue = txtPayDate.Text;
                    rpt.CompiledReport.DataSources["[Graduate].[SP_getFormGovahiMovaghatPayanTahsilat]"].Parameters["@monthPay"].ParameterValue = txtAmount.Text;
                    rpt.CompiledReport.DataSources["[Graduate].[SP_getFormGovahiMovaghatPayanTahsilat]"].Parameters["@acountNumber"].ParameterValue = txtAcountNumber.Text;
                    rpt.CompiledReport.DataSources["[Graduate].[SP_getFormGovahiMovaghatPayanTahsilat]"].Parameters["@bankName"].ParameterValue = txtBankName.Text;
                    rpt.CompiledReport.DataSources["[Graduate].[SP_getFormGovahiMovaghatPayanTahsilat]"].Parameters["@branchName"].ParameterValue = txtBranchName.Text;
                    rpt.CompiledReport.DataSources["[Graduate].[SP_getFormGovahiMovaghatPayanTahsilat]"].Parameters["@address"].ParameterValue = txtAddress.Text;


                    rpt.RegData(dr);
                    StiWebViewer1.Report = rpt;

                    break;
                case 8://دانشنامه
                    var SFD_Danesh = GFB.getStudentFeraghatDocument(ViewState["stCode"].ToString());
                    if (SFD_Danesh.dateDaneshnameSodur != null && SFD_Danesh.dateDaneshnameSodur.Trim() != "" && SFD_Danesh.dateDaneshnameSodur.Trim() != "-")
                    {
                        showMessage("برای این دانشجو در تاریخ " + SFD_Danesh.dateDaneshnameSodur + " دانشنامه صادر شده است.");
                    }
                    StiWebViewer1.ResetReport();
                    if (GFB.IsEquivalentTwoYearsGraduated(ViewState["stCode"].ToString()))
                    {
                        rpt.Load(Server.MapPath("../Reports/Daneshname_Kardani.mrt"));
                        rpt.Dictionary.Databases.Clear();
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("conn", CB.SupplementaryReportConnection.ToString()));
                        rpt.Compile();

                        rpt.CompiledReport.DataSources["Graduate.sp_getFormKardani"].Parameters["@stcode"].ParameterValue = ViewState["stCode"].ToString();
                    }
                    else
                    {
                        rpt.Load(Server.MapPath("../Reports/daneshname.mrt"));
                        rpt.Dictionary.Databases.Clear();
                        rpt.Dictionary.Databases.Add(new StiSqlDatabase("conn", CB.SupplementaryReportConnection.ToString()));
                        rpt.Compile();

                        rpt.CompiledReport.DataSources["Graduate.sp_getFromDaneshname"].Parameters["@stcode"].ParameterValue = ViewState["stCode"].ToString();
                    }
                    StiWebViewer1.Report = rpt;
                    StiWebViewer1.Visible = true;
                    break;
            }


            revPayDate.IsValid = true;

        }

        protected void drpForms_SelectedIndexChanged(object sender, EventArgs e)
        {
            StiWebViewer1.Visible = false;
            dvPayanShowType.Visible = drpForms.SelectedItem.Value == "3";
            ddl_PayanType.SelectedIndex = 0;
            //ddl_PayanType.Visible = drpForms.SelectedItem.Value == "3";
            chkRizNomre.Visible = drpForms.SelectedValue == "6";
            dvEstelam.Visible = drpForms.SelectedValue == "6" || drpForms.SelectedValue == "5";
            if(drpForms.SelectedValue == "6" || drpForms.SelectedValue == "5")
            {
                setDrpInquirySource();
            }
            if (drpForms.SelectedValue == "7")
            {
                DataTable checkDebit = new DataTable();
                CheckOutRefahBusiness refah = new CheckOutRefahBusiness();
                checkDebit = refah.GetAllDebitByStcode(ViewState["stCode"].ToString());
                if (checkDebit.Rows.Count != 0)
                {
                    divLoanInfo.Visible = true;
                }
                else
                {
                    divLoanInfo.Visible = false;
                }
            }
            else
            {
                divLoanInfo.Visible = false;
            }
        }

        private void clearTextBox()
        {
            txtPayDate.Text = null;
            txtAmount.Text = null;
            txtAcountNumber.Text = null;
            txtBankName.Text = null;
            txtBranchName.Text = null;
            txtAddress.Text = null;
        }

        protected void grdResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "btnView")
            {
                int rowInd = Convert.ToInt32(e.CommandArgument);
                grdResults.SelectedIndex = rowInd;
                showStdInfo();
                StiWebViewer1.Visible = false;

                divLoanInfo.Visible = false;
                drpForms.SelectedIndex = 0;
                dvPayanShowType.Visible = false;

            }
        }

        private void setLog(int modifyId)
        {
            Business.Common.CommonBusiness CB = new Business.Common.CommonBusiness();
            int userId;//کاربری که لاگین کرده
            int appId;//کد قسمتی از برنامه که الان توش هستیم. یعنی فارغ التحصیلان  -  10

            userId = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
            appId = 10;

            CB.InsertIntoUserLog(userId, DateTime.Now.ToString("HH:mm"), appId, (int)DTO.eventEnum.مشاهده_ريز_نمرات_فارغ_التحصيلان, "", modifyId);
        }

        private void showMessage(string msg)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);

        }

        private void setDrpInquirySource()
        {
           var inq= GFB.getStudentInquiry(ViewState["stCode"].ToString(), drpForms.SelectedValue == "5" ? 1 : 2);
            drpInquiry.Items.Clear();
            drpInquiry.DataSource = inq;
            drpInquiry.DataTextField = "requestName";
            drpInquiry.DataValueField = "id";
            drpInquiry.DataBind();
        }
    }
}