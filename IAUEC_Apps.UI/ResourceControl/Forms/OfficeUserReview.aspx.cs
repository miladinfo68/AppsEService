using ResourceControl.BLL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Wordprocessing;
using IAUEC_Apps.Business.Common;
using Telerik.Web.UI;
using Control = System.Web.UI.Control;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace ResourceControl.PL.Forms
{
    public partial class OfficeUserReview : System.Web.UI.Page
    {
        private List<RequestFR> _reqlist = null;
        private List<ResourceControl.Entity.Resource> _resList = null;
        private List<RequestDateTime> _dateTimeList;
        RequestHandler rh = new RequestHandler();

        int roleId;//daneshkade id has to come from session.
        static int userID;
        protected void Page_Load(object sender, EventArgs e)
        {
            userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
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
                }
                Session[sessionNames.menuID] = menuId;
                AccessControl.MenuId = menuId;
                AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();
                if (Request.QueryString["status"] != null)
                {
                    string status = Request.QueryString["status"];
                    drpRequestTypeList.ClearSelection();
                    drpRequestTypeList.Items.FindByValue(status).Selected = true;
                    GridBind(Convert.ToInt32(status));
                }
                else
                {
                    if (Session["StausLinke"] != null)
                    {
                        drpRequestTypeList.ClearSelection();
                        if (Session["StausLinke"] != null)
                            drpRequestTypeList.Items.FindByValue(Session["StausLinke"] as string).Selected = true;
                        GridBind(Convert.ToInt32(Session["StausLinke"]));
                    }
                    else
                    {
                        GridBind(1);
                    }

                }
                //userIDStr = Session[sessionNames.userID_Karbar].ToString();
                if (!drpRequestTypeList.Items.FindByValue("1").Selected)
                {
                    ((DataControlField)grdRequestList.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "رد درخواست").SingleOrDefault()).Visible = false;
                }
            }
        }

        private void GridBind(int status)
        {
            roleId = Convert.ToInt32(Session["RoleId"]);
            RequestHandler rq = new RequestHandler();
            //reqlist = new List<RequestFR>();
            if (roleId == 1 || roleId == 50)
            {
                if (status != 6)
                {
                    //if(status==1)
                    //{
                    //    FillLinkAndStatusForClickOffice(status);
                    //}
                    //else
                    //{
                    //    FillLinkAndStatusForClick(status);
                    //}
                    _reqlist = rq.GetRequestListByStatus(status);
                }
                else
                {
                    _reqlist = rq.GetRequestListOfTermJari();
                }
            }
            //else
            //{
            //    if (status != 6)
            //    {
            //        _reqlist = rq.GetRequestListByStatusAndRoleId(status, roleId);
            //    }
            //    else
            //    {
            //        _reqlist = rq.GetRequestListByRoleId(roleId);
            //    }
            //}
            else
            {

                _reqlist = rq.GetRequestListByStatusAndRoleIdForEdari(status, roleId);

            }
            if (_reqlist != null)
            {
                _reqlist = _reqlist.Where(x => x.Subject != "StudentDefence").ToList();
            }
            grdRequestList.DataSource = _reqlist;
            grdRequestList.DataBind();
        }


        //**********************
        protected void FillLinkAndStatusForClick(int status)
        {
            Session["StausLinke"] = status.ToString();
            var link = "~/ResourceControl/" + Session["linkeeee"] as string;
            Response.Redirect(link);
        }

        protected void FillLinkAndStatusForClickOffice(int status)
        {
            Session["StausLinke"] = status.ToString();
            var link = "~/ResourceControl/" + Session["linke2"] as string;
            Response.Redirect(link);
        }
        //***********************    
        protected void drpRequestTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {

            int status = Convert.ToInt32(drpRequestTypeList.SelectedValue);
            if (userID != 1)
            {

                if (drpRequestTypeList.SelectedIndex == 0)
                {
                    var singleOrDefault = (DataControlField)grdRequestList.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "رد درخواست").SingleOrDefault();
                    if (singleOrDefault != null)
                        singleOrDefault.Visible = true;
                    var dataControlField = (DataControlField)grdDateTime.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "عملیات").SingleOrDefault();
                    if (dataControlField != null)
                        dataControlField.Visible = true;
                    var dataControlFiel = (DataControlField)grdDateTime.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "عملیات مورد نیاز").SingleOrDefault();
                    if (dataControlFiel != null)
                        dataControlFiel.Visible = true;

                }
                else
                {
                    var dataControlField = (DataControlField)grdRequestList.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "رد درخواست").SingleOrDefault();
                    if (dataControlField != null)
                        dataControlField.Visible = false;

                    var singleOrDefault = (DataControlField)grdDateTime.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "عملیات").SingleOrDefault();
                    if (singleOrDefault != null)
                        singleOrDefault.Visible = false;

                    var singleOrDefaul = (DataControlField)grdDateTime.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "عملیات").SingleOrDefault();
                    if (singleOrDefaul != null)
                        singleOrDefaul.Visible = false;

                    var dataControlFie = (DataControlField)grdDateTime.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "عملیات مورد نیاز").SingleOrDefault();
                    if (dataControlFie != null)
                        dataControlFie.Visible = false;
                    var dropDownList = grdDateTime.FindControl("drpResource") as DropDownList;
                    if (dropDownList != null) dropDownList.Enabled = false;
                }
                GridBind(status);
            }
            else
            {
                Session["StausLinke"] = status.ToString();
                //  FillLinkAndStatusForClick(Convert.ToInt32(Session["StausLinke"]));
                FillLinkAndStatusForClickOffice(status);
            }
        }

        protected void grdRequestList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int status = Convert.ToInt32(drpRequestTypeList.SelectedValue);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int rowindex = e.Row.RowIndex;

                ResolveImageUrl(e);
            }
        }

        protected void grdRequestList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int reqId = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "checkReq")
            {
                hdnfReqId.Value = reqId.ToString();

                //List<ResourceControl.Entity.Resource> resList = new List<ResourceControl.Entity.Resource>();
                ResourceHandler rsh = new ResourceHandler();
  
                RequestHandler requestHandler = new RequestHandler();
                var requestDetails = requestHandler.GetRequestDetails(reqId);

                _resList = requestDetails.Status>1 ? rsh.GetResourceListByReqIDForAfterAccept(reqId) : rsh.GetResourceListByReqID(reqId);

                ViewState.Add("reqId", reqId);
                ViewState.Add("_resList", _resList);

                if (drpRequestTypeList.SelectedIndex == 0)
                {
                    drpCandidateResource.DataSource = _resList;
                    drpCandidateResource.DataTextField = "name";
                    drpCandidateResource.DataValueField = "ID";
                    drpCandidateResource.DataBind();
                    drpCandidateResource.Items.Insert(0, new ListItem { Value = "0", Text = "انتخاب کنید..." });

                    dvOperation.Visible = true;
                    dvOperation1.Visible = true;
                    dvSuggestResource.Visible = true;
                }
                else
                {
                    dvOperation.Visible = false;
                    dvOperation1.Visible = false;
                    dvSuggestResource.Visible = false;
                }

                if (drpRequestTypeList.SelectedIndex == 5)
                {
                    dvOperation.Visible = false;
                    dvOperation1.Visible = false;
                }

                grdDateTime.EditIndex = -1;
                RequestDateTimeHandler rqdateTimeH = new RequestDateTimeHandler();
                _dateTimeList = rqdateTimeH.GetDateTimeListByRequestId(reqId);
                grdDateTime.DataSource = _dateTimeList.OrderBy(c => c.Date);
                grdDateTime.DataBind();


           
                lblRequestNumber.Text = requestDetails.ID.ToString();
                lblIssuerName.Text = requestDetails.IssuerName.ToString();
                lblCourseName.Text = requestDetails.CourseName.ToString();


       

                string scrp = "function f(){$find(\"" + RadWindow1.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
            }

            if (e.CommandName == "deny")
            {
                hdnfDenyReqId.Value = reqId.ToString();

                string scrp = "function f(){$find(\"" + RadWindow2.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
            }

            if (e.CommandName == "edit")
            {
                Response.Redirect("UserEditRequest.aspx?id=" + generaterandomstr() + "@A" + "0" + "-" + generaterandomstr() + "&reqId=" + e.CommandArgument);
            }

            if (e.CommandName == "History")
            {

                CommonBusiness cmb = new CommonBusiness();

                lst_history.DataSource = cmb.GetUserLogByModifyId(int.Parse(e.CommandArgument.ToString()), 11);
                lst_history.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            if (e.CommandName == "showInfo")
            {
                #region ShowInfo

                hdnfReqId.Value = reqId.ToString();
                RequestHandler rh = new RequestHandler();
                GridViewRow curruntRow = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                imgStatus2.ImageUrl = ((Image)curruntRow.Cells[2].FindControl("imgStatus")).ImageUrl;
                var requestDetails = rh.GetRequestDetails(reqId);
                lblRequestId.Text = requestDetails.ID.ToString();
                lblDarkhast.Text = requestDetails.CourseName;
                lbldateOfRequest.Text = requestDetails.Issue_time;
                RequestDateTimeHandler rqdateTimeH = new RequestDateTimeHandler();
                _dateTimeList = rqdateTimeH.GetDateTimeListByRequestId(reqId);
                var dateTime = _dateTimeList.OrderBy(c => c.Date).FirstOrDefault(c => c.Date != null);
                if (dateTime != null)
                    lblRequest.Text = dateTime.Date;
                //var requestDateTime = requestDetails.DateTimeRange.FirstOrDefault(c => c.Date != null);
                //if (requestDateTime != null)
                //{
                //    var firstOrDefault = requestDetails.DateTimeRange.FirstOrDefault(c => c.StartTime != 0).Date;
                //    if (firstOrDefault != null)
                //        lblRequest.Text = firstOrDefault;
                //}




                var startTime = _dateTimeList.FirstOrDefault(c => c.StartTime != 0);
                if (startTime != null)
                    lblTime1.Text = TimeSpan.FromTicks((long)startTime.StartTime).ToString().Substring(0, 5);

                var endTime = _dateTimeList.FirstOrDefault(c => c.EndTime != 0);
                if (endTime != null)
                    lblTime2.Text = TimeSpan.FromTicks((long)endTime.EndTime).ToString().Substring(0, 5);

                switch (requestDetails.Status)
                {
                    case 0:
                        lblStatue.Text = "درخواست استاد ثبت گردیده و در انتظار ارجاع هست";
                        break;
                    case 1:
                        lblStatue.Text = "درخواست ارجاع داده شده است";
                        break;
                    case 2:
                        lblStatue.Text = "درخواست شما مورد تایید واقع گردیده";
                        break;
                    case 3:
                        lblStatue.Text = "درخواست شما مورد تایید واقع نگردیده";
                        break;
                    case 4:
                        lblStatue.Text = "درخواست شما اطلاع رسانی گردیده است";
                        break;
                    case 5:
                        lblStatue.Text = "درخواست شما از دست رفته است";
                        break;
                }
                lblTozieh.Text = requestDetails.Note;
                lblCapecity.Text = requestDetails.Capacity.ToString();
                lblLocation.Text = requestDetails.Location;
                if (!string.IsNullOrEmpty(requestDetails.Answer_time))
                {
                    if (requestDetails.Status == 2)
                    {
                        lblheader.Text = "زمان پاسخ به درخواست:";

                    }
                    else
                    {
                        lblheader.Text = "زمان رد درخواست:";
                        litDenyNot.Text = "علت رد درخواست:";
                    }
                    lblheader.Visible = true;
                    lblDateOfResponse.Visible = true;
                    lblDateOfResponse.Text = requestDetails.Answer_time;
                    if (requestDetails.Status != 2)
                    {
                        litDenyNot.Visible = true;
                        lblDenyNot.Visible = true;
                        lblDenyNot.Text = requestDetails.Answernote;
                    }
                }
                else
                {
                    lblheader.Visible = false;
                    lblDateOfResponse.Visible = false;
                    litDenyNot.Visible = false;
                    lblDenyNot.Visible = false;
                }
                var requestDateTimes = _dateTimeList.OrderBy(d => d.Date);
                if (requestDateTimes.Count() > 1)
                    tblRangeOfDate.Visible = true;
                else
                    tblRangeOfDate.Visible = false;
                if (requestDateTimes.Any())
                {
                    tblRangeOfDate.Visible = true;
                    grdOldDateTime.DataSource = requestDateTimes;
                    grdOldDateTime.DataBind();
                }
                var cth = new CategoryHandler();
                var categoryName = cth.GetCategoryList().FirstOrDefault(c => c.ID == requestDetails.CatID).Name;
                lblPosition.Text = categoryName;
                var opt = new OptionHandler();
                var optlist = opt.GetOptionListByCatID(requestDetails.CatID);
                var rsopt = new Req_Opt_JuncHandler();
                var resOptJunlist = rsopt.GetReq_Opt_JuncListByReqID(requestDetails.ID);
                var requestOption = new List<Option>();
                foreach (Req_Opt_Junc optJunc in resOptJunlist)
                {
                    foreach (var option in optlist)
                    {
                        if (optJunc.Opt_id == option.ID)
                            requestOption.Add(new Option { Name = option.Name });
                    }
                }
                grdFacilities.DataSource = requestOption;
                grdFacilities.DataBind();
                string scrp = "function f(){$find(\"" + RadWindow3.ClientID +
                              "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);


                #endregion
            }
        }

        protected void grdRequestList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdRequestList.PageIndex = e.NewPageIndex;
            int status = Convert.ToInt32(drpRequestTypeList.SelectedValue);
            if (drpRequestTypeList.SelectedIndex == 1 || drpRequestTypeList.SelectedIndex == 2 || drpRequestTypeList.SelectedIndex == 4)
            {
                ((DataControlField)grdRequestList.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "رد درخواست").SingleOrDefault()).Visible = true;
            }
            else
            {
                ((DataControlField)grdRequestList.Columns.Cast<DataControlField>().Where(fld => fld.HeaderText == "رد درخواست").SingleOrDefault()).Visible = false;
            }
            GridBind(status);
        }

        private void ResolveImageUrl(GridViewRowEventArgs e)
        {
            Image img = (Image)e.Row.FindControl("imgStatus");
            RequestFR dataRow = (RequestFR)e.Row.DataItem;
            switch (dataRow.Status)
            {
                case 0:
                    img.ImageUrl = GetImage(dataRow.Status);
                    img.ToolTip = "در حال بررسی آموزش";
                    break;
                case 1:
                    img.ImageUrl = GetImage(dataRow.Status);
                    img.ToolTip = "منتظر تایید اداری";
                    break;
                case 2:
                    img.ImageUrl = GetImage(dataRow.Status);
                    img.ToolTip = "تایید اداری";
                    break;
                case 3:
                    img.ImageUrl = GetImage(dataRow.Status);
                    img.ToolTip = "رد شده";//"تایید شده";
                    break;
                case 4:
                    img.ImageUrl = GetImage(dataRow.Status);
                    img.ToolTip = "اطلاع رسانی شده";
                    break;
                case 5:
                    img.ImageUrl = GetImage(dataRow.Status);
                    img.ToolTip = "از دست رفته";
                    break;
                default:
                    img.ImageUrl = GetImage(dataRow.Status);
                    img.ToolTip = "نامشخص";
                    break;
            }
        }

        protected void grdDateTime_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Enabled = true;
                RequestDateTime reqDateTime = (RequestDateTime)e.Row.DataItem;
                DropDownList drpResource = (DropDownList)e.Row.FindControl("drpResource");

                _resList = (List<ResourceControl.Entity.Resource>)ViewState["_resList"];
                drpResource.DataSource = _resList;
                drpResource.DataTextField = "name";
                drpResource.DataValueField = "ID";
                drpResource.DataBind();
                drpResource.Items.Insert(0, new ListItem { Value = "0", Text = "انتخاب کنید..." });

                if (drpCandidateResource.SelectedIndex > 0)
                {
                    if (drpResource.Items.FindByValue(drpCandidateResource.SelectedValue) != null)
                        drpResource.Items.FindByValue(drpCandidateResource.SelectedValue).Selected = true;

                }
                else
                {
                    if (drpResource.Items.FindByValue(reqDateTime.ResourceId.ToString()) != null)
                        drpResource.Items.FindByValue(reqDateTime.ResourceId.ToString()).Selected = true;

                }

                if (drpRequestTypeList.SelectedValue == "1")
                {
                    if (!reqDateTime.MayConflict)
                    {
                        e.Row.CssClass = "bg-success";
                        btnSubmitFinal.Enabled = true;

                    }
                    else
                    {

                        e.Row.CssClass = "bg-danger";
                        drpResource.Enabled = true;
                        btnSubmitFinal.Enabled = false;
                    }
                }
                else
                {
                    drpResource.Enabled = false;
                }

                if (drpResource.SelectedValue == "0")
                {
                    e.Row.CssClass = "bg-default";

                    drpResource.Enabled = true;
                    var Registerbtn = ((Button)e.Row.Cells[6].Controls[1]);
                    Registerbtn.Visible = true;
                    var Canclebtn = ((Button)e.Row.Cells[6].Controls[3]);
                    Canclebtn.Visible = true;
                    var Editbtn = ((Button)e.Row.Cells[6].Controls[5]);
                    Editbtn.Visible = false;
                }
                else
                {
                    drpResource.Enabled = false;
                    var Registerbtn = ((Button)e.Row.Cells[6].Controls[1]);
                    Registerbtn.Visible = false;
                    var Canclebtn = ((Button)e.Row.Cells[6].Controls[3]);
                    Canclebtn.Visible = false;
                    var Editbtn = ((Button)e.Row.Cells[6].Controls[5]);
                    Editbtn.Visible = true;
                }


                if (reqDateTime.MayConflict)
                {
                    drpResource.Enabled = true;
                    var Registerbtn = ((Button)e.Row.Cells[6].Controls[1]);
                    Registerbtn.Visible = false;
                    var Canclebtn = ((Button)e.Row.Cells[6].Controls[3]);
                    Canclebtn.Visible = true;
                    var Editbtn = ((Button)e.Row.Cells[6].Controls[5]);
                    Editbtn.Visible = false;
                }

                //int reqId = (int) ViewState["reqId"];
                //RequestDateTimeHandler reqdtH = new RequestDateTimeHandler();
                //var listOfDateTime = reqdtH.GetDateTimeListByRequestId(reqId);
                //var listOfHasResource = new List<int>();
                //foreach (var item in listOfDateTime)
                //{
                //    if (item.ResourceId != 0)
                //    {

                //        var id = item.ResourceId.ToString();
                //        drpResource.ClearSelection();
                //        drpResource.Items.FindByValue(id).Selected = true;
                //        var dropDownList = (DropDownList) e.Row.Cells[4].Controls[1];
                //        dropDownList.Enabled = false;
                //        var Registerbtn = ((Button) e.Row.Cells[6].Controls[1]);
                //        Registerbtn.Visible = false;
                //        var Canclebtn = ((Button) e.Row.Cells[6].Controls[3]);
                //        Canclebtn.Visible = false;
                //        var Editbtn = ((Button) e.Row.Cells[6].Controls[5]);
                //        Editbtn.Visible = true;

                //    }
                //    else
                //    {

                //        drpResource.ClearSelection();
                //        var dropDownList = (DropDownList)e.Row.Cells[4].Controls[1];
                //        dropDownList.Enabled = true;
                //        var Registerbtn = ((Button)e.Row.Cells[6].Controls[1]);
                //        Registerbtn.Visible = true;
                //        var Canclebtn = ((Button)e.Row.Cells[6].Controls[3]);
                //        Canclebtn.Visible = true;
                //        var Editbtn = ((Button)e.Row.Cells[6].Controls[5]);
                //        Editbtn.Visible = false;
                //    }
                //}
                if (!drpRequestTypeList.Items.FindByValue("1").Selected)
                {
                    e.Row.Enabled = false;
                    e.Row.FindControl("btnRegister").Visible = false;
                    e.Row.FindControl("btnCancle").Visible = false;
                    e.Row.FindControl("btnEdi").Visible = false;
                }
            }

        }

        protected void drpResource_SelectedIndexChanged(object sender, EventArgs e)
        {
            int reqId = Convert.ToInt32(hdnfReqId.Value);
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;

            if (ddl.SelectedIndex > 0)
            {
                string pk = grdDateTime.DataKeys[row.RowIndex].Values[0].ToString();
                int dateTimeId = Convert.ToInt32(pk);
                RequestDateTimeHandler reqdtH = new RequestDateTimeHandler();
                //int id = reqdtH.CheckOneDateTimeWithResourceId(dateTimeId, Convert.ToInt32(ddl.SelectedValue));
                var Canclebtn = ((Button)grdDateTime.Rows[row.RowIndex].Cells[6].Controls[3]);
                var Registerbtn = ((Button)grdDateTime.Rows[row.RowIndex].Cells[6].Controls[1]);
                int id = reqdtH.CheckOneDateTimeWithResourceIdPlus(dateTimeId, Convert.ToInt32(ddl.SelectedValue));
                if (id == 0)

                {
                    row.CssClass = "bg-success";
                    btnSubmitFinal.Enabled = true;

                    Registerbtn.Visible = true;
                    Canclebtn.Visible = true;
                }
                else
                {
                    row.CssClass = "bg-danger";
                    btnSubmitFinal.Enabled = false;

                    Registerbtn.Visible = false;

                    Canclebtn.Visible = true;
                    DropDownList drpResource = (DropDownList)row.FindControl("drpResource");

                    drpResource.Enabled = false;
                }
            }
            else
            {
                row.CssClass = "bg-default";
            }
            foreach (GridViewRow row1 in grdDateTime.Rows)
            {
                if (row1.RowIndex != row.RowIndex)
                {
                    row1.Enabled = false;
                }

            }
        }

        public string GetImage(int value)
        {
            if (value == 5)
            {
                return ResolveUrl("~/ResourceControl/Images/Messaging-Question-icon.png");
            }
            if (value == 4)
            {
                return ResolveUrl("~/ResourceControl/Images/informed.png");
            }
            if (value == 3)
            {
                return ResolveUrl("~/ResourceControl/Images/deny.png");
            }
            if (value == 2)
            {
                return ResolveUrl("~/ResourceControl/Images/Approved-icon.png");
            }
            if (value == 1)
            {
                return ResolveUrl("~/ResourceControl/Images/send-file-32.png");
            }
            if (value == 0)
            {
                return ResolveUrl("~/ResourceControl/Images/waiting.jpg");
            }
            else
            {
                return ResolveUrl("~/ResourceControl/Images/red_trans_question.png");
            }
        }

        protected void drpCandidateResource_SelectedIndexChanged(object sender, EventArgs e)
        {
            int reqId = Convert.ToInt32(hdnfReqId.Value);
            RequestDateTimeHandler reqdtH = new RequestDateTimeHandler();
            List<RequestDateTime> dtResult = reqdtH.CheckDateTimeListWithResourceId
                (reqId, Convert.ToInt32(drpCandidateResource.SelectedValue));
            grdDateTime.DataSource = dtResult.OrderBy(c => c.Date);
            grdDateTime.DataBind();
        }

        protected void grdDateTime_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //grdDateTime.EditIndex = e.NewEditIndex;
            //int reqId = (int)ViewState["reqId"];
            //RequestDateTimeHandler rqdateTimeH = new RequestDateTimeHandler();
            //_dateTimeList = rqdateTimeH.GetDateTimeListByRequestId(reqId);
            //grdDateTime.DataSource = _dateTimeList.OrderBy(c => c.Date);
            //grdDateTime.DataBind();

            ////Label txtDate = (Label)grdDateTime.Rows[e.NewEditIndex].FindControl("Date");
            ////string scrp = " var objCal1 = new AMIB.persianCalendar('" + txtDate.ClientID + "',{ extraInputID: '" + txtDate.ClientID + "', extraInputFormat: 'yyyy/mm/dd' });";
            ////ScriptManager.RegisterStartupScript(this, GetType(), "regp", scrp, true);
        }

        protected void grdDateTime_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Label txtDate = (Label)grdDateTime.Rows[e.RowIndex].FindControl("lblDate");
            //Label startTime = (Label)grdDateTime.Rows[e.RowIndex].FindControl("lblStartTime");
            //Label endTime = (Label)grdDateTime.Rows[e.RowIndex].FindControl("lblEndTime");
            //DropDownList drpResource = (DropDownList)grdDateTime.Rows[e.RowIndex].FindControl("drpResource");
            //TextBox datetimeId = (TextBox)grdDateTime.Rows[e.RowIndex].FindControl("lblDateTimeId");

            ////TableCell row = (TableCell)grdDateTime.Rows[e.RowIndex].Cells[5];
            ////string hdnfDateTimeId = row.f.Text;
            ////int dateTimeId = Convert.ToInt32(hdnfDateTimeId);
            //RequestDateTime reqDateTime = new RequestDateTime();
            //reqDateTime.DateTimeId = Convert.ToInt32(datetimeId.Text);
            //reqDateTime.Date = txtDate.Text;
            //reqDateTime.StartTime = TimeSpan.Parse(startTime.Text).Ticks;
            //reqDateTime.EndTime = TimeSpan.Parse(endTime.Text).Ticks;
            //reqDateTime.ResourceId = Convert.ToInt32(drpResource.SelectedValue);

            //RequestDateTimeHandler reqdtH = new RequestDateTimeHandler();
            //bool hasConflict = reqdtH.UpdateOneDateTimeRequest(reqDateTime);
            //if (hasConflict)
            //{
            //    grdDateTime.Rows[e.RowIndex].CssClass = "bg-danger";
            //}
            //else
            //{
            //    grdDateTime.Rows[e.RowIndex].CssClass = "bg-success";
            //}

            //grdDateTime.EditIndex = -1;
            //int reqId = (int)ViewState["reqId"];
            //RequestDateTimeHandler rqdateTimeH = new RequestDateTimeHandler();
            //_dateTimeList = rqdateTimeH.GetDateTimeListByRequestId(reqId);
            //grdDateTime.DataSource = _dateTimeList.OrderBy(c => c.Date);
            //grdDateTime.DataBind();
        }

        protected void grdDateTime_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdDateTime.EditIndex = -1;
            int reqId = (int)ViewState["reqId"];
            RequestDateTimeHandler rqdateTimeH = new RequestDateTimeHandler();
            _dateTimeList = rqdateTimeH.GetDateTimeListByRequestId(reqId);
            grdDateTime.DataSource = _dateTimeList.OrderBy(c => c.Date);
            grdDateTime.DataBind();
        }

        //protected void btnSubmitFinal_Click(object sender, EventArgs e)
        //{
        //    if (IsValid)
        //    {
        //        List<RequestDateTime> dateTimeList = new List<RequestDateTime>();

        //        //foreach (GridDataItem item in RadGridDateTime.MasterTableView.Items)
        //        //{
        //        //    RequestDateTime rqdt = new RequestDateTime();
        //        //    rqdt.DateTimeId = Convert.ToInt32(item["DateTimeId"].Text);
        //        //    rqdt.Date = item.Cells[1].Text;
        //        //    rqdt.StartTime = TimeSpan.Parse(item.Cells[2].Text).Ticks;
        //        //    rqdt.EndTime = TimeSpan.Parse(item.Cells[3].Text).Ticks;
        //        //    //DropDownList drpRes = (DropDownList)item.FindControl("drpResource");
        //        //    //rqdt.ResourceId = Convert.ToInt32(drpRes.SelectedValue);
        //        //    rqdt.RequestId = Convert.ToInt32(hdnfReqId.Value);
        //        //    rqdt.MayConflict = false;
        //        //    dateTimeList.Add(rqdt);
        //        //}

        //        //for (int i = 0; i < grdDateTime.Rows.Count; i++)
        //        //{
        //        //    RequestDateTime rqdt = new RequestDateTime();
        //        //    rqdt.DateTimeId = Convert.ToInt32(grdDateTime.Rows[i].Cells[5].Text);
        //        //    rqdt.Date = grdDateTime.Rows[i].Cells[1].Text;
        //        //    rqdt.StartTime = TimeSpan.Parse(grdDateTime.Rows[i].Cells[2].Text).Ticks;
        //        //    rqdt.EndTime = TimeSpan.Parse(grdDateTime.Rows[i].Cells[3].Text).Ticks;
        //        //    //DropDownList drpRes = (DropDownList)item.FindControl("drpResource");
        //        //    //rqdt.ResourceId = Convert.ToInt32(drpRes.SelectedValue);
        //        //    rqdt.RequestId = Convert.ToInt32(hdnfReqId.Value);
        //        //    rqdt.MayConflict = false;
        //        //    dateTimeList.Add(rqdt);
        //        //}
        //        RequestDateTimeHandler rqdtHandler = new RequestDateTimeHandler();
        //        bool flag = rqdtHandler.UpdateAllDateTimeRequest(dateTimeList ,(int)RequestStatus.approved);
        //    }
        //}

        [WebMethod(EnableSession = true)]
        public static string btnSubmitFinalClick(string[][] array)
        {
            List<RequestDateTime> dateTimeList = new List<RequestDateTime>();

            foreach (var item in array)
            {
                RequestDateTime rqdt = new RequestDateTime();

                rqdt.Date = item[1];
                rqdt.StartTime = TimeSpan.Parse(item[2]).Ticks;
                rqdt.EndTime = TimeSpan.Parse(item[3]).Ticks;
                rqdt.DateTimeId = Convert.ToInt32(item[5]);
                rqdt.ResourceId = Convert.ToInt32(item[4]);
                rqdt.RequestId = Convert.ToInt32(item[7]);
                rqdt.MayConflict = false;
                dateTimeList.Add(rqdt);
            }

            RequestDateTimeHandler rqdtHandler = new RequestDateTimeHandler();
            var roleId = Convert.ToInt32(HttpContext.Current.Session["RoleId"]);
            RequestDateTimeHandler reqdtH = new RequestDateTimeHandler();


                bool flag = rqdtHandler.UpdateAllDateTimeRequestForDefence(dateTimeList, (int)RequestStatus.approved, userID.ToString(), roleId);
                if (flag)
                {
                    RequestHandler reqH = new RequestHandler();
                    reqH.UpdateRequestAnswerTimeByIdrequest(dateTimeList[0].RequestId);

                    return "success";
                }
                else
                {
                    return "error";
                }
            

        }

        protected void vldSubmitDatetime_ServerValidate(object source, ServerValidateEventArgs args)
        {
            foreach (GridViewRow item in grdDateTime.Rows)
            {
                if (item.CssClass.Contains("bg-danger"))
                {
                    args.IsValid = false;
                    return;
                }

                DropDownList drpResource = (DropDownList)item.FindControl("drpResource");
                if (drpRequestTypeList.SelectedIndex < 0)
                {
                    args.IsValid = false;
                    return;
                }
            }

            args.IsValid = true;
        }

        protected void btnDenyRequest_Click(object sender, EventArgs e)
        {
            userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
            if (IsValid)
            {
                RequestFR req = new RequestFR();

                userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
                req.ID = Convert.ToInt32(hdnfDenyReqId.Value);
                req.ReplierID = userID;//education user ID
                req.Answernote = txtDenyMessage.Text;
                req.Answer_time = DateTime.Now.ToPeString();
                req.Status = 3;//3 means request has been denied .

                RequestHandler requestBusiness = new RequestHandler();
                int id = requestBusiness.DenyRequest(req);
                var comman = new CommonBusiness();
                comman.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 125, string.Format("{0} :{1}", "رد درخواست کلاس توسط اداری", txtDenyMessage.Text), req.ID);
                txtDenyMessage.Text = string.Empty;

                string denyMessage = "درخواست شماره " + id.ToString() + " رد شد.";
                RadWindowManager1.RadAlert(denyMessage, 300, 100, "پیام سیستم", "closeRadWindow2");
            }
        }

        protected void btnCancelRequest_Click(object sender, EventArgs e)
        {
            hdnfDenyReqId.Value = hdnfReqId.Value.ToString();

            string scrp = "function f(){$find(\"" + RadWindow2.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
        }

        public string generaterandomstr()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        protected void btnRefreshGrid_Click(object sender, EventArgs e)
        {
            GridBind(Convert.ToInt32(drpRequestTypeList.SelectedValue));
        }

        protected void grdRequestList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }



        protected void grdDateTime_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Register")
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;

                var txtDate = ((Label)grdDateTime.Rows[rowIndex].Cells[1].Controls[1]).Text;
                var startTime = ((Label)grdDateTime.Rows[rowIndex].Cells[2].Controls[1]).Text;
                var endTime = ((Label)grdDateTime.Rows[rowIndex].Cells[3].Controls[1]).Text;
                var drpResource = Convert.ToInt32(((DropDownList)grdDateTime.Rows[rowIndex].Cells[4].Controls[1]).SelectedValue);
                var datetimeId = ((TextBox)grdDateTime.Rows[rowIndex].Cells[5].Controls[1]).Text;

                RequestDateTime reqDateTime = new RequestDateTime();
                reqDateTime.DateTimeId = Convert.ToInt32(datetimeId);
                reqDateTime.Date = txtDate;
                reqDateTime.StartTime = TimeSpan.Parse(startTime).Ticks;
                reqDateTime.EndTime = TimeSpan.Parse(endTime).Ticks;
                reqDateTime.ResourceId = Convert.ToInt32(drpResource);

                RequestDateTimeHandler reqdtH = new RequestDateTimeHandler();
                bool hasConflict = reqdtH.UpdateOneDateTimeRequest(reqDateTime);
                if (hasConflict)
                {
                    grdDateTime.Rows[rowIndex].CssClass = "bg-danger";
                    var dropDownList = (DropDownList)grdDateTime.Rows[rowIndex].Cells[4].Controls[1];
                    dropDownList.Enabled = true;
                    var Registerbtn = ((Button)grdDateTime.Rows[rowIndex].Cells[6].Controls[1]);
                    Registerbtn.Visible = true;
                    var Canclebtn = ((Button)grdDateTime.Rows[rowIndex].Cells[6].Controls[3]);
                    Canclebtn.Visible = true;
                    var Editbtn = ((Button)grdDateTime.Rows[rowIndex].Cells[6].Controls[5]);
                    Editbtn.Visible = false;
                }
                else
                {
                    grdDateTime.Rows[rowIndex].CssClass = "bg-success";
                    var dropDownList = (DropDownList)grdDateTime.Rows[rowIndex].Cells[4].Controls[1];
                    dropDownList.Enabled = false;
                    var Registerbtn = ((Button)grdDateTime.Rows[rowIndex].Cells[6].Controls[1]);
                    Registerbtn.Visible = false;
                    var Canclebtn = ((Button)grdDateTime.Rows[rowIndex].Cells[6].Controls[3]);
                    Canclebtn.Visible = false;
                    var Editbtn = ((Button)grdDateTime.Rows[rowIndex].Cells[6].Controls[5]);
                    Editbtn.Visible = true;
                }

                grdDateTime.EditIndex = -1;
                int reqId = (int)ViewState["reqId"];
                RequestDateTimeHandler rqdateTimeH = new RequestDateTimeHandler();
                _dateTimeList = rqdateTimeH.GetDateTimeListByRequestId(reqId);
                grdDateTime.DataSource = _dateTimeList.OrderBy(c => c.Date);
                grdDateTime.DataBind();



            }
            if (e.CommandName == "Cancle")
            {
                grdDateTime.EditIndex = -1;
                int reqId = (int)ViewState["reqId"];
                RequestDateTimeHandler rqdateTimeH = new RequestDateTimeHandler();
                _dateTimeList = rqdateTimeH.GetDateTimeListByRequestId(reqId);
                grdDateTime.DataSource = _dateTimeList.OrderBy(c => c.Date);
                grdDateTime.DataBind();

                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;


                var datetimeId = Convert.ToInt32(((TextBox)grdDateTime.Rows[rowIndex].Cells[5].Controls[1]).Text);
                RequestDateTimeHandler reqdtH = new RequestDateTimeHandler();
                var hasResource = reqdtH.GetDateTimeListByRequestId(reqId).FirstOrDefault(c => c.DateTimeId == datetimeId).ResourceId;


                if (hasResource != 0)
                {
                    var dropDownList = (DropDownList)grdDateTime.Rows[rowIndex].Cells[4].Controls[1];
                    dropDownList.Enabled = false;
                    var Registerbtn = ((Button)grdDateTime.Rows[rowIndex].Cells[6].Controls[1]);
                    Registerbtn.Visible = false;
                    var Canclebtn = ((Button)grdDateTime.Rows[rowIndex].Cells[6].Controls[3]);
                    Canclebtn.Visible = false;
                    var Editbtn = ((Button)grdDateTime.Rows[rowIndex].Cells[6].Controls[5]);
                    Editbtn.Visible = true;

                }

            }
            if (e.CommandName == "Edit")
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;
                var Registerbtn = ((Button)grdDateTime.Rows[rowIndex].Cells[6].Controls[1]);
                Registerbtn.Visible = true;
                var Canclebtn = ((Button)grdDateTime.Rows[rowIndex].Cells[6].Controls[3]);
                Canclebtn.Visible = true;
                var Editbtn = ((Button)grdDateTime.Rows[rowIndex].Cells[6].Controls[5]);
                Editbtn.Visible = false;
                var dropDownList = (DropDownList)grdDateTime.Rows[rowIndex].Cells[4].Controls[1];
                dropDownList.Enabled = true;
                foreach (GridViewRow row in grdDateTime.Rows)
                {
                    if (row.RowIndex != rowIndex)
                    {
                        row.Enabled = false;
                    }

                }
            }

        }
    }
}