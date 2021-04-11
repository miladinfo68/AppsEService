using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.DTO.ResourceControlClasses;
using ResourceControl.BLL;
using ResourceControl.Entity;
using Telerik.Web.UI;
using Category = IAUEC_Apps.DTO.ResourceControlClasses.Category;
using Location = IAUEC_Apps.DTO.ResourceControlClasses.Location;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.Business.Conatct;
using IAUEC_Apps.Business.Conatct.Functions;

namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class StudentAddRequest : System.Web.UI.Page
    {
        private readonly RequestHandler _requestHandler = new RequestHandler();
        public bool ShowConfirm;
        AdobeConnectDTO adobeConnectDTO = new AdobeConnectDTO();
        protected void SetCustomTime()
        {
            txtTime.TimeView.CustomTimeValues = _requestHandler.GetTime();
          
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = Session[sessionNames.userID_StudentOstad].ToString();
            SetCustomTime();

            if (!IsPostBack)
            {
                litMobile.Text =(userId== "99900999"?"09192678116" :_requestHandler.GetStudentMobile(Convert.ToInt32(userId)));
                txtMbileChanged.Text = litMobile.Text;
             
                DataTable dtDefenceMeetingsOnline = _requestHandler.GetDefenceMeetingsOnline(userId);
                if (dtDefenceMeetingsOnline != null && dtDefenceMeetingsOnline.Rows.Count > 0)
                {
                    panelDoingDefence.Visible = true;
                    lblMoney.Text = int.Parse(dtDefenceMeetingsOnline.Rows[0]["money"].ToString()).ToString("#,###")
                                              +" "+dtDefenceMeetingsOnline.Rows[0]["currency"].ToString();
                    litStOnline.Text = dtDefenceMeetingsOnline.Rows[0]["StudentFullName"].ToString();
                }
                else
                    panelDoingDefence.Visible = false;

                if (userId != "99900999")
                {
                    var defenceInformation = _requestHandler.GetDefenceInformation(userId);
                    var attendanceProfessores = _requestHandler.GetAttendanceProfessores(userId);
                    grdAttendanceProfessores.DataSource = attendanceProfessores.OrderBy(professor => professor.Name)
                        .Where(x => x.Kind != "کلاس درسی" && x.Date.ToGregorian().Date >= DateTime.Now.Date);
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
                }
                else
                {
                    litSubject.Text = "ارزیابی عملکرد تلفیقی فرایند بیولوژیکی و فیزیکوشیم";

                    litGuide.Text = "فربد ساسانی";

                    litConsultent.Text = "مهدی رحمتی";

                    litReferee.Text = "مجید حسینی";
                    litStudentName.Text = "محمد سرگزی";

                    litAprrovPropDate.Text = "1397/11/27";
                }

            }

  



            
            ShowConfirm = Convert.ToInt32(Session["CountApprove"]) == 1;


        }


        protected void btnRegister_OnClick(object sender, EventArgs e)
        {

         //   lblCommitmentValidate.Visible = false;
            if (!Page.IsValid) return;//اینجا باید رد الرت باشه

  
            
            // sadegh saryazdi
            //DateTime dateTime = new DateTime(2020, 04, 20);//اول اردیبهشت 1399
            //if (DateTime.Now > dateTime&&!CheckRequest()) return;//بررسی سه دفاع
            //این شرط را داخل در جاییی که شرط ها بررسی می‌شوند بگذار

            var userId = Session[sessionNames.userID_StudentOstad].ToString();
            StudentDefenceRequest request = new StudentDefenceRequest();
            var defInfo = _requestHandler.GetDefenceInformation(userId);
            if (userId== "99900999")
            {
                 request = new StudentDefenceRequest
                {
                    CategoryId = 2,
                    Subject = "ارزیابی عملکرد تلفیقی فرایند بیولوژیکی و فیزیکوشیم",
                    Location = "2",
                    Status = 0,
                    IssuerId = 99900999,
                    IssuerName = "محمد سرگزی",
                    Capacity = 1,
                    DefenceSubject = "ارزیابی عملکرد تلفیقی فرایند بیولوژیکی و فیزیکوشیم",
                    DaneshId = 3,
                    CourseName = "ارزیابی عملکرد تلفیقی فرایند بیولوژیکی و فیزیکوشیم",
                    RequestDate = txtDate.Text,
                    RequestStartTime = txtTime.SelectedTime.Value.Ticks,
                    OnlineTeacherRole = ckbAprroveOnline.Checked ? drpRoleTeacher.SelectedValue : string.Empty,
                    OnlineFirstTeacherName = ckbAprroveOnline.Checked && rcbOnlineTeacher.Items[0].Checked ? rcbOnlineTeacher.Items[0].Text : string.Empty,
                    OnlineFirstTeacherId = ckbAprroveOnline.Checked && rcbOnlineTeacher.Items[0].Checked ? rcbOnlineTeacher.Items[0].Value.Substring(3) : string.Empty,

                    //sadeghsaryazdi

                    FlagDoingMeetingOnline = chkDoingOnlineDefence.Checked ? true : false,

                    // = ckbAprroveOnline.Checked ? drpRoleTeacher.SelectedItem.Text : string.Empty,
                    UseOwnPc = rdbOwnSystem.Checked,
                    UserId = Session[sessionNames.userID_StudentOstad].ToString(),
                    Gender = "1"

                };

                request.AcceptPropDate = "1397/11/27";


            }
            else
            { 
         

                 request = new StudentDefenceRequest
                {
                    CategoryId = (int)Category.InPersonClass,
                    Subject = StudentDefenceRequest.StaticStudentRequest().Subject,
                    Location = ((int)Location.Raam).ToString(),
                    Status = (int)RequestStatus.submitted,
                    IssuerId = Convert.ToInt32(Session[sessionNames.userID_StudentOstad].ToString()),
                    IssuerName = defInfo.StudentFullName,
                    Capacity = StudentDefenceRequest.StaticStudentRequest().Capacity,
                    DefenceSubject = defInfo.DefenceSubject,
                    DaneshId = Convert.ToInt32(defInfo.CollegeId),
                    CourseName = defInfo.DefenceSubject,
                    RequestDate = txtDate.Text,
                    RequestStartTime = txtTime.SelectedTime.Value.Ticks,
                    OnlineTeacherRole = ckbAprroveOnline.Checked ? drpRoleTeacher.SelectedValue : string.Empty,
                    OnlineFirstTeacherName = ckbAprroveOnline.Checked && rcbOnlineTeacher.Items[0].Checked ? rcbOnlineTeacher.Items[0].Text : string.Empty,
                    OnlineFirstTeacherId = ckbAprroveOnline.Checked && rcbOnlineTeacher.Items[0].Checked ? rcbOnlineTeacher.Items[0].Value.Substring(3) : string.Empty,

                    //sadeghsaryazdi

                    FlagDoingMeetingOnline = chkDoingOnlineDefence.Checked ? true : false,

                    // = ckbAprroveOnline.Checked ? drpRoleTeacher.SelectedItem.Text : string.Empty,
                    UseOwnPc = rdbOwnSystem.Checked,
                    UserId = Session[sessionNames.userID_StudentOstad].ToString(),
                    Gender = defInfo.studentGender
                };
                request.AcceptPropDate = defInfo.GroupAcceptDate;
            }
            if (rcbOnlineTeacher.Items.Count > 1)
            {
                request.OnlineSecondTeacherName = ckbAprroveOnline.Checked && rcbOnlineTeacher.Items[1].Checked
                    ? rcbOnlineTeacher.Items[1].Text
                    : string.Empty;
                request.OnlineSecondTeacherId = ckbAprroveOnline.Checked && rcbOnlineTeacher.Items[1].Checked
                    ? rcbOnlineTeacher.Items[1].Value.Substring(3)
                    : string.Empty;
            }
            request.RequestEndTime = request.RequestStartTime + _requestHandler.GetDefenceInMeetingLength(Convert.ToInt32(defInfo.CollegeId));
         
            var reqId = 0;
            request.IsRequestEducation = false;
            var message = _requestHandler.CreateStudentRequestV2(out reqId, request);

            if (message.Contains("ok"))
            {


                defInfo.RequestDate = request.RequestDate;
                defInfo.StartTime = request.RequestStartTime;
                defInfo.EndTime = request.RequestEndTime;

                string scrp = message.Substring(2);
                //PrevPage = ViewState["PrevPage"].ToString();
                //var uri = Request.UrlReferrer.ToString();
                // _prevPage = uri.Replace("StudentAddRequest.aspx", "StudentReview.aspx");
                //var requestRawUrl = Request.RawUrl;
                CommonBusiness CommonBusiness = new CommonBusiness();
                CommonBusiness.InsertIntoStudentLog(Session[sessionNames.userID_StudentOstad].ToString(), DateTime.Now.ToString("HH:mm"), 11, 40, "ثبت درخواست رزرو جلسه دفاع توسط دانشجو", reqId);
                
                string address = "../Forms/StudentReview.aspx";
                string resdirectFunc = "function redirectToLast(){ window.location= '" + address + "' ; }";
                RadWindowManager1.RadAlert(scrp, 500, 100, "پیام سیستم", resdirectFunc);
                //DateTime dateTimeEnd = RequestHandler.WorkingDays24h(DateTime.Now);
                DateTime dateTimeEnd = RequestHandler.WorkingDays12h(DateTime.Now);
                SendSmsContactBuisnes.SendSmsOsForOstadsDefence(userId, request.IssuerName,
                                    request.RequestDate, dateTimeEnd.Date.ToPeString(), txtTime.SelectedTime.Value.ToString().Substring(0,5), dateTimeEnd.Hour+":"+dateTimeEnd.Minute);

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
            var userId = Session[sessionNames.userID_StudentOstad].ToString();
            var defenceInformation = _requestHandler.GetDefenceInformation(userId);

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


            Response.Redirect("~/CommonUI/IntroPage.aspx");
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
            string scrp = "function f(){$find(\"" + rwmMobile.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
        }

        protected void btnRegisterMobile_OnClick(object sender, EventArgs e)
        {
            var userId = Session[sessionNames.userID_StudentOstad].ToString();
            var defenceInformation = _requestHandler.GetDefenceInformation(userId);

            double num;
            if (double.TryParse(txtMbileChanged.Text, out num))
            {
                _requestHandler.SetStudentMobile(defenceInformation.StudentCode, txtMbileChanged.Text);
                var cb = new CommonBusiness();
                cb.InsertIntoStudentLog(defenceInformation.StudentCode, DateTime.Now.ToString("HH:mm"), 11, 43, "ویرایش موبایل توسط خود دانشجو در رزرواسیون");


                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }


        protected void chbAccept_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chbAccept.Checked && chkCommitment.Checked)
                btnAccept.Enabled = true;
            else
                btnAccept.Enabled = false;
        }

        protected void btnClose_OnClick(object sender, EventArgs e)
        {
            Session["CountApprove"] = 2;
            Response.Redirect("~/CommonUI/IntroPage.aspx");
        }

        protected void btnAccept_OnClick(object sender, EventArgs e)
        {
            Session["CountApprove"] = 2;

            string scrp = "function f(){$('#myModal').modal('hide');}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
        }
        public bool CheckRequest()
        {
            ////domain
            //adobeConnectDTO.DomainAddress = "stc.iauec.ac.ir";
            adobeConnectDTO.DomainAddress = "vadafavc.iauec.ac.ir";
            ////admin
            //adobeConnectDTO.DomainLogin = "m_saryazdi";
            //adobeConnectDTO.DomainPassword = "M3312@M_";
            adobeConnectDTO.DomainLogin = "vcadmin";
            adobeConnectDTO.DomainLogin = "VCadmin@1399@"; 

            //curuser
            var userLogin = Session[sessionNames.userID_StudentOstad].ToString();
            //var UserPassWord = "123456";
            adobeConnectDTO.SetValueDefult(userLogin);
            AdobeDefenceBusiness adobeDefenceBusiness = new AdobeDefenceBusiness();

            if (!adobeDefenceBusiness.CheckAcceptRequestDefence(adobeConnectDTO))
            {
                RadWindowManager1.RadAlert("دانشجو گرامی برای درخواست دفاع جدید باید حداقل سه دفاع از دانشجویان را به صورت کامل مشاهده نمایید", 500, 100, "خطا", "");
                return false;
            }
            return true;
        }

        protected void chkCommitment_CheckedChanged(object sender, EventArgs e)
        {
            if (chbAccept.Checked && chkCommitment.Checked)
                btnAccept.Enabled = true;
            else
                btnAccept.Enabled = false;

        }



    }
        }

