using ResourceControl.BLL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;

namespace ResourceControl.PL.Forms
{
    public partial class EducationUserEditRequest : System.Web.UI.Page
    {
        List<Category> catlist = new List<Category>();
        List<Option> optlist = new List<Option>();
        List<Course> courselist;
        int userID;//this is education or office or admin id that has to come from session
        int reqID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.DataBind();
            userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
            reqID = Convert.ToInt32(Request.QueryString["reqId"]);
            if (!IsPostBack)
            {
                ViewState["GoBackTo"] = Request.UrlReferrer;
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
                if (reqID != 0)
                {
                    ViewState.Add("PreviousPage", Request.UrlReferrer);
                    ToEditMode(reqID);
                }
                else
                {
                    RadWindowManager1.RadAlert("خطا در هنگام دریافت کد درخواست.", 300, 100, "خطا", "redirectToPrevious");
                }

                //RadTimePicker1.Culture = new System.Globalization.CultureInfo("en-GB");
                //RadTimePicker2.Culture = new System.Globalization.CultureInfo("en-GB");
            }
            RegPcal();
        }

        private void ToEditMode(int reqID)
        {
            trOptions.Visible = true;
            chblOptions.Enabled = true;
            drpLocation.Enabled = false;
            RequestHandler reqH = new RequestHandler();
            RequestFR req = reqH.GetRequestDetails(reqID);
            CategoryHandler cth = new CategoryHandler();
            catlist = cth.GetCategoryList();
            drpCategory.DataSource = catlist;
            drpCategory.DataTextField = "name";
            drpCategory.DataValueField = "ID";
            drpCategory.DataBind();
            drpCategory.Items.Insert(0, "انتخاب کنید");
            drpCategory.Items.FindByValue(req.CatID.ToString()).Selected = true;
            OptionHandler opt = new OptionHandler();
            optlist = opt.GetOptionListByCatID(req.CatID);
            chblOptions.DataSource = optlist;
            chblOptions.DataTextField = "name";
            chblOptions.DataValueField = "ID";
            chblOptions.DataBind();
            List<Req_Opt_Junc> res_opt_junlist = new List<Req_Opt_Junc>();
            Req_Opt_JuncHandler rsopt = new Req_Opt_JuncHandler();
            res_opt_junlist = rsopt.GetReq_Opt_JuncListByReqID(reqID);
            if (res_opt_junlist != null)
            {
                for (int i = 0; i < res_opt_junlist.Count; i++)
                {
                    if (res_opt_junlist[i].IsActive == true)
                    {
                        chblOptions.Items.FindByValue(res_opt_junlist[i].Opt_id.ToString()).Selected = true;
                    }
                }
            }
            RC_UserHandler usrh = new RC_UserHandler();
            RC_User ostad = usrh.Get_Ostad_Details(req.IssuerID);
            drpProfessor.Items.Insert(0, ostad.Name);
            drpProfessor.Enabled = false;

            CourseHandler corH = new CourseHandler();
            courselist = corH.GetCourseListByUserID(req.IssuerID);
            drpCourse.DataSource = courselist;
            drpCourse.DataTextField = "name";
            drpCourse.DataValueField = "DID";
            drpCourse.DataBind();
            if (req.CourseName == "درخواست جلسه")
            {
                Session["isMeeting"] = true;
            }
            else
            {
                Session["isMeeting"] = false;
            }
            if ((bool)Session["isMeeting"])
            {
                trProfCrs.Visible = false;
            }
            LocationHandler locH = new LocationHandler();
            drpLocation.DataSource = locH.GetLocationListByCatID(Convert.ToInt32(drpCategory.SelectedValue));
            drpLocation.DataTextField = "name";
            drpLocation.DataValueField = "ID";
            drpLocation.DataBind();
            drpLocation.Items.FindByText(req.Location).Selected = true;
            txtCapacity.Text = req.Capacity.ToString();
            txtDescription.Text = req.Note;
            if (req.CourseDID != 0)
            {
                drpCourse.Items.FindByValue(req.CourseDID.ToString()).Selected = true;
            }

            var datetimelist = req.DateTimeRange;

            //Set a Time and Date of Request

            var requestStartTime = datetimelist.FirstOrDefault(c => c.StartTime != 0);
            if (requestStartTime != null)
            {
                var startTime = TimeSpan.FromTicks(requestStartTime.StartTime);
                RadTimePicker1.SelectedTime = startTime;
            }

            var requestEndTime = datetimelist.FirstOrDefault(c => c.StartTime != 0);
            if (requestEndTime != null)
            {
                var endTime = TimeSpan.FromTicks(requestEndTime.EndTime);
                RadTimePicker2.SelectedTime = endTime;
            }

            var requestDateTime = datetimelist.FirstOrDefault(c => c.StartTime != 0);
            if (requestDateTime != null)
                pcal1.Text = requestDateTime.Date;

            if (datetimelist.Count > 1)
            {
                chbRepeat.Checked = true;
                dvEndDate.Visible = true;
                string scrp2 = "var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_pcal2', {extraInputID: 'ContentPlaceHolder1_pcal2',extraInputFormat: 'yyyy/mm/dd'}); ";
                ScriptManager.RegisterStartupScript(this, GetType(), ClientID, scrp2, true);
                var listOfDay = new List<string>();
                foreach (RequestDateTime dateTime in datetimelist.OrderBy(c => c.Date))
                {
                    var dateSplit = dateTime.Date.Split('/');
                    var d = new DateTime(Convert.ToInt32(dateSplit[0]), Convert.ToInt32(dateSplit[1]), Convert.ToInt32(dateSplit[2]), new PersianCalendar());
                    listOfDay.Add(d.DayOfWeek.ToString());
                }


                var listOfDayforRequest = listOfDay.Distinct();

                var lastOrDefault = datetimelist.OrderBy(d => d.Date).LastOrDefault();
                if (lastOrDefault != null)
                {
                    var endDateRequest = lastOrDefault.Date;
                    pcal2.Text = endDateRequest;
                }
                foreach (var day in listOfDayforRequest)
                {
                    switch (day)
                    {
                        case "Friday":

                            chblWeekDates.Items[6].Selected = true;
                            break;
                        case "Saturday":
                            chblWeekDates.Items[0].Selected = true;
                            break;
                        case "Sunday":
                            chblWeekDates.Items[1].Selected = true;
                            break;
                        case "Monday":
                            chblWeekDates.Items[2].Selected = true;
                            break;
                        case "Tuesday":
                            chblWeekDates.Items[3].Selected = true;
                            break;
                        case "Wednesday":
                            chblWeekDates.Items[4].Selected = true;
                            break;
                        case "Thursday":
                            chblWeekDates.Items[5].Selected = true;
                            break;

                    }
                }



            }

            grdOldDateTime.DataSource = datetimelist.OrderBy(d => d.Date);
            grdOldDateTime.DataBind();

            btnEditRequest.Enabled = true;
            btnEditRequest.Visible = true;

            if (drpCategory.SelectedIndex == 1)
            {
                txtCapacity.Text = 1.ToString();
                txtCapacity.ReadOnly = true;
            }
        }

        protected void drpCategory_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if ((drpCategory.SelectedIndex == 1) || (drpCategory.SelectedIndex == 2) || (drpCategory.SelectedIndex == 3))
            {
                trOptions.Visible = true;
                chblOptions.Enabled = true;
                drpLocation.Enabled = true;
                LocationHandler locH = new LocationHandler();
                drpLocation.DataSource = locH.GetLocationListByCatID(Convert.ToInt32(drpCategory.SelectedValue));
                drpLocation.DataTextField = "name";
                drpLocation.DataValueField = "name";
                drpLocation.DataBind();
                drpLocation.Items.Insert(0, "انتخاب کنید");
                OptionHandler opt = new OptionHandler();
                optlist = opt.GetOptionListByCatID(Convert.ToInt32(drpCategory.SelectedValue));
                chblOptions.DataSource = optlist;
                chblOptions.DataTextField = "name";
                chblOptions.DataValueField = "ID";
                chblOptions.DataBind();
            }
            else
            {
                trOptions.Visible = false;
                chblOptions.Enabled = false;
                drpLocation.Enabled = false;
            }
        }

        protected void btnEditRequest_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                RequestFR req = new RequestFR();
                RequestHandler rqH = new RequestHandler();
                req = rqH.GetRequestDetails(reqID);
                req.CatID = Convert.ToInt32(drpCategory.SelectedValue);
                RC_User user = new RC_User();
                RC_UserHandler us = new RC_UserHandler();
                user = us.Get_Ostad_Details(req.IssuerID);
                req.Subject = "nosub";
                req.Note = txtDescription.Text;
                req.Location = drpLocation.SelectedValue;
                if (chbRepeat.Checked == false)
                {
                    if (CheckReqDate(pcal1.Text))
                    {
                        RequestDateTime rdt = new RequestDateTime();
                        rdt.Date = pcal1.Text;
                        rdt.StartTime = RadTimePicker1.SelectedTime.Value.Ticks;
                        rdt.EndTime = RadTimePicker2.SelectedTime.Value.Ticks;
                        rdt.RequestId = req.ID;
                        req.DateTimeRange = new List<RequestDateTime>();
                        req.DateTimeRange.Add(rdt);

                    }
                    else
                    {
                        string scrp = "تاریخ درخواست باید بعد از تاریخ امروز باشد.";
                        RadWindowManager1.RadAlert(scrp, 300, 100, "خطا", "");
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", scrp, true);
                        return;
                    }
                }
                else
                {
                    if (CheckReqDate(pcal1.Text) && CheckReqDate(pcal2.Text))
                    {
                        req.DateTimeRange = FillRequestDateTime(pcal1.Text, pcal2.Text, RadTimePicker1.SelectedTime, RadTimePicker2.SelectedTime, chblWeekDates, req.ID);
                    }
                    else
                    {
                        string scrp = "تاریخ درخواست باید بعد از تاریخ امروز باشد.";//باید این پیام عوض شود.
                        RadWindowManager1.RadAlert(scrp, 300, 100, "خطا", "");
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", scrp, true);
                        return;
                    }
                }
                req.IssuerID = user.ID;
                req.IssuerName = user.Name;
                if ((bool)Session["isMeeting"])
                {
                    trProfCrs.Visible = false;
                }
                CourseHandler corH = new CourseHandler();
                courselist = corH.GetCourseListByUserID(user.ID);
                int coursedid = Convert.ToInt32(drpCourse.SelectedValue);
                req.Capacity = courselist.Find(i => i.DID == coursedid).Capacity;
                req.CourseName = drpCourse.SelectedItem.ToString();
                req.DaneshID = courselist.Find(i => i.DID == coursedid).DaneshID;
                req.CourseDID = coursedid;
                req.Capacity = Convert.ToInt32(txtCapacity.Text);
                req.Issue_time = DateTime.Now.ToPeString();
                if (CheckReqDate(pcal1.Text))
                {
                    //req.Sessiondate = pcal1.Text;
                }
                else
                {
                    string scrp = "تاریخ درخواست باید بعد از تاریخ امروز باشد";
                    RadWindowManager1.RadAlert(scrp, 300, 100, "خطا", "");
                    return;
                }

                optlist = new List<Option>();
                foreach (ListItem item in chblOptions.Items)
                {
                    Option optstat = new Option();
                    optstat.ID = Convert.ToInt32(item.Value);
                    optstat.Name = item.Text;
                    optstat.IsActive = item.Selected;
                    optlist.Add(optstat);
                }
                if (RequestHandler.IsNotSpecifiedDay(DayOfWeek.Friday, req.DateTimeRange))
                {
                    string scrp = "در روز جمعه امکان برگزاری وجود ندارد";
                    RadWindowManager1.RadAlert(scrp, 300, 100, "خطا", "");

                    return;
                }
                try
                {
                    RequestHandler rq = new RequestHandler();
                    req.Status = 1;//sent
                    int reqid = rq.EditRequest(req, optlist);


                    var commanBusiness = new CommonBusiness();
                    userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
                    commanBusiness.InsertIntoUserLog(userID, "", 11, 115, "تایید آموزش درخواست رزرواسیون", reqid);



                    var address = ViewState["GoBackTo"].ToString();
                    string resdirectFunc = "function redirectToLast(){ window.location= '" + address + "' ; }";
                    string scrp = "درخواست شما با شماره " + reqid.ToString() + " با موفقیت ویرایش و ثبت گردید";
                    RadWindowManager1.RadAlert(scrp, 300, 100, "پیام سیستم", resdirectFunc);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private List<RequestDateTime> FillRequestDateTime(string p1, string p2, TimeSpan? sTime, TimeSpan? eTime, CheckBoxList chblWeekDates, int reqId)
        {
            DateTime sDate = p1.ToGregorian();
            // int sDayOfWeek = (int)sDate.DayOfWeek;
            DateTime eDate = p2.ToGregorian();

            //int eDayOfWeek = (int)eDate.DayOfWeek;
            var dateTimeList = new List<RequestDateTime>();

            TimeSpan diff = eDate - sDate;
            var days = diff.Days;
            var dayOfWeeks = new List<DayOfWeek>();
            foreach (ListItem item in chblWeekDates.Items)
            {
                if (item.Selected != true) continue;
                switch (item.Text)
                {
                    case "شنبه":
                        dayOfWeeks.Add(DayOfWeek.Saturday);
                        break;
                    case "یکشنبه":
                        dayOfWeeks.Add(DayOfWeek.Sunday);
                        break;
                    case "دوشنبه":
                        dayOfWeeks.Add(DayOfWeek.Monday);
                        break;
                    case "سه شنبه":
                        dayOfWeeks.Add(DayOfWeek.Tuesday);
                        break;
                    case "چهارشنبه":
                        dayOfWeeks.Add(DayOfWeek.Wednesday);
                        break;
                    case "پنج شنبه":
                        dayOfWeeks.Add(DayOfWeek.Thursday);
                        break;
                    case "جمعه":
                        dayOfWeeks.Add(DayOfWeek.Friday);
                        break;
                }
            }
            for (var i = 0; i <= days; i++)
            {
                var tempDate = sDate.AddDays(i);
                dateTimeList.AddRange(
                    from dayOfWeek in dayOfWeeks
                    where tempDate.DayOfWeek == dayOfWeek
                    where sTime != null
                    where eTime != null
                    select new RequestDateTime
                    {
                        Date = tempDate.ToPeString(),
                        StartTime = sTime.Value.Ticks,
                        EndTime = eTime.Value.Ticks,
                        RequestId = reqId
                    });
            }
            return dateTimeList;
        }


        private Boolean CheckReqDate(string inputdate)
        {
            int todaypersian = Convert.ToInt32(DateTime.Now.ToPeString("yyyyMMdd"));
            int inputdateNo = Convert.ToInt32(inputdate.Replace("/", ""));
            return (inputdateNo >= todaypersian);
        }

        private void RegPcal()
        {
            string scrp1 = "var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_pcal1', {extraInputID: 'ContentPlaceHolder1_pcal1',extraInputFormat: 'yyyy/mm/dd'}); ";
            string scrp2 = "var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_pcal2', {extraInputID: 'ContentPlaceHolder1_pcal2',extraInputFormat: 'yyyy/mm/dd'}); ";
            ScriptManager.RegisterClientScriptBlock(this, GetType(), ClientID, scrp1 + scrp2, true);
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (chblOptions.SelectedIndex != -1)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void chbRepeat_CheckedChanged(object sender, EventArgs e)
        {
            if (chbRepeat.Checked == true)
            {
                dvEndDate.Visible = true;

            }
            else
            {
                dvEndDate.Visible = false;
            }
        }

        private void resetform()
        {

            drpCourse.Items.Clear();
            RadTimePicker1.Clear();
            RadTimePicker2.Clear();
        }

        protected void vldTimes_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (RadTimePicker2.SelectedTime < RadTimePicker1.SelectedTime)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void btnPutOff_Click(object sender, EventArgs e)
        {
            // Response.Redirect("EducationUserReview.aspx?id=ORY3QHY2@A72-ORY3QHY2");

            if (Request.UrlReferrer != null) Response.Redirect(ViewState["GoBackTo"].ToString());
        }
    }
}