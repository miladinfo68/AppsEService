using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IAUEC_Apps.DAO.University.CooperationRequest;
using IAUEC_Apps.DTO.University.Request;

namespace IAUEC_Apps.Business.university.CooperationRequest
{
    public class CooperationRequestBusiness
    {
        public CooperationRequestBusiness() { }

        public DataTable getEmploymentActionHistory()
        {
            CooperationRequestDAO CR = new CooperationRequestDAO();
            return CR.getEmploymentActionHistory();
        }
        public DataTable getProfessorsByModifyType(int eventID, string fromDate, string toDate)
        {
            CooperationRequestDAO CR = new CooperationRequestDAO();
            return CR.getProfessorsByModifyType(eventID,fromDate,toDate);
        }

        public DataTable getAllHokmToShowToPortal(string family, string startDate, string endDate, string nationalCode, int martabe)
        {
            CooperationRequestDAO CR = new CooperationRequestDAO();
            DataTable dtAllHokm = new DataTable();
            DataTable dtResult = new DataTable();
            dtAllHokm = CR.getAllHokmInfo();
            string whereClause = "cooperation in(2,3) and seenByPortal=0";
            if (family.Trim() != "")
                whereClause += " and (family like '%" + family + "%' or family like '%" + family.Replace("ی", "ي") + "%')";
            if (startDate != "")
                whereClause += " and dateUpload>= '" + startDate + "'";
            if (endDate != "")
                whereClause += " and dateUpload <= '" + endDate + "'";
            if (nationalCode != "")
                whereClause += " and idd_meli = '" + nationalCode + "'";
            if (martabe != -1)
                whereClause += " and martabeh=" + martabe;

            DataRow[] drResult = dtAllHokm.Select(whereClause);

            dtResult.Columns.Add("hokmId");
            dtResult.Columns.Add("row");
            dtResult.Columns.Add("name");
            dtResult.Columns.Add("nationalCode");
            dtResult.Columns.Add("date");
            dtResult.Columns.Add("code_ostad");
            dtResult.Columns.Add("InfoPeopleId");
            int counter = 1;
            foreach (DataRow dr in drResult)
            {
                DataRow drNew = dtResult.NewRow();
                drNew["name"] = string.Format("{0} {1}", dr["name"], dr["family"]);
                drNew["hokmId"] = dr["HokmId"];
                drNew["row"] = counter++;
                drNew["nationalCode"] = dr["idd_meli"];
                drNew["date"] = dr["DateUpload"];
                drNew["code_ostad"] = dr["code_ostad"];
                drNew["InfoPeopleId"] = dr["InfoPeopleId"];
                dtResult.Rows.Add(drNew);
            }
            return dtResult;
        }

        public DataTable getTeachersHaveNotPersonalImage()
        {
            CooperationRequestDAO CR = new CooperationRequestDAO();

            return CR.getTeachersHaveNotPersonalImage();
        }
        public DataTable getTeachersHaveNotPersonalImage_CantUpload()
        {
            CooperationRequestDAO CR = new CooperationRequestDAO();

            return CR.getTeachersHaveNotPersonalImage_CantUpload();
        }

        public ProfessorHokmDTO getHokmInfoByHokmId(int hokmId)
        {
            CooperationRequestDAO CR = new CooperationRequestDAO();
            DataTable dtAllHokm = new DataTable();
            dtAllHokm = CR.getAllHokmInfo();
            ProfessorHokmDTO oHokm = new ProfessorHokmDTO();
            if (dtAllHokm.Rows.Count > 0)
            {
                DataRow[] dr = dtAllHokm.Select("hokmId=" + hokmId);
                if (dr.Length == 1)
                {
                    oHokm.HokmId = Convert.ToInt32(dr[0]["HokmId"]);
                    oHokm.InfoPeopleId = Convert.ToInt32(dr[0]["InfoPeopleId"]);
                    oHokm.Code_Ostad = Convert.ToInt32(dr[0]["code_ostad"]);
                    oHokm.HokmUrl = dr[0]["hokmurl"].ToString();
                    oHokm.MablaghHokm = Convert.ToInt64(dr[0]["MablaghHokm"]);
                    oHokm.Number_Hokm = dr[0]["number_hokm"].ToString();
                    oHokm.Date_RunHokm = dr[0]["date_runhokm"].ToString();
                    oHokm.Date_Hokm = dr[0]["date_hokm"].ToString();
                    oHokm.Payeh = Convert.ToInt32(dr[0]["payeh"]);
                    oHokm.Type_Estekhdam = Convert.ToInt32(dr[0]["type_estekhdam"]);
                    oHokm.Uni_Khedmat = Convert.ToInt32(dr[0]["uni_khedmat"]);
                    oHokm.Uni_KhedmatType = Convert.ToInt32(dr[0]["uni_khedmatType"]);
                    oHokm.Nahveh_Hamk = Convert.ToInt32(dr[0]["nahveh_hamk"]);
                    if (dr[0]["isRetired"] != DBNull.Value)
                        oHokm.IsRetired = Convert.ToBoolean(dr[0]["isRetired"]);
                    oHokm.DateUpload = dr[0]["dateupload"].ToString();
                    oHokm.State = Convert.ToInt32(dr[0]["state"]);
                    if (dr[0]["DateRunHokmHere"] != DBNull.Value)
                    {
                        oHokm.DateRunHokmHere = dr[0]["DateRunHokmHere"].ToString();
                    }
                    oHokm.Martabeh = Convert.ToInt32(dr[0]["Martabeh"]);
                    oHokm.EditRequestId = Convert.ToInt32(dr[0]["EditRequestId"]);
                    oHokm.BoundHour = Convert.ToBoolean(dr[0]["BoundHour"]);
                }
            }
            return oHokm;
        }

        public void updateHokmSeenStatus(int requestId, bool seenStatus)
        {
            CooperationRequestDAO CR = new CooperationRequestDAO();
            CR.updateHokmSeenStatus(requestId, Convert.ToInt16(seenStatus));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hrID">استاد: کد اچ آر  -   کاربر: هر عددی</param>
        /// <param name="userLevel">استاد:1  -  سرپرست دانشگاه:77  - مسئول کارگزینی:11 -  کارشناس کارگزینی:12</param>
        /// <returns></returns>
        public DataTable getSignature(int hrID, int userRole)
        {
            CooperationRequestDAO dao = new DAO.University.CooperationRequest.CooperationRequestDAO();
            DataTable dt = new DataTable();
            switch (userRole)
            {
                case 1:
                    dt = dao.getTeacherSignature(hrID);
                    //get Teacher signature
                    break;
                case (int)DTO.RoleEnums.سرپرست_واحد://سرپرست دانشگاه
                case (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی://پاراف سمت راست
                case (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی://پاراف سمت چپ
                case (int)DTO.RoleEnums.مسئول_حق_التدریس://پاراف سمت چپ
                    dt = dao.getUserSignature(userRole);
                    //get user signature
                    break;

            }
            return dt;
        }

        public DataTable getTerm_Contract(string term = "this")
        {
            CooperationRequestDAO dao = new DAO.University.CooperationRequest.CooperationRequestDAO();
            DataTable dt = new DataTable();
            dt.Columns.Add("term");
            dt.Columns.Add("nimSal");
            dt.Columns.Add("Sal");
            DataRow dr = dt.NewRow();
            dr[0] = dao.getTerm_Contract(term);

            if (dr[0].ToString().Contains("-"))
            {
                string[] termArr = dr[0].ToString().Split('-');
                if (termArr.Length == 3)
                {
                    dr[2] = string.Format("{0}-{1}", termArr[0], termArr[1]);
                    dr[1] = termArr[2] == "1" ? "اول" : (termArr[2] == "2" ? "دوم" : (termArr[2] == "3" ? "تابستان" : ""));
                }

            }
            dt.Rows.Add(dr);
            return dt;
        }

        public DataTable getAgreementOfTeacher(Int64 codeOstad)
        {
            CooperationRequestDAO dao = new DAO.University.CooperationRequest.CooperationRequestDAO();
            return dao.getTeacherAgreement(codeOstad);
        }

        public DataTable getContractOfTeacher(int codeOstad_Hr, string term = "this")
        {
            CooperationRequestDAO dao = new DAO.University.CooperationRequest.CooperationRequestDAO();
            return dao.getTeacherContract(codeOstad_Hr, term);
        }

        public DataTable getContractByStatus(int status, string term = "this")
        {
            CooperationRequestDAO dao = new DAO.University.CooperationRequest.CooperationRequestDAO();
            return dao.getContractByStatus(term, status);
        }

        public DataTable getAgreementByStatus(int status)
        {
            CooperationRequestDAO dao = new DAO.University.CooperationRequest.CooperationRequestDAO();
            return dao.getAgreementByStatus(status);
        }

        public DataTable getTermsToContractWithTeachers(int code_Ostad)
        {
            DataTable dt = new DataTable();
            CooperationRequestDAO dao = new CooperationRequestDAO();
            dt = dao.getTermsToContractWithTeachers(code_Ostad);
            return dt;
        }

        public DataTable getYearToSigncontract_HOD(int codeOstad = 0)
        {
            CooperationRequestDAO dao = new CooperationRequestDAO();
            DataTable dt;
            if (codeOstad == 0)
            {
                dt = dao.getYearsOfHodContract();
                if (dt.Rows.Count == 0)
                {
                    string thisYear = "1399";

                    DataRow drThisYear = dt.NewRow();
                    drThisYear["year"] = thisYear;
                    dt.Rows.Add(drThisYear);
                }
            }
            else
            {
                dt = dao.getYearToSigncontract_HOD(codeOstad);
            }
            //string thisYear = DateTime.Now.ToPeString().Substring(0, 4);
            //if (dt.Rows.Count > 0)
            //{
            //    DataRow[] dr = dt.Select("year=" + thisYear);
            //    if (dr.Length == 1)
            //        return dt;
            //}
            //DataRow drThisYear=dt.NewRow();
            //drThisYear["year"] = thisYear;
            //dt.Rows.Add(drThisYear);
            return dt;
        }
        public DataTable getbeginAndEndWorkTimeHOD(Int64 codeOstad, int year)
        {
            CooperationRequestDAO dao = new CooperationRequestDAO();
            return dao.getbeginAndEndWorkTimeHOD(codeOstad, year);
        }



        public DataTable getSignature_Status()
        {
            DataTable dt = new DataTable();
            CooperationRequestDAO dao = new DAO.University.CooperationRequest.CooperationRequestDAO();
            dt = dao.getSignaturesStatus();
            return dt;
        }

        public DataTable getAllContracts_Status(string term = "this")
        {

            DataTable dt = new DataTable();
            CooperationRequestDAO dao = new DAO.University.CooperationRequest.CooperationRequestDAO();
            if (term.Length > 4 || term == "this")
                dt = dao.getAllContractStatus(term);
            else
                dt = dao.getAllContractStatus_HOD(term);
            return dt;
        }

        public DataTable getAllAgreement_Status()
        {
            DataTable dt = new DataTable();
            CooperationRequestDAO dao = new DAO.University.CooperationRequest.CooperationRequestDAO();
            dt = dao.getAllAgreementsStatus();
            return dt;
        }

        public bool insertTeacherContract(int codeOstad, string contractFile, int hrID, string term, out int contractId)
        {
            CooperationRequestDAO dao = new DAO.University.CooperationRequest.CooperationRequestDAO();
            DataTable dtGetContract = getContractByStatus(4, term);
            DataRow[] dr = dtGetContract.Select("hrID=" + hrID);
            if (dr.Length == 0)
                return dao.insertTeacherContract(codeOstad, contractFile, hrID, term, out contractId);
            else
                return UpdateTeacherContractAfterReject(hrID, contractFile, term, out contractId);

        }
        public bool insertTeacherAgreement(int codeOstad, string agreementFile, int hrID, out int agreementID)
        {
            CooperationRequestDAO dao = new DAO.University.CooperationRequest.CooperationRequestDAO();
            DataTable dtGetAgreement = getAgreementByStatus(3);
            DataRow[] dr = dtGetAgreement.Select("hrID=" + hrID);
            if (dr.Length == 0)
                return dao.insertTeacherAgreement(codeOstad, agreementFile, hrID, out agreementID);
            else
                return UpdateTeacherAgreementAfterReject(hrID, agreementFile, out agreementID);

        }

        private bool UpdateTeacherContractAfterReject(int hrID, string contractFile, string Term, out int contractId)
        {
            CooperationRequestDAO dao = new DAO.University.CooperationRequest.CooperationRequestDAO();
            return dao.UpdateTeacherContractAfterReject(Term, contractFile, hrID, out contractId);
        }
        private bool UpdateTeacherAgreementAfterReject(int hrID, string contractFile, out int contractId)
        {
            CooperationRequestDAO dao = new DAO.University.CooperationRequest.CooperationRequestDAO();
            return dao.UpdateTeacherAgreementAfterReject(contractFile, hrID, out contractId);
        }

        public bool updateTeacherContractStatus(int codeOstad, int userCode, string contractFile, string term = "this")
        {
            CooperationRequestDAO dao = new DAO.University.CooperationRequest.CooperationRequestDAO();
            return dao.updateTeacherContractStatus(term, codeOstad, getNewContractStatus(userCode), contractFile);
        }

        public bool updateTeacherAgreementStatus(int codeOstad, int userCode, string contractFile, out int AgreementID)
        {
            CooperationRequestDAO dao = new DAO.University.CooperationRequest.CooperationRequestDAO();
            return dao.updateTeacherAgreementStatus(codeOstad, getNewAgreementStatus(userCode), contractFile, out AgreementID);
        }

        public bool rejectTeacherContract(int codeOstad, string descriptionOfReject, string term = "this")
        {
            CooperationRequestDAO dao = new DAO.University.CooperationRequest.CooperationRequestDAO();
            DataTable dtContract = getContractOfTeacher(codeOstad, term);
            if (dtContract.Rows.Count == 1)
                if (dtContract.Rows[0]["contractFile"] != DBNull.Value)
                    return dao.updateTeacherContractStatus(term, codeOstad, 4, dtContract.Rows[0]["contractFile"].ToString(), descriptionOfReject);
            return false;
        }

        public bool rejectTeacherAgreement(Int64 codeOstad, string descriptionOfReject, out int agreementID)
        {
            agreementID = 0;
            CooperationRequestDAO dao = new DAO.University.CooperationRequest.CooperationRequestDAO();
            DataTable dtAgreement = getAgreementOfTeacher(codeOstad);
            if (dtAgreement.Rows.Count == 1)
                if (dtAgreement.Rows[0]["agreementFile"] != DBNull.Value)
                    return dao.updateTeacherAgreementStatus(codeOstad, 3, dtAgreement.Rows[0]["AgreementFile"].ToString(), out agreementID, descriptionOfReject);
            return false;
        }

        public bool ChangeContractStatusToLastStep(int codeOstad, int userRole, string term = "this")
        {
            CooperationRequestDAO dao = new DAO.University.CooperationRequest.CooperationRequestDAO();
            DataTable dtContract = getContractOfTeacher(codeOstad, term);
            if (dtContract.Rows.Count == 1)
                if (dtContract.Rows[0]["contractFile"] != DBNull.Value)
                    switch (userRole)
                    {
                        case (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی:
                            return dao.updateTeacherContractStatus(term, codeOstad, 0, dtContract.Rows[0]["contractFile"].ToString());
                    }
            return true;
        }

        private int getNewContractStatus(int userCode)
        {
            //insert:0 karshenas-12: 1 masul-11: 2 sarparast-77: 3
            switch (userCode)
            {
                case (int)DTO.RoleEnums.مسئول_حق_التدریس:
                case (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی:
                    return 1;
                case (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی:
                    return 2;
                case (int)DTO.RoleEnums.سرپرست_واحد:
                    return 3;
                default:
                    return 0;
            }
        }

        private int getNewAgreementStatus(int userCode)
        {
            //insert:0 karshenas-12: 1 masul-11: 2 sarparast-77: 3
            switch (userCode)
            {
                case (int)DTO.RoleEnums.مدیر_کل_امور_پژوهشی:
                    return 1;
                case (int)DTO.RoleEnums.سرپرست_واحد:
                    return 2;
                default:
                    return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="codeUser">کد کاربر یا کد استاد در سیدا</param>
        /// <param name="userRole">برای استاد 1 و برای کاربر کد نقش</param>
        /// <param name="signaturePath"></param>
        /// <param name="hrID">برای کاربر برابر 0</param>
        /// <returns></returns>
        public bool UpdateOrInsertSignature(int codeUser, int userRole, string signaturePath, int hrID)
        {

            try
            {
                CooperationRequestDAO dao = new DAO.University.CooperationRequest.CooperationRequestDAO();
                bool signatureExist = false;

                DataTable dtSignature = getSignature(hrID, userRole);
                if (dtSignature.Rows.Count != 1)
                    return false;
                signatureExist = dtSignature.Rows[0]["signature"] != DBNull.Value;

                switch (signatureExist)
                {
                    case true://Update
                        #region 
                        switch (userRole)
                        {
                            case 1:
                                return dao.updateTeacherSignature(hrID, signaturePath);
                            case (int)DTO.RoleEnums.سرپرست_واحد://سرپرست دانشگاه
                            case (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی://پاراف سمت راست
                            case (int)DTO.RoleEnums.مسئول_حق_التدریس://پاراف سمت چپ
                            case (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی://پاراف سمت چپ
                                return dao.updateUserSignature(codeUser, signaturePath, userRole);
                        }
                        #endregion
                        break;
                    case false://Insert
                        #region 
                        switch (userRole)
                        {
                            case 1:
                                return dao.InsertTeacherSignature(codeUser, signaturePath, hrID);
                            case (int)DTO.RoleEnums.سرپرست_واحد://سرپرست دانشگاه
                            case (int)DTO.RoleEnums.مدیر_امور_کارگزینی_هیئت_علمی://پاراف سمت راست
                            case (int)DTO.RoleEnums.مسئول_حق_التدریس://پاراف سمت چپ
                            case (int)DTO.RoleEnums.مسئول_کارگزینی_هیات_علمی://پاراف سمت چپ
                                return dao.InsertUserSignature(codeUser, signaturePath, userRole);
                        }
                        #endregion
                        break;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }
        public bool deleteSignature(Int64 codeUser, int userRole, int hrID)
        {

            try
            {
                CooperationRequestDAO dao = new DAO.University.CooperationRequest.CooperationRequestDAO();
                bool signatureExist = false;

                DataTable dtSignature = getSignature(hrID, userRole);
                if (dtSignature.Rows.Count != 1)
                    return false;
                signatureExist = dtSignature.Rows[0]["signature"] != DBNull.Value;

                if (signatureExist)
                {
                    return dao.deleteSignature(codeUser, userRole);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        public int updateBlacklistTeacher(string idd_meli, bool inblacklist = true)
        {
            CooperationRequestDAO dao = new DAO.University.CooperationRequest.CooperationRequestDAO();
            return dao.updateBlacklistTeacher(idd_meli, inblacklist);
        }

        public DataTable getBlacklistTeachers()
        {
            CooperationRequestDAO dao = new DAO.University.CooperationRequest.CooperationRequestDAO();

            return dao.getBlacklistTeachers();
        }



    }
}
