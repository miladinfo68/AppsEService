using ResourceControl.BLL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Data;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.ResourceControl;

namespace ResourceControl.PL.Forms
{
    public partial class professor : System.Web.UI.Page
    {
        List<Category> catlist = new List<Category>();
        List<Option> optlist = new List<Option>();
        List<Course> courselist;
        int userID;//this is proffesor id that has to come from session

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session[sessionNames.userID_StudentOstad] != null)
                {

                    userID = Convert.ToInt32(Session[sessionNames.userID_StudentOstad]);


                    CourseHandler corH = new CourseHandler();
                    courselist = corH.GetCourseListByUserID(userID);
                    if (courselist == null)
                    {
                        string scrp = "استاد محترم در ترم جاری شما کلاسی برای برگزاری ندارید";
                        string address = "../../CommonUI/TeacherIntro.aspx";
                        string resdirectFunc = "function redirectToLast(){ window.location= '" + address + "' ; }";
                        RadWindowManager1.RadAlert(scrp, 300, 100, "پیام سیستم", resdirectFunc);
                    }
                    else
                    BindData(userID);
                }
                else
                {
                    Response.Redirect("../~/CommonUI/login.aspx");
                }
                
                //RadTimePicker1.Culture = new System.Globalization.CultureInfo("en-GB");
                //RadTimePicker2.Culture = new System.Globalization.CultureInfo("en-GB");
            }

            Page.LoadComplete += new EventHandler(Page_LoadComplete);
        }

        void Page_LoadComplete(object sender, EventArgs e)
        {
            RegPcal();
        }

        private void RegPcal()
        {
            string scrp1 = "var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder2_pcal1', {extraInputID: 'ContentPlaceHolder2_pcal1',extraInputFormat: 'yyyy/mm/dd'}); ";
            string scrp2 = "var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder2_pcal2', {extraInputID: 'ContentPlaceHolder2_pcal2',extraInputFormat: 'yyyy/mm/dd'}); ";
            ScriptManager.RegisterClientScriptBlock(this, GetType(), ClientID, scrp1 + scrp2, true);
        }

        private void BindData(int userID)
        {
            CategoryHandler cth = new CategoryHandler();
            catlist = cth.GetCategoryList();
            drpCategory.DataSource = catlist;
            drpCategory.DataTextField = "name";
            drpCategory.DataValueField = "ID";
            drpCategory.DataBind();
            drpCategory.Items.Insert(0, "انتخاب کنید");
            trOptions.Visible = false;
            drpLocation.Enabled = false;
            CourseHandler corH = new CourseHandler();
            courselist = corH.GetCourseListByUserID(userID);
            var courses = courselist.Distinct(new ComprareForListOfClassDistinc());
            drpCourse.DataSource = courses;
            drpCourse.DataTextField = "name";
            drpCourse.DataValueField = "DID";
            drpCourse.DataBind();
            drpCourse.Items.Insert(0, "انتخاب کنید");
        }

        protected void drpCategory_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if ((drpCategory.SelectedIndex == 1) || (drpCategory.SelectedIndex == 2) || (drpCategory.SelectedIndex == 3))
            {
                if (drpCategory.SelectedIndex == 1)
                {
                    txtCapacity.Text = 1.ToString();
                    txtCapacity.ReadOnly = true;
                }
                else
                {

                    txtCapacity.ReadOnly = false;
                }
                LocationHandler locH = new LocationHandler();
                List<Location> loclist = locH.GetLocationListByCatID(Convert.ToInt32(drpCategory.SelectedValue));
                if (loclist != null)
                {
                    drpLocation.Enabled = true;
                    drpLocation.DataSource = loclist;
                    drpLocation.DataTextField = "name";
                    drpLocation.DataValueField = "ID";
                    drpLocation.DataBind();
                    drpLocation.Items.Insert(0, "انتخاب کنید");
                }
                else
                {
                    drpLocation.ClearSelection();
                    drpLocation.Enabled = false;
                }
                OptionHandler opt = new OptionHandler();
                optlist = opt.GetOptionListByCatID(Convert.ToInt32(drpCategory.SelectedValue));
                if (optlist != null)
                {
                    trOptions.Visible = true;
                    chblOptions.Enabled = true;
                    chblOptions.DataSource = optlist;
                    chblOptions.DataTextField = "name";
                    chblOptions.DataValueField = "ID";
                    chblOptions.DataBind();
                }
                else
                {
                    trOptions.Visible = false;
                    chblOptions.Enabled = false;
                }
            }
            else
            {
                trOptions.Visible = false;
                chblOptions.Enabled = false;
                drpLocation.Enabled = false;
            }
        }

        protected void btnAddRquest_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                //DateTime S = RadTimePicker1.SelectedDate.Value;
                //string startTime = S.ToString("HH:mm:ss");

                //DateTime E = RadTimePicker2.SelectedDate.Value;
                //string endTime = E.ToString("HH:mm:ss");




                if (drpCategory.SelectedIndex != 0 /*&& !string.IsNullOrWhiteSpace(txtSubject.Text)*/)
                {
                    //Regex.IsMatch(input, "\d{4}(?:/\d{1,2}){2}"); have to check entry field with regex
                    //CheckUserInput();
                    CreateRequest();
                }
            }
        }

        private void CreateRequest()
        {
            RequestFR req = new RequestFR();
            req.CatID = Convert.ToInt32(drpCategory.SelectedValue);
            req.Subject = "nosub";
            req.Note = txtDescription.Text;
            req.Location = drpLocation.SelectedValue;
            req.IssuerID = Convert.ToInt32(Session[sessionNames.userID_StudentOstad]);
            RC_User user = new RC_User();
            RC_UserHandler us = new RC_UserHandler();
            user = us.Get_Ostad_Details(req.IssuerID);
            req.IssuerName = user.Name;
            CourseHandler corH = new CourseHandler();
            courselist = corH.GetCourseListByUserID(req.IssuerID);
            int coursedid = Convert.ToInt32(drpCourse.SelectedValue);
            req.Capacity = Convert.ToInt32(txtCapacity.Text);
            req.CourseName = drpCourse.SelectedItem.ToString();
            req.DaneshID = courselist.Find(i => i.DID == coursedid).DaneshID;
            req.CourseDID = coursedid;
            req.Issue_time = DateTime.Now.ToPeString();
            if (chbRepeat.Checked == false)
            {
                if (CheckReqDate(pcal1.Text))
                {
                    RequestDateTime rdt = new RequestDateTime();
                    rdt.Date = pcal1.Text;
                    rdt.StartTime = RadTimePicker1.SelectedTime.Value.Ticks;
                    rdt.EndTime = RadTimePicker2.SelectedTime.Value.Ticks;
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
                    req.DateTimeRange = FillRequestDateTime(pcal1.Text, pcal2.Text, RadTimePicker1.SelectedTime, RadTimePicker2.SelectedTime, chblWeekDates);

                }
                else
                {
                    string scrp = "تاریخ درخواست باید بعد از تاریخ امروز باشد.";//باید این پیام عوض شود.
                    RadWindowManager1.RadAlert(scrp, 300, 100, "خطا", "");
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", scrp, true);
                    return;
                }
            }

            optlist = new List<Option>();
            foreach (ListItem item in chblOptions.Items)
            {
                if (item.Selected)
                {
                    Option opt = new Option();
                    opt.ID = Convert.ToInt32(item.Value);
                    opt.Name = item.Text;
                    opt.IsActive = item.Selected;
                    optlist.Add(opt);
                }
            }
            RequestHandler rqh = new RequestHandler();
            DataTable dt = rqh.HasRequestBefore(req.IssuerID, req.DateTimeRange);
            if (dt.Rows.Count > 0)
            {
                if (dt.Select().ToList().Exists(row => row["MayConflict"].ToString() == "1"))
                {
                    string scrp = "تداخل در ساعت و تاریخ درخواست ، با ساعات درخواست های پیشین این استاد.";
                    RadWindowManager1.RadAlert(scrp, 300, 100, "خطا", "");

                    return;
                }
            }

            if (RequestHandler.IsNotSpecifiedDay(DayOfWeek.Friday, req.DateTimeRange))
            {
                string scrp = "در روز جمعه امکان برگزاری وجود ندارد";
                RadWindowManager1.RadAlert(scrp, 300, 100, "خطا", "");

                return;
            }
            try
            {
                int reqid = rqh.AddNewRequest(req, optlist, req.IssuerID.ToString());
                var userId = Convert.ToInt32(Session[sessionNames.userID_StudentOstad]);
                var comman = new CommonBusiness();
                comman.InsertIntoStudentLog(userId.ToString(), "", 11, 28, "ثبت درخواست کلاس توسط استاد");

                string scrp = "درخواست شما با شماره " + reqid.ToString() + " با موفقیت ثبت گردید.";
                RadWindowManager1.RadAlert(scrp, 300, 100, "پیام سیستم", "redirectToLast");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", scrp, true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private List<RequestDateTime> FillRequestDateTime(string p1, string p2, TimeSpan? sTime, TimeSpan? eTime, CheckBoxList chblWeekDates)
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
                        EndTime = eTime.Value.Ticks
                    });
            }

            #region Prev

            //foreach (ListItem item in chblWeekDates.Items)
            //{
            //    if (item.Selected == true)
            //    {
            //        int x = Convert.ToInt32(item.Value) - sDayOfWeek;
            //        if (x < 0)
            //        {
            //            x = x + 7;
            //        }
            //        DateTime date = sDate.AddDays(x);

            //        while (date <= eDate)
            //        {
            //            RequestDateTime rdt = new RequestDateTime();
            //            rdt.Date = date.ToPeString();
            //            rdt.StartTime = sTime.Value.Ticks;
            //            rdt.EndTime = eTime.Value.Ticks;

            //            dateTimeList.Add(rdt);
            //            date = date.AddDays(8);
            //        }
            //    }
            //}

            #endregion


            return dateTimeList;
        }

        // private List<RequestDateTime> FillRequestDateTime(string p1, string p2,TimeSpan? sTime , TimeSpan? eTime , CheckBoxList chblWeekDates)
        //{

        //DateTime sDate = p1.ToMiladi();
        //int sDayOfWeek = (int)sDate.DayOfWeek;
        //DateTime eDate = p2.ToMiladi();
        ////int eDayOfWeek = (int)eDate.DayOfWeek;
        //List<RequestDateTime> dateTimeList = new List<RequestDateTime>();

        //foreach (ListItem item in chblWeekDates.Items)
        //{
        //    if (item.Selected == true)
        //    {
        //        int x = Convert.ToInt32(item.Value) - sDayOfWeek;
        //        if (x < 0)
        //        {
        //            x = x + 7;
        //        }
        //        DateTime date = sDate.AddDays(x);

        //        while (date <= eDate)
        //        {
        //            RequestDateTime rdt = new RequestDateTime();
        //            rdt.Date = date.ToPeString();
        //            rdt.StartTime = sTime.Value.Ticks;
        //            rdt.EndTime = eTime.Value.Ticks;

        //            dateTimeList.Add(rdt);
        //            date = date.AddDays(7);
        //        }
        //    }
        //}

        //return dateTimeList;
        //  }

        private Boolean CheckReqDate(string inputdate)
        {
            int todaypersian = Convert.ToInt32(DateTime.Now.ToPeString("yyyyMMdd"));
            int inputdateNo = Convert.ToInt32(inputdate.Replace("/", ""));
            return (inputdateNo >= todaypersian);
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

        protected void drpCourse_DataBound(object sender, EventArgs e)
        {
            DropDownList drpCourse = (DropDownList)sender;
            foreach (ListItem item in drpCourse.Items)
            {
                item.Text = item.Text.Replace("کلاس آنلاینعادی", "");
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

        protected void vldTimes_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (RadTimePicker2.SelectedTime <= RadTimePicker1.SelectedTime)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
    }
}