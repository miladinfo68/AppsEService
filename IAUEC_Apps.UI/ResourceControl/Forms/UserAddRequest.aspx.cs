using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.ResourceControl;
using IAUEC_Apps.UI.CommonUI;
using ResourceControl.BLL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class UserAddRequest : System.Web.UI.Page
    {
        List<Category> catlist = new List<Category>();
        List<Option> optlist = new List<Option>();
        List<Daneshkade> dnshList = new List<Daneshkade>();
        List<Course> courselist;
        int userID;//this is education or office or admin id that has to come from session
        int daneshID;
        int roleID;
        static string prevPage = String.Empty;

        private void MasoulDaftarView(int roleId)
        {
            if (UtilityFunction.IsMasouleDaftarUser(roleId))
            {

                //foreach (ListItem i in drpCategory.Items)
                //{
                //    
                //}

                for (int i = 1; i < drpCategory.Items.Count; i++)
                {
                    drpCategory.Items[i].Enabled = false;
                }

                //       drpCategory.Items.FindByValue("0").Enabled = true;
                drpCategory.Items.FindByValue("3").Enabled = true;
                trDaneshkadeh.Visible = false;
                trProfCrs.Visible = false;

            }

        }



        protected void Page_Load(object sender, EventArgs e)
        {
            this.DataBind();
            userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
            roleID = Convert.ToInt32(Session["roleID"]);
            if (!IsPostBack)
            {
                chkMeeting.Visible = false;
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
                AccessControl1.MenuId = menuId;
                AccessControl1.UserId = Session[sessionNames.userID_Karbar].ToString();
                if (Session["DaneshId"].ToString() == "0")
                {
                    ToAdminAddMode();
                }
                else
                {
                    ViewState.Add("PreviousPage", Request.UrlReferrer);
                    ToAddMode();
                }
                if (Request.UrlReferrer != null) prevPage = Request.UrlReferrer.ToString();
                MasoulDaftarView(roleID);
            }
            RegPcal();
        }

        private void ToAdminAddMode()
        {
            // trDaneshkadeh.Visible = true;
            trProfCrs.Visible = false;
            trDaneshkadeh.Visible = true;
            drpChooseDanshkade.Enabled = true;
            // drpChooseDanshkade.Enabled = true;
            // drpChooseDanshkade.Visible = true;

            //DaneshkadeHandler daneshH = new DaneshkadeHandler();
            //drpChooseDanshkade.DataSource = daneshH.GetAllDaneshkade();
            //drpChooseDanshkade.DataTextField = "NameDanesh";
            //drpChooseDanshkade.DataValueField = "ID";
            //drpChooseDanshkade.DataBind();
            //drpChooseDanshkade.Items.Insert(0, "انتخاب کنید");
            CategoryHandler cth = new CategoryHandler();
            catlist = cth.GetCategoryList();
            drpCategory.DataSource = catlist;
            drpCategory.DataTextField = "name";
            drpCategory.DataValueField = "ID";
            drpCategory.DataBind();
            drpCategory.Items.Insert(0, "انتخاب کنید");
        }

        private void ToAddMode()
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
            drpCourse.Items.Insert(0, "انتخاب کنید");
            lblHeader.Text = "ثبت درخواست";
            btnAddRquest.Enabled = true;
            btnAddRquest.Visible = true;
            trDaneshkadeh.Visible = false;
        }

        protected void drpCategory_SelectedIndexChanged1(object sender, EventArgs e)
        {
            chkMeeting.Checked = false;
            chkMeeting_CheckedChanged(sender, e);
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
                trOptions.Visible = true;
                chblOptions.Enabled = true;
                drpLocation.Enabled = true;
                LocationHandler locH = new LocationHandler();
                drpLocation.Items.Clear();
                drpLocation.DataSource = locH.GetLocationListByCatID(Convert.ToInt32(drpCategory.SelectedValue)).Distinct();
                drpLocation.DataTextField = "name";
                drpLocation.DataValueField = "ID";
                drpLocation.DataBind();
                drpLocation.Items.Insert(0, "انتخاب کنید");
                OptionHandler opt = new OptionHandler();
                optlist = opt.GetOptionListByCatID(Convert.ToInt32(drpCategory.SelectedValue));
                chblOptions.Items.Clear();
                chblOptions.DataSource = optlist;
                chblOptions.DataTextField = "name";
                chblOptions.DataValueField = "ID";
                chblOptions.DataBind();

                trDaneshkadeh.Visible = userID == 1;
                Daneshkade item = new Daneshkade();
                item.NameDanesh = "--انتخاب کنید--";
                item.ID = -1;
                DaneshkadeHandler dnsh = new DaneshkadeHandler();
                dnshList = dnsh.GetAllDaneshkade();
                dnshList.Insert(0, item);
                drpChooseDanshkade.Items.Clear();
                drpChooseDanshkade.DataSource = dnshList;
                drpChooseDanshkade.DataTextField = "NameDanesh";
                drpChooseDanshkade.DataValueField = "ID";
                drpChooseDanshkade.DataBind();


                if (drpCategory.SelectedIndex == 3 || drpCategory.SelectedIndex == 2)
                {
                    chkMeeting.Visible = true;
                    if (drpCategory.SelectedIndex == 2)
                    {
                        chkMeeting.Text = "بدون استاد";
                    }
                    else
                    {
                        chkMeeting.Text = " تخصیص سالن کنفرانس جهت برگزاری جلسات";
                    }
                }
                else
                {
                    chkMeeting.Visible = false;
                }
            }
            else
            {
                trOptions.Visible = false;
                chblOptions.Enabled = false;
                drpLocation.Enabled = false;
                chkMeeting.Visible = false;
            }
            //////////////////////////////////////////// 
            RadComboBoxField.Enabled = true;
            RC_UserHandler usrH = new RC_UserHandler();
            List<RC_User> listostad = usrH.GetOstadListByDaneshID(Convert.ToInt32(Session["DaneshId"]));
            if (listostad != null)
                for (int i = 0; i <= listostad.Count - 1; i++)
                {
                    listostad[i].Name = listostad[i].Name.Replace("ي", "ی");
                }
            RadComboBoxField.Items.Clear();
            RadComboBoxField.DataSource = listostad;
            RadComboBoxField.DataTextField = "name";
            RadComboBoxField.DataValueField = "ID";
            RadComboBoxField.DataBind();
            RadComboBoxField.Items.Insert(0, new Telerik.Web.UI.RadComboBoxItem("جستجو و انتخاب کنید"));


            ///////////////////////////////////////////
        }

        protected void btnAddRquest_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (drpCategory.SelectedIndex != 0)
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
            req.Status = (int)RequestStatus.sent;
            RC_User user = new RC_User();
            RC_UserHandler us = new RC_UserHandler();
            CourseHandler corH = new CourseHandler();

            if (chkMeeting.Checked == true)
            {
                req.IssuerID = Convert.ToInt32(Session[sessionNames.userID_Karbar].ToString());
                user.Name = Session["UserName"].ToString();
                req.IssuerName = user.Name;
                if (drpCategory.SelectedIndex == 3)
                {
                    req.CourseName = "درخواست جلسه";
                }
                else
                {
                    req.CourseName = "درخواست کلاس بدون استاد";
                }
                Session["isMeeting"] = true;
                daneshID = Convert.ToInt32(Session["DaneshId"]);
                req.DaneshID = daneshID;
                req.Capacity = Convert.ToInt32(txtCapacity.Text);
            }
            else
            {

                if (UtilityFunction.IsMasouleDaftarUser(roleID))
                {
                    req.IssuerID = Convert.ToInt32(Session[sessionNames.userID_Karbar].ToString());
                    user.Name = Session["UserName"].ToString();
                    req.IssuerName = user.Name;
                    if (drpCategory.SelectedIndex == 3)
                    {
                        req.CourseName = "درخواست جلسه";
                    }
                    else
                    {
                        req.CourseName = "درخواست کلاس بدون استاد";
                    }
                    Session["isMeeting"] = true;
                    daneshID = Convert.ToInt32(Session["DaneshId"]);
                    req.DaneshID = daneshID;
                    req.Capacity = Convert.ToInt32(txtCapacity.Text);
                }
                else
                {


                    req.IssuerID = Convert.ToInt32(RadComboBoxField.SelectedValue.ToString());

                    user = us.Get_Ostad_Details(req.IssuerID);
                    req.IssuerName = user.Name;
                    courselist = corH.GetCourseListByUserID(req.IssuerID);
                    int coursedid = Convert.ToInt32(drpCourse.SelectedValue);
                    req.Capacity = Convert.ToInt32(txtCapacity.Text);
                    req.CourseName = drpCourse.SelectedItem.ToString();
                    req.DaneshID = courselist.Find(i => i.DID == coursedid).DaneshID;
                    req.CourseDID = coursedid;
                }
            }
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
                int reqid = rqh.AddNewRequest(req, optlist, Session[sessionNames.userID_Karbar].ToString());
                var comman = new CommonBusiness();
                comman.InsertIntoUserLog(userID, "", 11, 114, "ثبت درخواست کلاس ", reqid);



                string scrp = "درخواست شما با شماره " + reqid.ToString() + " با موفقیت ثبت گردید";
                string address = prevPage + "?id=" + generaterandomstr() + "@A" + "0" + "-" + generaterandomstr();
                string resdirectFunc = "function redirectToLast(){ window.location= '" + address + "' ; }";
                RadWindowManager1.RadAlert(scrp, 300, 100, "پیام سیستم", resdirectFunc);

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

        //protected void RadComboBoxField_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (RadComboBoxField.SelectedIndex != 0)
        //    {
        //        CourseHandler corH = new CourseHandler();
        //        courselist = corH.GetCourseListByUserID(Convert.ToInt32(RadComboBoxField.SelectedValue));
        //        if (courselist != null)
        //        {
        //            int daneshId = Convert.ToInt32(Session["DaneshId"] ?? 0);
        //            if (daneshId != 0)
        //            {
        //                drpCourse.DataSource = courselist.Where(i => i.DaneshID == daneshId).ToList();
        //            }
        //            else
        //            {
        //                drpCourse.DataSource = courselist;
        //            }
        //            drpCourse.DataTextField = "name";
        //            drpCourse.DataValueField = "DID";
        //            drpCourse.DataBind();
        //        }
        //        else
        //        {
        //            drpCourse.Items.Clear();
        //            drpCourse.Items.Insert(0, "انتخاب کنید");
        //        }
        //    }
        //    else
        //    {
        //        drpCourse.Items.Clear();
        //        drpCourse.Items.Insert(0, "انتخاب کنید");
        //    }
        //}

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

        protected void drpChooseDanshkade_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (drpChooseDanshkade.SelectedIndex > 0)
            {
                RadComboBoxField.Enabled = true;
                daneshID = Convert.ToInt32(Session["DaneshId"]);
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
            else
            {
                resetform();
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
            RadComboBoxField.Items.Clear();
            drpCourse.Items.Clear();
            RadTimePicker1.Clear();
            RadTimePicker2.Clear();
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

        protected void RadComboBoxField_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (RadComboBoxField.SelectedIndex != 0)
            {
                CourseHandler corH = new CourseHandler();

                var courseListByUserId = corH.GetCourseListByUserID(Convert.ToInt32(RadComboBoxField.SelectedValue));
                if (courseListByUserId != null)
                {
                    courselist = courseListByUserId;
                    var courses = courselist.Distinct(new ComprareForListOfClassDistinc());
                    if (courses != null)
                    {
                        int daneshId = Convert.ToInt32(Session["DaneshId"] ?? 0);
                        if (daneshId != 0)
                        {
                            drpCourse.DataSource = courses.Where(i => i.DaneshID == daneshId).ToList();
                        }
                        else
                        {
                            drpCourse.DataSource = courses;
                        }
                        drpCourse.DataTextField = "name";
                        drpCourse.DataValueField = "DID";
                        drpCourse.DataBind();
                    }
                    else
                    {
                        drpCourse.Items.Clear();
                        drpCourse.Items.Insert(0, "انتخاب کنید");
                    }
                }
            }
            else
            {
                drpCourse.Items.Clear();
                drpCourse.Items.Insert(0, "انتخاب کنید");
            }
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

        protected void chkMeeting_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMeeting.Checked == true)
            {
                if (drpCategory.SelectedIndex == 2)
                {
                    chkMeeting.Text = "بدون استاد";
                }
                else
                {
                    chkMeeting.Text = " تخصیص سالن کنفرانس جهت برگزاری جلسات";
                }
                RadComboBoxField.Visible = false;
                valField.Enabled = false;
                drpCourse.Visible = false;
                trProfCrs.Visible = false;
                trDaneshkadeh.Visible = false;
            }
            else
            {

                if (!UtilityFunction.IsMasouleDaftarUser(roleID))
                {
                    RadComboBoxField.Visible = true;
                    valField.Enabled = true;
                    drpCourse.Visible = true;
                    trProfCrs.Visible = true;
                    trDaneshkadeh.Visible = true;
                }

            }
        }
    }
}