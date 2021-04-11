using IAUEC_Apps.DTO.University.Request;
using IAUEC_Apps.Business.university.Request;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Faculty;
using System.IO;

namespace IAUEC_Apps.UI.University.CooperationRequest.Teachers
{
    public partial class EditMain : System.Web.UI.Page
    {
        ProfessorRequestBusiness ProfReqBuss = new ProfessorRequestBusiness();
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();
        CommonBusiness CB = new CommonBusiness();
        const string substringScanFile = "ScanMadarek";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadRequestGrid();
            }
        }

        private void LoadRequestGrid()
        {
            int codeostad = Convert.ToInt32(Session[sessionNames.userID_StudentOstad]);
            if (!FRB.HasNotationId(codeostad))
            {
                RadWindowManager1.RadAlert("به دلیل عدم وجود کد ملی در سامانه ثبت اساتید امکان ورود وجود ندارد", 400, 100, "پیام سیستم", "RedirectToMain");

            }
            DataTable dtRequest = ProfReqBuss.GetAllRequestsByProfCode(codeostad);
            DataTable dtResult = ProfReqBuss.GetProfessorFromResearchByCode(codeostad);
            var prof = FRB.GetOstadInfoFromHR(codeostad);
            if (dtResult.Rows.Count == 0)
            {
                if (prof.Rows.Count > 0)
                    ViewState["hrId"] = prof.Rows[0][0].ToString();
            }
            else
            {
                ViewState["hrId"] = dtResult.Rows[0][0].ToString();
            }

            grdEditRequests.DataSource = dtRequest;
            grdEditRequests.DataBind();
            Business.university.CooperationRequest.CooperationRequestBusiness bsn = new Business.university.CooperationRequest.CooperationRequestBusiness();
            //bool hasContract = false;
            //DataTable dt = bsn.getContractOfTeacher(Convert.ToInt32(ViewState["hrId"]));
            //if (dt.Rows.Count == 1)
            //    if (dt.Rows[0]["contractFile"] != DBNull.Value)
            //        hasContract = true;
            //dvContract.Visible = !hasContract;
            //A2.Visible = !hasContract;
            //A3.Visible = hasContract;

        }

        protected void grdEditRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnCancel = (Button)e.Row.FindControl("btnCancel");
                DataRowView drv = (DataRowView)e.Row.DataItem;
                int reqType = Convert.ToInt32(drv["RequestTypeId"]);
                string strState = null;
                switch (reqType)
                {
                    case (int)RequestTypeId.EditPersonalInfo:
                        strState = "ویرایش اطلاعات فردی";
                        break;
                    case (int)RequestTypeId.EditContactInfo:
                        strState = "ویرایش اطلاعات تماس";
                        break;
                    case (int)RequestTypeId.EditHokm:
                        strState = "بروزرسانی حکم کارگزینی";
                        break;
                    case (int)RequestTypeId.EditCooperation:
                        strState = "ویرایش وضعیت همکاری";
                        break;
                    default:
                        break;
                }

                if (!string.IsNullOrWhiteSpace(strState))
                {
                    e.Row.Cells[2].Text = strState;
                }

                int logId = Convert.ToInt32(drv["RequestLogId"]);
                string strLog = null;
                switch (logId)
                {
                    case (int)RequestLogId.denied:
                        strLog = "درخواست شما رد شد";
                        btnCancel.Enabled = false;
                        break;
                    case (int)RequestLogId.submitted:
                        strLog = "در حال بررسی";
                        break;
                    case (int)RequestLogId.approved:
                        strLog = "تایید شده";
                        btnCancel.Enabled = false;
                        break;
                    default:
                        break;
                }

                if (!string.IsNullOrWhiteSpace(strLog))
                {
                    e.Row.Cells[4].Text = strLog;
                }
            }
        }

        protected void grdEditRequests_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "showchanges")
            {
                int reqId = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                ViewState.Add("reqId", reqId);
                HiddenField hdnfReqType = (HiddenField)gvr.FindControl("hdnfReqType");
                int reqType = Convert.ToInt32(hdnfReqType.Value);
                ViewState.Add("reqType", reqType);
                HiddenField hdnfReqLog = (HiddenField)gvr.FindControl("hdnfReqLog");
                int reqStatus = Convert.ToInt32(hdnfReqLog.Value);
                ViewState.Add("reqStatus", reqStatus);

                BindData(reqId, reqType, reqStatus);

                HiddenField hdnfUrl = (HiddenField)gvr.FindControl("hdnfURL");
                int code_ostad = Convert.ToInt32(Session[sessionNames.userID_StudentOstad]);
                if (reqType != (int)RequestTypeId.EditHokm)
                {
                    if (!string.IsNullOrWhiteSpace(hdnfUrl.Value))
                    {
                        string hokmUrl = hdnfUrl.Value;
                        int subIndex = hokmUrl.IndexOf(substringScanFile) + substringScanFile.Length + 1;
                        btnShowImage.HRef = "../OpenScanImage.aspx?o=" + code_ostad + "&f=" + hokmUrl.Substring(subIndex);
                        btnShowImage.Visible = true;
                    }
                }

                string scrp = "function f(){$find(\"" + RadWindow1.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
            }
            if (e.CommandName == "RemoveReq")
            {
                int reqId = Convert.ToInt32(e.CommandArgument);
                int counter = ProfReqBuss.DeleteProfessorRequest(reqId);
                string msg = string.Empty;
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                HiddenField hdnfReqType = (HiddenField)gvr.FindControl("hdnfReqType");
                int reqType = Convert.ToInt32(hdnfReqType.Value);
                if (counter > 0)
                {
                    int eventType = 0;
                    switch (reqType)
                    {
                        case 17:
                            eventType = 31;
                            break;
                        case 18:
                            eventType = 33;
                            break;
                        case 19:
                            eventType = 35;
                            break;
                        case 20:
                            eventType = 37;
                            break;
                    }
                    var hrId = ViewState["hrId"].ToString();
                    if (string.IsNullOrEmpty(hrId))
                    {
                        var userId = Session[sessionNames.userID_StudentOstad].ToString();
                        CB.InsertIntoStudentLog(userId, DateTime.Now.ToString("HH:mm"), 13, eventType, reqId.ToString());

                    }
                    else
                        CB.InsertIntoStudentLog(ViewState["hrId"].ToString(), DateTime.Now.ToString("HH:mm"), 13, eventType, reqId.ToString());
                    msg = "درخواست  با موفقیت لغو گردید.";

                }
                else
                {
                    msg = " خطا در هنگام لغو درخواست ، لطفا مجددا تلاش کنید.";
                }
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    RadWindowManager1.RadAlert(msg, 300, 100, "پیام سیستم", "refreshGrid");
                }

            }
        }

        protected void grdChangeList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dataRowView = (DataRowView)e.Row.DataItem;

                int ControlTofieldId = Convert.ToInt32(dataRowView["ControlTofieldId"]);
                int codingTypeId = Convert.ToInt32(dataRowView["CodingId"]);
                string OldValue = dataRowView["OldValue"].ToString();
                string NewValue = dataRowView["NewValue"].ToString();

                DataTable dtCoding = new DataTable();
                DropDownList drpOldValue = new DropDownList();
                DropDownList drpNewValue = (DropDownList)e.Row.FindControl("drpNewValue");
                DataRow dr;


                #region MyRegion
                switch (ControlTofieldId)
                {
                    case 7://مدرک تحصیلی  -2
                           //case 9://رشته تحصیلی-  4
                    case 19://دانشگاه تحصیل  -  1
                    case 30://نظام  -  7
                    case 35://دانشگاه خدمت  -  1
                        #region MyRegion
                        dtCoding = CB.GetCodingByTypeId(codingTypeId);
                        dr = dtCoding.NewRow();
                        dr["id"] = "0";
                        dr["namecoding"] = "انتخاب نشده";
                        dr["idTypeCoding"] = codingTypeId;
                        dtCoding.Rows.Add(dr);
                        drpOldValue.CssClass = "form-control";
                        drpOldValue.Enabled = false;
                        e.Row.Cells[2].Controls.Add(drpOldValue);
                        drpNewValue.Visible = true;
                        setSource("namecoding", "Id", dtCoding, ref drpOldValue, OldValue);
                        setSource("namecoding", "Id", dtCoding, ref drpNewValue, NewValue);
                        #endregion
                        break;

                    case 15://کشور تحصیل  -  5
                        #region MyRegion
                        dtCoding = CB.GetCodingByTypeId(codingTypeId).AsEnumerable().Where(row => row.Field<decimal>("ID") < 56).CopyToDataTable();
                        dr = dtCoding.NewRow();
                        dr["id"] = "-1";
                        dr["namecoding"] = "انتخاب نشده";
                        dr["idTypeCoding"] = codingTypeId;
                        dtCoding.Rows.Add(dr);
                        drpOldValue.CssClass = "form-control";
                        drpOldValue.Enabled = false;
                        e.Row.Cells[2].Controls.Add(drpOldValue);
                        drpNewValue.Visible = true;
                        if (string.IsNullOrEmpty(OldValue))
                            OldValue = "-1";
                        if (string.IsNullOrEmpty(NewValue))
                            NewValue = "-1";
                        setSource("namecoding", "Id", dtCoding, ref drpOldValue, OldValue);
                        setSource("namecoding", "Id", dtCoding, ref drpNewValue, NewValue);
                        #endregion
                        break;
                    case 9://رشته تحصیلی-  4
                        #region MyRegion
                        dtCoding = CB.GetCodingByTypeId(codingTypeId);
                        dr = dtCoding.NewRow();
                        dr["id"] = "-1";
                        dr["namecoding"] = "انتخاب نشده";
                        dr["idTypeCoding"] = codingTypeId;
                        dtCoding.Rows.Add(dr);
                        drpOldValue.CssClass = "form-control";
                        drpOldValue.Enabled = false;
                        e.Row.Cells[2].Controls.Add(drpOldValue);
                        drpNewValue.Visible = true;
                        setSource("namecoding", "Id", dtCoding, ref drpOldValue, OldValue);
                        setSource("namecoding", "Id", dtCoding, ref drpNewValue, NewValue);
                        #endregion
                        break;
                    case 17://نوع دانشگاه تحصیل
                        #region MyRegion
                        dtCoding.Columns.Add("id");
                        dtCoding.Columns.Add("title");

                        dr = dtCoding.NewRow();
                        dr["id"] = 0;
                        dr["title"] = "انتخاب نشده";
                        dtCoding.Rows.Add(dr);
                        dr = dtCoding.NewRow();
                        dr["id"] = 1;
                        dr["title"] = "دولتی";
                        dtCoding.Rows.Add(dr);
                        dr = dtCoding.NewRow();
                        dr["id"] = 2;
                        dr["title"] = "آزاد";
                        dtCoding.Rows.Add(dr);
                        dr = dtCoding.NewRow();
                        dr["id"] = 3;
                        dr["title"] = "حوزه";
                        dtCoding.Rows.Add(dr);
                        dr = dtCoding.NewRow();
                        dr["id"] = 4;
                        dr["title"] = "خارج از کشور";
                        dtCoding.Rows.Add(dr);
                        dr = dtCoding.NewRow();
                        dr["id"] = 5;
                        dr["title"] = "سایر";
                        dtCoding.Rows.Add(dr);

                        drpOldValue.CssClass = "form-control";
                        drpOldValue.Enabled = false;
                        e.Row.Cells[2].Controls.Add(drpOldValue);
                        drpNewValue.Visible = true;
                        setSource("title", "Id", dtCoding, ref drpOldValue, OldValue);
                        setSource("title", "Id", dtCoding, ref drpNewValue, NewValue);
                        #endregion
                        break;
                    case 33://تاهل  -  3
                        #region MyRegion
                        dtCoding.Columns.Add("Id", typeof(int));
                        dtCoding.Columns.Add("namecoding", typeof(string));
                        dtCoding.Rows.Add(1, "مجرد");
                        dtCoding.Rows.Add(2, "متاهل");

                        drpOldValue.CssClass = "form-control";
                        drpOldValue.Enabled = false;
                        e.Row.Cells[2].Controls.Add(drpOldValue);
                        drpNewValue.Visible = true;
                        setSource("namecoding", "Id", dtCoding, ref drpOldValue, OldValue);
                        setSource("namecoding", "Id", dtCoding, ref drpNewValue, NewValue);
                        #endregion
                        break;
                    case 43://نوع بیمه
                        #region MyRegion
                        dtCoding.Columns.Add("id");
                        dtCoding.Columns.Add("title");
                        dr = dtCoding.NewRow();
                        dr["id"] = 0;
                        dr["title"] = "فاقد بیمه";
                        dtCoding.Rows.Add(dr);
                        dr = dtCoding.NewRow();
                        dr["id"] = 1;
                        dr["title"] = "مشمول بیمه";
                        dtCoding.Rows.Add(dr);
                        dr = dtCoding.NewRow();
                        dr["id"] = 2;
                        dr["title"] = "لشکری";
                        dtCoding.Rows.Add(dr);
                        dr = dtCoding.NewRow();
                        dr["id"] = 3;
                        dr["title"] = "کشوری";
                        dtCoding.Rows.Add(dr);
                        dr = dtCoding.NewRow();
                        dr["id"] = 4;
                        dr["title"] = "خدمات درمانی";
                        dtCoding.Rows.Add(dr);
                        dr = dtCoding.NewRow();
                        dr["id"] = 5;
                        dr["title"] = "سلامت";
                        dtCoding.Rows.Add(dr);
                        dr = dtCoding.NewRow();
                        dr["id"] = 6;
                        dr["title"] = "تامین اجتماعی";
                        dtCoding.Rows.Add(dr);
                        dr = dtCoding.NewRow();
                        dr["id"] = 7;
                        dr["title"] = "بازنشسته";
                        dtCoding.Rows.Add(dr);
                        dr = dtCoding.NewRow();
                        dr["id"] = 8;
                        dr["title"] = "سایر موارد";
                        dtCoding.Rows.Add(dr);

                        drpOldValue.CssClass = "form-control";
                        drpOldValue.Enabled = false;
                        e.Row.Cells[2].Controls.Add(drpOldValue);
                        drpNewValue.Visible = true;
                        setSource("title", "Id", dtCoding, ref drpOldValue, OldValue);
                        setSource("title", "Id", dtCoding, ref drpNewValue, NewValue);
                        #endregion
                        break;
                    case 44://بازنشستگی
                        #region MyRegion
                        dtCoding.Columns.Add("id");
                        dtCoding.Columns.Add("title");
                        dr = dtCoding.NewRow();
                        dr["id"] = "True";
                        dr["title"] = "بازنشسته";
                        dtCoding.Rows.Add(dr);
                        dr = dtCoding.NewRow();
                        dr["id"] = "False";
                        dr["title"] = "شاغل";
                        dtCoding.Rows.Add(dr);

                        drpOldValue.CssClass = "form-control";
                        drpOldValue.Enabled = false;
                        e.Row.Cells[2].Controls.Add(drpOldValue);
                        drpNewValue.Visible = true;
                        setSource("title", "Id", dtCoding, ref drpOldValue, OldValue);
                        setSource("title", "Id", dtCoding, ref drpNewValue, NewValue);
                        #endregion
                        break;
                    case 45://استان سکونت  -  14
                    case 47://استان کار  -  14
                        #region MyRegion
                        dtCoding = CB.GetOstan();
                        dr = dtCoding.NewRow();
                        dr["id"] = "-1";
                        dr["title"] = "انتخاب نشده";

                        drpOldValue.CssClass = "form-control";
                        drpOldValue.Enabled = false;
                        e.Row.Cells[2].Controls.Add(drpOldValue);
                        drpNewValue.Visible = true;
                        setSource("title", "Id", dtCoding, ref drpOldValue, OldValue);
                        setSource("title", "Id", dtCoding, ref drpNewValue, NewValue);
                        #endregion
                        break;
                    case 46://شهر سکونت  -  13
                    case 48://شهر کار  -  13
                        #region MyRegion
                        dtCoding = CB.GetNameCity_fcoding();
                        dr = dtCoding.NewRow();
                        dr["id"] = "-1";
                        dr["title"] = "انتخاب نشده";

                        drpOldValue.CssClass = "form-control";
                        drpOldValue.Enabled = false;
                        e.Row.Cells[2].Controls.Add(drpOldValue);
                        drpNewValue.Visible = true;
                        setSource("title", "Id", dtCoding, ref drpOldValue, OldValue);
                        setSource("title", "Id", dtCoding, ref drpNewValue, NewValue);
                        #endregion
                        break;
                    case 53://جنسیت
                        #region MyRegion
                        dtCoding.Columns.Add("id");
                        dtCoding.Columns.Add("title");
                        dr = dtCoding.NewRow();
                        dr["id"] = 0;
                        dr["title"] = "انتخاب نشده";
                        dtCoding.Rows.Add(dr);
                        dr = dtCoding.NewRow();
                        dr["id"] = "1";
                        dr["title"] = "مرد";
                        dtCoding.Rows.Add(dr);
                        dr = dtCoding.NewRow();
                        dr["id"] = "2";
                        dr["title"] = "زن";
                        dtCoding.Rows.Add(dr);

                        drpOldValue.CssClass = "form-control";
                        drpOldValue.Enabled = false;
                        e.Row.Cells[2].Controls.Add(drpOldValue);
                        drpNewValue.Visible = true;
                        setSource("title", "Id", dtCoding, ref drpOldValue, OldValue);
                        setSource("title", "Id", dtCoding, ref drpNewValue, NewValue);
                        #endregion
                        break;
                    case 54://گروه

                        #region MyRegion
                        #region get values
                        DataTable dtSelectedDanesh = new DataTable();
                        DataTable dtAllDanesh = new DataTable();
                        DataTable dtAllDep = CB.GetAllDepartman();// گرفتن دپارتمان ها با توجه به دانشکده
                        DataTable dtShowDepOld = new DataTable();
                        DataTable dtShowDepNew = new DataTable();

                        NewValue = NewValue.EndsWith(",") ? NewValue.TrimEnd(',') : NewValue;
                        OldValue = OldValue.EndsWith(",") ? OldValue.TrimEnd(',') : OldValue;

                        DataRow[] drSelectedDepOld = new DataRow[0]; if (OldValue.Length > 0) drSelectedDepOld = dtAllDep.Select("id in(" + OldValue + ")");
                        DataRow[] drSelectedDepNew = new DataRow[0]; if (NewValue.Length > 0) drSelectedDepNew = dtAllDep.Select("id in(" + NewValue + ")");
                        #endregion


                        #region set oldValue source
                        if (OldValue.Length > 0)
                            dtShowDepOld = drSelectedDepOld.CopyToDataTable();
                        CheckBoxList chkDepOld = new CheckBoxList();
                        chkDepOld.DataTextField = "namegroup";
                        chkDepOld.DataValueField = "id";
                        chkDepOld.RepeatColumns = 4;
                        chkDepOld.RepeatDirection = RepeatDirection.Horizontal;
                        chkDepOld.DataSource = dtShowDepOld;
                        chkDepOld.DataBind();
                        List<string> depListOld = new List<string>();
                        foreach (DataRow eachDr in drSelectedDepOld)
                        {
                            depListOld.Add(eachDr["namegroup"].ToString());
                        }
                        foreach (ListItem lch in chkDepOld.Items)
                        {
                            if (depListOld.Contains(lch.Text))
                            {
                                lch.Selected = true;
                            }
                        }
                        #endregion


                        #region set newValue source
                        if (drSelectedDepNew.Length > 0)
                        {
                            if (NewValue.Length > 0)
                                dtShowDepNew = drSelectedDepNew.CopyToDataTable();
                            CheckBoxList chkDepNew_View = (CheckBoxList)e.Row.FindControl("chkDepNew_View");
                            chkDepNew_View.Visible = true;
                            chkDepNew_View.DataTextField = "namegroup";
                            chkDepNew_View.DataValueField = "id";
                            chkDepNew_View.DataSource = dtShowDepNew;
                            chkDepNew_View.DataBind();

                            List<string> depListNew = new List<string>();
                            foreach (DataRow eachDr in drSelectedDepNew)
                            {
                                depListNew.Add(eachDr["namegroup"].ToString());
                            }
                            foreach (ListItem lch in chkDepNew_View.Items)
                            {
                                if (depListNew.Contains(lch.Text))
                                {
                                    lch.Selected = true;
                                }
                            }
                            chkDepNew_View.Enabled = false;
                        }
                        #endregion


                        #region add controls to grid
                        e.Row.Cells[2].Controls.Add(chkDepOld);
                        drpNewValue.Visible = false;
                        TextBox txt = (TextBox)e.Row.FindControl("txtNewValue");
                        if (txt != null)
                            txt.Visible = false;
                        Label lbl = (Label)e.Row.FindControl("lblNewValue");
                        if (lbl != null)
                            lbl.Visible = false;
                        #endregion

                        #endregion
                        break;


                }
                #endregion


                if (!IsTextBoxField(ControlTofieldId))
                {

                    Label lblNewValue = (Label)e.Row.FindControl("lblNewValue");
                    lblNewValue.Visible = false;
                    drpNewValue = (DropDownList)e.Row.FindControl("drpNewValue");
                    drpNewValue.Visible = true;

                }
            }
        }

        private bool IsDateTimeField(int changeFieldTypeId)
        {
            if (changeFieldTypeId == 37 || changeFieldTypeId == 38)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void setSource(string DataTextField, string DataValueField, DataTable source, ref DropDownList ddl, string value)
        {
            ddl.DataTextField = DataTextField;
            ddl.DataValueField = DataValueField;
            ddl.DataSource = source;
            ddl.DataBind();
            if (value != "" && !string.IsNullOrWhiteSpace(value))
            {
                ddl.SelectedValue = value;
            }
        }

        protected void btnRefreshGrid_Click(object sender, EventArgs e)
        {
            LoadRequestGrid();
        }

        private void BindData(int reqId, int reqType, int reqStatus)
        {
            if (reqType == (int)RequestTypeId.EditHokm)
            {
                ProfessorHokmDTO oNewHokm = ProfReqBuss.GetNewHokmInfo(reqId);

                if (oNewHokm.Code_Ostad == 0)
                {
                    rblIsHeiat.SelectedValue = 2.ToString();
                    DivInfoHokm.Visible = false;
                }
                else
                {
                    DivInfoHokm.Visible = true;
                    DivimgNewHokm.Visible = true;
                    DataTable dtUniName = CB.GetNameUni_fcoding();
                    for (int i = 0; i <= dtUniName.Rows.Count - 1; i++)
                    {
                        dtUniName.Rows[i][0] = dtUniName.Rows[i][0].ToString().Replace("ي", "ی");
                    }
                    drpPastUni.DataSource = dtUniName;
                    drpPastUni.DataTextField = "namecoding";
                    drpPastUni.DataValueField = "ID";
                    drpPastUni.DataBind();

                    drpPastUni.SelectedValue = oNewHokm.Uni_Khedmat.ToString();
                    hdnfHokmId.Value = oNewHokm.HokmId.ToString();
                    txtCodeOstad.Text = oNewHokm.Code_Ostad.ToString();
                    txtHokmNumber.Text = oNewHokm.Number_Hokm.ToString();
                    txtDateEjraHokm.Text = oNewHokm.Date_RunHokm;

                    txtDateSodoorHokm.Text = oNewHokm.Date_Hokm;
                    txtMablaghHokm.Text = oNewHokm.MablaghHokm.ToString();
                    txtPaye.Text = oNewHokm.Payeh.ToString();
                    if (oNewHokm.HokmUrl != null)
                    {
                        string hokmUrl = oNewHokm.HokmUrl.ToString();
                        int subIndex = hokmUrl.IndexOf(substringScanFile) +
                                       substringScanFile.Length + 1;
                        if (hokmUrl.Length > 0)
                            imgNewHokm.HRef = "../OpenScanImage.aspx?o=" + oNewHokm.Code_Ostad.ToString() + "&f=" +
                                              hokmUrl.Substring(subIndex);
                        else
                            imgNewHokm.Visible = false;
                    }
                    else
                        imgNewHokm.Visible = false;
                    drpHireType.SelectedValue = oNewHokm.Type_Estekhdam.ToString();
                    if (oNewHokm.Martabeh > 0 && oNewHokm.Martabeh<8)
                    {
                        if (drpMartabe.Items.FindByValue(oNewHokm.Martabeh.ToString()) != null)
                            drpMartabe.SelectedValue = oNewHokm.Martabeh.ToString();
                        rblIsHeiat.SelectedValue = "1";
                    }
                    else
                    {
                        rblIsHeiat.SelectedValue = "2";
                    }
                    if (oNewHokm.Nahveh_Hamk != 0)
                        rdblHireType.SelectedValue = oNewHokm.Nahveh_Hamk.ToString();
                    else
                        rdblHireType.Visible = false;
                    dvChangeList.Visible = false;

                    if (oNewHokm.BoundHour != null)
                        chkBoundHour.Checked = Convert.ToBoolean(oNewHokm.BoundHour);
                    ddlPastUniType.SelectedValue = oNewHokm.Uni_KhedmatType > 0
                        ? oNewHokm.Uni_KhedmatType.ToString()
                        : "0";
                }
                dvNewHokm.Visible = true;
                dvPersonalInfo.Visible = false;
                dvChangeList.Visible = false;
            }
            else
            {
                dvChangeList.Visible = true;
                dvPersonalInfo.Visible = false;
                dvNewHokm.Visible = false;
                DataTable oChangeList = ProfReqBuss.GetChangeListByReqId(reqId);
                foreach (DataRow row in oChangeList.Rows)
                {

                    foreach (DataColumn col in oChangeList.Columns)
                    {
                        if (row["Description"].ToString() == "نحوه همکاری")
                        {
                            if (row["OldValue"].ToString() == "1")
                            {
                                row["OldValue"] = "همکاری برای تدریس";
                            }
                            if (row["OldValue"].ToString() == "2")
                            {
                                row["OldValue"] = "همکاری برای مشاوره یا راهنمایی پروژه";
                            }
                            if (row["OldValue"].ToString() == "3")
                            {
                                row["OldValue"] = "همکاری برای تدریس و مشاوره یا راهنمایی پروژه";
                            }
                            if (row["NewValue"].ToString() == "1")
                            {
                                row["NewValue"] = "همکاری برای تدریس";
                            }
                            if (row["NewValue"].ToString() == "2")
                            {
                                row["NewValue"] = "همکاری برای مشاوره یا راهنمایی پروژه";
                            }
                            if (row["NewValue"].ToString() == "3")
                            {
                                row["NewValue"] = "همکاری برای تدریس و مشاوره یا راهنمایی پروژه";
                            }
                            if (row["NewValue"].ToString() == "0")
                            {
                                row["NewValue"] = "مشخص نشده";
                            }
                            if (row["OldValue"].ToString() == "0")
                            {
                                row["OldValue"] = "مشخص نشده";
                            }
                        }

                    }
                }
                if (reqType == (int)RequestTypeId.EditPersonalInfo)
                {
                    int docStatus = -1;
                    switch (reqStatus)
                    {
                        case 5://رد شده
                            docStatus = 2;
                            break;
                        case 6://در حال بررسی
                            docStatus = 0;
                            break;
                        case 7://تایید شده
                            docStatus = 1;
                            break;
                    }

                    dvNewHokm.Visible = false;
                    dvPersonalInfo.Visible = true;
                    dvChangeList.Visible = false;
                    DataRow[] drTemp;
                    dvIdd.Visible = false;
                    dvMadrak.Visible = false;
                    dvBime.Visible = false;
                    dvInf.Visible = false;


                    drTemp = oChangeList.Select("id1 in(49,50,51,53)");
                    if (drTemp.Length > 0)
                    {
                        dvIdd.Visible = true;
                        DataTable dtIdd = drTemp.CopyToDataTable();
                        grdIDD.DataSource = dtIdd;
                        grdIDD.DataBind();
                    }

                    addScanUrlToButton(btnScanIdd, 1, docStatus, dvIdd, reqId);
                    addScanUrlToButton(btnScanPersonelly, 10, docStatus, dvIdd, reqId);
                    addScanUrlToButton(btnScanMeli, 5, docStatus, dvIdd, reqId);
                    drTemp = oChangeList.Select("id1 in(7,9,14,15,17,19)");
                    if (drTemp.Length > 0)
                    {
                        dvMadrak.Visible = true;
                        DataTable dtMadrak = drTemp.CopyToDataTable();
                        grdMadrak.DataSource = dtMadrak;
                        grdMadrak.DataBind();
                    }

                    addScanUrlToButton(btnScanMadrak, 4, docStatus, dvMadrak, reqId);
                    addScanUrlToButton(btnScanArzesh, 14, docStatus, dvMadrak, reqId);


                    drTemp = oChangeList.Select("id1 in(43,40,44)");
                    if (drTemp.Length > 0)
                    {
                        dvBime.Visible = true;
                        DataTable dtBime = drTemp.CopyToDataTable();
                        grdBime.DataSource = dtBime;
                        grdBime.DataBind();
                    }


                    addScanUrlToButton(btnScanBime, 6, docStatus, dvBime, reqId);
                    addScanUrlToButton(btnScanBazneshaste, 18, docStatus, dvBime, reqId);



                    drTemp = oChangeList.Select("id1 in(30,31,32,33)");
                    if (drTemp.Length > 0)
                    {
                        dvInf.Visible = true;
                        DataTable dtOther = drTemp.CopyToDataTable();
                        grdInf.DataSource = dtOther;
                        grdInf.DataBind();

                    }

                    addScanUrlToButton(btnScanInf, 7, docStatus, dvInf, reqId);

                }
                else
                {
                    grdChangeList.Visible = true;
                    dvChangeList.Visible = true;
                    grdChangeList.DataSource = oChangeList;
                    grdChangeList.DataBind();
                }

                LoadChangeListGridBaseInfo(reqType);

            }



        }

        private void LoadChangeListGridBaseInfo(int reqType)
        {
            switch (reqType)
            {
                case (int)RequestTypeId.EditPersonalInfo:

                    break;
                case (int)RequestTypeId.EditContactInfo:

                    break;
                case (int)RequestTypeId.EditHokm:

                    break;
                case (int)RequestTypeId.EditCooperation:

                    break;
                default:
                    break;
            }

        }

        private bool IsTextBoxField(int controlToFieldID)
        {
            switch (controlToFieldID)
            {
                case 14:    //	سال اخذ مدرک تحصیلی
                case 21:    //	پایه
                case 23:    //	تلفن منزل
                case 24:    //	تلفن محل کار
                case 25:    //	آدرس منزل
                case 26:    //	آدرس محل کار
                case 27:    //	پست الکترونیک
                case 28:    //	موبایل
                case 29:    //	کد پستی
                case 31:    //	شماره سیبا
                case 32:    //	سنوات تدریس
                case 37:    //	تاریخ صدور حکم
                case 38:    //	تاریخ اجرای حکم
                case 39:    //	شماره حکم
                case 40:    //	شماره بیمه
                case 41:    //	مبلغ حکم
                case 49:    //	نام پدر
                case 50:    //	شماره شناسنامه
                case 51:    //	سال تولد
                    return true;
                case 7: //	مدرک تحصیلی
                case 9: //	رشته تحصیلی
                case 11:    //	مرتبه علمی
                case 15:    //	کشور محل تحصیل
                case 17:    //	نوع دانشگاه محل تحصیل
                case 19:    //	دانشگاه محل تحصیل
                case 30:    //	وضیعت نظام وظیفه
                case 33:    //	وضعیت تاهل
                case 34:    //	نوع استخدام دانشگاه مبدا
                case 35:    //	دانشگاه محل خدمت
                case 36:    //	نحوه همکاری در دانشگاه مبدا
                case 42:    //	نحوه همکاری
                case 43:    //	نوع بیمه 
                case 44:    //	وضعیت بازنشستگی
                case 45:    //	استان محل سکونت
                case 46:    //	شهر محل سکونت
                case 47:    //	استان محل کار
                case 48:    //	شهر محل کار
                case 52:    //	نوع دانشگاه محل خدمت
                case 53:    //	جنسیت
                case 54:    //	گروه آموزشی
                    return false;
                default:
                    return true;

            }
        }

        protected void grdIDD_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            grdChangeList_RowDataBound(sender, e);
        }

        protected void grdMadrak_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            grdChangeList_RowDataBound(sender, e);
        }

        protected void grdBime_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            grdChangeList_RowDataBound(sender, e);
        }

        protected void grdInf_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            grdChangeList_RowDataBound(sender, e);
        }

        private void addScanUrlToButton(System.Web.UI.HtmlControls.HtmlAnchor btn, int docType, int docStatus, System.Web.UI.HtmlControls.HtmlGenericControl panel, int reqID)
        {
            int codeOstad = Convert.ToInt32(Session[sessionNames.userID_StudentOstad]);
            DataTable dt = ProfReqBuss.getProfessorRequests_Scan(codeOstad, docStatus, reqID);
            if (dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select("doctype=" + docType);
                if (dr.Length == 1)
                {
                    try
                    {
                        if (dr[0]["scanUrl"] == DBNull.Value)
                            return;
                        panel.Visible = true;
                        btn.Visible = true;
                        if (dr[0]["scanImage"] == DBNull.Value)
                            return;
                        string path = "";
                        if (dr[0]["scanUrl"].ToString().StartsWith("~"))
                        {
                            path = Server.MapPath(dr[0]["scanUrl"].ToString());
                        }
                        else
                            path = dr[0]["scanUrl"].ToString();

                        if (!Directory.Exists(path.Substring(0, path.LastIndexOf("\\"))))
                        {
                            Directory.CreateDirectory(path.Substring(0, path.LastIndexOf("\\")));
                        }
                        if (!File.Exists(path))
                        {
                            var stream = new MemoryStream((byte[])dr[0]["scanImage"]);
                            var image = System.Drawing.Image.FromStream(stream);
                            image.Save(path);

                        }
                        btn.HRef = "../OpenScanImage.aspx" + "?o=" + codeOstad + "&f=" + dr[0]["scanUrl"].ToString().Substring(dr[0]["scanUrl"].ToString().IndexOf(substringScanFile) + substringScanFile.Length + 1);
                    }
                    catch (Exception ex) { btn.Visible = false; }
                }

            }
        }


    }
}