
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
using ResourceControl.PL.Forms;
using Telerik.Web.UI;
using Control = System.Web.UI.Control;
using ListItem = System.Web.UI.WebControls.ListItem;
using System.Data;
using System.Globalization;
using IAUEC_Apps.UI.University.Students.CMS;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Web;
using IAUEC_Apps.DTO.AdobeClasses;
using IAUEC_Apps.DTO.CommonClasses;
using IAUEC_Apps.Business.Adobe;
using IAUEC_Apps.Business.Conatct;
using IAUEC_Apps.Business.Conatct.Functions;
using Org.BouncyCastle.Ocsp;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;


namespace IAUEC_Apps.UI.ResourceControl.Forms
{
    public partial class EducationStudentReview : System.Web.UI.Page
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


                if (Session["StausLinke"] == null || Session["StausLinke"].ToString() == 1.ToString())
                {
                    ViewState["stateForStudent"] = 1; //منتظر تایید
                }
                if (Session["StausLinke"] != null && Session["StausLinke"].ToString() != 1.ToString())
                {
                    ViewState["stateForStudent"] = Convert.ToInt32(Session["StausLinke"]);
                    drpRequestTypeList.SelectedValue = Session["StausLinke"].ToString();
                }
                if(getUserRoles().Contains(86))//کارشناس پژوهش
                {
                    drpRequestTypeList.SelectedValue = "3";
                    drpRequestTypeList.Visible = false;
                }
            }


        }

        private List<int> getUserRoles()
        {
            Business.Common.LoginBusiness lb = new LoginBusiness();
            var roles = lb.Get_UserRoles(Session[sessionNames.userID_Karbar].ToString());
            List<int> a=new List<int>();
            foreach (DataRow dr in roles.Rows)
                a.Add(Convert.ToInt32(dr["roleID"]));
            return a;

        }
        protected void grdDefenceList_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            int defStatus;
            if (getUserRoles().Contains(86))//کارشناس پژوهش
                defStatus = 3;
            else
                 defStatus = Convert.ToInt32(ViewState["stateForStudent"]);

            switch (defStatus)
            {
                case 1://منتظر تایید
                    _reqlist = _requestHandler.GetStudentRequestListForEducation(Convert.ToInt32(Session["DaneshId"]))?
                       .Where(x =>( x.isDeleted != true && (x.status == (int)ResourceControlEnums.RequestDefenceStaus.submitted || x.status == (int)ResourceControlEnums.RequestDefenceStaus.denied) && x.RequestDate.StringPersianDateToGerogorianDate().Date >= DateTime.Now.Date)
                      && (RequestHandler.WorkingDays12h(x.DateRegistration) <= DateTime.Now)).ToList();
                        //.Where(x => x.isDeleted != true && (x.status == (int)ResourceControlEnums.RequestDefenceStaus.submitted || x.status == (int)ResourceControlEnums.RequestDefenceStaus.denied)).ToList(); //&& x.RequestDate.StringPersianDateToGerogorianDate().Date >= DateTime.Now.Date).ToList();
                    break;
                case 2://تایید شده
                    _reqlist = _requestHandler.GetStudentRequestListForEducation(Convert.ToInt32(Session["DaneshId"]))?
                        .Where(x => x.isDeleted != true
                                && (x.status == (int)ResourceControlEnums.RequestDefenceStaus.educationApprove
                                || x.status == (int)ResourceControlEnums.RequestDefenceStaus.technicalApprove
                                || x.status == (int)ResourceControlEnums.RequestDefenceStaus.approved
                                || x.status == (int)ResourceControlEnums.RequestDefenceStaus.FinancialApprove
                                ) /*&& x.RequestDate.StringPersianDateToGerogorianDate() >= DateTime.Now */
                                && x.isDeleted != true
                                && (RequestHandler.WorkingDays12h(x.DateRegistration) <= DateTime.Now)
                        ).ToList();
                    break;
                case 3://تایید اداری
                    _reqlist = _requestHandler.GetStudentRequestListForEducation(Convert.ToInt32(Session["DaneshId"]))?
                        .Where(x => x.isDeleted != true
                                && x.status == (int)ResourceControlEnums.RequestDefenceStaus.approved
                                //&& x.RequestDate.StringPersianDateToGerogorianDate() >= DateTime.Now 
                                && x.isDeleted != true
                                && (RequestHandler.WorkingDays12h(x.DateRegistration) <= DateTime.Now)
                        ).ToList();
                    break;
                case 4://از دست رفته
                    _reqlist = _requestHandler.GetStudentRequestListForEducation(Convert.ToInt32(Session["DaneshId"]))?
                        .Where(x => x.isDeleted != true
                        && (x.status == (int)ResourceControlEnums.RequestDefenceStaus.submitted || x.status == (int)ResourceControlEnums.RequestDefenceStaus.denied)
                        && x.RequestDate.StringPersianDateToGerogorianDate().Date < DateTime.Now.Date
                        && (RequestHandler.WorkingDays12h(x.DateRegistration) <= DateTime.Now)).ToList();
                    break;
                case 5://کلیه درخواست ها
                    _reqlist = _requestHandler.GetStudentRequestListForEducation(Convert.ToInt32(Session["DaneshId"]))
                    .Where(x =>
                    (x.status != 0 && x.isDeleted != true) ||
                    (x.status == 3 && x.isDeleted) ||
                    (x.status == 0 && x.isDeleted == false)
                   )
                    .ToList();
                    break;
                case 6://حذف شده
                    _reqlist = _requestHandler.GetStudentRequestListForEducation(Convert.ToInt32(Session["DaneshId"]))?
                        .Where(x => x.isDeleted == true && x.status == 3 && x.RequestDate.StringPersianDateToGerogorianDate().Date >= DateTime.Now.Date
                        && (RequestHandler.WorkingDays12h(x.DateRegistration) <= DateTime.Now)).ToList();
                    break;
                case 7://لیست درخواست های موجود در کارتابل استاد
                    _reqlist = _requestHandler.GetStudentRequestListForEducation(Convert.ToInt32(Session["DaneshId"]))?
                         .Where(x => (x.isDeleted != true && (x.status == (int)ResourceControlEnums.RequestDefenceStaus.submitted ))
                        && (RequestHandler.WorkingDays12h(x.DateRegistration) > DateTime.Now)).ToList();
                    break;
                case 9://لیست درخواست های موجود در کارتابل مالی
                    _reqlist = _requestHandler.GetStudentRequestListForEducation(Convert.ToInt32(Session["DaneshId"]))?
                               .Where(x => (x.isDeleted != true && (x.status == (int)ResourceControlEnums.RequestDefenceStaus.educationApprove)
                            //   && (((x.RequestDate.StringPersianDateToGerogorianDate().Date >= RequestHandler.WorkingDays48h(DateTime.Now.Date).Date) && !(x.IsRequestEducation))
                               && (x.RequestDate.StringPersianDateToGerogorianDate().Date >= DateTime.Now.Date) 
                               //&& (x.IsRequestEducation))
                               )).ToList();
                    break;
                case 8://لیست درخواست های موجود در کارتابل فنی
                    _reqlist = _requestHandler.GetStudentRequestListForTechnical()?.Where(
                                      x => x.status == (int)ResourceControlEnums.RequestDefenceStaus.FinancialApprove
                                 && x.RequestDate.StringPersianDateToGerogorianDate().Date >= DateTime.Now.Date
                                 && x.isDeleted != true
                                 // && (x.IsEquippingResource == true || !string.IsNullOrEmpty(x.OnlineTeacherRole))
                                 ).ToList();
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
            if (getUserRoles().Contains(86))//کارشناس پژوهش
                status = 3;
            else
                status = Convert.ToInt32(ViewState["stateForStudent"]);
 
            var eItem = e.Item as GridDataItem;
            if (eItem == null) return;
            GridDataItem item = eItem;
            Button divApprove = (Button)item["operator"].FindControl("btnApprove");
            Telerik.Web.UI.RadCheckBox divchkSelectUnit = (Telerik.Web.UI.RadCheckBox)eItem.FindControl("chkSelectUnit");
            Button divAvoid = (Button)item["operator"].FindControl("btnAvoid");
            Button divEdit = (Button)item["operator"].FindControl("btnEdit");
            Button divScore= (Button)item["operator"].FindControl("btnScore");
            System.Web.UI.WebControls.CheckBox divFinalReport = (System.Web.UI.WebControls.CheckBox)item["operator"].FindControl("ChkFinal");
            Button btnShowDetail = (Button)item["operator"].FindControl("btnShowDetail");
            var btnPrint = (Button)item["operator"].FindControl("btnPrint");
            //var drpDefenceDate = (DropDownList)item["operator"].FindControl("drpDefenceDate");
            var hiddenStatus = Convert.ToInt32(((HiddenField)item["operator"].FindControl("HiddenStatusValue")).Value);
            //if (Convert.ToInt32(Session["DaneshId"]) != 0)
            switch (status)
            {
                case 1:
                    btnPrint.Visible = false;
                    divScore.Visible = false;
                    //drpDefenceDate.Visible = false;
                    divApprove.Enabled = false;
                    //divApprove.Enabled = hiddenStatus != 3;
                    divAvoid.Enabled = true;
                    divEdit.Enabled = true;
                    divScore.Enabled = false;
                    btnShowDetail.Enabled = true;
                    divchkSelectUnit.Visible = hiddenStatus != 3;
                    divchkSelectUnit.Enabled = hiddenStatus != 3;
                    break;
                case 2:
                    divScore.Visible = false ;

                    btnPrint.Visible = false;
                    //drpDefenceDate.Visible = false;
                    divApprove.Enabled = false;
                    divAvoid.Enabled = true;
                    divEdit.Enabled = true;
                    btnShowDetail.Enabled = true;
                    divchkSelectUnit.Visible = false;
                    break;
                case 3:
                    btnPrint.Visible = true;
                    //drpDefenceDate.Visible = true;
                    divApprove.Visible = false;
                    divAvoid.Enabled = true;
                    divEdit.Enabled = true;
                    divScore.Visible = true;
                    divFinalReport.Visible = true;
                    btnShowDetail.Enabled = true;
                    divchkSelectUnit.Visible = false;
                    break;
                case 4:
                    divScore.Visible = false;
                    btnPrint.Visible = false;
                    //drpDefenceDate.Visible = false;
                    divApprove.Enabled = false;
                    divAvoid.Enabled = false;
                    divEdit.Enabled = true;
                    btnShowDetail.Enabled = true;
                    divchkSelectUnit.Visible = false;
                    break;
                case 5:
                    divScore.Visible = false;
                    btnPrint.Visible = false;
                    //drpDefenceDate.Visible = false;
                    divApprove.Enabled = false;
                    divAvoid.Enabled = false;
                    divEdit.Enabled = false;
                    btnShowDetail.Enabled = true;
                    divchkSelectUnit.Visible = false;
                    break;
                case 6:
                    divScore.Visible = false;
                    btnPrint.Visible = false;
                    //drpDefenceDate.Visible = false;
                    divApprove.Enabled = false;
                    divAvoid.Enabled = false;
                    divEdit.Enabled = false;
                    btnShowDetail.Enabled = true;
                    divchkSelectUnit.Visible = false;
                    break;
                case 7:
                    divScore.Visible = false;
                    btnPrint.Visible = false;
                    //drpDefenceDate.Visible = false;
                    divApprove.Enabled = false;
                    divAvoid.Enabled = false;
                    divEdit.Enabled = false;
                    btnShowDetail.Enabled = true;
                    divchkSelectUnit.Visible = false;
                    break;
                case 8:
                    divScore.Visible = false;
                    btnPrint.Visible = false;
                    //drpDefenceDate.Visible = false;
                    divApprove.Enabled = false;
                    divAvoid.Enabled = false;
                    divEdit.Enabled = false;
                    btnShowDetail.Enabled = true;
                    divchkSelectUnit.Visible = false;
                    break;
                case 9:
                    divScore.Visible = false;
                    btnPrint.Visible = false;
                    //drpDefenceDate.Visible = false;
                    divApprove.Enabled = false;
                    divAvoid.Enabled = false;
                    divEdit.Enabled = false;
                    btnShowDetail.Enabled = true;
                    divchkSelectUnit.Visible = false;
                    break;
            }
            if(getUserRoles().Contains(86))
            {
                btnPrint.Visible = true;
                //drpDefenceDate.Visible = true;
                divApprove.Visible = false;
                divAvoid.Visible = false;
                divEdit.Visible = false;
                divScore.Visible = true;
                divFinalReport.Visible = true;
                btnShowDetail.Enabled = true;
               
            }

        }

        protected void grdDefenceList_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            var eItem = e.Item as GridDataItem;
            
            if (eItem == null) return;
           
            Button divApprove = (Button)eItem.FindControl("btnApprove");
           // Button divApprove = (Button)eItem["operator"].FindControl("btnApprove");
            Telerik.Web.UI.RadCheckBox divchkSelectUnit = (Telerik.Web.UI.RadCheckBox)eItem.FindControl("chkSelectUnit");
            if (e.CommandName == "print")
            {

                var userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
                var splitedArgument = e.CommandArgument.ToString().Split('-');           
               
                //var eItem = e.Item as GridDataItem;
                System.Web.UI.WebControls.CheckBox ch = (System.Web.UI.WebControls.CheckBox)eItem.FindControl("ChkFinal");
                
                Session["PrintDefenceSession"] = e.CommandArgument + "-"+ch.Checked;
                if (!string.IsNullOrEmpty(splitedArgument[0]?.ToString()) && ch.Checked == true)
                    if (!string.IsNullOrEmpty(splitedArgument[0]?.ToString())&&ch.Checked)
                {
                    var scoreAccept = _requestHandler.GetScoreForDefence(int.Parse(splitedArgument[0].ToString().ToString()));
                

                    if((scoreAccept.Score == null|| scoreAccept.Score < 0 || scoreAccept.FlagAcceptScoreDavin==false|| scoreAccept.FlagAcceptScoreDavOut == false||scoreAccept.FlagAcceptScoreMosh1==false
                        ||scoreAccept.FlagAcceptScoreRah1==false|| scoreAccept.FlagAcceptScoreDavin == null || scoreAccept.FlagAcceptScoreDavOut == null || scoreAccept.FlagAcceptScoreMosh1 == null
                        || scoreAccept.FlagAcceptScoreRah1 == null))
                    {
                        
                        lblTitle.Text = "پیام سیستم";
                            if (scoreAccept.Score == null|| scoreAccept.Score<0)
                                lblAlert.Text = "نمره ای وارد نشده است";
                            else if (scoreAccept.FlagAcceptScoreDavin == false || scoreAccept.FlagAcceptScoreDavOut == false||
                                scoreAccept.FlagAcceptScoreDavin == null || scoreAccept.FlagAcceptScoreDavOut == null)
                                lblAlert.Text = "استاد یا استادان داور دانشجو نمره دفاع را تایید نکرده است";
                            else if (scoreAccept.FlagAcceptScoreMosh1 == false|| scoreAccept.FlagAcceptScoreMosh1 == null)
                                lblAlert.Text = "استاد مشاور دانشجو نمره دفاع را تایید نکرده است";
                            else if (scoreAccept.FlagAcceptScoreRah1 == false|| scoreAccept.FlagAcceptScoreRah1 == null)
                                lblAlert.Text = "استاد راهنما دانشجو نمره دفاع را تایید نکرده است";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal2", "$('#ModalAlert').modal();", true);
                        upModalAlert.Update();
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(splitedArgument[1]?.ToString()))
                {
                    var avg = _requestHandler.GetTotalAverageByStudentCode(splitedArgument[1].ToString());
                    if (avg <= 14)
                    {
                        string scrp = "function f(){$find(\"" + rwAverageIsLessThan14.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
                        return;
                    }
                }
                
                var comman = new CommonBusiness();
                comman.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 189,
                    string.Format("{0}", ("چاپ و مشاهده صورتجلسه دفاع کیفی" + Session["PrintDefenceSession"].ToString())), Convert.ToInt32(splitedArgument[0]));

                string scrp2 = "function f(){$find(\"" + rdwPrint.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp2, true);
            }

            if(e.CommandName == "Confirm")
            {
                var userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
                var splitedArgument = e.CommandArgument.ToString().Split('-');
                Session["PopupInfo"] = e.CommandArgument;

                if (!string.IsNullOrEmpty(splitedArgument[1]?.ToString()))
                {
                    var avg = _requestHandler.GetTotalAverageByStudentCode(splitedArgument[1].ToString());
                    if (avg <= 14)
                    {
                        string scrp = "function f(){$find(\"" + rwAverageIsLessThan14_2.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
                        return;
                    }
                }

                string scrp2 = "function f(){$find(\"" + rdwConfirmPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp2, true);                
            }
            if (e.CommandName == "SelectUnit")
            {
                if (divchkSelectUnit.Checked == true)
                    divApprove.Enabled = true;
                else
                    divApprove.Enabled = false;

            }
        }

 

        protected void btnShowRadWindowSooratJalaseh_Click(object sender, EventArgs e)
        {
            var comman = new CommonBusiness();
            var userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
            var stcode = Session["PrintDefenceSession"].ToString().Split('-')[1];

            comman.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 189,
                string.Format("{0}", ("چاپ و مشاهده صورتجلسه دفاع کیفی" + Session["PrintDefenceSession"].ToString())), Convert.ToInt32(stcode));

            string scrp2 = "function f(){ closeRadWindowAverageIsLessThan14(); $find(\"" + rdwPrint.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp2, true);
        }
        protected void btnCancleShowRadWindowSooratJalaseh_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, "closeRadWindowAverageIsLessThan14();", true);
        }



        protected void btnShowPopupAvgLassThan14_Click(object sender, EventArgs e)
        {
            string scrp2 = "function f(){ closeRadWindowAverageIsLessThan14_2();  $find(\"" + rdwConfirmPopup.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp2, true);
        }
        protected void btnCancleShowPopupAvgLassThan14_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, "closeRadWindowAverageIsLessThan14_2();", true);
        }

        protected void btnApprovedInConfirmPopup_Click(object sender, EventArgs e)
        {
            var dataSplited = Session["PopupInfo"].ToString().Split('-');
            var reqID = dataSplited[0];
            var stcode = dataSplited[1];
            var reqDate = dataSplited[2];

            var diffDateFor24 = (reqDate.StringPersianDateToGerogorianDate() - RequestHandler.OneWorkingDays(DateTime.Now.Date)).TotalDays;
            if (_requestHandler.ISOnlineRequest(Convert.ToInt32(reqID)))
                if (diffDateFor24 <= 0)
                {
                    string msg = "باید فاصله زمانی ثبت درخواست شما با برگزاری جلسه دفاع حداقل 24 ساعت باشد";
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, " closeRadWindowConfirmPopup();", true);
                    RadWindowManager1.RadAlert(msg, 500, 100, "پیام سیستم", "closeRadWindow2");                   
                    return;
                }
            _requestHandler.UpdateStatusDefRequest((int)ResourceControlEnums.RequestDefenceStaus.educationApprove, Convert.ToInt32(reqID));
         
            
            //send sms for def Online
            var sendDone= RequestHandler.SendSmsAcceptEducataionForDef(stcode);
            //-------------
            var userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
            var comman = new CommonBusiness();
            comman.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 164, string.Format("{0}", "تایید درخواست جلسه دفاع توسط دانشکده"), Convert.ToInt32(reqID));
            
            
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, " closeRadWindowConfirmPopup();", true);
            string approveMessage = "درخواست شماره " + reqID.ToString() + " با موفقیت تایید گردید.";
            RadWindowManager1.RadAlert(approveMessage, 400, 100, "پیام سیستم", "closeRadWindow2");
            Session["PopupInfo"] = null;
        }

        protected void btnCancleInConfirmPopup_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, "closeRadWindowConfirmPopup();", true);
        }


        //protected void btnApprove_OnClick(object sender, EventArgs e)
        //{
        //Button btn = (Button)sender;
        //GridDataItem data = (GridDataItem)btn.NamingContainer;
        //string id = data["ID"].Text;
        //var requestDate = data["RequestDate"].Text;

        //var diffDateFor24 = (requestDate.StringPersianDateToGerogorianDate() - RequestHandler.OneWorkingDays(DateTime.Now.Date)).TotalDays;
        //if (_requestHandler.ISOnlineRequest(Convert.ToInt32(id)))
        //    if (diffDateFor24 <= 0)
        //    {
        //        string msg = "باید فاصله زمانی ثبت درخواست شما با برگزاری جلسه دفاع حداقل 24 ساعت باشد";
        //        RadWindowManager1.RadAlert(msg, 500, 100, "پیام سیستم", "closeRadWindow2");
        //        return;
        //    }
        //_requestHandler.UpdateStatusDefRequest((int)ResourceControlEnums.RequestDefenceStaus.educationApprove, Convert.ToInt32(id));

        //var userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
        //var comman = new CommonBusiness();
        //comman.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 164, string.Format("{0}", "تایید درخواست جلسه دفاع توسط دانشکده"), Convert.ToInt32(id));
        //txtDenyMessage1.Text = string.Empty;



        //string approveMessage = "درخواست شماره " + id.ToString() + " با موفقیت تایید گردید.";
        //RadWindowManager1.RadAlert(approveMessage, 400, 100, "پیام سیستم", "closeRadWindow2");
        //}

        protected void btnAvoid_OnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridDataItem data = (GridDataItem)btn.NamingContainer;
            string id = data["ID"].Text;
            hdnfDenyReqId.Value = id;

            string scrp = "function f(){$find(\"" + RadWindow2.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);

        }

        protected void btnDenyRequest_OnClick(object sender, EventArgs e)
        {
            lblalertMessage.Visible = false;
            Button btn = (Button)sender;
            GridDataItem data = (GridDataItem)btn.NamingContainer;
            string id = data["ID"].Text;
            hdnfDenyReqId1.Value = id;

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
            //_reqlist  = _reqlist.OrderByDescending(x => x.RequestDate.StringPersianDateToGerogorianDate()).ToList();
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
            if(inCirculationRequest.FlagDoingMeetingOnline)
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


        protected void btnEdit_OnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridDataItem data = (GridDataItem)btn.NamingContainer;
            var id = Convert.ToInt32(data["ID"].Text);

            //Session["StudentrequestIdForEdit"] = id;


            string postfix = "?reqId=" + id;

            Response.Redirect("~/ResourceControl/Forms/StudentEditRequest.aspx" + postfix);
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
            req.Status = 3;//3 means request has been denied .

            RequestHandler requestBusiness = new RequestHandler();
            var beforeStatus = _requestHandler.GetStatusByReqId(req.ID);
            int id = requestBusiness.DenyRequest(req);


            var stcode = _requestHandler.GetStCodeByReqId(id);
            req.ID = Convert.ToInt32(hdnfDenyReqId1.Value);

            if (_requestHandler.DeleteStudentRequest(req.ID))
            {

                var comman = new CommonBusiness();
                comman.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 165, string.Format("{0}", "لغو درخواست جلسه دفاع توسط دانشکده"), req.ID);
                txtDenyMessage1.Text = string.Empty;
                if (beforeStatus == 2)
                {
                    _requestHandler.SendSMSForDef(stcode.ToString(), 2);
                }
                else
                {
                    _requestHandler.SendSMSForDef(stcode.ToString(), 0);
                }
                //sadegh saryazdy delete link
                 DeleteMeeting(stcode.ToString());
                _requestHandler.UpdateRequest_LinkMeeting(req.ID.ToString(), "");

                //
                txtDenyMessage1.Text = string.Empty;
                string denyMessage = "درخواست شماره " + req.ID.ToString() + " لغو گردید.";
                RadWindowManager1.RadAlert(denyMessage, 300, 100, "پیام سیستم", "closeRadWindow3");

            }
            else
            {
                string denyMessage = "درخواست شماره " + req.ID.ToString() + " لغو گردید.";
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

       
        public void DeleteMeeting(string stCode)
        {
       
            RequestHandler reqH = new RequestHandler();
            AdobeConnectDTO adobeConnectDTO = new AdobeConnectDTO();
            LoginBusiness lgb = new LoginBusiness();
            AdobeDefenceBusiness adobeDefenceBusiness = new AdobeDefenceBusiness();
            LoginDTO stInfo = lgb.Get_StInfo(stCode.ToString());
            const string pass = "4sx0pvauo4nleowu5ugvkkx9l0bpsbe";

            string firtsName = stInfo.Name.Trim() == "" ? "نامشخص" : stInfo.Name;
            string lastName = stInfo.LastName.Trim() == "" ? "نامشخص" : stInfo.LastName;
            DataTable dt = reqH.GetMeetingDefencesAStudentByStcodeBusinesss(stCode);
            adobeConnectDTO.SetValueDefult(stCode.ToString(), pass, firtsName, lastName);
            if (dt != null && dt.Rows.Count > 0)
            {
                adobeConnectDTO.MeetingUrlPath = "/" + dt.Rows[0]["resLink"].ToString();
                adobeDefenceBusiness.DeleteMeeting(adobeConnectDTO);
            }

         
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {

        }





        protected void btnScore_Click(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            GridDataItem data = (GridDataItem)btn.NamingContainer;
         
            hdnRequetIdScore.Value = data["ID"].Text;
            var score = _requestHandler.GetScoreForDefence(int.Parse(data["ID"].Text)).Score;
            if(score!=null&&score>=0&&score<=20)
            {
                txtScore.Text = score.ToString().Replace("/",".");
            }
            else
            {
                txtScore.Text = "";
            }

            string scrp = "function f(){$find(\"" + rdwInsertScore.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);
        }

        protected void btnInsertScore_Click(object sender, EventArgs e)
        {

            decimal number;
            var style = NumberStyles.None|NumberStyles.AllowDecimalPoint;
   
            if (hdnRequetIdScore.Value == null)
            {
                lblScoreValid.Text = "سیستم دچار اختلال شده است";
                lblScoreValid.Visible = true;

            }
            else if (txtScore.Text == "")
            {
                lblScoreValid.Text = "نمره را وارد نمایید";
                lblScoreValid.Visible = true;
            }
            
            else if (!decimal.TryParse(txtScore.Text, style, new CultureInfo("en-US"), out number) ||
                   decimal.Parse(txtScore.Text,new CultureInfo("en-US")) < 0 || decimal.Parse(txtScore.Text, new CultureInfo("en-US")) > 20)
            {
                lblScoreValid.Text = "فرمت صحیح نمره را وارد نمایید";
                lblScoreValid.Visible = true;
            }
            else
            {
                ScoreDefence score = new ScoreDefence();
                score.RequestId = int.Parse(hdnRequetIdScore.Value);
                score.Score = decimal.Parse(txtScore.Text, new CultureInfo("en-US"));
                if (_requestHandler.UpdateScoreForDefence(score))
                {
                    var userID = Convert.ToInt32(Session[sessionNames.userID_Karbar]);
                    lblScoreValid.Visible = false;
                    var comman = new CommonBusiness();
                    comman.InsertIntoUserLog(userID, DateTime.Now.ToString("HH:mm"), 11, 250,  "درج نمره دفاع توسط دانشکده", score.RequestId);
                    string Message = "نمره مدنظر شما برای دفاع  با شماره  " + score.RequestId + " ثبت گردید.";
                    RadWindowManager1.RadAlert(Message, 300, 100, "پیام سیستم", "closeRadWindow4");
              
                }
                else
                {
                    lblScoreValid.Visible = false;
                    string Message = "سیستم دچار اختلال گردید";
                    RadWindowManager1.RadAlert(Message, 300, 100, "پیام سیستم", "closeRadWindow4");
                }
            }
            
           
        }

        protected void chkSelectUnit_CheckedChanged(object sender, EventArgs e)
        {
           
         
        }

        //protected void chkSelectUnit_CheckedChanged1(object sender, EventArgs e)

        //{
        //    foreach (DataGridViewRow row in grdDefenceList.row)
        //    {
        //        dataGridView1.Rows[row.Index].SetValues(true);
        //    }
        //}

        protected void grdDefenceList_SelectedCellChanged(object sender, EventArgs e)
        {
         
        }

        //protected void btnSaveReport_OnClick(object sender, EventArgs e)
        //{
        //    var report = CreateDefenceSessionReport(hdnRequestId.Value, hdnStudentCode.Value);
        //    StiWebViewer1.Visible = true;

        //    StiReportResponse.ResponseAsPdf(this, report);

        //}

        //protected void btnPrintReport_OnClick(object sender, EventArgs e)
        //{
        //    var report = CreateDefenceSessionReport(hdnRequestId.Value, hdnStudentCode.Value);
        //    StiWebViewer1.Report = report;
        //    StiWebViewer1.Visible = true;

        //    report.Print(showPrintDialog: false);

        //}
    }
}