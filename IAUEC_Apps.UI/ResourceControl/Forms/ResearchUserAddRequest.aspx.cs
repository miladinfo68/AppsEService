using ResourceControl.BLL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO;

namespace ResourceControl.PL.Forms
{
    public partial class ResearchUserAddRequest : System.Web.UI.Page
    {
        List<Category> catlist = null;
        List<Location> loclist = null;
        List<Option> optlist = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["GoBackTo"] = Request.UrlReferrer;
                //var user1 = Session["name"].ToString();
                BindData();
            }
            Page.LoadComplete += new EventHandler(Page_LoadComplete);
        }

        void Page_LoadComplete(object sender, EventArgs e)
        {
            RegPcal();
        }

        private void RegPcal()
        {
            string scrp1 = "var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_pcal1', {extraInputID: 'ContentPlaceHolder1_pcal1',extraInputFormat: 'yyyy/mm/dd'}); ";
            string scrp2 = "var objCal1 = new AMIB.persianCalendar('ContentPlaceHolder1_pcal2', {extraInputID: 'ContentPlaceHolder1_pcal2',extraInputFormat: 'yyyy/mm/dd'}); ";
            ScriptManager.RegisterClientScriptBlock(this, GetType(), ClientID, scrp1 + scrp2, true);
        }

        private void BindData()
        {
            trOptions.Visible = false;
            chblOptions.Enabled = false;

            CategoryHandler cth = new CategoryHandler();
            catlist = cth.GetCategoryList();
            drpCategory.DataSource = catlist;
            drpCategory.DataTextField = "name";
            drpCategory.DataValueField = "ID";
            drpCategory.DataBind();
            drpCategory.Items.Insert(0, "انتخاب کنید");

            int depId = 0;
            int roleId = Convert.ToInt32(Session["roleId"]);
            switch (roleId)
            {
                case 13:
                case 1:
                    depId = 0;
                    break;
                case 41:
                    depId = 1;
                    break;
                case 42:
                    depId = 2;
                    break;
                case 43:
                    depId = 3;
                    break;
                case 44:
                    depId = 4;
                    break;
                case 45:
                    depId = 5;
                    break;
                case 46:
                    depId = 6;
                    break;
                case 47:
                    depId = 7;
                    break;
                case 48:
                    depId = 8;
                    break;

                default:
                    depId = -1;
                    break;
            }

            CourseHandler corH = new CourseHandler();
            //if (depId == 0)
            //{
            //    RadComboBoxField.DataSource = corH.GetShortCourseList();
            //}
            //else
            //{
            //    RadComboBoxField.DataSource = corH.GetShortTermProfByDepId(depId);
            //}
            //RadComboBoxField.DataTextField = "pn";
            //RadComboBoxField.DataValueField = "PROF_CODE";
            //RadComboBoxField.DataBind();
            //RadComboBoxField.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("جستجو و انتخاب کنید"));
        }

        protected void RadComboBoxField_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //CourseHandler corH = new CourseHandler();
            //drpCourse.DataSource = corH.GetShortTermByProfId(Convert.ToInt32(RadComboBoxField.SelectedValue));
            //drpCourse.DataTextField = "name";
            //drpCourse.DataValueField = "DID";
            //drpCourse.DataBind();
        }

        protected void drpCategory_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (drpCategory.SelectedIndex != 0)
            {
                LocationHandler locH = new LocationHandler();
                loclist = locH.GetLocationListByCatID(Convert.ToInt32(drpCategory.SelectedValue));
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
                trOptions.Visible = true;
                chblOptions.Enabled = true;
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
            }

            if (drpCategory.SelectedIndex == 1)
            {
                txtCapacity.Text = 1.ToString();
                txtCapacity.ReadOnly = true;
            }
            else
            {
                txtCapacity.ReadOnly = false;
            }
        }

        protected void btnAddRquest_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (drpCategory.SelectedIndex != 0 /* && !string.IsNullOrWhiteSpace(txtSubject.Text)*/)
                {
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
            RC_User user = new RC_User();
            RC_UserHandler us = new RC_UserHandler();
            var user1 = Session["UserName"].ToString();
            //user = us.Get_Ostad_DetailsByCourseId(Convert.ToInt32(drpCourse.SelectedValue));
            req.IssuerID = int.Parse(Session[sessionNames.userID_Karbar].ToString());
            req.IssuerName = Session["UserName"].ToString();
            //int coursedid = Convert.ToInt32(drpCourse.SelectedValue);
            req.Capacity = Convert.ToInt32(txtCapacity.Text);
            req.CourseName = txt_courseName.Text;
            req.DaneshID = 5;//dorehaye kootah modat
            req.CourseDID = 0;
            req.Issue_time = DateTime.Now.ToPeString();
            req.Status = (int)RequestStatus.sent;
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

            //DataTable dt = rqh.HasRequestBefore(req.IssuerID, req.DateTimeRange);
            //if (dt.Rows.Count > 0)
            //{
            //    if (dt.Select().ToList().Exists(row => row["MayConflict"].ToString() == "1"))
            //    {
            //        string scrp = "تداخل در ساعت و تاریخ درخواست ، با ساعات درخواست های پیشین این استاد.";
            //        RadWindowManager1.RadAlert(scrp, 300, 100, "خطا", "");

            //        return;
            //    }
            //}
            if (RequestHandler.IsNotSpecifiedDay(DayOfWeek.Friday, req.DateTimeRange))
            {
                string scrp = "در روز جمعه امکان برگزاری وجود ندارد";
                RadWindowManager1.RadAlert(scrp, 300, 100, "خطا", "");

                return;
            }
            try
            {
                var address = ViewState["GoBackTo"].ToString();
                int reqid = rqh.AddNewRequest(req, optlist, Session[sessionNames.userID_Karbar].ToString());

                var comman = new CommonBusiness();
                    comman.InsertIntoUserLog(Convert.ToInt32(Session[sessionNames.userID_Karbar].ToString()), "", 11, 114, "ثبت درخواست کلاس دوره کوتاه مدت", reqid);

                string resdirectFunc = "function redirectToLast(){ window.location= '" + address + "' ; }";
                string scrp = "درخواست شما با شماره " + reqid.ToString() + " با موفقیت ثبت گردید";
                RadWindowManager1.RadAlert(scrp, 300, 100, "پیام سیستم", resdirectFunc);



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
        //private List<RequestDateTime> FillRequestDateTime(string p1, string p2, TimeSpan? sTime, TimeSpan? eTime, CheckBoxList chblWeekDates)
        //{
        //    DateTime sDate = p1.ToMiladi();
        //    int sDayOfWeek = (int)sDate.DayOfWeek;
        //    DateTime eDate = p2.ToMiladi();
        //    //int eDayOfWeek = (int)eDate.DayOfWeek;
        //    List<RequestDateTime> dateTimeList = new List<RequestDateTime>();

        //    foreach (ListItem item in chblWeekDates.Items)
        //    {
        //        if (item.Selected == true)
        //        {
        //            int x = Convert.ToInt32(item.Value) - sDayOfWeek;
        //            if (x < 0)
        //            {
        //                x = x + 7;
        //            }
        //            DateTime date = sDate.AddDays(x);

        //            while (date <= eDate)
        //            {
        //                RequestDateTime rdt = new RequestDateTime();
        //                rdt.Date = date.ToPeString();
        //                rdt.StartTime = sTime.Value.Ticks;
        //                rdt.EndTime = eTime.Value.Ticks;

        //                dateTimeList.Add(rdt);
        //                date = date.AddDays(7);
        //            }
        //        }
        //    }

        //    return dateTimeList;
        //}

        private Boolean CheckReqDate(string text)
        {
            string today = DateTime.Now.ToPeString();
            int today1 = Convert.ToInt32(today.Replace("/", ""));
            int text1 = Convert.ToInt32(text.Replace("/", ""));
            return (text1 > today1);
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


    }
}