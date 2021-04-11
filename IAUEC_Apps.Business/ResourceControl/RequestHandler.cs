using IAUEC_Apps.Business.Common;
using IAUEC_Apps.Business.Conatct.Functions;
using IAUEC_Apps.DAO.ResourceControl;
using IAUEC_Apps.DTO.ResourceControlClasses;
using ResourceControl.DAL;
using ResourceControl.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Security.Policy;

namespace ResourceControl.BLL
{
    public class RequestHandler
    {
        RequestDBAccess requestDB = null;

        CommonBusiness cmb = new CommonBusiness();

        private int opt;
        public TimeSpan[] GetTime()
        {

            return new TimeSpan[] {
                 // new TimeSpan(8, 0, 0)
                //, 
                new TimeSpan(9, 0, 0)
               // , new TimeSpan(10, 0, 0)
                , new TimeSpan(11, 0, 0)
               // , new TimeSpan(12, 0, 0)
               // , new TimeSpan(13, 0, 0)
                , new TimeSpan(14, 0, 0)
                //, new TimeSpan(15, 0, 0)
                , new TimeSpan(16, 0, 0)
               // , new TimeSpan(17, 0, 0)
               // , new TimeSpan(18, 0, 0)
               // , new TimeSpan(19, 0, 0)
            };

        }
        public RequestHandler()
        {
            requestDB = new RequestDBAccess();
        }

        public List<RequestFR> GetRequestListOfTermJari()
        {
            return requestDB.GetRequestListOfTermJari();
        }
        public List<RequestFR> GetDeletedRequestListOfTermJari()
        {
            return requestDB.GetDeletedRequestListOfTermJari();
        }
        public List<RequestFR> GetOstadListinTerm()
        {
            return requestDB.GetOstadListinTerm();
        }

        public DataTable FindBedehkarForReserve(string stcode)
        {
            return requestDB.FindBedehkarForReserve(stcode);
        }
        public DataTable GetFinancialPermissionCondition(string stcode)
        {
            return requestDB.GetFinancialPermissionCondition(stcode);
        }
        public bool HasFinancialPermission(string stcode)
        {
            return requestDB.HasFinancialPermission(stcode);
        }


        public DataTable GetStRegisterd2(string stcode)
        {
            return requestDB.GetStRegisterd2(stcode);
        }

        public List<RequestFR> GetRequestListByIssuerID(int issuerID)
        {
            return requestDB.GetRequestListByIssuerID(issuerID);
        }
        public List<RequestFR> GetRequestListByIssuerIDAndStatus(int issuerID, int status)
        {
            return requestDB.GetRequestListByIssuerIDAndStatus(issuerID, status);
        }

        public List<RequestFR> GetRequestListBystatusForAdmin(int status)
        {
            return requestDB.GetRequestListBystatusForAdmin(status);
        }
        public List<RequestFR> GetRequestListByStatus(int status)
        {
            return requestDB.GetRequestListBystatus(status);
        }
        public List<RequestFR> GetStudentRequestListForStudentOffice(int status)
        {
            return requestDB.GetStudentRequestListForStudentOffice(status);
        }

        public List<StudentDefenceRequestDTO> GetStudentRequestListForTechnical()
        {
            return requestDB.GetStudentRequestListForTechnical();
        }

        public List<RefereeInformation> GetRefereeTeachersPaymentHasDown(string term = null)
        {
            return requestDB.GetRefereeTeachersPaymentHasDown(term);
        }

        public DataTable GetStudentDefenceListByProfossorCode(decimal profossorId = -1, int requestId = -1)
        {
            return requestDB.GetStudentDefenceListByProfossorCode(profossorId, requestId);
        }

        public bool UpdateRequest_RejectReason(int requestId = -1, string rejectText = null)
        {
            return requestDB.UpdateRequest_RejectReason(requestId, rejectText);
        }


        public List<StudentDefenceRequestDTO> GetStudentDefenceListForPazhoohesh(int isReport = 0, string term = null)
        {
            return requestDB.GetStudentDefenceListForPazhoohesh(isReport, term);
        }
        public List<RefereeInformation> GetRefereeTeachersPaymentHasNotDown(string term = null)
        {
            return requestDB.GetRefereeTeachersPaymentHasNotDown(term);
        }

        public DataTable GetAllTermsForDefence(string term = null)
        {
            return requestDB.GetAllTermsForDefence(term);
        }

        public DataTable GetAllMartabeAndWage()
        {
            return requestDB.GetAllMartabeAndWage();
        }

        public DataTable GetRefereeTeachersPaymentHasDone_Report(int isReport = 0, string term = null)
        {
            return requestDB.GetRefereeTeachersPaymentHasDone_Report(isReport, term);
        }

        public DataTable GetRefereeTeachersPayment_Report(int isPayedWage = 0, int reportType = 0, string term = null)
        {
            return requestDB.GetRefereeTeachersPayment_Report(isPayedWage, reportType, term);
        }


        public List<StudentDefenceRequestDTO> GetStudentRequestListForTraining()
        {
            return requestDB.GetStudentRequestListForTraining();
        }
        public List<StudentDefenceRequestDTO> GetStudentRequestListForOffice()
        {
            return requestDB.GetStudentRequestListForOffice();
        }
        public List<StudentDefenceRequestDTO> GetStudentRequestListForEducation(int daneshId)
        {
            return requestDB.GetStudentRequestListForEducation(daneshId);
        }


        public decimal GetTotalAverageByStudentCode(string stdcode)
        {
            return requestDB.GetTotalAverageByStudentCode(stdcode);
        }

        public List<FinancialPermission> GetFinancialPermission()
        {
            return requestDB.GetFinancialPermission();
        }
        public int AddOrUpdateFinancialPermission(FinancialPermission student)
        {
            return requestDB.AddOrUpdateFinancialPermission(student);
        }
        public int UpdateRequest(RequestFR request)
        {
            return requestDB.UpdateRequest(request);
        }
        public int UpdateStudentRequestDB(StudentDefenceRequest request)
        {

            return requestDB.UpdateStudentRequestDB(request);
        }
        public int UpdateStudentRequestDBV2(StudentDefenceRequest request)
        {

            return requestDB.UpdateStudentRequestDBV2(request);
        }
        public int UpdateStudentRequestWithEducation(StudentDefenceRequest request)
        {

            return requestDB.UpdateStudentRequestForEducation(request);
        }

        public bool DeleteStudentRequest(int id)
        {

            return requestDB.DeleteStudentRequest(id);
        }
        public bool IsEditedStudentRequest(int id)
        {

            return requestDB.IsEditedStudentRequest(id);
        }

        public RequestFR GetRequestDetails(int reqID)
        {
            return requestDB.GetRequestDetails(reqID);
        }
        public DataTable GetResourceStateForDefence(int reqID)
        {
            return requestDB.GetResourceStateForDefence(reqID);
        }

        public DataTable GetRequestByUserIDandType(int userID, int type)
        {
            return requestDB.GetRequestByUserIDandType(userID, type);
        }
        public DataTable GetAllRequestByTypeAndDate(int type, string sDate, string eDate)
        {
            return requestDB.GetAllRequestByTypeAndDate(type, sDate, eDate);
        }

        public bool DeleteRequest(int reqID)
        {
            try
            {
                Req_Opt_JuncHandler rqH = new Req_Opt_JuncHandler();
                rqH.DeleteReq_Opt_Junc(reqID);
            }
            catch (Exception)
            {
                throw;
            }
            return requestDB.DeleteRequest(reqID);
        }

        public int AddNewRequest(RequestFR request, List<Option> optlist, string userID)
        {
            try
            {
                int id = requestDB.AddNewRequest(request);
                if (id > 0)
                {
                    RequestDateTimeHandler ReqDThandler = new RequestDateTimeHandler();
                    for (int i = 0; i < request.DateTimeRange.Count; i++)
                    {
                        request.DateTimeRange[i].RequestId = id;
                    }
                    ReqDThandler.AddDateTimeOfRequest(request.DateTimeRange);

                    foreach (var item in optlist)
                    {
                        Req_Opt_Junc req_opt = new Req_Opt_Junc();
                        req_opt.Req_id = id;
                        req_opt.Opt_id = item.ID;
                        //req_opt.IsActive = item.IsActive;
                        Req_Opt_JuncHandler rqopt = new Req_Opt_JuncHandler();
                        rqopt.AddNewReq_Opt_Junc(req_opt);
                    }

                    return id;
                }
                else
                {
                    throw new Exception("خطا در هنگام ثبت درخواست ");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int AddNewRequestForDefence(RequestFR request, List<Option> optlist, string userID)
        {
            try
            {
                int id = requestDB.AddNewDefenceRequest(request);
                if (id > 0)
                {
                    RequestDateTimeHandler ReqDThandler = new RequestDateTimeHandler();
                    for (int i = 0; i < request.DateTimeRange.Count; i++)
                    {
                        request.DateTimeRange[i].RequestId = id;
                    }
                    ReqDThandler.AddDateTimeOfRequest(request.DateTimeRange);

                    foreach (var item in optlist)
                    {
                        Req_Opt_Junc req_opt = new Req_Opt_Junc();
                        req_opt.Req_id = id;
                        req_opt.Opt_id = item.ID;
                        //req_opt.IsActive = item.IsActive;
                        Req_Opt_JuncHandler rqopt = new Req_Opt_JuncHandler();
                        rqopt.AddNewReq_Opt_Junc(req_opt);
                    }

                    return id;
                }
                else
                {
                    throw new Exception("خطا در هنگام ثبت درخواست ");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int AddNewRequestForDefenceV2(RequestFR request, string userID)
        {
            try
            {
                int id = requestDB.AddNewDefenceRequest(request);
                if (id > 0)
                {
                    RequestDateTimeHandler ReqDThandler = new RequestDateTimeHandler();
                    foreach (var t in request.DateTimeRange)
                    {
                        t.RequestId = id;

                    }
                    ReqDThandler.AddDateTimeOfRequestV2(request.DateTimeRange.FirstOrDefault(), request.DaneshID);

                    return id;
                }
                else
                {
                    throw new Exception("خطا در هنگام ثبت درخواست ");
                }
            }
            catch (Exception exception)
            {

                throw;
            }
        }


        public List<RequestFR> GetRequestListBySessionDate_resID_status(string sessiondate, int resID, int status)
        {
            return requestDB.GetRequestListBySessionDate_resID_status(sessiondate, resID, status);
        }

        public List<RequestFR> GetRequestListBystatusAnddaneshID(int status, int daneshID)
        {
            return requestDB.GetRequestListBystatusAnddaneshID(status, daneshID);
        }
        public List<RequestFR> GetRequestListBystatusAnddaneshIDForDefence(int status, int daneshID)
        {
            return requestDB.GetRequestListBystatusAnddaneshIDForDefence(status, daneshID);
        }


        public List<RequestFR> GetDeletedRequestListByDaneshID(int daneshID)
        {
            return requestDB.GetDeletedRequestListByDaneshID(daneshID);
        }

        //public List<RequestFR> GetRequestListBystatusAndIssuerID(int status, int IssuerID)
        //{
        //    return requestDB.GetRequestListBystatusAndIssuerID(status, IssuerID);
        //}

        public List<RequestFR> GetRequestListByDaneshID(int daneshID)
        {
            return requestDB.GetRequestListByDaneshID(daneshID);
        }

        public int EditRequest(RequestFR req, List<Option> optlist)
        {
            try
            {
                foreach (var item in optlist)
                {
                    Req_Opt_Junc req_opt = new Req_Opt_Junc();
                    req_opt.Req_id = req.ID;
                    req_opt.Opt_id = item.ID;
                    req_opt.IsActive = item.IsActive;
                    Req_Opt_JuncHandler rqopt = new Req_Opt_JuncHandler();
                    rqopt.UpdateReq_Opt_Junc(req_opt);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return requestDB.UpdateRequest(req);
        }

        public DataTable GetRequestListBySessionDateResID(string date, int resId)
        {
            return requestDB.GetRequestListBySessionDateResID(date, resId);
        }



        //public List<RequestFR> GetRequestListByStatusAfterToday(int status)
        //{
        //    string emrooz = DateTime.Now.ToPeString();
        //    return requestDB.GetRequestListByStatusAfterToday(status, emrooz);
        //}

        public DataTable HasRequestBefore(int userID, List<RequestDateTime> dateTimeList)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(RequestDateTime));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (var item in dateTimeList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return requestDB.CheckRequestConflict(userID, table);

        }



        public mainViewModel GetRequestCountByStatusForAdmin()
        {
            return requestDB.GetRequestCountByStatusForAdmin();
        }
        public mainViewModel GetRequestCountByStatusForAdminForDefence()
        {
            return requestDB.GetRequestCountByStatusForAdminForDefence();
        }
        public mainViewModel GetRequestCountByStatus()
        {
            return requestDB.GetRequestCountByStatus();
        }
        public mainViewModel GetRequestCountByStatusForDefence()
        {
            return requestDB.GetRequestCountByStatusForDefence();
        }
        public mainViewModel GetRequestCountByStatusAndDaneshId(int daneshId)
        {
            return requestDB.GetRequestCountByStatusAndDaneshId(daneshId);
        }
        public mainViewModel GetRequestCountByStatusAndDaneshIdForDefence(int daneshId)
        {
            return requestDB.GetRequestCountByStatusAndDaneshIdForDefence(daneshId);
        }
        public mainViewModel GetDefenceRequestCountByLocationForEducation(int daneshId)
        {
            return requestDB.GetDefenceRequestCountByLocationForEducation(daneshId);
        }

        public mainViewModel GetDefenceRequestCountByLocationForTechnical()
        {
            return requestDB.GetDefenceRequestCountByLocationForTechnical();
        }


        public mainViewModel GetRequestCountByStatusAndIssuerId(int IssuerId)
        {
            return requestDB.GetRequestCountByStatusAndIssuerId(IssuerId);
        }
        public mainViewModel GetRequestCountByStatusAndIssuerIdForDefence(int IssuerId)
        {
            return requestDB.GetRequestCountByStatusAndIssuerIdForDefence(IssuerId);
        }
        public List<RequestFR> GetRequestListByDID(int did)
        {
            return requestDB.GetRequestListByDID(did);
        }

        public int DenyRequest(RequestFR req)
        {
            return requestDB.DenyRequest(req);
        }


        public List<RequestFR> GetRequestListByStatusAndRoleId(int status, int roleId)
        {
            int locationId = 0;
            if (roleId == 37 || roleId == 38)
            {
                locationId = 1;
            }
            else if (roleId == 39 || roleId == 40)
            {
                locationId = 2;
            }
            else
            {
                throw new Exception("Invalid RoleId");
            }

            return requestDB.GetRequestListByStatusAndLocationId(status, locationId);
        }

        public List<RequestFR> GetRequestListByRoleId(int roleId)
        {
            if (roleId == 37 || roleId == 38)
            {
                return requestDB.GetRequestListByLocationId(1);

            }
            else if (roleId == 39 || roleId == 40)
            {
                return requestDB.GetRequestListByLocationId(2);
            }
            else
            {
                throw new Exception("Invalid RoleId");
            }
        }

        internal int UpdateRequestStatus(int reqId, int status, string userID)
        {
            switch (status)
            {
                case 1:
                    cmb.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 11, 115, "تایید آموزش درخواست رزرواسیون", reqId);
                    break;
                case 2:
                    cmb.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 11, 117, "تایید اداری درخواست رزرواسیون", reqId);
                    break;
            }
            return requestDB.UpdateRequestStatus(reqId, status);
        }
        internal int UpdateRequestStatus(int reqId, int status, string userID, int roleId)
        {
            switch (status)
            {
                case 1:
                    cmb.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 11, 115, "تایید آموزش درخواست رزرواسیون", reqId);
                    break;
                case 2:

                    cmb.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 11, 117, "تایید اداری درخواست رزرواسیون", reqId);
                    break;
            }

            return requestDB.UpdateRequestStatus(reqId, status);
        }
        public mainViewModel GetRequestCountByLocation(int location_id)
        {
            return requestDB.GetRequestCountByLocation(location_id);

        }

        public int InformUserOfRequestByReqId(int reqId, string userID)
        {
            //send sms or email , lms post ,...
            SendSms(userID, reqId);
            cmb.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 11, 118, "اطلاع رسانی درخواست رزرواسیون", reqId);

            return requestDB.UpdateRequestStatus(reqId, (int)RequestStatus.informed);
        }

        public DataTable GetRequestListBySessionDateResID1(string p, int resID)
        {
            return requestDB.GetRequestListBySessionDateResID(p, resID);
        }

        public static string ToReadableString(TimeSpan span)
        {
            string formatted = string.Format("{0}{1}{2}{3}",
                span.Duration().Days > 0 ? string.Format("{0:0} day{1}, ", span.Days, span.Days == 1 ? String.Empty : "s") : string.Empty,
                span.Duration().Hours > 0 ? string.Format("{0:0} hour{1}, ", span.Hours, span.Hours == 1 ? String.Empty : "s") : string.Empty,
                span.Duration().Minutes > 0 ? string.Format("{0:0} minute{1}, ", span.Minutes, span.Minutes == 1 ? String.Empty : "s") : string.Empty,
                span.Duration().Seconds > 0 ? string.Format("{0:0} second{1}", span.Seconds, span.Seconds == 1 ? String.Empty : "s") : string.Empty);

            if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted)) formatted = "0 seconds";

            return formatted;
        }

        public void SendSms(string userID, int reqID)
        {
            CommonBusiness oCommon = new CommonBusiness();
            RequestDBAccess rdba = new RequestDBAccess();
            DataTable dt = new DataTable();
            TimeSpan sTime = new TimeSpan();
            TimeSpan eTime = new TimeSpan();

            string smsBody = "";
            string result = "";
            string className;
            string classDate;
            string location;
            string classLocation;

            dt = rdba.getClassInfo(reqID);
            className = dt.Rows[0]["courseName"].ToString();
            classDate = dt.Rows[0]["Date"].ToString();
            location = dt.Rows[0]["location_name"].ToString();
            classLocation = dt.Rows[0]["ClassName"].ToString();

            sTime = TimeSpan.FromTicks(Convert.ToInt64(dt.Rows[0]["startTime"].ToString()));
            eTime = TimeSpan.FromTicks(Convert.ToInt64(dt.Rows[0]["endTime"].ToString()));

            smsBody = "استاد گرامی کلاس " + className + " شما، در روز " + classDate + " ساعت " + sTime.ToString().Substring(0, 5) + " الی " + eTime.ToString().Substring(0, 5) + " در " + location + " و در " + classLocation + " برگزار میشود";

            //result = oCommon.SendSMSByUserIdAndType(smsBody, userID, 2);
            bool sentSMS; string smsStatus;
            result = oCommon.sendSMS(2, userID, smsBody, out sentSMS, out smsStatus);
        }

        public void UpdateRequestAnswerTimeByIdrequest(int requestId)
        {
            requestDB.UpdateRequestAnswerTimeByIdrequest(requestId);
        }

        public void ActiveSendSMSFlag(int requestId)
        {
            requestDB.ActiveSendSMSFlag(requestId);
        }


        public List<RequestFR> GetRequestListByStatusAndRoleIdForEdari(int status, int roleId)
        {
            int locationId = 0;
            if (roleId == 37 || roleId == 38)
            {
                locationId = 1;
            }
            else if (roleId == 39 || roleId == 40)
            {
                locationId = 2;
            }
            else
            {
                throw new Exception("Invalid RoleId");
            }

            return requestDB.GetRequestListByStatusAndLocationIdForEdari(status, locationId);
        }

        public mainViewModel GetRequestCountByLocationForEdari(int locationId)
        {
            return requestDB.GetRequestCountByLocationForEdari(locationId);
        }
        public mainViewModel GetRequestCountByLocationForEdariForDefence(int locationId)
        {
            return requestDB.GetRequestCountByLocationForEdariForDefence(locationId);
        }

        public mainViewModel GetDefenceRequestCountByLocationForEdari(int locationId)
        {
            return requestDB.GetDefenceRequestCountByLocationForEdari(locationId);
        }

        public bool IsThisTerm(string issuerDate, string requestDate, int appId)
        {
            var currentTermDateRange = GeDateRangeOfTerm(appId);
            if (currentTermDateRange.FirstOrDefault(x => x.Key == "start").Value == "ناموجود")
            {
                return false;
            }
            else
            {
                return currentTermDateRange["start"].ToGregorian() <= issuerDate.ToGregorian()
                       && currentTermDateRange["start"].ToGregorian() <= requestDate.ToGregorian()
                       && currentTermDateRange["end"].ToGregorian() >= issuerDate.ToGregorian()
                       && currentTermDateRange["end"].ToGregorian() >= requestDate.ToGregorian();
            }
        }
        public bool IsThisTerm2(string issuerDate, string requestDate, int appId)
        {
            var currentTermDateRange = GeDateRangeOfTerm2(appId);
            return currentTermDateRange["start"].ToGregorian() <= issuerDate.ToGregorian()
                   && currentTermDateRange["start"].ToGregorian() <= requestDate.ToGregorian()
                   && currentTermDateRange["end"].ToGregorian() >= issuerDate.ToGregorian()
                   && currentTermDateRange["end"].ToGregorian() >= requestDate.ToGregorian();
        }

        public string GetCurrentTerm()
        {
            return requestDB.GetCurrentTerm();
        }

        public Dictionary<string, string> GeDateRangeOfTerm(int appId)
        {
            var currentTerm = GetCurrentTerm();
            return requestDB.GeDateRangeOfTerm(appId, currentTerm);

        }
        public Dictionary<string, string> GeDateRangeOfTerm2(int appId)
        {

            return requestDB.GeDateRangeOfTerm2(appId);

        }



        public string CreateStudentRequest(out int requestId, StudentDefenceRequest request)
        {
            var message = "ok";
            var tempRequest = new RequestFR
            {
                CatID = request.CategoryId,
                Subject = request.Subject,
                Note = request.Description,
                Location = request.Location,
                Status = request.Status,
                IssuerID = request.IssuerId,
                IssuerName = request.IssuerName,
                Capacity = request.Capacity,
                CourseName = request.DefenceSubject,
                DaneshID = request.DaneshId,
                //CourseDID = request.CourseId,
                Issue_time = DateTime.Now.ToPeString(),


            };
            /****************/

            var msg0 = IsAvoidTime(request.RequestStartTime, request.RequestDate, request);
            if (msg0 != null)
            {
                requestId = 0;
                return msg0;
            }

            var issuerDate = DateTime.Now.Date;
            if (!IsThisTerm2(issuerDate.ToPeString(), request.RequestDate, 11)) //11 = appId
            {
                var currentTermDateRange = GeDateRangeOfTerm2(11); //11 = appId
                requestId = 0;
                return
                    $" امکان ثبت درخواست در ترم جاری در بازه زمانی {currentTermDateRange["start"]} تا {currentTermDateRange["end"]} می باشد";
            }

            if (request.RequestDate.ToGregorian().DayOfWeek == DayOfWeek.Friday || request.RequestDate.ToGregorian().DayOfWeek == DayOfWeek.Thursday)
            {
                requestId = 0;
                return "امکان برگزاری جلسه دفاع در روزهای پنجشنبه و جمعه وجود ندارد";
            }

            //if (!string.IsNullOrEmpty(request.OnlineTeacherRole) || request.IsEquippingResource)
            //    if (request.RequestDate.ToGregorian().DayOfWeek == DayOfWeek.Monday && request.RequestStartTime < 468000000000)
            //    {
            //        requestId = 0;
            //        return "دانشجوی گرامی امکان برگزاری جلسه دفاع آنلاین یا پخش  در روزهای دوشنبه قبل از ساعت 13 وجود ندارد";
            //    }
            if (CheckReqDate(request.RequestDate))
            {
                RequestDateTime requestDateTime = new RequestDateTime
                {
                    Date = request.RequestDate,
                    StartTime = request.RequestStartTime,
                    EndTime = request.RequestEndTime
                };
                tempRequest.DateTimeRange = new List<RequestDateTime> { requestDateTime };
            }
            else
            {
                message = "تاریخ درخواست باید بعد از تاریخ امروز باشد.";
                requestId = 0;
                return message;
            }


            var optlist = new List<Option>();

            RequestHandler requestHandler = new RequestHandler();




            //if (CheckBeforeDefenceRequest(request.RequestDate, request.RequestStartTime, request.RequestEndTime, request.IssuerId))
            //{
            //    message = "تداخل در ساعت و تاریخ درخواست ، با ساعات درخواست های پیشین این دانشجو وجود دارد.";
            //    requestId = 0;
            //    return message;
            //}


            if (HasAnotherRequestInThisTerm(request.IssuerId))
            {
                message = "به دلیل داشتن درخواست در حال گردش امکان ثبت وجود ندارد";
                requestId = 0;
                return message;
            }

            if (request.AcceptPropDate.Length < 6)
            {
                message = "به دلیل عدم ثبت تاریخ تصویب پروپوزال در گروه امکان ثبت درخواست وجود ندارد، لطفا به کارشناس پژوهش تیکت بزنید";
                requestId = 0;
            }
            var acceptDateSplit = request.AcceptPropDate.Split('/');
            var requestDateSplit = request.RequestDate.Split('/');

            var month = Convert.ToInt32(acceptDateSplit[1]);
            var addToYear = (month + 6) / 12;
            var addToMonth = (month + 6) % 12;

            var year1 = Convert.ToInt32(acceptDateSplit[0]);
            var month1 = Convert.ToInt32(acceptDateSplit[1]);
            var day1 = Convert.ToInt32(acceptDateSplit[2]);
            if (addToYear == 1 && addToMonth == 0)
            {
                addToYear = 0;
                addToMonth = 6;

                month1 += 6;

            }
            else
            {
                if (addToYear == 1 && addToMonth > 0)
                {
                    year1 += addToYear;
                    month1 = addToMonth;
                }
                else
                {
                    year1 += addToYear;
                    month1 = addToMonth;

                }
            }
            if (month1 == 12 && day1 > 29)
            {
                year1 += 1;
                month1 = 1;
                day1 = day1 % 29;
            }
            else
            if (month1 > 6 && day1 > 30)
            {
                month1 += 1;
                day1 = 1;
            }



            var acceptDate = new DateTime(year1, month1, day1, new PersianCalendar());
            var requestDate = new DateTime(Convert.ToInt32(requestDateSplit[0]), Convert.ToInt32(requestDateSplit[1]),
                Convert.ToInt32(requestDateSplit[2]), new PersianCalendar());

            //var date12 = new DateTime(1396, 12, 01, new PersianCalendar());
            //if (requestDate >= date12)
            //{
            //    requestId = 0;
            //    return "دانشجوي محترم حداكثر زمان مجاز جهت برگزاري جلسه دفاع در نيمسال جاري تاريخ 30/11/96 مي باشد.";
            //}



            var diffTimeSpan = (requestDate - acceptDate);
            //var startDatePrevent = new DateTime(1396, 10, 10, new PersianCalendar());
            //var endDatePrevent = new DateTime(1396, 11, 7, new PersianCalendar());
            //if (startDatePrevent <= requestDate && requestDate <= endDatePrevent)
            //{
            //    requestId = 0;
            //    return "دانشجوی گرامی به دلیل برگزاری امتحانات در این تاریخ امکان رزرو جلسه دفاع وجود ندارد.";
            //}


            //var diffDateFor72 = (requestDate.Date - ThreeWorkingDays(DateTime.Now.Date)).TotalDays;

            //if (diffDateFor72 < 0)
            //{
            //    requestId = 0;
            //    return "دانشجوی گرامی، باید فاصله زمانی ثبت درخواست شما با برگزاری جلسه دفاع حداقل 72 ساعت باشد ";
            //}
            double diffDateFor3day;

                 diffDateFor3day = (requestDate.Date - ThreeWorkingDays(DateTime.Now.Date)).TotalDays;

            if (diffDateFor3day < 0)
            {
                requestId = 0;
                return "دانشجوی گرامی، باید فاصله زمانی ثبت درخواست شما با برگزاری جلسه دفاع حداقل 3 روز کاری باشد ";
            }


            if (!(diffTimeSpan.TotalDays >= 1))
            {
                requestId = 0;
                message = "از زمان تصویب پروپوزال شما تا روز دفاع پیشنهادی کمتر از 6 ماه سپری شده است و امکان ثبت زمان به دلیل مغایرت با آیین نامه های آموزشی نمی باشد. جهت کسب اطلاعات بیشتر با مسئول پژوهش دانشکده ارتباط برقرار نمایید.";
                return message;

            }

            var _defenceInformation = GetDefenceInformation(request.IssuerId.ToString());

            Dictionary<string, string> profList = GetProfList(_defenceInformation);

            foreach (var keyValuePair in profList)
            {
                if (IsConflictProfessor(Convert.ToInt32(keyValuePair.Key), request.RequestDate, request.RequestStartTime, request.RequestEndTime))
                {
                    requestId = 0;
                    return $"دانشجوی گرامی برای استاد {keyValuePair.Value} در زمان مذکور جلسه دفاع دیگری رزرو شده است و امکان حضور استاد به صورت همزمان در جلسه دفاع شما وجود ندارد.";
                }
            }



            if (CanAssignmentResource(request.RequestDate, request.RequestStartTime, request.RequestEndTime, request.DaneshId))
            {
                try
                {

                    var requestId1 = requestHandler.AddNewRequestForDefence(tempRequest, optlist, request.UserId);
                    requestId = requestId1;



                    DefenceInformation defenceInformation = new DefenceInformation
                    {
                        StudentCode = request.IssuerId.ToString(),
                        StudentFullName = request.IssuerName,
                        DefenceSubject = request.DefenceSubject,
                        CollegeId = request.DaneshId.ToString(),
                        StartTime = request.RequestStartTime,
                        EndTime = request.RequestEndTime,
                        RequestDate = request.RequestDate,
                        UseOwnPc = request.UseOwnPc,
                        OnlineFirstTeacherId = request.OnlineFirstTeacherId,
                        OnlineSecondTeacherId = request.OnlineSecondTeacherId,
                        OnlineTeacherRole = request.OnlineTeacherRole,
                        RequestId = requestId1.ToString(),
                        IsRequestEducation=request.IsRequestEducation

                    };
                    cmb.InsertIntoDefenceInfo(defenceInformation);

                    //Log
                    //var comman = new CommonBusiness();
                    //comman.InsertIntoUserLog(Convert.ToInt32(request.UserId), "", 11, 114, "ثبت درخواست کلاس ", reqid);



                    message = "درخواست شما با شماره " + requestId1.ToString() + " با موفقیت ثبت گردید";


                    return (string.Concat("ok", message));
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            else
            {
                message = "در زمان انتخاب شده، به دلیل پر بودن ظرفیت کلاس های دفاعِ این دانشکده، امکان برگزاری جلسه دفاع وجود ندارد";
            }
            requestId = 0;
            return message;
        }


        public string CreateStudentRequestV2(out int requestId, StudentDefenceRequest request)
        {
            var message = "ok";
            var tempRequest = new RequestFR
            {
                CatID = request.CategoryId,
                Subject = request.Subject,
                Note = request.Description,
                Location = request.Location,
                Status = request.Status,
                IssuerID = request.IssuerId,
                IssuerName = request.IssuerName,
                Capacity = request.Capacity,
                CourseName = request.DefenceSubject,
                DaneshID = request.DaneshId,
                //CourseDID = request.CourseId,
                Issue_time = DateTime.Now.ToPeString(),


            };

            //var hasOnline = HasOnlineRequest(request.IsEquippingResource, request.OnlineTeacherRole, request.RequestDate);
            //if (hasOnline != "no")
            //{
            //    requestId = 0;
            //    return hasOnline;
            //}
            RequestHandler requestHandler = new RequestHandler();
         
                var msg0 = IsAvoidTime(request.RequestStartTime, request.RequestDate, request);
                if (msg0 != null)
                {
                    requestId = 0;
                    return msg0;
                }
                var msg = HasPreventCertainTime(request.RequestDate);
                if (msg != null && msg.IsForStudent)
                {
                    requestId = 0;
                    return msg.Description;
                }
                var msg1 = HasPreventedFromCurrentDate((int)Applicant.دانشجو);
                if (msg1 != "ok")
                {
                    requestId = 0;
                    return msg1;

                }
                /****************/
                var issuerDate = DateTime.Now.Date;
                if (!IsThisTerm2(issuerDate.ToPeString(), request.RequestDate, 11)&&request.UserId!="99900999") //11 = appId
                {
                    var currentTermDateRange = GeDateRangeOfTerm2(11); //11 = appId
                    requestId = 0;
                    return
                        $" امکان ثبت درخواست در ترم جاری در بازه زمانی {currentTermDateRange["start"]} تا {currentTermDateRange["end"]} می باشد";
                }



                if (request.RequestDate.ToGregorian().DayOfWeek == DayOfWeek.Friday || request.RequestDate.ToGregorian().DayOfWeek == DayOfWeek.Thursday)
                {
                    requestId = 0;
                    return "امکان برگزاری جلسه دفاع در روزهای پنجشنبه و جمعه وجود ندارد";
                }

                //if (!string.IsNullOrEmpty(request.OnlineTeacherRole) || request.IsEquippingResource)
                //    if (request.RequestDate.ToGregorian().DayOfWeek == DayOfWeek.Monday && request.RequestStartTime < 468000000000)
                //    {
                //        requestId = 0;
                //        return "دانشجوی گرامی امکان برگزاری جلسه دفاع آنلاین یا پخش  در روزهای دوشنبه قبل از ساعت 13 وجود ندارد";
                //    }
                if (!CheckReqDate(request.RequestDate))
                {



                    message = "تاریخ درخواست باید بعد از تاریخ امروز باشد.";
                    requestId = 0;
                    return message;
                }





               




                //if (CheckBeforeDefenceRequest(request.RequestDate, request.RequestStartTime, request.RequestEndTime, request.IssuerId))
                //{
                //    message = "تداخل در ساعت و تاریخ درخواست ، با ساعات درخواست های پیشین این دانشجو وجود دارد.";
                //    requestId = 0;
                //    return message;
                //}


                if (HasAnotherRequestInThisTerm(request.IssuerId)&&request.UserId!="99900999")
                {
                    message = "به دلیل داشتن درخواست در حال گردش امکان ثبت وجود ندارد";
                    requestId = 0;
                    return message;
                }

                if (request.AcceptPropDate.Length < 6)
                {
                    message = "به دلیل عدم ثبت تاریخ تصویب پروپوزال در گروه امکان ثبت درخواست وجود ندارد، لطفا به کارشناس پژوهش تیکت بزنید";
                    requestId = 0;
                }
                var acceptDateSplit = request.AcceptPropDate.Split('/');
                var requestDateSplit = request.RequestDate.Split('/');

                var month = Convert.ToInt32(acceptDateSplit[1]);
                var addToYear = (month + 6) / 12;
                var addToMonth = (month + 6) % 12;

                var year1 = Convert.ToInt32(acceptDateSplit[0]);
                var month1 = Convert.ToInt32(acceptDateSplit[1]);
                var day1 = Convert.ToInt32(acceptDateSplit[2]);
                if (addToYear == 1 && addToMonth == 0)
                {
                    addToYear = 0;
                    addToMonth = 6;

                    month1 += 6;

                }
                else
                {
                    if (addToYear == 1 && addToMonth > 0)
                    {
                        year1 += addToYear;
                        month1 = addToMonth;
                    }
                    else
                    {
                        year1 += addToYear;
                        month1 = addToMonth;

                    }
                }
                if (month1 == 12 && day1 > 29)
                {
                    year1 += 1;
                    month1 = 1;
                    day1 = day1 % 29;
                }
                else
                if (month1 > 6 && day1 > 30)
                {
                    month1 += 1;
                    day1 = 1;
                }



                var acceptDate = new DateTime(year1, month1, day1, new PersianCalendar());
                var requestDate = new DateTime(Convert.ToInt32(requestDateSplit[0]), Convert.ToInt32(requestDateSplit[1]),
                    Convert.ToInt32(requestDateSplit[2]), new PersianCalendar());

                //var date12 = new DateTime(1396, 12, 01, new PersianCalendar());
                //if (requestDate >= date12)
                //{
                //    requestId = 0;
                //    return "دانشجوي محترم حداكثر زمان مجاز جهت برگزاري جلسه دفاع در نيمسال جاري تاريخ 30/11/96 مي باشد.";
                //}



                var diffTimeSpan = (requestDate - acceptDate);
                //var startDatePrevent = new DateTime(1396, 10, 10, new PersianCalendar());
                //var endDatePrevent = new DateTime(1396, 11, 7, new PersianCalendar());
                //if (startDatePrevent <= requestDate && requestDate <= endDatePrevent)
                //{
                //    requestId = 0;
                //    return "دانشجوی گرامی به دلیل برگزاری امتحانات در این تاریخ امکان رزرو جلسه دفاع وجود ندارد.";
                //}


                //var diffDateFor72 = (requestDate.Date - ThreeWorkingDays(DateTime.Now.Date)).TotalDays;

                //if (diffDateFor72 < 0)
                //{
                //    requestId = 0;
                //    return "دانشجوی گرامی، باید فاصله زمانی ثبت درخواست شما با برگزاری جلسه دفاع حداقل 72 ساعت باشد ";
                //}
                double diffDateFor3day;

                diffDateFor3day = (requestDate.Date - ThreeWorkingDays(DateTime.Now.Date)).TotalDays;

                if (diffDateFor3day < 0)
                {
                    requestId = 0;
                    return "دانشجوی گرامی، باید فاصله زمانی ثبت درخواست شما با برگزاری جلسه دفاع حداقل 3 روز کاری باشد ";
                }

                if (!(diffTimeSpan.TotalDays >= 1))
                {
                    requestId = 0;
                    message = "از زمان تصویب پروپوزال شما تا روز دفاع پیشنهادی کمتر از 6 ماه سپری شده است و امکان ثبت زمان به دلیل مغایرت با آیین نامه های آموزشی نمی باشد. جهت کسب اطلاعات بیشتر با مسئول پژوهش دانشکده ارتباط برقرار نمایید.";
                    return message;

                }

                var _defenceInformation = GetDefenceInformation(request.IssuerId.ToString());

                var refereeParticipatingOtherDefensesSameDateMsg = GetRefereeParticipatingOtherDefensesSameDate(
                    _defenceInformation.FirstRefereeFullName, _defenceInformation.SecondRefereeFullName
                    , _defenceInformation.FirstRefereeId,
                    _defenceInformation.SecondRefereeId, request.RequestDate);

                if (refereeParticipatingOtherDefensesSameDateMsg != "ok")
                {
                    requestId = 0;
                    return refereeParticipatingOtherDefensesSameDateMsg;

                }


                Dictionary<string, string> profList = GetProfList(_defenceInformation);

                foreach (var keyValuePair in profList)
                {
                    if (IsConflictProfessor(Convert.ToInt32(keyValuePair.Key), request.RequestDate, request.RequestStartTime, request.RequestEndTime))
                    {
                        requestId = 0;
                        return $"دانشجوی گرامی برای استاد {keyValuePair.Value} در زمان مذکور جلسه دفاع دیگری رزرو شده است و امکان حضور استاد به صورت همزمان در جلسه دفاع شما وجود ندارد.";
                    }
                }
            


            if (CanAssignmentResourceV2(request.RequestDate, request.RequestStartTime, request.RequestEndTime, request.DaneshId)||request.UserId=="99900999")
            {
                try
                {
                    tempRequest.DateTimeRange = new List<RequestDateTime>()
                    {
                        new RequestDateTime
                        {
                            Date = request.RequestDate,
                            StartTime = request.RequestStartTime,
                            EndTime = request.RequestEndTime,

                        }
                    };

                    var requestId1 = requestHandler.AddNewRequestForDefenceV2(tempRequest, request.UserId);
                    requestId = requestId1;



                    DefenceInformation defenceInformation = new DefenceInformation
                    {
                        StudentCode = request.IssuerId.ToString(),
                        StudentFullName = request.IssuerName,
                        DefenceSubject = request.DefenceSubject,
                        CollegeId = request.DaneshId.ToString(),
                        StartTime = request.RequestStartTime,
                        EndTime = request.RequestEndTime,
                        RequestDate = request.RequestDate,
                        UseOwnPc = request.UseOwnPc,
                        OnlineFirstTeacherId = request.OnlineFirstTeacherId,
                        OnlineSecondTeacherId = request.OnlineSecondTeacherId,
                        OnlineTeacherRole = request.OnlineTeacherRole,
                        RequestId = requestId1.ToString(),
                        
                        //sadeghsaryazdi
                        IsEquippingResource=request.IsEquippingResource,
                        FlagDoingMeetingOnline = request.FlagDoingMeetingOnline
                        ,IsRequestEducation=request.IsRequestEducation

                    };
                    cmb.InsertIntoDefenceInfo(defenceInformation);

                    //Log
                    //var comman = new CommonBusiness();
                    //comman.InsertIntoUserLog(Convert.ToInt32(request.UserId), "", 11, 114, "ثبت درخواست کلاس ", reqid);
                    


                    message = "درخواست شما با شماره " + requestId1.ToString() + " با موفقیت ثبت گردید";


                    return (string.Concat("ok", message));
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            else
            {
                message = "در زمان انتخاب شده، به دلیل پر بودن ظرفیت کلاس های دفاعِ این دانشکده، امکان برگزاری جلسه دفاع وجود ندارد";
            }
            requestId = 0;
            return message;
        }

        //private string IsAvoidTime(long startTime, string requestDate, StudentDefenceRequest request)
        //{

        //    if (requestDate.ToGregorian() > "1398/06/22".ToGregorian() && (((new TimeSpan(startTime)).TotalHours == 17) || ((new TimeSpan(startTime)).TotalHours == 18)))
        //    {
        //        if (!string.IsNullOrEmpty(request.OnlineTeacherRole) || request.IsEquippingResource)
        //            return "امکان ثبت درخواست پخش و دفاع آنلاین در ساعت 17 و 18 امکان پذیر نمی باشد";
        //    }
        //    if (requestDate.ToGregorian() > "1398/06/22".ToGregorian() && (((new TimeSpan(startTime)).TotalHours == 12)))
        //    {
        //        return "امکان ثبت درخواست در ساعت 12 امکان پذیر نمی باشد";
        //    }
        //    return null;
        //}

        class AvoidTime
        {
            public string Time { get; set; }
            public bool JustForOnline { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }

        }
        private string IsAvoidTime(long startTime, string requestDate, StudentDefenceRequest request)
        {
            var avoidTimes = requestDB.GetAvoidTime().ConvertDataTableToList<AvoidTime>();
            foreach (var time in avoidTimes)
            {

                if (requestDate.ToGregorian() >= time.StartDate.ToGregorian() &&
                    requestDate.ToGregorian() <= time.EndDate.ToGregorian() &&
                    (new TimeSpan(startTime)).TotalHours.ToString() == time.Time)
                {
                    if (time.JustForOnline && (!string.IsNullOrEmpty(request.OnlineTeacherRole) || request.IsEquippingResource))
                        return "امکان ثبت درخواست برگزاری دفاع آنلاین در ساعت مورد نظر شما امکان پذیر نمی باشد";
                    else if (time.JustForOnline && string.IsNullOrEmpty(request.OnlineTeacherRole) && !request.IsEquippingResource) { }
                    else
                        return "امکان ثبت درخواست در ساعت مورد نظر شما امکان پذیر نمی باشد";
                }
            }


            return null;
        }




        public string CreateStudentRequestForEducation(out int requestId, StudentDefenceRequest request)
        {
            var message = "ok";
            var tempRequest = new RequestFR
            {
                CatID = request.CategoryId,
                Subject = request.Subject,
                Note = request.Description,
                Location = request.Location,
                Status = request.Status,
                IssuerID = request.IssuerId,
                IssuerName = request.IssuerName,
                Capacity = request.Capacity,
                CourseName = request.DefenceSubject,
                DaneshID = request.DaneshId,
                //CourseDID = request.CourseId,
                Issue_time = DateTime.Now.ToPeString(),


            };


            /*******************/
            var issuerDate = DateTime.Now.Date;
            if (!IsThisTerm2(issuerDate.ToPeString(), request.RequestDate, 11)) //11 = appId
            {
                var currentTermDateRange = GeDateRangeOfTerm2(11); //11 = appId
                requestId = 0;
                return
                    $" امکان ثبت درخواست در ترم جاری در بازه زمانی {currentTermDateRange["start"]} تا {currentTermDateRange["end"]} می باشد";
            }




            if (request.RequestDate.ToGregorian().DayOfWeek == DayOfWeek.Friday ||
                request.RequestDate.ToGregorian().DayOfWeek == DayOfWeek.Thursday)
            {
                requestId = 0;
                return "امکان برگزاری جلسه دفاع در روزهای پنجشنبه و جمعه وجود ندارد";
            }
            //if (!string.IsNullOrEmpty(request.OnlineTeacherRole) || request.IsEquippingResource)
            //    if (request.RequestDate.ToGregorian().DayOfWeek == DayOfWeek.Monday &&
            //        request.RequestStartTime < 468000000000)
            //    {
            //        requestId = 0;
            //        return "امکان برگزاری جلسه دفاع آنلاین یا پخش  در روزهای دوشنبه قبل از ساعت 13 وجود ندارد";
            //    }
            if (CheckReqDate(request.RequestDate))
            {
                RequestDateTime requestDateTime = new RequestDateTime
                {
                    Date = request.RequestDate,
                    StartTime = request.RequestStartTime,
                    EndTime = request.RequestEndTime
                };
                tempRequest.DateTimeRange = new List<RequestDateTime> { requestDateTime };
            }
            else
            {
                message = "تاریخ درخواست باید بعد از تاریخ امروز باشد.";
                requestId = 0;
                return message;
            }


            var optlist = new List<Option>();

            RequestHandler requestHandler = new RequestHandler();




            //if (CheckBeforeDefenceRequest(request.RequestDate, request.RequestStartTime, request.RequestEndTime, request.IssuerId))
            //{
            //    message = "تداخل در ساعت و تاریخ درخواست ، با ساعات درخواست های پیشین این دانشجو وجود دارد.";
            //    requestId = 0;
            //    return message;
            //}


            if (HasAnotherRequestInThisTerm(request.IssuerId))
            {
                message = "به دلیل داشتن درخواست در حال گردش امکان ثبت وجود ندارد";
                requestId = 0;
                return message;
            }
            if (request.AcceptPropDate.Length < 6)
            {
                message = "به دلیل عدم ثبت تاریخ تصویب پروپوزال در گروه امکان ثبت درخواست وجود ندارد، لطفا به کارشناس پژوهش تیکت بزنید";
                requestId = 0;
            }
            var acceptDateSplit = request.AcceptPropDate.Split('/');
            var requestDateSplit = request.RequestDate.Split('/');

            var month = Convert.ToInt32(acceptDateSplit[1]);
            var addToYear = (month + 6) / 12;
            var addToMonth = (month + 6) % 12;

            if (addToYear == 1 && addToMonth == 0)
            {
                addToYear = 0;
                addToMonth = 6;
            }
            var year1 = Convert.ToInt32(acceptDateSplit[0]);
            var month1 = Convert.ToInt32(acceptDateSplit[1]);
            var day1 = Convert.ToInt32(acceptDateSplit[2]);
            if (addToYear == 1 && addToMonth == 0)
            {
                addToYear = 0;
                addToMonth = 6;

                month1 += 6;

            }
            else
            {
                if (addToYear == 1 && addToMonth > 0)
                {
                    year1 += addToYear;
                    month1 = addToMonth;
                }
                else
                {
                    year1 += addToYear;
                    month1 = addToMonth;

                }
            }


            if (month1 == 12 && day1 > 29)
            {
                year1 += 1;
                month1 = 1;
                day1 = day1 % 29;
            }
            else if (month1 > 6 && day1 > 30)
            {
                month1 += 1;
                day1 = 1;
            }

            var acceptDate = new DateTime(year1, month1, day1, new PersianCalendar());
            var requestDate = new DateTime(Convert.ToInt32(requestDateSplit[0]), Convert.ToInt32(requestDateSplit[1]),
                Convert.ToInt32(requestDateSplit[2]), new PersianCalendar());
            var diffTimeSpan = (requestDate - acceptDate);

            //var date12 = new DateTime(1396, 12, 01, new PersianCalendar());
            //if (requestDate >= date12)
            //{
            //    requestId = 0;
            //    return "دانشجوی گرامی در این تاریخ امکان رزرو جلسه دفاع وجود ندارد،تاریخ درخواست باید نهایتا تا 96/11/30 باشد .";
            //}
            //var startDatePrevent = new DateTime(1396, 10, 7, new PersianCalendar());
            //var endDatePrevent = new DateTime(1396, 11, 7, new PersianCalendar());
            //if (startDatePrevent <= requestDate && requestDate <= endDatePrevent)
            //{
            //    requestId = 0;
            //    return "به دلیل برگزاری امتحانات در این تاریخ امکان رزرو جلسه دفاع وجود ندارد، جهت اطلاعات بیشتر با دانشکده خود در تماس باشد.";
            //}


            var diffDateFor24 = (requestDate.Date - OneWorkingDays(DateTime.Now.Date)).TotalDays;
            if (!string.IsNullOrEmpty(request.OnlineTeacherRole.Trim()) || request.IsEquippingResource)
                if (diffDateFor24 <= 0)
                {
                    requestId = 0;
                    //  return "باید فاصله زمانی ثبت درخواست شما با برگزاری جلسه دفاع به دلیل پخش آنلاین یا حضور آنلاین استاد حداقل 24 ساعت باشد ";
                    return "باید فاصله زمانی ثبت درخواست شما با برگزاری جلسه دفاع به دلیل برگزاری آنلاین یا حضور آنلاین استاد حداقل 24 ساعت باشد ";
                }



            //if (!(diffTimeSpan.TotalDays >= 1))
            //{
            //    requestId = 0;
            //    message = "باید حداقل 6 ماه از زمان تصویب پرووزال تا زمان درخواستی گذشته باشد";
            //    return message;

            //}

            var _defenceInformation = GetDefenceInformation(request.IssuerId.ToString());

            Dictionary<string, string> profList = GetProfList(_defenceInformation);

            foreach (var keyValuePair in profList)
            {
                if (IsConflictProfessor(Convert.ToInt32(keyValuePair.Key), request.RequestDate, request.RequestStartTime, request.RequestEndTime))
                {
                    requestId = 0;
                    return $"دانشجوی گرامی برای استاد {keyValuePair.Value} در زمان مذکور جلسه دفاع دیگری رزرو شده است و امکان حضور استاد به صورت همزمان در جلسه دفاع شما وجود ندارد.";
                }
            }

        

            if (CanAssignmentResource(request.RequestDate, request.RequestStartTime, request.RequestEndTime, request.DaneshId))
            {
                try
                {

                    var requestId1 = requestHandler.AddNewRequestForDefence(tempRequest, optlist, request.UserId);
                    requestId = requestId1;



                    DefenceInformation defenceInformation = new DefenceInformation
                    {
                        StudentCode = request.IssuerId.ToString(),
                        StudentFullName = request.IssuerName,
                        DefenceSubject = request.DefenceSubject,
                        CollegeId = request.DaneshId.ToString(),
                        StartTime = request.RequestStartTime,
                        EndTime = request.RequestEndTime,
                        RequestDate = request.RequestDate,
                        UseOwnPc = request.UseOwnPc,
                        OnlineFirstTeacherId = request.OnlineFirstTeacherId,
                        OnlineSecondTeacherId = request.OnlineSecondTeacherId,
                        OnlineTeacherRole = request.OnlineTeacherRole,
                        RequestId = requestId1.ToString(),
                        IsRequestEducation=request.IsRequestEducation
                        

                    };
                    cmb.InsertIntoDefenceInfo(defenceInformation);

                    //Log
                    //var comman = new CommonBusiness();
                    //comman.InsertIntoUserLog(Convert.ToInt32(request.UserId), "", 11, 114, "ثبت درخواست کلاس ", reqid);



                    message = "درخواست شما با شماره " + requestId1.ToString() + " با موفقیت ثبت گردید";


                    return (string.Concat("ok", message));
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            else
            {
                message = "در زمان انتخاب شده، به دلیل پر بودن ظرفیت کلاس های دفاعِ این دانشکده، امکان برگزاری جلسه دفاع وجود ندارد";
            }
            requestId = 0;
            return message;
        }
        public string CreateStudentRequestForEducationV2(out int requestId, StudentDefenceRequest request)
        {

            var message = "ok";

            var tempRequest = new RequestFR
            {
                CatID = request.CategoryId,
                Subject = request.Subject,
                Note = request.Description,
                Location = request.Location,
                Status = request.Status,
                IssuerID = request.IssuerId,
                IssuerName = request.IssuerName,
                Capacity = request.Capacity,
                CourseName = request.DefenceSubject,
                DaneshID = request.DaneshId,
                //CourseDID = request.CourseId,
                Issue_time = DateTime.Now.ToPeString(),


            };

            //var hasOnline = HasOnlineRequest(request.IsEquippingResource, request.OnlineTeacherRole, request.RequestDate);
            //if (hasOnline != "no")
            //{
            //    requestId = 0;
            //    return hasOnline;
            //}

            var msg0 = IsAvoidTime(request.RequestStartTime, request.RequestDate, request);
            if (msg0 != null)
            {
                requestId = 0;
                return msg0;
            }

            CommonBusiness cmnb = new CommonBusiness();
            var falg = cmnb.CheckDifenceCondition(tempRequest.IssuerID.ToString());
            if (falg != 1)
            {
                message = falg == 2 ? "در حال حاظر شما امکان استفاده از این بخش را ندارید" : "دانشجوي گرامي شما بدليل عدم تكميل فرايندهاي مربوط به پورتال پژوهش مجاز به ثبت تاريخ جلسه دفاع خود نيستيد. جهت کسب اطلاعات بیشتر به بخش مربوطه تیکت ارسال نمایید";

                requestId = 0;
                return message;
            }

            var msg = HasPreventCertainTime(request.RequestDate);
            if (msg != null && msg.IsForEmployee)
            {
                requestId = 0;
                return msg.Description;

            }
            var msg1 = HasPreventedFromCurrentDate((int)Applicant.کارمند);
            if (msg1 != "ok")
            {
                requestId = 0;
                return msg1;

            }
            /*******************/
            var issuerDate = DateTime.Now.Date;
            if (!IsThisTerm2(issuerDate.ToPeString(), request.RequestDate, 11)) //11 = appId
            {
                var currentTermDateRange = GeDateRangeOfTerm2(11); //11 = appId
                requestId = 0;
                return
                    $" امکان ثبت درخواست در ترم جاری در بازه زمانی {currentTermDateRange["start"]} تا {currentTermDateRange["end"]} می باشد";
            }




            if (request.RequestDate.ToGregorian().DayOfWeek == DayOfWeek.Friday ||
                request.RequestDate.ToGregorian().DayOfWeek == DayOfWeek.Thursday)
            {
                requestId = 0;
                return "امکان برگزاری جلسه دفاع در روزهای پنجشنبه و جمعه وجود ندارد";
            }
            //if (!string.IsNullOrEmpty(request.OnlineTeacherRole) || request.IsEquippingResource)
            //    if (request.RequestDate.ToGregorian().DayOfWeek == DayOfWeek.Monday &&
            //        request.RequestStartTime < 468000000000)
            //    {
            //        requestId = 0;
            //        return "امکان برگزاری جلسه دفاع آنلاین یا پخش  در روزهای دوشنبه قبل از ساعت 13 وجود ندارد";
            //    }
            if (CheckReqDate(request.RequestDate))
            {
                RequestDateTime requestDateTime = new RequestDateTime
                {
                    Date = request.RequestDate,
                    StartTime = request.RequestStartTime,
                    EndTime = request.RequestEndTime
                };
                tempRequest.DateTimeRange = new List<RequestDateTime> { requestDateTime };
            }
            else
            {
                message = "تاریخ درخواست باید بعد از تاریخ امروز باشد.";
                requestId = 0;
                return message;
            }


            var optlist = new List<Option>();

            RequestHandler requestHandler = new RequestHandler();




            //if (CheckBeforeDefenceRequest(request.RequestDate, request.RequestStartTime, request.RequestEndTime, request.IssuerId))
            //{
            //    message = "تداخل در ساعت و تاریخ درخواست ، با ساعات درخواست های پیشین این دانشجو وجود دارد.";
            //    requestId = 0;
            //    return message;
            //}


            if (HasAnotherRequestInThisTerm(request.IssuerId))
            {
                message = "به دلیل داشتن درخواست در حال گردش امکان ثبت وجود ندارد";
                requestId = 0;
                return message;
            }
            if (request.AcceptPropDate.Length < 6)
            {
                message = "به دلیل عدم ثبت تاریخ تصویب پروپوزال در گروه امکان ثبت درخواست وجود ندارد، لطفا به کارشناس پژوهش تیکت بزنید";
                requestId = 0;
            }
            var acceptDateSplit = request.AcceptPropDate.Split('/');
            var requestDateSplit = request.RequestDate.Split('/');

            var month = Convert.ToInt32(acceptDateSplit[1]);
            var addToYear = (month + 6) / 12;
            var addToMonth = (month + 6) % 12;

            if (addToYear == 1 && addToMonth == 0)
            {
                addToYear = 0;
                addToMonth = 6;
            }
            var year1 = Convert.ToInt32(acceptDateSplit[0]);
            var month1 = Convert.ToInt32(acceptDateSplit[1]);
            var day1 = Convert.ToInt32(acceptDateSplit[2]);
            if (addToYear == 1 && addToMonth == 0)
            {
                addToYear = 0;
                addToMonth = 6;

                month1 += 6;

            }
            else
            {
                if (addToYear == 1 && addToMonth > 0)
                {
                    year1 += addToYear;
                    month1 = addToMonth;
                }
                else
                {
                    year1 += addToYear;
                    month1 = addToMonth;

                }
            }


            if (month1 == 12 && day1 > 29)
            {
                year1 += 1;
                month1 = 1;
                day1 = day1 % 29;
            }
            else if (month1 > 6 && day1 > 30)
            {
                month1 += 1;
                day1 = 1;
            }

            var acceptDate = new DateTime(year1, month1, day1, new PersianCalendar());
            var requestDate = new DateTime(Convert.ToInt32(requestDateSplit[0]), Convert.ToInt32(requestDateSplit[1]),
                Convert.ToInt32(requestDateSplit[2]), new PersianCalendar());
            var diffTimeSpan = (requestDate - acceptDate);

            //var date12 = new DateTime(1396, 12, 01, new PersianCalendar());
            //if (requestDate >= date12)
            //{
            //    requestId = 0;
            //    return "دانشجوی گرامی در این تاریخ امکان رزرو جلسه دفاع وجود ندارد،تاریخ درخواست باید نهایتا تا 96/11/30 باشد .";
            //}
            //var startDatePrevent = new DateTime(1396, 10, 7, new PersianCalendar());
            //var endDatePrevent = new DateTime(1396, 11, 7, new PersianCalendar());
            //if (startDatePrevent <= requestDate && requestDate <= endDatePrevent)
            //{
            //    requestId = 0;
            //    return "به دلیل برگزاری امتحانات در این تاریخ امکان رزرو جلسه دفاع وجود ندارد، جهت اطلاعات بیشتر با دانشکده خود در تماس باشد.";
            //}


            var diffDateFor24 = (requestDate.Date - OneWorkingDays(DateTime.Now)).TotalDays;
            if (!string.IsNullOrEmpty(request.OnlineTeacherRole.Trim()) || request.IsEquippingResource)
                if (diffDateFor24 <= 0)
                {
                    requestId = 0;
                    //return "باید فاصله زمانی ثبت درخواست شما با برگزاری جلسه دفاع به دلیل پخش آنلاین یا حضور آنلاین استاد حداقل 24 ساعت باشد ";
                    return "باید فاصله زمانی ثبت درخواست شما با برگزاری جلسه دفاع به دلیل برگزاری آنلاین یا حضور آنلاین استاد حداقل 24 ساعت باشد ";

                }



            //if (!(diffTimeSpan.TotalDays >= 1))
            //{
            //    requestId = 0;
            //    message = "باید حداقل 6 ماه از زمان تصویب پرووزال تا زمان درخواستی گذشته باشد";
            //    return message;

            //}



            //Debit

            //DataTable dt = null;

            //dt = FindBedehkarForReserve(request.IssuerId.ToString());
            //if (!(dt.Rows[0]["bedehi"] == null || Convert.ToDouble(dt.Rows[0]["bedehi"].ToString()) <= 0))
            //{
            //    var financialPermissionCondition = GetFinancialPermissionCondition(request.IssuerId.ToString());
            //    if (financialPermissionCondition.Rows.Count > 0)
            //    {
            //        var financialPermissionDate = financialPermissionCondition.Rows[0]["financialPermissionDate"].ToString().ToGregorian();
            //        var unitSectionDate = financialPermissionCondition.Rows[0]["unitSectionDate"].ToString().ToGregorian();
            //        var dateDiff = (financialPermissionDate.Date - unitSectionDate.Date).Days;

            //        var hasFinancialPermission = HasFinancialPermission(request.IssuerId.ToString());

            //        if (financialPermissionCondition.Rows[0]["stg12"].ToString() == 2.ToString()
            //            && dateDiff >= 0 && !hasFinancialPermission)
            //        {
            //            requestId = 0;
            //            return "دانشجوي گرامي شما بدلیل داشتن بدهکاری مالی امکان رزرو جلسه دفاع را ندارید برای اطلاعات بیشتر به واحد مالی تیکت بزنید ";
            //        }
            //    }

            //}







            var _defenceInformation = GetDefenceInformation(request.IssuerId.ToString());

            var refereeParticipatingOtherDefensesSameDateMsg = GetRefereeParticipatingOtherDefensesSameDate(
                _defenceInformation.FirstRefereeFullName, _defenceInformation.SecondRefereeFullName
                , _defenceInformation.FirstRefereeId,
                  _defenceInformation.SecondRefereeId, request.RequestDate);

            if (refereeParticipatingOtherDefensesSameDateMsg != "ok")
            {
                requestId = 0;
                return refereeParticipatingOtherDefensesSameDateMsg;

            }
            Dictionary<string, string> profList = GetProfList(_defenceInformation);

            foreach (var keyValuePair in profList)
            {
                if (IsConflictProfessor(Convert.ToInt32(keyValuePair.Key), request.RequestDate, request.RequestStartTime, request.RequestEndTime))
                {
                    requestId = 0;
                    return $"دانشجوی گرامی برای استاد {keyValuePair.Value} در زمان مذکور جلسه دفاع دیگری رزرو شده است و امکان حضور استاد به صورت همزمان در جلسه دفاع شما وجود ندارد.";
                }
            }



            if (CanAssignmentResourceV2(request.RequestDate, request.RequestStartTime, request.RequestEndTime, request.DaneshId))
            {
                try
                {
                    tempRequest.DateTimeRange = new List<RequestDateTime>()
                    {
                        new RequestDateTime
                        {
                            Date = request.RequestDate,
                            StartTime = request.RequestStartTime,
                            EndTime = request.RequestEndTime,
                        }
                    };

                    var requestId1 = requestHandler.AddNewRequestForDefenceV2(tempRequest, request.UserId);
                    requestId = requestId1;



                    DefenceInformation defenceInformation = new DefenceInformation
                    {
                        StudentCode = request.IssuerId.ToString(),
                        StudentFullName = request.IssuerName,
                        DefenceSubject = request.DefenceSubject,
                        CollegeId = request.DaneshId.ToString(),
                        StartTime = request.RequestStartTime,
                        EndTime = request.RequestEndTime,
                        RequestDate = request.RequestDate,
                        UseOwnPc = request.UseOwnPc,
                        OnlineFirstTeacherId = request.OnlineFirstTeacherId,
                        OnlineSecondTeacherId = request.OnlineSecondTeacherId,
                        OnlineTeacherRole = request.OnlineTeacherRole,
                        RequestId = requestId1.ToString(),
                        IsRequestEducation=request.IsRequestEducation
                        

                    };
                    cmb.InsertIntoDefenceInfo(defenceInformation);

                    //Log
                    //var comman = new CommonBusiness();
                    //comman.InsertIntoUserLog(Convert.ToInt32(request.UserId), "", 11, 114, "ثبت درخواست کلاس ", reqid);



                    message = "درخواست شما با شماره " + requestId1.ToString() + " با موفقیت ثبت گردید";


                    return (string.Concat("ok", message));
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            else
            {
                message = "در زمان انتخاب شده، به دلیل پر بودن ظرفیت کلاس های دفاعِ این دانشکده، امکان برگزاری جلسه دفاع وجود ندارد";
            }
            requestId = 0;
            return message;
        }

        public CertainTimesDto HasPreventCertainTime(string requestDate)
        {
            var preventCertainTime = GetPreventCertainTime(requestDate);
            if (preventCertainTime != null && preventCertainTime.Count > 0)
            {
                return preventCertainTime.FirstOrDefault();
            }
            else
            {
                return null;
            }

        }



        public enum Applicant
        {
            کارمند = 1,
            دانشجو = 0
        }
        public string HasPreventedFromCurrentDate(int applicantEnum)
        {
            var applicant = applicantEnum == 1 ? true : false;
            var preventedDate = requestDB.HasPreventedFromCurrentDate(DateTime.Now.ToPeString(), applicant);
            if (preventedDate?.Rows.Count > 0)
            {
                var startDate = preventedDate.Rows[0]["StartDate"].ToString();
                var endDate = preventedDate.Rows[0]["EndDate"].ToString();
                var description = preventedDate.Rows[0]["Description"].ToString();
                return " از تاریخ" + startDate + " تا تاریخ " + endDate + " به علت: " + description;

            }
            else
            {
                return "ok";
            }


        }

        public string UpdateStudentRequest(StudentDefenceRequest request)
        {
            var message = "ok";
            RequestHandler requestHandler = new RequestHandler();
            if (request.Status > 0)
            {
                return "شما تنها قبل از تایید دانشکده امکان ویرایش دارید.";
            }
            if (requestHandler.IsEditedStudentRequest(request.Id))
            {
                return "به دلیل اینکه قبلا یک بار ویرایش نموده اید امکان ویرایش مجدد وجود ندارد.";
            }

            var msg = HasPreventCertainTime(request.RequestDate);
            if (msg != null && msg.IsForStudent)
                return msg.Description;
            /******************************/
            var issuerDate = DateTime.Now.Date;
            if (!IsThisTerm2(issuerDate.ToPeString(), request.RequestDate, 11)) //11 = appId
            {
                var currentTermDateRange = GeDateRangeOfTerm2(11); //11 = appId

                return
                    $" امکان ثبت درخواست در ترم جاری در بازه زمانی {currentTermDateRange["start"]} تا {currentTermDateRange["end"]} می باشد";
            }
            var tempRequest = new RequestFR
            {
                CatID = request.CategoryId,
                Subject = request.Subject,
                Note = request.Description,
                Location = request.Location,
                Status = request.Status,
                IssuerID = request.IssuerId,
                IssuerName = request.IssuerName,
                Capacity = request.Capacity,
                CourseName = request.DefenceSubject,
                DaneshID = request.DaneshId,
                //CourseDID = request.CourseId,
                Issue_time = DateTime.Now.ToPeString(),

                ID = request.Id
            };
            //var hasOnline = HasOnlineRequest(request.IsEquippingResource, request.OnlineTeacherRole, request.RequestDate);
            //if (hasOnline != "no")
            //{

            //    return hasOnline;
            //}

            var msg0 = IsAvoidTime(request.RequestStartTime, request.RequestDate, request);
            if (msg0 != null)
            {
                return msg0;
            }

            if (CheckReqDate(request.RequestDate))
            {
                RequestDateTime requestDateTime = new RequestDateTime
                {
                    Date = request.RequestDate,
                    StartTime = request.RequestStartTime,
                    EndTime = request.RequestEndTime
                };
                tempRequest.DateTimeRange = new List<RequestDateTime> { requestDateTime };
            }
            else
            {
                message = "تاریخ درخواست باید بعد از تاریخ امروز باشد.";
                return message;
            }


            var optlist = new List<Option>();





            //if (CheckBeforeDefenceRequest(request.RequestDate, request.RequestStartTime, request.RequestEndTime, request.IssuerId))
            //{
            //    message = "تداخل در ساعت و تاریخ درخواست ، با ساعات درخواست های پیشین این دانشجو وجود دارد.";
            //    return message;
            //}


            //if (HasAnotherRequestInThisTerm(request.IssuerId))
            //{
            //    message = "به دلیل داشتن درخواست در حال گردش امکان ثبت وجود ندارد";
            //    return message;
            //}

            var acceptDateSplit = request.AcceptPropDate.Split('/');
            var requestDateSplit = request.RequestDate.Split('/');
            var month = Convert.ToInt32(acceptDateSplit[1]);
            var addToYear = (month + 6) / 12;
            var addToMonth = (month + 6) % 12;
            if (addToYear == 1 && addToMonth == 0)
            {
                addToYear = 0;
                addToMonth = 6;
            }

            var year1 = Convert.ToInt32(acceptDateSplit[0]);
            var month1 = Convert.ToInt32(acceptDateSplit[1]);
            var day1 = Convert.ToInt32(acceptDateSplit[2]);
            if (addToYear == 1 && addToMonth == 0)
            {
                addToYear = 0;
                addToMonth = 6;

                month1 += 6;

            }
            else
            {
                if (addToYear == 1 && addToMonth > 0)
                {
                    year1 += addToYear;
                    month1 = addToMonth;
                }
                else
                {
                    year1 += addToYear;
                    month1 = addToMonth;

                }
            }

            if (month1 == 12 && day1 > 29)
            {
                year1 += 1;
                month1 = 1;
                day1 = day1 % 29;
            }
            else
            if (month1 > 6 && day1 > 30)
            {
                month1 += 1;
                day1 = 1;
            }


            var acceptDate = new DateTime(year1, month1, day1, new PersianCalendar());
            var requestDate = new DateTime(Convert.ToInt32(requestDateSplit[0]), Convert.ToInt32(requestDateSplit[1]),
                Convert.ToInt32(requestDateSplit[2]), new PersianCalendar());
            var diffTimeSpan = (requestDate - acceptDate);

            //var date12 = new DateTime(1396, 12, 01, new PersianCalendar());
            // if (requestDate >= date12)
            //   return "دانشجوي محترم حداكثر زمان مجاز جهت برگزاري جلسه دفاع در نيمسال جاري تاريخ 30/11/96 مي باشد.";

            //  var startDatePrevent = new DateTime(1396, 10, 10, new PersianCalendar());
            //  var endDatePrevent = new DateTime(1396, 11, 7, new PersianCalendar());
            //  if (startDatePrevent <= requestDate && requestDate <= endDatePrevent)
            //  {

            //      return "دانشجوی گرامی به دلیل برگزاری امتحانات در این تاریخ امکان رزرو جلسه دفاع وجود ندارد، جهت اطلاعات بیشتر با دانشکده خود در تماس باشد.";
            //  }
            //var diffDateFor72 = (requestDate.Date - ThreeWorkingDays(DateTime.Now.Date)).TotalDays;

            //if (diffDateFor72 < 0)
            //{
            //    return "باید از زمان درخواست شما تا زمان کنونی حداقل 72 ساعت گذشته باشد";
            //}
            double diffDateFor3day;

                 diffDateFor3day = (requestDate.Date - ThreeWorkingDays(DateTime.Now.Date)).TotalDays;

            if (diffDateFor3day < 0)
            {
               
                return "دانشجوی گرامی، باید فاصله زمانی ثبت درخواست شما با برگزاری جلسه دفاع حداقل 3 روز کاری باشد ";
            }
            if (request.RequestDate.ToGregorian().DayOfWeek == DayOfWeek.Friday || request.RequestDate.ToGregorian().DayOfWeek == DayOfWeek.Thursday)
            {

                return "امکان برگزاری جلسه دفاع در روزهای پنجشنبه و جمعه وجود ندارد";
            }
            //if (!string.IsNullOrEmpty(request.OnlineTeacherRole) || request.IsEquippingResource)

            //    if (request.RequestDate.ToGregorian().DayOfWeek == DayOfWeek.Monday && request.RequestStartTime < 468000000000)
            //    {

            //        return "دانشجوی گرامی امکان برگزاری جلسه دفاع آنلاین یا پخش  در روزهای دوشنبه قبل از ساعت 13 وجود ندارد";
            //    }


            if (!(diffTimeSpan.TotalDays >= 1))
            {
                message = "دانشجو گرامی باید حداقل 6 ماه از زمان تصویب پرووزال تا زمان درخواستی گذشته باشد";
                return message;

            }

            var defenceInformation1 = GetDefenceInformation(request.IssuerId.ToString());

            var refereeParticipatingOtherDefensesSameDateMsg = GetRefereeParticipatingOtherDefensesSameDate(
                defenceInformation1.FirstRefereeFullName, defenceInformation1.SecondRefereeFullName
                , defenceInformation1.FirstRefereeId,
                defenceInformation1.SecondRefereeId, request.RequestDate, request.Id);

            if (refereeParticipatingOtherDefensesSameDateMsg != "ok")
            {

                return refereeParticipatingOtherDefensesSameDateMsg;

            }

            Dictionary<string, string> profList = GetProfList(defenceInformation1);

            foreach (var keyValuePair in profList)
            {
                if (IsConflictProfessorForEducation(Convert.ToInt32(keyValuePair.Key), request.RequestDate, request.RequestStartTime, request.RequestEndTime, request.Id))
                {
                    return $"دانشجوی گرامی برای استاد {keyValuePair.Value} در زمان مذکور جلسه دفاع دیگری رزرو شده است و امکان حضور استاد به صورت همزمان در جلسه دفاع شما وجود ندارد.";
                }
            }


            if (CanAssignmentResourceV2(request.RequestDate, request.RequestStartTime, request.RequestEndTime, request.DaneshId, request.Id))
            {
                try
                {
                    var req = new RequestFR();
                    req.DateTimeRange = tempRequest.DateTimeRange;
                    req.ID = tempRequest.ID;
                    DefenceInformation defenceInformation = new DefenceInformation
                    {
                        StudentCode = request.IssuerId.ToString(),
                        StudentFullName = request.IssuerName,
                        DefenceSubject = request.DefenceSubject,
                        CollegeId = request.DaneshId.ToString(),
                        StartTime = request.RequestStartTime,
                        EndTime = request.RequestEndTime,
                        RequestDate = request.RequestDate,
                        UseOwnPc = request.UseOwnPc,
                        OnlineFirstTeacherId = request.OnlineFirstTeacherId,
                        OnlineSecondTeacherId = request.OnlineSecondTeacherId,
                        OnlineTeacherRole = request.OnlineTeacherRole,
                        RequestId = tempRequest.ID.ToString(),
                        IsEdited = request.IsEdited,
                        FlagDoingMeetingOnline = request.FlagDoingMeetingOnline,
                        FlagUpdateRegisterDate = true
                        ,IsRequestEducation=request.IsRequestEducation
                    };
                    cmb.UpdateIntoDefenceInfo(defenceInformation);
                    var requestId = requestHandler.UpdateStudentRequestDBV2(request);





                    //Log
                    //var comman = new CommonBusiness();
                    //comman.InsertIntoUserLog(Convert.ToInt32(request.UserId), "", 11, 114, "ثبت درخواست کلاس ", reqid);



                    message = "درخواست شما با شماره " + requestId.ToString() + " با موفقیت ثبت گردید";


                    return (string.Concat("ok", message));
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            else
            {
                message = "در این روز و ساعت امکان به دلیل پر بودن ظرفیت کلاس های دفاع امکان برگزاری جلسه دفاع وجود ندارد";
            }
            return message;
        }


        public string UpdateStudentRequestForEducation(StudentDefenceRequest request)
        {
            var message = "ok";
            RequestHandler requestHandler = new RequestHandler();
            //if (request.Status > 0)
            //{
            //    return "شما تنها قبل از تایید دانشکده امکان ویرایش دارید.";
            //}
            //if (requestHandler.IsEditedStudentRequest(request.Id))
            //{
            //    return "به دلیل اینکه قبلا یک بار ویرایش نموده اید امکان ویرایش مجدد وجود ندارد.";
            //}
            var tempRequest = new RequestFR
            {

                Subject = request.Subject,
                Note = request.Description,
                Location = request.Location,
                Status = request.Status,
                IssuerID = request.IssuerId,
                IssuerName = request.IssuerName,
                Capacity = request.Capacity,
                CourseName = request.DefenceSubject,
                DaneshID = request.DaneshId,
                //CourseDID = request.CourseId,
                Issue_time = DateTime.Now.ToPeString(),

                ID = request.Id,

            };
            //var hasOnline = HasOnlineRequest(request.IsEquippingResource, request.OnlineTeacherRole, request.RequestDate);
            //if (hasOnline != "no")
            //{


            //    return hasOnline;
            //}
            var msg0 = IsAvoidTime(request.RequestStartTime, request.RequestDate, request);
            if (msg0 != null)
            {
                return msg0;
            }

            CommonBusiness cmnb = new CommonBusiness();
            var falg = cmnb.CheckDifenceCondition(tempRequest.IssuerID.ToString());
            if (falg != 1)
            {
                message = falg == 2 ? "در حال حاظر شما امکان استفاده از این بخش را ندارید" : "دانشجوي گرامي شما بدليل عدم تكميل فرايندهاي مربوط به پورتال پژوهش مجاز به ثبت تاريخ جلسه دفاع خود نيستيد. جهت کسب اطلاعات بیشتر به بخش مربوطه تیکت ارسال نمایید";


                return message;
            }

            var msg = HasPreventCertainTime(request.RequestDate);
            if (msg != null && msg.IsForEmployee)
                return msg.Description;
            /********************************/
            var issuerDate = DateTime.Now.Date;
            if (!IsThisTerm2(issuerDate.ToPeString(), request.RequestDate, 11)) //11 = appId
            {
                var currentTermDateRange = GeDateRangeOfTerm2(11); //11 = appId

                return
                    $" امکان ثبت درخواست در ترم جاری در بازه زمانی {currentTermDateRange["start"]} تا {currentTermDateRange["end"]} می باشد";
            }

            if (CheckReqDate(request.RequestDate))
            {
                RequestDateTime requestDateTime = new RequestDateTime
                {
                    Date = request.RequestDate,
                    StartTime = request.RequestStartTime,
                    EndTime = request.RequestEndTime
                };
                tempRequest.DateTimeRange = new List<RequestDateTime> { requestDateTime };
            }
            else
            {
                message = "تاریخ درخواست باید بعد از تاریخ امروز باشد.";
                return message;
            }
            if (request.RequestDate.ToGregorian().DayOfWeek == DayOfWeek.Friday || request.RequestDate.ToGregorian().DayOfWeek == DayOfWeek.Thursday)
            {

                return "امکان برگزاری جلسه دفاع در روزهای پنجشنبه و جمعه وجود ندارد";
            }
            //if (!string.IsNullOrEmpty(request.OnlineTeacherRole) || request.IsEquippingResource)

            //    if (request.RequestDate.ToGregorian().DayOfWeek == DayOfWeek.Monday && request.RequestStartTime < 468000000000)
            //    {

            //        return "امکان برگزاری جلسه دفاع آنلاین یا پخش در روزهای دوشنبه قبل از ساعت 13 وجود ندارد";
            //    }

            var optlist = new List<Option>();





            //if (CheckBeforeDefenceRequest(request.RequestDate, request.RequestStartTime, request.RequestEndTime, request.IssuerId))
            //{
            //    message = "تداخل در ساعت و تاریخ درخواست ، با ساعات درخواست های پیشین این دانشجو وجود دارد.";
            //    return message;
            //}


            //if (HasAnotherRequestInThisTerm(request.IssuerId))
            //{
            //    message = "به دلیل داشتن درخواست در حال گردش امکان ثبت وجود ندارد";
            //    return message;
            //}

            var acceptDateSplit = request.AcceptPropDate.Split('/');
            var requestDateSplit = request.RequestDate.Split('/');

            var month = Convert.ToInt32(acceptDateSplit[1]);
            var addToYear = (month + 6) / 12;
            var addToMonth = (month + 6) % 12;
            if (addToYear == 1 && addToMonth == 0)
            {
                addToYear = 0;
                addToMonth = 6;
            }

            var year1 = Convert.ToInt32(acceptDateSplit[0]);
            var month1 = Convert.ToInt32(acceptDateSplit[1]);
            var day1 = Convert.ToInt32(acceptDateSplit[2]);
            if (addToYear == 1 && addToMonth == 0)
            {
                addToYear = 0;
                addToMonth = 6;

                month1 += 6;

            }
            else
            {
                if (addToYear == 1 && addToMonth > 0)
                {
                    year1 += addToYear;
                    month1 = addToMonth;
                }
                else
                {
                    year1 += addToYear;
                    month1 = addToMonth;

                }
            }


            if (month1 == 12 && day1 > 29)
            {
                year1 += 1;
                month1 = 1;
                day1 = day1 % 29;
            }
            else
            if (month1 > 6 && day1 > 30)
            {
                month1 += 1;
                day1 = 1;
            }

            var acceptDate = new DateTime(year1, month1, day1, new PersianCalendar());
            var requestDate = new DateTime(Convert.ToInt32(requestDateSplit[0]), Convert.ToInt32(requestDateSplit[1]),
                Convert.ToInt32(requestDateSplit[2]), new PersianCalendar());
            var diffTimeSpan = (requestDate - acceptDate);

            // var date12 = new DateTime(1396, 12, 01, new PersianCalendar());
            //  if (requestDate >= date12)
            //     return "کاربر گرامی حداكثر زمان مجاز جهت برگزاري جلسه دفاع در نيمسال جاري تاريخ 30/11/96 مي باشد.";

            var diffDateFor24 = (requestDate.Date - OneWorkingDays(DateTime.Now.Date)).TotalDays;
            if (!string.IsNullOrEmpty(request.OnlineTeacherRole.Trim()) || request.IsEquippingResource)
                if (diffDateFor24 <= 0)
                {
                    //  return "باید از زمان درخواست شما تا زمان کنونی به دلیل پخش آنلاین یا حضور آنلاین استاد حداقل 24 ساعت گذشته باشد";
                    return "باید از زمان درخواست شما تا زمان کنونی به دلیل برگزاری آنلاین یا حضور آنلاین استاد حداقل 24 ساعت گذشته باشد";
                }



            //if (!(diffTimeSpan.TotalDays >= 1))
            //{
            //    message = "دانشجو گرامی باید حداقل 6 ماه از زمان تصویب پرووزال تا زمان درخواستی گذشته باشد";
            //    return message;

            //}


            var defenceInformation1 = GetDefenceInformation(request.IssuerId.ToString());

            var refereeParticipatingOtherDefensesSameDateMsg = GetRefereeParticipatingOtherDefensesSameDate(
                defenceInformation1.FirstRefereeFullName, defenceInformation1.SecondRefereeFullName
                , defenceInformation1.FirstRefereeId,
                defenceInformation1.SecondRefereeId, request.RequestDate, request.Id);

            if (refereeParticipatingOtherDefensesSameDateMsg != "ok")
            {
                return refereeParticipatingOtherDefensesSameDateMsg;
            }

            Dictionary<string, string> profList = GetProfList(defenceInformation1);

            foreach (var keyValuePair in profList)
            {
                if (IsConflictProfessorForEducation(Convert.ToInt32(keyValuePair.Key), request.RequestDate, request.RequestStartTime, request.RequestEndTime, request.Id))
                {
                    return $" برای استاد {keyValuePair.Value} در زمان مذکور جلسه دفاع دیگری رزرو شده است و امکان حضور استاد به صورت همزمان در جلسه دفاع شما وجود ندارد.";
                }
            }

            if (CanAssignmentResourceV2(request.RequestDate, request.RequestStartTime, request.RequestEndTime, request.DaneshId, request.Id))
            {
                try
                {
                    var req = new RequestFR();
                    req.DateTimeRange = tempRequest.DateTimeRange;
                    req.ID = tempRequest.ID;
                    DefenceInformation defenceInformation = new DefenceInformation
                    {
                        StudentCode = request.IssuerId.ToString(),
                        StudentFullName = request.IssuerName,
                        DefenceSubject = request.DefenceSubject,
                        CollegeId = request.DaneshId.ToString(),
                        StartTime = request.RequestStartTime,
                        EndTime = request.RequestEndTime,
                        RequestDate = request.RequestDate,
                        UseOwnPc = request.UseOwnPc,
                        OnlineFirstTeacherId = request.OnlineFirstTeacherId,
                        OnlineSecondTeacherId = request.OnlineSecondTeacherId,
                        OnlineTeacherRole = request.OnlineTeacherRole,
                        RequestId = tempRequest.ID.ToString(),
                        IsEdited = request.IsEdited,
                        IsEquippingResource = request.IsEquippingResource,
                        FlagDoingMeetingOnline = request.FlagDoingMeetingOnline,
                        FlagUpdateRegisterDate = false
                        ,IsRequestEducation=request.IsRequestEducation


                    };
                    cmb.UpdateIntoDefenceInfo(defenceInformation);
                    var reqDb = new RequestDBAccess();
                    reqDb.UpdateStatusDefRequest(9, Convert.ToInt32(defenceInformation.RequestId));

                    //var requestId = requestHandler.UpdateStudentRequestWithEducation(request);
                    var requestId = requestHandler.UpdateStudentRequestDBV2(request);



                    //Log
                    //var comman = new CommonBusiness();
                    //comman.InsertIntoUserLog(Convert.ToInt32(request.UserId), "", 11, 114, "ثبت درخواست کلاس ", reqid);



                    message = "درخواست شما با شماره " + requestId.ToString() + " با موفقیت ثبت گردید";


                    return (string.Concat("ok", message));
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            else
            {
                message = "در این روز و ساعت امکان به دلیل پر بودن ظرفیت کلاس های دفاع امکان برگزاری جلسه دفاع وجود ندارد";
            }
            return message;
        }

        private Dictionary<string, string> GetProfList(DefenceInformation defenceInformation)
        {
            var profList = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(defenceInformation.FirstConsultantId) &&
                defenceInformation.FirstConsultantId != 0.ToString())
            {
                if (!profList.Keys.Contains(defenceInformation.FirstConsultantId.Substring(3)))
                    profList.Add(defenceInformation.FirstConsultantId.Substring(3), defenceInformation.FirstConsultantFullName);
            }

            if (!string.IsNullOrEmpty(defenceInformation.FirstGuideId) &&
                defenceInformation.FirstGuideId != 0.ToString())
            {
                if (!profList.Keys.Contains(defenceInformation.FirstGuideId.Substring(3)))
                    profList.Add(defenceInformation.FirstGuideId.Substring(3), defenceInformation.FirstGuideFullName);
            }

            if (!string.IsNullOrEmpty(defenceInformation.FirstRefereeId) &&
                defenceInformation.FirstRefereeId != 0.ToString())
            {
                if (!profList.Keys.Contains(defenceInformation.FirstRefereeId.Substring(3)))
                    profList.Add(defenceInformation.FirstRefereeId.Substring(3), defenceInformation.FirstRefereeFullName);
            }


            if (!string.IsNullOrEmpty(defenceInformation.SecondConsultantId) &&
                defenceInformation.SecondConsultantId != 0.ToString())
            {
                if (!profList.Keys.Contains(defenceInformation.SecondConsultantId.Substring(3)))
                    profList.Add(defenceInformation.SecondConsultantId.Substring(3), defenceInformation.SecondConsultantFullName);
            }

            if (!string.IsNullOrEmpty(defenceInformation.SecondGuideId) &&
                defenceInformation.SecondGuideId != 0.ToString())
            {
                if (!profList.Keys.Contains(defenceInformation.SecondGuideId.Substring(3)))
                    profList.Add(defenceInformation.SecondGuideId.Substring(3), defenceInformation.SecondGuideFullName);
            }

            if (!string.IsNullOrEmpty(defenceInformation.SecondRefereeId) &&
                defenceInformation.SecondRefereeId != 0.ToString())
            {
                if (!profList.Keys.Contains(defenceInformation.SecondRefereeId.Substring(3)))
                    profList.Add(defenceInformation.SecondRefereeId.Substring(3), defenceInformation.SecondRefereeFullName);
            }

            return profList;
        }


        public Boolean CheckReqDate(string inputdate)
        {
            int todaypersian = Convert.ToInt32(DateTime.Now.ToPeString("yyyyMMdd"));
            int inputdateNo = Convert.ToInt32(inputdate.Replace("/", ""));
            return (inputdateNo >= todaypersian);
        }


        public DefenceInformation GetDefenceInformation(string studentCode)
        {
            return requestDB.GetDefenceInformation(studentCode);
        }

        public bool ISOnlineRequest(int requestId)
        {
            return requestDB.ISOnlineRequest(requestId);
        }
        public List<ProfessorDto> GetProfessorsRelatedStudent(int studentCode)
        {
            return requestDB.GetProfessorsRelatedStudent(studentCode);
        }

        public List<CertainTimesDto> GetPreventCertainTime(string startDate)
        {
            return requestDB.GetPreventCertainTime(startDate);
        }
        public DataTable GetProfessorAttendanceInCurrentTerm(int studentCode)
        {
            return requestDB.GetProfessorAttendanceInCurrentTerm(studentCode);
        }
        public int ReadSMSFlagByReqId(int reqID)
        {
            return requestDB.ReadSMSFlagByReqId(reqID);
        }
        public long GetDefenceInMeetingLength(int collegeId)
        {
            var length = requestDB.GetDefenceInMeetingLength(collegeId);
            var division = length / 60;
            var remaining = length % 60;


            return TimeSpan.Parse(string.Concat(division, ":", remaining)).Ticks;
        }

        public bool CanAssignmentResource(string requestDate, long startTime, long endTime, int collegeId)
        {
            return requestDB.CanAssignmentResource(requestDate, startTime, endTime, collegeId);
        }
        public bool CanAssignmentResourceV2(string requestDate, long startTime, long endTime, int collegeId)
        {
            return requestDB.CanAssignmentResourceV2(requestDate, startTime, endTime, collegeId);
        }
        public bool CanAssignmentResource(string requestDate, long startTime, long endTime, int collegeId, int reqId)
        {
            return requestDB.CanAssignmentResource(requestDate, startTime, endTime, collegeId, reqId);
        }
        public bool CanAssignmentResourceV2(string requestDate, long startTime, long endTime, int collegeId, int reqId)
        {
            return requestDB.CanAssignmentResourceV2(requestDate, startTime, endTime, collegeId, reqId);
        }
        public bool HasAnotherRequestInThisTerm(int issuerId)
        {
            return requestDB.HasAnotherRequestInThisTerm(issuerId);
        }
        public bool HasAnotherRequestInThisTermForIntro(int issuerId)
        {
            return requestDB.HasAnotherRequestInThisTermForIntro(issuerId);
        }

        public bool CheckBeforeDefenceRequest(string requestDate, long startTime, long endTime, int issuerId)
        {
            return requestDB.CheckBeforeDefenceRequest(requestDate, startTime, endTime, issuerId);
        }

        public DataTable GetStudentDefenceRequest(int studentId)
        {
            return requestDB.GetStudentDefenceRequest(studentId);
        }

        public string GetStudentMobile(int studentId)
        {
            return requestDB.GetStudentMobile(studentId);
        }
        public void SetStudentMobile(string studentId, string mobileNumber)
        {
            requestDB.SetStudentMobile(studentId, mobileNumber);
        }

        public DataTable GetStudentRequestAndDefInfo(int daneshID)
        {
            return requestDB.GetStudentRequestAndDefInfo(daneshID);

        }

        public static List<T> ConvertDataTableToList<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName && dr[column.ColumnName] != DBNull.Value && dr[column.ColumnName] != null)
                    {
                        pro.SetValue(obj, dr[column.ColumnName], null);
                        break;
                    }
                }
            }
            return obj;
        }

        public static DateTime ThreeWorkingDays(DateTime dateTime)
        {
            var date = dateTime;
            const int countTot = 3;
            int counter = 0;
            RequestDBAccess requestDBAccess = new RequestDBAccess();
            for (int i = 0; i < countTot;)
            {
                string datePersian = date.AddDays(counter).Date.ToPeString();
                DataTable dt = requestDBAccess.CheckExistSpecialDate(datePersian, datePersian, 1);
                if (date.AddDays(counter).DayOfWeek != DayOfWeek.Thursday
                    && date.AddDays(counter).DayOfWeek != DayOfWeek.Friday
                   && (dt == null || dt.Rows.Count == 0))
                {
                    ++i;

                }
                ++counter;
            }
            return date.AddDays(counter);
            //var dayZeroFlag = 0;
            //var dayOneflag = 0;
            //var dayTwoflag = 0;
            //var dayThreeflag = 0;
            //if (date.DayOfWeek == DayOfWeek.Friday || date.DayOfWeek == DayOfWeek.Thursday)
            //    dayZeroFlag = 1;

            //if (date.AddDays(1).DayOfWeek == DayOfWeek.Friday || date.AddDays(1).DayOfWeek == DayOfWeek.Thursday)
            //    dayOneflag = 1;
            //if (date.AddDays(2).DayOfWeek == DayOfWeek.Friday || date.AddDays(2).DayOfWeek == DayOfWeek.Thursday)
            //    dayTwoflag = 1;
            //if (date.AddDays(3).DayOfWeek == DayOfWeek.Friday || date.AddDays(3).DayOfWeek == DayOfWeek.Thursday)
            //    dayThreeflag = 1;
            //return dateTime.AddDays((3 + dayOneflag + dayTwoflag + dayThreeflag + dayZeroFlag));
            ////return dateTime.AddDays((1 + dayOneflag));
        }
        public static DateTime FiveWorkingDays(DateTime dateTime)
        {
            var date = dateTime;
            const int countTot = 5;
            int counter = 0;
            //mocaghat baraye shoroe defence
            DateTime dt1 = new DateTime(2020, 08, 05);
            int i = 0;
            if (dateTime == dt1)
            {

                ++i;
            }
            RequestDBAccess requestDBAccess = new RequestDBAccess();
            for (; i < countTot;)
            {
                string datePersian = date.AddDays(counter).Date.ToPeString();
                DataTable dt = requestDBAccess.CheckExistSpecialDate(datePersian, datePersian, 1);
                if (date.AddDays(counter).DayOfWeek != DayOfWeek.Thursday
                    && date.AddDays(counter).DayOfWeek != DayOfWeek.Friday
                   && (dt == null || dt.Rows.Count == 0))
                {
                    ++i;

                }
                ++counter;
            }
            return date.AddDays(counter);
            //var dayZeroFlag = 0;
            //var dayOneflag = 0;
            //var dayTwoflag = 0;
            //var dayThreeflag = 0;
            //if (date.DayOfWeek == DayOfWeek.Friday || date.DayOfWeek == DayOfWeek.Thursday)
            //    dayZeroFlag = 1;

            //if (date.AddDays(1).DayOfWeek == DayOfWeek.Friday || date.AddDays(1).DayOfWeek == DayOfWeek.Thursday)
            //    dayOneflag = 1;
            //if (date.AddDays(2).DayOfWeek == DayOfWeek.Friday || date.AddDays(2).DayOfWeek == DayOfWeek.Thursday)
            //    dayTwoflag = 1;
            //if (date.AddDays(3).DayOfWeek == DayOfWeek.Friday || date.AddDays(3).DayOfWeek == DayOfWeek.Thursday)
            //    dayThreeflag = 1;
            //return dateTime.AddDays((3 + dayOneflag + dayTwoflag + dayThreeflag + dayZeroFlag));
            ////return dateTime.AddDays((1 + dayOneflag));
        }


        public static DateTime OneWorkingDays(DateTime dateTime)
        {
            var date = dateTime;
            const int countTot = 1;
            int counter = 0;
            RequestDBAccess requestDBAccess = new RequestDBAccess();
            for (int i = 0; i < countTot;)
            {
                string datePersian = date.AddDays(counter).Date.ToPeString();
                DataTable dt = requestDBAccess.CheckExistSpecialDate(datePersian, datePersian, 1);
                if (date.AddDays(counter).DayOfWeek != DayOfWeek.Thursday
                    && date.AddDays(counter).DayOfWeek != DayOfWeek.Friday
                   && (dt == null || dt.Rows.Count == 0))
                {
                    ++i;

                }
                ++counter;
            }
            return date.AddDays(counter);




            //var date = dateTime;
            //var dayZeroFlag = 0;
            //var dayOneflag = 0;

            //if (date.DayOfWeek == DayOfWeek.Friday || date.DayOfWeek == DayOfWeek.Thursday)
            //    dayZeroFlag = 1;

            //if (date.AddDays(1).DayOfWeek == DayOfWeek.Thursday)
            //    ++dayOneflag;
            //if (date.AddDays(1).DayOfWeek == DayOfWeek.Friday)
            //    ++dayOneflag;
            //if (date.AddDays(2).DayOfWeek == DayOfWeek.Thursday)
            //    ++dayOneflag;
            //if (date.AddDays(2).DayOfWeek == DayOfWeek.Friday)
            //    ++dayOneflag;


            //return dateTime.AddDays(1 + dayOneflag + dayZeroFlag);
            ////return dateTime.AddDays((1 + dayOneflag));
        }
        public static DateTime WorkingDays24h(DateTime? dateTime)
        {

            var date = dateTime;
            const int countTot = 1;
            int counter = 0;
            RequestDBAccess requestDBAccess = new RequestDBAccess();
            for (int i = 0; i < countTot;)
            {
                string datePersian = date.Value.AddDays(counter).Date.ToPeString();
                DataTable dt = requestDBAccess.CheckExistSpecialDate(datePersian, datePersian, 1);
                if (date.Value.AddDays(counter).DayOfWeek != DayOfWeek.Thursday
                    && date.Value.AddDays(counter).DayOfWeek != DayOfWeek.Friday
                   && (dt == null || dt.Rows.Count == 0))
                {
                    ++i;

                }
                ++counter;
            }
            return date.Value.AddDays(counter);


            //DateTime? date = dateTime.Value;
            //if (date.Value.DayOfWeek == DayOfWeek.Wednesday)
            //    date = date.Value.AddDays(3);

            //else if (date.Value.DayOfWeek == DayOfWeek.Thursday)
            //    date = date.Value.Date.AddDays(3);
            //else if (date.Value.DayOfWeek == DayOfWeek.Friday)
            //{
            //    date = date.Value.Date.AddDays(2);
            //}
            //else if (
            //    date.Value.DayOfWeek == DayOfWeek.Monday ||
            //    date.Value.DayOfWeek == DayOfWeek.Saturday ||
            //    date.Value.DayOfWeek == DayOfWeek.Sunday ||
            //    date.Value.DayOfWeek == DayOfWeek.Tuesday)
            //    date = date.Value.AddDays(1);

            //return date.Value;
        }
        public static DateTime WorkingDays12h(DateTime? dateTime)
        {
            DateTime date = WorkingDays24h(dateTime).AddHours(-12);
        
            return date;
        }
        public static DateTime WorkingDays48h(DateTime? dateTime)
        {

            var date = dateTime;
            const int countTot = 2;
            int counter = 0;
            RequestDBAccess requestDBAccess = new RequestDBAccess();
            for (int i = 0; i < countTot;)
            {
                string datePersian = date.Value.AddDays(counter).Date.ToPeString();
                DataTable dt = requestDBAccess.CheckExistSpecialDate(datePersian, datePersian, 1);
                if (date.Value.AddDays(counter).DayOfWeek != DayOfWeek.Thursday
                    && date.Value.AddDays(counter).DayOfWeek != DayOfWeek.Friday
                   && (dt == null || dt.Rows.Count == 0))
                {
                    ++i;

                }
                ++counter;
            }
            return date.Value.AddDays(counter);


        }

        public void UpdateStatusDefRequest(int status, int reqID)
        {
            requestDB.UpdateStatusDefRequest(status, reqID);

        }
        public bool UpdateDefenceInformation_DefenceHasDone(int requestId = -1, bool defenceHasDone = false, int refereeType = -1, bool paymentStatusForReferee = false)
        {
            return requestDB.UpdateDefenceInformation_DefenceHasDone(requestId, defenceHasDone, refereeType, paymentStatusForReferee);

        }

        public DataTable GetDefenceInformationByRequestID(int requestID = -1)
        {
            return requestDB.GetDefenceInformationByRequestID(requestID);
        }

        public bool InsertIntoRefereeWagePayment_Log(int requestId, string studentName, string requestDate, string collageName, string refereeMobile, int martabe, string refereefullName, string studentCode, string sibaNo, string wage, int refereeType, string term, bool isRejected)
        {
            return requestDB.InsertIntoRefereeWagePayment_Log(requestId, studentName, requestDate, collageName, refereeMobile, martabe, refereefullName, studentCode, sibaNo, wage, refereeType, term, isRejected);
        }


        public bool UpdateRefereeWagePayment_Log(int requestId, int refereeType, string term, int martabe, string wage, bool chkPaymentDavar, bool isRejected)
        {
            return requestDB.UpdateRefereeWagePayment_Log(requestId, refereeType, term, martabe, wage, chkPaymentDavar, isRejected);
        }

        public bool UpdateEducateProfessorRequest(bool isEducateProfessor, int requestId)
        {
            return requestDB.UpdateEducateProfessorRequest(isEducateProfessor, requestId);

        }
        public void IsDeleteDefRequest(int reqID)
        {
            requestDB.IsDeleteDefRequest(reqID);

        }

        public List<StudentDefenceRequestDTO> GetAllOnlineDefense()
        {
            return requestDB.GetAllOnlineDefense();
        }
        /// <summary>
        /// چک کردن این موضوع که استاد در آن ساعت خاص جلسه دفاع دیگری نداشته باشد
        /// </summary>
        /// <param name="professerId">
        /// آیدی استاد</param>
        /// <param name="requestDate">
        /// تاریخ درخواستی</param>
        /// <param name="startTime">
        /// ساعت شروع</param>
        /// <param name="endTime">
        /// ساعت پایان</param>
        /// <returns></returns>
        public bool IsConflictProfessor(int professerId, string requestDate, long startTime, long endTime)
        {
            var resualtDt = requestDB.IsConflictProfessor(requestDate, startTime, endTime);
            if (professerId != 0)
            {
                string professer = "200" + professerId.ToString();
                foreach (DataRow row in resualtDt.Rows)
                {

                    for (int i = 0; i < resualtDt.Columns.Count; i++)
                    {
                        if (professer == row[i].ToString())
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            else
                return false;

        }

        public bool IsConflictProfessorForEducation(int professerId, string requestDate, long startTime, long endTime, int reqId)
        {
            var resualtDt = requestDB.IsConflictProfessorForEducation(requestDate, startTime, endTime, reqId);
            if (professerId != 0)
            {
                string professer = "200" + professerId.ToString();
                foreach (DataRow row in resualtDt.Rows)
                {

                    for (int i = 0; i < resualtDt.Columns.Count; i++)
                    {
                        if (professer == row[i].ToString())
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            else
                return false;

        }



        public int GetStCodeByReqId(int reqID)
        {
            return requestDB.GetStCodeByReqId(reqID);

        }

        public int GetStatusByReqId(int reqID)
        {
            return requestDB.GetStatusByReqId(reqID);

        }
        public bool SendSMSForFinacial(string StudentCode ,string msg)
        {
            var _reqHand = new RequestHandler();
           
            bool sentSMS; string smsStatusText;
            try
            {
                var StMob = _reqHand.GetStudentMobile(Convert.ToInt32(StudentCode));
                //var msg = "";
                //msg = @"دانشجوي گرامي شما بدلیل داشتن بدهکاری مالی امکان رزرو جلسه دفاع را ندارید
                //                    لطفا نسبت به پرداخت شهریه اقدام نمایید  ";
                 var _ComBuiness = new CommonBusiness();
                _ComBuiness.sendSMS(StMob, msg, out sentSMS, out smsStatusText);
                return true;
               
            }
            catch(Exception ex){
                return false;
            }
        }
            public bool SendSMSForDef(string StudentCode, int typeId)
        {
            var _reqHand = new RequestHandler();
            var StMob = _reqHand.GetStudentMobile(Convert.ToInt32(StudentCode));
            var _ComBuiness = new CommonBusiness();
            DefenceInformation DefInfo = _reqHand.GetDefenceInformation(StudentCode);
            try
            {
                var msg = "";
                bool sentSMS; string smsStatusText;
                if (typeId == 2)
                {    //تایید اداری شده 
                    msg = " دانشجوی گرامی جلسه دفاع شما در تاریخ " + DefInfo.RequestDate + " ساعت: " + DefInfo.StartTime + " توسط دانشکده حذف شد و برگزار نمیگردد ";
                    if (StMob != null)
                    {
                        _ComBuiness.sendSMS(StMob, msg, out sentSMS, out smsStatusText);
                    }
                    var msg2 = " استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + DefInfo.RequestDate + " ساعت " + DefInfo.StartTime + " توسط دانشکده حذف شد و برگزار نمیگردد، شما به عنوان استاد داور ایشان انتخاب شده اید";
                    if (DefInfo.FirstRefereeMobile != null)
                    {
                        _ComBuiness.sendSMS(DefInfo.FirstRefereeMobile, msg2, out sentSMS, out smsStatusText);
                    }
                    var msg3 = " استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + DefInfo.RequestDate + " ساعت " + DefInfo.StartTime + " توسط دانشکده حذف شد و برگزار نمیگردد، شما به عنوان استاد راهنما ایشان انتخاب شده اید";
                    if (DefInfo.FirstGuideMobile != null)
                    {
                        _ComBuiness.sendSMS(DefInfo.FirstGuideMobile, msg3, out sentSMS, out smsStatusText);
                    }
                    var msg4 = " استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + DefInfo.RequestDate + " ساعت " + DefInfo.StartTime + " توسط دانشکده حذف شد و برگزار نمیگردد، شما به عنوان استاد مشاور ایشان انتخاب شده اید";

                    if (DefInfo.FirstConsultantMobile != null)
                    {
                        _ComBuiness.sendSMS(DefInfo.FirstConsultantMobile, msg4, out sentSMS, out smsStatusText);
                    }
                    var msg5 = " استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + DefInfo.RequestDate + " ساعت " + DefInfo.StartTime + " توسط دانشکده حذف شد و برگزار نمیگردد، شما به عنوان استاد داور ایشان انتخاب شده اید";
                    if (DefInfo.SecondRefereeMobile != null)
                    {
                        _ComBuiness.sendSMS(DefInfo.SecondRefereeMobile, msg5, out sentSMS, out smsStatusText);
                    }
                    var msg6 = " استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + DefInfo.RequestDate + " ساعت " + DefInfo.StartTime + " توسط دانشکده حذف شد و برگزار نمیگردد، شما به عنوان استاد راهنما ایشان انتخاب شده اید";
                    if (DefInfo.SecondGuideMobile != null)
                    {
                        _ComBuiness.sendSMS(DefInfo.SecondGuideMobile, msg6, out sentSMS, out smsStatusText);
                    }
                    var msg7 = " استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + DefInfo.RequestDate + " ساعت " + DefInfo.StartTime + " توسط دانشکده حذف شد و برگزار نمیگردد، شما به عنوان استاد مشاور ایشان انتخاب شده اید";
                    if (DefInfo.SecondConsultantMobile != null)
                    {
                        _ComBuiness.sendSMS(DefInfo.SecondConsultantMobile, msg7, out sentSMS, out smsStatusText);
                    }
                }

                else if (typeId == 0)
                {       //حذف دانشکده
                    msg = "دانشجوی گرامی درخواست دفاع شما حذف گردید برای ثبت درخواست جدید به سامانه رزرواسیون جلسه دفاع مراجعه نمایید ";
                }
                _ComBuiness.sendSMS(StMob, msg, out sentSMS, out smsStatusText);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }




        }

        public bool SendSMSForArticle(string studentCode, int typeId, string locationName, string resourceName1, long startTime, string date, int reqID, int resId)
        {
            var resourceName = resourceName1.Replace("حضوری ", "");
            var _reqHand = new RequestHandler();
            var StMob = _reqHand.GetStudentMobile(Convert.ToInt32(studentCode));
            var _ComBuiness = new CommonBusiness();
            DefenceInformation DefInfo = _reqHand.GetDefenceInformation(studentCode);
            ResourceDBAccess resDB = new ResourceDBAccess();
            var SMSFlag = _reqHand.ReadSMSFlagByReqId(reqID);
            try
            {
                var msg = "دانشجوی گرامی پس از برگزاری جلسه دفاع، تکمیل فرم مربوط به ارائه مقاله یا انصراف از مقاله و تحویل آن به کارشناس پژوهش دانشکده الزامیست. عدم تکمیل فرم مذکور موجب تأخیر روند فارغ التحصیلی خواهد شد";
                if (typeId != 2)
                {
                    bool sentSMS; string smsStatusText;
                    _ComBuiness.sendSMS(StMob, msg, out sentSMS, out smsStatusText);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DateTime StringPersianDateToGerogorianDate(string stringDate)
        {
            if (!string.IsNullOrWhiteSpace(stringDate))
            {
                var splitStringDate = stringDate.Split('/');
                return new DateTime(Convert.ToInt32(splitStringDate[0]), Convert.ToInt32(splitStringDate[1]), Convert.ToInt32(splitStringDate[2]), new PersianCalendar());
            }
            return new DateTime();
        }


        public void ServerLogger(string mobile = null, string note = null, string absPath = null)
        {
            try
            {
                if (!System.IO.File.Exists(absPath))
                    System.IO.File.Create(absPath).Dispose();
                using (StreamWriter w = File.AppendText(absPath))
                {
                    w.WriteLine(string.Format("\t\t{0}\t\t{1}\r\n\t\t#####################################\r\n\t\t\t", mobile, note));
                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public static bool SendSmsAcceptEducataionForDef(string studentCode)
        {
  
            var _reqHand = new RequestHandler();
            var StMob = _reqHand.GetStudentMobile(Convert.ToInt32(studentCode));
            var _ComBuiness = new CommonBusiness();
            DefenceInformation DefInfo = _reqHand.GetDefenceInformation(studentCode);

           string msgStudent = " دانشجو گرامی "
                                + " با توجه به برگزاری جلسه دفاع از پایان نامه بصورت کاملا' آنلاین ، لطفا' جهت کسب آمادگی لازم و تست تجهیزات فنی خود، در اسرع وقت ضمن مطالعه راهنما و نصب نرم افزارهای مورد نیاز واقع در آدرس" + System.Environment.NewLine
                                + " http://iauec.ac.ir/support " + System.Environment.NewLine
                                + " با مراجعه به سامانه خدمات الکترونیکی- بخش امور پژوهشی ، در جلسه دفاع آنلاین آزمایشی شرکت نمایید. " + System.Environment.NewLine
                                + " زمان برگزاری جلسات دفاع آنلاین آزمایشی:" + System.Environment.NewLine
                                + "شنبه تا چهارشنبه از ساعت ۸ الی ۱۶" + System.Environment.NewLine
                                + "واحد الکترونیکی دانشگاه آزاد اسلامی";

            string msgOstad = " استاد محترم "
                                   + " با توجه به برگزاری جلسه دفاع از پایان نامه بصورت کاملا' آنلاین، لطفا' جهت کسب آمادگی لازم و تست تجهیزات فنی خود، در اسرع وقت ضمن مطالعه راهنما و نصب نرم افزارهای مورد نیاز واقع در آدرس" + System.Environment.NewLine
                                + " http://iauec.ac.ir/support " + System.Environment.NewLine
                                + " با مراجعه به سامانه خدمات الکترونیکی- بخش امور پژوهشی ، در جلسه دفاع آنلاین آزمایشی شرکت نمایید. " + System.Environment.NewLine
                                + " زمان برگزاری جلسات دفاع آنلاین آزمایشی:" + System.Environment.NewLine
                                + "شنبه تا چهارشنبه از ساعت ۸ الی ۱۶" + System.Environment.NewLine
                                + "واحد الکترونیکی دانشگاه آزاد اسلامی";
            try
            {
                bool sentSMS;
                string smsStatusText;
                if (StMob != null)
                {
                    _ComBuiness.sendSMS(StMob, msgStudent, out sentSMS, out smsStatusText);
                }
                if (DefInfo.FirstRefereeMobile != null)
                {
                    _ComBuiness.sendSMS(DefInfo.FirstRefereeMobile, msgOstad, out sentSMS, out smsStatusText);

                }
                if (DefInfo.FirstGuideMobile != null)
                {
                    _ComBuiness.sendSMS(DefInfo.FirstGuideMobile, msgOstad, out sentSMS, out smsStatusText);

                }
                if (DefInfo.FirstConsultantMobile != null)
                {
                    _ComBuiness.sendSMS(DefInfo.FirstConsultantMobile, msgOstad, out sentSMS, out smsStatusText);

                }
                if (DefInfo.SecondRefereeMobile != null)
                {
                    _ComBuiness.sendSMS(DefInfo.SecondRefereeMobile, msgOstad, out sentSMS, out smsStatusText);

                }
                if (DefInfo.SecondGuideMobile != null)
                {
                    _ComBuiness.sendSMS(DefInfo.SecondGuideMobile, msgOstad, out sentSMS, out smsStatusText);

                }
               if (DefInfo.SecondConsultantMobile != null)
                {
                    _ComBuiness.sendSMS(DefInfo.SecondConsultantMobile, msgOstad, out sentSMS, out smsStatusText);

                }
                return true;
            }
      
            catch (Exception ex)
            {
                return false;
            }
        
          
        }



        private  string GetTextSmsAcceptRequestByEducation(string stCode, string stName, string typeOs, string dateDef, long TimeDef, bool isStudent=false,bool IsVir=false)
        {
            typeOs = typeOs.Replace("اول", "").Replace("داخلی", "").Replace("خارجی", "");
            string msg = "";
            if (IsVir)
                msg += " اصلاحی :";
            if(isStudent)
            {
                msg += "دانشجو گرامی جلسه دفاع شما ";
            }
            else
            {
                msg+= " استاد گرامی جلسه دفاع دانشجو "
                 + stCode + " " + stName;
            }
            msg += " در تاریخ "
                 + dateDef
                 + " ساعت "
                 + TimeDef
                 + "به صورت کاملا آنلاین برگزار می‌گردد";
            if (!isStudent)
            {
                msg += " شما به عنوان استاد "
                + typeOs
                + " ایشان انتخاب شده اید ";
             }
            msg+=
                ". ورود به جلسه دفاع آنلاین از سامانه خدمات الکترونیکی  "+ System.Environment.NewLine
                 + " service.iauec.ac.ir " + System.Environment.NewLine
                + " بخش امور پژوهشی قسمت  دفاع‌های آنلاین امکانپذیر می‌باشد " + System.Environment.NewLine
                + " واحد الکترونیکی دانشگاه آزاد اسلامی ";
            return msg;
        }
        private string GetTextSmsAcceptRequestByEducation()
        {
            return " باتوجه به برگزاری جلسه دفاع به صورت آنلاین , حضور تمامی ارکان دفاع شامل اساتید راهنما , مشاور و داور (داوران) در جلسه دفاع الزامی می‌باشد"
                      + System.Environment.NewLine 
                      + "واحد الکترونیکی دانشگاه آزاد اسلامی";
        }
        public bool SendSMSForDef(string studentCode, int typeId, string locationName, string resourceName1, long startTime, string date, int reqID, int resId, string absPath = null)
        {
            var resourceName = resourceName1.Replace("حضوری ", "");
            var _reqHand = new RequestHandler();
            var StMob = _reqHand.GetStudentMobile(Convert.ToInt32(studentCode));

            var _ComBuiness = new CommonBusiness();

            DefenceInformation DefInfo = _reqHand.GetDefenceInformation(studentCode);

            var studentDefenceRequestList = _reqHand.GetStudentDefenceRequest(Convert.ToInt32(studentCode));
            var listOfDefenceRequest = RequestHandler.ConvertDataTableToList<StudentDefenceRequestDTO>(studentDefenceRequestList);

            var inCirculationRequest = listOfDefenceRequest.FirstOrDefault(x => x.isDeleted != true && StringPersianDateToGerogorianDate(x.RequestDate) >= DateTime.Now);

            ResourceDBAccess resDB = new ResourceDBAccess();

            var SMSFlag = _reqHand.ReadSMSFlagByReqId(reqID);
            try
            {
                var msg = "";
                bool sentSMS;
                string smsStatusText;
                if (typeId == 2)
                {    // تایید اداری شده و رد شده دانشکده
                    msg = "دانشجوی گرامی جلسه دفاع شما در تاریخ " + date + " ساعت: " + startTime + " توسط دانشکده حذف شد و برگزار نمیگردد ";
                    if (StMob != null)
                    {
                        _ComBuiness.sendSMS(StMob, msg, out sentSMS, out smsStatusText);
                    }

                    var msg2 = " استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + " توسط دانشکده حذف شد و برگزار نمیگردد، شما به عنوان استاد داور ایشان انتخاب شده اید";
                    if (DefInfo.FirstRefereeMobile != null)
                    {
                        _ComBuiness.sendSMS(DefInfo.FirstRefereeMobile, msg2, out sentSMS, out smsStatusText);

                    }
                    var msg3 = " استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + " توسط دانشکده حذف شد و برگزار نمیگردد، شما به عنوان استاد راهنما ایشان انتخاب شده اید";
                    if (DefInfo.FirstGuideMobile != null)
                    {
                        _ComBuiness.sendSMS(DefInfo.FirstGuideMobile, msg3, out sentSMS, out smsStatusText);

                    }
                    var msg4 = " استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + " توسط دانشکده حذف شد و برگزار نمیگردد، شما به عنوان استاد مشاور ایشان انتخاب شده اید";
                    if (DefInfo.FirstConsultantMobile != null)
                    {
                        _ComBuiness.sendSMS(DefInfo.FirstConsultantMobile, msg4, out sentSMS, out smsStatusText);

                    }
                    var msg5 = " استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + " توسط دانشکده حذف شد و برگزار نمیگردد، شما به عنوان استاد داور ایشان انتخاب شده اید";
                    if (DefInfo.SecondRefereeMobile != null)
                    {
                        _ComBuiness.sendSMS(DefInfo.SecondRefereeMobile, msg5, out sentSMS, out smsStatusText);

                    }
                    var msg6 = " استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + " توسط دانشکده حذف شد و برگزار نمیگردد، شما به عنوان استاد راهنما ایشان انتخاب شده اید";
                    if (DefInfo.SecondGuideMobile != null)
                    {
                        _ComBuiness.sendSMS(DefInfo.SecondGuideMobile, msg6, out sentSMS, out smsStatusText);

                    }
                    var msg7 = " استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + " توسط دانشکده حذف شد و برگزار نمیگردد، شما به عنوان استاد مشاور ایشان انتخاب شده اید";
                    if (DefInfo.SecondConsultantMobile != null)
                    {
                        _ComBuiness.sendSMS(DefInfo.SecondConsultantMobile, msg7, out sentSMS, out smsStatusText);

                    }
                }
                else if (typeId == 1)
                {    //تایید اداری شده است

                    if (SMSFlag == 1)
                    {
                        #region SmsOldVirComment
                        //msg = "اصلاحی :  دانشجوی گرامی جلسه دفاع شما در تاریخ " + date + " ساعت: " + startTime + "در" + " " + resourceName + " برگزار میگردد "
                        //+ " همچنین دفاع مذکور در سامانه خدمات بخش امورپژوهشی پنل دفاع آنلاین قابل مشاهده میباشد.";
                        //var msg2 = "اصلاحی :  استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + "در" + " " + resourceName + "برگزار میگردد، شما به عنوان استاد داور ایشان انتخاب شده اید "
                        //    + " همچنین دفاع مذکور در سامانه خدمات بخش امورپژوهشی پنل دفاع آنلاین قابل مشاهده میباشد .";

                        //var msg3 = "اصلاحی :  استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + "در" + " " + resourceName + "برگزار میگردد، شما به عنوان استاد راهنما ایشان انتخاب شده اید "
                        //+ " همچنین دفاع مذکور در سامانه خدمات بخش امورپژوهشی پنل دفاع آنلاین قابل مشاهده میباشد .";
                        //var msg4 = "اصلاحی :  استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + "در" + " " + resourceName + "برگزار میگردد، شما به عنوان استاد مشاور ایشان انتخاب شده اید "
                        //+ " همچنین دفاع مذکور در سامانه خدمات بخش امورپژوهشی پنل دفاع آنلاین قابل مشاهده میباشد .";
                        //var msg5 = "اصلاحی :  استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + "در" + " " + resourceName + "برگزار میگردد، شما به عنوان استاد داور ایشان انتخاب شده اید "
                        //+ " همچنین دفاع مذکور در سامانه خدمات بخش امورپژوهشی پنل دفاع آنلاین قابل مشاهده میباشد .";
                        //var msg6 = "اصلاحی :  استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + "در" + " " + resourceName + "برگزار میگردد، شما به عنوان استاد راهنما ایشان انتخاب شده اید "
                        //+ " همچنین دفاع مذکور در سامانه خدمات بخش امورپژوهشی پنل دفاع آنلاین قابل مشاهده میباشد.";
                        //var msg7 = "اصلاحی :  استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + "در" + " " + resourceName + "برگزار میگردد، شما به عنوان استاد مشاور ایشان انتخاب شده اید "
                        //+ " همچنین دفاع مذکور در سامانه خدمات بخش امورپژوهشی پنل دفاع آنلاین قابل مشاهده میباشد .";

                        //if (inCirculationRequest != null && (inCirculationRequest.OnlineTeacherRole == "guide" || inCirculationRequest.OnlineTeacherRole == "consultant"))
                        //{
                        //    if (inCirculationRequest.OnlineTeacherRole == "guide")
                        //    {

                        //        if (inCirculationRequest.OnlineFirstTeacherId.ToString() == DefInfo.FirstGuideId)
                        //        {
                        //            msg3 = " اصلاحی :  استاد گرامی جلسه دفاع دانشجو  " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + "در" + " " + resourceName + " برگزار میگردد، شما به عنوان استاد راهنما ایشان انتخاب شده اید "
                        //                 + " برای ورود به جلسه آنلاین دفاع به سامانه خدمات الکترونیکی به ادرس زیر بخش امور پژوهشی قسمت دفاع آنلاین مراجعه فرمایید ."
                        //                 + " service.iauec.ac.ir ";
                        //        }
                        //        else
                        //        {
                        //            if (inCirculationRequest.OnlineFirstTeacherId.ToString() == DefInfo.SecondGuideId)
                        //            {
                        //                msg6 = "اصلاحی :  استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + "در" + " " + resourceName + "برگزار میگردد، شما به عنوان استاد راهنما ایشان انتخاب شده اید "
                        //                 + " برای ورود به جلسه آنلاین دفاع به سامانه خدمات الکترونیکی به ادرس زیر بخش امور پژوهشی قسمت دفاع آنلاین مراجعه فرمایید ."
                        //                 + " service.iauec.ac.ir ";
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        if (inCirculationRequest.OnlineTeacherRole == "consultant")
                        //        {

                        //            if (inCirculationRequest.OnlineFirstTeacherId.ToString() == DefInfo.FirstConsultantId)
                        //            {
                        //                msg4 = "اصلاحی :  استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + "در" + " " + resourceName + "برگزار میگردد، شما به عنوان استاد مشاور ایشان انتخاب شده اید "
                        //                     + " برای ورود به جلسه آنلاین دفاع به سامانه خدمات الکترونیکی به ادرس زیر بخش امور پژوهشی قسمت دفاع آنلاین مراجعه فرمایید ."
                        //                           + " service.iauec.ac.ir";
                        //            }
                        //            else
                        //            {
                        //                if (inCirculationRequest.OnlineFirstTeacherId.ToString() == DefInfo.SecondConsultantId)
                        //                {
                        //                    msg7 = "اصلاحی :  استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + "در" + " " + resourceName + "برگزار میگردد، شما به عنوان استاد مشاور ایشان انتخاب شده اید "
                        //                     + " برای ورود به جلسه آنلاین دفاع به سامانه خدمات الکترونیکی به ادرس زیر بخش امور پژوهشی قسمت دفاع آنلاین مراجعه فرمایید ."
                        //                           + " service.iauec.ac.ir";
                        //                }
                        //            }
                        //        }
                        //    }
                        //}





                        //var msg8 = "فرهیخته گرامی؛ با سلام، بنابر بخشنامه سازمان مركزي هرگونه اجبار دانشجو به پذيرايي و تهيه هديه در جلسات دفاع از پايان نامه از جانب گروه، اساتيد، كاركنان و ... ممنوع مي باشد.دانشجوياني كه شخصاً مايل به انجام پذيرايي مي باشند مي توانند اين كار را در حد متعارف و منحصر به آب معدني، شيريني و ميوه انجام دهند.ضمناً اهدا و دريافت هديه به هر نحو ممنوع بوده و در صورت مشاهده با متخلف (دانشجو، كارمند و اركان دفاع) برخورد قانوني خواهد شد.  واحد الکترونیکی دانشگاه آزاد اسلامی ";
                        //var msg8 = "فرهیخته گرامی؛ با سلام و احترام،\r\n از این که با رعایت شئونات اخلاقی و اسلامی در جلسات دفاع از پایان نامه، مطابق با بخشنامه \"دستورالعمل آراستگی و شئون فرهنگی و رفتاری\"، ساحت معنوی و مقدس علم را پاس می دارید، سپاسگزاریم";
                        //if (StMob != null)
                        //{
                        //    _ComBuiness.sendSMS(StMob, msg8, out sentSMS, out smsStatusText);
                        //}
                        //if (DefInfo.FirstRefereeMobile != null)
                        //{
                        //    _ComBuiness.sendSMS(DefInfo.FirstRefereeMobile, msg8, out sentSMS, out smsStatusText);
                        //}
                        //if (DefInfo.FirstGuideMobile != null)
                        //{
                        //    _ComBuiness.sendSMS(DefInfo.FirstGuideMobile, msg8, out sentSMS, out smsStatusText);
                        //}
                        //if (DefInfo.FirstConsultantMobile != null)
                        //{
                        //    _ComBuiness.sendSMS(DefInfo.FirstConsultantMobile, msg8, out sentSMS, out smsStatusText);
                        //}
                        //if (DefInfo.SecondRefereeMobile != null)
                        //{
                        //    _ComBuiness.sendSMS(DefInfo.SecondRefereeMobile, msg8, out sentSMS, out smsStatusText);
                        //}
                        //if (DefInfo.SecondGuideMobile != null)
                        //{
                        //    _ComBuiness.sendSMS(DefInfo.SecondGuideMobile, msg8, out sentSMS, out smsStatusText);
                        //}
                        //if (DefInfo.SecondConsultantMobile != null)
                        //{
                        //    _ComBuiness.sendSMS(DefInfo.SecondConsultantMobile, msg8, out sentSMS, out smsStatusText);
                        //}
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        #endregion



                        if (StMob != null)
                        {
                            msg = GetTextSmsAcceptRequestByEducation(DefInfo.StudentCode, DefInfo.StudentFullName, "", date, startTime, true, true);
                            _ComBuiness.sendSMS(StMob, msg, out sentSMS, out smsStatusText);

                        }
                        if (DefInfo.FirstRefereeMobile != null)
                        {
                            msg = GetTextSmsAcceptRequestByEducation(DefInfo.StudentCode, DefInfo.StudentFullName, "داور", date, startTime, false, true);
                            _ComBuiness.sendSMS(DefInfo.FirstRefereeMobile, msg, out sentSMS, out smsStatusText);
                            _ComBuiness.sendSMS(DefInfo.FirstRefereeMobile, GetTextSmsAcceptRequestByEducation(), out sentSMS, out smsStatusText);

                        }
                        if (DefInfo.FirstGuideMobile != null)
                        {
                            msg = GetTextSmsAcceptRequestByEducation(DefInfo.StudentCode, DefInfo.StudentFullName, "راهنما", date, startTime, false, true);
                            _ComBuiness.sendSMS(DefInfo.FirstGuideMobile, msg, out sentSMS, out smsStatusText);
                            _ComBuiness.sendSMS(DefInfo.FirstGuideMobile, GetTextSmsAcceptRequestByEducation(), out sentSMS, out smsStatusText);

                        }
                        if (DefInfo.FirstConsultantMobile != null)
                        {
                            msg = GetTextSmsAcceptRequestByEducation(DefInfo.StudentCode, DefInfo.StudentFullName, "مشاور", date, startTime, false, true);
                            _ComBuiness.sendSMS(DefInfo.FirstConsultantMobile, msg, out sentSMS, out smsStatusText);
                            _ComBuiness.sendSMS(DefInfo.FirstConsultantMobile, GetTextSmsAcceptRequestByEducation(), out sentSMS, out smsStatusText);

                        }
                        if (DefInfo.SecondRefereeMobile != null)
                        {
                            msg = GetTextSmsAcceptRequestByEducation(DefInfo.StudentCode, DefInfo.StudentFullName, "داور", date, startTime, false, true);
                            _ComBuiness.sendSMS(DefInfo.SecondRefereeMobile, msg, out sentSMS, out smsStatusText);
                            _ComBuiness.sendSMS(DefInfo.SecondRefereeMobile, GetTextSmsAcceptRequestByEducation(), out sentSMS, out smsStatusText);

                        }
                        if (DefInfo.SecondGuideMobile != null)
                        {
                            msg = GetTextSmsAcceptRequestByEducation(DefInfo.StudentCode, DefInfo.StudentFullName, "راهنما", date, startTime, false, true);
                            _ComBuiness.sendSMS(DefInfo.SecondGuideMobile, msg, out sentSMS, out smsStatusText);
                            _ComBuiness.sendSMS(DefInfo.SecondGuideMobile, GetTextSmsAcceptRequestByEducation(), out sentSMS, out smsStatusText);

                        }
                        if (DefInfo.SecondConsultantMobile != null)
                        {
                             msg = GetTextSmsAcceptRequestByEducation(DefInfo.StudentCode, DefInfo.StudentFullName, "مشاور", date, startTime, false, true);
                            _ComBuiness.sendSMS(DefInfo.SecondConsultantMobile, msg, out sentSMS, out smsStatusText);
                            _ComBuiness.sendSMS(DefInfo.SecondConsultantMobile, GetTextSmsAcceptRequestByEducation(), out sentSMS, out smsStatusText);

                        }
                    }

                    else if (SMSFlag != 1)
                    {
                        #region SendSmsOldComment
                        //msg = "دانشجوی گرامی جلسه دفاع شما در تاریخ " + date + " ساعت: " + startTime + "در" + " " + resourceName + " برگزار میگردد   "
                        //+ "  همچنین دفاع مذکور در سامانه خدمات بخش امورپژوهشی پنل دفاع آنلاین قابل مشاهده میباشد .";
                        //var msg2 = " استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + "در" + " " + resourceName + " برگزار میگردد، شما به عنوان استاد داور ایشان انتخاب شده اید "
                        //+ " همچنین دفاع مذکور در سامانه خدمات بخش امورپژوهشی پنل دفاع آنلاین قابل مشاهده میباشد . ";
                        //var msg3 = " استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + "در" + " " + resourceName + "برگزار میگردد، شما به عنوان استاد راهنما ایشان انتخاب شده اید "
                        //+ " همچنین دفاع مذکور در سامانه خدمات بخش امورپژوهشی پنل دفاع آنلاین قابل مشاهده میباشد .";
                        //var msg4 = " استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + "در" + " " + resourceName + "برگزار میگردد، شما به عنوان استاد مشاور ایشان انتخاب شده اید "
                        //+ " همچنین دفاع مذکور در سامانه خدمات بخش امورپژوهشی پنل دفاع آنلاین قابل مشاهده میباشد .";
                        //var msg5 = " استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + "در" + " " + resourceName + "برگزار میگردد، شما به عنوان استاد داور ایشان انتخاب شده اید "
                        //+ " همچنین دفاع مذکور در سامانه خدمات بخش امورپژوهشی پنل دفاع آنلاین قابل مشاهده میباشد .";
                        //var msg6 = " استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + "در" + " " + resourceName + "برگزار میگردد، شما به عنوان استاد راهنما ایشان انتخاب شده اید "
                        //+ "  همچنین دفاع مذکور در سامانه خدمات بخش امورپژوهشی پنل دفاع آنلاین قابل مشاهده میباشد .";
                        //var msg7 = " استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + "در" + " " + resourceName + "برگزار میگردد، شما به عنوان استاد مشاور ایشان انتخاب شده اید "
                        //+ "  همچنین دفاع مذکور در سامانه خدمات بخش امورپژوهشی پنل دفاع آنلاین قابل مشاهده میباشد .";




                        //if (inCirculationRequest != null && (inCirculationRequest.OnlineTeacherRole == "guide" || inCirculationRequest.OnlineTeacherRole == "consultant"))
                        //{
                        //    if (inCirculationRequest.OnlineTeacherRole == "guide")
                        //    {

                        //        if (inCirculationRequest.OnlineFirstTeacherId.ToString() == DefInfo.FirstGuideId)
                        //        {
                        //            // msg3 = "  استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + " در لینک" + " " + resDB.GetResourcelink(Convert.ToInt32(resId)) + "برگزار میگردد، شما به عنوان استاد راهنما ایشان انتخاب شده اید   ";//لینک 
                        //            msg3 =

                        //               " استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + "در" + " " + resourceName + "برگزار میگردد، شما به عنوان استاد راهنما ایشان انتخاب شده اید "
                        //             + "  برای ورود به جلسه آنلاین دفاع به سامانه خدمات الکترونیکی به ادرس زیر بخش امور پژوهشی قسمت دفاع آنلاین مراجعه فرمایید . "
                        //             + " service.iauec.ac.ir ";
                        //        }
                        //        else
                        //        {
                        //            if (inCirculationRequest.OnlineFirstTeacherId.ToString() == DefInfo.SecondGuideId)
                        //            {
                        //                // msg6 = "  استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + "در لینک " + " " + resDB.GetResourcelink(Convert.ToInt32(resId)) + "برگزار میگردد، شما به عنوان استاد راهنما ایشان انتخاب شده اید   ";//اینو تغییر بده
                        //                msg6 = " استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + "در" + " " + resourceName + "برگزار میگردد، شما به عنوان استاد راهنما ایشان انتخاب شده اید "
                        //             + "  برای ورود به جلسه آنلاین دفاع به سامانه خدمات الکترونیکی به ادرس زیر بخش امور پژوهشی قسمت دفاع آنلاین مراجعه فرمایید . "
                        //             + " service.iauec.ac.ir ";
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        if (inCirculationRequest.OnlineTeacherRole == "consultant")
                        //        {

                        //            if (inCirculationRequest.OnlineFirstTeacherId.ToString() == DefInfo.FirstConsultantId)
                        //            {
                        //                // msg4 = "  استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + "در لینک " + " " + resDB.GetResourcelink(Convert.ToInt32(resId)) + "برگزار میگردد، شما به عنوان استاد مشاور ایشان انتخاب شده اید    ";//اینو تغییر بده
                        //                msg4 = " استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + "در" + " " + resourceName + "برگزار میگردد، شما به عنوان استاد مشاور ایشان انتخاب شده اید "
                        //                    + "  برای ورود به جلسه آنلاین دفاع به سامانه خدمات الکترونیکی به ادرس زیر بخش امور پژوهشی قسمت دفاع آنلاین مراجعه فرمایید . "
                        //                        + " service.iauec.ac.ir ";
                        //            }
                        //            else
                        //            {
                        //                if (inCirculationRequest.OnlineFirstTeacherId.ToString() == DefInfo.SecondConsultantId)
                        //                {
                        //                    //   msg7 = "  استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + "در لینک " + " " + resDB.GetResourcelink(Convert.ToInt32(resId)) + "برگزار میگردد، شما به عنوان استاد مشاور ایشان انتخاب شده اید   ";//اینو تغییر بده
                        //                    msg7 = " استاد گرامی جلسه دفاع دانشجو " + DefInfo.StudentFullName + " " + DefInfo.StudentCode + " در تاریخ " + date + " ساعت " + startTime + "در" + " " + resourceName + "برگزار میگردد، شما به عنوان استاد مشاور ایشان انتخاب شده اید "
                        //                    + "  برای ورود به جلسه آنلاین دفاع به سامانه خدمات الکترونیکی به ادرس زیر بخش امور پژوهشی قسمت دفاع آنلاین مراجعه فرمایید . "
                        //                        + " service.iauec.ac.ir ";
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        #endregion

                        #region ss

                        //var msg8 = "فرهیخته گرامی؛ با سلام، بنابر بخشنامه سازمان مركزي هرگونه اجبار دانشجو به پذيرايي و تهيه هديه در جلسات دفاع از پايان نامه از جانب گروه، اساتيد، كاركنان و ... ممنوع مي باشد.دانشجوياني كه شخصاً مايل به انجام پذيرايي مي باشند مي توانند اين كار را در حد متعارف و منحصر به آب معدني، شيريني و ميوه انجام دهند.ضمناً اهدا و دريافت هديه به هر نحو ممنوع بوده و در صورت مشاهده با متخلف (دانشجو، كارمند و اركان دفاع) برخورد قانوني خواهد شد.  واحد الکترونیکی دانشگاه آزاد اسلامی ";
                        //var msg8 = "فرهیخته گرامی؛ با سلام و احترام،\r\n از این که با رعایت شئونات اخلاقی و اسلامی در جلسات دفاع از پایان نامه، مطابق با بخشنامه \"دستورالعمل آراستگی و شئون فرهنگی و رفتاری\"، ساحت معنوی و مقدس علم را پاس می دارید، سپاسگزاریم";

                        //_ComBuiness.sendSMS(StMob, msg8, out sentSMS, out smsStatusText);
                        //_ComBuiness.sendSMS(DefInfo.FirstRefereeMobile, msg8, out sentSMS, out smsStatusText);
                        //_ComBuiness.sendSMS(DefInfo.FirstGuideMobile, msg8, out sentSMS, out smsStatusText);
                        //_ComBuiness.sendSMS(DefInfo.FirstConsultantMobile, msg8, out sentSMS, out smsStatusText);
                        //_ComBuiness.sendSMS(DefInfo.SecondRefereeMobile, msg8, out sentSMS, out smsStatusText);
                        //_ComBuiness.sendSMS(DefInfo.SecondGuideMobile, msg8, out sentSMS, out smsStatusText);
                        //_ComBuiness.sendSMS(DefInfo.SecondConsultantMobile, msg8, out sentSMS, out smsStatusText);

                        //_ComBuiness.sendSMS(StMob, msg, out sentSMS, out smsStatusText);
                        //_ComBuiness.sendSMS(DefInfo.FirstRefereeMobile, msg2, out sentSMS, out smsStatusText);
                        //_ComBuiness.sendSMS(DefInfo.FirstGuideMobile, msg3, out sentSMS, out smsStatusText);
                        //_ComBuiness.sendSMS(DefInfo.FirstConsultantMobile, msg4, out sentSMS, out smsStatusText);
                        //_ComBuiness.sendSMS(DefInfo.SecondRefereeMobile, msg5, out sentSMS, out smsStatusText);
                        //_ComBuiness.sendSMS(DefInfo.SecondGuideMobile, msg6, out sentSMS, out smsStatusText);
                        //_ComBuiness.sendSMS(DefInfo.SecondConsultantMobile, msg7, out sentSMS, out smsStatusText);
                        //////////////////////

                        // var msg8 = "فرهیخته گرامی؛ با سلام، بنابر بخشنامه سازمان مركزي هرگونه اجبار دانشجو به پذيرايي و تهيه هديه در جلسات دفاع از پايان نامه از جانب گروه، اساتيد، كاركنان و ... ممنوع مي باشد.دانشجوياني كه شخصاً مايل به انجام پذيرايي مي باشند مي توانند اين كار را در حد متعارف و منحصر به آب معدني، شيريني و ميوه انجام دهند.ضمناً اهدا و دريافت هديه به هر نحو ممنوع بوده و در صورت مشاهده با متخلف (دانشجو، كارمند و اركان دفاع) برخورد قانوني خواهد شد.  واحد الکترونیکی دانشگاه آزاد اسلامی ";

                        //if (StMob != null)
                        //{
                        //    _ComBuiness.sendSMS(StMob, msg8, out sentSMS, out smsStatusText);
                        //}
                        //if (DefInfo.FirstRefereeMobile != null)
                        //{
                        //    _ComBuiness.sendSMS(DefInfo.FirstRefereeMobile, msg8, out sentSMS, out smsStatusText);
                        //}
                        //if (DefInfo.FirstGuideMobile != null)
                        //{
                        //    _ComBuiness.sendSMS(DefInfo.FirstGuideMobile, msg8, out sentSMS, out smsStatusText);
                        //}
                        //if (DefInfo.FirstConsultantMobile != null)
                        //{
                        //    _ComBuiness.sendSMS(DefInfo.FirstConsultantMobile, msg8, out sentSMS, out smsStatusText);
                        //}
                        //if (DefInfo.SecondRefereeMobile != null)
                        //{
                        //    _ComBuiness.sendSMS(DefInfo.SecondRefereeMobile, msg8, out sentSMS, out smsStatusText);
                        //}
                        //if (DefInfo.SecondGuideMobile != null)
                        //{
                        //    _ComBuiness.sendSMS(DefInfo.SecondGuideMobile, msg8, out sentSMS, out smsStatusText);
                        //}
                        //if (DefInfo.SecondConsultantMobile != null)
                        //{
                        //    _ComBuiness.sendSMS(DefInfo.SecondConsultantMobile, msg8, out sentSMS, out smsStatusText);
                        //}
                        //////////////////////////////////////////////////////////

                        //if (StMob != null)
                        //{
                        //    _ComBuiness.sendSMS(StMob, msg8, out sentSMS, out smsStatusText);
                        //}

                        #endregion ss



                        if (StMob != null)
                        {
                             msg = GetTextSmsAcceptRequestByEducation(DefInfo.StudentCode, DefInfo.StudentFullName, "", date, startTime, true, false);
                            _ComBuiness.sendSMS(StMob, msg, out sentSMS, out smsStatusText);
                        }
                        if (DefInfo.FirstRefereeMobile != null)
                        {
                            msg = GetTextSmsAcceptRequestByEducation(DefInfo.StudentCode, DefInfo.StudentFullName, "داور", date, startTime, false, false);
                            _ComBuiness.sendSMS(DefInfo.FirstRefereeMobile, msg, out sentSMS, out smsStatusText);
                            _ComBuiness.sendSMS(DefInfo.FirstRefereeMobile, GetTextSmsAcceptRequestByEducation(), out sentSMS, out smsStatusText);
                        }
                        if (DefInfo.FirstGuideMobile != null)
                        {
                            msg = GetTextSmsAcceptRequestByEducation(DefInfo.StudentCode, DefInfo.StudentFullName, "راهنما", date, startTime, false, false);
                            _ComBuiness.sendSMS(DefInfo.FirstGuideMobile, msg, out sentSMS, out smsStatusText);
                            _ComBuiness.sendSMS(DefInfo.FirstGuideMobile, GetTextSmsAcceptRequestByEducation(), out sentSMS, out smsStatusText);
                        }
                        if (DefInfo.FirstConsultantMobile != null)
                        {
                            msg = GetTextSmsAcceptRequestByEducation(DefInfo.StudentCode, DefInfo.StudentFullName, "مشاور", date, startTime, false, false);
                            _ComBuiness.sendSMS(DefInfo.FirstConsultantMobile, msg, out sentSMS, out smsStatusText);
                            _ComBuiness.sendSMS(DefInfo.FirstConsultantMobile, GetTextSmsAcceptRequestByEducation(), out sentSMS, out smsStatusText);
                        }
                        if (DefInfo.SecondRefereeMobile != null)
                        {
                            msg = GetTextSmsAcceptRequestByEducation(DefInfo.StudentCode, DefInfo.StudentFullName, "داور", date, startTime, false, false);
                            _ComBuiness.sendSMS(DefInfo.SecondRefereeMobile, msg, out sentSMS, out smsStatusText);
                            _ComBuiness.sendSMS(DefInfo.SecondRefereeMobile, GetTextSmsAcceptRequestByEducation(), out sentSMS, out smsStatusText);
                        }
                        if (DefInfo.SecondGuideMobile != null)
                        {
                            msg = GetTextSmsAcceptRequestByEducation(DefInfo.StudentCode, DefInfo.StudentFullName, "راهنما", date, startTime, false, false);
                            _ComBuiness.sendSMS(DefInfo.SecondGuideMobile, msg, out sentSMS, out smsStatusText);
                            _ComBuiness.sendSMS(DefInfo.SecondGuideMobile, GetTextSmsAcceptRequestByEducation(), out sentSMS, out smsStatusText);
                        }
                        if (DefInfo.SecondConsultantMobile != null)
                        {
                            msg = GetTextSmsAcceptRequestByEducation(DefInfo.StudentCode, DefInfo.StudentFullName, "مشاور", date, startTime, false, false);
                            _ComBuiness.sendSMS(DefInfo.SecondConsultantMobile, msg, out sentSMS, out smsStatusText);
                            _ComBuiness.sendSMS(DefInfo.SecondConsultantMobile, GetTextSmsAcceptRequestByEducation(), out sentSMS, out smsStatusText);
                        }

                        //set sendSMS=1
                        ActiveSendSMSFlag(reqID);
                    }







                    //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    //1=man , 2=woman , 0=jensiyat


                    //var msg9 = "فرهیخته گرامی، با سلام، بنابر بخشنامه سازمان مركزي هرگونه اجبار دانشجو به پذيرايي و تهيه هديه در جلسات دفاع از پايان نامه از جانب گروه، اساتيد، كاركنان و ... ممنوع مي باشد. دانشجوياني كه شخصاً مايل به انجام پذيرايي مي باشند مي توانند اين كار را در حد متعارف و منحصر به آب معدني، شيريني و ميوه انجام دهند. ضمناً اهدا و دريافت هديه به هر نحو ممنوع بوده و در صورت مشاهده با متخلف (دانشجو، كارمند و اركان دفاع) برخورد قانوني خواهد شد. واحد الکترونیکی دانشگاه آزاد اسلامی ";
                    //  var mgs00 = "با سلام و احترام،\r\nاز اين كه با رعايت شئونات اخلاقي و اسلامي در جلسات دفاع از پايان نامه، مطابق با بخشنامه \" دستورالعمل آراستگي و شئون فرهنگي و رفتاري\"، ساحت معنوي و مقدس علم را پاس مي داريد، سپاسگزاريم. واحد الكترونيكي دانشگاه آزاد اسلامي";
                    var mgs00 = "با سلام و احترام،\r\nاز اين كه با رعايت شئونات اخلاقي و اسلامي در جلسات دفاع از پايان نامه آنلاین ،  ساحت معنوي و مقدس علم را پاس داشته و دانشگاه را در جهت برگزاری مناسب‌تر جلسات دفاع از پایان‌نامه یاری می‌رسانید، سپاسگزاريم. واحد الكترونيكي دانشگاه آزاد اسلامي";

                    var OstMsg = "استاد گرامي،";
                    var xx = "سركار خانم دكتر / جناب آقاي دكتر";
                    var txt = "";
                    if (StMob != null)
                    {

                        //if (resDB.IsGreaterThan30Days_LastTime_Sms_Sent(StMob))
                        //{
                        //  //  _ComBuiness.sendSMS(StMob, msg9, out sentSMS, out smsStatusText);
                        //    resDB.InsertIntoDefenceSmsLog(DefInfo.StudentFullName, DefInfo.StudentCode, int.Parse(DefInfo.RequestId), StMob, true, smsStatusText, DateTime.Now);

                        //    txt = $"sentSMS : {sentSMS}\t\tsmsStatusText : {smsStatusText}";
                        //    this.ServerLogger(StMob, txt, absPath);
                        //    resDB.AddOrUpdate_tbl_StudentDefence_Log(StMob);
                        //}


                        if (!resDB.HasSentSmsTodayForShounat(StMob, true))
                        {
                            var msgStudent = "دانشجوی گرامي،";
                            string StudentTitle = (!string.IsNullOrEmpty(DefInfo.studentGender) && DefInfo.studentGender != "0") ? (DefInfo.studentGender == "1" ? " جناب آقاي " : " سركار خانم ") : "";
                            msgStudent += StudentTitle + " " + DefInfo.StudentFullName;
                            msgStudent += "\r\n";
                            msgStudent += mgs00;
                            _ComBuiness.sendSMS(StMob, msgStudent, out sentSMS, out smsStatusText);

                            txt = "";
                            txt = $"sentSMS : {sentSMS}\t\tsmsStatusText : {smsStatusText}";
                            this.ServerLogger(StMob, txt, absPath);
                            resDB.InsertIntoDefenceSmsLog(DefInfo.StudentFullName, DefInfo.StudentCode, int.Parse(DefInfo.RequestId), StMob, false, smsStatusText, DateTime.Now);
                            resDB.AddOrUpdate_tbl_StudentDefence_Log(StMob, true);
                        }
                    }

                    if (DefInfo.FirstGuideMobile != null)
                    {
                        //if (resDB.IsGreaterThan30Days_LastTime_Sms_Sent(DefInfo.FirstGuideMobile))
                        //{
                        //    //_ComBuiness.sendSMS(DefInfo.FirstGuideMobile, msg9, out sentSMS, out smsStatusText);

                        //    txt = "";
                        //    txt = $"sentSMS : {sentSMS}\t\tsmsStatusText : {smsStatusText}";
                        //    this.ServerLogger(DefInfo.FirstGuideMobile, txt, absPath);

                        //    resDB.InsertIntoDefenceSmsLog(DefInfo.FirstGuideFullName, DefInfo.FirstGuideId, int.Parse(DefInfo.RequestId), DefInfo.FirstGuideMobile, true, smsStatusText, DateTime.Now);
                        //    resDB.AddOrUpdate_tbl_StudentDefence_Log(DefInfo.FirstGuideMobile);
                        //}

                        if (!string.IsNullOrEmpty(DefInfo.FirstGuideId) && DefInfo.FirstGuideId != "0")
                        {
                            if (!resDB.HasSentSmsTodayForShounat(DefInfo.FirstGuideMobile, true))
                            {
                                string msg10 = "";
                                string FirstGuideTitle = (!string.IsNullOrEmpty(DefInfo.FirstGuideGender) && DefInfo.FirstGuideGender != "0") ? (DefInfo.FirstGuideGender == "1" ? " جناب آقاي دكتر " : " سركار خانم دكتر ") : "";
                                if (!string.IsNullOrEmpty(FirstGuideTitle))
                                    msg10 = OstMsg + " " + FirstGuideTitle + " " + DefInfo.FirstGuideFullName + "\r\n" + mgs00;
                                else
                                    msg10 = OstMsg + " " + xx + " " + DefInfo.FirstGuideFullName + "\r\n" + mgs00;

                                _ComBuiness.sendSMS(DefInfo.FirstGuideMobile, msg10, out sentSMS, out smsStatusText);

                                txt = "";
                                txt = $"sentSMS : {sentSMS}\t\tsmsStatusText : {smsStatusText}";
                                this.ServerLogger(DefInfo.FirstGuideMobile, txt, absPath);

                                resDB.InsertIntoDefenceSmsLog(DefInfo.FirstGuideFullName, DefInfo.FirstGuideId, int.Parse(DefInfo.RequestId), DefInfo.FirstGuideMobile, false, smsStatusText, DateTime.Now);
                                resDB.AddOrUpdate_tbl_StudentDefence_Log(DefInfo.FirstGuideMobile, true);
                            }
                        }
                    }

                    if (DefInfo.SecondGuideMobile != null)
                    {
                        //if (resDB.IsGreaterThan30Days_LastTime_Sms_Sent(DefInfo.SecondGuideMobile))
                        //{
                        //    //_ComBuiness.sendSMS(DefInfo.SecondGuideMobile, msg9, out sentSMS, out smsStatusText);

                        //    txt = "";
                        //    txt = $"sentSMS : {sentSMS}\t\tsmsStatusText : {smsStatusText}";
                        //    this.ServerLogger(DefInfo.SecondGuideMobile, txt, absPath);

                        //    resDB.InsertIntoDefenceSmsLog(DefInfo.SecondGuideFullName, DefInfo.SecondGuideId, int.Parse(DefInfo.RequestId), DefInfo.SecondGuideMobile, true, smsStatusText, DateTime.Now);
                        //    resDB.AddOrUpdate_tbl_StudentDefence_Log(DefInfo.SecondGuideMobile);
                        //}

                        if (!string.IsNullOrEmpty(DefInfo.SecondGuideId) && DefInfo.SecondGuideId != "0")
                        {

                            //string msg11 = $" استاد گرامي، { SecondGuideTitle } با سلام و احترام ،از اینکه با رعایت شئونات اخلاقی و اسلامی در جلسات دفاع از پایان نامه، مطابق با بشخنامه ؛دستور العمل آراستگی و شئون فرهنگی و هنری و رفتاری؛ساحت معنوی و مقدس علم را پاس می دارید،سپاسگزاریم ";
                            if (!resDB.HasSentSmsTodayForShounat(DefInfo.SecondGuideMobile, true))
                            {
                                string msg11 = "";
                                string SecondGuideTitle = (!string.IsNullOrEmpty(DefInfo.SecondGuideGender) && DefInfo.SecondGuideGender != "0") ? (DefInfo.SecondGuideGender == "1" ? " جناب آقاي دكتر " : " سركار خانم دكتر ") : "";

                                if (!string.IsNullOrEmpty(SecondGuideTitle))
                                    msg11 = OstMsg + SecondGuideTitle + " " + DefInfo.SecondGuideFullName + "\r\n" + mgs00;
                                else
                                    msg11 = OstMsg + " " + xx + " " + DefInfo.SecondGuideFullName + "\r\n" + mgs00;
                                _ComBuiness.sendSMS(DefInfo.SecondGuideMobile, msg11, out sentSMS, out smsStatusText);

                                txt = "";
                                txt = $"sentSMS : {sentSMS}\t\tsmsStatusText : {smsStatusText}";
                                this.ServerLogger(DefInfo.SecondGuideMobile, txt, absPath);

                                resDB.InsertIntoDefenceSmsLog(DefInfo.SecondGuideFullName, DefInfo.SecondGuideId, int.Parse(DefInfo.RequestId), DefInfo.SecondGuideMobile, false, smsStatusText, DateTime.Now);
                                resDB.AddOrUpdate_tbl_StudentDefence_Log(DefInfo.SecondGuideMobile, true);
                            }

                        }

                    }

                    if (DefInfo.FirstRefereeMobile != null)
                    {
                        //if (resDB.IsGreaterThan30Days_LastTime_Sms_Sent(DefInfo.FirstRefereeMobile))
                        //{
                        //    //_ComBuiness.sendSMS(DefInfo.FirstRefereeMobile, msg9, out sentSMS, out smsStatusText);

                        //    txt = "";
                        //    txt = $"sentSMS : {sentSMS}\t\tsmsStatusText : {smsStatusText}";
                        //    this.ServerLogger(DefInfo.FirstRefereeMobile, txt, absPath);

                        //    resDB.InsertIntoDefenceSmsLog(DefInfo.FirstRefereeFullName, DefInfo.FirstRefereeId, int.Parse(DefInfo.RequestId), DefInfo.FirstRefereeMobile, true, smsStatusText, DateTime.Now);
                        //    resDB.AddOrUpdate_tbl_StudentDefence_Log(DefInfo.FirstRefereeMobile);
                        //}

                        if (!string.IsNullOrEmpty(DefInfo.FirstRefereeId) && DefInfo.FirstRefereeId != "0")
                        {
                            //string msg12 = $" استاد گرامي، { FirstRefereeTitle } با سلام و احترام ،از اینکه با رعایت شئونات اخلاقی و اسلامی در جلسات دفاع از پایان نامه، مطابق با بشخنامه ؛دستور العمل آراستگی و شئون فرهنگی و هنری و رفتاری؛ساحت معنوی و مقدس علم را پاس می دارید،سپاسگزاریم ";
                            if (!resDB.HasSentSmsTodayForShounat(DefInfo.FirstRefereeMobile, true))
                            {
                                string msg12 = "";
                                string FirstRefereeTitle = (!string.IsNullOrEmpty(DefInfo.FirstRefereeGender) && DefInfo.FirstRefereeGender != "0") ? (DefInfo.FirstRefereeGender == "1" ? " جناب آقاي دكتر " : " سركار خانم دكتر ") : "";
                                if (!string.IsNullOrEmpty(FirstRefereeTitle))
                                    msg12 = OstMsg + " " + FirstRefereeTitle + " " + DefInfo.FirstRefereeFullName + "\r\n" + mgs00;
                                else
                                    msg12 = OstMsg + " " + xx + " " + DefInfo.FirstRefereeFullName + "\r\n" + mgs00;
                                _ComBuiness.sendSMS(DefInfo.FirstRefereeMobile, msg12, out sentSMS, out smsStatusText);

                                txt = "";
                                txt = $"sentSMS : {sentSMS}\t\tsmsStatusText : {smsStatusText}";
                                this.ServerLogger(DefInfo.FirstRefereeMobile, txt, absPath);

                                resDB.InsertIntoDefenceSmsLog(DefInfo.FirstRefereeFullName, DefInfo.FirstRefereeId, int.Parse(DefInfo.RequestId), DefInfo.FirstRefereeMobile, false, smsStatusText, DateTime.Now);
                                resDB.AddOrUpdate_tbl_StudentDefence_Log(DefInfo.FirstRefereeMobile, true);
                            }
                        }
                    }

                    if (DefInfo.SecondRefereeMobile != null)
                    {
                        //if (resDB.IsGreaterThan30Days_LastTime_Sms_Sent(DefInfo.SecondRefereeMobile))
                        //{
                        //   // _ComBuiness.sendSMS(DefInfo.SecondRefereeMobile, msg9, out sentSMS, out smsStatusText);

                        //    txt = "";
                        //    txt = $"sentSMS : {sentSMS}\t\tsmsStatusText : {smsStatusText}";
                        //    this.ServerLogger(DefInfo.SecondRefereeMobile, txt, absPath);

                        //    resDB.InsertIntoDefenceSmsLog(DefInfo.SecondRefereeFullName, DefInfo.SecondRefereeId, int.Parse(DefInfo.RequestId), DefInfo.SecondRefereeMobile, true, smsStatusText, DateTime.Now);
                        //    resDB.AddOrUpdate_tbl_StudentDefence_Log(DefInfo.SecondRefereeMobile);
                        //}

                        if (!string.IsNullOrEmpty(DefInfo.SecondRefereeId) && DefInfo.SecondRefereeId != "0")
                        {
                            //string msg13 = $" استاد گرامي، { SecondRefereeTitle } با سلام و احترام ،از اینکه با رعایت شئونات اخلاقی و اسلامی در جلسات دفاع از پایان نامه، مطابق با بشخنامه ؛دستور العمل آراستگی و شئون فرهنگی و هنری و رفتاری؛ساحت معنوی و مقدس علم را پاس می دارید،سپاسگزاریم ";
                            if (!resDB.HasSentSmsTodayForShounat(DefInfo.SecondRefereeMobile, true))
                            {
                                string msg13 = "";
                                string SecondRefereeTitle = (!string.IsNullOrEmpty(DefInfo.SecondRefereeGender) && DefInfo.SecondRefereeGender != "0") ? (DefInfo.SecondRefereeGender == "1" ? " جناب آقاي دكتر " : " سركار خانم دكتر ") : "";
                                if (!string.IsNullOrEmpty(SecondRefereeTitle))
                                    msg13 = OstMsg + " " + SecondRefereeTitle + " " + DefInfo.SecondRefereeFullName + "\r\n" + mgs00;
                                else
                                    msg13 = OstMsg + " " + xx + " " + DefInfo.SecondRefereeFullName + "\r\n" + mgs00;

                                _ComBuiness.sendSMS(DefInfo.SecondRefereeMobile, msg13, out sentSMS, out smsStatusText);

                                txt = "";
                                txt = $"sentSMS : {sentSMS}\t\tsmsStatusText : {smsStatusText}";
                                this.ServerLogger(DefInfo.SecondRefereeMobile, txt, absPath);

                                resDB.InsertIntoDefenceSmsLog(DefInfo.SecondRefereeFullName, DefInfo.SecondRefereeId, int.Parse(DefInfo.RequestId), DefInfo.SecondRefereeMobile, false, smsStatusText, DateTime.Now);
                                resDB.AddOrUpdate_tbl_StudentDefence_Log(DefInfo.SecondRefereeMobile, true);
                            }


                        }
                    }

                    if (DefInfo.FirstConsultantMobile != null)
                    {
                        //if (resDB.IsGreaterThan30Days_LastTime_Sms_Sent(DefInfo.FirstConsultantMobile))
                        //{
                        //   // _ComBuiness.sendSMS(DefInfo.FirstConsultantMobile, msg9, out sentSMS, out smsStatusText);
                        //    txt = "";
                        //    txt = $"sentSMS : {sentSMS}\t\tsmsStatusText : {smsStatusText}";
                        //    this.ServerLogger(DefInfo.FirstConsultantMobile, txt, absPath);

                        //    resDB.InsertIntoDefenceSmsLog(DefInfo.FirstConsultantFullName, DefInfo.FirstConsultantId, int.Parse(DefInfo.RequestId), DefInfo.FirstConsultantMobile, true, smsStatusText, DateTime.Now);
                        //    resDB.AddOrUpdate_tbl_StudentDefence_Log(DefInfo.FirstConsultantMobile);
                        //}

                        if (!string.IsNullOrEmpty(DefInfo.FirstConsultantId) && DefInfo.FirstConsultantId != "0")
                        {
                            //string msg14 = $" استاد گرامي، { FirstConsultantTitle } با سلام و احترام ،از اینکه با رعایت شئونات اخلاقی و اسلامی در جلسات دفاع از پایان نامه، مطابق با بشخنامه ؛دستور العمل آراستگی و شئون فرهنگی و هنری و رفتاری؛ساحت معنوی و مقدس علم را پاس می دارید،سپاسگزاریم ";

                            if (!resDB.HasSentSmsTodayForShounat(DefInfo.FirstConsultantMobile, true))
                            {
                                string msg14 = "";
                                string FirstConsultantTitle = (!string.IsNullOrEmpty(DefInfo.FirstConsultantGender) && DefInfo.FirstConsultantGender != "0") ? (DefInfo.FirstConsultantGender == "1" ? " جناب آقاي دكتر " : " سركار خانم دكتر ") : "";
                                if (!string.IsNullOrEmpty(FirstConsultantTitle))
                                    msg14 = OstMsg + " " + FirstConsultantTitle + " " + DefInfo.FirstConsultantFullName + "\r\n" + mgs00;
                                else
                                    msg14 = OstMsg + " " + xx + " " + DefInfo.FirstConsultantFullName + "\r\n" + mgs00;

                                _ComBuiness.sendSMS(DefInfo.FirstConsultantMobile, msg14, out sentSMS, out smsStatusText);
                                txt = "";
                                txt = $"sentSMS : {sentSMS}\t\tsmsStatusText : {smsStatusText}";
                                this.ServerLogger(DefInfo.FirstConsultantMobile, txt, absPath);


                                resDB.InsertIntoDefenceSmsLog(DefInfo.FirstConsultantFullName, DefInfo.FirstConsultantId, int.Parse(DefInfo.RequestId), DefInfo.FirstConsultantMobile, false, smsStatusText, DateTime.Now);
                                resDB.AddOrUpdate_tbl_StudentDefence_Log(DefInfo.FirstConsultantMobile, true);
                            }
                        }


                    }

                    if (DefInfo.SecondConsultantMobile != null)
                    {
                        //if (resDB.IsGreaterThan30Days_LastTime_Sms_Sent(DefInfo.SecondConsultantMobile))
                        //{
                        //    //_ComBuiness.sendSMS(DefInfo.SecondConsultantMobile, msg9, out sentSMS, out smsStatusText);

                        //    txt = "";
                        //    txt = $"sentSMS : {sentSMS}\t\tsmsStatusText : {smsStatusText}";
                        //    this.ServerLogger(DefInfo.SecondConsultantMobile, txt, absPath);


                        //    resDB.InsertIntoDefenceSmsLog(DefInfo.SecondConsultantFullName, DefInfo.SecondConsultantId, int.Parse(DefInfo.RequestId), DefInfo.SecondConsultantMobile, true, smsStatusText, DateTime.Now);
                        //    resDB.AddOrUpdate_tbl_StudentDefence_Log(DefInfo.SecondConsultantMobile);
                        //}

                        if (!string.IsNullOrEmpty(DefInfo.SecondConsultantId) && DefInfo.SecondConsultantId != "0")
                        {
                            //string msg15 = $" استاد گرامي، { SecondConsultantTitle } با سلام و احترام ،از اینکه با رعایت شئونات اخلاقی و اسلامی در جلسات دفاع از پایان نامه، مطابق با بشخنامه ؛دستور العمل آراستگی و شئون فرهنگی و هنری و رفتاری؛ساحت معنوی و مقدس علم را پاس می دارید،سپاسگزاریم ";

                            if (!resDB.HasSentSmsTodayForShounat(DefInfo.SecondConsultantMobile, true))
                            {
                                string msg15 = "";
                                string SecondConsultantTitle = (!string.IsNullOrEmpty(DefInfo.SecondConsultantGender) && DefInfo.SecondConsultantGender != "0") ? (DefInfo.SecondConsultantGender == "1" ? " جناب آقاي دكتر " : " سركار خانم دكتر ") : "";
                                if (!string.IsNullOrEmpty(SecondConsultantTitle))
                                    msg15 = OstMsg + " " + SecondConsultantTitle + " " + DefInfo.SecondConsultantFullName + "\r\n" + mgs00;
                                else
                                    msg15 = OstMsg + " " + xx + " " + DefInfo.SecondConsultantFullName + "\r\n" + mgs00;
                                _ComBuiness.sendSMS(DefInfo.SecondConsultantMobile, msg15, out sentSMS, out smsStatusText);

                                txt = "";
                                txt = $"sentSMS : {sentSMS}\t\tsmsStatusText : {smsStatusText}";
                                this.ServerLogger(DefInfo.SecondConsultantMobile, txt, absPath);

                                resDB.InsertIntoDefenceSmsLog(DefInfo.SecondConsultantFullName, DefInfo.SecondConsultantId, int.Parse(DefInfo.RequestId), DefInfo.SecondConsultantMobile, false, smsStatusText, DateTime.Now);
                                resDB.AddOrUpdate_tbl_StudentDefence_Log(DefInfo.SecondConsultantMobile, true);
                            }
                        }

                    }

                    //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

                }
                else if (typeId == 0)
                {       //حذف دانشکده
                    msg = "دانشجوی گرامی درخواست دفاع شما حذف گردید برای ثبت درخواست جدید به سامانه رزرواسیون جلسه دفاع مراجعه نمایید";
                    _ComBuiness.sendSMS(StMob, msg, out sentSMS, out smsStatusText);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }




        }


        public DataTable GetAllAccepetedDefenceRequests(int daneshId)
        {
            return requestDB.GetAllAccepetedDefenceRequests(daneshId);
        }


        public DataTable GetStudentDefenceListForPazhoohesh_Report(int isReport = 0, string term = null)
        {
            return requestDB.GetStudentDefenceListForPazhoohesh_Report(isReport, term);
        }

        public DataTable GetDefenceAcceptInformationReports(int collegeId, string startDate, string endDate, int dateType = 0)
        {
            return requestDB.GetDefenceAcceptInformationReports(collegeId, startDate, endDate, dateType);
        }

        public DataTable GetHeldDefenseMeetingReports(int collegeId, string startDate, string endDate, int dateType = 0)
        {
            return requestDB.GetHeldDefenseMeetingReports(collegeId, startDate, endDate, dateType);
        }
        public DataTable GetListOfOnlinePlayDefenceReports(int collegeId, string startDate, string endDate, int dateType = 0)
        {
            return requestDB.GetListOfOnlinePlayDefenceReports(collegeId, startDate, endDate, dateType);
        }

        public DataTable GetListOfOnlineProfessorDefenceReports(int collegeId, string startDate, string endDate, int dateType = 0)
        {
            return requestDB.GetListOfOnlineProfessorDefenceReports(collegeId, startDate, endDate, dateType);
        }
        public DataTable NumberOfDefensesRequestedReport(int collegeId, string startDate, string endDate, int dateType = 0)
        {
            return requestDB.NumberOfDefensesRequestedReport(collegeId, startDate, endDate, dateType);
        }
        public DataTable HeldStudentDefenseRequestReport(int collegeId, string startDate, string endDate, int dateType = 0)
        {
            return requestDB.HeldStudentDefenseRequestReport(collegeId, startDate, endDate, dateType);
        }
        public DataTable GetOnlinePlayDefenceReports(int collegeId, string startDate, string endDate, int dateType = 0)
        {
            return requestDB.GetOnlinePlayDefenceReports(collegeId, startDate, endDate, dateType);
        }

        public DataTable GetOnlineProfessorDefenceReports(int collegeId, string startDate, string endDate, int dateType = 0)
        {
            return requestDB.GetOnlineProfessorDefenceReports(collegeId, startDate, endDate, dateType);
        }
        public DataTable GetAllAccepetedDefenceRequests2(int daneshId)
        {
            return requestDB.GetAllAccepetedDefenceRequests2(daneshId);
        }

        public static bool IsNotSpecifiedDay(DayOfWeek dayOfWeek, List<RequestDateTime> requestDateTimes)
        {

            foreach (var item in requestDateTimes)
            {
                var date = item.Date.ToGregorian();
                if (date.DayOfWeek == dayOfWeek)
                {
                    return true;
                }
            }

            return false;
        }

        public List<AttendanceProfessor> GetAttendanceProfessores(string userId)
        {
            var teachers = GetProfessorsRelatedStudent(Convert.ToInt32(userId));
            var professorAttendanceInCurrentTerm = GetProfessorAttendanceInCurrentTerm(Convert.ToInt32(userId));

            var attendanceInCurrentTerm = new List<AttendanceProfessor>();

            foreach (DataRow row in professorAttendanceInCurrentTerm.Rows)
            {
                if (row["issuerID"].ToString() != userId)
                {
                    var issuerID = row["issuerID"].ToString();
                    var issuerName = row["issuerName"] as string;
                    var date = row["Date"] as string;
                    var name = row["name"] as string;
                    var startTime = row["startTime"] as long?;
                    var endTime = row["endTime"] as long?;
                    var mosh1 = row["mosh1"].ToString();
                    var mosh2 = row["mosh2"].ToString();
                    var rah1 = row["rah1"].ToString();
                    var rah2 = row["rah2"].ToString();
                    var dav_in = row["dav_in"].ToString();
                    var dav_out = row["dav_out"].ToString();
                    var onlineFirstTeacher = row["OnlineFirstTeacher"].ToString();
                    var onlineSecondTeacher = row["OnlineSecondTeacher"].ToString();
                    var subject = row["subject"] as string;

                    if (subject == "StudentDefence")
                    {
                        if (!string.IsNullOrEmpty(mosh1) && mosh1 != "0" && onlineFirstTeacher != mosh1 &&
                            onlineSecondTeacher != mosh1)
                        {

                            if (!attendanceInCurrentTerm.Any(x => x.Id == Convert.ToInt32(issuerID)
                                                                  && x.Name == teachers
                                                                      .FirstOrDefault(y => y.Id.ToString() == mosh1).Name
                                                                  && x.Date == date
                                                                  && x.StartTime == startTime.ToTime()
                                                                  && x.EndTime == endTime.ToTime()
                                                                  && x.Place == name
                                                                  && x.Kind == "دفاع دانشجو"
                                                                  && x.Role == "استاد مشاور"
                            ))
                                attendanceInCurrentTerm.Add(new AttendanceProfessor
                                {
                                    Id = Convert.ToInt32(issuerID),
                                    Name = teachers.FirstOrDefault(x => x.Id.ToString() == mosh1).Name,
                                    Date = date,
                                    StartTime = startTime.ToTime(),
                                    EndTime = endTime.ToTime(),
                                    Place = name,
                                    Kind = "دفاع دانشجو",
                                    Role = "استاد مشاور"

                                });
                        }

                        if (!string.IsNullOrEmpty(mosh2) && mosh2 != "0" && onlineFirstTeacher != mosh2 &&
                            onlineSecondTeacher != mosh2)
                        {
                            if (!attendanceInCurrentTerm.Any(x => x.Id == Convert.ToInt32(issuerID)
                                                                  && x.Name == teachers
                                                                      .FirstOrDefault(y => y.Id.ToString() == mosh2).Name
                                                                  && x.Date == date
                                                                  && x.StartTime == startTime.ToTime()
                                                                  && x.EndTime == endTime.ToTime()
                                                                  && x.Place == name
                                                                  && x.Kind == "دفاع دانشجو"
                                                                  && x.Role == "استاد مشاور"
                            ))

                                attendanceInCurrentTerm.Add(new AttendanceProfessor
                                {
                                    Id = Convert.ToInt32(issuerID),
                                    Name = teachers.FirstOrDefault(x => x.Id.ToString() == mosh2).Name,
                                    Date = date,
                                    StartTime = startTime.ToTime(),
                                    EndTime = endTime.ToTime(),
                                    Place = name,
                                    Kind = "دفاع دانشجو",
                                    Role = "استاد مشاور"

                                });
                        }

                        if (!string.IsNullOrEmpty(rah1) && rah1 != "0" && onlineFirstTeacher != rah1 &&
                            onlineSecondTeacher != rah1)
                        {
                            if (!attendanceInCurrentTerm.Any(x => x.Id == Convert.ToInt32(issuerID)
                                                                  && x.Name == teachers
                                                                      .FirstOrDefault(y => y.Id.ToString() == rah1).Name
                                                                  && x.Date == date
                                                                  && x.StartTime == startTime.ToTime()
                                                                  && x.EndTime == endTime.ToTime()
                                                                  && x.Place == name
                                                                  && x.Kind == "دفاع دانشجو"
                                                                  && x.Role == "استاد راهنما"
                            ))

                                attendanceInCurrentTerm.Add(new AttendanceProfessor
                                {
                                    Id = Convert.ToInt32(issuerID),
                                    Name = teachers.FirstOrDefault(x => x.Id.ToString() == rah1).Name,
                                    Date = date,
                                    StartTime = startTime.ToTime(),
                                    EndTime = endTime.ToTime(),
                                    Place = name,
                                    Kind = "دفاع دانشجو",
                                    Role = "استاد راهنما"

                                });
                        }

                        if (!string.IsNullOrEmpty(rah2) && rah2 != "0" && onlineFirstTeacher != rah2 &&
                            onlineSecondTeacher != rah2)
                        {
                            if (!attendanceInCurrentTerm.Any(x => x.Id == Convert.ToInt32(issuerID)
                                                                  && x.Name == teachers
                                                                      .FirstOrDefault(y => y.Id.ToString() == rah2).Name
                                                                  && x.Date == date
                                                                  && x.StartTime == startTime.ToTime()
                                                                  && x.EndTime == endTime.ToTime()
                                                                  && x.Place == name
                                                                  && x.Kind == "دفاع دانشجو"
                                                                  && x.Role == "استاد راهنما"
                            ))

                                attendanceInCurrentTerm.Add(new AttendanceProfessor
                                {
                                    Id = Convert.ToInt32(issuerID),
                                    Name = teachers.FirstOrDefault(x => x.Id.ToString() == rah2).Name,
                                    Date = date,
                                    StartTime = startTime.ToTime(),
                                    EndTime = endTime.ToTime(),
                                    Place = name,
                                    Kind = "دفاع دانشجو",
                                    Role = "استاد راهنما"

                                });
                        }

                        if (!string.IsNullOrEmpty(dav_in) && dav_in != "0" && onlineFirstTeacher != dav_in &&
                            onlineSecondTeacher != dav_in)
                        {

                            if (!attendanceInCurrentTerm.Any(x => x.Id == Convert.ToInt32(issuerID)
                                                                  && x.Name == teachers
                                                                      .FirstOrDefault(y => y.Id.ToString() == dav_in).Name
                                                                  && x.Date == date
                                                                  && x.StartTime == startTime.ToTime()
                                                                  && x.EndTime == endTime.ToTime()
                                                                  && x.Place == name
                                                                  && x.Kind == "دفاع دانشجو"
                                                                  && x.Role == "داور داخلی"
                            ))
                                attendanceInCurrentTerm.Add(new AttendanceProfessor
                                {
                                    Id = Convert.ToInt32(issuerID),
                                    Name = teachers.FirstOrDefault(x => x.Id.ToString() == dav_in).Name,
                                    Date = date,
                                    StartTime = startTime.ToTime(),
                                    EndTime = endTime.ToTime(),
                                    Place = name,
                                    Kind = "دفاع دانشجو",
                                    Role = "داور داخلی"

                                });
                        }

                        if (!string.IsNullOrEmpty(dav_out) && dav_out != "0" && onlineFirstTeacher != dav_out &&
                            onlineSecondTeacher != dav_out)
                        {

                            if (!attendanceInCurrentTerm.Any(x => x.Id == Convert.ToInt32(issuerID)
                                                                  && x.Name == teachers
                                                                      .FirstOrDefault(y => y.Id.ToString() == dav_out).Name
                                                                  && x.Date == date
                                                                  && x.StartTime == startTime.ToTime()
                                                                  && x.EndTime == endTime.ToTime()
                                                                  && x.Place == name
                                                                  && x.Kind == "دفاع دانشجو"
                                                                  && x.Role == "داور خارجی"
                            ))
                                attendanceInCurrentTerm.Add(new AttendanceProfessor
                                {
                                    Id = Convert.ToInt32(issuerID),
                                    Name = teachers.FirstOrDefault(x => x.Id.ToString() == dav_out).Name,
                                    Date = date,
                                    StartTime = startTime.ToTime(),
                                    EndTime = endTime.ToTime(),
                                    Place = name,
                                    Kind = "دفاع دانشجو",
                                    Role = "داور خارجی"
                                });

                        }

                    }
                    else
                    {
                        if (!attendanceInCurrentTerm.Any(x => x.Id == Convert.ToInt32(issuerID)
                                                              && x.Name == issuerName
                                                              && x.Date == date
                                                              && x.StartTime == startTime.ToTime()
                                                              && x.EndTime == endTime.ToTime()
                                                              && x.Place == name
                                                              && x.Kind == "کلاس درسی"
                                                              && x.Role == "استاد مدرس"
                        ))
                            attendanceInCurrentTerm.Add(new AttendanceProfessor
                            {
                                Id = Convert.ToInt32(issuerID),
                                Name = issuerName,
                                Date = date,
                                StartTime = startTime.ToTime(),
                                EndTime = endTime.ToTime(),
                                Place = name,
                                Kind = "کلاس درسی",
                                Role = "استاد مدرس"
                            });
                    }
                }
            }
            return attendanceInCurrentTerm;
        }

        public List<StudentDefenceRequestDTO> GetStudentDefenceRequests(string studentCode)
        {
            var resualt = requestDB.GetStudentDefenceRequests(studentCode);
            return resualt.ConvertDataTableToList<StudentDefenceRequestDTO>();
        }
        public static string HasOnlineRequest(bool onlineShow, string onlineDefence, string requestedDate, int avoidMonth = 5)
        {
            var resualt = "no";
            var dateSplited = requestedDate.Split('/');
            var requestDate = new DateTime(int.Parse(dateSplited[0]), int.Parse(dateSplited[1]), int.Parse(dateSplited[2]), new PersianCalendar());
            var startAvoidDate = new DateTime(int.Parse(dateSplited[0]), 5, 1, new PersianCalendar());
            var endAvoidDate = new DateTime(int.Parse(dateSplited[0]), 5, 31, new PersianCalendar());

            if (requestDate >= startAvoidDate && requestDate <= endAvoidDate)
            {
                if (onlineShow || !string.IsNullOrEmpty(onlineDefence))
                {
                    resualt = "کاربر محترم با توجه به بخشنامه دانشگاه مبنی بر تعطیلی مرداد ماه امکان هماهنگی جلسه آنلاین و پخش آنلاین وجود ندارد";
                }

            }
            return resualt;
        }


        public string GetRefereeParticipatingOtherDefensesSameDate(
            string firstRefereeFullName, string secondRefereeFullName,
            string refereeIn, string refereeOut, string requestDate, int requestId = 0)
        {
            var refereeParticipatingOtherDefensesSameDate = requestDB.GetRefereeParticipatingOtherDefensesSameDate(refereeIn, refereeOut, requestDate, requestId);
            var msg = "ok";


            if (refereeParticipatingOtherDefensesSameDate.Count > 0)
            {
                var isDaveIn = false;
                var isDaveOut = false;
                if (refereeIn != "0")
                    isDaveIn = refereeParticipatingOtherDefensesSameDate.Any(x => x == refereeIn);
                if (refereeOut != "0")
                    isDaveOut = refereeParticipatingOtherDefensesSameDate.Any(x => x == refereeOut);
                if (isDaveOut && isDaveIn)
                {
                    msg = " استاد" + firstRefereeFullName + " و " + secondRefereeFullName +
                          " به دلیل حضور در سه جلسه دفاع در روز مورد نظر شما، امکان حضور در این جلسه را دارا نمی باشند  ";
                }
                else
                {
                    if (isDaveIn)
                        msg = " استاد" + firstRefereeFullName +
                              " به دلیل حضور در سه جلسه دفاع در روز مورد نظر شما، امکان حضور در این جلسه را دارا نمی باشند  ";
                    else if (isDaveOut)
                        msg = " استاد" + secondRefereeFullName +
                              " به دلیل حضور در سه جلسه دفاع در روز مورد نظر شما، امکان حضور در این جلسه را دارا نمی باشند  ";
                }


            }
            return msg;
        }
        //sadegh saryazdy
        public DataTable GetMeetingTotalDefencesbyCollegeIdBusiness(int collegeId = -1)
        {
            return requestDB.GetMeetingDefencesbyCollegeId(collegeId, "-1");
        }
        public DataTable GetMeetingDefencesbyCollegeIdBusiness(int collegeId = -1)
        {
            return requestDB.GetMeetingDefencesbyCollegeId(collegeId, DatePersian.PersianToEnglish(DatePersian.GetDateNow()));
        }
        public DataTable GetMeetingDefencesByStcodeBusinessToday(string stcode)
        {
            return requestDB.GetMeetingDefencesbyStcode(stcode, DatePersian.PersianToEnglish(DatePersian.GetDateNow()));
        }
        public DataTable GetMeetingDefencesByStcodeBusinesss(string stcode, string date = "-1")
        {
            return requestDB.GetMeetingDefencesbyStcode(stcode, date);
        }
        public DataTable GetMeetingDefencesAStudentByStcodeBusinesss(string stcode, string date = "-1")
        {
            return requestDB.GetMeetingDefencesAStudentbyStcode(stcode, date);
        }
        public DataTable GetMeetingDefencesByOscodeBusiness(string oscode)
        {
            return requestDB.GetMeetingDefencesbybyOscode(oscode, DatePersian.PersianToEnglish(DatePersian.GetDateNow()));
        }
        public DataTable GetMeetingDefencesforScore(string oscode)
        {
            return requestDB.GetMeetingDefencesbybyOscode(oscode);
        }
        public bool UpdateRequest_LinkMeeting(string reqId, string link)
        {
            return requestDB.UpdeteDefence_LinkMeeting(int.Parse(reqId), link);
        }
        public bool UpdateDateTime_DefenceMeetingBusiness(string idDefence, string date, string startTime, string endDate)
        {
            return requestDB.UpdateDateTime_DefenceMeeting(idDefence, date, startTime, endDate);
        }
        public bool UpdateFlagMeeting_DefenceMeeting(string reqId, bool flagStartMeeting, bool flagEndMeeting)
        {
            return requestDB.UpdateFlagMeeting_DefenceMeeting(int.Parse(reqId.Trim()), flagStartMeeting, flagEndMeeting);
        }
        public DataTable getCollege()
        {
            return requestDB.GetAllCollge();
        }
        public DataTable GetStudentInformationByStCode(string studentCode)
        {
            return requestDB.GetStudentInformationByStcode(studentCode);
        }
        public DataTable GetTypeDefenceMeetingOnline(int typeId = -1)
        {
            return requestDB.GetTypeDefenceMeetingOnline(typeId);
        }
        public DataTable GetDefenceMeetingsOnline(string stcode = "-1")
        {
            return requestDB.GetDefenceMeetingsOnline(stcode);
        }
        public bool Enter_StudentsAllowDefenceMeetingOnline(string stcode, int typeid)
        {
            return requestDB.Insert_StudentsAllowDefenceMeetingOnline(stcode, typeid);
        }
        public DataTable CheckRequestDefenceStudent(string stcode)
        {
            return requestDB.CheckRequestDefenceStudent(stcode);
        }
        public bool Delete_StudentsAllowDefenceMeetingOnline(string stcode)
        {
            return requestDB.Delete_StudentsAllowDefenceMeetingOnline(stcode);
        }
        public DataTable GetStudentsDefenceforAOstad(string userId)
        {

            return requestDB.GetStudentsDefenceforAOstad(userId);
        }
        public bool UpdateFlagReject_DefenceMeeting(int requestId, int idTypeOstad, bool flagAccept)
        {
            return requestDB.UpdateFlagReject_DefenceMeeting(requestId, idTypeOstad, flagAccept);
        }
        public DataTable GetAssistanceDefence(string stcode = "-1", int status = -1)
        {
            return requestDB.GetAssistanceDefence(stcode, status);
        }
        public bool Enter_AssistanceDefence(string stcode, string rDate, string rTime)
        {
            return requestDB.Insert_AssistanceDefence(stcode, rDate, rTime);
        }
        public DataTable GetLogMeetingDefences(string stcode = "-1")
        {
            return requestDB.GetLogMeetingDefences(stcode);
        }
        public bool Update_AssistanceDefence(string stcode, int status, string msgAnswer)
        {
            return requestDB.Update_AssistanceDefence(stcode, status, msgAnswer);
        }
        public int GetCountMeetingDefencesRejectByOstad(string stcode)
        {
            return requestDB.GetCountMeetingDefencesRejectByOstad(stcode);
        }
        public bool EnterCalenderHoliday(string date, string dsc, bool IsForStudent, bool IsForEmployee)
        {
            const bool flagTatili = true;
            return requestDB.Insert_SpecialDescription(date, date, dsc, IsForStudent, IsForEmployee, flagTatili);
        }
        public DataTable GetCalenderHoliday()
        {
            const int flagTatili = 1;
            return requestDB.GetSpecialDescription(flagTatili);
        }
        public bool DeleteCalenderHoliday(int id)
        {

            return requestDB.DeleteSpecialDescription(id);
        }
        public DataTable CheckDefenceInformationByRequestDate(string startDate, string endDate, string startTime = "-1", string justForOnline = "-1")
        {
            return requestDB.CheckDefenceInformationByRequestDate(startDate, endDate, startTime, justForOnline);
        }
        public DataTable CheckExistCalendarHolidayDate(string startDate, string endDate)
        {
            const int flagTatili = 1;
            return requestDB.CheckExistSpecialDate(startDate, endDate, flagTatili);
        }
        public DataTable CheckExistSpecialDate(string startDate, string endDate)
        {
            const int flagTatili = 0;
            return requestDB.CheckExistSpecialDate(startDate, endDate, flagTatili);
        }
        public DataTable EnterAvoidDate(string startDate, string endDate, string dsc, bool isForStudent, bool isForEmployee)
        {
            return requestDB.InsertAvoidDate(startDate, endDate, dsc, isForStudent, isForEmployee);
        }
        public DataTable GetAvoidDate(string startDate = "-1", string endDate = "-1")
        {
            return requestDB.GetAvoidDate(startDate, endDate);

        }
        public bool DeleteAvoidDate(int id)
        {
            return requestDB.DeleteAvoidDate(id);

        }
        public bool UpdateAvoidDate(int id, string startDate, string endDate, string dsc, bool isForEmployee, bool isForStudent)
        {
            return requestDB.UpdateAvoidDate(id, startDate, endDate, dsc, isForEmployee, isForStudent);

        }

        public bool EnterSpecialDate(string startDate, string endDate, string dsc, bool IsForStudent, bool IsForEmployee)
        {
            const bool flagTatili = false;
            return requestDB.Insert_SpecialDescription(startDate, endDate, dsc, IsForStudent, IsForEmployee, flagTatili);
        }
        public bool DeleteSpecialDescription(int id)
        {

            return requestDB.DeleteSpecialDescription(id);
        }
        public DataTable GetSpecialDescription()
        {
            const int flagTatili = 0;
            return requestDB.GetSpecialDescription(flagTatili);
        }
        public bool UpdateSpecialDescription(int id, string startDate, string endDate, string dsc, bool IsForStudent, bool IsForEmployee)
        {

            return requestDB.UpdateSpecialDate(id, startDate, endDate, dsc, IsForStudent, IsForEmployee);
        }
        public DataTable GetAvoidTime(string startDate = "-1", string endDate = "-1", string time = "-1", int justForOnline = -1)
        {

            return requestDB.GetAvoidTime(startDate, endDate, time, justForOnline);
        }
        public bool EnterAvoidTime(string startDate, string endDate, string time, bool justForOnline)
        {

            return requestDB.InsertAvoidTime(startDate, endDate, time, justForOnline);
        }
        public bool DeleteAvoidTime(string startDate, string endDate, string time, bool justForOnline)
        {

            return requestDB.DeleteAvoidTime(time, startDate, endDate, justForOnline);
        }
        public DataTable GetEducationCalender(int appId, int id = -1, string term = "-1")
        {
            return requestDB.GetEducationCalender(id, term, appId);
        }
        public bool EnterEducationCalender(string startDate, string endDate, string term, int calenderType)
        {

            return requestDB.InsertEducationCalender(startDate, endDate, term, calenderType);
        }
        public bool UpdateEducationCalender(int id, string startDate, string endDate, string term, int calenderType)
        {
            return requestDB.UpdateEducationCalender(id, calenderType, startDate, endDate, term);

        }
        public DataTable GetResource(int catId = -1)
        {
            return requestDB.GetResource(catId);
        }
        public bool EnterResourece_College_junc(string startDate, string endDate, int CollegeId, int ResourceId, int IsShared)
        {

            return requestDB.InsertResourece_College_junc(startDate, endDate, CollegeId, ResourceId, IsShared);
        }
        public DataTable GetResourece_College_junc(string startDate = "-1", string endDate = "-1", int collegeId = -1, int resourceId = -1, int isShared = -1)
        {

            return requestDB.GetResourece_College_junc(startDate, endDate, collegeId, resourceId, isShared);
        }
        public DataTable GetRequestDateTime(string startDate = "-1", string endDate = "-1", int resourceId = -1, int collegeId = -1)
        {

            return requestDB.GetRequestDateTime(startDate, endDate, resourceId, collegeId);
        }
        public bool UpdateResourece_College_junc(int id, string startDate, string endDate, int resourceId
            , int collegeId, int IsShared)
        {

            return requestDB.UpdateResourece_College_junc(id, startDate, endDate, resourceId, collegeId, IsShared);
        }
        public bool DeleteResourece_College_junc(int id)
        {

            return requestDB.DeleteResourece_College_junc(id);
        }
        public DataTable GetRefereeTeachersPaymentHasNotDownTransportation(string term = null)
        {
            return requestDB.GetTeachersPaymentTransportationHasNotDown(term);
        }
        public bool EnterRefereeWageTransportationPayment(int  id_Os, string date, string wage,
                                                            string dsc,string term ,int status)
        {
            return requestDB.InsertRefereeWageTransportationPayment(id_Os, date, wage, dsc, term, status);
        }
        public DataTable GetRefereeTeachersPaymentHasDownTransportation(string term = null)
        {
            return requestDB.GetTeachersPaymentTransportationHasDown(term);
        }
        public bool DeleteRefereeWageTransportationPayment(int id_Os, string date)
        {
            return requestDB.DeleteRefereeWageTransportationPayment(id_Os, date);
        }
        public DataTable GetRefereeTeachersPaymentTransportation(int id_os,string date)
        {
            return requestDB.GetTeachersPaymentTransportation(id_os, date);
        }
        public bool UpdateRefereeWageTransportationPayment(int id_Os, string date, string wage,
                                                       string dsc, string term, int status)
        {
            return requestDB.UpdateRefereeWageTransportationPayment(id_Os, date, wage, dsc, term, status);
        }
        public DataTable GetRefereeTeachersPaymentTransportation_Report(int isPayedWage = 0, int reportType = 0, string term = null)
        {
            return requestDB.GetRefereeTeachersPaymentTransportation_Report(isPayedWage, reportType, term);

        }
        public DataTable GetRequestIdAcceptedByStcode(string stcode)
        {
            return requestDB.GetRequestIdAcceptedByStcode(stcode);
        }
        public DataTable GetTeacherSignature(long osCode)
        {
            return requestDB.GetTeacherSignature(osCode);
        }
        public List<InformationOstadForDefenceStudent> GetSignutreOstad(DefenceInformation information)
        {
            List<InformationOstadForDefenceStudent> infoOut = new List<InformationOstadForDefenceStudent>();
            infoOut.Add(GetSignModirGroup(information.StudentCode));
            if (infoOut.Last().AddressSignature != null&& infoOut.Last().AddressSignature!="")
            {
                var sign = infoOut.Last().AddressSignature;
                infoOut.Last().AddressSignature = sign.Replace("~/", "../../");
            }
            infoOut.Last().IdTypeOs =1;
          
            if (information.FirstConsultantId != null )
            {
                var sign = requestDB.GetTeacherSignature(long.Parse(information.FirstConsultantId));
                infoOut.Add(new InformationOstadForDefenceStudent
                {
                    Id = information.FirstConsultantId,
                    FullName = information.FirstConsultantFullName,
                    AddressSignature = ((sign?.Rows?.Count ?? 0) > 0)?(sign.Rows[0][0].ToString()).Replace("~/", "../../"):"",

                    Mobile = information.FirstConsultantMobile,
                    TypeOS="استاد مشاور",
                    IdTypeOs=2
                    
                });
                

            }
            if (information.FirstGuideId != null)
            {
                var sign = requestDB.GetTeacherSignature(long.Parse(information.FirstGuideId));

                infoOut.Add(new InformationOstadForDefenceStudent
                {
                    Id = information.FirstGuideId,
                    FullName = information.FirstGuideFullName,
                    AddressSignature = ((sign?.Rows?.Count ?? 0) > 0) ? sign.Rows[0][0].ToString().Replace("~/", "../../") : "",
                    Mobile = information.FirstGuideMobile,
                    TypeOS = "استادراهنما",
                    IdTypeOs = 3

                });
            }
            if (information.FirstRefereeId != null)
            {
                var sign = requestDB.GetTeacherSignature(long.Parse(information.FirstRefereeId));

                infoOut.Add(new InformationOstadForDefenceStudent
                {
                    Id = information.FirstRefereeId,
                    FullName = information.FirstRefereeFullName,
                    AddressSignature = ((sign?.Rows?.Count ?? 0) > 0) ? sign.Rows[0][0].ToString().Replace("~/", "../../") : "",
                    Mobile = information.FirstRefereeMobile,
                    TypeOS = "استاد داور",
                    IdTypeOs = 4

                });
            }
            if (information.SecondRefereeId != null)
            {
                var sign = requestDB.GetTeacherSignature(long.Parse(information.SecondRefereeId));

                infoOut.Add(new InformationOstadForDefenceStudent
                {
                    Id = information.SecondRefereeId,
                    FullName = information.SecondRefereeFullName,
                    AddressSignature = ((sign?.Rows?.Count ?? 0) > 0) ? sign.Rows[0][0].ToString().Replace("~/", "../../") : "",
                    Mobile = information.SecondRefereeMobile,
                    TypeOS = "استاد داور",
                    IdTypeOs = 5

                });
            }
            if (information.SecondConsultantId != null)
            {
                var sign = requestDB.GetTeacherSignature(long.Parse(information.SecondConsultantId));
                infoOut.Add(new InformationOstadForDefenceStudent
                {
                    Id = information.SecondConsultantId,
                    FullName = information.SecondConsultantFullName,
                    AddressSignature = ((sign?.Rows?.Count ?? 0) > 0) ? sign.Rows[0][0].ToString().Replace("~/", "../../") : "",
                    Mobile = information.SecondConsultantMobile,
                    TypeOS = "استاد مشاور",
                    IdTypeOs = 6

                });


            }
 
            if (information.SecondGuideId != null)
            {
                var sign = requestDB.GetTeacherSignature(long.Parse(information.SecondGuideId));
               
                infoOut.Add(new InformationOstadForDefenceStudent
                {
                    Id = information.SecondGuideId,
                    FullName = information.SecondGuideFullName,
                    AddressSignature = ((sign?.Rows?.Count ?? 0) > 0) ? sign.Rows[0][0].ToString().Replace("~/", "../../") : "",
                    Mobile = information.SecondGuideMobile,
                    TypeOS = "استادراهنما",
                    IdTypeOs = 7

                });
            }


            return infoOut;
        }
        public bool UpdateScoreForDefence(ScoreDefence score)
        {
            return requestDB.UpdateScoreForDefence(score);
        }
        public ScoreDefence GetScoreForDefence(int reqId)
        {
            return requestDB.SelectScoreForDefence(reqId);
        }

        public InformationOstadForDefenceStudent GetSignModirGroup(string stcode)
        {
            return requestDB.SelectSignModirGroup(stcode);
        }
        public List<InformationOstadForDefenceStudent> GetSignutreOstadByImage(string stCode, string addresPathImage)
        {
            DefenceInformation information = new DefenceInformation();
            information = GetDefenceInformation(stCode);

            List <InformationOstadForDefenceStudent> infoOut = GetSignutreOstad(information);

            if (addresPathImage != "")
            { 

                foreach (var item in infoOut)
                {
                    if (item.AddressSignature != null && item.AddressSignature!="")
                    {
                         int indexSign=item.AddressSignature.LastIndexOf("/");
                          item.singAddress = addresPathImage + "University/CooperationRequest/Signatures/teachers/" + item.AddressSignature.Substring(indexSign + 1);
                    }
                       
                }
            }
            return infoOut;
        }

        public StatusDefenceTechnichal GetStatusDefenceTechnical(int reqId)
        {
            return requestDB.SelectStatusDefenceTechnical(reqId);
        }
        public bool updateStatusDefenceTechnical(StatusDefenceTechnichal statusDefence)
        {

            return requestDB.updateStatusDefenceTechnical(statusDefence);
        }
        public bool UpdateIsRejectFinancial(int reqId, bool flag)
        {
            return requestDB.UpdateIsRejectFinancial(reqId, flag);
        }
        public bool UpdateSendSmsFlagFinancial(int reqId, bool flag,string msg)
        {
            return requestDB.UpdateSendSmsFlagFinancial(reqId, flag, msg);
        }
        public DataTable GetAllAccepetedDefenceRequestsForFinacial(int daneshId)
        {
            return requestDB.GetAllAccepetedDefenceRequestsForFinaical(daneshId);
        }

    }
}