using ResourceControl.BLL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using Telerik.Web.UI;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.ResourceControl;


namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class DeletedRequests : System.Web.UI.Page
    {

        List<Daneshkade> dnshList = new List<Daneshkade>();
        private List<RequestFR> _reqlist = null;
        int daneshID;
        private List<RequestDateTime> _dateTimeList;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                string status = Request.QueryString["status"];
                string scrp = "var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_pcal1', {extraInputID: 'ContentPlaceHolder1_pcal1',extraInputFormat: 'yyyy/mm/dd'}); ";
                ScriptManager.RegisterStartupScript(this, GetType(), ClientID, scrp, true);
            }
        }

        protected void grdDeletedRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdDeletedRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int rowindex = e.Row.RowIndex;


            }
        }

        protected void grdDeletedRequest_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void grdDeletedRequest_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int reqId = Convert.ToInt32(e.CommandArgument);


            if (e.CommandName == "showInfo")
            {
                #region ShowInfo

                hdnfReqId.Value = reqId.ToString();
                RequestHandler rh = new RequestHandler();
                GridViewRow curruntRow = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                //   imgStatus2.ImageUrl = ((Image)curruntRow.Cells[2].FindControl("imgStatus")).ImageUrl;
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
                // var resOptJunlist = rsopt.GetReq_Opt_JuncListByReqID(requestDetails.ID);
                var requestOption = new List<Option>();
                //foreach (Req_Opt_Junc optJunc in resOptJunlist)
                //{
                //    foreach (var option in optlist)
                //    {
                //        if (optJunc.Opt_id == option.ID)
                //            requestOption.Add(new Option { Name = option.Name });
                //    }
                //}
                grdFacilities.DataSource = requestOption;
                grdFacilities.DataBind();
                string scrp = "function f(){$find(\"" + RadWindow3.ClientID +
                              "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);


                #endregion
            }
        }

        protected void grdDeletedRequest_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void BindGrid()
        {


            RequestHandler rq = new RequestHandler();
            _reqlist = rq.GetDeletedRequestListOfTermJari();/*.Where(x=>x.Issue_time == pcal1.Text).ToList();*/
            grdDeletedRequest.DataSource = _reqlist;
            grdDeletedRequest.DataBind();
            List<string> issuerNames = new List<string>();
            foreach (var x in _reqlist)
            {

                issuerNames.Add(x.IssuerName);
            }

            //drpIssureName.DataSource = issuerNames;
            //drpIssureName.DataBind();
            //drpIssureName.Items.Insert(0, "انتخاب کنید");

            Daneshkade item = new Daneshkade();
            item.NameDanesh = "--انتخاب کنید--";
            item.ID = -1;
            DaneshkadeHandler dnsh = new DaneshkadeHandler();
            dnshList = dnsh.GetAllDaneshkade();
            dnshList.Insert(0, item);
            //  drpChooseDanshkade.Items.Clear();
            drpChooseDanshkade.DataSource = dnshList;
            drpChooseDanshkade.DataTextField = "NameDanesh";
            drpChooseDanshkade.DataValueField = "ID";
            drpChooseDanshkade.DataBind();

            RC_UserHandler usrH = new RC_UserHandler();
            daneshID = Convert.ToInt32(Session["DaneshId"]);
            List<RC_User> listostad = usrH.GetOstadListByDaneshID(daneshID);//need to get list of ostad of one daneshkade
            if (listostad != null)
                for (int i = 0; i <= listostad.Count - 1; i++)
                {
                    listostad[i].Name = listostad[i].Name.Replace("ي", "ی");
                }

            RadComboBoxField.DataSource = listostad;
            RadComboBoxField.DataTextField = "name";
            RadComboBoxField.DataValueField = "ID";
            RadComboBoxField.DataBind();
            RadComboBoxField.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("جستجو و انتخاب کنید"));


        }




        protected void btnSubmit_Click(object sender, EventArgs e)

        {
            if (Page.IsValid)
            {
                MyBindGrid(txtRequestNumber.Text.Trim(), txtResourceNumber.Text, pcal1.Text.Trim(), drpChooseDanshkade.SelectedValue, RadComboBoxField.SelectedValue);
                //Label1.Text = string.Format("لیست درخواست ها} ", pcal1.Text);
                string scrp = "var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_pcal1', {extraInputID: 'ContentPlaceHolder1_pcal1',extraInputFormat: 'yyyy/mm/dd'}); ";
                ScriptManager.RegisterStartupScript(this, GetType(), ClientID, scrp, true);
            }
        }

        private void MyBindGrid(string requestNumber, string resourceNumber, string date, string daneshId, string issureId)
        {
            RequestHandler rq = new RequestHandler();

            _reqlist = rq.GetDeletedRequestListOfTermJari();

            if (!string.IsNullOrEmpty(requestNumber))
            {
                _reqlist = _reqlist.Where(s => s.ID == Convert.ToInt32(requestNumber)).ToList();
            }
            if (!string.IsNullOrEmpty(resourceNumber))
            {
                _reqlist = _reqlist.Where(s => s.CourseDID == Convert.ToInt32(resourceNumber)).ToList();
            }
            if (!string.IsNullOrEmpty(date))
            {
                _reqlist = _reqlist.Where(s => s.Issue_time == date).ToList();
            }
            if (daneshId != "-1")
            {
                _reqlist = _reqlist.Where(s => s.DaneshID == Convert.ToInt32(daneshId)).ToList();
            }
            if (!string.IsNullOrEmpty(issureId))
            {
                _reqlist = _reqlist.Where(s => s.IssuerID == Convert.ToInt32(issureId)).ToList();
            }
            grdDeletedRequest.DataSource = _reqlist;
            grdDeletedRequest.DataBind();
        }

        protected void drpChooseDanshkade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpChooseDanshkade.SelectedIndex > 0)
            {
                //RadComboBoxField.Enabled = true;
                int daneshID = Convert.ToInt32(Session["DaneshId"]);
                RC_UserHandler usrH = new RC_UserHandler();
                List<RC_User> listostad = usrH.GetOstadListByDaneshID(Convert.ToInt32(drpChooseDanshkade.SelectedValue));
                if (listostad?.Count > 0)
                    for (int i = 0; i <= listostad.Count - 1; i++)
                    {
                        listostad[i].Name = listostad[i].Name.Replace("ي", "ی");
                    }
                if (listostad != null) RadComboBoxField.DataSource = listostad.Distinct();
                RadComboBoxField.Items.Clear();
                RadComboBoxField.DataTextField = "name";
                RadComboBoxField.DataValueField = "ID";
                RadComboBoxField.DataBind();
                RadComboBoxField.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("جستجو و انتخاب کنید"));
            }
            //else
            //{
            //    resetform();
            //}
        }

        protected void RadComboBoxField_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }
    }
}