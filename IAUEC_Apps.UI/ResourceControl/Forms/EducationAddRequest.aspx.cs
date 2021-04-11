using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.university.Request;
using ResourceControl.BLL;
using ResourceControl.Entity;
using Telerik.Web.UI;
using IAUEC_Apps.DTO.ResourceControlClasses;

namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class EducationAddRequest : System.Web.UI.Page
    {

        private RequestHandler _requestHandler = new RequestHandler();
        private string PrevPage = string.Empty;
        protected void SetCustomTime()
        {
            txtTime.TimeView.CustomTimeValues = _requestHandler.GetTime();

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            SetCustomTime();
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
                AccessControl.MenuId = Session[sessionNames.menuID].ToString();
                AccessControl.UserId = Session[sessionNames.userID_Karbar].ToString();

            }
        }
        protected void btnRegister_OnClick(object sender, EventArgs e)
        {

            string message;
            if (!Page.IsValid) return;//اینجا باید رد الرت باشه

            var defenceInformation = _requestHandler.GetDefenceInformation(txtStCode.Text);
            var request = new StudentDefenceRequest
            {
                CategoryId = (int)IAUEC_Apps.DTO.ResourceControlClasses.Category.InPersonClass,
                Subject = StudentDefenceRequest.StaticStudentRequest().Subject,
                Location = ((int)IAUEC_Apps.DTO.ResourceControlClasses.Location.Raam).ToString(),
                Status = (int)RequestStatus.submitted,
                IssuerId = Convert.ToInt32(txtStCode.Text),
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
      

                // = ckbAprroveOnline.Checked ? drpRoleTeacher.SelectedItem.Text : string.Empty,
                UseOwnPc = rdbOwnSystem.Checked,
                UserId = Session[sessionNames.userName_Karbar].ToString(),
                Gender = defenceInformation.studentGender

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
            int reqId = 0;
            request.IsRequestEducation = true;
            message = _requestHandler.CreateStudentRequestForEducationV2(out reqId, request);

            if (message.Contains("ok"))
            {

                defenceInformation.RequestDate = request.RequestDate;
                defenceInformation.StartTime = request.RequestStartTime;
                defenceInformation.EndTime = request.RequestEndTime;

                string scrp = message.Substring(2);
                //PrevPage = ViewState["PrevPage"].ToString();
                //var uri = Request.UrlReferrer.ToString();
                //PrevPage = uri.ToLower().Replace("educationaddrequest.aspx", "EducationStudentReview.aspx");



                var userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
                var comman = new CommonBusiness();
                comman.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 171, string.Format("{0}", "ثبت درخواست جلسه دفاع توسط دانشکده"), reqId/*Convert.ToInt32(request.Id)*/);

                //CommonBusiness CommonBusiness = new CommonBusiness();
                //CommonBusiness.InsertIntoStudentLogReservation(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToString("HH:mm"), 11, 40, "ثبت درخواست رزرو جلسه دفاع توسط دانشجو", reqId);

                string address = "../Forms/EducationStudentReview.aspx" + "?id=" + generaterandomstr() + "@A" + "0" + "-" + generaterandomstr();
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
            var list = new List<Teacher>();
            var defenceInformation = _requestHandler.GetDefenceInformation(txtStCode.Text);
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
            //PrevPage = ViewState["PrevPage"].ToString();

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
            Session["ComeFromRes"] = "ok";
            Response.Redirect("~/University/Request/Pages/EditPersonalInformationUI.aspx");
        }

        protected void btnSerach_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStCode.Text.Trim()))
            {
                var daneshId = Convert.ToInt32(Session["DaneshId"]);
                CheckOutPajooheshBusiness business = new CheckOutPajooheshBusiness();
                var studentInfo = business.GetStudentInfoForPajohesh(txtStCode.Text.Trim());

                if (daneshId == Convert.ToInt32(studentInfo.Rows[0]["iddanesh"]) || daneshId == 0)
                {
                    RequestHandler rh = new RequestHandler();
                    if (rh.GetStRegisterd2(txtStCode.Text.Trim()).Rows.Count == 0)
                    {
                        RadWindowManager1.RadAlert("توجه: این دانشجو در ترم جاری انتخاب واحد ندارد.", 300, 100, "هشدار", null);
                    }

                   var defenceInformation = _requestHandler.GetDefenceInformation(txtStCode.Text);
                    var attendanceProfessores = _requestHandler.GetAttendanceProfessores(txtStCode.Text);
                    grdAttendanceProfessores.DataSource = attendanceProfessores.OrderBy(professor => professor.Name).Where(x => x.Kind != "کلاس درسی" && x.Date.ToGregorian().Date >= DateTime.Now.Date);
                    grdAttendanceProfessores.DataBind();
                    litSubject.Text = defenceInformation.DefenceSubject;

                    litGuide.Text = string.Concat(defenceInformation.FirstGuideFullName,
                        !string.IsNullOrEmpty(defenceInformation.SecondGuideFullName) ? "-" : "",
                        defenceInformation.SecondGuideFullName);

                    litConsultent.Text = string.Concat(defenceInformation.FirstConsultantFullName,
                        !string.IsNullOrEmpty(defenceInformation.SecondConsultantFullName) ? "-" : "",
                        defenceInformation.SecondConsultantFullName);

                    litReferee.Text = string.Concat(defenceInformation.FirstRefereeFullName,
                        !string.IsNullOrEmpty(defenceInformation.SecondRefereeFullName) ? "-" : "",
                        defenceInformation.SecondRefereeFullName);
                    litStudentName.Text = defenceInformation.StudentFullName;

                    litAprrovPropDate.Text = defenceInformation.GroupAcceptDate;

                    litMobile.Text = _requestHandler.GetStudentMobile(Convert.ToInt32(txtStCode.Text));

                    divNewRequest.Visible = true;
                    divAlert.Visible = false;
                }
                else
                {
                    divAlert.Visible = true;
                    divNewRequest.Visible = false;
                }

            }
        }
    }
}