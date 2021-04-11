using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.DTO.ResourceControlClasses;
using IAUEC_Apps.UI.University.GraduateAffair.CMS;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using ResourceControl.BLL;
using ResourceControl.Entity;
using Telerik.Web.UI;
using Telerik.Web.UI.ComboBox;
using Category = IAUEC_Apps.DTO.ResourceControlClasses.Category;
using Location = IAUEC_Apps.DTO.ResourceControlClasses.Location;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.Conatct;

namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class StudentReview : System.Web.UI.Page
    {
        
        //private string PrevPage = string.Empty;

        RequestHandler _requestHandler = new RequestHandler();
        public static int StatusNumber;
        public static string NextStatusText;
        public static string Istechnical;
        public static bool IsDeleted;
        public static bool IsWaste;
        public static string AlertText;
        public static bool IsNotOnline;

        protected void SetCustomTime()
        {
            txtTime.TimeView.CustomTimeValues = _requestHandler.GetTime();
        }

 
        protected void Page_Load(object sender, EventArgs e)
        {
            SetCustomTime();
            if (!Page.IsPostBack)
            {
                var userId = Convert.ToInt32(Session[sessionNames.userID_StudentOstad].ToString());
               var studentDefenceRequestList = _requestHandler.GetStudentDefenceRequest(userId);
               var defenceInformation = _requestHandler.GetDefenceInformation(userId.ToString());
                var attendanceProfessores = _requestHandler.GetAttendanceProfessores(userId.ToString());
                grdAttendanceProfessores.DataSource = attendanceProfessores.OrderBy(professor => professor.Name).Where(x => x.Kind != "کلاس درسی" && x.Date.ToGregorian().Date >= DateTime.Now.Date);
                grdAttendanceProfessores.DataBind();
                studentDefenceRequestList.DefaultView.Sort = "ID DESC";
                grdLastRequest.DataSource = studentDefenceRequestList;
                grdLastRequest.DataBind();

                var listOfDefenceRequest =
                    RequestHandler.ConvertDataTableToList<StudentDefenceRequestDTO>(studentDefenceRequestList);
                var inCirculationRequest =
                    listOfDefenceRequest.FirstOrDefault(
                        x => x.isDeleted != true && x.RequestDate.StringPersianDateToGerogorianDate() >= DateTime.Now) ??
                    listOfDefenceRequest.OrderByDescending(x => x.ID).FirstOrDefault();
                if (inCirculationRequest == null) return;
                litReqId.Text = inCirculationRequest.ID.ToString();
                litReqRegisterDate.Text = inCirculationRequest.issue_time;
                litReqDenfenceDate.Text = inCirculationRequest.RequestDate;
                litDefenceTime.Text = inCirculationRequest.StartTime.Split(':')[0] + ":" + "00";
                litStudentMobile.Text = (userId==99900999? "9192678116":_requestHandler.GetStudentMobile(userId));
                StatusNumber = inCirculationRequest.status;

                Istechnical = (inCirculationRequest.IsEquippingResource || !string.IsNullOrEmpty(inCirculationRequest.OnlineTeacherRole)).ToString();
               
                var statusDefenceTechnical= _requestHandler.GetStatusDefenceTechnical(inCirculationRequest.RequestId);
                string rejectOstads = "";
                if (inCirculationRequest.status == 1&& Convert.ToBoolean(Istechnical) ==true)
                {
                    var defence = _requestHandler.GetSignutreOstad(defenceInformation);
                    if (defence != null)
                        rejectOstads = StringReasonRejectOstads(statusDefenceTechnical, defence);
                }
                NextStatusText = StudentRequestStatus.NextStatusText(inCirculationRequest.status, Convert.ToBoolean(Istechnical), rejectOstads);
                IsDeleted = inCirculationRequest.isDeleted;

               
                if (!IsDeleted)
                    IsWaste =    ((inCirculationRequest.status ==0||inCirculationRequest.status == 9)
                            //   && (inCirculationRequest.RequestDate.StringPersianDateToGerogorianDate().Date < RequestHandler.WorkingDays48h(DateTime.Now.Date).Date)
                               && (//!(inCirculationRequest.IsRequestEducation)||
                               inCirculationRequest.RequestDate.StringPersianDateToGerogorianDate() < DateTime.Now) )
                               ||( (inCirculationRequest.status == 1 || inCirculationRequest.status == 3) && (inCirculationRequest.RequestDate.StringPersianDateToGerogorianDate() < DateTime.Now));
                else
                {
                    string rejectostad = RejectOstad(inCirculationRequest);
                    if (rejectostad != "1")
                    {
                        PanelTeacher.Visible = true;
                        PanelColege.Visible = false;
                        litostad.Text = rejectostad;
                    }
                    else { 
                        PanelTeacher.Visible = false;
                        PanelColege.Visible = true;
                        litIsDeleted.Text = inCirculationRequest.answernote;
                    }
                }

                txtDate.Text = inCirculationRequest.RequestDate;
                txtTime.SelectedTime = new TimeSpan(0, Convert.ToInt32(inCirculationRequest.StartTime.Split(':')[0]), 0,0);
                //litStudentName.Text = inCirculationRequest.StudentFullName;

                if (!string.IsNullOrEmpty(inCirculationRequest.OnlineTeacherRole.Trim()))
                {

                    ckbAprroveOnline.Checked = true;
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
                    IsNotOnline = false;

                }
                else
                {
                    IsNotOnline = true;
                }

                rdbOwnSystem.Checked = inCirculationRequest.IsUseOwnSystem;

                if (!IsDeleted)
                {
                    AlertText = "alert-success";
                }
                else
                {
                    AlertText = "alert-danger";
                }

                hdnRequestId.Value = inCirculationRequest.RequestId.ToString();
                hdnStatus.Value = inCirculationRequest.status.ToString();

                //sadeghsaryazdy
                DataTable dtDefenceMeetingsOnline = _requestHandler.GetDefenceMeetingsOnline(userId.ToString());
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
                LblLastDate.Value = inCirculationRequest.RequestDate;
                LblLastTime.Value = inCirculationRequest.StartTime;

            }
        }
        public string RejectOstad(StudentDefenceRequestDTO inCirculationRequest)
        {
            if (inCirculationRequest.FlagAcceptDavin == false)
            {
                return "داور داخلی";
            }
            else if (inCirculationRequest.FlagAcceptDavOut == false)
            {
                return "داور خارجی";
            }
            else if (inCirculationRequest.FlagAcceptMosh1 == false)
            {
                return "مشاور اول";
            }
            else if (inCirculationRequest.FlagAcceptMosh2 == false)
            {
                return "مشاور دوم";
            }
            else if (inCirculationRequest.FlagAcceptRah1 == false)
            {
                return "راهنما اول";
            }
            else if (inCirculationRequest.FlagAcceptRah2 == false)
            {
                return "راهنما دوم";
            }
            else return "1";
        }
        protected void deleteRequest_OnServerClick(object sender, EventArgs e)
        {
            var status = Convert.ToInt32(hdnStatus.Value);
            if (status > 0)
            {
                var mesg = "شما تنها قبل از تایید دانشکده امکان حذف دارید.";

                // var uri = Request.UrlReferrer.ToString();
                // PrevPage = uri.Replace("StudentAddRequest.aspx", "StudentReview.aspx");
                // var requestRawUrl = Request.RawUrl;

                string address = "../Forms/StudentReview.aspx";
                string resdirectFunc = "function redirectToLast(){ window.location= '" + address + "' ; }";
                RadWindowManager1.RadAlert(mesg, 500, 100, "پیام سیستم", resdirectFunc);
            }
            else
            if (_requestHandler.DeleteStudentRequest(Convert.ToInt32(hdnRequestId.Value)))
            {
               // var PrevPage = "";
                //var uri = Request.UrlReferrer.ToString();
                //if (uri.ToLower().Contains("forms/studentreview.aspx"))
                 //   PrevPage = uri.ToLower().Replace("resourceControl/forms/studentReview.aspx", "CommonUI/IntroPage.aspx");

                CommonBusiness CommonBusiness = new CommonBusiness();
                CommonBusiness.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToString("HH:mm"), 11, 42, "حذف درخواست رزرو جلسه دفاع توسط دانشجو", Convert.ToInt32(hdnRequestId.Value));

                string address = "../Forms/StudentReview.aspx";
                string resdirectFunc = "function redirectToLast(){ window.location= '" + address + "' ; }";
                string scrp = "درخواست شما با موفقیت انجام پذیرفت";
                RadWindowManager1.RadAlert(scrp, 0, 100, " پیام سیستم", resdirectFunc);
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



        protected void btnRegister_OnClick(object sender, EventArgs e)
        {

            string message;
            if (!Page.IsValid) return;//اینجا باید رد الرت باشه


            var userId = Convert.ToInt32(Session[sessionNames.userID_StudentOstad].ToString());
            var defenceInformation = _requestHandler.GetDefenceInformation(userId.ToString());
            var studentDefenceRequestList = _requestHandler.GetStudentDefenceRequest(userId);

            var listOfDefenceRequest = RequestHandler.ConvertDataTableToList<StudentDefenceRequestDTO>(studentDefenceRequestList);
            var inCirculationRequest =
                listOfDefenceRequest.FirstOrDefault(
                    x => x.isDeleted != true && x.RequestDate.StringPersianDateToGerogorianDate() >= DateTime.Now) ??
                listOfDefenceRequest.OrderByDescending(x => x.ID).FirstOrDefault();

            if (txtTime.SelectedTime == null) return;
            var request = new StudentDefenceRequest
            {
                IssuerId = Convert.ToInt32(Session[sessionNames.userID_StudentOstad].ToString()),
                RequestDate = txtDate.Text,
                RequestStartTime = txtTime.SelectedTime.Value.Ticks,
                OnlineTeacherRole = ckbAprroveOnline.Checked ? drpRoleTeacher.SelectedValue : string.Empty,
                OnlineFirstTeacherName = ckbAprroveOnline.Checked && rcbOnlineTeacher.Items[0].Checked ? rcbOnlineTeacher.Items[0].Text : string.Empty,
                OnlineFirstTeacherId = ckbAprroveOnline.Checked && rcbOnlineTeacher.Items[0].Checked ? rcbOnlineTeacher.Items[0].Value.Substring(3) : string.Empty,
                UseOwnPc = rdbOwnSystem.Checked,
                UserId = Session[sessionNames.userID_StudentOstad].ToString(),
                Gender = defenceInformation.studentGender,
                DaneshId = Convert.ToInt32(inCirculationRequest.CollegeId)
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
            request.IsEdited = true;
            request.Id = inCirculationRequest.ID;
            request.Status = inCirculationRequest.status;
            //sadeghsaryazdi
            request.FlagDoingMeetingOnline = chkDoingOnlineDefence.Checked ? true : false;
            request.IsRequestEducation = false;
            message = _requestHandler.UpdateStudentRequest(request);

            if (message.Contains("ok"))
            {

                defenceInformation.RequestDate = request.RequestDate;
                defenceInformation.StartTime = request.RequestStartTime;
                defenceInformation.EndTime = request.RequestEndTime;

                string scrp = message.Substring(2);
                //PrevPage = ViewState["PrevPage"].ToString();
                //var uri = Request.UrlReferrer.ToString();
               // PrevPage = uri.Replace("StudentAddRequest.aspx", "StudentReview.aspx");
               // var requestRawUrl = Request.RawUrl;

                CommonBusiness CommonBusiness = new CommonBusiness();
                CommonBusiness.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToString("HH:mm"), 11, 41, "ویرایش درخواست رزرو جلسه دفاع توسط دانشجو", request.Id);
                //if (LblLastDate.Value != txtDate.Text || LblLastTime.Value != txtTime.SelectedTime.Value.Ticks.ToString())
                //{
                   
                    const bool editMsg = true;

                //DateTime dateTimeEnd = RequestHandler.WorkingDays24h(DateTime.Now);
                DateTime dateTimeEnd = RequestHandler.WorkingDays12h(DateTime.Now);
                SendSmsContactBuisnes.SendSmsOsForOstadsDefence(userId.ToString(), request.IssuerName,
                                        request.RequestDate, dateTimeEnd.Date.ToPeString(), txtTime.SelectedTime.Value.ToString().Substring(0, 5), dateTimeEnd.Hour + ":" + dateTimeEnd.Minute, editMsg);
                //}

                string address = "../Forms/StudentReview.aspx";
                string resdirectFunc = "function redirectToLast(){ window.location= '" + address + "' ; }";
                RadWindowManager1.RadAlert(scrp, 500, 100, "پیام سیستم", resdirectFunc);
            }
            else
            {
                RadWindowManager1.RadAlert(message, 500, 100, "خطا", "");
            }
        }

        protected void drpRoleTeacher_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var userId = Convert.ToInt32(Session[sessionNames.userID_StudentOstad].ToString());
            var defenceInformation = _requestHandler.GetDefenceInformation(userId.ToString());
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
            string address = "../Forms/StudentReview.aspx";
            Response.Redirect(address);
        }
        protected void btnRedirectToEditInfo_OnServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/University/Request/Pages/EditPersonalInformationUI.aspx");
        }

        protected void redirectToBack_OnServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~/CommonUI/IntroPage.aspx");
        }

        protected void redirectToNew_OnServerClick(object sender, EventArgs e)
        {
            string address = "~/ResourceControl/Forms/StudentAddRequest.aspx";
            Response.Redirect(address);
        }



        public string StringReasonRejectOstads(StatusDefenceTechnichal inp , List<InformationOstadForDefenceStudent> def)
        {
            string reason="";
            if (inp != null && (
                inp.FlagAcceptTechnicalMosh1 == false || inp.FlagAcceptTechnicalRah1 == false
                || inp.FlagAcceptTechnicalDavIn == false || inp.FlagAcceptTechnicalDavOut == false
                || inp.FlagAcceptTechnicalMosh2 == false || inp.FlagAcceptTechnicalRah2 == false))
            {
                reason = "دانشجوی گرامی ، درخواست رزرو جلسه دفاع شما در گردش می باشد اما ";
                if(inp.FlagAcceptTechnicalMosh1==false)
                {
                    reason += " استاد مشاور شما سرکارخانم/جناب آقای  ";
                    reason += def.Where(c => c.IdTypeOs == 2).Select(c => c.FullName).FirstOrDefault();
                    reason += " به علت ";
                    reason += inp.ReasonTechnicalMosh1;
                    reason += "و";

                }
                if (inp.FlagAcceptTechnicalRah1 == false)
                {
                    reason += " استاد راهنما شما سرکارخانم/جناب آقای  ";
                    reason += def.Where(c => c.IdTypeOs == 3).Select(c => c.FullName).FirstOrDefault();
                    reason += " به علت ";
                    reason += inp.ReasonTechnicalRah1;
                    reason += "و";

                }
                if (inp.FlagAcceptTechnicalDavIn == false)
                {
                    reason += " استاد داور شما سرکارخانم/جناب آقای  ";
                    reason += def.Where(c => c.IdTypeOs == 4).Select(c => c.FullName).FirstOrDefault();
                    reason += " به علت ";
                    reason += inp.ReasonTechnicalDavin;
                    reason += "و";

                }
                if (inp.FlagAcceptTechnicalDavOut== false)
                {
                    reason += " استاد داور شما سرکارخانم/جناب آقای  ";
                    reason += def.Where(c => c.IdTypeOs == 5).Select(c => c.FullName).FirstOrDefault();
                    reason += " به علت ";
                    reason += inp.ReasonTechnicalDavOut;
                    reason += "و";

                }
                if (inp.FlagAcceptTechnicalMosh2 == false)
                {
                    reason += " استاد مشاور شما سرکارخانم/جناب آقای  ";
                    reason += def.Where(c => c.IdTypeOs == 6).Select(c => c.FullName).FirstOrDefault();
                    reason += " به علت ";
                    reason += inp.ReasonTechnicalMosh2;
                    reason += "و";

                }
                if (inp.FlagAcceptTechnicalRah2 == false)
                {
                    reason += " استاد راهنما شما سرکارخانم/جناب آقای  ";
                    reason += def.Where(c => c.IdTypeOs == 7).Select(c => c.FullName).FirstOrDefault();
                    reason += " به علت ";
                    reason += inp.ReasonTechnicalRah2;
                    reason += "و";

                }
                reason = reason.TrimEnd('و');
                reason += " این درخواست را رد کردند . ";
            }
            return reason;
        }
    }
}