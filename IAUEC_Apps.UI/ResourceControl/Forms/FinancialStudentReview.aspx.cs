
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.ResourceControlClasses;
using ResourceControl.BLL;
using System;
using System.Collections.Generic;
using Telerik.Web.UI;
using System.Linq;
using ResourceControl.PL.Forms;
using IAUEC_Apps.UI.University.GraduateAffair.CMS;
using System.Web.UI.WebControls;
using System.Web.UI;
using ResourceControl.Entity;

namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class FinancialStudentReview : System.Web.UI.Page
    {
        private List<StudentDefenceRequestDTO> _reqlist = null;
        private RequestHandler _requestHandler = new RequestHandler();
        CommonBusiness CB = new CommonBusiness();
        protected void Page_Load(object sender, EventArgs e)
        {

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
                t.Text = "بررسی درخواست دفاع توسط مالی";
                ViewState["stateForStudent"] = 1;


            }


        }

        protected void grdDefenceList_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            int defStatus;

            defStatus = Convert.ToInt32(ViewState["stateForStudent"]);

            switch (defStatus)
            {

                case 1://منتظر تایید
                    _reqlist = _requestHandler.GetStudentRequestListForEducation(Convert.ToInt32(Session["DaneshId"]))?
                       .Where(x => (x.isDeleted != true && (x.status == (int)ResourceControlEnums.RequestDefenceStaus.educationApprove)
                      // && (((x.RequestDate.StringPersianDateToGerogorianDate().Date >= RequestHandler.WorkingDays48h(DateTime.Now.Date).Date) && !(x.IsRequestEducation)
                    // )
                       && ((x.RequestDate.StringPersianDateToGerogorianDate().Date >= DateTime.Now.Date)
                       //&& (x.IsRequestEducation
                      // ||x.RequestDate.StringPersianDateToGerogorianDate().Date == new DateTime(2020, 11, 07).Date || x.RequestDate.StringPersianDateToGerogorianDate().Date == new DateTime(2020, 11, 08).Date)))
                       ))).ToList();
                            break;
                case 2://تایید شده
                    _reqlist = _requestHandler.GetStudentRequestListForEducation(Convert.ToInt32(Session["DaneshId"]))?
                        .Where(x => x.isDeleted != true && x.status != (int)ResourceControlEnums.RequestDefenceStaus.educationApprove
                                && ((x.status == (int)ResourceControlEnums.RequestDefenceStaus.FinancialApprove)
                                ||  (x.status == (int)ResourceControlEnums.RequestDefenceStaus.approved))
                                || (x.status == (int)ResourceControlEnums.RequestDefenceStaus.technicalApprove)).ToList();
                    break;

                case 3://رد شده
                    _reqlist = _requestHandler.GetStudentRequestListForEducation(Convert.ToInt32(Session["DaneshId"]))?
                        .Where(x => x.IsRejectFinancial == true&&x.status!= (int)ResourceControlEnums.RequestDefenceStaus.educationApprove).ToList();
                    break;
                case 4://ازدست رفته
                    _reqlist = _requestHandler.GetStudentRequestListForEducation(Convert.ToInt32(Session["DaneshId"]))?
                    .Where(x =>  x.status == (int)ResourceControlEnums.RequestDefenceStaus.educationApprove
                    //   && ((((x.RequestDate.StringPersianDateToGerogorianDate().Date < RequestHandler.WorkingDays48h(DateTime.Now.Date).Date) && !(x.IsRequestEducation))
                     //  )
                       && ((x.RequestDate.StringPersianDateToGerogorianDate().Date < DateTime.Now.Date) 
                       //&& (x.IsRequestEducation ||  x.RequestDate.StringPersianDateToGerogorianDate().Date == new DateTime(2020, 11, 07).Date || x.RequestDate.StringPersianDateToGerogorianDate().Date == new DateTime(2020, 11, 08).Date))
                       )).ToList();
                    break;
                case 5://کلیه درخواست ها
                    _reqlist = _requestHandler.GetStudentRequestListForEducation(Convert.ToInt32(Session["DaneshId"]))
                    .Where(x =>
                    (x.status == 1 && x.isDeleted != true) ||
                    (x.status == 9 && x.isDeleted != true) ||
                    (x.status == 6 && x.isDeleted != true) ||
                    (x.status == 2 && x.isDeleted != true) ||
                    (x.IsRejectFinancial == true)
                   )
                    .ToList();
                    break;

            }



            grdDefenceList.DataSource = _reqlist;
            GridFilterMenu menu = grdDefenceList.FilterMenu;
            if (menu.Items.Count > 3)
            {
                int im = 0;
                while (im < menu.Items.Count)
                {
                    if (menu.Items[im].Text == "NoFilter" || menu.Items[im].Text == "Contains" || menu.Items[im].Text == "EqualTo")
                    {
                        im++;
                    }
                    else
                    {
                        menu.Items.RemoveAt(im);
                    }
                }
                foreach (RadMenuItem item in menu.Items)
                {    //change the text for the "StartsWith" menu item  
                    if (item.Text == "NoFilter")
                    {
                        item.Text = "حذف فیلتر";
                        //item.Remove();
                    }
                    if (item.Text == "Contains")
                    {
                        item.Text = "شامل";
                        //item.Remove();
                    }
                    if (item.Text == "EqualTo")
                    {
                        item.Text = "مساوی با";
                        //item.Remove();
                    }

                }
            }

        }

        protected void grdDefenceList_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            int status;

                status = Convert.ToInt32(ViewState["stateForStudent"]);

            var eItem = e.Item as GridDataItem;
            if (eItem == null) return;
            GridDataItem item = eItem;
            Button divApprove = (Button)item["operator"].FindControl("btnApprove");
            Button divbtnSendSms = (Button)item["operator"].FindControl("btnSendSms");
            Button divAvoid = (Button)item["operator"].FindControl("btnAvoid");
            Button btnShowDetail = (Button)item["operator"].FindControl("btnShowDetail");
            var hiddenStatus = Convert.ToInt32(((HiddenField)item["operator"].FindControl("HiddenStatusValue")).Value);
            switch (status)
            {
                case 1:
                    divAvoid.Enabled = true;
                    divAvoid.Visible = true;
                    divApprove.Enabled = true;
                    btnShowDetail.Enabled = true;
                    divbtnSendSms.Visible = true;
                    break;
                case 2:
                    divAvoid.Visible = false;
                    divbtnSendSms.Visible = false;
                    divApprove.Visible = false;
                    btnShowDetail.Enabled = true;
                    btnShowDetail.Visible=true;
                    divAvoid.Visible = false ;
                    break;
                case 3:
                    divAvoid.Visible = false;
                    divbtnSendSms.Visible = false;
                    divApprove.Visible = false;
                    btnShowDetail.Enabled = true;
                    btnShowDetail.Visible = true;
                    divAvoid.Visible = false;
                    break;
                case 4:
                    divAvoid.Visible = false;
                    divbtnSendSms.Visible = false;
                    divApprove.Visible = false;
                    btnShowDetail.Enabled = true;
                    btnShowDetail.Visible = true;
                    divAvoid.Visible = false;
                    break;
                case 5:
                    divAvoid.Visible = false;
                    divbtnSendSms.Visible = false ;
                    divApprove.Visible = false;
                    btnShowDetail.Enabled = true;
                    divAvoid.Visible = false;
                    btnShowDetail.Visible = true;
                    break;

            }
            

        }

        protected void grdDefenceList_OnItemCommand(object sender, GridCommandEventArgs e)
        {


            if (e.CommandName == "Confirm")
            {
                var userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
                var splitedArgument = e.CommandArgument.ToString().Split('-');
                Session["PopupInfo"] = e.CommandArgument;


                string scrp2 = "function f(){$find(\"" + rdwConfirmPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp2, true);
            }
            if(e.CommandName=="SendSms")
            {
                txtSendSms.Text = "";
                var userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
                var splitedArgument = e.CommandArgument.ToString().Split('-');
                Session["PopupInfo"] = e.CommandArgument;
                var dataSplited = Session["PopupInfo"].ToString().Split('-');
                var reqID = dataSplited[0];
                var stcode = dataSplited[1];
                var reqDate = dataSplited[2];
                var SendSms = dataSplited[4];
                var IsSendSms = bool.Parse(dataSplited[3]);
                if (IsSendSms)
                {
                    txtSendSms.Text = SendSms;
                    txtSendSms.Enabled = false;
                    btnCancleSendSms.Text = "بستن";
                    btnApproveSendSms.Visible = false;
                }
                else
                {
                    txtSendSms.Text = "";
                    txtSendSms.Enabled = true;
                    btnCancleSendSms.Text = "لغو";
                    btnApproveSendSms.Visible = true;

                }
                    string scrp2 = "function f(){$find(\"" + rdwSendSms.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp2, true);
            }

        
        }



        protected void btnApprovedInConfirmPopup_Click(object sender, EventArgs e)
        {
            var dataSplited = Session["PopupInfo"].ToString().Split('-');
            var reqID = dataSplited[0];
            var stcode = dataSplited[1];
            var reqDate = dataSplited[2];

          
                _requestHandler.UpdateStatusDefRequest((int)ResourceControlEnums.RequestDefenceStaus.FinancialApprove, Convert.ToInt32(reqID));


                var userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
                var comman = new CommonBusiness();
                comman.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 253, string.Format("{0}", "تایید درخواست جلسه دفاع توسط مالی"), Convert.ToInt32(reqID));


                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, "closeRadWindowConfirmPopup();", true);
                string approveMessage = "درخواست شماره " + reqID.ToString() + " با موفقیت تایید گردید.";
                RadWindowManager1.RadAlert(approveMessage, 400, 100, "پیام سیستم", "closeRadWindow2");
            
       
            Session["PopupInfo"] = null;
        }

        protected void btnCancleInConfirmPopup_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, "closeRadWindowConfirmPopup();", true);
        }




        protected void btnDenyRequest_OnClick(object sender, EventArgs e)
        {
            lblalertMessage.Visible = false;
            Button btn = (Button)sender;
            GridDataItem data = (GridDataItem)btn.NamingContainer;
            string id = data["ID"].Text;
            string stcode = data["StudentCode"].Text;
            hdnfDenyReqId1.Value = id;
            hdnfDenyStcode.Value = stcode;
            string scrp = "function f(){$find(\"" + RadWindow21.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);

        }


        protected void btnHistory_OnClick(object sender, ImageClickEventArgs e)
        {
            RequestHandler _reqHandler = new RequestHandler();
            CommonBusiness cmb = new CommonBusiness();
            ImageButton btn = (ImageButton)sender;
            GridDataItem data = (GridDataItem)btn.NamingContainer;
            string id = data["ID"].Text;

            var dtLog = cmb.GetUserAndStudentLogModifyId(int.Parse(id), 11);//.AsEnumerable();
            var myLog = RequestHandler.ConvertDataTableToList<logDetail>(dtLog)
                .OrderBy(o => o.LogDate.ToGregorian())
                .ThenBy(x => x.LogTime.TimeToTicks());

            //var Rows = (from row in dtLog
            //            orderby row["LogDate"].ToString().ToGregorian() 
            //            select row);

            lst_history.DataSource = myLog;// Rows;
            lst_history.DataBind();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
        }

        protected void drpRequestTypeList_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid(Convert.ToInt32(drpRequestTypeList.SelectedValue));
        }

        private void BindGrid(int selectedTypeRequest)
        {

            ViewState["stateForStudent"] = selectedTypeRequest;
            grdDefenceList.Rebind();
        }

        protected void btnRefreshGrid_OnClick(object sender, EventArgs e)
        {
            BindGrid(Convert.ToInt32(drpRequestTypeList.SelectedValue));

        }

        protected void btnShowDetail_OnClick(object sender, EventArgs e)
        {
            #region ShowInfo

            Button btn = (Button)sender;
            GridDataItem data = (GridDataItem)btn.NamingContainer;
            var id = Convert.ToInt32(data["ID"].Text);
            var requestDetails = _requestHandler.GetRequestDetails(id);
            lblRequestId.Text = requestDetails.ID.ToString();
            lblDarkhast.Text = requestDetails.CourseName;
            lbldateOfRequest.Text = requestDetails.Issue_time;
            RequestDateTimeHandler rqdateTimeH = new RequestDateTimeHandler();
            var _dateTimeList = rqdateTimeH.GetDateTimeListByRequestId(id);
            var dateTime = _dateTimeList.OrderBy(c => c.Date).FirstOrDefault(c => c.Date != null);
            if (dateTime != null)
                lblRequest.Text = dateTime.Date;





            var startTime = _dateTimeList.FirstOrDefault(c => c.StartTime != 0);
            if (startTime != null)
                lblTime1.Text = TimeSpan.FromTicks((long)startTime.StartTime).ToString().Substring(0, 5);

            var endTime = _dateTimeList.FirstOrDefault(c => c.EndTime != 0);
            if (endTime != null)
                lblTime2.Text = TimeSpan.FromTicks((long)endTime.EndTime).ToString().Substring(0, 5);

            switch (requestDetails.Status)
            {
                case 0:
                    lblStatue.Text = "درخواست توسط دانشجو ثبت گردیده";
                    break;
                case 1:
                    lblStatue.Text = "درخواست توسط آموزش تایید گردید";
                    break;
                case 2:
                    lblStatue.Text = "درخواست توسط اداری تایید گردید";
                    break;
                case 3:
                    lblStatue.Text = "درخواست رد شد و برای بررسی به کارتابل دانشکده ارجاع داده شد";
                    break;
                case 6:
                    //  lblStatue.Text = "درخواست توسط فنی جهت پخش آنلاین تایید گردید";
                    lblStatue.Text = "درخواست توسط فنی جهت برگزاری آنلاین تایید گردید";
                    break;



            }
            //lblTozieh.Text = requestDetails.Note;

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
            var studentDefenceRequestList = _requestHandler.GetStudentDefenceRequest(requestDetails.IssuerID);

            var listOfDefenceRequest =
                RequestHandler.ConvertDataTableToList<StudentDefenceRequestDTO>(studentDefenceRequestList);

            var inCirculationRequest =
                listOfDefenceRequest.FirstOrDefault(
                x => x.isDeleted != true && x.RequestDate.StringPersianDateToGerogorianDate() >= DateTime.Now);

            if (inCirculationRequest == null)
                inCirculationRequest = listOfDefenceRequest.OrderByDescending(x => x.ID).FirstOrDefault();



            if (inCirculationRequest.IsUseOwnSystem)
            {
                lblOwnSysytem.Text = "بله";
            }
            else
            {
                lblOwnSysytem.Text = "خیر";
            }

            if (inCirculationRequest.IsEquippingResource)
            {
                lblOnlineShow.Text = "بله";
            }
            else
            {
                lblOnlineShow.Text = "خیر";
            }

            if (!string.IsNullOrEmpty(inCirculationRequest.OnlineTeacherRole))
            {
                lblOnlineDef.Text = "بله";
            }
            else
            {
                lblOnlineDef.Text = "خیر";
            }
            if (inCirculationRequest.FlagDoingMeetingOnline)
            {
                lblOnlineDoingMeeting.Text = "بله";

            }
            else
            {
                lblOnlineDoingMeeting.Text = "خیر";
            }
            var defenceInformation = _requestHandler.GetDefenceInformation(inCirculationRequest.StudentCode);
            string firstOnlineTeachName = string.Empty;
            string firstOnlineTeachId = string.Empty;
            string firstOnlineTeachMobile = string.Empty;
            string firstOnlineTeachEmail = string.Empty;
            string secondOnlineTeachName = string.Empty;
            string secondOnlineTeachId = string.Empty;
            string secondOnlineTeachMobile = string.Empty;
            string secondOnlineTeachEmail = string.Empty;

            //اگر استاد آنلاین داره
            if (!string.IsNullOrEmpty(inCirculationRequest.OnlineTeacherRole))
            {

                // اگر نقش استاد مشاور هست
                if (inCirculationRequest.OnlineTeacherRole.ToLower() == "consultant")
                {
                    //اگر استاد مشاور اول آنلاین بوده
                    if (!string.IsNullOrEmpty(inCirculationRequest.OnlineFirstTeacherId.ToString()) && ("200" + inCirculationRequest.OnlineFirstTeacherId.ToString()) == defenceInformation.FirstConsultantId)
                    {
                        // فقط اولی
                        firstOnlineTeachName = defenceInformation.FirstConsultantFullName;
                        firstOnlineTeachId = defenceInformation.FirstConsultantId;
                        firstOnlineTeachMobile = defenceInformation.FirstConsultantMobile;
                        firstOnlineTeachEmail = defenceInformation.FirstConsultantMail;
                        //اولی و دومی
                        //اگر استاد مشاور دوم آنلاین بوده
                        if (!string.IsNullOrEmpty(inCirculationRequest.OnlineSecondTeacherId.ToString()) && ("200" + inCirculationRequest.OnlineSecondTeacherId.ToString()) == defenceInformation.SecondConsultantId)
                        {
                            secondOnlineTeachName = defenceInformation.SecondConsultantFullName;
                            secondOnlineTeachId = defenceInformation.SecondConsultantId;
                            secondOnlineTeachMobile = defenceInformation.SecondConsultantMobile;
                            secondOnlineTeachEmail = defenceInformation.SecondConsultantMail;
                        }
                    }
                    //فقط دومی
                    //اگر استاد مشاور دوم آنلاین بوده                 
                    else
                    {
                        firstOnlineTeachName = defenceInformation.SecondConsultantFullName;
                        firstOnlineTeachId = defenceInformation.SecondConsultantId;
                        firstOnlineTeachMobile = defenceInformation.SecondConsultantMobile;
                        firstOnlineTeachEmail = defenceInformation.SecondConsultantMail;
                    }
                }
                //ااگر نقش استاد راهنما هست
                else
                {
                    //اگر استاد راهنما اول آنلاین بوده
                    if (!string.IsNullOrEmpty(inCirculationRequest.OnlineFirstTeacherId.ToString()) && ("200" + inCirculationRequest.OnlineFirstTeacherId.ToString()) == defenceInformation.FirstGuideId)
                    {
                        firstOnlineTeachName = defenceInformation.FirstGuideFullName;
                        firstOnlineTeachId = defenceInformation.FirstGuideId;
                        firstOnlineTeachMobile = defenceInformation.FirstGuideMobile;
                        firstOnlineTeachEmail = defenceInformation.FirstGuideMail;

                        //اگر استاد راهنما دوم آنلاین بوده
                        if (!string.IsNullOrEmpty(inCirculationRequest.OnlineFirstTeacherId.ToString()) && ("200" + inCirculationRequest.OnlineFirstTeacherId.ToString()) == defenceInformation.SecondGuideId)
                        {
                            secondOnlineTeachName = defenceInformation.SecondGuideFullName;
                            secondOnlineTeachId = defenceInformation.SecondGuideId;
                            secondOnlineTeachMobile = defenceInformation.SecondGuideMobile;
                            secondOnlineTeachEmail = defenceInformation.SecondGuideMail;
                        }
                    }
                    else
                    {
                        firstOnlineTeachName = defenceInformation.SecondGuideFullName;
                        firstOnlineTeachId = defenceInformation.SecondGuideId;
                        firstOnlineTeachMobile = defenceInformation.SecondGuideMobile;
                        firstOnlineTeachEmail = defenceInformation.SecondGuideMail;
                    }
                }



            }

            if (!string.IsNullOrEmpty(firstOnlineTeachName))
            {
                divFirstOnlineTeacher.Visible = true;
                lblfirstTeacherEmail.Text = firstOnlineTeachEmail;
                lblfirstTeacherMobile.Text = firstOnlineTeachMobile;
                lblfirstTeacherName.Text = firstOnlineTeachName;
            }
            else
            {
                divFirstOnlineTeacher.Visible = false;
                lblfirstTeacherEmail.Text = string.Empty;
                lblfirstTeacherMobile.Text = string.Empty;
                lblfirstTeacherName.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(secondOnlineTeachName))
            {
                divSecondOnlineTeacher.Visible = true;
                lblSecondTeacherEmail.Text = secondOnlineTeachEmail;
                lblSecondTeacherMobile.Text = secondOnlineTeachMobile;
                lblSecondTeacherName.Text = secondOnlineTeachName;
            }
            else
            {
                divSecondOnlineTeacher.Visible = false;
                lblSecondTeacherEmail.Text = string.Empty;
                lblSecondTeacherMobile.Text = string.Empty;
                lblSecondTeacherName.Text = string.Empty;
            }


            if (requestDetails.Status == 2)
            {

                tdGrdResult.Visible = true;

                _dateTimeList = rqdateTimeH.GetDateTimeListByRequestId(id);
                grdResult.DataSource = _dateTimeList.OrderBy(c => c.Date);
                grdResult.DataBind();
            }
            else
            {
                tdGrdResult.Visible = true;

                _dateTimeList = rqdateTimeH.GetDateTimeListByRequestId(id);
                grdResult.DataSource = _dateTimeList.OrderBy(c => c.Date);
                grdResult.DataBind();
            }

            GrdInfoOstad.DataSource = _requestHandler.GetSignutreOstad(defenceInformation);
            GrdInfoOstad.DataBind();
            string scrp = "function f(){$find(\"" + RadWindow3.ClientID +
                          "\").show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);


            #endregion

        }





        protected void btnDenyRequest1_OnClick(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtDenyMessage1.Text.Trim()))
            {
                lblalertMessage.Visible = true;
                return;
            }

            var userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);

            RequestFR req = new RequestFR();

            userID = Convert.ToInt32(Session["UserId"]);
            req.ID = Convert.ToInt32(hdnfDenyReqId1.Value);


            req.ReplierID = userID;//education user ID
            req.Answernote = txtDenyMessage1.Text;
            req.Answer_time = DateTime.Now.ToPeString();

            req.IsRejectFinancial = true;

            req.Status = (int)ResourceControlEnums.RequestDefenceStaus.denied;//3 means request has been denied .

            RequestHandler requestBusiness = new RequestHandler();
            _requestHandler.UpdateIsRejectFinancial(req.ID, true);
            int id = requestBusiness.DenyRequest(req);
            var comman = new CommonBusiness();
            if (_requestHandler.DeleteStudentRequest(req.ID))
            {
                
                comman.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 254, string.Format("{0}", "لغو درخواست جلسه دفاع توسط مالی"), req.ID);
                txtDenyMessage1.Text = string.Empty;
                string smsText = "  دفاع شما با شماره درخواست" + req.ID +"توسط مالی لغو گردید ";
                var acceptSms = _requestHandler.SendSMSForFinacial(hdnfDenyStcode.Value, smsText);


                string denyMessage = "درخواست شماره " + req.ID.ToString() + " لغو گردید.";
                RadWindowManager1.RadAlert(denyMessage, 300, 100, "پیام سیستم", "closeRadWindow3");

            }
            else
            {
                txtDenyMessage1.Text = string.Empty;


                txtDenyMessage1.Text = string.Empty;
                string denyMessage = "لغو درخواست با  شماره " + req.ID.ToString() + " با مشکل روبه رو گردید.";
                RadWindowManager1.RadAlert(denyMessage, 300, 100, "پیام سیستم", "closeRadWindow3");
            }


        }

        protected void bt1ExportExcle_OnClick(object sender, ImageClickEventArgs e)
        {
            var daneshId = Convert.ToInt32(Session["DaneshId"]);
            var allAcceptedRequest = _requestHandler.GetAllAccepetedDefenceRequests(daneshId);
            if (allAcceptedRequest.Rows.Count > 0)
            {
                try
                {

                    var pck = new OfficeOpenXml.ExcelPackage();
                    var ws = pck.Workbook.Worksheets.Add("ProfInfoList");

                    ws.Cells["A1"].LoadFromDataTable(allAcceptedRequest, true);
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;  filename=DefenceList.xlsx");

                    Response.BinaryWrite(pck.GetAsByteArray());

                }
                catch (Exception ex)
                {
                    //log error
                }
                Response.End();

            }
        }

        protected void btnSendSms_Click(object sender, EventArgs e)
        {
            lblalertMessage.Visible = false;
            lblSendSmsValidate.Visible = false;
            Button btn = (Button)sender;
            GridDataItem data = (GridDataItem)btn.NamingContainer;
            string id = data["ID"].Text;
            hdnfDenyReqId1.Value = id;

            string scrp = "function f(){$find(\"" + rdwSendSms.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
        }

        protected void btnApproveSendSms_Click(object sender, EventArgs e)
        {
            if(txtSendSms.Text.Trim()=="")
            {
                lblSendSmsValidate.Visible = true;
                return;
            }
      
            var dataSplited = Session["PopupInfo"].ToString().Split('-');
            var reqID = dataSplited[0];
            var stcode = dataSplited[1];
            var reqDate = dataSplited[2];

          


            var userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
            var comman = new CommonBusiness();
            comman.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 255, string.Format("{0}", "ارسال پیامک بدهی شهریه توسط مالی"), Convert.ToInt32(reqID));
            string approveMessage = "";
            var acceptSms= _requestHandler.SendSMSForFinacial(stcode.ToString(),txtSendSms.Text);
            if (acceptSms)
            {
                _requestHandler.UpdateSendSmsFlagFinancial(int.Parse(reqID), acceptSms, txtSendSms.Text);
                 ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, "closeRadWindowConfirmSendSmspopup();", true);
                 approveMessage = "درخواست شماره " + reqID.ToString() + " با موفقیت پیامک بدهی ارسال گردید.";
            }
            else
            {
                approveMessage = "درخواست شماره " + reqID.ToString() + "پیامک ارسالی دچار اختلال گردید";
            }
            RadWindowManager1.RadAlert(approveMessage, 400, 100, "پیام سیستم", "closeRadWindow2");
            Session["PopupInfo"] = null;
        }

        protected void btnCancleSendSms_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, "closeRadWindowConfirmSendSmspopup();", true);
        }
    }
}


