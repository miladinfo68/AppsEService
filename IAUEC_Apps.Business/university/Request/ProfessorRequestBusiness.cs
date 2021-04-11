using IAUEC_Apps.DAO.University.Request;
using IAUEC_Apps.DTO.University.Request;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.University.Faculty;

namespace IAUEC_Apps.Business.university.Request
{
    public class ProfessorRequestBusiness
    {
        ProfessorRequestDAO reqDAO = new ProfessorRequestDAO();

        public int AddNewEditRequest(ProfessorEditRequestDTO oEditDTO)
        {
            int Id = reqDAO.AddNewEditRequest(oEditDTO);

            if (Id > 0)
            {
                if (oEditDTO.RequestTypeID == (int)RequestTypeId.EditHokm)
                {
                   
                        oEditDTO.Hokm.EditRequestId = Id;
                        if (oEditDTO.Hokm.Martabeh >= 0 && oEditDTO.Hokm.Martabeh < 8)
                            reqDAO.AddNewHokm(oEditDTO.Hokm);
                    
                }
                //else if(oEditDTO.Hokm.Martabeh <= 0)
                //{
                //    // Set Martabe
                //}
                else
                {
                    reqDAO.AddChangeList(Id, oEditDTO.ChangeList);
                }
                if (oEditDTO.ScanList != null)
                    addNewEditRequestScan(oEditDTO, Id);
                return Id;
            }
            else
            {
                return 0;
            }
        }

        private bool addNewEditRequestScan(ProfessorEditRequestDTO oEditDTO, int reqId)
        {
            try
            {
                foreach (int _key in oEditDTO.ScanList.Keys)
                {
                    ImageStructure _img;
                    bool hasValue = oEditDTO.ScanList.TryGetValue(_key, out _img);
                    if (hasValue)
                    {
                        reqDAO.AddNewEditRequestScan(_key, _img.image, _img.imageUrl, oEditDTO.Code_Ostad, reqId);
                    }
                }
                return true;
            }
            catch { return false; }
        }

        public DataTable GetAllRequestsByProfCode(int codeostad)
        {
            return reqDAO.GetAllRequestsByProfCode(codeostad);
        }

        public DataTable GetAllRequestsByHRCode(int codeostad)
        {
            return reqDAO.GetAllRequestsByHRCode(codeostad);
        }

        public DataTable GetAllRequestDocsByProfCode(int codeostad)
        {
            return reqDAO.GetAllRequestDocsByProfCode(codeostad);
        }

        public DataTable GetChangeListByReqId(int reqId)
        {
            return reqDAO.GetChangeListByReqId(reqId);
        }

        public List<ProfessorEditRequestDTO> GetRequestByTypeAndStatus(string reqType, string reqStatus)
        {
            return reqDAO.GetRequestByTypeAndStatus(reqType, reqStatus);
        }

        public DataTable getProfessorRequests_Scan(int codeOstad, int docStatus,int reqID)
        {
            try
            {
                DataTable dtDoc = reqDAO.getScanOfProfessor(codeOstad, docStatus,reqID);
                return dtDoc;
            }
            catch { return new DataTable(); }
        }

        public int UpdateOneChangeById(int reqId, string NewValue, int state)
        {
            return reqDAO.UpdateOneChangeById(reqId, NewValue, state);
        }

        public int UpdateProfessorRequestStatus(int reqId, int RequestLogId)
        {
            return reqDAO.UpdateProfessorRequestStatus(reqId, RequestLogId);
        }

        public bool UpdateProfessorRequestStatus_Doc(int reqId, int status)
        {
            DataTable dtScan = reqDAO.getScanOfProfessor(reqId);

            foreach (DataRow dr in dtScan.Rows)
            {
                if (!reqDAO.updateProffessorScanStatus(Convert.ToInt64(dr["CodeOstad"]), reqId, Convert.ToInt32(dr["DocType"]), status))
                    return false;
            }
            return true;
        }

        public bool UpdateOstadInformation_AfterApprove(int requestId)
        {

            return reqDAO.UpdateOstadInformation_AfterApprove(requestId);
        }

        public bool InsertDocumentsToHr(int reqId, int hrCode)
        {
            try
            {
                DataTable dtScan = reqDAO.getScanOfProfessor(reqId);
                if (dtScan.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtScan.Rows)
                    {
                        string ext = dr["scanUrl"].ToString();
                        int ind = dr["scanUrl"].ToString().LastIndexOf('.');
                        ext = ext.Substring(ind + 1);
                        if (!reqDAO.InsertDocumentToHr(hrCode, (byte[])(dr["ScanImage"]), Convert.ToInt32(dr["DocType"]), 1, ext))
                            return false;
                        if (!reqDAO.updateProffessorScanStatus(Convert.ToInt64(dr["CodeOstad"]), reqId, Convert.ToInt32(dr["DocType"]), 1))
                            return false;
                    }
                }
            }
            catch { return false; }
            return true;
        }

        public bool InsertDocumentToHr(int hrId, byte[] image, int docType, int status, string extention)
        {
            return reqDAO.InsertDocumentToHr(hrId, image, docType, 1,extention);
        }

        public DataTable GetProfessorEditInfoFieldsByProfessorRequestId(int requestId)
        {
            return reqDAO.GetProfessorEditInfoFieldsByProfessorRequestId(requestId);
        }

        public DataTable getCustomizedProfessorEditedFields(int requestId)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("fieldName");
            dt.Columns.Add("oldValue");
            dt.Columns.Add("newValue");

            DataTable dtFields = GetProfessorEditInfoFieldsByProfessorRequestId(requestId);
            if (dtFields.Rows.Count > 0)
            {
                DataTable dtfCoding;
                Faculty.FacultyReportsBusiness FRB = new Faculty.FacultyReportsBusiness();
                Common.CommonBusiness CB = new Common.CommonBusiness();
                DataTable dtFieldName = FRB.GetAllControlToSidaFields();
                foreach (DataRow dr in dtFields.Rows)
                {
                    DataRow drNew = dt.NewRow();
                    DataRow[] drFieldName = dtFieldName.Select("id=" + dr["controlToFieldId"]);
                    if (drFieldName.Length == 1)
                    {
                        drNew["fieldName"] = drFieldName[0]["Description"];
                        drNew["oldValue"] = "";
                        drNew["newValue"] = "";


                        if (Convert.ToInt32(drFieldName[0]["codingid"]) != 0)//مقادیر را از جدول بخواند
                        {
                            dtfCoding = CB.GetCodingByTypeId(Convert.ToInt32(drFieldName[0]["codingId"]));
                            if (dtfCoding.Rows.Count > 0)
                            {
                                DataRow[] drfCoding;
                                if (dr["oldValue"] != DBNull.Value && dr["oldValue"].ToString() != "")
                                {
                                    drfCoding = dtfCoding.Select("id=" + dr["oldValue"]);
                                    if (drfCoding.Length == 1)
                                    {
                                        drNew["oldValue"] = drfCoding[0]["namecoding"];
                                    }
                                }
                                if (dr["newValue"] != DBNull.Value && dr["newValue"].ToString() != "")
                                {
                                    drfCoding = dtfCoding.Select("id=" + dr["newValue"]);
                                    if (drfCoding.Length == 1)
                                    {
                                        drNew["newValue"] = drfCoding[0]["namecoding"];
                                    }
                                }
                            }
                        }
                        else if (Convert.ToInt32(drFieldName[0]["id"]) == 44)//وضعیت بازنشستگی
                        {
                            if (dr["oldvalue"] != DBNull.Value)
                                drNew["oldvalue"] = dr["oldvalue"].ToString() == "1" ? "بازنشسته" : "شاغل";
                            if (dr["newvalue"] != DBNull.Value)
                                drNew["newvalue"] = dr["newvalue"].ToString() == "1" ? "بازنشسته" : "شاغل";
                        }
                        else if (Convert.ToInt32(drFieldName[0]["id"]) == 53)//جنسیت
                        {
                            if (dr["oldvalue"] != DBNull.Value)
                                drNew["oldvalue"] = dr["oldvalue"].ToString() == "1" ? "مرد" : "زن";
                            if (dr["newvalue"] != DBNull.Value)
                                drNew["newvalue"] = dr["newvalue"].ToString() == "1" ? "مرد" : "زن";

                        }
                        else if (Convert.ToInt32(drFieldName[0]["id"]) == 45 || Convert.ToInt32(drFieldName[0]["id"]) == 47)
                        {
                            dtfCoding = CB.GetOstan();
                            if (dtfCoding.Rows.Count > 0)
                            {
                                DataRow[] drfCoding = dtfCoding.Select("id=" + dr["oldValue"]);
                                if (drfCoding.Length == 1)
                                {
                                    drNew["oldValue"] = drfCoding[0]["title"];
                                }
                                drfCoding = dtfCoding.Select("id=" + dr["newValue"]);
                                if (drfCoding.Length == 1)
                                {
                                    drNew["newValue"] = drfCoding[0]["title"];
                                }
                            }
                        }
                        else if (Convert.ToInt32(drFieldName[0]["id"]) == 46 || Convert.ToInt32(drFieldName[0]["id"]) == 48)
                        {
                            dtfCoding = CB.getShahrestan();
                            if (dtfCoding.Rows.Count > 0)
                            {
                                DataRow[] drfCoding = dtfCoding.Select("id=" + dr["oldValue"]);
                                if (drfCoding.Length == 1)
                                {
                                    drNew["oldValue"] = drfCoding[0]["title"];
                                }
                                drfCoding = dtfCoding.Select("id=" + dr["newValue"]);
                                if (drfCoding.Length == 1)
                                {
                                    drNew["newValue"] = drfCoding[0]["title"];
                                }
                            }
                        }
                        else if(Convert.ToInt32(drFieldName[0]["id"]) == 54)
                        {
                            DataTable dtAllDep = CB.GetAllDepartman();// گرفتن دپارتمان ها با توجه به دانشکده
                            DataTable dtShowDepOld = new DataTable();
                            DataTable dtShowDepNew = new DataTable();
                            string NewValue = dr["newvalue"].ToString();
                            string OldValue = dr["oldvalue"].ToString();


                            NewValue = NewValue.EndsWith(",") ? NewValue.TrimEnd(',') : NewValue;
                            OldValue = OldValue.EndsWith(",") ? OldValue.TrimEnd(',') : OldValue;

                            DataRow[] drSelectedDepOld = new DataRow[0]; if (OldValue.Length > 0) drSelectedDepOld = dtAllDep.Select("id in(" + OldValue + ")");
                            DataRow[] drSelectedDepNew = new DataRow[0]; if (NewValue.Length > 0) drSelectedDepNew = dtAllDep.Select("id in(" + NewValue + ")");

                            foreach(DataRow drOld in drSelectedDepOld)
                            {
                                drNew["oldValue"] += drOld["NameGroup"].ToString().Replace("دپارتمان","")+",";
                            }
                            foreach (DataRow drDepNew in drSelectedDepNew)
                            {
                                drNew["newValue"] += drDepNew["namegroup"].ToString().Replace("دپارتمان", "") + ",";
                            }
                            drNew["oldValue"] = drNew["oldValue"].ToString().TrimEnd(',');
                            drNew["newValue"] = drNew["newValue"].ToString().TrimEnd(',');
                        }
                        else if(Convert.ToInt32(drFieldName[0]["id"]) == 42)
                        {
                            if (dr["oldvalue"] == DBNull.Value)
                                drNew["oldvalue"] = "مشخص نشده";
                            else
                            {
                                switch (Convert.ToInt32(dr["oldvalue"]))
                                {
                                    case 1:
                                        drNew["oldvalue"] = "همکاری برای تدریس";
                                        break;
                                    case 2:
                                        drNew["oldvalue"] = "همکاری برای مشاوره یا راهنمایی پروژه";
                                        break;
                                    case 3:
                                        drNew["oldvalue"] = "همکاری برای تدریس و مشاوره یا راهنمایی پروژه";
                                        break;
                                    default:
                                        drNew["oldvalue"] = "مشخص نشده";
                                        break;
                                }
                            }
                            if (dr["newvalue"] == DBNull.Value)
                                drNew["newvalue"] = "مشخص نشده";
                            else
                            {
                                switch (Convert.ToInt32(dr["newvalue"]))
                                {
                                    case 1:
                                        drNew["newvalue"] = "همکاری برای تدریس";
                                        break;
                                    case 2:
                                        drNew["newvalue"] = "همکاری برای مشاوره یا راهنمایی پروژه";
                                        break;
                                    case 3:
                                        drNew["newvalue"] = "همکاری برای تدریس و مشاوره یا راهنمایی پروژه";
                                        break;
                                    default:
                                        drNew["newvalue"] = "مشخص نشده";
                                        break;
                                }
                            }
                        }
                        else//همان مقدار را برگرداند
                        {
                            if (dr["oldvalue"] != DBNull.Value)
                                drNew["oldvalue"] = dr["oldvalue"];
                            if (dr["newvalue"] != DBNull.Value)
                                drNew["newvalue"] = dr["newvalue"];
                        }
                        dt.Rows.Add(drNew);
                    }

                }
            }
            else
            {
                Common.CommonBusiness CB = new Common.CommonBusiness();
                ProfessorHokmDTO newHokm = GetNewHokmInfo(requestId);
                if (newHokm.InfoPeopleId > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["fieldName"] = "مرتبه دانشگاهی";
                    switch (newHokm.Martabeh)
                    {
                        case 0:
                            dr["newValue"] = "فاقد مرتبه علمی";
                            break;
                        case 1:
                            dr["newValue"] = "مربی";
                            break;
                        case 2:
                            dr["newValue"] = "دانشیار";
                            break;
                        case 3:
                            dr["newValue"] = "استادیار";
                            break;
                        case 4:
                            dr["newValue"] = "استاد";
                            break;
                    }
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    dr["fieldName"] = "پایه";
                    dr["newValue"] = newHokm.Payeh;
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    dr["fieldName"] = "نوع استخدام";
                    switch (newHokm.Type_Estekhdam)
                    {
                        case 0:
                            dr["newValue"] = "رسمی";
                            break;
                        case 1:
                            dr["newValue"] = "پیمانی";
                            break;
                        case 2:
                            dr["newValue"] = "قراردادی";
                            break;
                        case 3:
                            dr["newValue"] = "آزمایشی";
                            break;
                    }
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    dr["fieldName"] = "نام دانشگاه محل خدمت";
                    if (newHokm.Uni_Khedmat != 0)
                    {
                        DataRow[] drUni = CB.GetNameUni_fcoding().Select("id=" + newHokm.Uni_Khedmat);
                        if(drUni.Length==1)
                        dr["newValue"] = drUni[0]["namecoding"];
                    }
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    dr["fieldName"] = "نوع دانشگاه محل خدمت";
                    switch (newHokm.Uni_KhedmatType)
                    {
                        case 1:
                            dr["newValue"] = "دولتی";
                            break;
                        case 2:
                            dr["newValue"] = "آزاد";
                            break;
                        case 3:
                            dr["newValue"] = "حوزه";
                            break;
                        case 4:
                            dr["newValue"] = "خارج از کشور";
                            break;
                        case 5:
                            dr["newValue"] = "سایر";
                            break;

                    }
                    
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    dr["fieldName"] = "تاریخ صدور حکم کارگزینی";
                    dr["newValue"] = newHokm.Date_Hokm;
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    dr["fieldName"] = "تاریخ اجرای حکم کارگزینی";
                    dr["newValue"] = newHokm.DateRunHokmHere;
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    dr["fieldName"] = "شماره حکم";
                    dr["newValue"] = newHokm.Number_Hokm;
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    dr["fieldName"] = "مبلغ حکم";
                    dr["newValue"] = string.Format("{0:C}",newHokm.MablaghHokm);
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    dr["fieldName"] = "متقاضی تکمیل ساعت موظفی";
                    dr["newValue"] = newHokm.BoundHour ? "می باشم" : "نمی باشم";
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                    dr["fieldName"] = "نحوه همکاری";
                    switch (newHokm.Nahveh_Hamk)
                    {
                        case 1:
                            dr["newValue"] = string.Format("{0} {1}", "تمام وقت 32 ساعت", newHokm.Nahveh_Hamk);
                            break;
                        case 2:
                            dr["newValue"] = string.Format("{0} {1}", "نیمه وقت", newHokm.Nahveh_Hamk);
                            break;
                        case 3:
                            dr["newValue"] = string.Format("{0} {1}", "ساعتی", newHokm.Nahveh_Hamk);
                            break;
                        case 4:
                            dr["newValue"] = string.Format("{0} {1}", "تمام وقت طرح مشمولان", newHokm.Nahveh_Hamk);
                            break;
                        case 5:
                            dr["newValue"] = string.Format("{0} {1}", "بورسیه دکتری", newHokm.Nahveh_Hamk);
                            break;
                        case 6:
                            dr["newValue"] = string.Format("{0} {1}", "کارمند", newHokm.Nahveh_Hamk);
                            break;
                        case 7:
                            dr["newValue"] = string.Format("{0} {1}", "تمام وقت 44 ساعت", newHokm.Nahveh_Hamk);
                            break;
                    }

                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }


        public ProfessorHokmDTO GetNewHokmInfo(int reqId)
        {
            return reqDAO.GetNewHokmInfo(reqId);
        }
        public string GetLastMartabe(int codeOstad)
        { 
            CooperationRequestFaculty rf = new CooperationRequestFaculty();
           var dt= rf.GetOstadInfoFromHR(codeOstad);
            if (dt.Rows.Count == 1)
                return dt.Rows[0]["martabeh"].ToString();
            else
                return "بدون مرتبه";

        }

        public bool ApproveNewHokm(ProfessorHokmDTO oHokm)
        {
            int Id = reqDAO.ApproveNewHokm(oHokm);

            if (Id > 0)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool updateHokmInThreeTables(ProfessorHokmDTO oHokm)
        {
            try
            {
                return reqDAO.updateHokmInThreeTables(oHokm);
            }
            catch
            {
                return false;
            }
            
        }

        public List<ProfessorEditRequestDTO> GetProfessorRequestsByIdAndStatus(int code_Ostad, string state)
        {
            return reqDAO.GetProfessorRequestsByIdAndStatus(code_Ostad, state);
        }

        public bool HasPendingRequest(int codeostad, int RequestTypeId)
        {
            return reqDAO.HasPendingRequest(codeostad, RequestTypeId);
        }

        public int DeleteProfessorRequest(int reqId)
        {
            int a = reqDAO.DeleteProfessorRequest(reqId);
            reqDAO.DeleteProfessorRequest_Doc(reqId);
            return a;
        }

        public int InsertMessageToRequest(int reqId, string p)
        {
            return reqDAO.InsertMessageToRequest(reqId, p);
        }

        public ProfessorHokmDTO GetLastHokmInfoByInfoPeopleID(int infoPeopleId)
        {
            ProfessorHokmDTO oHokm = new ProfessorHokmDTO();
            DataTable dt = reqDAO.GetLastHokmInfoByInfoPeopleID(infoPeopleId);

            if (dt.Rows.Count > 0)
            {
                oHokm.HokmId = Convert.ToInt32(dt.Rows[0]["HokmId"]);
                oHokm.InfoPeopleId = Convert.ToInt32(dt.Rows[0]["InfoPeopleId"]);
                oHokm.Code_Ostad = Convert.ToInt32(dt.Rows[0]["code_ostad"]);
                oHokm.HokmUrl = dt.Rows[0]["hokmurl"].ToString();
                oHokm.MablaghHokm = Convert.ToInt64(dt.Rows[0]["MablaghHokm"]);
                oHokm.Number_Hokm = dt.Rows[0]["number_hokm"].ToString();
                oHokm.Date_RunHokm = dt.Rows[0]["date_runhokm"].ToString();
                oHokm.Date_Hokm = dt.Rows[0]["date_hokm"].ToString();
                oHokm.Payeh = Convert.ToInt32(dt.Rows[0]["payeh"]);
                oHokm.Type_Estekhdam = Convert.ToInt32(dt.Rows[0]["type_estekhdam"]);
                oHokm.Uni_Khedmat = Convert.ToInt32(dt.Rows[0]["uni_khedmat"]);
                oHokm.Uni_KhedmatType = dt.Rows[0]["uni_khedmatType"] == System.DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["uni_khedmatType"]);
                oHokm.Nahveh_Hamk = Convert.ToInt32(dt.Rows[0]["nahveh_hamk"]);
                if (dt.Rows[0]["isRetired"] != DBNull.Value)
                    oHokm.IsRetired = Convert.ToBoolean(dt.Rows[0]["isRetired"]);
                oHokm.DateUpload = dt.Rows[0]["dateupload"].ToString();
                oHokm.State = Convert.ToInt32(dt.Rows[0]["state"]);
                if (dt.Rows[0]["BoundHour"] != DBNull.Value)
                    oHokm.BoundHour = Convert.ToBoolean(dt.Rows[0]["boundHour"]);
                if (dt.Rows[0]["DateRunHokmHere"] != DBNull.Value)
                {
                    oHokm.DateRunHokmHere = dt.Rows[0]["DateRunHokmHere"].ToString();
                }
                oHokm.Martabeh = Convert.ToInt32(dt.Rows[0]["Martabeh"]);
                oHokm.EditRequestId = Convert.ToInt32(dt.Rows[0]["EditRequestId"]);
                if (dt.Rows[0]["BoundHour"] != DBNull.Value)
                    oHokm.BoundHour = Convert.ToBoolean(dt.Rows[0]["BoundHour"]);
            }
            return oHokm;
        }

        public DataTable GetLastHokmInfoByInfoPeopleID_Datatable(int infoPeopleId)
        {
            DataTable dt= reqDAO.GetLastHokmInfoByInfoPeopleID(infoPeopleId);
            return dt;
            
        }

        public DataTable GetProfessorFromResearchByCode(int codeOstad)
        {
            return reqDAO.GetProfessorFromResearchByCode(codeOstad);
        }

        public DataTable GetDocByInfoIdAndType(int infoId, int typeId)
        {
            return reqDAO.GetDocByInfoIdAndType(infoId, typeId);
        }

        /// <summary>
        /// اطلاعات کاربر سیستم ثبت اطلاعات اساتید
        /// </summary>
        /// <param name="infoPeopleId"></param>
        /// <returns></returns>
        //public DataTable GetUserByInfoPeopleId(int infoPeopleId)
        //{
        //    return reqDAO.GetUserByInfoPeopleId(infoPeopleId);
        //}
        public void updateHokmstatusForIsNotHeiat(int userId,int martabe)
        {
            var rf = new CooperationRequestFaculty();
            rf.UpdateMartabeInfoPeople(userId,martabe);
            
        }
    }
}
