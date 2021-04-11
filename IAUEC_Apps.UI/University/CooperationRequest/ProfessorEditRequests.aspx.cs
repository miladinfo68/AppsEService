using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Faculty;
using IAUEC_Apps.Business.university.Request;
using IAUEC_Apps.DTO.University.Request;
using IAUEC_Apps.UI.University.CooperationRequest.Teachers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Configuration;
using System.Text.RegularExpressions;

namespace IAUEC_Apps.UI.University.CooperationRequest
{
    public partial class ProfessorEditRequests : System.Web.UI.Page
    {
        ProfessorRequestBusiness ProfReqBuss = new ProfessorRequestBusiness();
        FacultyReportsBusiness FRB = new FacultyReportsBusiness();
        CommonBusiness CB = new CommonBusiness();
        const string requestId = "reqId", requestType = "reqType", requestStatus = "reqStatus", hrId = "hrId", codeOstad = "codeOstad",substringScanFile= "ScanMadarek";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
                Response.Redirect("~/CommonUI/LoginRequestCMS.aspx");


        }

        #region Show Request
        protected void btnShowRequest_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                showRequest();
            }
        }

        protected void btnSearchProf_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                List<ProfessorEditRequestDTO> oRequestList = new List<ProfessorEditRequestDTO>();
                int code_Ostad = Convert.ToInt32(txtSearchByProfCode.Text);
                string state = rdblRequestState.SelectedValue;
                oRequestList = ProfReqBuss.GetProfessorRequestsByIdAndStatus(code_Ostad, state);
                BindRequestGrid(oRequestList, state.ToString());
                ViewState.Add("BindType", "Single");
            }
        }

        private void BindRequestGrid(List<ProfessorEditRequestDTO> oRequestList, string reqStatus)
        {
            grdRequestList.DataSource = oRequestList;
            grdRequestList.DataBind();
        }

        protected void grdRequestList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ProfessorEditRequestDTO drv = (ProfessorEditRequestDTO)e.Row.DataItem;
                int reqType = drv.RequestTypeID;
                string strReqType = null;
                switch (reqType)
                {
                    case (int)RequestTypeId.EditPersonalInfo:
                        strReqType = "ویرایش اطلاعات فردی";
                        break;
                    case (int)RequestTypeId.EditHokm:
                        strReqType = "ویرایش حکم کارگزینی";
                        break;
                    case (int)RequestTypeId.EditContactInfo:
                        strReqType = "ویرایش اطلاعات تماس";
                        break;
                    case (int)RequestTypeId.EditCooperation:
                        strReqType = "ویرایش نحوه همکاری";
                        break;
                    default:
                        break;
                }

                if (strReqType != null)
                {
                    e.Row.Cells[4].Text = strReqType;
                }

                int reqStatus = drv.RequestLogID;
                string strReqStatus = null;
                switch (reqStatus)
                {
                    case (int)RequestLogId.reviewed:
                        strReqStatus = "بررسی شده";
                        break;
                    case (int)RequestLogId.approved:
                        strReqStatus = "تایید شده";
                        break;
                    case (int)RequestLogId.denied:
                        strReqStatus = "رد شده";
                        break;
                    case (int)RequestLogId.submitted:
                        strReqStatus = "ثبت شده";
                        break;
                    default:
                        break;
                }

                if (strReqStatus != null)
                {
                    e.Row.Cells[5].Text = strReqStatus;
                }
            }
        }

        protected void grdRequestList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int reqId = Convert.ToInt32(e.CommandArgument);
            HiddenField hdnfReqType = (HiddenField)gvr.FindControl("hdnfReqType");
            HiddenField hdnfReqLog = (HiddenField)gvr.FindControl("hdnfReqLog");
            HiddenField hdnfInfPeo = (HiddenField)gvr.FindControl("hdnfInfPeo");
            HiddenField hdn_fullName = (HiddenField)gvr.FindControl("hdnfName");
            ViewState[hrId] = hdnfInfPeo.Value;
            int reqType = Convert.ToInt32(hdnfReqType.Value);
            int reqStatus = Convert.ToInt32(hdnfReqLog.Value);
            if (e.CommandName == "show")
            {
                ViewState.Add(requestId, reqId);
                ViewState.Add(requestType, reqType);
                ViewState.Add(requestStatus, reqStatus);

                int code_ostad = Convert.ToInt32(gvr.Cells[1].Text);
                ViewState[codeOstad] = code_ostad;
                grdIDD.EditIndex = -1;
                grdMadrak.EditIndex = -1;
                grdBime.EditIndex = -1;
                grdInf.EditIndex = -1;
                grdChangeList.EditIndex = -1;

                BindData();

                btnShowDetailInfo.HRef = "ShowDetailInfo.aspx?ID=" + hdnfInfPeo.Value.ToString();

                HiddenField hdnfUrl = (HiddenField)gvr.FindControl("hdnfURL");


                hReq.InnerText = drpRequestType.Items.FindByValue(reqType.ToString()).Text;
                hCode.InnerText = ViewState[requestId].ToString();
                hName.InnerText = hdn_fullName.Value;

                string scrp = "function f(){var win = $find(\"" + RadWindow1.ClientID + "\"); win.show(); if (!win.isClosed()) {win.center();} Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
            }
        }

        private void showRequest()
        {
            List<ProfessorEditRequestDTO> oRequestList = new List<ProfessorEditRequestDTO>();
            string reqType = drpRequestType.SelectedValue;
            string reqStatus = rdblStatus.SelectedValue;
            oRequestList = ProfReqBuss.GetRequestByTypeAndStatus(reqType, reqStatus);
            BindRequestGrid(oRequestList, reqStatus);
            ViewState.Add("BindType", "Group");
        }

        #endregion ShowRequest


        #region bind Data
        private void BindData()
        {
            int reqId = Convert.ToInt32(ViewState[requestId]), reqType = Convert.ToInt32(ViewState[requestType]), reqStatus = Convert.ToInt32(ViewState[requestStatus]);
            dvChangeList.Visible = false;
            dvNewHokm.Visible = false;
            dvPersonalInfo.Visible = false;
            DataTable oChangeList;
            btnSendToPending.Visible = reqStatus == (int)RequestLogId.denied;
            grdChangeList.DataSource = null;
            grdChangeList.DataBind();


            switch ((RequestTypeId)reqType)
            {
                case RequestTypeId.EditPersonalInfo:
                    #region MyRegion
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
                    dvPersonalInfo.Visible = true;
                    dvIdd.Visible = false;
                    dvMadrak.Visible = false;
                    dvBime.Visible = false;
                    dvInf.Visible = false;
                    btnScanArzesh.Visible = false;
                    btnScanBazneshaste.Visible = false;
                    btnScanBime.Visible = false;
                    btnScanIdd.Visible = false;
                    btnScanInf.Visible = false;
                    btnScanMadrak.Visible = false;

                    grdBime.DataSource = null;
                    grdIDD.DataSource = null;
                    grdInf.DataSource = null;
                    grdMadrak.DataSource = null;
                    grdBime.DataBind();
                    grdIDD.DataBind();
                    grdInf.DataBind();
                    grdMadrak.DataBind();



                    oChangeList = ProfReqBuss.GetChangeListByReqId(reqId);
                    DataRow[] drTemp;
                    drTemp = oChangeList.Select("id1 in(49,50,51)");
                    if (drTemp.Length > 0)
                    {
                        dvIdd.Visible = true;
                        DataTable dtIdd = drTemp.CopyToDataTable();
                        grdIDD.DataSource = dtIdd;
                        grdIDD.DataBind();
                    }
                    else
                    {
                        grdIDD.DataSource = null;
                        grdIDD.DataBind();
                    }
                    addScanUrlToButton(btnScanIdd, 1, docStatus, dvIdd, reqId);
                    addScanUrlToButton(btnScanPersonelly, 10, docStatus, dvIdd, reqId);
                    addScanUrlToButton(btnScanMelli, 5, docStatus, dvIdd, reqId);
                    drTemp = oChangeList.Select("id1 in(7,9,14,15,17,19)");
                    if (drTemp.Length > 0)
                    {
                        dvMadrak.Visible = true;
                        DataTable dtMadrak = drTemp.CopyToDataTable();
                        grdMadrak.DataSource = dtMadrak;
                        grdMadrak.DataBind();
                    }
                    else
                    {
                        grdMadrak.DataSource = null;
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
                    else
                    {
                        grdBime.DataSource = null;
                        grdBime.DataBind();
                    }
                    addScanUrlToButton(btnScanBime, 6, docStatus, dvBime, reqId);
                    addScanUrlToButton(btnScanBazneshaste, 18, docStatus, dvBime, reqId);
                    drTemp = oChangeList.Select("id1 in(30,31,32,33,53)");
                    if (drTemp.Length > 0)
                    {
                        dvInf.Visible = true;
                        DataTable dtOther = drTemp.CopyToDataTable();
                        grdInf.DataSource = dtOther;
                        grdInf.DataBind();
                    }
                    else
                    {
                        grdInf.DataSource = null;
                        grdInf.DataBind();
                    }
                    addScanUrlToButton(btnScanInf, 7, docStatus, dvInf, reqId);



                    if (reqStatus != (int)RequestLogId.submitted)
                    {
                        ((DataControlField)grdChangeList.Columns.Cast<DataControlField>()
                                                                .Where(fld => fld.HeaderText == "عملیات")
                                                                .SingleOrDefault()).Visible = false;
                    }
                    #endregion
                    break;


                case RequestTypeId.EditHokm:
                    dvNewHokm.Visible = true;
                    NewHokm_DataBound(reqId);
                    break;


                case RequestTypeId.EditContactInfo:
                case RequestTypeId.EditCooperation:
                    #region MyRegion
                    oChangeList = ProfReqBuss.GetChangeListByReqId(reqId);
                    grdChangeList.DataSource = oChangeList;
                    grdChangeList.DataBind();

                    if (reqStatus != (int)RequestLogId.submitted)
                    {
                        ((DataControlField)grdChangeList.Columns.Cast<DataControlField>()
                                                                .Where(fld => fld.HeaderText == "عملیات")
                                                                .SingleOrDefault()).Visible = false;
                    }
                    grdChangeList.Visible = true;
                    dvChangeList.Visible = true;
                    #endregion
                    break;
            }

            //------------------------------------------------------------------------------------------
            //------------------------------------------------------------------------------------------
            //------------------------------------------------------------------------------------------
            if (reqStatus != 6)
            {
                btnApprove.Visible = false;
                btnDeny.Visible = false;
            }
            else
            {
                btnApprove.Visible = true;
                btnDeny.Visible = true;

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

        protected void grdChangeList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                grdChangeList_Bound(e);
            }
        }

        private void grdChangeList_Bound(GridViewRowEventArgs e)
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


            #region add source to controls
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
                    dtCoding = CB.GetCodingByTypeId(codingTypeId).Select("id<56").CopyToDataTable();

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
                case 42://نحوه همکاری
                    #region MyRegion
                    dtCoding.Columns.Add("value");
                    dtCoding.Columns.Add("text");
                    dtCoding.Rows.Add(0, "مشخص نشده");
                    dtCoding.Rows.Add(1, "همکاری برای تدریس");
                    dtCoding.Rows.Add(2, "همکاری برای مشاوره یا راهنمایی پروژه");
                    dtCoding.Rows.Add(3, "همکاری برای تدریس و مشاوره یا راهنمایی پروژه");

                    drpOldValue.CssClass = "form-control";
                    drpOldValue.Enabled = false;
                    e.Row.Cells[2].Controls.Add(drpOldValue);
                    drpNewValue.Visible = true;
                    setSource("text", "value", dtCoding, ref drpOldValue, OldValue);
                    setSource("text", "value", dtCoding, ref drpNewValue, NewValue);
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
                    DataView defaultViewOstan = dtCoding.DefaultView;
                    defaultViewOstan.Sort = "title";
                    dtCoding = defaultViewOstan.ToTable();
                    dr = dtCoding.NewRow();
                    dr["id"] = "-1";
                    dr["title"] = "انتخاب نشده";
                    dtCoding.Rows.Add(dr);
                    drpOldValue.CssClass = "form-control";
                    drpOldValue.Enabled = false;
                    e.Row.Cells[2].Controls.Add(drpOldValue);
                    drpNewValue.Visible = true;
                    OldValue = OldValue == "" ? "-1" : OldValue;
                    NewValue = NewValue == "" ? "-1" : NewValue;
                    setSource("title", "Id", dtCoding, ref drpOldValue, OldValue);
                    setSource("title", "Id", dtCoding, ref drpNewValue, NewValue);
                    #endregion
                    break;
                case 46://شهر سکونت  -  13
                case 48://شهر کار  -  13
                    #region MyRegion
                    //dtCoding = FRB.GetShahrestan();
                    dtCoding = CB.getShahrestan(getOstanCode(ControlTofieldId));
                    DataView defaultViewShahr = dtCoding.DefaultView;
                    defaultViewShahr.Sort = "Title";
                    dtCoding = defaultViewShahr.ToTable();
                    dr = dtCoding.NewRow();
                    dr["id"] = "-1";
                    dr["title"] = "انتخاب نشده";
                    dtCoding.Rows.Add(dr);
                    drpOldValue.CssClass = "form-control";
                    drpOldValue.Enabled = false;
                    e.Row.Cells[2].Controls.Add(drpOldValue);
                    drpNewValue.Visible = true;
                    OldValue = OldValue == "" ? "-1" : OldValue;
                    NewValue = NewValue == "" ? "-1" : NewValue;
                    setSource("title", "Id", dtCoding, ref drpOldValue, OldValue);
                    setSource("title", "Id", dtCoding, ref drpNewValue, NewValue);
                    if (drpNewValue.Items.FindByValue(NewValue) == null)
                        NewValue = "-1";
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

                    if ((e.Row.RowState & DataControlRowState.Edit) != 0)//in edit mode
                    {

                        dtShowDepNew = dtAllDep.Copy();
                        dtAllDanesh = CB.SelectAllDaneshkade();
                        dtSelectedDanesh = FRB.GetDaneshkadeByGroup("idgroup in(" + NewValue + ")");
                        CheckBoxList chkDepNew = (CheckBoxList)e.Row.FindControl("chkDepNew");
                        chkDepNew.Visible = true;
                        chkDepNew.DataTextField = "namegroup";
                        chkDepNew.DataValueField = "id";
                        chkDepNew.DataSource = dtShowDepNew;
                        chkDepNew.DataBind();

                        List<string> depListNew = new List<string>();
                        foreach (DataRow eachDr in drSelectedDepNew)
                        {
                            depListNew.Add(eachDr["namegroup"].ToString());
                        }
                        foreach (ListItem lch in chkDepNew.Items)
                        {
                            if (depListNew.Contains(lch.Text))
                            {
                                lch.Selected = true;
                            }
                        }

                    }
                    else//in view mode
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

            if ((e.Row.RowState & DataControlRowState.Edit) != 0)
            {

                if (ControlTofieldId != 53 && ControlTofieldId != 54)
                {
                    if (IsTextBoxField(ControlTofieldId)/* && (ControlTofieldId != 17 && ControlTofieldId != 43 && ControlTofieldId != 44)*/)
                    {
                        if (IsDateTimeField(ControlTofieldId))
                        {
                            TextBox txtNewValue = (TextBox)e.Row.FindControl("txtNewValue");
                            txtNewValue.Attributes.Add("class", "pcal");
                            RegularExpressionValidator vldDateTime = (RegularExpressionValidator)e.Row.FindControl("vldDateTime");
                            vldDateTime.Enabled = true;
                            RegularExpressionValidator vldDateRequired = (RegularExpressionValidator)e.Row.FindControl("vldDateRequired");
                            vldDateRequired.Enabled = true;
                        }
                    }
                    else
                    {
                        TextBox txtNewValue = (TextBox)e.Row.FindControl("txtNewValue");
                        txtNewValue.Visible = false;
                        drpNewValue = (DropDownList)e.Row.FindControl("drpNewValue");
                        drpNewValue.Visible = true;
                    }
                }
            }
            else if (ControlTofieldId != 54)
            {

                if (!IsTextBoxField(ControlTofieldId))
                {

                    Label lblNewValue = (Label)e.Row.FindControl("lblNewValue");
                    lblNewValue.Visible = false;
                    drpNewValue = (DropDownList)e.Row.FindControl("drpNewValue");
                    drpNewValue.Visible = true;

                }
                else
                {
                    Label lblNewValue = (Label)e.Row.FindControl("lblNewValue");
                    lblNewValue.Visible = true;
                    drpNewValue = (DropDownList)e.Row.FindControl("drpNewValue");
                    drpNewValue.Visible = false;
                }


            }
        }

        private void NewHokm_DataBound(int requestId)
        {
            ProfessorHokmDTO oNewHokm = ProfReqBuss.GetNewHokmInfo(requestId);
            dvNewHokm.Visible = true;

            if (oNewHokm.Martabeh < 0 || oNewHokm.Code_Ostad == 0)
            {
                DivisHeiat.Visible = true;
                rblIsHeiat.Enabled = false;
                rblIsHeiat.SelectedValue = "2";
                DivChangeHokm.Visible = false;
            }
            else
            {
                DivisHeiat.Visible = false;
                rblIsHeiat.SelectedValue = "1";
                DivChangeHokm.Visible = true;

                DataTable dtUniName = CB.GetNameUni_fcoding();
                for (int i = 0; i <= dtUniName.Rows.Count - 1; i++)
                {
                    dtUniName.Rows[i][0] = dtUniName.Rows[i][0].ToString().Replace("ي", "ی");
                }
                drpPastUni.DataSource = dtUniName;
                drpPastUni.DataTextField = "namecoding";
                drpPastUni.DataValueField = "ID";
                drpPastUni.DataBind();
                drpPastUni.Items.Add(new Telerik.Web.UI.RadComboBoxItem("جستجو و انتخاب کنید", "0"));
                drpPastUni.SelectedValue = oNewHokm.Uni_Khedmat.ToString();

                if (oNewHokm.HokmId == 0)
                {
                    return;
                }

                hdnfHokmId.Value = oNewHokm.HokmId.ToString();
                txtCodeOstad.Text = oNewHokm.Code_Ostad.ToString();
                txtHokmNumber.Text = oNewHokm.Number_Hokm.ToString();
                txtDateEjraHokm.Text = oNewHokm.Date_RunHokm;
                txtDateRunHokmNew.Text = oNewHokm.Date_RunHokm;
                txtDateSodoorHokm.Text = oNewHokm.Date_Hokm;
                txtMablaghHokm.Text = oNewHokm.MablaghHokm.ToString();
                txtPaye.Text = oNewHokm.Payeh.ToString();
                ddlPastUniType.SelectedValue = oNewHokm.Uni_KhedmatType.ToString();
                imgNewHokm.Visible = false;
                string hokmUrl = oNewHokm.HokmUrl.ToString();
                if (hokmUrl.Length > 0)
                {
                    string plusSign = "(plusSign)";
                    string percentSign = "(percentSign)";
                    hokmUrl = hokmUrl.Replace("+", plusSign);
                    hokmUrl = hokmUrl.Replace("%", percentSign);
                    int subIndex = hokmUrl.IndexOf(substringScanFile) + substringScanFile.Length + 1;
                    imgNewHokm.HRef = "OpenScanImage.aspx?o=" + oNewHokm.Code_Ostad.ToString() + "&f=" + hokmUrl.Substring(subIndex);
                    imgNewHokm.Visible = true;
                }
                if(oNewHokm.Type_Estekhdam!=0)
                drpHireType.SelectedValue = oNewHokm.Type_Estekhdam.ToString();
                if (oNewHokm.Martabeh > 0)
                {
                    if (drpMartabe.Items.FindByValue(oNewHokm.Martabeh.ToString()) != null)
                        drpMartabe.SelectedValue = oNewHokm.Martabeh.ToString();

                }
                rdblHireType.SelectedValue = oNewHokm.Nahveh_Hamk.ToString();
                dvChangeList.Visible = false;
                dvNewHokm.Visible = true;
                chkBoundHour.Checked = oNewHokm.BoundHour;
            }

        }

        #endregion bind Data


        #region set source

        private void setSource(string DataTextField, string DataValueField, DataTable source, ref DropDownList ddl, string value)
        {
            ddl.DataTextField = DataTextField;
            ddl.DataValueField = DataValueField;
            ddl.DataSource = source;
            ddl.DataBind();
            if (value != "" && !string.IsNullOrWhiteSpace(value) && ddl.Items.FindByValue(value) != null)
            {
                ddl.SelectedValue = value;
            }
        }
        private DataTable dropDownSource(int id)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("title");
            DataRow dr;
            switch (id)
            {
                case 17://نوع دانشگاه
                    dr = dt.NewRow();
                    dr["id"] = 1;
                    dr["title"] = "دولتی";
                    dt.Rows.Add(dr); dr = dt.NewRow();
                    dr["id"] = 2;
                    dr["title"] = "آزاد";
                    dt.Rows.Add(dr); dr = dt.NewRow();
                    dr["id"] = 3;
                    dr["title"] = "حوزه";
                    dt.Rows.Add(dr); dr = dt.NewRow();
                    dr["id"] = 4;
                    dr["title"] = "خارج از کشور";
                    dt.Rows.Add(dr); dr = dt.NewRow();
                    dr["id"] = 5;
                    dr["title"] = "سایر";
                    dt.Rows.Add(dr);
                    break;
                case 43://نوع بیمه
                    dr = dt.NewRow();
                    dr["id"] = 1;
                    dr["title"] = "مشمول بیمه";
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    dr["id"] = 2;
                    dr["title"] = "لشکری";
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    dr["id"] = 3;
                    dr["title"] = "کشوری";
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    dr["id"] = 4;
                    dr["title"] = "خدمات درمانی";
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    dr["id"] = 5;
                    dr["title"] = "سلامت";
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    dr["id"] = 6;
                    dr["title"] = "تامین اجتماعی";
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    dr["id"] = 7;
                    dr["title"] = "بازنشسته";
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    dr["id"] = 8;
                    dr["title"] = "سایر موارد";
                    dt.Rows.Add(dr);
                    break;
                case 44://بازنشستگی
                    dr = dt.NewRow();
                    dr["id"] = "True";
                    dr["title"] = "بله";
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    dr["id"] = "False";
                    dr["title"] = "خیر";
                    break;
            }
            return dt;
        }

        private void addScanUrlToButton(System.Web.UI.HtmlControls.HtmlAnchor btn, int docType, int docStatus, System.Web.UI.HtmlControls.HtmlGenericControl panel, int reqID)
        {
            btn.Visible = false;
            btn.HRef = "";
            int code = Convert.ToInt32(ViewState[codeOstad]);
            DataTable dt = ProfReqBuss.getProfessorRequests_Scan(code, docStatus, reqID);
            if (dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select("doctype=" + docType);
                if (dr.Length == 1)
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
                    btn.HRef = "OpenScanImage.aspx?o=" + code + "&f=" + path.Substring(path.IndexOf(substringScanFile) + substringScanFile.Length + 1);

                    //}
                }
            }
        }


        #endregion set source


        #region change field 
        protected void grdChangeList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            (sender as GridView).EditIndex = e.NewEditIndex;

            BindData();
        }

        protected void grdChangeList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            (sender as GridView).EditIndex = -1;

            BindData();
        }

        protected void grdChangeList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (IsValid)
            {
                string oldValue_Log = "", newValue_Log = "", fieldName = "", oldValue = "";
                DataTable dtRequestChange = ProfReqBuss.GetChangeListByReqId(Convert.ToInt32(ViewState[requestId]));

                int changeId = (int)e.Keys["Id"];
                DataRow[] dr = dtRequestChange.Select("id=" + changeId);
                if (dr.Length == 1)
                    oldValue = dr[0]["newValue"].ToString();
                GridViewRow row = (GridViewRow)(sender as GridView).Rows[e.RowIndex];
                HiddenField hdnfCodingTypeId = (HiddenField)row.FindControl("hdnfCodingTypeId");
                HiddenField hdnfControlToFieldId = (HiddenField)row.FindControl("hdnfControlTofieldId");
                int codingTypeId = Convert.ToInt32(hdnfCodingTypeId.Value);
                int controlToFieldId = Convert.ToInt32(hdnfControlToFieldId.Value);
                string NewValue = null;
                (sender as GridView).EditIndex = -1;
                fieldName = row.Cells[1].Text;

                if (IsTextBoxField(controlToFieldId))
                {
                    TextBox txtNewValue = (TextBox)row.FindControl("txtNewValue");

                    if (!string.IsNullOrWhiteSpace(txtNewValue.Text))
                    {
                        NewValue = txtNewValue.Text;
                        newValue_Log = NewValue;
                        oldValue_Log = oldValue;
                    }
                }
                else
                {
                    if (controlToFieldId == 54)
                    {
                        CheckBoxList chk = (CheckBoxList)row.FindControl("chkDepNew");
                        if (chk.Items.Count != 0)
                        {
                            List<string> lsgroups =
                            chk.Items.Cast<ListItem>()
                                .Where(li => li.Selected)
                                .Select(li => li.Value)
                                .ToList();
                            List<string> lsgroups_Name =
                            chk.Items.Cast<ListItem>()
                                .Where(li => li.Selected)
                                .Select(li => li.Text)
                                .ToList();
                            NewValue = "";
                            foreach (string item in lsgroups)
                            {
                                NewValue += item + ",";
                            }
                            foreach (string item in lsgroups_Name)
                            {
                                newValue_Log += item + ",";
                            }
                            NewValue = NewValue.TrimEnd(',');
                            newValue_Log = newValue_Log.TrimEnd(',');
                            newValue_Log = newValue_Log.Replace("دپارتمان", "");
                        }
                    }
                    else
                    {
                        DropDownList drpNewValue = (sender as GridView).Rows[e.RowIndex].FindControl("drpNewValue") as DropDownList;
                        NewValue = drpNewValue.SelectedValue;
                        newValue_Log = drpNewValue.SelectedItem.Text;
                        if (oldValue != "" && drpNewValue.Items.FindByValue(oldValue) != null)
                            oldValue_Log = drpNewValue.Items.FindByValue(oldValue).Text;
                        else
                            oldValue_Log = "انتخاب نشده";
                    }

                }

                if (!string.IsNullOrWhiteSpace(NewValue))
                {
                    UpdateNewValue(changeId, NewValue);
                    setLog(DTO.eventEnum.تغییر_فیلد_در_درخواست_ویرایش,
                        string.Format("{0} , {1} : {2}  ,  {3} : {4}       {5} : {6}       ,{7} : {8}",
                        drpRequestType.Items.FindByValue(ViewState[requestType].ToString()).Text,
                        "کد سطر", changeId,
                        "فیلد ویرایش شده", fieldName,
                        controlToFieldId != 54 ? "مقدار قبلی" : "", oldValue_Log,
                        "مقدار جدید", newValue_Log));
                }
            }
        }

        private void UpdateNewValue(int changeId, string NewValue)
        {
            int count = ProfReqBuss.UpdateOneChangeById(changeId, NewValue, (int)ChangeState.submitted);
            if (count > 0)
            {
                grdChangeList.EditIndex = -1;
                BindData();
            }
            else
            {
                string msg = "خطا در هنگام بروزرسانی!";
                RadWindowManager1.RadAlert(msg, 200, 100, "خطا", "");
            }
        }

        #endregion change Fied


        #region aprrove changes

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            approveChange();
            showRequest();
        }
        private bool approveChange()
        {
            string msg = string.Empty;
            try
            {
                if (!Page.IsValid)
                { return false; }
                if (!canContinue())
                {
                    BindData();
                    return false;
                }
                int reqId = Convert.ToInt32(ViewState[requestId]);
                int reqType = Convert.ToInt32(ViewState[requestType]);



                //int count = 0;
                //int count =ProfReqBuss.UpdateProfessorRequestStatus(reqId, (int)RequestLogId.approved);
                //if (count > 0)
                //{
                switch ((RequestTypeId)reqType)
                {
                    case RequestTypeId.EditContactInfo:
                    case RequestTypeId.EditPersonalInfo:
                        try
                        {
                            if (ProfReqBuss.UpdateOstadInformation_AfterApprove(reqId) &&
                                                    ProfReqBuss.InsertDocumentsToHr(reqId, Convert.ToInt32(ViewState[hrId]))
                                                    && ProfReqBuss.UpdateProfessorRequestStatus(reqId, (int)RequestLogId.approved) > 0)
                            {


                                switch ((RequestTypeId)reqType)
                                {
                                    case RequestTypeId.EditContactInfo:
                                        setLog(DTO.eventEnum.تاييد_درخواست_ويرايش_اطلاعات_تماس, "");
                                        break;
                                    case RequestTypeId.EditPersonalInfo:
                                        setLog(DTO.eventEnum.تاييد_درخواست_ويرايش_اطلاعات_فردي, "");
                                        break;
                                }

                                msg = "درخواست شماره " + reqId + " با موفقیت بروزرسانی شد.";

                            }
                            else
                                msg = "خطا درهنگام بروزرسانی درخواست شماره " + reqId + " ، لطفا مجددا تلاش نمایید .";
                        }
                        catch (Exception ex)
                        {
                            msg = "خطا درهنگام بروزرسانی درخواست شماره " + reqId + "   ex:   " + ex.Message.ToString().Replace("\r\n", "") + " ، لطفا مجددا تلاش نمایید .";
                        }
                        break;

                    case RequestTypeId.EditCooperation:
                        try
                        {
                            var editInfoFields = ProfReqBuss.GetProfessorEditInfoFieldsByProfessorRequestId(reqId);
                            foreach (DataRow row in editInfoFields.Rows)
                            {
                                if (Convert.ToInt32(row["ControlToFieldId"]) == 54)
                                {
                                    string newValue = row["NewValue"].ToString();
                                    newValue = newValue.EndsWith(",") ? newValue.Substring(0, newValue.Length - 1) : newValue;
                                    FRB.SetProfessorGroups(Convert.ToInt32(ViewState[hrId]), newValue.Split(',').ToList());
                                }
                            }
                            if (ProfReqBuss.UpdateOstadInformation_AfterApprove(reqId)
                                                    && ProfReqBuss.UpdateProfessorRequestStatus(reqId, (int)RequestLogId.approved) > 0)
                            {
                                setLog(DTO.eventEnum.تاييد_درخواست_ويرايش_اطلاعات_نحوه_همکاری, "");
                                msg = "درخواست شماره " + reqId + " با موفقیت بروزرسانی شد.";
                            }
                            else
                            {
                                msg = "خطا درهنگام بروزرسانی درخواست شماره " + reqId + " ، لطفا مجددا تلاش نمایید .";

                            }
                        }
                        catch (Exception ex)
                        {
                            msg = "خطا درهنگام بروزرسانی درخواست شماره " + reqId + "   ex:   " + ex.Message.ToString().Replace("\r\n", "") + " ، لطفا مجددا تلاش نمایید .";
                        }
                        break;


                    case RequestTypeId.EditHokm:
                        if (rblIsHeiat.SelectedValue == "2")//هیئت علمی نبود
                        {
                            int codOstadInfoFromHr = 0;
                            try
                            {
                                setLogForEditedListInHokmWithIsNotHeiat();
                            }
                            catch (Exception ex)
                            {
                                msg = "setLogForEditedListInHokmWithIsNotHeiat    " + ex.Message.ToString().Replace("\r\n", "");
                            }
                            try
                            {
                                codOstadInfoFromHr = GetCodOstadInfoFromHR();
                            }
                            catch (Exception ex)
                            {
                                msg = "GetCodOstadInfoFromHR      " + ex.Message.ToString().Replace("\r\n", "");
                            }
                            try
                            {
                                updateHokmstatusForIsNotHeiat(codOstadInfoFromHr);
                            }
                            catch (Exception ex)
                            {
                                msg = "updateHokmstatusForIsNotHeiat       " + ex.Message.ToString().Replace("\r\n", "");
                            }
                            try
                            {
                                if (ProfReqBuss.UpdateProfessorRequestStatus(reqId, (int)RequestLogId.approved) > 0)
                                {
                                    msg = "درخواست شماره " + reqId + " با موفقیت بروزرسانی شد.";
                                    setLog(DTO.eventEnum.تاييد_درخواست_ويرايش_حکم_کارگزینی, "");
                                }
                                else
                                    msg = "خطا درهنگام بروزرسانی درخواست شماره " + reqId + " ، لطفا مجددا تلاش نمایید .";
                            }
                            catch (Exception ex)
                            {
                                msg = "ProfReqBuss.UpdateProfessorRequestStatus       " + ex.Message.ToString().Replace("\r\n", "");
                            }

                        }
                        else//هیئت علمی بود
                        {
                            try
                            {
                                setLogForEditedListInHokm();
                                if (updateHokmstatus()
                                                && ProfReqBuss.UpdateProfessorRequestStatus(reqId, (int)RequestLogId.approved) > 0)
                                {
                                    msg = "درخواست شماره " + reqId + " با موفقیت بروزرسانی شد.";
                                    setLog(DTO.eventEnum.تاييد_درخواست_ويرايش_حکم_کارگزینی, "");
                                }
                                else
                                    msg = "خطا درهنگام بروزرسانی درخواست شماره " + reqId + " ، لطفا مجددا تلاش نمایید .";
                            }
                            catch (Exception ex)
                            {
                                msg = "خطا درهنگام بروزرسانی درخواست شماره " + reqId + "   ex:   " + ex.Message.ToString().Replace("\r\n", "") + " ، لطفا مجددا تلاش نمایید .";
                            }
                        }

                        break;
                }
                //}
                //else
                //{
                //    msg = "خطا درهنگام بروزرسانی درخواست شماره " + reqId + " ، لطفا مجددا تلاش نمایید .";
                //}
            }
            catch (Exception exx)
            {
                msg = exx.ToString();
            }
            RadWindowManager1.RadAlert(msg, 300, 100, "پیام سیستم", "CloseRadWindow1");
            return true;

        }

        private void updateHokmstatusForIsNotHeiat(int codeOstad)
        {
            ProfReqBuss.updateHokmstatusForIsNotHeiat(codeOstad, 8);
        }

        private bool updateHokmstatus()
        {
            try
            {
                ProfessorHokmDTO oHokm = new ProfessorHokmDTO();

                oHokm.HokmId = Convert.ToInt32(hdnfHokmId.Value);
                oHokm.Code_Ostad = Convert.ToInt32(txtCodeOstad.Text);
                oHokm.Number_Hokm = txtHokmNumber.Text.ToString();
                oHokm.Date_Hokm = txtDateSodoorHokm.Text;
                oHokm.Date_RunHokm = txtDateEjraHokm.Text;
                oHokm.Martabeh = Convert.ToInt32(drpMartabe.SelectedValue);
                oHokm.Payeh = Convert.ToInt32(txtPaye.Text);
                oHokm.Type_Estekhdam = Convert.ToInt32(drpHireType.SelectedValue);
                oHokm.Uni_Khedmat = Convert.ToInt32(drpPastUni.SelectedValue);
                oHokm.Nahveh_Hamk = Convert.ToInt32(rdblHireType.SelectedValue);
                oHokm.MablaghHokm = Convert.ToInt64(txtMablaghHokm.Text);
                oHokm.BoundHour = chkBoundHour.Checked;
                oHokm.DateRunHokmHere = txtDateRunHokmNew.Text;
                oHokm.State = (int)ChangeState.approved;
                oHokm.Uni_KhedmatType = Convert.ToInt32(ddlPastUniType.SelectedValue);
                oHokm.InfoPeopleId = Convert.ToInt32(ViewState[hrId]);
                bool updated = false;
                if (ProfReqBuss.updateHokmInThreeTables(oHokm))
                    updated = ProfReqBuss.ApproveNewHokm(oHokm);
                return updated;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }

        }



        #endregion aprrove changes

        private int GetCodOstadInfoFromHR()
        {
            var userId1 = Convert.ToInt32(ViewState[hrId]);
            return Convert.ToInt32(userId1);
            //return Convert.ToInt32(ProfReqBuss.GetOstadInfoInHr(userId1).Rows[0][0]);
        }

        protected void btnDeny_Click(object sender, EventArgs e)
        {
            popupMessageBox(Convert.ToInt32(ViewState[requestId]));
        }

        private void denyChange(int reqId, string reason)
        {

            int count = ProfReqBuss.UpdateProfessorRequestStatus(reqId, (int)RequestLogId.denied);
            ProfReqBuss.UpdateProfessorRequestStatus_Doc(reqId, 2);
            string msg = string.Empty;
            if (count > 0)
            {
                int reqType = Convert.ToInt32(ViewState[requestType]);
                switch ((RequestTypeId)reqType)
                {
                    case RequestTypeId.EditContactInfo:
                        setLog(DTO.eventEnum.رد_درخواست_ويرايش_اطلاعات_تماس, reason);
                        break;
                    case RequestTypeId.EditHokm:
                        setLog(DTO.eventEnum.رد_درخواست_ويرايش_حکم_کارگزینی, reason);
                        break;
                    case RequestTypeId.EditPersonalInfo:
                        setLog(DTO.eventEnum.رد_درخواست_ويرايش_اطلاعات_فردي, reason);
                        break;
                    case RequestTypeId.EditCooperation:
                        setLog(DTO.eventEnum.رد_درخواست_ويرايش_اطلاعات_نحوه_همکاری, reason);
                        break;
                }
                msg = "درخواست شماره " + reqId + " رد شد.";
            }
            else
            {
                msg = "خطا درهنگام بروزرسانی درخواست شماره " + reqId + " ، لطفا مجددا تلاش نمایید .";
            }
            RadWindowManager1.RadAlert(msg, 300, 100, "پیام سیستم", "CloseRadWindow2");
            showRequest();
        }

        protected void btnRefreshGrid_Click(object sender, EventArgs e)
        {
            string BindType = ViewState["BindType"].ToString();
            string state = null;
            List<ProfessorEditRequestDTO> oRequestList = null;
            if (BindType == "Single")
            {
                int code_ostad = Convert.ToInt32(txtSearchByProfCode.Text);
                state = rdblRequestState.SelectedValue;
                oRequestList = ProfReqBuss.GetProfessorRequestsByIdAndStatus(code_ostad, state);
            }
            else
            {
                string reqType = drpRequestType.SelectedValue;
                state = rdblStatus.SelectedValue;
                oRequestList = ProfReqBuss.GetRequestByTypeAndStatus(reqType, state);
            }
            BindRequestGrid(oRequestList, state);
        }

        protected void btnSendToPending_Click(object sender, EventArgs e)
        {
            int reqId = Convert.ToInt32(ViewState[requestId]);
            int count = ProfReqBuss.UpdateProfessorRequestStatus(reqId, (int)RequestLogId.submitted);
            string msg = "";
            if (count > 0)
            {
                ProfReqBuss.UpdateProfessorRequestStatus_Doc(reqId, 1);
                int reqType = Convert.ToInt32(ViewState[requestType]);
                string firstPart = "تغییر وضعیت درخواست ویرایش", lastPart = "از رد شده به درحال بررسی", description = "";
                switch ((RequestTypeId)reqType)
                {
                    case RequestTypeId.EditContactInfo:
                        description = string.Format("{0} {1} {2}", firstPart, "اطلاعات تماس", lastPart);
                        break;
                    case RequestTypeId.EditHokm:
                        description = string.Format("{0} {1} {2}", firstPart, "حکم کارگزینی", lastPart);
                        break;
                    case RequestTypeId.EditPersonalInfo:
                        description = string.Format("{0} {1} {2}", firstPart, "اطلاعات فردی", lastPart);
                        break;
                    case RequestTypeId.EditCooperation:
                        description = string.Format("{0} {1} {2}", firstPart, "نحوه همکاری", lastPart);
                        break;
                }
                setLog(DTO.eventEnum.تغییر_وضعیت_درخواست_ویرایش_استاد, description);
                msg = "درخواست شماره " + reqId + " به کارتابل در حال بررسی برگشت داده شد.";
            }
            else
            {
                msg = "خطا درهنگام بروزرسانی درخواست شماره " + reqId + " ، لطفا مجددا تلاش نمایید .";
            }
            RadWindowManager1.RadAlert(msg, 300, 100, "پیام سیستم", "CloseRadWindow2");
            showRequest();

        }

        private int getOstanCode(int controlToFieldId)
        {
            DataTable dtAllChanges = ProfReqBuss.GetChangeListByReqId(Convert.ToInt32(ViewState[requestId]));
            DataRow[] dr = dtAllChanges.Select("controlToFieldId=" + (controlToFieldId - 1));
            if (dr.Length == 1)
            {
                if (dr[0]["newvalue"] != DBNull.Value)
                    return Convert.ToInt32(dr[0]["newValue"]);
            }
            DTO.University.Faculty.editInfoStruct OstadInf = FRB.getOstadInf(Convert.ToInt32(ViewState[codeOstad]));
            switch (controlToFieldId)
            {
                case 46:
                    return OstadInf.ostanHome;
                case 48:
                    return OstadInf.ostanKar;
            }
            return 0;
        }

        #region 'check' methods

        private bool canContinue()
        {
            DataTable dtAllChanges = ProfReqBuss.GetChangeListByReqId(Convert.ToInt32(ViewState[requestId]));
            int reqType = Convert.ToInt32(ViewState[requestType]);
            DataRow[] drSelect;

            foreach (DataRow dr in dtAllChanges.Rows)
            {
                int controlToFieldId = Convert.ToInt32(dr["controlTofieldId"]);
                switch (controlToFieldId)
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
                    case 40:    //	شماره بیمه
                    case 49:    //	نام پدر
                    case 50:    //	شماره شناسنامه
                    case 51:    //	سال تولد
                    case 7:     //	مدرک تحصیلی
                    case 17:    //	نوع دانشگاه محل تحصیل
                    case 19:    //	دانشگاه محل تحصیل
                    case 30:    //	وضیعت نظام وظیفه
                    case 42:    //	نحوه همکاری
                    case 53:    //	جنسیت
                        if (dr["newValue"] != DBNull.Value)
                        {
                            if (dr["newvalue"].ToString() != "" && dr["newvalue"].ToString() != "0")
                                break;
                        }
                        string msg = string.Format("{0}, {1} {2} {3} {4}", "کاربر گرامی", "مقدار وارد شده برای", dr["description"].ToString(), "صحیح نمی باشد.", "لطفا آن را اصلاح کرده و مجددا تلاش فرمایید");
                        RadWindowManager1.RadAlert(msg, 300, 100, "پیام سیستم", "");
                        return false;

                    case 15:    //	کشور محل تحصیل
                    case 9:     //	رشته تحصیلی
                    case 45:    //	استان محل سکونت
                    case 47:    //	استان محل کار
                    case 46:    //	شهر محل سکونت
                    case 48:    //	شهر محل کار
                        drSelect = dtAllChanges.Select("controlToFieldId=" + controlToFieldId);
                        if (drSelect.Length > 0)
                        {
                            if (drSelect[0]["newValue"] != DBNull.Value)
                            {
                                if (drSelect[0]["newValue"].ToString() != "" && drSelect[0]["newValue"].ToString() != "-1")
                                    break;
                            }
                        }
                        string msg4 = string.Format("{0}, {1} {2} {3} {4}", "کاربر گرامی", "مقدار وارد شده برای", dr["description"].ToString(), "صحیح نمی باشد.", "لطفا آن را اصلاح کرده و مجددا تلاش فرمایید");
                        RadWindowManager1.RadAlert(msg4, 300, 100, "پیام سیستم", "");
                        return false;
                    case 54:    //	گروه آموزشی
                        drSelect = dtAllChanges.Select("controlToFieldId=" + controlToFieldId);
                        if (drSelect.Length > 0)
                        {
                            if (drSelect[0]["newValue"] != DBNull.Value)
                            {
                                if (drSelect[0]["newValue"].ToString() != "" && drSelect[0]["newValue"].ToString().TrimEnd(',') != "")
                                    break;
                            }
                        }
                        string msg3 = string.Format("{0}, {1} {2} {3} {4}", "کاربر گرامی", "شما هیچ مقداری را برای", dr["description"].ToString(), "انتخاب نکرده اید.", "لطفا حداقل یک دپارتمان انتخاب کرده و مجددا تلاش فرمایید");
                        RadWindowManager1.RadAlert(msg3, 300, 100, "پیام سیستم", "");
                        return false;

                    default:
                        return true;

                }
            }


            return true;
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

        private bool IsTextBoxField(int controlTofieldId)
        {
            switch (controlTofieldId)
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

        #endregion 'check' methods


        #region messageBox
        protected void btnSubmitMessage_Click(object sender, EventArgs e)
        {
            if (txtMessage.Text.Trim() == "")
                return;
            if (IsValid)
            {
                int reqId = Convert.ToInt32(ViewState[requestId]);
                int Id = ProfReqBuss.InsertMessageToRequest(reqId, txtMessage.Text);
                denyChange(reqId, txtMessage.Text);
            }
            txtMessage.Text = "";
        }

        private void popupMessageBox(int reqId)
        {
            txtMessage.Text = string.Empty;
            string scrp = "function f(){$find(\"" + RadWindow2.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
        }

        #endregion messageBox



        #region Log
        private void setLogForEditedListInHokmWithIsNotHeiat()
        {

            var userId1 = Convert.ToInt32(ViewState[hrId]);
            string oLastMartabe = ProfReqBuss.GetLastMartabe(userId1);
            string lastMartabe = "";
            switch (oLastMartabe)
            {
                case "-2":
                    lastMartabe = "بدون مرتبه";
                    break;
                case "1":
                    lastMartabe = "مربی";
                    break;
                case "2":
                    lastMartabe = "دانشیار";
                    break;
                case "3":
                    lastMartabe = "استادیار";
                    break;
                case "4":
                    lastMartabe = "استاد";
                    break;
                default:
                    lastMartabe = "نا مشخص "+oLastMartabe;
                    break;


            }
            setLogForHokm("مرتبه", lastMartabe, "بدون مرتبه");

        }

        private void setLogForEditedListInHokm()
        {
            int reqId = Convert.ToInt32(ViewState[requestId]);
            ProfessorHokmDTO oNewHokm = ProfReqBuss.GetNewHokmInfo(reqId);

            if (drpPastUni.SelectedValue != oNewHokm.Uni_Khedmat.ToString())
                setLogForHokm("نام دانشگاه محل خدمت", oNewHokm.Uni_Khedmat.ToString(), drpPastUni.SelectedValue);
            if (txtHokmNumber.Text.Trim() != oNewHokm.Number_Hokm.ToString().Trim())
                setLogForHokm("شماره حکم", oNewHokm.Number_Hokm.ToString().Trim(), txtHokmNumber.Text.Trim());
            if (txtDateEjraHokm.Text.Trim() != oNewHokm.Date_RunHokm.Trim())
                setLogForHokm("تاریخ اجرای حکم", oNewHokm.Date_RunHokm.Trim(), txtDateEjraHokm.Text.Trim());
            if (txtDateSodoorHokm.Text != oNewHokm.Date_Hokm)
                setLogForHokm("تاریخ صدور حکم", oNewHokm.Date_Hokm, txtDateSodoorHokm.Text);
            if (txtMablaghHokm.Text != oNewHokm.MablaghHokm.ToString())
                setLogForHokm("مبلغ حکم", oNewHokm.MablaghHokm.ToString(), txtMablaghHokm.Text);
            if (txtPaye.Text != oNewHokm.Payeh.ToString())
                setLogForHokm("پایه", oNewHokm.Payeh.ToString(), txtPaye.Text);
            if (ddlPastUniType.SelectedValue != oNewHokm.Uni_KhedmatType.ToString())
                setLogForHokm("نوع دانشگاه محل خدمت", ddlPastUniType.Items.FindByValue(oNewHokm.Uni_KhedmatType.ToString()).Text, ddlPastUniType.SelectedItem.Text);
            if (drpHireType.SelectedValue != oNewHokm.Type_Estekhdam.ToString())
                setLogForHokm("نوع استخدام", drpHireType.Items.FindByValue(oNewHokm.Type_Estekhdam.ToString()).Text, drpHireType.SelectedItem.Text);
            if (drpMartabe.SelectedValue != oNewHokm.Martabeh.ToString())
                setLogForHokm("مرتبه دانشگاهی", drpMartabe.Items.FindByValue(oNewHokm.Martabeh.ToString()).Text, drpMartabe.SelectedItem.Text);
            if (rdblHireType.SelectedValue != oNewHokm.Nahveh_Hamk.ToString())
                setLogForHokm("نحوه همکاری در دانشگاه مبدا", rdblHireType.Items.FindByValue(oNewHokm.Nahveh_Hamk.ToString()).Text, rdblHireType.SelectedItem.Text);
            if (chkBoundHour.Checked != oNewHokm.BoundHour)
                setLogForHokm("متقاضی تکمیل ساعت موظفی", oNewHokm.BoundHour ? "هستم" : "نیستم", chkBoundHour.Checked ? "هستم" : "نیستم");
        }

        private void setLogForHokm(string fieldName, string oldValue, string newValue)
        {
            setLog(DTO.eventEnum.تغییر_فیلد_در_درخواست_ویرایش,
                                    string.Format("{0} , {1} : {2}  ,  {3} : {4}       {5} : {6}",
                                    drpRequestType.Items.FindByValue(ViewState[requestType].ToString()).Text,
                                    "فیلد ویرایش شده", fieldName,
                                    "مقدار قبلی", oldValue,
                                    "مقدار جدید", newValue));
        }

        private void setLog(DTO.eventEnum eventType, string description)
        {
            int userId;//کاربری که لاگین کرده
            int appId;//کد قسمتی از برنامه که الان توش هستیم. یعنی کارگزینی  -  13
            string Description = description;//توضیحات اختیاری
            int modifyId;//کد درخواست ویرایش شده. ویرایش اطلاعات فردی،اطلاعات تماس و ...

            userId = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
            appId = 13;
            description = "";
            modifyId = Convert.ToInt32(ViewState[requestId]);

            CB.InsertIntoUserLog(userId, DateTime.Now.ToString("HH:mm"), appId, (int)eventType, Description, modifyId);
        }

        #endregion Log

    }
}