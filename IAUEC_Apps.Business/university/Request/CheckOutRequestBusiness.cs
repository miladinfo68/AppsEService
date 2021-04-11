using IAUEC_Apps.DAO.University.Request;
using IAUEC_Apps.DTO.University.Request;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.Business.Common;
using IAUEC_Apps.DTO.University.Graduate;
using IAUEC_Apps.Business.university.GraduateAffair;

namespace IAUEC_Apps.Business.university.Request
{
    public class CheckOutRequestBusiness
    {
        CommonBusiness CommonBusiness = new CommonBusiness();
        CheckOutRequestDAO ReqDAO = new CheckOutRequestDAO();
        CheckOutDebitDAO DebitDAO = new CheckOutDebitDAO();
        CheckOutMaliBusiness MaliDAO = new CheckOutMaliBusiness();
        CheckOutRefahBusiness Refahbusiness = new CheckOutRefahBusiness();
        RequestPaymentBusiness RPB = new RequestPaymentBusiness();


        public bool insertCodeBayegan(string stcode, string codeBayegan)
        {
            return ReqDAO.insertCodeBayegan(stcode, codeBayegan);
        }
        public DataTable getAllCodeBayegan()
        {
            return ReqDAO.getAllCodeBayegan();
        }
        public int InsertInToStudentRequest(string stcode, int RequestTypeID, int RequestLogID, string Erae_Be, string MashmulNumber, string CreateDate, string note, int isOnline, bool sendSms)
        {
            int reqId = 0;
            DataTable dt = new DataTable();
            dt = ReqDAO.checkExistingRequest(stcode);
            if (dt.Rows.Count == 0)
            {
                reqId = int.Parse(ReqDAO.InsertInToStudentRequest(stcode, RequestTypeID, RequestLogID, Erae_Be, MashmulNumber, CreateDate, note, isOnline).ToString());
                if (reqId > 0 && RequestTypeID == 15 && isOnline == 1)
                {
                    string smsBody;
                    string result;
                    bool sentSMS;
                    DataTable dtMsg = CommonBusiness.GetAppIDMessage(0, 12, 1, 2);
                    smsBody = dtMsg.Rows[0][0].ToString();

                    if (sendSms)
                    {
                        string smsStatusText;
                        result = CommonBusiness.sendSMS(1, stcode, smsBody, out sentSMS, out smsStatusText);

                    }
                    CheckOutNaghsBusiness naghs = new CheckOutNaghsBusiness();

                    naghs.InsertNaghs(new DTO.University.Request.CheckOutNaghsDTO
                    {

                        IsResolved = false,
                        NaghsMessage = smsBody,
                        RequestLogId = 29,
                        ResolveDate = "",
                        ResolveMessage = "",
                        StCode = stcode,
                        StudentRequestId = reqId,
                        SubmitDate = DateTime.Now.ToPeString(),
                        Erae_Be = Erae_Be
                    });

                    naghs.InsertNaghs(new DTO.University.Request.CheckOutNaghsDTO
                    {

                        IsResolved = false,
                        NaghsMessage = smsBody,
                        RequestLogId = 30,
                        ResolveDate = "",
                        ResolveMessage = "",
                        StCode = stcode,
                        StudentRequestId = reqId,
                        SubmitDate = DateTime.Now.ToPeString(),
                        Erae_Be = Erae_Be
                    });

                }
            }
            return reqId;
        }



        public bool InsertCheckOutSign(byte[] sign, string userID, int status, string fromDate, string toDate)
        {
            //CommonBusiness.InsertIntoUserLog(Convert.ToInt32(adminID), DateTime.Now.ToString("HH:mm"), 12, 85, "درج امضا کاربر در سیستم تسویه");
            return ReqDAO.InsertCheckOutSign(sign, userID, status, fromDate, toDate);
        }

        //public string ApproveCheckOutRequestByRole(string userID, int roleID, int reqID, int reqType)
        //{
        //    int status = GetStatusOfRole(roleID);
        //    string note = GetStatusNote(status);
        //    string issuerID = ReqDAO.GetCheckOutIssuerID(reqID);
        //    return ApproveRequest(userID, reqID, status, note, issuerID, reqType);
        //}

        public string ApproveCheckOutRequestByCurrentStatus(string userID, int currentstatus, int reqID, int reqType, bool isMashmul = false, int idVahed = 0)
        {
            string note = GetStatusNote(currentstatus);
            string issuerID = ReqDAO.GetCheckOutIssuerID(reqID);
            return ApproveRequest(userID, reqID, currentstatus, note, issuerID, reqType, isMashmul, idVahed);
        }
        public void UpdateStudentLastUpdate(int reqID)
        {

            ReqDAO.UpdateLastUpdate(reqID);
        }
        public bool insertThesisFile(string studentID, string thesisPath_Doc, string thesisPath_PDF)
        {
            return ReqDAO.insertThesisFile(thesisPath_Doc, thesisPath_PDF, studentID);
        }

        public bool denyThesis(string studentID)
        {
            return ReqDAO.denyThesis(studentID);
        }


        public bool hasPassedCoursesToSubmitGraduateRequest(string stcode, int isBachelor)
        {
            if (isBachelor == 1)
            {
                var Voroodi = GetSaleVoroodByStCode(stcode);

                if (Voroodi == "89" || Voroodi == "90")
                {
                    if (IsMojazCheckOut_146(stcode) == 1)
                    {
                        return true;
                        // -ورودي هاي 89 و 90 : 146 واحد
                    }
                }
                else if (Voroodi == "91" || Voroodi == "92" || Voroodi == "93")
                {
                    if (IsMojazCheckOut_148(stcode) == 1)
                    {
                        return true;
                    }

                }
                else if (Voroodi == "94" || Voroodi == "95")
                {
                    if (IsMojazCheckOut_144(stcode) == 1)
                    {
                        return true;
                        //  - ورودي هاي 94 و 95 : 144 واحد
                    }
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        //public DataTable 

        public string GetSaleVoroodByStCode(string stcode)
        {
            return ReqDAO.GetSaleVoroodByStCode(stcode);
        }


        public int IsMojazCheckOut_144(string stcode)
        {
            return ReqDAO.IsMojazCheckOut_144(stcode);
        }
        public int IsMojazCheckOut_146(string stcode)
        {
            return ReqDAO.IsMojazCheckOut_146(stcode);
        }
        public int IsMojazCheckOut_148(string stcode)
        {
            return ReqDAO.IsMojazCheckOut_148(stcode);
        }
        public DataTable getThesisByStudentID(string studentID)
        {
            return ReqDAO.getThesisByStcode(studentID);
        }

        public DataTable getThesisByFiltering(string daneshID, string reshte, string family, string stcode)
        {
            return ReqDAO.getThesisByFiltering(daneshID, reshte, family, stcode);
        }

        public string DenyCheckOutRequestByCurrentStatus(string userID, int currentstatus, int eraeBe, int reqID, int reqType)
        {
            string note = GetStatusNote(currentstatus);
            string issuerID = ReqDAO.GetCheckOutIssuerID(reqID);
            return DenyRequest(userID, reqID, 5, 5, note, issuerID, reqType, false);
        }


        public string SendCheckOutRequestToStatus(string userID, int status, int reqID, int reqType, int CurStatus)
        {
            string issuerID = ReqDAO.GetCheckOutIssuerID(reqID);
            int previousstatus = GetPreviousStatus(status, reqType);
            string currentStatus = GetPersianStatus(CurStatus);
            string note = GetStatusNote(previousstatus);
            int ID = ReqDAO.UpdateCheckOutRequest(reqID, previousstatus, status, note, null, null, false);
            string persianStatus = GetPersianStatus(status);
            CommonBusiness.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, 36, note + "درخواست شماره " + reqID.ToString() + " از مرحله " + currentStatus + " به مرحله " + persianStatus + " فرستاده شد.", reqID);
            return "درخواست شماره " + reqID + " فرستاده شد.";
        }

        private string ApproveRequest(string userID, int reqID, int currentstatus, string note, string issuerID, int reqType, bool isMashmool, int idVahed)
        {
            string message = null;
            int ID;

            if (currentstatus == Convert.ToInt32(CheckOutStatusEnum.CheckOutAllStatusEnum.refah))
            {
                int countBedehi = hasRefahBedehi(issuerID);
                if (countBedehi > 0)
                {
                    message = "این درخواست به دلیل بدهی به صندوق رفاه قابل تایید نمی باشد.";
                    return message;
                }

                DataTable dt = new DataTable();
                dt = Refahbusiness.GetAllDebitByStcode(issuerID);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["DebitTypeID"].ToString() == "1" || dt.Rows[i]["DebitTypeID"].ToString() == "10")
                    {
                        if (dt.Rows[i]["TotalLoanAmount"].ToString() == "")
                        {
                            message = "این درخواست به دلیل عدم درج مبلغ برگه تسویه قابل تایید نمی باشد.";
                            return message;
                        }
                    }
                }
            }

            if (currentstatus == Convert.ToInt32(CheckOutStatusEnum.CheckOutAllStatusEnum.maali))
            {
                if (MaliDAO.CheckMaliCheckOut(issuerID) > 0)
                {
                    return "این درخواست به دلیل بدهی قابل تایید نمی باشد";
                }
            }
            if (currentstatus == Convert.ToInt32(CheckOutStatusEnum.CheckOutAllStatusEnum.archive))//sargol: همه آپدیت ها یکجا باشند
            {
                if (reqType == (int)CheckOutStatusEnum.CheckOutType.fareq_tahsil)
                {
                    ID = ReqDAO.UpdateCheckOutRequestArchive(reqID);
                    string archiveNote = GetStatusNote((int)CheckOutStatusEnum.CheckOutAllStatusEnum.archive);

                }
                else
                {
                    if (isMashmool)
                    {
                        ID = ReqDAO.UpdateCheckOutRequestArchive(reqID);
                        string archiveNote = GetStatusNote((int)CheckOutStatusEnum.CheckOutAllStatusEnum.archive);


                        int mashmoolStatus = (int)CheckOutStatusEnum.CheckOutAllStatusEnum.mashmulan;
                        ID = ReqDAO.UpdateCheckOutRequest(reqID, currentstatus, mashmoolStatus, note, null, null, isMashmool);//sargol: همه آپدیت ها یکجا باشند
                        

                        return "درخواست شماره " + reqID + " تایید شد.";
                    }
                }
            }
            if (currentstatus == Convert.ToInt32(CheckOutStatusEnum.FareghReqStatus.ersal_sodoor_ok))
            {

                CheckOutRequestBusiness _reqBusiness = new CheckOutRequestBusiness();
                var checkID = Convert.ToInt32(_reqBusiness.CheckIsReady(reqID));
                if (checkID == 0)
                {
                    return "این درخواست آماده ارسال نمی باشد";
                }
                else
                {
                    GraduateFormsBusiness GFB = new GraduateFormsBusiness();
                    StudentFeraghatDocument SFD = new StudentFeraghatDocument();

                    DataTable dtTemp = new DataTable();
                    SFD = GFB.getStudentFeraghatDocument(issuerID);
                    if (!SFD.HasPaymentReceipt || !SFD.HasStamp)
                    {
                        var msg = "با توجه به نقص فیش و یا تمبر امکان تایید درخواست وجود ندارد";
                        return msg;
                        //  RadWindowManager1.RadAlert(msg, 300, 100, "پیام سیستم", "");
                    }
                    if (SFD.HasPaymentReceipt && SFD.HasStamp)
                    {
                        GFB.freeDocArchiveId(reqID);
                    }
                }

            }

            CheckOutStatusEnum.CheckOutAllStatusEnum nextStatus = GetNextStatus(currentstatus, reqType, reqID, issuerID);
            if (idVahed > 0)
                ID = ReqDAO.UpdateCheckOutRequest(reqID, currentstatus, (int)nextStatus, note, null, null, isMashmool, idVahed);
            else
                ID = ReqDAO.UpdateCheckOutRequest(reqID, currentstatus, (int)nextStatus, note, null, null, isMashmool);


            string nextNote1 = GetStatusNote((int)nextStatus);
            int eventId;
            switch (currentstatus)
            {
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.Moavenat_Amoozesh:
                    eventId = 197;
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.daneshkade:
                    eventId = 73;
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.amoozesh:
                    eventId = 198;
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.daneshjooyi:
                    eventId = 199;
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.pajohesh:
                    eventId = 88;
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.refah:
                    eventId = 200;
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.maali:
                    eventId = 201;
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.fani:
                    eventId = 202;
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.mashmulan:
                    eventId = 203;
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.archive:
                    eventId = 93;
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.takmil_parvande:
                    eventId = 204;
                    break;
                //case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.dabirkhane:
                //    eventId = 205;
                //    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.vrood_moavenat:
                    eventId = 206;
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.ersal_sodoor:
                    eventId = 207;
                    break;
                default:
                    eventId = 36;
                    break;
            }

            CommonBusiness.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, eventId, " در خواست شماره " + reqID + " تایید در مرحله " + note + " ارسال به مرحله " + nextNote1, reqID);
            return "درخواست شماره " + reqID + " تایید شد.";
        }

        private CheckOutStatusEnum.CheckOutAllStatusEnum GetNextStatus(int currentstatus, int reqType, int reqID, string issuerID = null)
        {
            CheckOutRequestBusiness _business = new CheckOutRequestBusiness();
            CheckOutStatusEnum.CheckOutAllStatusEnum nextStatus = 0;
            switch ((CheckOutStatusEnum.CheckOutType)reqType)
            {
                #region taqir_reshte
                case CheckOutStatusEnum.CheckOutType.taqir_reshte:
                    if (currentstatus == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.end || currentstatus == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.mashmulan)
                    {
                        nextStatus = CheckOutStatusEnum.CheckOutAllStatusEnum.end;
                    }
                    else if ((CheckOutStatusEnum.CheckOutAllStatusEnum)currentstatus == CheckOutStatusEnum.CheckOutAllStatusEnum.daneshjooyi)
                    {
                        nextStatus = CheckOutStatusEnum.CheckOutAllStatusEnum.refah;
                    }
                    else if (currentstatus == Convert.ToInt32(CheckOutStatusEnum.CheckOutAllStatusEnum.maali))
                    {
                        if (isMale(issuerID))
                            nextStatus = CheckOutStatusEnum.CheckOutAllStatusEnum.mashmulan;
                        else//خانم ها مرحله مشمولان رو ندارند
                            nextStatus = CheckOutStatusEnum.CheckOutAllStatusEnum.end;
                    }
                    else
                    {
                        nextStatus = ((CheckOutStatusEnum.CheckOutAllStatusEnum)currentstatus).Next();
                    }
                    break;
                #endregion
                #region ekhraj
                case CheckOutStatusEnum.CheckOutType.ekhraj:
                    if (currentstatus == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.end || currentstatus == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.archive)
                    {
                        nextStatus = CheckOutStatusEnum.CheckOutAllStatusEnum.end;
                    }
                    else if (!isMale(issuerID) && currentstatus == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.fani)
                    {
                        nextStatus = CheckOutStatusEnum.CheckOutAllStatusEnum.archive;
                    }
                    else if ((CheckOutStatusEnum.CheckOutAllStatusEnum)currentstatus == CheckOutStatusEnum.CheckOutAllStatusEnum.daneshjooyi)
                    {
                        nextStatus = CheckOutStatusEnum.CheckOutAllStatusEnum.refah;
                    }
                    else
                    {
                        nextStatus = ((CheckOutStatusEnum.CheckOutAllStatusEnum)currentstatus).Next();
                    }
                    break;
                #endregion
                #region fareq_tahsil
                case CheckOutStatusEnum.CheckOutType.fareq_tahsil:
                    if (currentstatus == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.end)
                    {
                        nextStatus = CheckOutStatusEnum.CheckOutAllStatusEnum.end;
                    }
                    else if (currentstatus == Convert.ToInt32(CheckOutStatusEnum.CheckOutAllStatusEnum.vrood_moavenat) &&
                        _business.GetIsBachelor(issuerID) == 1)//لیسانسه ها نباید ارسال جهت صدور داشته باشند
                    {
                        nextStatus = CheckOutStatusEnum.CheckOutAllStatusEnum.ersal_sodoor;
                    }
                    else if (_business.GetIsBachelor(issuerID) == 1 && (CheckOutStatusEnum.CheckOutAllStatusEnum)currentstatus == CheckOutStatusEnum.CheckOutAllStatusEnum.daneshjooyi)
                    {
                        nextStatus = CheckOutStatusEnum.CheckOutAllStatusEnum.refah;
                    }
                    else if (!isMale(issuerID) && currentstatus == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.fani)
                    {
                        nextStatus = CheckOutStatusEnum.CheckOutAllStatusEnum.archive;
                    }
                    else if (currentstatus == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.refah && (hasStampDefect(reqID) || hasDefencePaymentInquery(issuerID)))
                    {
                        nextStatus = CheckOutStatusEnum.CheckOutAllStatusEnum.stampPay;
                    }
                    else
                    {
                        nextStatus = ((CheckOutStatusEnum.CheckOutAllStatusEnum)currentstatus).Next();
                    }
                    break;

                #endregion
                #region enseraf
                case CheckOutStatusEnum.CheckOutType.enseraf:
                    if ((CheckOutStatusEnum.CheckOutAllStatusEnum)currentstatus == CheckOutStatusEnum.CheckOutAllStatusEnum.daneshjooyi)
                    {
                        nextStatus = CheckOutStatusEnum.CheckOutAllStatusEnum.refah;
                    }
                    else if (!isMale(issuerID) && currentstatus == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.fani)
                    {
                        nextStatus = CheckOutStatusEnum.CheckOutAllStatusEnum.archive;
                    }
                    else if (currentstatus == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.end || currentstatus == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.archive)
                    {
                        nextStatus = CheckOutStatusEnum.CheckOutAllStatusEnum.end;
                    }
                    else
                    {
                        nextStatus = ((CheckOutStatusEnum.CheckOutAllStatusEnum)currentstatus).Next();
                    }
                    break;
                #endregion
                #region enteqali
                case CheckOutStatusEnum.CheckOutType.enteqali:
                    if ((CheckOutStatusEnum.CheckOutAllStatusEnum)currentstatus == CheckOutStatusEnum.CheckOutAllStatusEnum.daneshjooyi)
                    {
                        nextStatus = CheckOutStatusEnum.CheckOutAllStatusEnum.refah;
                    }
                    else if (!isMale(issuerID) && currentstatus == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.fani)
                    {
                        nextStatus = CheckOutStatusEnum.CheckOutAllStatusEnum.archive;
                    }
                    else if (currentstatus == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.end || currentstatus == (int)CheckOutStatusEnum.CheckOutAllStatusEnum.archive)
                    {
                        nextStatus = CheckOutStatusEnum.CheckOutAllStatusEnum.end;
                    }
                    else
                    {
                        nextStatus = ((CheckOutStatusEnum.CheckOutAllStatusEnum)currentstatus).Next();
                    }
                    break;
                    #endregion
            }
            return nextStatus;
        }

        private string DenyRequest(string userID, int reqID, int currentstatus, int eraeBe, string note, string issuerID, int reqType, bool isMashmool)
        {
            int ID;
            int tempCurrentstatus = 5;
            ID = ReqDAO.UpdateCheckOutRequest(reqID, tempCurrentstatus, eraeBe, note, null, null, isMashmool);
            string currentNote1 = GetStatusNote(currentstatus);
            string nextNote1 = GetStatusNote(eraeBe);
            CommonBusiness.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, 109, " در خواست شماره " + reqID + " رد در مرحله " + currentNote1 + "ارسال به مرحله " + nextNote1, reqID);
            return "درخواست شماره " + reqID + " رد شد.";
        }
        public void DeleteCheckOutFromFraghat(int reqID)
        {
            ReqDAO.DeleteCheckOutFromFraghat(reqID);

        }
        public int CheckIsReady(int ReqID)
        {
            return ReqDAO.CheckIsReady(ReqID);

        }
        public void UpdateReadyRequest(int reqID, string userID)

        {
            ReqDAO.UpdateReadyRequest(reqID);
            CommonBusiness.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, 194, " در خواست شماره " + reqID + "  " + "آماده ارسال شد", reqID);

        }
        private bool HasNaghs(string issuerID)
        {
            using (CheckOutNaghsBusiness oNaghsBus = new CheckOutNaghsBusiness())
            {
                return oNaghsBus.HasNaghs(issuerID);
            }
        }

        public DataTable GetStudentInfo(string stcode)
        {
            return ReqDAO.GetStudentInfo(stcode);

        }
        public DataTable GetStudentMadrakInfo(string stcode)
        {
            return ReqDAO.GetStudentMadrakInfo(stcode);

        }
        public DataTable getArchiveUserSignByStudentStcode(string stcode)
        {
            return ReqDAO.getArchiveUserSignByStudentStcode(stcode);

        }

        public bool isMale(string issuerID)
        {
            return ReqDAO.CheckIsMale(issuerID);
        }

        private int hasRefahBedehi(string issuerID)
        {
            return DebitDAO.CheckRefahHasBedehi(issuerID);
        }

        public void UpdatePrintStatus(int reqID)
        {
            ReqDAO.UpdatePrintStatus(reqID);
        }
        public string SendOdatMessageAndInsertOdatLog(string userID, int reqID, string message)
        {
            int ID = ReqDAO.AddMessage(reqID, message);
            CommonBusiness.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, 210, message, reqID);
            return "پیام شما با موفقیت ارسال شد.";
        }

        public string SendMessage(string userID, int reqID, string message)
        {
            int ID = ReqDAO.AddMessage(reqID, message);
            CommonBusiness.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, 37, message, reqID);
            return "پیام شما با موفقیت ارسال شد.";
        }
        public string SendMessageOfDenyThesis(string userID, int reqID, string message)
        {
            int ID = ReqDAO.AddMessage(reqID, message);
            CommonBusiness.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, 168, message, reqID);
            return "پیام شما با موفقیت ارسال شد.";
        }

        public string SendMessageStudent(string userID, int reqID, string message)
        {
            int ID = ReqDAO.AddStudentMessage(reqID, message);
            CommonBusiness.InsertIntoUserLog(Convert.ToInt32(userID), DateTime.Now.ToString("HH:mm"), 12, 37, message, reqID);
            return "پیام شما با موفقیت ارسال شد.";
        }

        private int GetNextStatus_OLD(int currentstatus, int reqType, string issuerID = null)
        {
            int nextStatus = 0;
            switch (reqType)
            {
                case (int)CheckOutStatusEnum.CheckOutType.fareq_tahsil:
                    if (currentstatus == (int)CheckOutStatusEnum.FareghReqStatus.end)
                    {
                        nextStatus = (int)CheckOutStatusEnum.FareghReqStatus.end;
                    }
                    else
                    {
                        nextStatus = (int)(((CheckOutStatusEnum.FareghReqStatus)currentstatus).Next());
                    }

                    break;
                case (int)CheckOutStatusEnum.CheckOutType.ekhraj:
                    if (currentstatus == (int)CheckOutStatusEnum.EkhrajStatus.end)
                    {
                        nextStatus = (int)CheckOutStatusEnum.EkhrajStatus.end;
                    }
                    else
                    {
                        nextStatus = (int)(((CheckOutStatusEnum.EkhrajStatus)currentstatus).Next());
                    }
                    break;
                case (int)CheckOutStatusEnum.CheckOutType.taqir_reshte:
                    if (currentstatus == (int)CheckOutStatusEnum.TaghirReshteStatus.end)
                    {
                        nextStatus = (int)CheckOutStatusEnum.TaghirReshteStatus.end;
                    }
                    else if (currentstatus == Convert.ToInt32(CheckOutStatusEnum.CheckOutAllStatusEnum.pajohesh))
                    {
                        nextStatus = (int)((CheckOutStatusEnum.CheckOutAllStatusEnum)currentstatus).Next();
                    }
                    else if (!isMale(issuerID) && currentstatus == Convert.ToInt32(CheckOutStatusEnum.FareghReqStatus.mashmulan_ok.Previous().Previous()))
                    {
                        nextStatus = (int)(((CheckOutStatusEnum.TaghirReshteStatus)currentstatus).Next().Next());
                    }
                    else
                    {
                        nextStatus = (int)(((CheckOutStatusEnum.TaghirReshteStatus)currentstatus).Next());
                    }
                    break;
                case (int)CheckOutStatusEnum.CheckOutType.enseraf:
                    if (currentstatus == (int)CheckOutStatusEnum.EnserafReqStatus.end)
                    {
                        nextStatus = (int)CheckOutStatusEnum.EnserafReqStatus.end;
                    }
                    else
                    {
                        nextStatus = (int)(((CheckOutStatusEnum.CheckOutAllStatusEnum)currentstatus).Next());
                    }
                    break;
                case (int)CheckOutStatusEnum.CheckOutType.enteqali:
                    if (currentstatus == (int)CheckOutStatusEnum.EnteghaliStatus.end)
                    {
                        nextStatus = (int)CheckOutStatusEnum.EnteghaliStatus.end;
                    }
                    else
                    {
                        nextStatus = (int)(((CheckOutStatusEnum.EnteghaliStatus)currentstatus).Next());
                    }
                    break;
                default:
                    break;
            }
            if (currentstatus == Convert.ToInt32(CheckOutStatusEnum.FareghReqStatus.mashmulan_ok.Previous()) && !isMale(issuerID))
            {
                nextStatus = (int)(CheckOutStatusEnum.FareghReqStatus.mashmulan_ok).Next();
            }
            return nextStatus;
        }

        private int GetPreviousStatus(int currentstatus, int reqType)
        {
            int previousstatus = 0;
            switch (reqType)
            {
                case (int)CheckOutStatusEnum.CheckOutType.fareq_tahsil:
                    previousstatus = (int)(((CheckOutStatusEnum.FareghReqStatus)currentstatus).Previous());
                    break;
                case (int)CheckOutStatusEnum.CheckOutType.ekhraj:
                    previousstatus = (int)(((CheckOutStatusEnum.EkhrajStatus)currentstatus).Previous());
                    break;
                case (int)CheckOutStatusEnum.CheckOutType.taqir_reshte:
                    previousstatus = (int)(((CheckOutStatusEnum.TaghirReshteStatus)currentstatus).Previous());
                    break;
                case (int)CheckOutStatusEnum.CheckOutType.enseraf:
                    previousstatus = (int)(((CheckOutStatusEnum.EnserafReqStatus)currentstatus).Previous());
                    break;
                case (int)CheckOutStatusEnum.CheckOutType.enteqali:
                    previousstatus = (int)(((CheckOutStatusEnum.EnteghaliStatus)currentstatus).Previous());
                    break;
                default:
                    break;
            }
            return previousstatus;
        }


        public DataTable GetUsers()
        {
            return ReqDAO.GetUsers();
        }

        public int GetIsBachelor(string stcode)
        {
            return ReqDAO.GetIsBachelor(stcode);
        }
        public DataTable GetCheckOutInfoByStCode(string stcode)
        {
            return ReqDAO.GetCheckOutInfoByStCode(stcode);
        }

        public DataTable GetCheckOutInfoByStCodeAndFamily(FeraghatTahsilDTO oFeraghat)
        {
            return ReqDAO.GetCheckOutInfoByStCodeAndFamily(oFeraghat);
        }

        public DataTable UpdateStatusOfStMsg(int reqID)
        {
            return ReqDAO.UpdateStatusOfStMsg(reqID);
        }

        public DataTable GetListOfStatusByRoleId(int RoleId)
        {
            return ReqDAO.GetCheckOutStatusByRoleId(RoleId);
        }
        public DataTable UpdateStatusOfStMsgUnRead(int reqID)
        {
            return ReqDAO.UpdateStatusOfStMsgUnRead(reqID);
        }
        public DataTable GetListOFRequestByNextStatus(int nextstatus)
        {
            return ReqDAO.GetListOFRequestByNextStatusID(nextstatus);
        }
        public DataTable GetListOFRequestByNextStatus_Excel(int nextstatus, int daneshID)
        {
            return ReqDAO.GetListOFRequestByNextStatusID_Excel(nextstatus, daneshID);
        }
        public DataTable GetListOFRequestByNextStatus_BythesisFileStatus(int nextstatus, int thesisFileThesis)
        {
            return ReqDAO.GetListOFRequestByNextStatusID_BythesisFileStatus(nextstatus, thesisFileThesis);
        }
        public DataTable GetListOFRequestByNextStatusForFaregh(int nextstatus)
        {
            return ReqDAO.GetListOFRequestByNextStatusIDForFaregh(nextstatus);
        }
        public DataTable GetListOFRequestByNextStatusAndDaneshId(int nextstatus, int daneshId)
        {
            return ReqDAO.GetListOFRequestByNextStatusAndDaneshId(nextstatus, daneshId);
        }

        public DataTable GetListOFRequestByNextStatusAndArchiveRole(int nextstatus, int roleID)
        {
            return ReqDAO.GetListOFRequestByNextStatusAndArchiveRole(nextstatus, roleID);
        }

        public DataTable GetListOFRequestByCurrentStatus(int status)
        {
            return ReqDAO.GetListOFRequestByCurrentStatusID(status);
        }

        public string GetCheckOutStudentIDByReqID(int reqID)
        {
            return ReqDAO.GetCheckOutIssuerID(reqID);
        }

        public int GetStatusOfRole(int roleID)
        {
            int status = 1000;
            switch (roleID)
            {
                case 32://moavenat amoozeshi
                    status = (int)CheckOutStatusEnum.CheckOutAllStatusEnum.Moavenat_Amoozesh;
                    break;


                case (int)(DTO.RoleEnums.مدیر_دانشکده_فنی_و_مهندسی)://15
                case (int)(DTO.RoleEnums.مدیر_دانشکده_مدیریت)://16:Modir Daneshkade modiriat
                case (int)(DTO.RoleEnums.مدیر_دانشکده_انسانی)://17:Modir Daneshkade ensani
                case (int)(DTO.RoleEnums.کارشناس_دانشکده_فنی_و_مهندسی): // 26://karshenas amoozesh fani
                case (int)(DTO.RoleEnums.کارشناس_دانشکده_مدیریت): // 27://karshenas amoozesh modiriat
                case (int)(DTO.RoleEnums.کارشناس_دانشکده_انسانی): // 28://karshenas amoozesh ensani
                case (int)(DTO.RoleEnums.مدیر_دانشکده_علوم_پایه_و_فناوری_های_نوین): // 67://Modir Daneshkade payeh
                case (int)(DTO.RoleEnums.کارشناس_دانشکده__علوم_پایه_و_فناوری_های_نوین): // 68://karshenas amoozesh payeh
                case (int)(DTO.RoleEnums.رئیس_دانشکده_علوم_پایه_و_فناوری_های_نوین): // 66:
                    status = (int)CheckOutStatusEnum.FareghReqStatus.daneshkade_ok;
                    break;


                case (int)(DTO.RoleEnums.مدیر_آموزش): // 2://amoozesh
                    status = (int)CheckOutStatusEnum.FareghReqStatus.amoozesh_ok;
                    break;

                case (int)(DTO.RoleEnums.مدیر_دانشجویی): // 21://modir daneshjooyi
                case (int)(DTO.RoleEnums.کارشناس_دانشجویی): // 22:
                case (int)(DTO.RoleEnums.معاونت_دانشجویی): // 62:
                    status = (int)CheckOutStatusEnum.FareghReqStatus.daneshjooyi_ok;
                    break;


                case (int)(DTO.RoleEnums.مدیر_کل_امور_پژوهشی): // 9://modir pajoohesh
                case (int)(DTO.RoleEnums.کارشناس_پژوهش): // 10:
                case (int)(DTO.RoleEnums.معاونت_پژوهش): // 64://moavene pajoohesh
                    status = (int)CheckOutStatusEnum.FareghReqStatus.pajohesh_ok;
                    break;


                case (int)(DTO.RoleEnums.مدیر_امور_رفاهی): // 23://modir refah
                case (int)(DTO.RoleEnums.کارشناس_امور_رفاهی): // 49://karshenas refah
                case (int)(DTO.RoleEnums.معاون_اداری): // 50://moavene edari
                    status = (int)CheckOutStatusEnum.FareghReqStatus.refah_ok;
                    break;


                case (int)(DTO.RoleEnums.مدیر_مالی): // 4://modir maali
                case (int)(DTO.RoleEnums.کارشناس_مالی): //  5://karshenas maali          
                    status = (int)CheckOutStatusEnum.FareghReqStatus.maali_ok;
                    break;


                case (int)(DTO.RoleEnums.معاون_فنی): // 6://modir fani
                case (int)(DTO.RoleEnums.کارشناس_فنی): // 7://karshenas fani 
                case (int)(DTO.RoleEnums.مسئول_پشتیبان): // 18://masool poshtibani
                    status = (int)CheckOutStatusEnum.FareghReqStatus.fani_ok;
                    break;


                case (int)(DTO.RoleEnums.امور_مشمولان): // 29://mashmoolan
                    status = (int)CheckOutStatusEnum.FareghReqStatus.mashmulan_ok;
                    break;


                case (int)(DTO.RoleEnums.مسئول_بایگانی_دانشکده_علـوم_انسانی): // 72://baygani ensani
                case (int)(DTO.RoleEnums.مسئول_بایگانی_دانشکده_فنی_و_مهندسی): // 69://baygani fani
                case (int)(DTO.RoleEnums.مسئول_بایگانی_دانشکده_مدیریت): // 70://baygani modiriat
                case (int)(DTO.RoleEnums.مسئول_بایگانی_دانشکده_علـوم_پایه): // 71://baygani oloom payeh
                    status = (int)CheckOutStatusEnum.FareghReqStatus.archive_ok;
                    break;


                //case (int)(DTO.RoleEnums.دبیرخانه): // 76://karmande dabirkhane
                //    status = (int)CheckOutStatusEnum.FareghReqStatus.dabirkhane_ok;
                //    break;

                case (int)(DTO.RoleEnums.کارشناس_امور_فارغ_التحصیلان): // 30://karshenas fareghotahsilan
                case (int)(DTO.RoleEnums.رئیس_امور_فارغ_التحصیلان): // 35://modir fareghotahsilan
                    status = (int)CheckOutStatusEnum.FareghReqStatus.vrood_moavenat_ok;
                    break;

                default:
                    break;
            }
            return status;
        }

        public string GetStatusNote(int status)
        {
            string note = "";
            switch (status)
            {
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.submited:
                    note = "ثبت شده ";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.Moavenat_Amoozesh:
                    note = "تایید معاونت آموزشی ";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.daneshkade:
                    note = "تایید آموزش دانشکده ";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.amoozesh:
                    note = "تایید آموزش کل ";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.daneshjooyi:
                    note = "تایید امور دانشجویی ";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.pajohesh:
                    note = "تایید پژوهش ";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.refah:
                    note = "تایید امور رفاه ";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.maali:
                    note = "تایید امور مالی ";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.archive:
                    note = "تایید بایگانی";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.mashmulan:
                    note = "تایید امور مشمولان ";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.fani:
                    note = "تایید فنی ";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.takmil_parvande:
                    note = " تکمیل پرونده در دانشکده";
                    break;
                //case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.dabirkhane:
                //    note = "دبیرخانه";
                //    break;

                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.vrood_moavenat:
                    note = "ورود پرونده به معاونت دانشجویی";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.ersal_sodoor:
                    note = "ارسال پرونده جهت صدور مدرک ";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.end:
                    note = "پایان";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.stampPay:
                    note = "پرداخت وجه تمبر";
                    break;

                default:
                    note = "رد درخواست";
                    break;
            }
            return note;
        }

        public string GetPersianStatus(int status)
        {
            string step;
            switch (status)
            {
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.submited:
                    step = "ثبت شده";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.Moavenat_Amoozesh:
                    step = " معاونت آموزشی";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.daneshkade:
                    step = "آموزش دانشکده ";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.amoozesh:
                    step = "آموزش کل ";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.daneshjooyi:
                    step = " امور دانشجویی";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.pajohesh:
                    step = " پژوهش";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.refah:
                    step = " امور رفاه";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.maali:
                    step = " امور مالی";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.fani:
                    step = " فنی";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.takmil_parvande:
                    step = "تکمیل پرونده در دانشکده ";
                    break;
                //case (int)CheckOutStatusEnum.FareghReqStatus.dabirkhane_ok:
                //    step = "دبیرخانه ";
                //    break;

                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.vrood_moavenat:
                    step = " ورود پرونده به معاونت دانشجویی";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.ersal_sodoor:
                    step = " ارسال پرونده جهت صدور مدرک";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.mashmulan:
                    step = " امور مشمولان";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.archive:
                    step = " بایگانی";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.end:
                    step = "پایان";
                    break;
                case (int)CheckOutStatusEnum.CheckOutAllStatusEnum.stampPay:
                    step = "پرداخت وجه تمبر";
                    break;
                default:
                    step = "";
                    break;
            }
            return step;
        }

        public byte[] GetSignByStatus(CheckOutStatusEnum.FareghReqStatus reqStatus)
        {
            int status = Convert.ToInt32(reqStatus);
            return ReqDAO.GetSignByStatus(status);
        }

        public DataTable GetCheckOutTypes()
        {
            return ReqDAO.GetCheckOutTypes();
        }

        public DataTable checkExistingRequest(string stcode)
        {
            return ReqDAO.checkExistingRequest(stcode);
        }

        public DataTable GetonlineStatus(string stcode)
        {
            return ReqDAO.GetonlineStatus(stcode);
        }

        public DataTable GetListOFRequestByTypeID(int TypeID)
        {
            return ReqDAO.GetListOFRequestByTypeID(TypeID);
        }

        public DataTable GetUserRole(int userID)
        {
            return ReqDAO.GetUserRoleByUserID(userID);
        }

        private bool isGraduated(string userID)
        {
            DataTable dt = new DataTable();
            LoginBusiness logBusiness = new LoginBusiness();
            dt = logBusiness.GetStIdVaz(userID);
            if (dt.Rows[0]["idvazkol"].ToString() == "7")
            {
                return true;
            }
            return false;
        }

        public bool isMashmool(string issuerID)
        {
            return ReqDAO.CheckIsMashmool(issuerID);
        }
        public int isMashmoolferaghat(string issuerID)
        {
            return ReqDAO.CheckIsMashmoolFeraghat(issuerID);
        }
        public byte[] GetSignByUserAndCartable(string userID, int cartable)
        {
            DataTable sign = GetSignInfByUserAndCartable(userID, cartable);
            if (sign.Rows.Count > 0)
            {
                var img = (byte[])sign.Rows[0]["SignImage"];
                return img;
            }
            return null;
            //return ReqDAO.GetSignByUser(userID);
        }
        public DataTable GetSignInfByUserAndCartable(string userID, int cartable)
        {
            DataTable dt = GetSignInfByUser(userID);
            if (dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select("RequestLogID=" + cartable);
                if (dr.Length > 0)
                {
                    DataTable dtReturn = dr.CopyToDataTable();
                    return dtReturn;
                }
            }
            return new DataTable();
        }
        public DataTable GetSignInfByUser(string userID)
        {
            return ReqDAO.GetSignByUser(userID);
        }

        public DataTable GetAllSigns()
        {
            return ReqDAO.GetAllSigns();
        }

        public DataTable GetCheckOutInfoByReqId(int reqID)
        {
            return ReqDAO.GetCheckOutInfoByReqId(reqID);
        }

        public int GetDaneshKadeIdByRoleId(int roleId)
        {
            int DaneshId = 0;
            switch (roleId)
            {
                case 17:
                case 28:
                case 51://raeis_ensani
                    DaneshId = 1;
                    break;
                case 72:
                    //ensani
                    DaneshId = 1;
                    break;
                case 15:
                case 26:
                case 53://raeis_fani
                    DaneshId = 2;
                    break;
                case 69:
                    //fani
                    DaneshId = 2;
                    break;
                case 16:
                case 27:
                case 52://raeis_Modiriat
                    DaneshId = 3;
                    break;
                case 70:
                    //modiriat
                    DaneshId = 3;
                    break;
                case 13:
                case 14:
                    //kootah modat
                    DaneshId = 5;
                    break;
                case 66:
                    DaneshId = 8;
                    break;
                case 67:
                case 68:

                case 71:
                    //oloum payeh
                    DaneshId = 8;
                    break;
                default:
                    break;
            }
            return DaneshId;
        }

        public DataTable GetListOFRequestByCurrentStatusDaneshId(int status, int daneshId)
        {
            return ReqDAO.GetListOFRequestByCurrentStatusDaneshId(status, daneshId);
        }
        public void UpdateStatusInReg(string stcode, int reqtypeId, int currentstatus)
        {
            if (currentstatus == (int)CheckOutStatusEnum.FareghReqStatus.maali_ok)
                ReqDAO.UpdateStatusInReg(stcode, reqtypeId);
            //if (reqtypeId == 15 && currentstatus == (int)CheckOutStatusEnum.FareghReqStatus.archive_ok)
            //    ReqDAO.UpdateStatusInReg(stcode, reqtypeId);
        }
        public int GetCheckOutStatusByreqID(int reqID)
        {
            return ReqDAO.GetCheckOutStatusByreqID(reqID);
        }

        public List<MahaleSodoor> GetListOfVahed()
        {
            return ReqDAO.GetListOfVahed();
        }
        public List<Field> GetListOfReshte()
        {
            return ReqDAO.GetListOfReshte();
        }
        public List<Daneshkade> GetListOfDaneshkade()
        {
            return ReqDAO.GetListOfDaneshkade();
        }
        public DataTable GetNameOfVahedByVahedID(int vahedID)
        {
            return ReqDAO.GetNameOfVahedByVahedID(vahedID);
        }
        public DataTable GetListOFRequestByVahedAndDate(int idVahed, string startDate, string endDate, int MadrakType)
        {
            return ReqDAO.GetListOFRequestByVahedAndDate(idVahed, startDate, endDate, MadrakType);
        }

        public DataTable getAllArchiveID_GraduateDocuments()
        {
            return ReqDAO.getAllArchiveID_GraduateDocuments();
        }
        public DataTable GetListOfMadarekByDateSodoor(DataTable idVahed, string startDate, string endDate, DataTable idResh, DataTable iddanesh, int idCaseStatus, int idMadrakStatus, int madrakTypeid)
        {
            return ReqDAO.GetListOfMadarekByDateSodoor(idVahed, startDate, endDate, idResh, iddanesh, idCaseStatus, idMadrakStatus, madrakTypeid);
        }
        public DataTable GetListOfCaseInKarTabl(DataTable idVahed, string startDate, string endDate, DataTable idResh, DataTable iddanesh, int idCaseStatus, int idMadrakStatus, int madrakTypeid)
        {
            return ReqDAO.GetListOfCaseInKarTabl(idVahed, startDate, endDate, idResh, iddanesh, idCaseStatus, idMadrakStatus, madrakTypeid);
        }
        public DataTable GetListOfMadrakVoroodUni(DataTable idVahed, string startDate, string endDate, DataTable idResh, DataTable iddanesh, int idCaseStatus, int idMadrakStatus, int madrakTypeid)
        {
            return ReqDAO.GetListOfMadrakVoroodUni(idVahed, startDate, endDate, idResh, iddanesh, idCaseStatus, idMadrakStatus, madrakTypeid);
        }
        public DataTable GetListOfExitCaseFromKartabl(DataTable idVahed, string startDate, string endDate, DataTable idResh, DataTable iddanesh, int idCaseStatus, int idMadrakStatus, int madrakTypeid)
        {
            return ReqDAO.GetListOfExitCaseFromKartabl(idVahed, startDate, endDate, idResh, iddanesh, idCaseStatus, idMadrakStatus, madrakTypeid);
        }
        public void InsertApproveDatebyFaregh(int reqId)
        {
            ReqDAO.InsertApproveDatebyFaregh(reqId);
        }
        public void InsertApproveDatebyMali(int reqId)
        {
            ReqDAO.InsertApproveDatebyMali(reqId);
        }

        public DataTable GetListOFApproveList()
        {
            return ReqDAO.GetListOFApproveList();
        }
        public void SetSendToPay(List<int> reqId, int userId, string IpAddress)
        {
            ReqDAO.SetSendToPay(reqId, userId, IpAddress);
        }
        public void SetDateApprove(List<int> reqId, int userId, string IpAddress)
        {
            ReqDAO.SetDateApprove(reqId, userId, IpAddress);
        }
        public void AddSignature(byte[] imageBytes, int identityNumber, int appId)
        {
            ReqDAO.AddSignature(imageBytes, identityNumber, appId);
        }
        public int exist_IdMelli(string StCode)
        {
            return ReqDAO.exist_IdMelli(StCode);
        }
        public DataTable GetSignByLogDate(int State, int reqID)
        {
            return ReqDAO.GetSignByLogDate(State, reqID);
        }
        public DataTable GetLogDatesignByModifyID(int reqID)
        {
            return ReqDAO.GetLogDatesignByModifyID(reqID);
        }
        public DataTable GetfishNaghsIdBystcode(string stcode)
        {
            return ReqDAO.GetfishNaghsIdBystcode(stcode);
        }

        public bool hasStampDefect(Int64 reqID)
        {
            var naghs = ReqDAO.getStampRecieptDefect(reqID);
            if (naghs.Rows.Count > 0)
            {
                DataRow[] dr = naghs.Select("RequestLogID=29");
                if (dr.Length > 0)
                    return true;
            }
            return false;
        }

        public bool hasDefencePaymentInquery(string stcode)
        {
            DataTable dt = RPB.GetDefencePaymentByStcode(stcode);
            if (dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select("AppStatus='commit'");

                return dr.Length == 0;
            }
            return false;
        }
        public DataTable getStampRecieptDefect(Int64 reqID)
        {
            return ReqDAO.getStampRecieptDefect(reqID);
        }

        public int insertCheckoutReason(Int64 requestID, string stcode, string reason)
        {
            return ReqDAO.insertCheckoutReason(requestID, stcode, reason);
        }
        public int updateCheckoutReasonFrequency(Int64 requestID, string stcode, int frequencyID)
        {
            return ReqDAO.updateCheckoutReasonFrequency(requestID, stcode, frequencyID);
        }
        public DataTable SelectCheckOutReasons(Int64 requestID = 0)
        {
            return ReqDAO.SelectCheckOutReasons(requestID);
        }
        public DataTable SelectFrequencyReasons()
        {
            return ReqDAO.SelectFrequencyReasons();
        }

        public DataTable getCheckoutFrequencyIteration()
        {
            return ReqDAO.getCheckoutFrequencyIteration();
        }
        public DataTable getCheckoutFrequencyAllStudents()
        {
            return ReqDAO.getCheckoutFrequencyAllStudents();
        }
        public DataTable getCheckoutFrequency_ByDepartment()
        {
            return ReqDAO.getCheckoutFrequencyByDepartment();
        }
        public DataTable getCheckoutFrequency_ByEnterYear()
        {
            return ReqDAO.getCheckoutFrequencyByEnterYear();
        }
        public DataTable getCheckoutFrequency_ByLevel()
        {
            return ReqDAO.getCheckoutFrequencyByLevel();
        }
    }
}
