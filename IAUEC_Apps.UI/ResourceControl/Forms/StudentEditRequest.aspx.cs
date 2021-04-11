using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.DTO.ResourceControlClasses;
using IAUEC_Apps.UI.University.GraduateAffair.CMS;
using ResourceControl.BLL;
using ResourceControl.Entity;
using Telerik.Web.UI;
using Category = IAUEC_Apps.DTO.ResourceControlClasses.Category;
using Location = IAUEC_Apps.DTO.ResourceControlClasses.Location;
using IAUEC_Apps.Business.Common;
using System.Data;
using IAUEC_Apps.Business.Conatct;

namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class StudentEditRequest : System.Web.UI.Page
    {
        private RequestHandler _requestHandler = new RequestHandler();
        private string PrevPage = string.Empty;
        private int requestId;

        protected void SetCustomTime()
        {
            txtTime.TimeView.CustomTimeValues = _requestHandler.GetTime();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SetCustomTime();
            if (!IsPostBack)
            {
                if (Request.QueryString["reqId"] != null)
                {
                    hdnrequestId.Value = Request.QueryString["reqId"];

                }
                //if (Request.UrlReferrer != null)
                //    ViewState["PrevPage"] = Request.UrlReferrer.ToString();
                //var requestID = Convert.ToInt32(Session["StudentrequestIdForEdit"]);

                var requestDetails = _requestHandler.GetRequestDetails(Convert.ToInt32(hdnrequestId.Value));


                RequestDateTimeHandler rqdateTimeH = new RequestDateTimeHandler();
                //var _dateTimeList = rqdateTimeH.GetDateTimeListByRequestId(Convert.ToInt32(hdnrequestId.Value));



                var userId = requestDetails.IssuerID.ToString();
                hdnUserId.Value = userId;
                var defenceInformation = _requestHandler.GetDefenceInformation(userId);
                var attendanceProfessores = _requestHandler.GetAttendanceProfessores(userId);
                grdAttendanceProfessores.DataSource = attendanceProfessores.OrderBy(professor => professor.Name).Where(x => x.Kind != "کلاس درسی" && x.Date.ToGregorian().Date >= DateTime.Now.Date);
                grdAttendanceProfessores.DataBind();

                var _studentDefenceRequestList = _requestHandler.GetStudentDefenceRequest(requestDetails.IssuerID);
                var listOfDefenceRequest = RequestHandler.ConvertDataTableToList<StudentDefenceRequestDTO>(_studentDefenceRequestList);
                var inCirculationRequest =
                listOfDefenceRequest.FirstOrDefault(
                x => x.RequestId == Convert.ToInt32(hdnrequestId.Value));

                litSubject.Text = defenceInformation.DefenceSubject;

                litGuide.Text = string.Concat(defenceInformation.FirstGuideFullName, !string.IsNullOrEmpty(defenceInformation.SecondGuideFullName) ? "-" : "",
                    defenceInformation.SecondGuideFullName);

                litConsultent.Text = string.Concat(defenceInformation.FirstConsultantFullName, !string.IsNullOrEmpty(defenceInformation.SecondConsultantFullName) ? "-" : "",
                    defenceInformation.SecondConsultantFullName);

                litReferee.Text = string.Concat(defenceInformation.FirstRefereeFullName, !string.IsNullOrEmpty(defenceInformation.SecondRefereeFullName) ? "-" : "",
                    defenceInformation.SecondRefereeFullName);
                litStudentName.Text = defenceInformation.StudentFullName;


                txtDate.Text = inCirculationRequest.RequestDate;
                txtTime.SelectedTime = new TimeSpan(0, Convert.ToInt32(inCirculationRequest.StartTime.Split(':')[0]), 0,
                    0);
                litStudentName.Text = inCirculationRequest.StudentFullName;

                if (!string.IsNullOrEmpty(inCirculationRequest.OnlineTeacherRole.Trim()))
                {

                    ckbAprroveOnline.Checked = true;
                    OnlineTeacherPanel.Visible = ckbAprroveOnline.Checked;
                    drpRoleTeacher.SelectedValue = inCirculationRequest.OnlineTeacherRole;

                    var list = new List<Teacher>();
                    switch (drpRoleTeacher.SelectedValue)
                    {
                        case "guide":
                            var firstGuidText = defenceInformation.FirstGuideFullName;
                            var firstGuidValue = defenceInformation.FirstGuideId;
                            var secoundGuidText = defenceInformation.SecondGuideFullName;
                            var secoundGuidValue = defenceInformation.SecondGuideId;
                            if (!string.IsNullOrEmpty(firstGuidText))
                                list.Add(new Teacher { text = firstGuidText, Value = firstGuidValue });
                            if (!string.IsNullOrEmpty(secoundGuidText))
                                list.Add(new Teacher { text = secoundGuidText, Value = secoundGuidValue });
                            break;
                        case "consultant":
                            var firstConsultantText = defenceInformation.FirstConsultantFullName;
                            var firstConsultantValue = defenceInformation.FirstConsultantId;
                            var secoundConsultantText = defenceInformation.SecondConsultantFullName;
                            var secoundConsultantValue = defenceInformation.SecondConsultantId;
                            if (!string.IsNullOrEmpty(firstConsultantText))
                                list.Add(new Teacher { text = firstConsultantText, Value = firstConsultantValue });
                            if (!string.IsNullOrEmpty(secoundConsultantText))
                                list.Add(new Teacher { text = secoundConsultantText, Value = secoundConsultantValue });
                            break;
                    }
                    rcbOnlineTeacher.DataSource = list;
                    rcbOnlineTeacher.DataTextField = "text";

                    rcbOnlineTeacher.DataValueField = "Value";
                    rcbOnlineTeacher.DataBind();

                    foreach (RadComboBoxItem item in rcbOnlineTeacher.Items)
                    {
                        if (item.Value?.Substring(3) == inCirculationRequest.OnlineFirstTeacherId.ToString())
                        {

                            item.Checked = true;


                        }
                        if (item.Value?.Substring(3) == inCirculationRequest.OnlineSecondTeacherId.ToString())
                        {
                            item.Checked = true;

                        }

                    }

                }

                rdbOwnSystem.Checked = inCirculationRequest.IsUseOwnSystem;

                ckbOnlineShow.Checked = inCirculationRequest.IsEquippingResource;

                //sadeghsaryazdi
                DataTable dtDefenceMeetingsOnline = _requestHandler.GetDefenceMeetingsOnline(userId);
                if (dtDefenceMeetingsOnline != null && dtDefenceMeetingsOnline.Rows.Count > 0)
                {
                    panelDoingDefence.Visible = true;
                    lblMoney.Text = int.Parse(dtDefenceMeetingsOnline.Rows[0]["money"].ToString()).ToString("#,###")
                         + " " + dtDefenceMeetingsOnline.Rows[0]["currency"].ToString(); 
                    litStOnline.Text = dtDefenceMeetingsOnline.Rows[0]["StudentFullName"].ToString();
                    chkDoingOnlineDefence.Checked = inCirculationRequest.FlagDoingMeetingOnline ? true : false;
                }
                else
                    panelDoingDefence.Visible = false;

                //if (LblLastDate.Value != txtDate.Text || LblLastTime.Value != txtTime.SelectedTime.Value.Ticks.ToString())
                //{
                //    string endDate = DateTime.Parse(inCirculationRequest.RequestDate.ToString()).AddDays(2).ToShortDateString();

                //    SendSmsContactBuisnes.SendSmsOsForOstadsDefence(userId.ToString(), inCirculationRequest.issuerName,
                //                        inCirculationRequest.RequestDate, endDate, txtTime.SelectedTime.Value.ToString(), "18:00");
                //}

            }

            if (hdnrequestId.Value != null)
            {
                hdnrequestId.Value = hdnrequestId.Value;
            }




        }

        protected void btnRegister_OnClick(object sender, EventArgs e)
        {

            string message;
            if (!Page.IsValid) return;//اینجا باید رد الرت باشه



            var defenceInformation = _requestHandler.GetDefenceInformation(hdnUserId.Value.ToString());
            var request = new StudentDefenceRequest
            {
                CategoryId = (int)Category.InPersonClass,
                Subject = StudentDefenceRequest.StaticStudentRequest().Subject,
                Location = ((int)Location.Raam).ToString(),
                Status = (int)RequestStatus.sent,
                IssuerId = Convert.ToInt32(hdnUserId.Value.ToString()),
                IssuerName = defenceInformation.StudentFullName,
                Capacity = StudentDefenceRequest.StaticStudentRequest().Capacity,
                DefenceSubject = defenceInformation.DefenceSubject,
                DaneshId = Convert.ToInt32(defenceInformation.CollegeId),
                CourseName = defenceInformation.DefenceSubject,
                RequestDate = txtDate.Text,
                RequestStartTime = txtTime.SelectedTime.Value.Ticks,
                OnlineTeacherRole = ckbAprroveOnline.Checked ? drpRoleTeacher.SelectedValue : string.Empty,
                OnlineFirstTeacherName = ckbAprroveOnline.Checked && rcbOnlineTeacher.Items[0].Checked ? rcbOnlineTeacher.Items[0].Text : string.Empty,
                OnlineFirstTeacherId = ckbAprroveOnline.Checked && rcbOnlineTeacher.Items[0].Checked ? rcbOnlineTeacher.Items[0].Value.Substring(3) : string.Empty,
                IsEquippingResource = ckbOnlineShow.Checked,
                  
                // = ckbAprroveOnline.Checked ? drpRoleTeacher.SelectedItem.Text : string.Empty,
                UseOwnPc = rdbOwnSystem.Checked,
                UserId = Session[sessionNames.userID_Karbar].ToString(),
                Gender = defenceInformation.studentGender,

                //sadeghsaryazdi
                FlagDoingMeetingOnline = chkDoingOnlineDefence.Checked ? true : false,


            };
            if (rcbOnlineTeacher.Items.Count > 1)
            {
                request.OnlineSecondTeacherName = ckbAprroveOnline.Checked && rcbOnlineTeacher.Items[1].Checked
                    ? rcbOnlineTeacher.Items[1].Text
                    : string.Empty;
                request.OnlineSecondTeacherId = ckbAprroveOnline.Checked && rcbOnlineTeacher.Items[1].Checked
                    ? rcbOnlineTeacher.Items[1].Value.Substring(3)
                    : string.Empty;
            }
            request.RequestEndTime = request.RequestStartTime + _requestHandler.GetDefenceInMeetingLength(Convert.ToInt32(defenceInformation.CollegeId));
            request.AcceptPropDate = defenceInformation.GroupAcceptDate;
            request.Id = Convert.ToInt32(hdnrequestId.Value);
            request.IsRequestEducation = true;
            message = _requestHandler.UpdateStudentRequestForEducation(request);




            if (message.Contains("ok"))
            {

                defenceInformation.RequestDate = request.RequestDate;
                defenceInformation.StartTime = request.RequestStartTime;
                defenceInformation.EndTime = request.RequestEndTime;

                string scrp = message.Substring(2);

                string address = "EducationStudentReview.aspx" + "?id=" + generaterandomstr() + "@A" + "0" + "-" + generaterandomstr();

                var userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
                var comman = new CommonBusiness();
                comman.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 166, string.Format("{0}", "ویرایش درخواست جلسه دفاع توسط دانشکده"), Convert.ToInt32(request.Id));
             
                 //sadegh saryazdy 
                //  _requestHandler.UpdateRequest_LinkMeeting(stcode,"")

                string resdirectFunc = "function redirectToLast(){ window.location= '" + address + "' ; }";
                RadWindowManager1.RadAlert(scrp, 500, 100, "پیام سیستم", resdirectFunc);
            }
            else
            {
                RadWindowManager1.RadAlert(message, 500, 100, "خطا", "");
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

        protected void drpRoleTeacher_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            var defenceInformation = _requestHandler.GetDefenceInformation(hdnUserId.Value);
            var list = new List<Teacher>();
            switch (drpRoleTeacher.SelectedValue)
            {
                case "guide":
                    var firstGuidText = defenceInformation.FirstGuideFullName;
                    var firstGuidValue = defenceInformation.FirstGuideId;
                    var secoundGuidText = defenceInformation.SecondGuideFullName;
                    var secoundGuidValue = defenceInformation.SecondGuideId;
                    if (!string.IsNullOrEmpty(firstGuidText))
                        list.Add(new Teacher { text = firstGuidText, Value = firstGuidValue });
                    if (!string.IsNullOrEmpty(secoundGuidText))
                        list.Add(new Teacher { text = secoundGuidText, Value = secoundGuidValue });
                    break;
                case "consultant":
                    var firstConsultantText = defenceInformation.FirstConsultantFullName;
                    var firstConsultantValue = defenceInformation.FirstConsultantId;
                    var secoundConsultantText = defenceInformation.SecondConsultantFullName;
                    var secoundConsultantValue = defenceInformation.SecondConsultantId;
                    if (!string.IsNullOrEmpty(firstConsultantText))
                        list.Add(new Teacher { text = firstConsultantText, Value = firstConsultantValue });
                    if (!string.IsNullOrEmpty(secoundConsultantText))
                        list.Add(new Teacher { text = secoundConsultantText, Value = secoundConsultantValue });
                    break;
            }
            rcbOnlineTeacher.DataSource = list;
            rcbOnlineTeacher.DataTextField = "text";
            rcbOnlineTeacher.DataValueField = "Value";
            rcbOnlineTeacher.DataBind();
            txtDate.Text = txtDate.Text;
        }

        //protected void CustomValidatorckbAprroveOnline_OnServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    var flag = false;
        //    foreach (RadComboBoxItem item in rcbOnlineTeacher.Items)
        //    {
        //        if (item.Checked)
        //        {
        //            flag = true;
        //            break;
        //            ;
        //        }
        //    }
        //    if (drpRoleTeacher.SelectedIndex > 0 && flag)
        //        args.IsValid = true;
        //    else
        //        args.IsValid = false;
        //}

        protected void CustomValidatorrcbOnlineTeacher_OnServerValidate(object source, ServerValidateEventArgs args)
        {
            var flag = false;
            foreach (RadComboBoxItem item in rcbOnlineTeacher.Items)
            {
                if (item.Checked)
                {
                    flag = true;
                    break;
                    ;
                }
            }
            if (ckbAprroveOnline.Checked)
            {
                if (flag)
                {
                    args.IsValid = true;
                }
                else
                {

                    args.IsValid = false;


                }
            }

        }
        protected void CustomValidatorrcbOnlineTeacherRole_OnServerValidate(object source, ServerValidateEventArgs args)
        {

            if (ckbAprroveOnline.Checked)
            {
                if (drpRoleTeacher.SelectedIndex > 0)
                {
                    args.IsValid = true;
                }
                else
                {

                    args.IsValid = false;


                }
            }
        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            // PrevPage = ViewState["PrevPage"].ToString();

            string address = "../Forms/EducationStudentReview.aspx" + "?id=" + generaterandomstr() + "@A" + "0" + "-" + generaterandomstr();
            Response.Redirect(address);
        }

        protected void ckbAprroveOnline_OnCheckedChanged(object sender, EventArgs e)
        {
            if (ckbAprroveOnline.Checked)
                OnlineTeacherPanel.Visible = true;
            else
                OnlineTeacherPanel.Visible = false;

        }

        protected void btnRedirectToEditInfo_OnServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/University/Request/Pages/EditPersonalInformationUI.aspx");
        }
    }


}