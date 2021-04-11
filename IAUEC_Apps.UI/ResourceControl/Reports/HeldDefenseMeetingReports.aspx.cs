using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.ResourceControlClasses;
using IAUEC_Apps.UI.University.GraduateAffair.CMS;
using ResourceControl.BLL;
using Telerik.Web.UI;

namespace IAUEC_Apps.UI.ResourceControl.Reports
{
    public partial class HeldDefenseMeetingReports : System.Web.UI.Page
    {
        private RequestHandler _requestHandler = new RequestHandler();

        private string _daneshId;
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
            }

            _daneshId = Session["DaneshId"].ToString();
            if (_daneshId != 0.ToString())
            {
                foreach (ListItem item in CollegeIddrp.Items)
                {
                    if (_daneshId != item.Value)
                        item.Enabled = false;
                }
            }
        }

        protected void SearchBtn_OnClick(object sender, EventArgs e)
        {
            grdResualtList.Visible = true;
            grdResualtList.DataSource = null;
            grdResualtList.Rebind();

        }

        protected void ExcleExportBtn_OnClick(object sender, EventArgs e)
        {

            var allAcceptedRequest = _requestHandler.GetHeldDefenseMeetingReports(Convert.ToInt32(CollegeIddrp.SelectedValue), StartDateTxt.Text, EndDateTxt.Text, Convert.ToInt32(drpDateType.SelectedValue));

            if (allAcceptedRequest.Rows.Count <= 0) return;
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

        protected void ClearBtn_OnClick(object sender, EventArgs e)
        {
            StartDateTxt.Text = "";
            EndDateTxt.Text = "";
            CollegeIddrp.SelectedValue = 0.ToString();
            grdResualtList.Visible = false;
        }

        protected void grdResualtList_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdResualtList.DataSource = _requestHandler.GetHeldDefenseMeetingReports(Convert.ToInt32(CollegeIddrp.SelectedValue),
                  StartDateTxt.Text, EndDateTxt.Text, Convert.ToInt32(drpDateType.SelectedValue));

            GridFilterMenu menu = grdResualtList.FilterMenu;
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

            CurrentPage.CurrentPageNumberValue = grdResualtList.MasterTableView.CurrentPageIndex;
            CurrentPage.PageSizeValue = grdResualtList.MasterTableView.PageSize;


        }

        public static class CurrentPage
        {
            public static int CurrentPageNumberValue { get; set; }
            public static int PageSizeValue { get; set; }
        }

        protected void btnShowDetail_OnClick(object sender, EventArgs e)
        {
                        #region ShowInfo

            Button btn = (Button)sender;
            //GridDataItem data = (GridDataItem)btn.NamingContainer;
            //var id = Convert.ToInt32(data["ID"].Text);
            var id = Convert.ToInt32(btn.CommandArgument);
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
                    if(requestDetails.DateTimeRange.Any(x=>x.Date.ToGregorian()< DateTime.Now))
                        lblStatue.Text = "جلسه دفاع برگزار گردید";
                    else
                    lblStatue.Text = "درخواست توسط اداری تایید گردید";
                    break;
                case 3:
                    lblStatue.Text = "درخواست رد شد و برای بررسی به کارتابل دانشکده ارجاع داده شد";
                    break;
                case 6:
                    // lblStatue.Text = "درخواست توسط فنی جهت پخش آنلاین تایید گردید";
                    lblStatue.Text = "درخواست توسط فنی جهت برگزاری آنلاین تایید گردید";
                    break;



            }
            //lblTozieh.Text = requestDetails.Note;

            //if (!string.IsNullOrEmpty(requestDetails.Answer_time))
            //{
            //    if (requestDetails.Status == 2)
            //    {
            //        lblheader.Text = "زمان پاسخ به درخواست:";

            //    }
            //    else
            //    {
            //        lblheader.Text = "زمان رد درخواست:";
            //        litDenyNot.Text = "علت رد درخواست:";
            //    }
            //    lblheader.Visible = true;
            //    lblDateOfResponse.Visible = true;
            //    lblDateOfResponse.Text = requestDetails.Answer_time;
            //    if (requestDetails.Status != 2)
            //    {
            //        litDenyNot.Visible = true;
            //        lblDenyNot.Visible = true;
            //        lblDenyNot.Text = requestDetails.Answernote;
            //    }
            //}
            //else
            //{
            //    lblheader.Visible = false;
            //    lblDateOfResponse.Visible = false;
            //    litDenyNot.Visible = false;
            //    lblDenyNot.Visible = false;
            //}
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
                tdGrdResult.Visible = false;

            }

            string scrp = "function f(){$find(\"" + RadWindow3.ClientID +
                          "\").show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(this.Page, GetType(), ClientID, scrp, true);


            #endregion

        }

        protected void btnHistory_OnClick(object sender, EventArgs e)
        {
            RequestHandler _reqHandler = new RequestHandler();
            CommonBusiness cmb = new CommonBusiness();
            Button btn = (Button)sender;

            string id = btn.CommandArgument;

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
    }
}