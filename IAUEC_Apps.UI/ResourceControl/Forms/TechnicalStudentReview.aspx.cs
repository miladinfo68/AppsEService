using ResourceControl.BLL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Wordprocessing;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.ResourceControlClasses;
using IAUEC_Apps.UI.University.GraduateAffair.CMS;
using Telerik.Web.UI;
using Control = System.Web.UI.Control;
using ListItem = System.Web.UI.WebControls.ListItem;
using IAUEC_Apps.Business.Conatct;
using IAUEC_Apps.DTO.CommonClasses;
using PersianDateControls;
using System.Globalization;

namespace ResourceControl.PL.Forms
{
    public partial class TechnicalStudentReview : System.Web.UI.Page
    {

        private List<StudentDefenceRequestDTO> _reqlist = null;
        private RequestHandler _requestHandler = new RequestHandler();
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
                _reqlist = _requestHandler.GetStudentRequestListForTechnical()?
                    .Where(x => x.status == (int)ResourceControlEnums.RequestDefenceStaus.educationApprove && x.RequestDate.StringPersianDateToGerogorianDate() >= DateTime.Now && x.isDeleted != true)
                    .Where(y => y.IsEquippingResource == true || !string.IsNullOrEmpty(y.OnlineTeacherRole))
                   .OrderByDescending(x => x.RequestDate.StringPersianDateToGerogorianDate())
                    .ToList();
                //_reqlist = _requestHandler.GetStudentRequestListForTechnical()?.OrderByDescending(x => x.RequestDate.StringPersianDateToGerogorianDate()).ToList();




                if (Session["StausLinke"] == null || Session["StausLinke"].ToString() == 1.ToString())
                {
                    ViewState["stateForStudent"] = 1; //منتظز تایید
                }
                if (Session["StausLinke"] != null && Session["StausLinke"].ToString() != 1.ToString())
                {
                    ViewState["stateForStudent"] = Convert.ToInt32(Session["StausLinke"]);
                    drpRequestTypeList.SelectedValue = Session["StausLinke"].ToString();
                }

            }


        }


        protected void grdDefenceList_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

            switch (Convert.ToInt32(ViewState["stateForStudent"]))
            {
                case 1:
                    _reqlist = _requestHandler.GetStudentRequestListForTechnical()?.Where(
                        x => x.status == (int)ResourceControlEnums.RequestDefenceStaus.FinancialApprove
                   && x.RequestDate.StringPersianDateToGerogorianDate().Date >= DateTime.Now.Date
                   && x.isDeleted != true
                   // && (x.IsEquippingResource == true || !string.IsNullOrEmpty(x.OnlineTeacherRole))
                   ).ToList();
                    break;
                case 2:
                    _reqlist = _requestHandler.GetStudentRequestListForTechnical()?.Where(
                        x => (x.status == (int)ResourceControlEnums.RequestDefenceStaus.technicalApprove
                    || x.status == (int)ResourceControlEnums.RequestDefenceStaus.approved)
                    // && x.RequestDate.StringPersianDateToGerogorianDate() >= DateTime.Now
                    && x.isDeleted != true
                    && (x.IsEquippingResource == true || !string.IsNullOrEmpty(x.OnlineTeacherRole))).ToList();
                    break;
                case 3:
                    _reqlist = _requestHandler.GetStudentRequestListForTechnical()?.Where(
                        x => x.status == (int)ResourceControlEnums.RequestDefenceStaus.denied
                    && x.RequestDate.StringPersianDateToGerogorianDate().Date >= DateTime.Now.Date
                    && x.isDeleted != true && (x.IsEquippingResource == true || !string.IsNullOrEmpty(x.OnlineTeacherRole))).ToList();
                    break;
                case 4:
                    _reqlist = _requestHandler.GetStudentRequestListForTechnical()?.Where(
                    x => x.RequestDate.StringPersianDateToGerogorianDate().Date < DateTime.Now.Date
                    && x.status == (int)ResourceControlEnums.RequestDefenceStaus.educationApprove
                    && x.isDeleted != true
                    && (x.IsEquippingResource == true || !string.IsNullOrEmpty(x.OnlineTeacherRole))).ToList();
                    break;
                case 5:
                    _reqlist = _requestHandler.GetStudentRequestListForTechnical()?.Where(
                        x =>
                        (x.IsEquippingResource == true || !string.IsNullOrEmpty(x.OnlineTeacherRole)
                        && x.isDeleted != true)).ToList();
                    break;
                case 6:
                    _reqlist = _requestHandler.GetStudentRequestListForTechnical()?.Where(x =>
                    (x.status == (int)ResourceControlEnums.RequestDefenceStaus.approved)
                    //&& x.RequestDate.StringPersianDateToGerogorianDate() >= DateTime.Now
                    && x.isDeleted != true
                    && (x.IsEquippingResource == true || !string.IsNullOrEmpty(x.OnlineTeacherRole))).ToList();
                    break;
                case 7:
                    _reqlist = _requestHandler.GetStudentRequestListForTechnical()?.Where(x =>
                    (x.isDeleted == true && (x.IsEquippingResource == true || !string.IsNullOrEmpty(x.OnlineTeacherRole)))).ToList();
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
            var status = Convert.ToInt32(ViewState["stateForStudent"]);
            var eItem = e.Item as GridDataItem;
            if (eItem == null) return;
            GridDataItem item = eItem;
            HtmlGenericControl divApprove = (HtmlGenericControl)item["operator"].FindControl("divApprove");
            HtmlGenericControl divAvoid = (HtmlGenericControl)item["operator"].FindControl("divAvoid");
            HtmlGenericControl divAccept = (HtmlGenericControl)item["operator"].FindControl("divAccept");
            Button btnShowDetail = (Button)item["operator"].FindControl("btnShowDetail");

            switch (status)
            {
                case 1:
                    divApprove.Visible = true;
                    divAvoid.Visible = true;
                    btnShowDetail.Visible = true;
                    divAccept.Visible = true;
                    break;
                case 2:
                    divApprove.Visible = false;
                    divAvoid.Visible = false;
                    btnShowDetail.Visible = true;
                    divAccept.Visible = false;
                    break;
                case 3:
                    divApprove.Visible = false;
                    divAvoid.Visible = false;
                    btnShowDetail.Visible = true;
                    divAccept.Visible = false;
                    break;
                case 4:
                    divApprove.Visible = false;

                    divAvoid.Visible = false;
                    btnShowDetail.Visible = true;
                    divAccept.Visible = false;
                    break;
                case 5:
                    divApprove.Visible = false;
                    divAvoid.Visible = false;
                    btnShowDetail.Visible = true;
                    divAccept.Visible = false;
                   
                    break;
                case 6:
                    divApprove.Visible = false;
                    divAvoid.Visible = false;
                    btnShowDetail.Visible = true;
                    divAccept.Visible = false;
                    break;
                case 7:
                    divApprove.Visible = false;
                    divAvoid.Visible = false;
                    btnShowDetail.Visible = true;
                    divAccept.Visible = false;
                    break;
            }
        }

        protected void grdDefenceList_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            //throw new NotImplementedException();
        }


        protected void btnApprove_OnClick(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            GridDataItem data = (GridDataItem)btn.NamingContainer;
            string id = data["ID"].Text;
            var statusDefence = _requestHandler.GetStatusDefenceTechnical(int.Parse(id));
            if(statusDefence.FlagAcceptTechnicalDavIn==false|| statusDefence.FlagAcceptTechnicalDavOut==false)
            {
                lblAlert.Text = "استاد داور این دانشجو دفاع را رد کرده است";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal2", "$('#ModalAlert').modal();", true);
                upModalAlert.Update();
                return;
            }
            if (statusDefence.FlagAcceptTechnicalMosh1 == false || statusDefence.FlagAcceptTechnicalMosh2 == false)
            {
                lblAlert.Text = "استاد مشاور این دانشجو دفاع را رد کرده است";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal2", "$('#ModalAlert').modal();", true);
                upModalAlert.Update();
                return;
            }
            if (statusDefence.FlagAcceptTechnicalRah1 == false || statusDefence.FlagAcceptTechnicalRah2 == false)
            {
                lblAlert.Text = "استاد راهنما این دانشجو دفاع را رد کرده است";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal2", "$('#ModalAlert').modal();", true);
                upModalAlert.Update();
                return;
            }
            if (statusDefence.FlagAcceptTechnicalStudent == false)
            {
                lblAlert.Text = "دانشجو دفاع را رد کرده است";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal2", "$('#ModalAlert').modal();", true);
                upModalAlert.Update();
                return;
            }
            _requestHandler.UpdateStatusDefRequest((int)ResourceControlEnums.RequestDefenceStaus.technicalApprove, Convert.ToInt32(id));

            var userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
            var comman = new CommonBusiness();
            comman.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 162, string.Format("{0}", "تایید درخواست جلسه دفاع توسط فنی"), Convert.ToInt32(id));
            txtDenyMessage.Text = string.Empty;



            string approveMessage = "درخواست شماره " + id.ToString() + " با موفقیت تایید گردید.";
            RadWindowManager1.RadAlert(approveMessage, 400, 100, "پیام سیستم", "closeRadWindow2");
        }

        protected void btnAvoid_OnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridDataItem data = (GridDataItem)btn.NamingContainer;
            string id = data["ID"].Text;
            hdnfDenyReqId.Value = id;
            //sadeghsaryazdy
            hdStcode.Value= data["StudentCode"].Text;
            hdStName.Value = data["StudentFullName"].Text;

            string scrp = "function f(){$find(\"" + RadWindow2.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);

        }

        protected void btnDenyRequest_OnClick(object sender, EventArgs e)
        {
            var userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
            if (IsValid)
            {
                RequestFR req = new RequestFR();

                req.ID = Convert.ToInt32(hdnfDenyReqId.Value);
                req.ReplierID = userID;//education user ID
                req.Answernote = txtDenyMessage.Text;
                req.Answer_time = DateTime.Now.ToPeString();
                req.Status = (int)ResourceControlEnums.RequestDefenceStaus.denied;//3 means request has been denied .
                
                RequestHandler requestBusiness = new RequestHandler();
                int id = requestBusiness.DenyRequest(req);
                var comman = new CommonBusiness();
                if (_requestHandler.DeleteStudentRequest(req.ID))
                {
                    comman.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 251, string.Format("{0}", "رد درخواست جلسه دفاع توسط فنی ولغو مستقیم دفاع "), req.ID);
                }
                else
                    comman.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 161, string.Format("{0}", "رد درخواست جلسه دفاع توسط فنی"), req.ID);
               
                txtDenyMessage.Text = string.Empty;

                string denyMessage = "درخواست شماره " + id.ToString() + " رد شد.";
                RadWindowManager1.RadAlert(denyMessage, 300, 100, "پیام سیستم", "closeRadWindow2");
           
                //sadeghsaryazdi
                SendSmsContactBuisnes.SendSmsStudentForRejectTechnichal(hdStcode.Value);
                SendSmsContactBuisnes.SendSmsOstadForRejectTechnichal(hdStcode.Value,hdStName.Value);
            }


        }


        protected void btnHistory_OnClick(object sender, ImageClickEventArgs e)
        {
            CommonBusiness cmb = new CommonBusiness();
            ImageButton btn = (ImageButton)sender;
            GridDataItem data = (GridDataItem)btn.NamingContainer;
            string id = data["ID"].Text;
            var dtlog = cmb.GetUserAndStudentLogModifyId(int.Parse(id), 11);
            var myLog = RequestHandler.ConvertDataTableToList<logDetail>(dtlog).OrderBy(O => O.LogDate.ToGregorian()).ThenBy(x => x.LogTime.TimeToTicks());

            lst_history.DataSource = myLog;
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

            //switch (selectedTypeRequest)
            //{
            //    case 1:
            //        _reqlist = _requestHandler.GetStudentRequestListForTechnical().Where(x => x.status == (int)ResourceControlEnums.RequestDefenceStaus.educationApprove && x.RequestDate.StringPersianDateToGerogorianDate() >= DateTime.Now && x.RequestDate.StringPersianDateToGerogorianDate() >= DateTime.Now && x.isDeleted!=true  && (x.IsEquippingResource == true || !string.IsNullOrEmpty(x.OnlineTeacherRole))).ToList();
            //        break;
            //    case 2:
            //        _reqlist = _requestHandler.GetStudentRequestListForTechnical().Where(x => x.status == (int)ResourceControlEnums.RequestDefenceStaus.technicalApprove && x.RequestDate.StringPersianDateToGerogorianDate() >= DateTime.Now && x.RequestDate.StringPersianDateToGerogorianDate() >= DateTime.Now && x.isDeleted != true && (x.IsEquippingResource == true || !string.IsNullOrEmpty(x.OnlineTeacherRole))).ToList();
            //        break;
            //    case 3:
            //        _reqlist = _requestHandler.GetStudentRequestListForTechnical().Where(x => x.status == (int)ResourceControlEnums.RequestDefenceStaus.denied && x.RequestDate.StringPersianDateToGerogorianDate() >= DateTime.Now && x.RequestDate.StringPersianDateToGerogorianDate() >= DateTime.Now && x.isDeleted != true && (x.IsEquippingResource == true || !string.IsNullOrEmpty(x.OnlineTeacherRole))).ToList();
            //        break;
            //    case 4:
            //        _reqlist = _requestHandler.GetStudentRequestListForTechnical().Where(x => x.RequestDate.StringPersianDateToGerogorianDate() < DateTime.Now && x.RequestDate.StringPersianDateToGerogorianDate() >= DateTime.Now && x.isDeleted != true && (x.IsEquippingResource == true || !string.IsNullOrEmpty(x.OnlineTeacherRole))).ToList();
            //        break;
            //    case 5:
            //        _reqlist = _requestHandler.GetStudentRequestListForTechnical().Where(x => (x.IsEquippingResource == true || !string.IsNullOrEmpty(x.OnlineTeacherRole) &&  x.isDeleted != true)).ToList(); ;
            //        break;
            //}
            //grdDefenceList.DataSource = _reqlist.OrderByDescending(x => x.RequestDate.StringPersianDateToGerogorianDate());
            //grdDefenceList.DataBind();
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
            //if (endTime != null)
            //    lblTime2.Text = TimeSpan.FromTicks((long)endTime.EndTime).ToString().Substring(0, 5);

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
                    lblStatue.Text = "درخواست لغو شد و برای بررسی به کارتابل دانشکده ارجاع داده شد";
                    break;
                case 6:
                    // lblStatue.Text = "درخواست توسط فنی جهت پخش آنلاین تایید گردید";
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
            string firstOnlineTeachRole = string.Empty;
            string secondOnlineTeachName = string.Empty;
            string secondOnlineTeachId = string.Empty;
            string secondOnlineTeachMobile = string.Empty;
            string secondOnlineTeachEmail = string.Empty;
            string secondOnlineTeachRole = string.Empty;

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
                        firstOnlineTeachRole = "استاد مشاور";
                        //اولی و دومی
                        //اگر استاد مشاور دوم آنلاین بوده
                        if (!string.IsNullOrEmpty(inCirculationRequest.OnlineSecondTeacherId.ToString()) && ("200" + inCirculationRequest.OnlineSecondTeacherId.ToString()) == defenceInformation.SecondConsultantId)
                        {
                            secondOnlineTeachName = defenceInformation.SecondConsultantFullName;
                            secondOnlineTeachId = defenceInformation.SecondConsultantId;
                            secondOnlineTeachMobile = defenceInformation.SecondConsultantMobile;
                            secondOnlineTeachEmail = defenceInformation.SecondConsultantMail;
                            secondOnlineTeachRole = "استاد مشاور";
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
                        firstOnlineTeachRole = "استاد مشاور";

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
                        firstOnlineTeachRole = "استاد راهنما";


                        //اگر استاد راهنما دوم آنلاین بوده
                        if (!string.IsNullOrEmpty(inCirculationRequest.OnlineFirstTeacherId.ToString()) && ("200" + inCirculationRequest.OnlineFirstTeacherId.ToString()) == defenceInformation.SecondGuideId)
                        {
                            secondOnlineTeachName = defenceInformation.SecondGuideFullName;
                            secondOnlineTeachId = defenceInformation.SecondGuideId;
                            secondOnlineTeachMobile = defenceInformation.SecondGuideMobile;
                            secondOnlineTeachEmail = defenceInformation.SecondGuideMail;
                            secondOnlineTeachRole = "استاد راهنما";

                        }
                    }
                    else
                    {
                        firstOnlineTeachName = defenceInformation.SecondGuideFullName;
                        firstOnlineTeachId = defenceInformation.SecondGuideId;
                        firstOnlineTeachMobile = defenceInformation.SecondGuideMobile;
                        firstOnlineTeachEmail = defenceInformation.SecondGuideMail;
                        firstOnlineTeachRole = "استاد راهنما";

                    }
                }



            }

            if (!string.IsNullOrEmpty(firstOnlineTeachName))
            {
                divFirstOnlineTeacher.Visible = true;
                lblfirstTeacherEmail.Text = firstOnlineTeachEmail;
                lblfirstTeacherMobile.Text = firstOnlineTeachMobile;
                lblfirstTeacherName.Text = "(" + firstOnlineTeachRole + ") " + firstOnlineTeachName;

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
                lblSecondTeacherName.Text = "(" + secondOnlineTeachRole + ") " + secondOnlineTeachName;
            }
            else
            {
                divSecondOnlineTeacher.Visible = false;
                lblSecondTeacherEmail.Text = string.Empty;
                lblSecondTeacherMobile.Text = string.Empty;
                lblSecondTeacherName.Text = string.Empty;
            }

            //if (requestDetails.Status == 2)
            //{

            tdGrdResult.Visible = true;

            _dateTimeList = rqdateTimeH.GetDateTimeListByRequestId(id);
            grdResult.DataSource = _dateTimeList.OrderBy(c => c.Date);
            grdResult.DataBind();
            //}
            //else
            //{
            //tdGrdResult.Visible = false;

            //}
            var StMob = _requestHandler.GetStudentMobile(Convert.ToInt32(defenceInformation.StudentCode));
            lblMobile.Text = StMob;
            GrdInfoOstad.DataSource = _requestHandler.GetSignutreOstad(defenceInformation);
            GrdInfoOstad.DataBind();
            string scrp = "function f(){$find(\"" + RadWindow3.ClientID +
                          "\").show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);


            #endregion

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
        public void EmptyFieldModalTechnichal()
        {
            hdnReqId.Value = null;
            lblMobile.Text = "";
            hdnStcode.Value = null;
            
            mdlLblNameStudent.Text = "";
            mdlLblMobileStudent.Text = "";
            mdlPanelStudent.Visible = false;
            mdlRdbYesStudent.Checked = false;
            mdlRdbNoStudent.Checked = false;
            mdlTxtReasonStudent.Text = "";
            mdlPnlStReason.Visible = false;
            mdlLblDateStudent.Text = "";

            mdlLblNameDav1.Text = "";
            mdlLblMobileDav1.Text = "";
            mdlPanelDav1.Visible = false;
            mdlrdbYesDav1.Checked = false;
            mdlRdbNoDav1.Checked = false;
            mdlTxtReasonDav1.Text = "";
            mdlPnlDav1Reason.Visible = false;
            mdlLblDateDav1.Text = "";

            mdlLblNameDav2.Text = "";
            mdlLblMobileDav2.Text = "";
            mdlPanelDav2.Visible = false;
            mdlRdbYesDav2.Checked = false;
            mdlRdbNoDav2.Checked = false;
            mdlTxtReasonDav2.Text = "";
            mdlPnlDav2Reason.Visible = false;
            mdlLblDateDav2.Text = "";

            mdlLblNameMosh1.Text = "";
            mdlLblMobileMosh1.Text = "";
            mdlPanelMosh1.Visible = false;
            mdlRdbYesMosh1.Checked = false;
            mdlRdbNoMosh1.Checked = false;
            mdlTxtReasonMosh1.Text = "";
            mdlPnlMosh1Reason.Visible = false;
            mdlLblDateMosh1.Text = "";

            mdlLblNameMosh2.Text = "";
            mdlLblMobileMosh2.Text = "";
            mdlPanelMosh2.Visible = false;
            mdlRdbYesMosh2.Checked = false;
            mdlRdbNoMosh2.Checked = false;
            mdlTxtReasonMosh2.Text = "";
            mdlPnlMosh2Reason.Visible = false;
            mdlLblDateMosh2.Text = "";

            mdlLblNameRah1.Text = "";
            mdlLblMobileRah1.Text = "";
            mdlPanelRah1.Visible = false;
            mdlRdbYesRah1.Checked = false;
            mdlRdbNoRah1.Checked = false;
            mdlTxtReasonRah1.Text = "";
            mdlPnlRah1Reason.Visible = false;
            mdlLblDateRah1.Text = "";

            mdlLblNameRah2.Text = "";
            mdlLblMobileRah2.Text = "";
            mdlPanelRah2.Visible = false;
            mdlRdbYesRah2.Checked = false;
            mdlRdbNoRah2.Checked = false;
            mdlTxtReasonRah2.Text = "";
            mdlPnlRah2Reason.Visible = false;
            mdlLblDateRah2.Text = "";


        }
        protected void modalOpenAcceptTechnical_Click(object sender, EventArgs e)
        {
            EmptyFieldModalTechnichal();
            Button btn = (Button)sender;
            GridDataItem data = (GridDataItem)btn.NamingContainer;
            var id = Convert.ToInt32(data["ID"].Text);
            var studentCode = data["StudentCode"].Text;
            var studentName = data["StudentFullName"].Text;
            var StMob = _requestHandler.GetStudentMobile(Convert.ToInt32(studentCode));
            lblMobile.Text = StMob;

            hdnReqId.Value = id.ToString();
            hdnStcode .Value= studentCode.ToString();
            var defenceInformation = _requestHandler.GetDefenceInformation(studentCode);
            var defence = _requestHandler.GetSignutreOstad(defenceInformation);

            
            mdlLblNameStudent.Text =" "+ studentName+" ";
            mdlLblMobileStudent.Text = StMob;
            mdlPanelStudent.Visible = true;
            foreach(var item in defence)
            {
                if(item.IdTypeOs == 2)
                {
                    mdlPanelMosh1.Visible = true;
                    mdlLblMobileMosh1.Text = item.Mobile;
                    mdlLblNameMosh1.Text = " " + item.FullName + " ";
                }
                else if(item.IdTypeOs==3)
                {
                    mdlPanelRah1.Visible = true;
                    mdlLblMobileRah1.Text = item.Mobile;
                    mdlLblNameRah1.Text = " " + item.FullName + " ";
                }
                else if(item.IdTypeOs == 4)
                {
                    mdlPanelDav1.Visible = true;
                    mdlLblMobileDav1.Text = item.Mobile;
                    mdlLblNameDav1.Text = " "+item.FullName+" ";

                }
                else if(item.IdTypeOs == 5)
                {
                    mdlPanelDav2.Visible = true;
                    mdlLblMobileDav2.Text = item.Mobile;
                    mdlLblNameDav2.Text = " " + item.FullName + " ";

                }
                else if(item.IdTypeOs==6)
                {
                    mdlPanelMosh2.Visible = true;
                    mdlLblMobileMosh2.Text = item.Mobile;
                    mdlLblNameMosh2.Text = " " + item.FullName + " ";
                }
                else if(item.IdTypeOs==7)
                {
                    mdlPanelRah2.Visible = true;
                    mdlLblMobileRah2.Text = item.Mobile;
                    mdlLblNameRah2.Text = " " + item.FullName + " ";
                }
            }



            var statusDefence = _requestHandler.GetStatusDefenceTechnical(id);
            if (statusDefence.FlagAcceptTechnicalStudent != null)
            {
                mdlRdbYesStudent.Checked = (bool)statusDefence.FlagAcceptTechnicalStudent;
                mdlRdbNoStudent.Checked = !(bool)statusDefence.FlagAcceptTechnicalStudent;
                mdlTxtReasonStudent.Text = statusDefence.ReasonTechnicalStudent;
                mdlLblDateStudent.Text = statusDefence.DateReasonTechnicalStudent;
               if( mdlRdbNoStudent.Checked==true)
                {
                    mdlPnlStReason.Visible = true;
                }
            }
            if (statusDefence.FlagAcceptTechnicalMosh1 != null)
            {
                mdlRdbYesMosh1.Checked = (bool)statusDefence.FlagAcceptTechnicalMosh1;
                mdlRdbNoMosh1.Checked = !(bool)statusDefence.FlagAcceptTechnicalMosh1;
                mdlTxtReasonMosh1.Text = statusDefence.ReasonTechnicalMosh1;
                mdlLblDateMosh1.Text = statusDefence.DateReasonTechnicalMosh1;
                if (mdlRdbNoMosh1.Checked == true)
                {
                    mdlPnlMosh1Reason.Visible = true;
                }
            }
            if (statusDefence.FlagAcceptTechnicalMosh2 != null)
            {
                mdlRdbYesMosh2.Checked = (bool)statusDefence.FlagAcceptTechnicalMosh2;
                mdlRdbNoMosh2.Checked = !(bool)statusDefence.FlagAcceptTechnicalMosh2;
                mdlTxtReasonMosh2.Text = statusDefence.ReasonTechnicalMosh2;
                mdlLblDateMosh2.Text = statusDefence.DateReasonTechnicalMosh2;
                if (mdlRdbNoMosh2.Checked == true)
                {
                    mdlPnlMosh2Reason.Visible = true;
                }
            }
            if (statusDefence.FlagAcceptTechnicalRah1 != null)
            {
                mdlRdbYesRah1.Checked = (bool)statusDefence.FlagAcceptTechnicalRah1;
                mdlRdbNoRah1.Checked = !(bool)statusDefence.FlagAcceptTechnicalRah1;
                mdlTxtReasonRah1.Text = statusDefence.ReasonTechnicalRah1;
                mdlLblDateRah1.Text = statusDefence.DateReasonTechnicalRah1;
                if (mdlRdbNoRah1.Checked == true)
                {
                    mdlPnlRah1Reason.Visible = true;
                }
            }
            if (statusDefence.FlagAcceptTechnicalRah2!= null)
            {
                mdlRdbYesRah2.Checked = (bool)statusDefence.FlagAcceptTechnicalRah2;
                mdlRdbNoRah2.Checked = !(bool)statusDefence.FlagAcceptTechnicalRah2;
                mdlTxtReasonRah2.Text = statusDefence.ReasonTechnicalRah2;
                mdlLblDateRah2.Text = statusDefence.DateReasonTechnicalRah2;
                if (mdlRdbNoRah2.Checked == true)
                {
                    mdlPnlRah2Reason.Visible = true;
                }
            }
            if (statusDefence.FlagAcceptTechnicalDavIn != null)
            {
                mdlrdbYesDav1.Checked = (bool)statusDefence.FlagAcceptTechnicalDavIn;
                mdlRdbNoDav1.Checked = !(bool)statusDefence.FlagAcceptTechnicalDavIn;
                mdlTxtReasonDav1.Text = statusDefence.ReasonTechnicalDavin;
                mdlLblDateDav1.Text = statusDefence.DateReasonTechnicalDavin;
                if (mdlRdbNoDav1.Checked == true)
                {
                    mdlPnlDav1Reason.Visible = true;
                }
            }
            if ( statusDefence.FlagAcceptTechnicalDavOut!=null)
                {
                mdlRdbYesDav2.Checked = (bool) statusDefence.FlagAcceptTechnicalDavOut;
                mdlRdbNoDav2.Checked = !(bool)statusDefence.FlagAcceptTechnicalDavOut;
                mdlTxtReasonDav2.Text = statusDefence.ReasonTechnicalDavOut;
                mdlLblDateDav2.Text = statusDefence.DateReasonTechnicalDavOut;
                if (mdlRdbNoDav2.Checked == true)
                {
                    mdlPnlDav2Reason.Visible = true;
                }
            }


            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "$('#ModalAcceptTechnical').modal();", true);
            upModalAccept.Update();
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            var defenceInformation = _requestHandler.GetDefenceInformation(hdnStcode.Value);
            var defence = _requestHandler.GetSignutreOstad(defenceInformation);
        
            var status = new StatusDefenceTechnichal();
            var statusLast = new StatusDefenceTechnichal();
            statusLast = _requestHandler.GetStatusDefenceTechnical(int.Parse(hdnReqId.Value.ToString()));
            status.RequestId =int.Parse( hdnReqId.Value);
            status.StudentCode = int.Parse(hdnStcode.Value);
            if (mdlPanelStudent.Visible == false)
            {
                status.DateReasonTechnicalStudent = null;
                status.ReasonTechnicalStudent = null;
                status.FlagAcceptTechnicalStudent = true;
            }
            else if (mdlPanelStudent.Visible == true && mdlRdbNoStudent.Checked == false && mdlRdbYesStudent.Checked == false)
            {
                status.DateReasonTechnicalStudent = null;
                status.ReasonTechnicalStudent = null;
                status.FlagAcceptTechnicalStudent = null;
            }
            else if (mdlPanelStudent.Visible == true && (mdlRdbNoStudent.Checked != false || mdlRdbYesStudent.Checked != false)
                    && (mdlRdbYesStudent.Checked != statusLast.FlagAcceptTechnicalStudent
                    || !(mdlRdbNoStudent.Checked) != statusLast.FlagAcceptTechnicalStudent
                     || (mdlRdbNoStudent.Checked == true && statusLast.ReasonTechnicalStudent != mdlTxtReasonStudent.Text)))
            {
                status.DateReasonTechnicalStudent = GetDateNowPersian();
                status.FlagAcceptTechnicalStudent = mdlRdbYesStudent.Checked;
                if (mdlRdbYesStudent.Checked == false)
                {
                    status.ReasonTechnicalStudent = mdlTxtReasonStudent.Text;
                }
                else
                    status.ReasonTechnicalStudent = null;

            }
            else
            {
                status.DateReasonTechnicalStudent = statusLast.DateReasonTechnicalStudent;
                status.ReasonTechnicalStudent = statusLast.ReasonTechnicalStudent;
                status.FlagAcceptTechnicalStudent = statusLast.FlagAcceptTechnicalStudent; ;
            }
            if (mdlPanelDav1.Visible==false)
            {
                status.DateReasonTechnicalDavin = null;
                status.ReasonTechnicalDavin = null;
                status.FlagAcceptTechnicalDavIn = true;
            }
            else if (mdlPanelDav1.Visible == true && mdlRdbNoDav1.Checked == false && mdlrdbYesDav1.Checked == false)
            {
                status.DateReasonTechnicalDavin = null;
                status.ReasonTechnicalDavin = null;
                status.FlagAcceptTechnicalDavIn = null;
            }
            else if(mdlPanelDav1.Visible == true&&(mdlRdbNoDav1.Checked!=false|| mdlrdbYesDav1.Checked!=false) 
                    &&(mdlrdbYesDav1.Checked!= statusLast.FlagAcceptTechnicalDavIn
                    ||!(mdlRdbNoDav1.Checked)!= statusLast.FlagAcceptTechnicalDavIn||
                     (mdlRdbNoDav1.Checked==true && statusLast.ReasonTechnicalDavin!= mdlTxtReasonDav1.Text))
                    )
            {
                status.DateReasonTechnicalDavin = GetDateNowPersian();
                status.FlagAcceptTechnicalDavIn = mdlrdbYesDav1.Checked;
                if (mdlrdbYesDav1.Checked == false)
                {
                    status.ReasonTechnicalDavin = mdlTxtReasonDav1.Text;
                }
                else
                    status.ReasonTechnicalDavin = null;

            }
            else
            {
                status.DateReasonTechnicalDavin = statusLast.DateReasonTechnicalDavin;
                status.ReasonTechnicalDavin = statusLast.ReasonTechnicalDavin;
                status.FlagAcceptTechnicalDavIn = statusLast.FlagAcceptTechnicalDavIn; ;
            }
       
           if (mdlPanelDav2.Visible == false)
            {
                status.DateReasonTechnicalDavOut = null;
                status.ReasonTechnicalDavOut = null;
                status.FlagAcceptTechnicalDavOut = true;
            }
            else if (mdlPanelDav2.Visible == true && mdlRdbNoDav2.Checked == false && mdlRdbYesDav2.Checked == false)
            {
                status.DateReasonTechnicalDavOut = null;
                status.ReasonTechnicalDavOut = null;
                status.FlagAcceptTechnicalDavOut = null;
            }
            else if (mdlPanelDav2.Visible == true && (mdlRdbNoDav2.Checked != false || mdlRdbYesDav2.Checked != false)
                    && (mdlRdbYesDav2.Checked != statusLast.FlagAcceptTechnicalDavOut
                    || !(mdlRdbNoDav2.Checked) != statusLast.FlagAcceptTechnicalDavOut
                     || (mdlRdbNoDav2.Checked == true && statusLast.ReasonTechnicalDavOut != mdlTxtReasonDav2.Text))
                   )
            {
                status.DateReasonTechnicalDavOut = GetDateNowPersian();
                status.FlagAcceptTechnicalDavOut = mdlRdbYesDav2.Checked;
                if (mdlRdbYesDav2.Checked == false)
                {
                    status.ReasonTechnicalDavOut = mdlTxtReasonDav2.Text;
                }
                else
                    status.ReasonTechnicalDavOut = null;

            }
            else
            {
                status.DateReasonTechnicalDavOut = statusLast.DateReasonTechnicalDavOut;
                status.ReasonTechnicalDavOut = statusLast.ReasonTechnicalDavOut;
                status.FlagAcceptTechnicalDavOut = statusLast.FlagAcceptTechnicalDavOut; ;
            }

            if (mdlPanelMosh1.Visible == false)
            {
                status.DateReasonTechnicalMosh1 = null;
                status.ReasonTechnicalMosh1 = null;
                status.FlagAcceptTechnicalMosh1 = true;
            }
            else if (mdlPanelMosh1.Visible == true && mdlRdbNoMosh1.Checked == false && mdlRdbYesMosh1.Checked == false)
            {
                status.DateReasonTechnicalMosh1 = null;
                status.ReasonTechnicalMosh1 = null;
                status.FlagAcceptTechnicalMosh1 = null;
            }
            else if (mdlPanelMosh1.Visible == true && (mdlRdbNoMosh1.Checked != false || mdlRdbYesMosh1.Checked != false)
                    && (mdlRdbYesMosh1.Checked != statusLast.FlagAcceptTechnicalMosh1
                    || !(mdlRdbNoMosh1.Checked) != statusLast.FlagAcceptTechnicalMosh1
                     || (mdlRdbNoMosh1.Checked == true && statusLast.ReasonTechnicalMosh1 != mdlTxtReasonMosh1.Text)))
            {
                status.DateReasonTechnicalMosh1 = GetDateNowPersian();
                status.FlagAcceptTechnicalMosh1 = mdlRdbYesMosh1.Checked;
                if (mdlRdbYesMosh1.Checked == false)
                {
                    status.ReasonTechnicalMosh1 = mdlTxtReasonMosh1.Text;
                }
                else
                    status.ReasonTechnicalMosh1 = null;

            }
            else
            {
                status.DateReasonTechnicalMosh1 = statusLast.DateReasonTechnicalMosh1;
                status.ReasonTechnicalMosh1 = statusLast.ReasonTechnicalMosh1;
                status.FlagAcceptTechnicalMosh1 = statusLast.FlagAcceptTechnicalMosh1; ;
            }

            if (mdlPanelMosh2.Visible == false)
            {
                status.DateReasonTechnicalMosh2 = null;
                status.ReasonTechnicalMosh2 = null;
                status.FlagAcceptTechnicalMosh2 = true;
            }
            else if (mdlPanelMosh2.Visible == true && mdlRdbNoMosh2.Checked == false && mdlRdbYesMosh2.Checked == false)
            {
                status.DateReasonTechnicalMosh2 = null;
                status.ReasonTechnicalMosh2 = null;
                status.FlagAcceptTechnicalMosh2 = null;
            }
            else if (mdlPanelMosh2.Visible == true && (mdlRdbNoMosh2.Checked != false || mdlRdbYesMosh2.Checked != false)
                    && (mdlRdbYesMosh2.Checked != statusLast.FlagAcceptTechnicalMosh2
                    || !(mdlRdbNoMosh2.Checked) != statusLast.FlagAcceptTechnicalMosh2
                     || (mdlRdbNoMosh2.Checked == true && statusLast.ReasonTechnicalMosh2 != mdlTxtReasonMosh2.Text)))
            {
                status.DateReasonTechnicalMosh2 = GetDateNowPersian();
                status.FlagAcceptTechnicalMosh2 = mdlRdbYesMosh2.Checked;
                if (mdlRdbYesMosh2.Checked == false)
                {
                    status.ReasonTechnicalMosh2 = mdlTxtReasonMosh2.Text;
                }
                else
                    status.ReasonTechnicalMosh2 = null;

            }
            else
            {
                status.DateReasonTechnicalMosh2 = statusLast.DateReasonTechnicalMosh2;
                status.ReasonTechnicalMosh2 = statusLast.ReasonTechnicalMosh2;
                status.FlagAcceptTechnicalMosh2 = statusLast.FlagAcceptTechnicalMosh2; ;
            }

            if (mdlPanelRah1.Visible == false)
            {
                status.DateReasonTechnicalRah1 = null;
                status.ReasonTechnicalRah1 = null;
                status.FlagAcceptTechnicalRah1 = true;
            }
            else if (mdlPanelRah1.Visible == true && mdlRdbNoRah1.Checked == false && mdlRdbYesRah1.Checked == false)
            {
                status.DateReasonTechnicalRah1 = null;
                status.ReasonTechnicalRah1 = null;
                status.FlagAcceptTechnicalRah1 = null;
            }
            else if (mdlPanelRah1.Visible == true && (mdlRdbNoRah1.Checked != false || mdlRdbYesRah1.Checked != false)
                    && (mdlRdbYesRah1.Checked != statusLast.FlagAcceptTechnicalRah1
                    || !(mdlRdbNoRah1.Checked) != statusLast.FlagAcceptTechnicalRah1
                    || (mdlRdbNoRah1.Checked == true && statusLast.ReasonTechnicalRah1 != mdlTxtReasonRah1.Text)))
            {
                status.DateReasonTechnicalRah1 = GetDateNowPersian();
                status.FlagAcceptTechnicalRah1 = mdlRdbYesRah1.Checked;
                if (mdlRdbYesRah1.Checked == false)
                {
                    status.ReasonTechnicalRah1 = mdlTxtReasonRah1.Text;
                }
                else
                    status.ReasonTechnicalRah1 = null;

            }
           else
            {
                status.DateReasonTechnicalRah1 = statusLast.DateReasonTechnicalRah1;
                status.ReasonTechnicalRah1 = statusLast.ReasonTechnicalRah1;
                status.FlagAcceptTechnicalRah1 = statusLast.FlagAcceptTechnicalRah1; ;
            }
            if (mdlPanelRah2.Visible == false)
            {
                status.DateReasonTechnicalRah2 = null;
                status.ReasonTechnicalRah2 = null;
                status.FlagAcceptTechnicalRah2 = true;
            }
            else if (mdlPanelRah2.Visible == true && mdlRdbNoRah2.Checked == false && mdlRdbYesRah2.Checked == false)
            {
                status.DateReasonTechnicalRah2 = null;
                status.ReasonTechnicalRah2 = null;
                status.FlagAcceptTechnicalRah2 = null;
            }
            else if (mdlPanelRah2.Visible == true && (mdlRdbNoRah2.Checked != false || mdlRdbYesRah2.Checked != false)
                    && (mdlRdbYesRah2.Checked != statusLast.FlagAcceptTechnicalRah2
                    || !(mdlRdbNoRah2.Checked) != statusLast.FlagAcceptTechnicalRah2
                    || (mdlRdbNoRah2.Checked == true && statusLast.ReasonTechnicalRah2 != mdlTxtReasonRah2.Text)))
            {
                status.DateReasonTechnicalRah2 = GetDateNowPersian();
                status.FlagAcceptTechnicalRah2 = mdlRdbYesRah2.Checked;
                if (mdlRdbYesRah2.Checked == false)
                {
                    status.ReasonTechnicalRah2 = mdlTxtReasonRah2.Text;
                }
                else
                    status.ReasonTechnicalRah2 = null;

            }

            else
            {
                status.DateReasonTechnicalRah2 = statusLast.DateReasonTechnicalRah2;
                status.ReasonTechnicalRah2 = statusLast.ReasonTechnicalRah2;
                status.FlagAcceptTechnicalRah2 = statusLast.FlagAcceptTechnicalRah2; ;
            }


            if (_requestHandler.updateStatusDefenceTechnical(status))
            {
                lblAlert.Text = "عملیات با موفقیت انجام شد";
            }
            else
                lblAlert.Text = "سیستم دچار اختلال گردید";



            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal1", "$('#ModalAcceptTechnical').modal('hide');", true);
            upModalAccept.Update();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal2", "$('#ModalAlert').modal();", true);
            upModalAlert.Update();

        }
        protected void mdlRdbNoDav2_CheckedChanged(object sender, EventArgs e)
        {
            if (mdlRdbNoDav2.Checked == true)
            {
                mdlPnlDav2Reason.Visible = true;
                upModalAccept.Update();
            }

        }

        protected void mdlRdbYesStudent_CheckedChanged(object sender, EventArgs e)
        {
            if (mdlRdbYesStudent.Checked == true)
            {
                mdlPnlStReason.Visible = false;
                upModalAccept.Update();
            }

        }

        protected void mdlRdbNoStudent_CheckedChanged(object sender, EventArgs e)
        {
            if (mdlRdbNoStudent.Checked == true)
            {
                mdlPnlStReason.Visible = true;
                upModalAccept.Update();
            }
        }

        protected void mdlRdbYesMosh1_CheckedChanged(object sender, EventArgs e)
        {
            if (mdlRdbYesMosh1.Checked == true)
            {
             
                mdlPnlMosh1Reason.Visible = false;
                upModalAccept.Update();

            }

        }

        protected void mdlRdbNoMosh1_CheckedChanged(object sender, EventArgs e)
        {
            if (mdlRdbNoMosh1.Checked == true)
            { mdlPnlMosh1Reason.Visible = true;
                upModalAccept.Update();
            }
        }

        protected void mdlRdbYesMosh2_CheckedChanged(object sender, EventArgs e)
        {
            if (mdlRdbYesMosh2.Checked == true)
            { mdlPnlMosh1Reason.Visible = false;
                upModalAccept.Update();
            }

        }

        protected void mdlRdbNoMosh2_CheckedChanged(object sender, EventArgs e)
        {
            if (mdlRdbNoMosh2.Checked == true)
            { 
                mdlPnlMosh2Reason.Visible = true;
                upModalAccept.Update();
            }
        }

        protected void mdlRdbYesRah1_CheckedChanged(object sender, EventArgs e)
        {
            if (mdlRdbYesRah1.Checked == true)
            { mdlPnlRah1Reason.Visible = false;
                upModalAccept.Update();
            }
        }

        protected void mdlRdbNoRah1_CheckedChanged(object sender, EventArgs e)
        {
            if (mdlRdbNoRah1.Checked == true)
            { 
                mdlPnlRah1Reason.Visible = true;
                upModalAccept.Update();
            }
        }

        protected void mdlRdbYesRah2_CheckedChanged(object sender, EventArgs e)
        {
            if (mdlRdbYesRah2.Checked == true)
            {
                mdlPnlRah2Reason.Visible = false;
                upModalAccept.Update();
            }
        }

        protected void mdlRdbNoRah2_CheckedChanged(object sender, EventArgs e)
        {
            if (mdlRdbNoRah2.Checked == true)
            { mdlPnlRah2Reason.Visible = true;
                upModalAccept.Update();
            }
        }


        protected void mdlRdbNoDav1_CheckedChanged(object sender, EventArgs e)
        {
            if (mdlRdbNoDav1.Checked == true)
            {
                mdlPnlDav1Reason.Visible = true;
                upModalAccept.Update();
            }
        }

        protected void mdlRdbYesDav2_CheckedChanged(object sender, EventArgs e)
        {
            if (mdlRdbYesDav2.Checked == true)
            {
                mdlPnlDav2Reason.Visible = false;
                upModalAccept.Update();
            }
        }

        protected void mdlrdbYesDav1_CheckedChanged(object sender, EventArgs e)
        {
            if(mdlrdbYesDav1.Checked==true)
            {
                mdlPnlDav1Reason.Visible = false;
                upModalAccept.Update();
            }
        }
        public string GetDateNowPersian()
        {
            var pc = new PersianCalendar();

            string year = pc.GetYear(DateTime.Now).ToString();
            string month=pc.GetMonth(DateTime.Now).ToString().Length==0? ("0"+pc.GetMonth(DateTime.Now).ToString()): pc.GetMonth(DateTime.Now).ToString();
            string date = pc.GetDayOfMonth(DateTime.Now).ToString().Length == 0 ? ("0" + pc.GetDayOfMonth(DateTime.Now).ToString()): pc.GetDayOfMonth(DateTime.Now).ToString();

            return year + "/" + month + "/" + date;
        }

    }
}