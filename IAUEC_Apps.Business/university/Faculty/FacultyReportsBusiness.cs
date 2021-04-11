using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAUEC_Apps.DAO.University.Faculty;
using System.Data;
using IAUEC_Apps.DTO.University.Faculty;
using IAUEC_Apps.DAO.University.CooperationRequest;

namespace IAUEC_Apps.Business.university.Faculty
{
    public class FacultyReportsBusiness
    {
        FacultyReportDAO FRD = new FacultyReportDAO();
        CooperationRequestFaculty CRF = new CooperationRequestFaculty();
        InsertToSida ITS = new InsertToSida();
        CooperationRequestDAO CRD = new CooperationRequestDAO();

        public DataTable getBooklet(string term, bool hasBooklet, string idDanesh, string idResh)
        {
            return FRD.getBooklet(term, hasBooklet, Convert.ToInt32(idDanesh), Convert.ToInt32(idResh));
        }
        public DataTable getBookletData(int bookletID)
        {
            return FRD.getBookletData(bookletID);
        }
        public DataTable GetAllGroup(int iddanesh)
        {
            return FRD.GetAllGroup(iddanesh);
        }

        public DataTable GetDepartmentList(int daneshID)
        {
            return FRD.GetDepartmentList(daneshID);
        }

        public void DeleteProfDepartmentByDaneshID(int idProf, int daneshid)
        {
            FRD.DeleteProfDepartmentByDaneshID(idProf, daneshid);
        }
        public List<string> GetGroupList(int InfoPeopleId)
        {
            List<string> list = new List<string>();
            DataTable dt = FRD.GetOstadGroupByIdInfoPeople(InfoPeopleId);
            foreach (DataRow vaRow in dt.Rows)
            {
                list.Add(vaRow[0].ToString());
            }
            return list;
        }

        public DataTable GetNotificationProf(string CodeOstad, string Term, int Daneshkade, int Group, int Cooperation, int order)
        {
            return FRD.GetNotificationProf(CodeOstad, Term, Daneshkade, Group, Cooperation, order);
        }
        public DataTable GetTeachingExperienceProf(string CodeOstad, int Daneshkade, int Group, int Cooperation, string AzTerm, string TaTerm)
        {
            return FRD.GetTeachingExperienceProf(CodeOstad, Daneshkade, Group, Cooperation, AzTerm, TaTerm);
        }
        public DataTable GetEvalutionAllProf(string Term, int CodeOstad, int Departman, int Lesson, int Order)
        {
            return FRD.GetEvalutionAllProf(Term, CodeOstad, Departman, Lesson, Order);
        }
        public DataTable GetEvalutionProfDividedODDQ(string Term, int CodeOstad, int Departman, int Lesson, int Order)
        {
            return FRD.GetEvalutionProfDividedODDQ(Term, CodeOstad, Departman, Lesson, Order);
        }
        public DataTable GetEvalutionProfDividedODD(string Term, int CodeOstad, int Departman, int Lesson, int Order)
        {
            return FRD.GetEvalutionProfDividedODD(Term, CodeOstad, Departman, Lesson, Order);
        }
        public DataTable GetEvalutionProfDividedODR(string Term, int CodeOstad, int Departman, int Lesson, int Order)
        {
            return FRD.GetEvalutionProfDividedODR(Term, CodeOstad, Departman, Lesson, Order);
        }
        public DataTable GetEvalutionProfDividedDid(string Term, int CodeOstad, int Departman, int Lesson, int Order)
        {
            return FRD.GetEvalutionProfDividedDid(Term, CodeOstad, Departman, Lesson, Order);
        }
        public DataTable GetCooperation()
        {
            return FRD.GetCooperation();
        }
        public DataTable GetListProfinCurrentTerm(string Term, int Departman, int Cooperation)
        {
            return FRD.GetListProfinCurrentTerm(Term, Departman, Cooperation);
        }
        public DataTable GetAccessCardsProf(string Term, int Departman, int Cooperation)
        {
            return FRD.GetAccessCardsProf(Term, Departman, Cooperation);
        }
        public DataTable GetNumberClass()
        {
            return FRD.GetNumberClass();
        }
        public DataTable GetConflictClassByCodeOstad(string Term, int CodeOstad, int Sort, int Day, int NumberClass)
        {
            return FRD.GetConflictClassByCodeOstad(Term, CodeOstad, Sort, Day, NumberClass);
        }
        //public DataTable GetConflictClassByNumberClass(string Term, string NumberClass, int Sort , int Lesson)
        //{
        //    return FRD.GetConflictClassByNumberClass(Term, NumberClass, Sort, Lesson);
        //}
        public DataTable GetConflictClassByNumberClass(string Term, string NumberClass, int Sort, int Day)
        {
            return FRD.GetConflictClassByNumberClass(Term, NumberClass, Sort, Day);
        }
        public DataTable ReferToProf(string Term, string CodeOstad, string FromDate, string ToDate)
        {
            return FRD.ReferToProf(Term, CodeOstad, FromDate, ToDate);
        }
        public DataTable GetAbsenceButNoCompensationProf(string Term, int Departman, int Daneshkade, int CodeOstad, string FromDate, string ToDate, int CountAbsence)
        {
            return FRD.GetAbsenceButNoCompensationProf(Term, Departman, Daneshkade, CodeOstad, FromDate, ToDate, CountAbsence);
        }
        public DataTable GetAbsenceAndCompensationProf(string Term, int Departman, int Daneshkade, int CodeOstad, string FromDate, string ToDate, int CountAbsence, string AzJobrani, string TaJobrani)
        {
            return FRD.GetAbsenceAndCompensationProf(Term, Departman, Daneshkade, CodeOstad, FromDate, ToDate, CountAbsence, AzJobrani, TaJobrani);
        }
        public DataTable GetListTuition(string Term, int Cooperation, int Departman, int Daneshkade, int CodeOstad)
        {
            return FRD.GetListTuition(Term, Cooperation, Departman, Daneshkade, CodeOstad);
        }
        public DataTable TeachingExperienceMoreThanADay(string Term, int CodeOsatd, int Daneshkade, int Departman, int Cooperation, int Number)
        {
            return FRD.TeachingExperienceMoreThanADay(Term, CodeOsatd, Daneshkade, Departman, Cooperation, Number);
        }
        public DataTable ShowClockDateExam(string Term)
        {
            return FRD.ShowClockDateExam(Term);
        }
        public DataTable GetNameDepartmanAndGroup(int Departman, int Cooperation)
        {
            return FRD.GetNameDepartmanAndGroup(Departman, Cooperation);
        }
        public DataTable GetStudentsProbation(string Term, int Degree, int Percent)
        {
            return FRD.GetStudentsProbation(Term, Degree, Percent);
        }
        public DataTable GetStudentsProbationAcceptance(string Term, int Degree, int Percent)
        {
            return FRD.GetStudentsProbationAcceptance(Term, Degree, Percent);
        }
        public DataTable GetListOfSelectedCoursesTeachers(string CodeOstad, string Term, int Daneshkade, int Group, int Cooperation)
        {
            return FRD.GetListOfSelectedCoursesTeachers(CodeOstad, Term, Daneshkade, Group, Cooperation);
        }
        public DataTable GetListPayProf(string Term, int Daneshkade, int Field, int Departman, int Cooperation, int CodeOstad, int Zarib)
        {
            return FRD.GetListPayProf(Term, Daneshkade, Field, Departman, Cooperation, CodeOstad, Zarib);
        }
        public DataTable GetInfoPeo(int ID)
        {
            return CRF.GetInfoProf(ID);
        }

        public DataTable SelectInfoPeopleBystatus(int status)
        {
            return CRF.SelectInfoPeopleBystatus(status);
        }
        public void UpdateInfoPeopleStatus(string codemeli, int status)
        {
            CRF.UpdateInfoPeopleStatus(codemeli, status);

        }
        public DataTable GetInfoPeoByCodeMeli(int CodeOstad)
        {
            return CRF.GetInfoPeoByCodeMeli(CodeOstad);
        }


        public void UpdateInfoPeople(int CodeOstad, int Status)
        {
            CRF.UpdateInfoPeople(CodeOstad, Status);
        }
        public DataTable InsertToFostadSida(int Code_Ostad, string codeMeli)
        {
            DataTable dt = CRF.InsertToFostadSida(Code_Ostad, Business.Common.CommonBusiness.EncryptPass(codeMeli));
            if (dt != null && dt.Rows.Count > 0)
            {
                Common.CommonBusiness cb = new Common.CommonBusiness();
                if (!cb.insertProfessorPassword(dt.Rows[0]["code_ostad"].ToString(), codeMeli))
                    return new DataTable();
            }
            return dt;
        }
        public DataTable GetInfoPeoByFilter(int CodeOstad)
        {
            return CRF.GetInfoPeoByFilter(CodeOstad);
        }
        public DataTable GetListTuition2(string Term, int Cooperation, int Departman, int Daneshkade, int CodeOstad)
        {
            return FRD.GetListTuition2(Term, Cooperation, Departman, Daneshkade, CodeOstad);
        }
        public DataTable GetAcceptMark(string Term, int Did, int CodeOstad, int daneshID)
        {
            return FRD.GetAcceptMark(Term, Did, CodeOstad, daneshID);
        }
        public DataTable GetGroupByCode(int Code_Ostad)
        {
            return FRD.GetGroupByCode(Code_Ostad);
        }

        public void SetProfessorGroups(int infopeopleid, List<string> lsgroups)
        {
            FRD.DELETEProfessorGroupByPeopleid(infopeopleid);
            foreach (string item in lsgroups)
            {
                FRD.InsertUpdateProfGroupStatus(infopeopleid, item, 1);
            }
        }


        public DataTable GetDaneshkadeByGroup(string Field)
        {
            return FRD.GetDaneshkadeByGroup(Field);
        }

        public DataTable GetNameProvince()
        {
            return CRF.GetNameProvince();
        }

        public DataTable GetFilePDF(int Code_Ostad)
        {
            return CRF.GetFilePDF(Code_Ostad);
        }
        public void UpdateStatus(int Type, int CodeOstad, int Check, string Description, int newType)
        {
            FRD.UpdateStatus(Type, CodeOstad, Check, Description, newType);
        }
        public DataTable GetDepartmanProf(string idgroup)
        {
            return FRD.GetDepartmanProf(idgroup);
        }
        public DataTable GetInfoPeoByFilterPDF(int CodeOstad, int DocType)
        {
            return CRF.GetInfoPeoByFilterPDF(CodeOstad, DocType);
        }
        public void UpdateInfoPeople(InfoPeople IP)
        {
            FRD.UpdateInfoPeople(IP);
        }
        //public void UpdateUserStatus(string mobile  , int status)
        //{
        //    FRD.UpdateUserStatus(mobile , status);
        //}
        public void UpdateDocStatus(string mobile, int status)
        {
            FRD.UpdateDocStatus(mobile, status);
        }
        public void InsertImageToOstImage(int Code_Ostad, byte[] Image)
        {
            CRF.InsertImageToOstImage(Code_Ostad, Image);
        }
        public DataTable DepartmanProf(int ID)
        {
            return CRF.DepartmanProf(ID);
        }
        public DataTable GetRequestAccept()
        {
            return CRD.GetRequestAccept();
        }
        public DataTable GetRequestReject()
        {
            return CRD.GetRequestReject();
        }
        public DataTable GetRequestEditing()
        {
            return CRD.GetRequestEditing();
        }
        public DataTable GetRequestPending()
        {
            return CRD.GetRequestPending();
        }
        public DataTable GetResearchRequestAccept()
        {
            return CRD.GetResearchRequestAccept();
        }
        public DataTable GetResearchRequestReject()
        {
            return CRD.GetResearchRequestReject();
        }
        public DataTable GetResearchRequestPending()
        {
            return CRD.GetResearchRequestPending();
        }
        public DataTable ListAllProf()
        {
            return FRD.ListAllProf();
        }
        public DataTable InsertToPortalPazhuhesh(int Code, string codeMeli)
        {
            var dt = CRF.InsertToPortalPazhuhesh(Code, Business.Common.CommonBusiness.EncryptPass(codeMeli));
            if (dt != null && dt.Rows.Count > 0)
            {
                Common.CommonBusiness cb = new Common.CommonBusiness();
                if (!cb.insertProfessorPassword(dt.Rows[0]["ocode"].ToString(), Common.CommonBusiness.DecryptPass(dt.Rows[0]["password_ost"].ToString())))
                    return dt;// new DataTable();
            }
            return dt;
        }
        public DataTable GetRequestScanDocID(int ID)
        {
            return CRD.GetRequestScanDoc(ID);
        }

        public DataTable GetInfoPeoByCodeMeli(string codemeli)
        {
            return CRD.GetInfoPeoByCodeMeli(codemeli);
        }

        public DataTable GetInfoPeoByCodeMeliAndFamily(string codemeli, string family)
        {
            if (codemeli == string.Empty)
            {
                codemeli = null;
            }
            if (family == string.Empty)
            {
                family = null;
            }
            return CRD.GetInfoPeoByCodeMeliAndFamily(codemeli, family);
        }

        public void UpdateInfoPeopleStatus(string UserName, string status)
        {
            CRD.UpdateInfoPeopleStatus(UserName, status);
        }
        public DataTable GetNameMadarek()
        {
            return CRD.GetNameMadarek();
        }
        public DataTable GetNameScanDoc(int ID)
        {
            return CRD.GetNameScanDoc(ID);
        }

        public DataTable GetOstadInfoFromHR(int codeostad)
        {
            return CRF.GetOstadInfoFromHR(codeostad);
        }
        public bool HasNotationId(int codeostad)
        {
            return Convert.ToBoolean(CRF.HasNotationId(codeostad));
        }

        public editInfoStruct getOstadInf(int codeOstad)
        {
            editInfoStruct editInfo;
            DataTable dtOstad = new DataTable();
            DAO.University.Research.ResearchDAO dao = new DAO.University.Research.ResearchDAO();

            editInfo = getOstadInfoFromSida(codeOstad);
            if (editInfo.codeOstad == 0)
                editInfo = getOstadInfoFromPortal(codeOstad);
            return editInfo;
        }

        public DataTable getAllMartabe()
        {
            martabe mtb = new martabe();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("martabe");
            var a = Enum.GetValues(typeof(martabe));

            return dt;
        }
        public string getMartabeText(int idMartabe)
        {
            martabe mtb = new martabe();
            return ((martabe)idMartabe).ToString();
        }


        public editInfoStruct getOstadInfoFromSida(int codeOstad)
        {
            editInfoStruct editInfo = new editInfoStruct();
            DAO.University.Research.ResearchDAO dao = new DAO.University.Research.ResearchDAO();
            DataTable dtOstad = dao.GetOstadInfoByCodeOstad(codeOstad);
            
            if (dtOstad.Rows.Count >= 1)
            {
                editInfo.codeOstad = codeOstad;
                if (dtOstad.Rows[0]["name"] != DBNull.Value)
                    editInfo.name = dtOstad.Rows[0]["name"].ToString();
                if (dtOstad.Rows[0]["family"] != DBNull.Value)
                    editInfo.family = dtOstad.Rows[0]["family"].ToString();
                if (dtOstad.Rows[0]["namep"] != DBNull.Value)
                    editInfo.fatherName = dtOstad.Rows[0]["namep"].ToString();
                if (dtOstad.Rows[0]["add_hom"] != DBNull.Value)
                    editInfo.addressHome = dtOstad.Rows[0]["add_hom"].ToString();
                if (dtOstad.Rows[0]["add_kar"] != DBNull.Value)
                    editInfo.addressKar = dtOstad.Rows[0]["add_kar"].ToString();
                if (dtOstad.Rows[0]["add_email"] != DBNull.Value)
                    editInfo.email = dtOstad.Rows[0]["add_email"].ToString();

                if (dtOstad.Rows[0]["siba"] != DBNull.Value)
                    editInfo.siba = dtOstad.Rows[0]["siba"].ToString();
                if (dtOstad.Rows[0]["num_bime"] != DBNull.Value)
                    editInfo.bimeNum = dtOstad.Rows[0]["num_bime"].ToString();
                if (dtOstad.Rows[0]["tel_hom"] != DBNull.Value)
                    editInfo.telHome = dtOstad.Rows[0]["tel_hom"].ToString();
                if (dtOstad.Rows[0]["tel_kar"] != DBNull.Value)
                    editInfo.telKar = dtOstad.Rows[0]["tel_kar"].ToString();
                if (dtOstad.Rows[0]["mobile"] != DBNull.Value)
                    editInfo.telMobile = dtOstad.Rows[0]["mobile"].ToString();
                if (dtOstad.Rows[0]["code_posti"] != DBNull.Value)
                    editInfo.codePosti = dtOstad.Rows[0]["code_posti"].ToString();

                if (dtOstad.Rows[0]["hrID"] != DBNull.Value)
                    editInfo.hrId = Convert.ToInt32(dtOstad.Rows[0]["hrID"]);
                if (dtOstad.Rows[0]["idd"] != DBNull.Value)
                    editInfo.idd = dtOstad.Rows[0]["idd"].ToString();
                if (dtOstad.Rows[0]["idd_meli"] != DBNull.Value)
                    editInfo.idd_Melli = dtOstad.Rows[0]["idd_meli"].ToString();
                if (dtOstad.Rows[0]["sal_tav"] != DBNull.Value)
                    editInfo.salTavalod = dtOstad.Rows[0]["sal_tav"].ToString();
                if (dtOstad.Rows[0]["nezam"] != DBNull.Value)
                    editInfo.nezam = Convert.ToInt32(dtOstad.Rows[0]["nezam"]);
                if (dtOstad.Rows[0]["idmadrak"] != DBNull.Value)
                    editInfo.maghta = Convert.ToInt32(dtOstad.Rows[0]["idmadrak"]);
                if (dtOstad.Rows[0]["os_idresh"] != DBNull.Value)
                    editInfo.reshte = Convert.ToInt32(dtOstad.Rows[0]["os_idresh"]);
                if (dtOstad.Rows[0]["sal_madrak"] != DBNull.Value)
                    editInfo.salMadrak = dtOstad.Rows[0]["sal_madrak"].ToString();
                if (dtOstad.Rows[0]["sanavat_tadris"] != DBNull.Value)
                    editInfo.sanavat = dtOstad.Rows[0]["sanavat_tadris"].ToString();
                if (dtOstad.Rows[0]["keshvar"] != DBNull.Value)
                    editInfo.keshvar = Convert.ToInt32(dtOstad.Rows[0]["keshvar"]);
                if (dtOstad.Rows[0]["type_university"] != DBNull.Value)
                    editInfo.typeUniMadrak = Convert.ToInt32(dtOstad.Rows[0]["type_university"]);
                if (dtOstad.Rows[0]["university"] != DBNull.Value)
                    editInfo.nameUniMadrak = Convert.ToInt32(dtOstad.Rows[0]["university"]);
                if (dtOstad.Rows[0]["BimehTypeId"] != DBNull.Value)
                    editInfo.bimeType = Convert.ToInt32(dtOstad.Rows[0]["BimehTypeId"]);
                if (dtOstad.Rows[0]["code_ostan_home"] != DBNull.Value)
                    editInfo.ostanHome = Convert.ToInt32(dtOstad.Rows[0]["code_ostan_home"]);
                if (dtOstad.Rows[0]["code_city_home"] != DBNull.Value)
                    editInfo.shahrHome = Convert.ToInt32(dtOstad.Rows[0]["code_city_home"]);
                if (dtOstad.Rows[0]["code_ostan_work"] != DBNull.Value)
                    editInfo.ostanKar = Convert.ToInt32(dtOstad.Rows[0]["code_ostan_work"]);
                if (dtOstad.Rows[0]["code_city_work"] != DBNull.Value)
                    editInfo.shahrKar = Convert.ToInt32(dtOstad.Rows[0]["code_city_work"]);
                if (dtOstad.Rows[0]["note"] != DBNull.Value)
                    editInfo.description = dtOstad.Rows[0]["note"].ToString();


                if (dtOstad.Rows[0]["BimehTypeId"] != DBNull.Value)
                    editInfo.bime = Convert.ToInt16(dtOstad.Rows[0]["BimehTypeId"]) > 0;
                if (dtOstad.Rows[0]["IsRetired"] != DBNull.Value)
                    editInfo.bazneshaste = Convert.ToBoolean(dtOstad.Rows[0]["IsRetired"]);
                if (dtOstad.Rows[0]["marital_status"] != DBNull.Value)
                    editInfo.taahol = dtOstad.Rows[0]["marital_status"].ToString() == "1" ? false : true;

                if (dtOstad.Rows[0]["sex"] != DBNull.Value)
                    editInfo.sexIsMan = Convert.ToInt32(dtOstad.Rows[0]["sex"]) == 1 ? true : false;
            }
            return editInfo;
        }

        public editInfoStruct getOstadInfoFromPortal(Int64 codeOstad)
        {

            editInfoStruct editInfo = new editInfoStruct();
            DAO.University.Research.ResearchDAO dao = new DAO.University.Research.ResearchDAO();
            DataTable dtOstad;
            int _codeOstad = int.Parse("200" + codeOstad);
            dtOstad = dao.GetOstadInfoByCodeOstad_Portal(_codeOstad);
            if (dtOstad.Rows.Count > 0)
            {
                editInfo.codeOstad = codeOstad;
                if (dtOstad.Rows[0]["name_os"] != DBNull.Value)
                    editInfo.name = dtOstad.Rows[0]["name_os"].ToString();

                if (dtOstad.Rows[0]["hrId"] != DBNull.Value)
                    editInfo.hrId = Convert.ToInt32(dtOstad.Rows[0]["hrId"]);

                if (dtOstad.Rows[0]["famili_os"] != DBNull.Value)
                    editInfo.family = dtOstad.Rows[0]["famili_os"].ToString();

                if (dtOstad.Rows[0]["faName"] != DBNull.Value)
                    editInfo.fatherName = dtOstad.Rows[0]["faName"].ToString();

                if (dtOstad.Rows[0]["addres_os"] != DBNull.Value)
                    editInfo.addressHome = dtOstad.Rows[0]["addres_os"].ToString();

                if (dtOstad.Rows[0]["add_kar"] != DBNull.Value)
                    editInfo.addressKar = dtOstad.Rows[0]["add_kar"].ToString();

                if (dtOstad.Rows[0]["mail"] != DBNull.Value)
                    editInfo.email = dtOstad.Rows[0]["mail"].ToString();

                if (dtOstad.Rows[0]["num_hesab"] != DBNull.Value)
                    editInfo.siba = dtOstad.Rows[0]["num_hesab"].ToString();

                if (dtOstad.Rows[0]["num_bime"] != DBNull.Value)
                    editInfo.bimeNum = dtOstad.Rows[0]["num_bime"].ToString();

                if (dtOstad.Rows[0]["tel_home"] != DBNull.Value)
                    editInfo.telHome = dtOstad.Rows[0]["tel_home"].ToString();

                if (dtOstad.Rows[0]["tel_kar"] != DBNull.Value)
                    editInfo.telKar = dtOstad.Rows[0]["tel_kar"].ToString();

                if (dtOstad.Rows[0]["mob_os"] != DBNull.Value)
                    editInfo.telMobile = dtOstad.Rows[0]["mob_os"].ToString();

                if (dtOstad.Rows[0]["code_posti"] != DBNull.Value)
                    editInfo.codePosti = dtOstad.Rows[0]["code_posti"].ToString();

                if (dtOstad.Rows[0]["sh_shn"] != DBNull.Value)
                    editInfo.idd = dtOstad.Rows[0]["sh_shn"].ToString();

                if (dtOstad.Rows[0]["code_meli"] != DBNull.Value)
                    editInfo.idd_Melli = dtOstad.Rows[0]["code_meli"].ToString();

                if (dtOstad.Rows[0]["tarikh_t"] != DBNull.Value)
                {
                    string birthYear = dtOstad.Rows[0]["tarikh_t"].ToString();
                    if (birthYear.Length > 4 && birthYear.Contains("/"))
                        birthYear = birthYear.Split('/')[0];
                    editInfo.salTavalod = birthYear;
                }

                if (dtOstad.Rows[0]["status_nezam"] != DBNull.Value)
                    editInfo.nezam = Convert.ToInt32(dtOstad.Rows[0]["status_nezam"]);

                if (dtOstad.Rows[0]["magta"] != DBNull.Value)
                    editInfo.maghta = Convert.ToInt32(dtOstad.Rows[0]["magta"]);

                //if (dtOstad.Rows[0]["resh"] != DBNull.Value)
                //    editInfo.reshte = Convert.ToInt32(dtOstad.Rows[0]["resh"]);

                if (dtOstad.Rows[0]["sal_madrak"] != DBNull.Value)
                    editInfo.salMadrak = dtOstad.Rows[0]["sal_madrak"].ToString();

                if (dtOstad.Rows[0]["sanavat_tadris"] != DBNull.Value)
                    editInfo.sanavat = dtOstad.Rows[0]["sanavat_tadris"].ToString();

                if (dtOstad.Rows[0]["country"] != DBNull.Value)
                    editInfo.keshvar = Convert.ToInt32(dtOstad.Rows[0]["country"]);

                if (dtOstad.Rows[0]["MadrakUniType"] != DBNull.Value)
                    editInfo.typeUniMadrak = Convert.ToInt32(dtOstad.Rows[0]["MadrakUniType"]);

                if (dtOstad.Rows[0]["university"] != DBNull.Value)
                    editInfo.nameUniMadrak = Convert.ToInt32(dtOstad.Rows[0]["university"]);

                if (dtOstad.Rows[0]["BimehTypeId"] != DBNull.Value)
                    editInfo.bimeType = Convert.ToInt32(dtOstad.Rows[0]["BimehTypeId"]);

                if (dtOstad.Rows[0]["code_ostan_home"] != DBNull.Value)
                    editInfo.ostanHome = Convert.ToInt32(dtOstad.Rows[0]["code_ostan_home"]);

                if (dtOstad.Rows[0]["code_city_home"] != DBNull.Value)
                    editInfo.shahrHome = Convert.ToInt32(dtOstad.Rows[0]["code_city_home"]);

                if (dtOstad.Rows[0]["code_ostan_work"] != DBNull.Value)
                    editInfo.ostanKar = Convert.ToInt32(dtOstad.Rows[0]["code_ostan_work"]);

                if (dtOstad.Rows[0]["code_city_work"] != DBNull.Value)
                    editInfo.shahrKar = Convert.ToInt32(dtOstad.Rows[0]["code_city_work"]);


                if (dtOstad.Rows[0]["BimehTypeId"] != DBNull.Value)
                    editInfo.bime = Convert.ToInt32(dtOstad.Rows[0]["BimehTypeId"]) > 0;

                if (dtOstad.Rows[0]["IsRetired"] != DBNull.Value)
                    editInfo.bazneshaste = Convert.ToBoolean(dtOstad.Rows[0]["IsRetired"]);

                if (dtOstad.Rows[0]["marital_status"] != DBNull.Value)
                    editInfo.taahol = dtOstad.Rows[0]["marital_status"].ToString() == "1" ? false : true;

                if (dtOstad.Rows[0]["jens"] != DBNull.Value)
                    editInfo.sexIsMan = Convert.ToInt32(dtOstad.Rows[0]["jens"]) == 1 ? true : false;
            }

            return editInfo;
        }

        public DataTable GetAllControlToSidaFields()
        {
            return CRD.GetAllControlToSidaFields();
        }
    }
}

