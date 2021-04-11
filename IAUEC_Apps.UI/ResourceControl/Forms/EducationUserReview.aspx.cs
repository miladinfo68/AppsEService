using ResourceControl.Entity;
using ResourceControl.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Web.Services;
using IAUEC_Apps.Business.Common;


namespace ResourceControl.PL.Forms
{
    public partial class EducationUser : System.Web.UI.Page
    {
        private List<RequestFR> _reqlist = null;
        private List<ResourceControl.Entity.Resource> _resList = null;
        private List<RequestDateTime> _dateTimeList;
        int userID;
        int daneshID;//daneshkade id has to come from session.

        static string userIDStr;

        public EducationUser()
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
            daneshID = Convert.ToInt32(Session["DaneshId"]);
            int roleId;
            // if (userID == 1)
            //{
            //    drpRequestTypeList.Items.Clear();
            //    drpRequestTypeList.Items.Add("درخواستهای منتظر ارجاع");
            //    drpRequestTypeList.Items.Add("درخواستهای منتظر تایید");
            //    drpRequestTypeList.Items.Add("درخواستهای تایید شده");
            //    drpRequestTypeList.Items.Add("درخواستهای اطلاع رسانی شده");
            //    drpRequestTypeList.Items.Add("درخواستهای رد شده");
            //     drpRequestTypeList.Items.Add("درخواستهای از دست رفته");

            //}
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
                    roleId = Convert.ToInt32(Session["roleID"]);
                    if (UtilityFunction.IsMasouleDaftarUser(roleId))
                    {
                        if (Session["StausLinke"] != null)
                        {
                            drpRequestTypeList.ClearSelection();
                            drpRequestTypeList.Items.FindByValue(Session["StausLinke"] as string).Selected = true;
                            GridBindForMasoulDaftar(roleId, Convert.ToInt32(Session["StausLinke"]));


                        }
                        else
                        {
                            drpRequestTypeList.ClearSelection();
                            drpRequestTypeList.Items.FindByValue("1").Selected = true;
                            AvoidDrpRequestForMasoulDaftar();
                            GridBindForMasoulDaftar(roleId, 1);
                        }

                    }
                    else
                    {
                        if (Session["StausLinke"] != null)
                        {
                            drpRequestTypeList.ClearSelection();
                            drpRequestTypeList.Items.FindByValue(Session["StausLinke"] as string).Selected = true;

                            if (roleId == 1)
                                GridBindForAdmin(roleId, Convert.ToInt32(Session["StausLinke"]));
                            else
                                GridBind(Convert.ToInt32(Session["StausLinke"]));

                        }
                        else
                        {
                            if (roleId == 1)
                                GridBindForAdmin(roleId, 0);
                            else
                                GridBind(0);
                        }
                    }
                }
                userIDStr = Session[sessionNames.userID_Karbar].ToString();

            }
            var rolId = -1;
            if (Session["roleID"] != null)
                rolId = Convert.ToInt32(Session["roleID"]);
            if (rolId == 1)//admin
            {
                drpRequestTypeList.Items.FindByValue("5").Text = "لیست درخواستهای از دست رفته";
                drpRequestTypeList.Items.FindByValue("7").Enabled = false;
            }

        }

        private void GridBindForAdmin(int roleId, int state)
        {
            var requestHandler = new RequestHandler();
            if (requestHandler.GetRequestListBystatusForAdmin(state) != null)
                grdListOfRequest.DataSource = requestHandler.GetRequestListBystatusForAdmin(state).Where(x => x.Subject != "StudentDefence" || string.IsNullOrEmpty(x.Subject)).ToList();
            else
                grdListOfRequest.DataSource = requestHandler.GetRequestListBystatusForAdmin(state);
            grdListOfRequest.DataBind();
            var dataControlField1 = (DataControlField)grdListOfRequest.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "مشاهده");
            if (dataControlField1 != null)
                dataControlField1.Visible = true;
            if (drpRequestTypeList.Items.FindByValue("0").Selected || drpRequestTypeList.Items.FindByValue("3").Selected)
            {
                var controlField = (DataControlField)grdListOfRequest.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "بررسی");
                if (controlField != null)
                    controlField.Visible = true;
            }
            else
            {
                var controlField = (DataControlField)grdListOfRequest.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "بررسی");
                if (controlField != null)
                    controlField.Visible = false;
            }
            if (state == 2)
            {
                var dataControlField = (DataControlField)grdListOfRequest.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "اطلاع رسانی");
                if (dataControlField != null)
                    dataControlField.Visible = true;
                var controlField = (DataControlField)grdListOfRequest.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "حذف درخواست");
                if (controlField != null)
                    controlField.Visible = true;
            }
            else
            {
                var dataControlField = (DataControlField)grdListOfRequest.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "اطلاع رسانی");
                if (dataControlField != null)
                    dataControlField.Visible = false;
                var controlField = (DataControlField)grdListOfRequest.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "حذف درخواست");
                if (controlField != null)
                    controlField.Visible = false;
            }

            if (state == 0 || state == 3)
            {
                var dataControlField = (DataControlField)grdListOfRequest.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "بررسی");
                if (dataControlField != null)
                    dataControlField.Visible = true;
                var controlField = (DataControlField)grdDateTime.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "عملیات");
                if (controlField != null)
                    controlField.Visible = true;
            }
            else
            {
                var dataControlField = (DataControlField)grdListOfRequest.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "بررسی");
                if (dataControlField != null)
                    dataControlField.Visible = false;
                var controlField = (DataControlField)grdDateTime.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "عملیات");
                if (controlField != null)
                    controlField.Visible = false;
            }

        }

        private void GridBindForMasoulDaftar(int roleId, int state)
        {

            var requestHandler = new RequestHandler();
            if ((requestHandler.GetRequestListByIssuerID(userID) != null && state != 6) || (requestHandler.GetRequestListByIssuerID(userID) != null && state == 6))
            {
                grdListOfRequest.DataSource = (state != 6 ? requestHandler.GetRequestListByIssuerID(userID).Where(r => r.Status == state).ToList() : requestHandler.GetRequestListByIssuerID(userID)).Where(x => x.Subject != "StudentDefence").ToList();
                grdListOfRequest.DataBind();
            }
            var dataControlField = (DataControlField)grdListOfRequest.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "مشاهده");
            if (dataControlField != null)
                dataControlField.Visible = true;
            var controlField = (DataControlField)grdListOfRequest.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "بررسی");
            if (controlField != null)
                controlField.Visible = false;
            drpRequestTypeList.Items.FindByValue("0").Enabled = false;
        }

        private void AvoidDrpRequestForMasoulDaftar()
        {
            drpRequestTypeList.Items.FindByValue("0").Enabled = false;
            drpRequestTypeList.Items.FindByValue("4").Enabled = false;
            drpRequestTypeList.Items.FindByValue("5").Enabled = false;
            //drpRequestTypeList.Items.FindByValue("5").Text = "لیست درخواستهای از دست رفته";
            drpRequestTypeList.Items.FindByValue("7").Enabled = false;
            drpRequestTypeList.Items.FindByValue("8").Enabled = false;
        }

        private void GridBind(int status)
        {
            int daneshID = Convert.ToInt32(Session["DaneshId"]);
            RequestHandler rq = new RequestHandler();
            //_reqlist = new List<RequestFR>();
            if (daneshID != 0)
            {
                if (status != 6)
                {
                    if (status == 7)
                    {
                        _reqlist = rq.GetRequestListBystatusAnddaneshID(5, daneshID).Where(c => c.Status == 1).ToList();
                    }
                    else if (status != 8)
                        _reqlist = status == 5 ? rq.GetRequestListBystatusAnddaneshID(status, daneshID).Where(c => c.Status == 0).ToList() : rq.GetRequestListBystatusAnddaneshID(status, daneshID);
                    else
                    {
                        _reqlist = rq.GetDeletedRequestListByDaneshID(daneshID);
                    }

                }
                else
                {
                    //  var myList = rq.GetRequestListBystatusAnddaneshID(5, daneshID).Where(c => c.Status == 1).ToList();
                    //  var dataSource= rq.GetRequestListByDaneshID(daneshID);
                    //var data=  dataSource.Except(myList);
                    var data = new List<RequestFR>();
                    //for (var i = 0; i < 6; i++)
                    //{
                    //    if (i == 5)
                    //    {
                    //        data.AddRange(rq.GetRequestListBystatusAnddaneshID(i, daneshID).Where(c => c.Status == 0));
                    //    }
                    //    else
                    //    {
                    //        data.AddRange(rq.GetRequestListBystatusAnddaneshID(i, daneshID));

                    //    }
                    //}
                    for (int i = 0; i < 6; i++)
                    {
                        var items = rq.GetRequestListBystatusAnddaneshID(i, daneshID);
                        if (items != null)
                            data.AddRange(items);
                    }

                    _reqlist = data.ToList();
                }
            }
            else
            {
                if (status != 6)
                {
                    _reqlist = rq.GetRequestListByStatus(status);
                }
                else
                {
                    _reqlist = rq.GetRequestListOfTermJari();
                }
            }

            if (status == 2)
            {
                var dataControlField = (DataControlField)grdListOfRequest.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "اطلاع رسانی");
                if (dataControlField != null)
                    dataControlField.Visible = true;
                var controlField = (DataControlField)grdListOfRequest.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "حذف درخواست");
                if (controlField != null)
                    controlField.Visible = true;
            }
            else
            {
                var dataControlField = (DataControlField)grdListOfRequest.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "اطلاع رسانی");
                if (dataControlField != null)
                    dataControlField.Visible = false;
                var controlField = (DataControlField)grdListOfRequest.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "حذف درخواست");
                if (controlField != null)
                    controlField.Visible = false;
            }

            if (status == 0 || status == 3)
            {
                var dataControlField = (DataControlField)grdListOfRequest.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "بررسی");
                if (dataControlField != null)
                    dataControlField.Visible = true;
                var controlField = (DataControlField)grdDateTime.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "عملیات");
                if (controlField != null)
                    controlField.Visible = true;
            }
            else
            {
                var dataControlField = (DataControlField)grdListOfRequest.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "بررسی");
                if (dataControlField != null)
                    dataControlField.Visible = false;
                var controlField = (DataControlField)grdDateTime.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "عملیات");
                if (controlField != null)
                    controlField.Visible = false;
            }
            if (_reqlist!=null)
            {
                _reqlist= _reqlist.Where(x => x.Subject == null || x.Subject != "StudentDefence").ToList();
            }

            grdListOfRequest.DataSource = _reqlist;
            //if (drpRequestTypeList.SelectedValue == 2.ToString())
            //{
            //    //grdListOfRequest.Columns[7].Visible = false;
            //}
            grdListOfRequest.DataBind();
        }
        //******************
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
        //*****************
        protected void drpRequestTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int status = Convert.ToInt32(drpRequestTypeList.SelectedValue);
            var roleId = Convert.ToInt32(Session["roleID"]);
            if (UtilityFunction.IsMasouleDaftarUser(roleId))
                GridBindForMasoulDaftar(roleId, status);
            else
            {
                if (roleId == 1)
                {
                    GridBindForAdmin(roleId, status);
                    if (status == 1)
                    {
                        FillLinkAndStatusForClick(status);
                        //  FillLinkAndStatusForClickOffice(1);
                    }
                    else
                    {
                        FillLinkAndStatusForClick(status);
                    }

                }
                else
                    GridBind(status);
            }
        }

        protected void grdListOfRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int status = Convert.ToInt32(drpRequestTypeList.SelectedValue);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int rowindex = e.Row.RowIndex;

                ResolveImageUrl(e);
            }
        }

        protected void grdListOfRequest_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int reqId = Convert.ToInt32(e.CommandArgument);
            ViewState.Add("reqId", reqId);
            if (e.CommandName == "checkReq")
            {
                hdnfReqId.Value = reqId.ToString();

                List<ResourceControl.Entity.Resource> resList = new List<ResourceControl.Entity.Resource>();
                ResourceHandler rsh = new ResourceHandler();
                _resList = rsh.GetResourceListByReqID(reqId);
                ViewState.Add("reqId", reqId);
                ViewState.Add("_resList", _resList);

                if (drpRequestTypeList.SelectedIndex == 0 || drpRequestTypeList.SelectedIndex == 3)
                {
                    drpCandidateResource.DataSource = _resList;
                    drpCandidateResource.DataTextField = "name";
                    drpCandidateResource.DataValueField = "ID";
                    drpCandidateResource.DataBind();
                    drpCandidateResource.Items.Insert(0, new ListItem { Value = "0", Text = "انتخاب کنید..." });

                    dvSuggestResource.Visible = true;
                }
                else
                {
                    dvSuggestResource.Visible = false;
                }

                if (drpRequestTypeList.SelectedIndex == 5)
                {
                    drpCandidateResource.ClearSelection();
                    dvOperation.Visible = false;
                }

                grdDateTime.EditIndex = -1;
                RequestDateTimeHandler rqdateTimeH = new RequestDateTimeHandler();
                _dateTimeList = rqdateTimeH.GetDateTimeListByRequestId(reqId);
                grdDateTime.DataSource = _dateTimeList;
                grdDateTime.DataBind();

                string scrp = "function f(){$find(\"" + RadWindow1.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
            }

            if (e.CommandName == "info")
            {
                RequestHandler reqHandler = new RequestHandler();
                int counter = reqHandler.InformUserOfRequestByReqId(reqId, Session[sessionNames.userID_Karbar].ToString());
                string msg = null;
                if (counter > 0)
                {
                    msg = "درخواست شماره " + reqId + " با موفقیت اطلاع رسانی شد.";
                }
                else
                {
                    msg = "خطا در اطلاع رسانی درخواست ، لطفا دوباره تلاش کنید.";
                }
                RadWindowManager1.RadAlert(msg, 300, 100, "پیام سیستم", "refresgGrid");
            }

            if (e.CommandName == "edit")
            {
                Response.Redirect("UserEditRequest.aspx?id=" + generaterandomstr() + "@A" + "0" + "-" + generaterandomstr() + "&reqId=" + e.CommandArgument);
            }
            if (e.CommandName == "delete")
            {
                RequestHandler rh = new RequestHandler();
                rh.DeleteRequest(reqId);
                //log
                var commanBusiness = new CommonBusiness();
                userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
                //124=حذف درخواست کلاس توسط آموزش
                commanBusiness.InsertIntoUserLog(userID, "", 11, 124, "حذف درخواست کلاس توسط آموزش", reqId);
                GridBind(Convert.ToInt32(drpRequestTypeList.SelectedValue));
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
                        lblStatue.Text = "درخواست شمااطلاع رسانی شده است";
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

                if (drpRequestTypeList.SelectedItem.Value == 2.ToString() ||
                    drpRequestTypeList.SelectedItem.Value == 4.ToString())
                {
                    tdRangeOfDate.Visible = false;
                    tdGrdResult.Visible = true;

                    _dateTimeList = rqdateTimeH.GetDateTimeListByRequestId(reqId);
                    grdResult.DataSource = _dateTimeList.OrderBy(c => c.Date);
                    grdResult.DataBind();
                }
                else
                {
                    tdRangeOfDate.Visible = true;
                    tdGrdResult.Visible = false;

                }

                string scrp = "function f(){$find(\"" + RadWindow3.ClientID +
                              "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);


                #endregion

            }
            if (e.CommandName == "History")
            {

                CommonBusiness cmb = new CommonBusiness();

                lst_history.DataSource = cmb.GetUserLogByModifyId(int.Parse(e.CommandArgument.ToString()), 11);
                lst_history.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }

        protected void grdDateTime_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "submit")
            {
                int reqId = (int)ViewState["reqId"];
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                DropDownList drpResource = row.FindControl("drpResource") as DropDownList;
                Button btnSingleSubmit = row.FindControl("btnSingleSubmit") as Button;
                if (drpResource.SelectedValue != "0")
                {
                    RequestDateTime rqdt = new RequestDateTime();
                    RequestDateTimeHandler rqdateTimeH = new RequestDateTimeHandler();
                    List<RequestDateTime> dateTimeList = new List<RequestDateTime>();
                    _dateTimeList = rqdateTimeH.GetDateTimeListByRequestId(reqId);
                    rqdt.Date = _dateTimeList[row.DataItemIndex].Date;
                    rqdt.StartTime = _dateTimeList[row.DataItemIndex].StartTime;
                    rqdt.EndTime = _dateTimeList[row.DataItemIndex].EndTime;
                    rqdt.DateTimeId = _dateTimeList[row.DataItemIndex].DateTimeId;
                    rqdt.ResourceId = Convert.ToInt32(drpResource.SelectedValue);
                    rqdt.RequestId = reqId;
                    rqdt.MayConflict = false;
                    dateTimeList.Add(rqdt);
                    RequestDateTimeHandler rqdtHandler = new RequestDateTimeHandler();
                    bool flag = rqdtHandler.UpdateAllDateTimeRequest(dateTimeList, (int)RequestStatus.sent, userIDStr);

                    btnSingleSubmit.Enabled = false;
                    string msg = "کلاسی با موفقیت ثبت شد";
                    RadWindowManager1.RadAlert(msg, 300, 100, "پیام سیستم", "");
                }
                else
                {
                    string msg = "کلاسی را پیشنهاد نداده اید";
                    RadWindowManager1.RadAlert(msg, 300, 100, "پیام سیستم", "");
                }
            }
        }

        protected void grdListOfRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdListOfRequest.PageIndex = e.NewPageIndex;
            int status = Convert.ToInt32(drpRequestTypeList.SelectedValue);
            if (drpRequestTypeList.SelectedIndex == 1 || drpRequestTypeList.SelectedIndex == 2 || drpRequestTypeList.SelectedIndex == 4)
            {

                var dataControlField = (DataControlField)grdListOfRequest.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "لغو درخواست");
                if (dataControlField != null)
                    dataControlField.Visible = true;
            }
            else
            {
                var dataControlField = (DataControlField)grdListOfRequest.Columns.Cast<DataControlField>().SingleOrDefault(fld => fld.HeaderText == "لغو درخواست");
                if (dataControlField != null)
                    dataControlField.Visible = false;
            }
            var rolId = -1;
            if (Session["roleID"] != null)
                rolId = Convert.ToInt32(Session["roleID"]);
            if (UtilityFunction.IsMasouleDaftarUser(rolId))
            {
                GridBindForMasoulDaftar(rolId, status);
            }
            else
            {
                if (rolId == 1)//admin
                {
                    GridBindForAdmin(rolId, status);
                }
                else
                {
                    GridBind(status);
                }
            }

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
                    img.ToolTip = "رد شده";
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
                RequestDateTime reqDateTime = (RequestDateTime)e.Row.DataItem;
                DropDownList drpResource = (DropDownList)e.Row.FindControl("drpResource");

                _resList = (List<ResourceControl.Entity.Resource>)ViewState["_resList"];
                drpResource.DataSource = _resList;
                drpResource.DataTextField = "name";
                drpResource.DataValueField = "ID";
                drpResource.DataBind();
                drpResource.Items.Insert(0, new ListItem { Value = "0", Text = "انتخاب کنید..." });

                if (drpRequestTypeList.SelectedValue == "0" || drpRequestTypeList.SelectedValue == "3")
                {
                    drpResource.Items.FindByValue(drpCandidateResource.SelectedValue).Selected = true;
                    if (drpCandidateResource.SelectedIndex > 0)
                    {
                        if (!reqDateTime.MayConflict)
                        {
                            if (e.Row.RowState != DataControlRowState.Edit)
                            {
                                e.Row.CssClass = "bg-success";
                                drpResource.Enabled = false;
                            }
                        }
                        else
                        {
                            e.Row.CssClass = "bg-danger";
                            drpResource.Enabled = true;
                            //btnSubmitFinal.Enabled = false;
                        }
                    }
                }
                else
                {
                    drpResource.Items.FindByValue(reqDateTime.ResourceId.ToString()).Selected = true;
                    drpResource.Enabled = false;
                }

                if (drpResource.SelectedValue == "0")
                {
                    e.Row.CssClass = "bg-default";
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
                int id = reqdtH.CheckOneDateTimeWithResourceId(dateTimeId, Convert.ToInt32(ddl.SelectedValue));

                if (id == 0)
                {
                    row.CssClass = "bg-success";
                }
                else
                {
                    row.CssClass = "bg-danger";
                    //btnSubmitFinal.Enabled = false;
                }
            }
            else
            {
                row.CssClass = "bg-default";
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
            grdDateTime.DataSource = dtResult;
            grdDateTime.DataBind();
        }

        protected void grdDateTime_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdDateTime.EditIndex = e.NewEditIndex;
            int reqId = (int)ViewState["reqId"];
            RequestDateTimeHandler rqdateTimeH = new RequestDateTimeHandler();
            _dateTimeList = rqdateTimeH.GetDateTimeListByRequestId(reqId);
            grdDateTime.DataSource = _dateTimeList;
            grdDateTime.DataBind();

            TextBox txtDate = (TextBox)grdDateTime.Rows[e.NewEditIndex].FindControl("txtDate");
            string scrp = " var objCal1 = new AMIB.persianCalendar('" + txtDate.ClientID + "',{ extraInputID: '" + txtDate.ClientID + "', extraInputFormat: 'yyyy/mm/dd' });";
            ScriptManager.RegisterStartupScript(this, GetType(), "regp", scrp, true);
        }

        protected void grdDateTime_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox txtDate = (TextBox)grdDateTime.Rows[e.RowIndex].FindControl("txtDate");
            RadTimePicker startTime = (RadTimePicker)grdDateTime.Rows[e.RowIndex].FindControl("RadTimePicker1");
            RadTimePicker endTime = (RadTimePicker)grdDateTime.Rows[e.RowIndex].FindControl("RadTimePicker2");
            DropDownList drpResource = (DropDownList)grdDateTime.Rows[e.RowIndex].FindControl("drpResource");
            TextBox datetimeId = (TextBox)grdDateTime.Rows[e.RowIndex].FindControl("lblDateTimeId");

            //TableCell row = (TableCell)grdDateTime.Rows[e.RowIndex].Cells[5];
            //string hdnfDateTimeId = row.f.Text;
            //int dateTimeId = Convert.ToInt32(hdnfDateTimeId);
            RequestDateTime reqDateTime = new RequestDateTime();
            reqDateTime.DateTimeId = Convert.ToInt32(datetimeId.Text);
            reqDateTime.Date = txtDate.Text;
            reqDateTime.StartTime = startTime.SelectedTime.Value.Ticks;
            reqDateTime.EndTime = endTime.SelectedTime.Value.Ticks;
            reqDateTime.ResourceId = Convert.ToInt32(drpResource.SelectedValue);

            RequestDateTimeHandler reqdtH = new RequestDateTimeHandler();
            bool hasConflict = reqdtH.UpdateOneDateTimeRequest(reqDateTime);
            if (hasConflict)
            {
                grdDateTime.Rows[e.RowIndex].CssClass = "bg-danger";
            }
            else
            {
                grdDateTime.Rows[e.RowIndex].CssClass = "bg-success";
            }

            grdDateTime.EditIndex = -1;
            int reqId = (int)ViewState["reqId"];
            RequestDateTimeHandler rqdateTimeH = new RequestDateTimeHandler();
            _dateTimeList = rqdateTimeH.GetDateTimeListByRequestId(reqId);
            grdDateTime.DataSource = _dateTimeList;
            grdDateTime.DataBind();
        }

        protected void grdDateTime_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdDateTime.EditIndex = -1;
            int reqId = (int)ViewState["reqId"];
            RequestDateTimeHandler rqdateTimeH = new RequestDateTimeHandler();
            _dateTimeList = rqdateTimeH.GetDateTimeListByRequestId(reqId);
            grdDateTime.DataSource = _dateTimeList;
            grdDateTime.DataBind();
        }

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
                rqdt.RequestId = Convert.ToInt32(item[6]);
                rqdt.MayConflict = false;
                dateTimeList.Add(rqdt);
            }

            RequestDateTimeHandler rqdtHandler = new RequestDateTimeHandler();
            bool flag = rqdtHandler.UpdateAllDateTimeRequest(dateTimeList, (int)RequestStatus.sent, userIDStr);
            if (flag)
            {
                return "success";
            }
            else
            {
                return "error";
            }
        }

        protected void vldSubmitDatetime_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (drpCandidateResource.SelectedValue == "0")
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
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void btnDenyRequest_Click(object sender, EventArgs e)
        {
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
                comman.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 124, string.Format("{0} :{1}", "حذف درخواست کلاس توسط آموزش", txtDenyMessage.Text), req.ID);
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

        protected void grdListOfRequest_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}